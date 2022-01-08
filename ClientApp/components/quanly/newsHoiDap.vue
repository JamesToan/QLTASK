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
              <li class="breadcrumb-item active">Hỏi đáp</li>
            </ol>
          </div>
          <h4 class="page-title">Quản lý Hỏi đáp</h4>
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
            <el-table-column prop="LinhVucId" label="Lĩnh vực" width="120">
              <template slot-scope="scope">
                <text-highlight :queries="search" style="word-break: normal;">
                  {{ scope.row.LinhVuc ? scope.row.LinhVuc.TenLinhVuc : "" }}
                </text-highlight>
              </template>
            </el-table-column>
            <el-table-column prop="TieuDe" label="Tiêu đề" width="250">
              <template slot-scope="scope">
                <text-highlight :queries="search" style="word-break: normal;">
                  {{ scope.row.TieuDe }}
                </text-highlight>
              </template>
            </el-table-column>
            <!-- <el-table-column prop="NoiDung" label="Nội dung" min-width="150">
              <template slot-scope="scope">
                <text-highlight :queries="search" style="word-break: normal;">
                  {{ scope.row.NoiDung }}
                </text-highlight>
              </template>
            </el-table-column> -->
            <el-table-column prop="TraLoi" label="Trả lời" min-width="150">
              <template slot-scope="scope">
                <div v-html="formatString(scope.row.TraLoi)"></div>
                <!-- {{ formatString(scope.row.TraLoi) }} -->
              </template>
            </el-table-column>
            <el-table-column align="center" label="" width="180">
              <!-- <template slot="header">
                <el-button
                  type="primary"
                  size="small"
                  icon="el-icon-plus"
                  class="filter-item"
                  @click="handleAdd"
                  >Thêm mới</el-button
                >
              </template> -->
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
      title="Quản lý hỏi đáp"
      :visible.sync="dialogFormDisplay"
      top="15px"
      width="75%"
      center
    >
      <el-form
        :model="formData"
        :rules="formRules"
        ref="formData"
        label-width="140px"
        class="m-auto"
        size="small"
      >
        <el-form-item label="Người hỏi: ">
          {{
            formData.HoTen +
              " - Email: " +
              formData.Email +
              (formData.SoDienThoai
                ? " - SĐT: " + formData.SoDienThoai
                : "")
          }}
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
        <el-form-item label="Tiêu đề" prop="TieuDe">
          <el-input
            v-model="formData.TieuDe"
            type="text"
            size="small"
          ></el-input>
        </el-form-item>
        <el-form-item label="Nội dung câu hỏi" prop="NoiDung">
          <el-input
            v-model="formData.NoiDung"
            type="textarea"
            rows="5"
            size="small"
          ></el-input>
        </el-form-item>
        <el-form-item label="Nội dung trả lời" prop="TraLoi">
          <ckeditor
            :editor="editor"
            v-model="formData.TraLoi"
            :config="editorConfig"
          ></ckeditor>
        </el-form-item>
        <el-form-item label="Văn bản trả lời">
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
import ClassicEditor from "@ckeditor/ckeditor5-build-classic";
import moment from "moment";
import { getRole, getUser } from "../../store/common";
import {
  getCauHinh,
  getListDanhMucNews,
  selectNewsHoiDap,
  updateNewsHoiDap,
  activeNewsHoiDap,
  deleteNewsHoiDap
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
      formData: {
        NguoiHoi: {},
        LinhVucId: null,
        TieuDe: null,
        NoiDung: null,
        TraLoi: null
      },
      formRules: {
        LinhVucId: [
          {
            required: true,
            message: "Vui lòng chọn lĩnh vực",
            trigger: "blur"
          }
        ],
        TieuDe: [
          {
            required: true,
            message: "Vui lòng nhập tiêu đề",
            trigger: "blur"
          }
        ],
        NoiDung: [
          {
            required: true,
            message: "Vui lòng nhập nội dung câu hỏi",
            trigger: "blur"
          }
        ],
        TraLoi: [
          {
            required: true,
            message: "Vui lòng nhập nội dung trả lời",
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
      ListDMLinhVuc: [],
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
    formatString(val) {
      if (val) {
        if (val.length > 200) {
          return val.substring(0, 200) + "...";
        } else {
          return val;
        }
      } else return null;
    },
    // handleAdd() {
    //   if (this.$refs.formData !== undefined) {
    //     this.$refs.formData.resetFields();
    //   }

    //   this.isEditor = false;
    //   this.dialogFormDisplay = true;
    // },
    handleActive(index, row) {
      if (this.RoleId == this.roleActive) {
        activeNewsHoiDap(row).then(data => {
          //console.log(data);
          this.$message({
            type: "success",
            message: row.NguoiDuyetId
              ? "Hủy duyệt trả lời thành công!"
              : "Duyệt trả lời thành công!"
          });
          this.getListData();
        });
      } else {
        this.$message.error("Bạn không có quyền duyệt câu trả lời!");
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
        deleteNewsHoiDap(row.Id).then(data => {
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
        selectNewsHoiDap().then(data => {
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
            addNewsHoiDap(this.formData).then(data => {
              //console.log(data);
              this.$message({
                type: "success",
                message: "Thêm mới thành công!"
              });
              this.getListData();
            });
          } else {
            //delete this.formData.LinhVuc;
            updateNewsHoiDap(this.formData).then(data => {
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
          post.TieuDe.toLowerCase().includes(this.search.toLowerCase()) ||
          post.NoiDung.toLowerCase().includes(this.search.toLowerCase())
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
    getCauHinh("VaiTroDuyetHoiDap").then(data => {
      if (data) {
        this.roleActive = data[0].GiaTri;
        console.log(this.roleActive);
      }
    });
    getListDanhMucNews().then(data => {
      if (data) {
        this.ListDMLinhVuc = data.DMLinhVuc;
      }
    });

    this.getListData();
  }
};
</script>
<style></style>
