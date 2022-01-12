<template>
  <div>
    <!-- start page title -->
    <div class="row">
      <div class="col-12">
        <div class="page-title-box">
          <div class="page-title-right">
            <ol class="breadcrumb m-0">
              <li class="breadcrumb-item">
                <a href="javascript: void(0);">Quản lý</a>
              </li>
              <li class="breadcrumb-item active">Yêu cầu</li>
            </ol>
          </div>
          <h4 class="page-title">Quản lý yêu cầu</h4>
        </div>
      </div>
    </div>
    <!-- end page title -->
    <div class="row">
      <div class="col-12">
        <div class="card-box table-responsive">
          <div class="header-title" style="margin-bottom: 10px; float: right">
            <el-input clearable
                      v-model="search"
                      placeholder="Tìm kiếm"
                      style="width: 240px; float: right;"></el-input>
          </div>
          <el-table :data="renderData()"
                    border
                    v-loading="loading"
                    default-expand-all
                    row-key="Id"
                    style="width: 100%">
            <!-- <el-table-column width="50" label="" align="center">
    <template></template>
  </el-table-column> -->
            <el-table-column width="50" label="STT" align="center">
              <template slot-scope="scope">
                {{ renderIndex(scope.$index) }}
              </template>
            </el-table-column>
            <el-table-column prop="TenYeuCau" label="Yêu cầu" width="150">
              <template slot-scope="scope">
                <text-highlight :queries="search" style="word-break: normal;">
                  {{ scope.row.TenYeuCau }}
                </text-highlight>
              </template>
            </el-table-column>
            <el-table-column prop="NoiDung"
                             label="Nội Dung"
                             min-width="250">
              <template slot-scope="scope">
                <text-highlight :queries="search" style="word-break: normal;" v-html="scope.row.NoiDung">
                  <!--{{ scope.row.NoiDung }}-->
                </text-highlight>
              </template>
            </el-table-column>
            <el-table-column prop="JiraDaGui"
                             label="Jira Đã Gửi"
                             width="120">
              <template slot-scope="scope">
                <text-highlight :queries="search" style="word-break: normal;">
                  {{ scope.row.JiraDaGui}}
                </text-highlight>
                <!--<el-link href="scope.row.JiraDaGui" target="_blank">{{ scope.row.JiraDaGui}}</el-link>-->
              </template>
            </el-table-column>
            <el-table-column prop="ThoiHan"
                             label="Thời Hạn"
                             width="120"
                             align="center">
              <template slot-scope="scope">
                {{ formatDate(scope.row.ThoiHan) }}
              </template>
            </el-table-column>
            <el-table-column prop="StateId"
                             label="Trạng Thái"
                             width="120">
              <template slot-scope="scope">
                <text-highlight :queries="search" style="word-break: normal;">
                  {{ scope.row.States ? scope.row.States.StateName : ""}}
                </text-highlight>
              </template>
            </el-table-column>

            <el-table-column prop="Status" label="Tình trạng" width="125">

              <template slot-scope="scope" >
                <span v-if="new Date(scope.row.ThoiHan) < Date.now()">
                  Trễ hạn
                </span>
                <span v-if="new Date(scope.row.ThoiHan) >= Date.now()">
                  Trong hạn
                </span>
              </template>
             
            </el-table-column>
            <el-table-column align="center" label="" width="185">
              <template slot="header" slot-scope="scope">
                <el-button type="primary"
                           size="small"
                           icon="el-icon-plus"
                           class="filter-item"
                           :title="scope.row"
                           @click="handleAdd"
                           v-if="allowEdit">Thêm</el-button>
                <!--<el-button type="success"
                           size="small"
                           icon="el-icon-download"
                           class="filter-item"
                           title="Xuất DS"
                           @click="handleExport">Xuất</el-button>-->
              </template>
              <template slot-scope="scope">
                <!--<el-button @click="handleActive(scope.$index, scope.row)"
                 :type="scope.row.XacThuc ? 'success' : 'warning'"
                 :title="scope.row.XacThuc ? 'Đã xác thực' : 'Chưa xác thực'"
                 :icon="scope.row.XacThuc ? 'el-icon-check' : 'el-icon-close'"
                 size="mini"></el-button>-->
                <el-button @click="handleEdit(scope.$index, scope.row)"
                           type="primary"
                           :title="allowEdit ? 'Cập nhật' : 'Xem chi tiết'"
                           :icon="allowEdit ? 'el-icon-edit' : 'el-icon-view'"
                           size="mini"></el-button>
                <el-button @click="handleDelete(scope.row)"
                           type="danger"
                           icon="el-icon-delete"
                           title="Xóa"
                           size="mini"
                           v-if="allowEdit"></el-button>
              </template>
            </el-table-column>
          </el-table>
          <el-pagination class="pt-2 pl-0"
                         :page-size="pagination"
                         background
                         style="width: 100%"
                         @size-change="handleSizeChange"
                         :current-page.sync="activePage"
                         :page-sizes="[10, 20, 50, 100, 500]"
                         layout="total,sizes,prev, pager, next"
                         :total="total">
          </el-pagination>
        </div>
      </div>
    </div>
    <el-dialog title="Quản lý yêu cầu"
               :visible.sync="dialogFormDisplay"
               top="15px"
               center>
      <el-form :model="formData"
               :rules="formRules"
               ref="formData"
               label-width="140px"
               class="m-auto"
               size="small"
               :disabled="!allowEdit">
        <el-form-item label="Tiêu đề yêu cầu" prop="TenYeuCau">
          <el-input v-model="formData.TenYeuCau"
                    type="text"
                    size="small"></el-input>
        </el-form-item>

        <el-row>
          <el-col :span="12">
            <el-form-item label="Ngày yêu cầu" prop="NgayYeuCau">
              <el-date-picker v-model="formData.NgayYeuCau"
                              type="date"
                              placeholder="Chọn ngày"
                              format="dd/MM/yyyy"
                              size="small"
                              style="width: 100%"
                              value-format="yyyy-MM-dd">
              </el-date-picker>
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="Thời hạn" prop="ThoiHan">
              <el-date-picker v-model="formData.ThoiHan"
                              type="date"
                              placeholder="Chọn ngày"
                              format="dd/MM/yyyy"
                              size="small"
                              style="width: 100%"
                              value-format="yyyy-MM-dd">
              </el-date-picker>
            </el-form-item>
          </el-col>

        </el-row>

        <el-row>
          <el-col :span="12">
            <el-form-item label="Trạng thái" prop="StateId">
              <el-select v-model="formData.StateId"
                         placeholder="Chọn trạng thái"
                         class="w-100">
                <el-option v-for="item in ListDMTrangThai"
                           :key="item.Id"
                           :label="item.StateName"
                           :value="item.Id">
                </el-option>
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="Nhân Sự" prop="NhanSuId">
              <el-select v-model="formData.NhanSuId"
                         placeholder="Chọn nhân sự"
                         class="w-100">
                <el-option v-for="item in ListDMNhanSu"
                           :key="item.Id"
                           :label="item.TenNhanSu"
                           :value="item.Id">
                </el-option>
              </el-select>
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="12">
            <el-form-item label="Dịch vụ" prop="DichVuId">
              <el-select v-model="formData.DichVuId"
                         placeholder="Chọn dịch vụ"
                         @change="changeDichVu(formData.DichVuId)"
                         class="w-100">
                <el-option v-for="item in ListDMDichVu"
                           :key="item.Id"
                           :label="item.TenDichVu"
                           :value="item.Id">
                </el-option>
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="Đơn vị yêu cầu" prop="DonVi">
              <el-input v-model="formData.DonVi"
                        type="text"
                        size="small"></el-input>
            </el-form-item>
          </el-col>
        </el-row>
        <el-form-item label="Nội dung yêu cầu" prop="NoiDung">
          <ckeditor :editor="editor"
                    v-model="formData.NoiDung"
                    :config="editorConfig"
                    :disabled="!allowEdit"></ckeditor>
        </el-form-item>

        <el-form-item label="Jira" prop="JiraDaGui">
          <el-input v-model="formData.JiraDaGui"
            type="text"
            size="small"></el-input>
            
            <!--<el-button type="primary" size="small" @click="handleAddJira">Thêm</el-button>-->
            <!--<el-button size="small" @click="updateData">Xóa</el-button>-->
        </el-form-item>
        <!--<el-row>
    <el-col :span="12">
      <el-form-item label="Địa chỉ" prop="DiaChiTinhId">
        <el-select v-model="formData.DiaChiTinhId"
                   placeholder="Chọn tỉnh"
                   class="w-100"
                   @change="changeTinh(formData.DiaChiTinhId)"
                   filterable>
          <el-option v-for="item in ListDMTinh"
                     :key="item.Id"
                     :label="item.TenTinh"
                     :value="item.Id">
          </el-option>
        </el-select>
      </el-form-item>
    </el-col>
    <el-col :span="12">
      <el-form-item prop="DiaChiHuyenId">
        <el-select v-model="formData.DiaChiHuyenId"
                   placeholder="Chọn huyện"
                   class="w-100"
                   filterable>
          <el-option v-for="item in ListDMHuyen"
                     :key="item.Id"
                     :label="item.TenHuyen"
                     :value="item.Id">
          </el-option>
        </el-select>
      </el-form-item>
    </el-col>
  </el-row>
  <el-form-item prop="DiaChi">
    <el-input v-model="formData.DiaChi"
              type="text"
              size="small"></el-input>
  </el-form-item>-->
      </el-form>
      <span slot="footer" class="dialog-footer" v-if="allowEdit">
        <el-button @click="resetForm" size="small">Bỏ qua</el-button>
        <el-button type="primary" size="small" @click="updateData">Cập nhật</el-button>
      </span>
    </el-dialog>

    <el-dialog title="Quản lý Jira"
               :visible.sync="dialogFormDisplayJira"
               top="165px"
               center>
      <el-form :model="formData1"
               :rules="formRules"
               ref="formData1"
               label-width="140px"
               class="m-auto"
               size="small"
               :disabled="!allowEdit">

        <el-form-item label="Tên Jira" prop="TenJira">
          <el-input v-model="formData1.TenJira"
                    type="text"
                    size="small"></el-input>
        </el-form-item>
        <el-form-item label="Link Jira" prop="LinkJira">
          <el-input v-model="formData1.LinkJira"
                    type="text"
                    size="small"></el-input>
        </el-form-item>
        <!--<el-form-item label="Yêu Cầu Id" prop="YeuCauId">
          <el-input v-model="formData.Id"
                   
                    type="text"
                    size="small"></el-input>
        </el-form-item>-->
      </el-form>
      <span slot="footer" class="dialog-footer" v-if="allowEdit">
        <el-button @click="resetFormJira" size="small">Bỏ qua</el-button>
        <el-button type="primary" size="small" @click="updateDataJira">Cập nhật</el-button>
      </span>
    </el-dialog>
  </div>
</template>
<script>
import ClassicEditor from "@ckeditor/ckeditor5-build-classic";
import moment from "moment";
import { getRole, getUser } from "../../store/common";
import {
  getYeuCau,
  getListDanhMucYeuCau,
  selectYeuCau,
  updateYeuCau,
  deleteYeuCau,
  addYeuCau,
  addJira,
  selectJira,
  updateJira,
  deleteJira
} from "../../store/api";
export default {
  data() {
    return {
      title: null,
      dialogFormDisplay: false,
      dialogFormDisplayJira: false,
      loading: false,
      isEditor: false,
      isXacThuc: false,
      allowEdit: true,
      search: "",
      formData: {
        Id: null,
        TenYeuCau: null,
        NoiDung: null,
        ThoiHan: null,
        JiraDaGui: null,
        StatusId: null,
        StateId: null,
        NhanSuId: null,
        DonVi: null,
        DichVuId: null,
        NgayYeuCau: null,
       
        domains: [{
          key: 1,
          value:''
        }]
      },
      formData1: {
        TenJira: null,
        LinkJira: null,
        YeuCauId:null
      },
      formRules: {
        TenYeuCau: [
          {
            required: true,
            message: "Nhập tiêu đề yêu cầu",
            trigger: "blur"
          }
        ],
        NoiDung: [
          {
            required: true,
            message: "Vui lòng nhập nội dung",
            trigger: "blur"
          }
        ],
        ThoiHan: [
          {
            required: true,
            message: "Vui lòng chọn thời hạn",
            trigger: "blur"
          }
        ],
        NgayYeuCau: [
          {
            required: true,
            message: "Vui lòng chọn ngày yêu cầu",
            trigger: "blur"
          }
        ],
        StateId: [
          {
            required: true,
            message: "Vui lòng chọn trạng thái",
            trigger: "blur"
          }
        ],
        NhanSuId: [
          {
            required: true,
            message: "Vui lòng chọn nhân sự",
            trigger: "blur"
          }
        ],
        DonVi: [
          {
            required: true,
            message: "Vui lòng nhập đơn vị yêu cầu",
            trigger: "blur"
          }
        ],
        DichVuId: [
          {
            required: true,
            message: "Vui lòng chọn dịch vụ",
            trigger: "blur"
          }
        ],
        TenJira: [
          {
            required: true,
            message: "Vui lòng nhập dữ liệu",
            trigger: "blur"
          }
        ],
        LinkJira: [
          {
            required: true,
            message: "Vui lòng nhập dữ liệu",
            trigger: "blur"
          }
        ],
      },
      editor: ClassicEditor,
      editorConfig: {
        // The configuration of the editor.
        ckfinder: {
          // Upload the images to the server using the CKFinder QuickUpload command.
          uploadUrl:
            "/api/TapTin/CKUpload?command=QuickUpload&type=Images&responseType=json"
        }
      },
      RoleId: getRole(),
      listData: [],
      ListDMTrangThai: [],
      ListDMNhanSu: [],
      ListDMDichVu: [],
      ListDMJira: [],
      ListDMTinhTrang: [],
      ListJira: [],
      pagination: 10,
      total: 10,
      activePage: 1,
      dataFilter: null
    };
  },

  computed: {
    //...mapState({
    //  currentCount: state => state.counter
    //})
  },

  methods: {
    formatDate(date) {
      if (date) {
        return moment(date).format("DD/MM/YYYY");
      } else return null;
    },
   
    formatJson(filterVal, jsonData) {
      return jsonData.map(data =>
        filterVal.map(f => {
          let result;
          if (f.includes(".")) {
            let f1 = f.split(".")[0];
            let f2 = f.split(".")[1];
            result = data[f1][f2];
          } else {
            if (f.startsWith("Ngay")) {
              return this.formatDate(data[f]);
            }
            result = data[f];
          }
          if (f == "Status") {
            return result ? result.StatusName : "";
          }
          if (f == "NhanSu") {
            return result ? result.TenNhanSu : "";
          }
          if (f == "DichVu") {
            return result ? result.DichVu : "";
          }
          if (f == "States") {
            return result ? result.StateName : "";
          }
          return result;
        })
      );
    },
    changeDichVu(val) {
      //console.log(val)
      var dv = this.ListDMDichVu.find(obj => obj.Id == val);
      if (dv) {
        this.formData.DonVi = dv.DonViYeuCau;
       
      } else {
        this.DonVi = [];
      }
    },
    handleAdd() {
      if (this.$refs.formData !== undefined) {
        this.$refs.formData.resetFields();
      }
      this.formData = {
        //IsActive = true
      };
      this.ListJira = [];
      this.isEditor = false;
      this.dialogFormDisplay = true;
    },

    handleAddJira() {
      if (this.$refs.formData1 !== undefined) {
        this.$refs.formData1.resetFields();
      }
      
      this.formData1 = {
        YeuCauId: this.formData.Id
      };

      this.isEditor = false;
      this.dialogFormDisplayJira = true;
    },
    //handleActive(index, row) {
    //  row.XacThuc = !row.XacThuc;
    //  activeNguoiLaoDong(row).then(data => {
    //    //console.log(data);
    //    this.$message({
    //      type: "success",
    //      message: row.XacThuc
    //        ? "Xác thực thành công!"
    //        : "Hủy xác thực thành công!"
    //    });
    //    this.getListData();
    //  });
    //},
    handleEdit(index, row) {
      if (this.$refs.formData !== undefined) {
        this.$refs.formData.resetFields();
      }
      this.formData = Object.assign({}, row);
      if (this.formData.JiraDaGui) {
        this.ListJira = [];
        
        var _arr = this.formData.JiraDaGui;
        this.ListJira = _arr.split(",");
       
      }
      
      this.isEditor = true;
      this.dialogFormDisplay = true;
    },
    
    handleDelete(row) {
      this.$confirm("Bạn có chắc chắn muốn xóa?", "Thông báo", {
        confirmButtonText: "OK",
        cancelButtonText: "Cancel",
        type: "warning"
      }).then(() => {
        deleteYeuCau(row.Id).then(data => {
          this.$message({
            type: "success",
            message: "Xóa thành công!"
          });
          this.getListData();
        });
      });
    },
    handleSuccess(response, file, ListJira) {
      this.fileDoc.push({ key: file.name, file: response.file });
    },
    handleRemove(file, ListJira) {
      let i = this.fileDoc.map(item => item.key).indexOf(file.name);
      this.fileDoc.splice(i, 1);
    },
    //handleExport() {
    //  import("../../vendor/Export2Excel").then(excel => {
    //    const tHeader = [
    //      "Họ tên",
    //      "Giới tính",
    //      "Ngày sinh",
    //      "Số điện thoại",
    //      "Email",
    //      "Địa chỉ",
    //      "Huyện",
    //      "Tỉnh",
    //      "Tình trạng"
    //    ];
    //    const filterVal = [
    //      "HoTen",
    //      "GioiTinh",
    //      "NgaySinh",
    //      "SoDienThoai",
    //      "Email",
    //      "DiaChi",
    //      "DiaChiHuyen",
    //      "DiaChiTinh",
    //      "TinhTrang"
    //    ];
    //    const data = this.formatJson(filterVal, this.listData);
    //    console.log(data);
    //    var filename = "DSNguoiLaoDong_" + moment().format("YYYYMMDD_hhmmss");
    //    excel.export_json_to_excel({
    //      header: tHeader,
    //      data,
    //      filename: filename
    //    });
    //  });
    //},
    resetForm() {
      this.dialogFormDisplay = false;
      return true;
    },
    resetFormJira() {
      this.dialogFormDisplayJira = false;
      return true;
    },
    getListData() {
      this.loading = true;
      
        //Quản lý
        this.loading = true;

        selectYeuCau(true).then(data => {
          this.listData = data;
          this.total = data.length;
          //this.isXacThuc = true;
          this.loading = false;
        });
      
    },
    getListDataJira() {
      this.loading = true;

      //Quản lý
      this.loading = true;

      selectJira(this.formData.Id).then(data => {
        this.listData = data;
        this.total = data.length;

        //this.isXacThuc = true;
        this.loading = false;
      });

    },

    updateData() {
      this.$refs.formData.validate(valid => {
        if (valid) {
          if (this.isEditor == 0) {
            addYeuCau(this.formData).then(data => {
              //console.log(data);
              this.$message({
                type: "success",
                message: "Thêm mới thành công!"
              });
              this.getListData();
            });
          } else {
            //delete this.formData.LinhVuc;
            updateYeuCau(this.formData).then(data => {
              //console.log(data);
              this.$message({
                type: "success",
                message: "Cập nhật thành công!"
              });
              this.getListData();
            });
          }
          this.dialogFormDisplay = false;
        } else {
          return false;
        }
      });
    },
    updateDataJira() {
      this.$refs.formData1.validate(valid => {
        if (valid) {
          if (this.isEditor == 0) {
            addJira(this.formData1).then(data => {
              //console.log(data);
              this.$message({
                type: "success",
                message: "Thêm mới thành công!"
              });
            });
          } else {
            //delete this.formData.LinhVuc;
            updateJira(this.formData1).then(data => {
              //console.log(data);
              this.$message({
                type: "success",
                message: "Cập nhật thành công!"
              });
            });
          }
          this.dialogFormDisplayJira = false;
        } else {
          return false;
        }
      });
    },
    handleSizeChange(val) {
      this.pagination = val;
    },
    renderData() {
      var _data = this.listData.filter(post => {
        return post.TenYeuCau.toLowerCase().includes(this.search.toLowerCase());
      });
      this.total = _data.length;
      return _data.slice(
        (this.activePage - 1) * this.pagination,
        (this.activePage - 1) * this.pagination + this.pagination
      );
    },
    renderIndex(idx) {
      return idx + this.pagination * (this.activePage - 1) + 1;
    }
  },

  created() {
    //getCauHinh("ChoPhepCapNhatDN").then(data => {
    //  if (data) {
    //    this.allowEdit = data[0].GiaTri == "1";
    //    console.log(this.allowEdit);
    //  }
    //});
    getListDanhMucYeuCau().then(data => {
      if (data) {
        this.ListDMNhanSu = data.DMNhanSu;
        this.ListDMTinhTrang = data.DMTinhTrang;
        this.ListDMTrangThai = data.DMTrangThai;
        this.ListDMDichVu = data.DMDichVu;
        this.ListDMJira = data.DMJira;
       
      }
    });

    this.getListData();
    
  }
};
</script>
<style>
  .el-input.is-disabled .el-input__inner {
    background-color: #fff !important;
    color: #606266 !important;
  }
  .ck-editor__editable_inline {
    min-height: 200px;
  }
</style>
