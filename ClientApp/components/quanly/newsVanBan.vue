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
              <li class="breadcrumb-item active">Văn bản</li>
            </ol>
          </div>
          <h4 class="page-title">Quản lý Văn bản</h4>
        </div>
      </div>
    </div>
    <!-- end page title -->
    <div class="row" v-loading="loading">
      <div class="col-12">
        <div class="card-box table-responsive">
          <div class="header-title pb-3">
            <el-input
              clearable
              v-model="search"
              placeholder="Tìm kiếm"
              style="width: 240px; float: right;"
            ></el-input>
            <el-select
              style="width: 240px; float: left;"
              v-model="ChuyenMucIdFilter"
              @change="changeChuyenMucFilter"
              placeholder="Chọn chuyên mục"
            >
              <el-option
                v-for="item in ListDMChuyenMuc"
                :key="item.Id"
                :label="item.TenChuyenMuc"
                :value="item.Id"
              >
              </el-option>
            </el-select>
          </div>
          <el-table
            :data="renderData()"
            border
            default-expand-all
            row-key="Id"
            class="w-100 mt-3"
          >
            <!-- <el-table-column width="50" label="" align="center">
              <template></template>
            </el-table-column> -->
            <el-table-column width="50" label="STT" align="center">
              <template slot-scope="scope">
                {{ renderIndex(scope.$index) }}
              </template>
            </el-table-column>
            <el-table-column prop="LinhVucId" label="Lĩnh vực" width="120">
              <template slot-scope="scope">
                <text-highlight :queries="search" style="word-break: normal;">
                  {{ scope.row.LinhVuc.TenLinhVuc }}
                </text-highlight>
              </template>
            </el-table-column>
            <el-table-column prop="SoKyHieu" label="Số, ký hiệu" width="120">
              <template slot-scope="scope">
                <text-highlight :queries="search" style="word-break: normal;">
                  {{ scope.row.SoKyHieu }}
                </text-highlight>
              </template>
            </el-table-column>
            <el-table-column
              prop="NgayThang"
              label="Ngày tháng"
              width="100"
              align="center"
            >
              <template slot-scope="scope">
                {{ formatDate(scope.row.NgayThang) }}
              </template>
            </el-table-column>
            <el-table-column prop="TrichYeu" label="Trích yếu" min-width="200">
              <template slot-scope="scope">
                <text-highlight :queries="search" style="word-break: normal;">
                  {{ formatTrichYeu(scope.row.LoaiVanBan, scope.row.TrichYeu) }}
                </text-highlight>
              </template>
            </el-table-column>
            <el-table-column
              prop="VanBan"
              label="Tập tin"
              width="70"
              align="center"
            >
              <template slot-scope="scope">
                <div v-for="item in formatList(scope.row.VanBan)" :key="item">
                  <el-tooltip placement="top" :content="item">
                    <el-link
                      icon="el-icon-document"
                      :href="'../../uploads/' + item"
                      target="_blank"
                      :underline="false"
                    >
                      <!-- {{formatTenTapTin(item)}} -->
                    </el-link>
                  </el-tooltip>
                </div>
              </template>
            </el-table-column>
            <el-table-column align="center" label="" width="180">
              <template slot="header">
                <el-button
                  type="primary"
                  size="small"
                  icon="el-icon-plus"
                  class="filter-item"
                  @click="handleAdd"
                  >Thêm mới</el-button
                >
              </template>
              <template slot-scope="scope">
                <el-button
                  @click="handleActive(scope.$index, scope.row)"
                  :type="scope.row.NguoiDuyetId ? 'success' : 'warning'"
                  :title="scope.row.NguoiDuyetId ? 'Đã duyệt' : 'Chưa duyệt'"
                  :icon="
                    scope.row.NguoiDuyetId ? 'el-icon-check' : 'el-icon-close'
                  "
                  size="mini"
                ></el-button>
                <el-button
                  @click="handleEdit(scope.$index, scope.row)"
                  type="primary"
                  title="Cập nhật"
                  icon="el-icon-edit"
                  size="mini"
                ></el-button>
                <el-button
                  @click="handleDelete(scope.row)"
                  type="danger"
                  icon="el-icon-delete"
                  title="Xóa"
                  size="mini"
                  v-if="RoleId == roleActive"
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
      title="Quản lý Văn bản"
      :visible.sync="dialogFormDisplay"
      top="15px"
      center
    >
      <el-form
        :model="formData"
        :rules="formRules"
        ref="formData"
        label-width="140px"
        class="m-auto"
        size="small"
        ><el-form-item label="Chuyên mục" prop="ChuyenMucId">
          <el-select
            v-model="formData.ChuyenMucId"
            placeholder="Chọn chuyên mục"
            class="w-100"
            disabled
          >
            <el-option
              v-for="item in ListDMChuyenMuc"
              :key="item.Id"
              :label="item.TenChuyenMuc"
              :value="item.Id"
            >
            </el-option>
          </el-select>
        </el-form-item>
        <el-form-item label="Lĩnh vực" prop="LinhVucId">
          <el-select
            v-model="formData.LinhVucId"
            placeholder="Chọn lĩnh vực"
            class="w-100"
          >
            <el-option
              v-for="item in ListDMLinhVuc"
              :key="item.Id"
              :label="item.TenLinhVuc"
              :value="item.Id"
            >
            </el-option>
          </el-select>
        </el-form-item>
        <el-row>
          <el-col :span="12">
            <el-form-item label="Số, ký hiệu" prop="SoKyHieu">
              <el-input
                v-model="formData.SoKyHieu"
                type="text"
                size="small"
              ></el-input>
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="Ngày tháng" prop="NgayThang">
              <el-date-picker
                v-model="formData.NgayThang"
                type="date"
                placeholder="Chọn ngày"
                format="dd/MM/yyyy"
                size="small"
                style="width: 100%"
                value-format="yyyy-MM-dd"
              >
              </el-date-picker>
            </el-form-item>
          </el-col>
        </el-row>
        <el-form-item label="Trích yếu văn bản" prop="TrichYeu">
          <el-input
            v-model="formData.TrichYeu"
            type="textarea"
            rows="3"
            size="small"
          ></el-input>
        </el-form-item>
        <el-form-item label="Loại văn bản" prop="LoaiVanBanId">
          <el-select
            v-model="formData.LoaiVanBanId"
            placeholder="Chọn loại văn bản"
            class="w-100"
            clearable
          >
            <el-option
              v-for="item in ListDMLoaiVanBan"
              :key="item.Id"
              :label="item.TenLoaiVanBan"
              :value="item.Id"
            >
            </el-option>
          </el-select>
        </el-form-item>
        <el-form-item label="Cơ quan ban hành" prop="CoQuanBanHanh">
          <el-input
            v-model="formData.CoQuanBanHanh"
            type="text"
            size="small"
          ></el-input>
        </el-form-item>
        <el-form-item label="Tập tin đính kèm">
          <el-upload
            action="/api/TapTin/UploadDoc"
            :limit="3"
            :multiple="true"
            :on-preview="handlePreview"
            :on-remove="handleRemove"
            :file-list="fileList"
            :on-success="handleSuccess"
            :before-upload="beforeUpload"
            accept=".pdf,.doc,.docx"
            :auto-upload="true"
            size="mini"
          >
            <el-button size="small" type="primary">Tải lên</el-button>
          </el-upload>
        </el-form-item>
      </el-form>
      <span slot="footer" class="dialog-footer">
        <el-button @click="resetForm" size="small">Bỏ qua</el-button>
        <el-button type="primary" size="small" @click="updateData"
          >Cập nhật</el-button
        >
      </span>
    </el-dialog>
    <el-dialog
      top="50px"
      :title="dialogFileUrl"
      :visible.sync="dialogVisibleDoc"
      width="85%"
    >
      <embed
        :src="dialogFileUrl + '#navpanes=0&scrollbar=0'"
        width="100%"
        height="500px"
      />
    </el-dialog>
  </div>
</template>
<script>
import moment from "moment";
import { getRole, getUser } from "../../store/common";
import {
  getCauHinh,
  getListDanhMucNews,
  selectNewsVanBan,
  addNewsVanBan,
  updateNewsVanBan,
  activeNewsVanBan,
  deleteNewsVanBan
} from "../../store/api";
export default {
  data() {
    return {
      title: null,
      dialogFormDisplay: false,
      dialogVisibleDoc: false,
      dialogFileUrl: null,
      loading: false,
      isEditor: false,
      roleActive: true,
      search: "",
      ChuyenMucIdFilter: 11,
      formData: {
        ChuyenMucId: null,
        LinhVucId: null,
        LoaiVanBanId: null,
        SoKyHieu: null,
        NgayThang: null,
        TrichYeu: null,
        CoQuanBanHanh: null
      },
      formRules: {
        ChuyenMucId: [
          {
            required: true,
            message: "Vui lòng chọn chuyên mục",
            trigger: "blur"
          }
        ],
        LinhVucId: [
          {
            required: true,
            message: "Vui lòng chọn lĩnh vực",
            trigger: "blur"
          }
        ],
        SoKyHieu: [
          {
            required: true,
            message: "Vui lòng nhập Số, ký hiệu",
            trigger: "blur"
          }
        ],
        TrichYeu: [
          {
            required: true,
            message: "Vui lòng nhập Trích yếu Văn bản",
            trigger: "blur"
          }
        ]
      },
      RoleId: getRole(),
      listData: [],
      ListDMChuyenMuc: [],
      ListDMLinhVuc: [],
      ListDMLoaiVanBan: [],
      fileList: [],
      fileDoc: [],
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
    formatTrichYeu(loai, trichYeu) {
      if (loai) {
        return loai.TenLoaiVanBan + " " + trichYeu;
      } else return trichYeu;
    },
    formatList(val) {
      if (val) {
        return JSON.parse(val);
      } else return [];
    },
    formatTenTapTin(val) {
      if (val) {
        return val.substring(val.length - 18, val.length);
      } else return null;
    },
    changeChuyenMucFilter() {
      this.getListData();
    },
    handleAdd() {
      if (this.ChuyenMucIdFilter) {
        if (this.$refs.formData !== undefined) {
          this.$refs.formData.resetFields();
        }
        this.formData = {
          ChuyenMucId: this.ChuyenMucIdFilter
        };
        this.fileList = [];
        this.fileDoc = [];
        this.isEditor = false;
        this.dialogFormDisplay = true;
      } else {
        alert("Chọn chuyên mục trước khi thêm mới!");
      }
    },
    handleActive(index, row) {
      if (this.RoleId == this.roleActive) {
        activeNewsVanBan(row).then(data => {
          //console.log(data);
          this.$message({
            type: "success",
            message: row.NguoiDuyetId
              ? "Hủy duyệt văn bản thành công!"
              : "Duyệt văn bản thành công!"
          });
          this.getListData();
        });
      } else {
        this.$message.error("Bạn không có quyền duyệt văn bản!");
      }
    },
    handleEdit(index, row) {
      if (this.$refs.formData !== undefined) {
        this.$refs.formData.resetFields();
      }
      this.formData = Object.assign({}, row);
      //VanBan
      if (this.formData.VanBan) {
        this.fileList = [];
        this.fileDoc = [];
        var _arr = JSON.parse(this.formData.VanBan);
        for (var i = 0; i < _arr.length; i++) {
          var urlFile = "/uploads/" + _arr[i];
          this.fileList.push({ name: _arr[i], url: urlFile });
          this.fileDoc.push({ key: _arr[i], file: _arr[i] });
        }
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
        deleteNewsVanBan(row.Id).then(data => {
          this.$message({
            type: "success",
            message: "Xóa thành công!"
          });
          this.getListData();
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
        selectNewsVanBan(this.ChuyenMucIdFilter).then(data => {
          this.listData = data;
          this.total = data.length;

          this.loading = false;
        });
      }
    },
    handlePreview(file) {
      this.dialogFileUrl = file.url;
      this.dialogVisibleDoc = true;
    },
    handleSuccess(response, file, fileList) {
      this.fileDoc.push({ key: file.name, file: response.file });
    },
    handleRemove(file, fileList) {
      let i = this.fileDoc.map(item => item.key).indexOf(file.name);
      this.fileDoc.splice(i, 1);
    },
    beforeUpload(file) {
      const isPdf = file.type === "application/pdf";
      const isDoc = file.type === "application/msword";
      const isDocx =
        file.type ===
        "application/vnd.openxmlformats-officedocument.wordprocessingml.document";

      const isLt10M = file.size / 1024 / 1024 < 10;

      if (!isPdf && !isDoc && !isDocx) {
        this.$message.error("Không đúng định dạng quy định");
      }
      if (!isLt10M) {
        this.$message.error("Dung lượng vượt quá 10M");
      }
      return (isPdf || isDoc || isDocx) && isLt10M;
    },
    updateData() {
      this.$refs.formData.validate(valid => {
        if (valid) {
          this.formData.VanBan = JSON.stringify(
            this.fileDoc.map(ite => ite.file)
          );
          if (this.isEditor == 0) {
            addNewsVanBan(this.formData).then(data => {
              //console.log(data);
              this.$message({
                type: "success",
                message: "Thêm mới thành công!"
              });
              this.getListData();
            });
          } else {
            if (!this.formData.LoaiVanBanId) {
              delete this.formData.LoaiVanBanId;
            }
            updateNewsVanBan(this.formData).then(data => {
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
    handleSizeChange(val) {
      this.pagination = val;
    },
    renderData() {
      var _data = this.listData.filter(post => {
        return (
          post.SoKyHieu.toLowerCase().includes(this.search.toLowerCase()) ||
          post.TrichYeu.toLowerCase().includes(this.search.toLowerCase())
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
    getCauHinh("VaiTroDuyetVanBan").then(data => {
      if (data) {
        this.roleActive = data[0].GiaTri;
        console.log(this.roleActive);
      }
    });
    getListDanhMucNews().then(data => {
      if (data) {
        this.ListDMChuyenMuc = data.DMChuyenMucDoc;
        this.ListDMLinhVuc = data.DMLinhVuc;
        this.ListDMLoaiVanBan = data.DMLoaiVanBan;
      }
    });

    this.getListData();
  }
};
</script>
<style></style>
