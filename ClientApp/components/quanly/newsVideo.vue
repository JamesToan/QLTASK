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
              <li class="breadcrumb-item active">Video</li>
            </ol>
          </div>
          <h4 class="page-title">Quản lý Video</h4>
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
            <el-table-column prop="TieuDe" label="Tiêu đề" width="400">
              <template slot-scope="scope">
                <text-highlight :queries="search" style="word-break: normal;">
                  {{ scope.row.TieuDe }}
                </text-highlight>
              </template>
            </el-table-column>
            <el-table-column prop="Video" label="Video" min-width="200">
              <template slot-scope="scope">
                <text-highlight :queries="search" style="word-break: normal;">
                  {{ scope.row.Video }}
                </text-highlight>
              </template>
            </el-table-column>
            <!-- <<el-table-column
              prop="Video"
              label="Hình ảnh"
              width="200"
              align="center"
            >
              template slot-scope="scope">
                <div v-for="item in formatList(scope.row.HinhAnh)" :key="item">
                  <el-tooltip placement="top" :content="item">
                    <el-link
                      :href="'../../images/' + item"
                      target="_blank"
                      :underline="false"
                    >
                      <el-image
                        :src="'../../images/' + item"
                        fit="contain"
                      ></el-image>
                    </el-link>
                  </el-tooltip>
                </div>
              </template> 
            </el-table-column>-->
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
      title="Quản lý Video"
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
        <el-form-item label="Tiêu đề" prop="TieuDe">
          <el-input
            v-model="formData.TieuDe"
            type="text"
            size="small"
          ></el-input>
        </el-form-item>

        <el-form-item label="Video" prop="Video">
          <el-input
            v-model="formData.Video"
            type="text"
            size="small"
          ></el-input>
        </el-form-item>
        <!-- <el-form-item label="Hình ảnh">
          <el-upload
            action="/api/TapTin/UploadImage"
            :limit="1"
            :multiple="true"
            :on-preview="handlePreview"
            :on-remove="handleRemove"
            :file-list="fileList"
            :on-success="handleSuccess"
            :before-upload="beforeUpload"
            accept=".png,.jpg,.jpeg"
            :auto-upload="true"
            size="mini"
          >
            <el-button size="small" type="primary">Tải lên</el-button>
          </el-upload>
        </el-form-item> -->
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
      :visible.sync="dialogVisibleImage"
      width="85%"
    >
      <img width="100%" :src="dialogFileUrl" alt="" />
    </el-dialog>
  </div>
</template>
<script>
import moment from "moment";
import { getRole, getUser } from "../../store/common";
import {
  getCauHinh,
  getListDanhMucNews,
  selectNewsVideo,
  addNewsVideo,
  updateNewsVideo,
  activeNewsVideo,
  deleteNewsVideo
} from "../../store/api";
export default {
  data() {
    return {
      title: null,
      dialogFormDisplay: false,
      dialogVisibleImage: false,
      dialogFileUrl: null,
      loading: false,
      isEditor: false,
      roleActive: true,
      search: "",
      ChuyenMucIdFilter: 32,
      formData: {
        ChuyenMucId: null,
        TieuDe: null,
        Video: null
      },
      formRules: {
        ChuyenMucId: [
          {
            required: true,
            message: "Vui lòng chọn chuyên mục",
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
        Video: [
          {
            required: true,
            message: "Vui lòng nhập liên kết",
            trigger: "blur"
          }
        ]
      },
      RoleId: getRole(),
      listData: [],
      ListDMChuyenMuc: [],
      fileList: [],
      fileImage: [],
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
        this.isEditor = false;
        this.dialogFormDisplay = true;
      } else {
        alert("Chọn chuyên mục trước khi thêm mới!");
      }
    },
    handleActive(index, row) {
      if (this.RoleId == this.roleActive) {
        activeNewsVideo(row).then(data => {
          //console.log(data);
          this.$message({
            type: "success",
            message: row.NguoiDuyetId
              ? "Hủy duyệt Video thành công!"
              : "Duyệt Video thành công!"
          });
          this.getListData();
        });
      } else {
        this.$message.error("Bạn không có quyền duyệt Video!");
      }
    },
    handleEdit(index, row) {
      if (this.$refs.formData !== undefined) {
        this.$refs.formData.resetFields();
      }
      this.formData = Object.assign({}, row);
      //HinhAnh
      if (this.formData.HinhAnh) {
        this.fileList = [];
        this.fileImage = [];
        var _arr = JSON.parse(this.formData.HinhAnh);
        for (var i = 0; i < _arr.length; i++) {
          var urlFile = "/images/" + _arr[i];
          this.fileList.push({ name: _arr[i], url: urlFile });
          this.fileImage.push({ key: _arr[i], file: _arr[i] });
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
        deleteNewsVideo(row.Id).then(data => {
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
        selectNewsVideo(this.ChuyenMucIdFilter).then(data => {
          this.listData = data;
          this.total = data.length;

          this.loading = false;
        });
      }
    },
    handlePreview(file) {
      this.dialogFileUrl = file.url;
      this.dialogVisibleImage = true;
    },
    handleSuccess(response, file, fileList) {
      this.fileImage.push({ key: file.name, file: response.file });
    },
    handleRemove(file, fileList) {
      let i = this.fileImage.map(item => item.key).indexOf(file.name);
      this.fileImage.splice(i, 1);
    },
    beforeUpload(file) {
      const isPng = file.type === "image/png";
      const isJpg = file.type === "image/jpeg";

      const isLt10M = file.size / 1024 / 1024 < 10;

      if (!isPng && !isJpg) {
        this.$message.error("Không đúng định dạng quy định");
      }
      if (!isLt10M) {
        this.$message.error("Dung lượng vượt quá 10M");
      }
      return (isPng || isJpg) && isLt10M;
    },
    updateData() {
      this.$refs.formData.validate(valid => {
        if (valid) {
          this.formData.HinhAnh = JSON.stringify(
            this.fileImage.map(ite => ite.file)
          );
          if (this.isEditor == 0) {
            addNewsVideo(this.formData).then(data => {
              //console.log(data);
              this.$message({
                type: "success",
                message: "Thêm mới thành công!"
              });
              this.getListData();
            });
          } else {
            updateNewsVideo(this.formData).then(data => {
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
          post.Video.toLowerCase().includes(this.search.toLowerCase())
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
    getCauHinh("VaiTroDuyetVideo").then(data => {
      if (data) {
        this.roleActive = data[0].GiaTri;
        console.log(this.roleActive);
      }
    });
    getListDanhMucNews().then(data => {
      if (data) {
        this.ListDMChuyenMuc = data.DMChuyenMucVideo;
      }
    });

    this.getListData();
  }
};
</script>
<style></style>
