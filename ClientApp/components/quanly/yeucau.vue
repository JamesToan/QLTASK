<template>
  <div>
    <!-- start page title -->
    <div class="row">
      <div class="col-12">
        <div class="page-title-box">
          <div class="page-title-right">
            <ol class="breadcrumb m-0">
              <li class="breadcrumb-item">
                <a href="javascript: void(0);">Hồ sơ</a>
              </li>
              <li class="breadcrumb-item active">Tuyển dụng</li>
            </ol>
          </div>
          <h4 class="page-title">Hồ sơ tuyển dụng</h4>
        </div>
      </div>
    </div>
    <!-- end page title -->
    <div class="row">
      <div class="col-12">
        <div class="card-box table-responsive">
          <div class="header-title" style="margin-bottom: 10px; float: right">
            <el-input
              clearable
              v-model="search"
              placeholder="Tìm kiếm"
              style="width: 240px; float: right;"
            ></el-input>
          </div>
          <el-table
            :data="renderData()"
            border
            v-loading="loading"
            default-expand-all
            row-key="Id"
            style="width: 100%"
          >
            <!-- <el-table-column width="50" label="" align="center">
              <template></template>
            </el-table-column> -->
            <el-table-column width="50" label="STT" align="center">
              <template slot-scope="scope">
                {{ renderIndex(scope.$index) }}
              </template>
            </el-table-column>
            <el-table-column
              prop="DoanhNghiepId"
              label="Doanh nghiệp"
              width="200"
            >
              <template slot-scope="scope">
                <text-highlight :queries="search" style="word-break: normal;">
                  {{ scope.row.DoanhNghiep.TenDoanhNghiep }}
                </text-highlight>
              </template>
            </el-table-column>
            <el-table-column
              prop="TenCongViec"
              label="Tên công việc"
              min-width="150"
            >
              <template slot-scope="scope">
                <text-highlight :queries="search" style="word-break: normal;">
                  {{ scope.row.TenCongViec }}
                </text-highlight>
              </template>
            </el-table-column>
            <el-table-column
              prop="GioiTinhId"
              label="Giới tính"
              width="100"
              align="center"
            >
              <template slot-scope="scope">
                {{
                  scope.row.GioiTinh
                    ? scope.row.GioiTinh.TenGioiTinh
                    : "Nam / Nữ"
                }}
              </template>
            </el-table-column>
            <!-- <el-table-column prop="ViTriCongViecId" label="Vị trí" width="180">
              <template slot-scope="scope">
                {{ scope.row.ViTriCongViec.TenViTriCongViec }}
              </template>
            </el-table-column> -->
            <el-table-column
              prop="NoiLamViec"
              label="Nơi làm việc"
              min-width="225"
            >
              <template slot-scope="scope">
                {{ scope.row.NoiLamViec }}
              </template>
            </el-table-column>
            <el-table-column
              prop="SoLuong"
              label="Số lựơng"
              width="80"
              align="right"
            >
              <template slot-scope="scope">
                {{ scope.row.SoLuong }}
              </template>
            </el-table-column>
            <el-table-column
              prop="HanNop"
              label="Hạn nộp"
              width="100"
              align="center"
            >
              <template slot-scope="scope">
                {{ formatDate(scope.row.HanNop) }}
              </template>
            </el-table-column>
            <el-table-column align="center" label="" width="185">
              <template slot="header" slot-scope="scope">
                <el-button
                  type="primary"
                  size="small"
                  icon="el-icon-plus"
                  class="filter-item"
                  :title="scope.row"
                  @click="handleAdd"
                  v-if="allowEdit"
                  >Thêm</el-button
                >
                <el-button
                  type="success"
                  size="small"
                  icon="el-icon-download"
                  class="filter-item"
                  title="Xuất DS"
                  @click="handleExport"
                  >Xuất</el-button
                >
              </template>
              <template slot-scope="scope">
                <el-button
                  @click="handleActive(scope.$index, scope.row)"
                  :type="scope.row.XacThuc ? 'success' : 'warning'"
                  :title="scope.row.XacThuc ? 'Đã xác thực' : 'Chưa xác thực'"
                  :icon="scope.row.XacThuc ? 'el-icon-check' : 'el-icon-close'"
                  size="mini"
                ></el-button>
                <el-button
                  @click="handleView(scope.$index, scope.row)"
                  type="info"
                  title="Xem chi tiết"
                  icon="el-icon-view"
                  size="mini"
                  v-if="!allowEdit"
                ></el-button>
                <el-button
                  @click="handleEdit(scope.$index, scope.row)"
                  type="primary"
                  title="Cập nhật"
                  icon="el-icon-edit"
                  size="mini"
                  v-if="allowEdit"
                ></el-button>
                <el-button
                  @click="handleDelete(scope.row)"
                  type="danger"
                  icon="el-icon-delete"
                  title="Xóa"
                  size="mini"
                  v-if="allowEdit"
                ></el-button>
              </template>
            </el-table-column>
          </el-table>
          <el-pagination
            class="pt-2 pl-0"
            :page-size="pagination"
            background
            style="width: 100%"
            @size-change="handleSizeChange"
            :current-page.sync="activePage"
            :page-sizes="[10, 20, 50, 100, 500]"
            layout="total,sizes,prev, pager, next"
            :total="total"
          >
          </el-pagination>
        </div>
      </div>
    </div>
    <el-dialog
      title="Hồ sơ tuyển dụng"
      :visible.sync="dialogFormDisplay"
      top="15px"
      center
    >
      <el-form
        :model="formData"
        :rules="formRules"
        :disabled="!allowEdit"
        ref="formData"
        label-width="140px"
        class="m-auto"
        size="small"
      >
        <el-form-item label="Doanh nghiệp" prop="DoanhNghiepId">
          <el-select
            v-model="formData.DoanhNghiepId"
            placeholder="Chọn doanh nghiệp"
            class="w-100"
            filterable
          >
            <el-option
              v-for="item in listDoanhNghiep"
              :key="item.Id"
              :label="item.TenDoanhNghiep"
              :value="item.Id"
            >
            </el-option>
          </el-select>
        </el-form-item>
        <el-form-item label="Tên công việc" prop="NganhNgheId">
          <!-- <el-input
            v-model="formData.TenCongViec"
            type="text"
            size="small"
          ></el-input> -->
          <el-select
            v-model="formData.NganhNgheId"
            placeholder="Chọn công việc"
            class="w-100"
            clearable
            filterable
          >
            <el-option
              v-for="item in ListDMNganhNghe"
              :key="item.Id"
              :label="item.TenNganhNghe"
              :value="item.Id"
            >
            </el-option>
          </el-select>
        </el-form-item>
        <el-row>
          <el-col :span="12"
            ><el-form-item label="Vị trí công việc" prop="ViTriCongViecId">
              <el-select
                v-model="formData.ViTriCongViecId"
                placeholder="Chọn vị trí công việc"
                class="w-100"
                clearable
                filterable
              >
                <el-option
                  v-for="item in ListDMViTriCongViec"
                  :key="item.Id"
                  :label="item.TenViTriCongViec"
                  :value="item.Id"
                >
                </el-option>
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="Mức lương" prop="MucLuongId">
              <el-select
                v-model="formData.MucLuongId"
                placeholder="Chọn mức lương"
                class="w-100"
                clearable
                filterable
              >
                <el-option
                  v-for="item in ListDMMucLuong"
                  :key="item.Id"
                  :label="item.TenMucLuong"
                  :value="item.Id"
                >
                </el-option>
              </el-select>
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="12">
            <el-form-item label="Số lượng" prop="ViTriCongViecId">
              <el-input
                v-model="formData.SoLuong"
                type="number"
                size="small"
                placeholder="Số lượng tuyển dụng"
              ></el-input> </el-form-item
          ></el-col>
          <el-col :span="12">
            <el-form-item label="Thời gian làm việc" prop="ThoiGianLamViecId">
              <el-select
                v-model="formData.ThoiGianLamViecId"
                placeholder="Chọn thời gian làm việc"
                class="w-100"
                clearable
              >
                <el-option
                  v-for="item in ListDMThoiGianLamViec"
                  :key="item.Id"
                  :label="item.TenThoiGianLamViec"
                  :value="item.Id"
                >
                </el-option>
              </el-select>
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="12">
            <el-form-item label="Tuổi từ" prop="TuoiTu">
              <el-input
                v-model="formData.TuoiTu"
                type="number"
                size="small"
                placeholder="Tuổi từ"
              ></el-input>
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="đến" prop="TuoiDen">
              <el-input
                v-model="formData.TuoiDen"
                type="number"
                size="small"
                placeholder="Tuổi đến"
              ></el-input> </el-form-item
          ></el-col>
        </el-row>
        <el-row>
          <el-col :span="12">
            <el-form-item label="Giới tính" prop="GioiTinhId">
              <el-select
                v-model="formData.GioiTinhId"
                placeholder="Chọn giới tính"
                class="w-100"
                clearable
              >
                <el-option
                  v-for="item in ListDMGioiTinh"
                  :key="item.Id"
                  :label="item.TenGioiTinh"
                  :value="item.Id"
                >
                </el-option>
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="Hạn nộp hồ sơ" prop="HanNop">
              <el-date-picker
                v-model="formData.HanNop"
                type="date"
                placeholder="Chọn ngày"
                format="dd/MM/yyyy"
                size="small"
                style="width: 100%"
                value-format="yyyy-MM-dd"
              >
              </el-date-picker> </el-form-item
          ></el-col>
        </el-row>
        <el-row>
          <el-col :span="12">
            <el-form-item label="Nơi làm việc" prop="NoiLamViecTinhId">
              <el-select
                v-model="formData.NoiLamViecTinhId"
                placeholder="Chọn tỉnh"
                class="w-100"
                @change="changeTinh(formData.NoiLamViecTinhId)"
                filterable
              >
                <el-option
                  v-for="item in ListDMTinh"
                  :key="item.Id"
                  :label="item.TenTinh"
                  :value="item.Id"
                >
                </el-option>
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item prop="NoiLamViecHuyenId">
              <el-select
                v-model="formData.NoiLamViecHuyenId"
                placeholder="Chọn huyện"
                class="w-100"
                filterable
              >
                <el-option
                  v-for="item in ListDMHuyen"
                  :key="item.Id"
                  :label="item.TenHuyen"
                  :value="item.Id"
                >
                </el-option>
              </el-select>
            </el-form-item>
          </el-col>
        </el-row>
        <el-form-item label="" prop="NoiLamViec">
          <el-input
            v-model="formData.NoiLamViec"
            type="text"
            size="small"
          ></el-input>
        </el-form-item>
        <el-form-item label="Mô tả công việc" prop="MoTaCongViec">
          <ckeditor
            :editor="editor"
            v-model="formData.MoTaCongViec"
            :config="editorConfig"
            :disabled="!allowEdit"
          ></ckeditor>
        </el-form-item>
        <el-form-item label="Yêu cầu công việc" prop="YeuCauCongViec">
          <ckeditor
            :editor="editor"
            v-model="formData.YeuCauCongViec"
            :config="editorConfig"
            :disabled="!allowEdit"
          ></ckeditor>
        </el-form-item>
        <el-form-item label="Phúc lợi" prop="PhucLoi">
          <ckeditor
            :editor="editor"
            v-model="formData.PhucLoi"
            :config="editorConfig"
            :disabled="!allowEdit"
          ></ckeditor>
        </el-form-item>
      </el-form>
      <span slot="footer" class="dialog-footer" v-if="allowEdit">
        <el-button @click="resetForm" size="small">Bỏ qua</el-button>
        <el-button type="primary" size="small" @click="updateData"
          >Cập nhật</el-button
        >
      </span>
    </el-dialog>
  </div>
</template>
<script>
import ClassicEditor from "@ckeditor/ckeditor5-build-classic";
import moment from "moment";
import { getRole, getUser } from "../../store/common";
import {
  getCauHinh,
  getListDanhMucSVL,
  selectDoanhNghiep,
  addHoSoTuyenDung,
  updateHoSoTuyenDung,
  deleteHoSoTuyenDung,
  activeHoSoTuyenDung,
  getHoSoTuyenDung,
  selectHoSoTuyenDung
} from "../../store/api";
export default {
  data() {
    return {
      title: null,
      dialogFormDisplay: false,
      loading: false,
      isEditor: false,
      isXacThuc: false,
      allowEdit: false,
      search: "",
      formData: {
        UID: null,
        ViTriCongViecId: null,
        TenCongViec: null,
        SoLuong: null,
        MucLuongTu: null,
        MucLuongDen: null,
        NoiLamViec: null,
        NoiLamViecTinhId: "80",
        NoiLamViecHuyenId: null,
        ThoiGianLamViecId: null,
        HanNop: null,
        MoTaCongViec: null,
        YeuCauCongViec: null,
        PhucLoi: null
      },
      formRules: {
        DoanhNghiepId: [
          {
            required: true,
            message: "Vui lòng chọn doanh nghiệp",
            trigger: "blur"
          }
        ],
        TenCongViec: [
          {
            required: true,
            message: "Vui lòng nhập tên công việc",
            trigger: "blur"
          }
        ],
        NganhNgheId: [
          {
            required: true,
            message: "Vui lòng chọn nghề nghiệp",
            trigger: "blur"
          }
        ],
        ViTriCongViecId: [
          {
            required: true,
            message: "Vui lòng chọn vị trí công việc",
            trigger: "blur"
          }
        ],
        SoLuong: [
          {
            required: true,
            message: "Vui lòng nhập số lượng tuyển dụng",
            trigger: "blur"
          }
        ]
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
      listDoanhNghiep: [],
      ListDMGioiTinh: [],
      ListDMTinh: [],
      ListDMHuyen: [],
      ListDMNganhNghe: [],
      ListDMMucLuong: [],
      ListDMThoiGianLamViec: [],
      ListDMViTriCongViec: [],
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
    // formatDiaChi(tinh, huyen, noi) {
    //   return (
    //     (tinh ? tinh.TenTinh : "") +
    //     (huyen ? ", " + huyen.TenHuyen : "") +
    //     (noi ? ", " + noi : "")
    //   );
    // },

    formatJson(filterVal, jsonData) {
      return jsonData.map(data =>
        filterVal.map(f => {
          let result;
          if (f.includes(".")) {
            let f1 = f.split(".")[0];
            let f2 = f.split(".")[1];
            result = data[f1][f2];
          } else {
            if (f.startsWith("HanNop")) {
              return this.formatDate(data[f]);
            }
            result = data[f];
          }
          if (f == "DoanhNghiep") {
            return result ? result.TenDoanhNghiep : "";
          }
          if (f == "GioiTinh") {
            return result ? result.TenGioiTinh : "";
          }
          if (f == "ThoiGianLamViec") {
            return result ? result.TenThoiGianLamViec : "";
          }
          if (f == "MucLuong") {
            return result ? result.TenMucLuong : "";
          }
          return result;
        })
      );
    },
    changeTinh(val) {
      var tinh = this.ListDMTinh.find(obj => obj.Id == val);
      if (tinh) {
        this.ListDMHuyen = tinh.Huyen;
      } else {
        this.ListDMHuyen = [];
      }
      delete this.formData.NoiLamViecHuyenId;
    },
    handleAdd() {
      if (this.$refs.formData !== undefined) {
        this.$refs.formData.resetFields();
      }
      this.formData = {
        NoiLamViecTinhId: "80"
      };

      this.isEditor = false;
      this.dialogFormDisplay = true;
    },
    handleActive(index, row) {
      row.XacThuc = !row.XacThuc;
      activeHoSoTuyenDung(row).then(data => {
        //console.log(data);
        this.$message({
          type: "success",
          message: row.XacThuc
            ? "Xác thực thành công!"
            : "Hủy xác thực thành công!"
        });
        this.getListData();
      });
    },
    handleEdit(index, row) {
      if (this.$refs.formData !== undefined) {
        this.$refs.formData.resetFields();
      }
      this.formData = Object.assign({}, row);

      this.isEditor = true;
      this.dialogFormDisplay = true;
    },
    handleView(index, row) {
      if (this.$refs.formData !== undefined) {
        this.$refs.formData.resetFields();
      }
      this.formData = Object.assign({}, row);
      this.dialogFormDisplay = true;
    },
    handleDelete(row) {
      this.$confirm("Bạn có chắc chắn muốn xóa?", "Thông báo", {
        confirmButtonText: "OK",
        cancelButtonText: "Cancel",
        type: "warning"
      }).then(() => {
        deleteHoSoTuyenDung(row.Id).then(data => {
          this.$message({
            type: "success",
            message: "Xóa thành công!"
          });
          this.getListData();
        });
      });
    },

    handleExport() {
      import("../../vendor/Export2Excel").then(excel => {
        const tHeader = [
          "Doanh nghiệp",
          "Tên công việc",
          "Giới tính",
          "Tuổi từ",
          "Tuổi đến",
          "Nơi làm việc",
          "Thời gian làm việc",
          "Mức lương",
          "Số lượng",
          "Kinh nghiệm",
          "Yêu cầu",
          "Hạn nộp"
        ];
        const filterVal = [
          "DoanhNghiep",
          "TenCongViec",
          "GioiTinh",
          "TuoiTu",
          "TuoiDen",
          "NoiLamViec",
          "ThoiGianLamViec",
          "MucLuong",
          "SoLuong",
          "KinhNghiem",
          "YeuCauCongViec",
          "HanNop"
        ];
        const data = this.formatJson(filterVal, this.listData);
        console.log(data);
        var filename = "DSDoanhNghiep_" + moment().format("YYYYMMDD_hhmmss");
        excel.export_json_to_excel({
          header: tHeader,
          data,
          filename: filename
        });
      });
    },

    resetForm() {
      this.dialogFormDisplay = false;
      return true;
    },
    getListData() {
      this.loading = true;
      if (this.RoleId == 2) {
        //Quản lý
        selectHoSoTuyenDung().then(data => {
          this.listData = data;
          this.total = data.length;

          this.isXacThuc = true;
          this.loading = false;
        });
      }
      if (this.RoleId == 4) {
        //DoanhNghiep
        getHoSoTuyenDung(false)
          .then(response => {
            this.listData = response.data;
            this.total = response.data.length;

            this.isXacThuc = true;
            this.loading = false;
          })
          .catch(error => {
            this.$message.error(
              "Chưa cập nhật thông tin tài khoản doanh nghiệp hoặc chưa được xác nhận!"
            );
            this.loading = false;
          });
      }
    },

    updateData() {
      this.$refs.formData.validate(valid => {
        if (valid) {
          if (this.isEditor == 0) {
            //delete this.formData.LinhVuc;
            addHoSoTuyenDung(this.formData).then(data => {
              //console.log(data);
              this.getListData();
            });
          } else {
            //delete this.formData.LinhVuc;
            updateHoSoTuyenDung(this.formData).then(data => {
              //console.log(data);
              this.getListData();
            });
          }
          this.dialogFormDisplay = false;
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
        return (
          post.DoanhNghiep.TenDoanhNghiep.toLowerCase().includes(
            this.search.toLowerCase()
          ) ||
          post.TenCongViec.toLowerCase().includes(this.search.toLowerCase())
        );
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
    getCauHinh("ChoPhepCapNhatDN").then(data => {
      if (data) {
        this.allowEdit = data[0].GiaTri == "1";
        console.log(this.allowEdit);
      }
    });

    this.getListData();

    getListDanhMucSVL().then(data => {
      if (data) {
        this.ListDMGioiTinh = data.DMGioiTinh;
        this.ListDMTinh = data.DMTinh;
        this.ListDMHuyen = data.DMHuyen;
        this.ListDMNganhNghe = data.DMNganhNghe;
        this.ListDMMucLuong = data.DMMucLuong;
        this.ListDMThoiGianLamViec = data.DMThoiGianLamViec;
        this.ListDMViTriCongViec = data.DMViTriCongViec;
      }
    });
    selectDoanhNghiep().then(data => {
      if (data) {
        this.listDoanhNghiep = data;
      }
    });
  }
};
</script>
<style></style>
