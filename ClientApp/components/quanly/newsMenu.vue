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
              <li class="breadcrumb-item active">Thanh menu</li>
            </ol>
          </div>
          <h4 class="page-title">Quản lý Thanh menu</h4>
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
            <el-table-column prop="MenuCha" label="Menu cha" width="200">
              <template slot-scope="scope">
                {{ scope.row.MenuCha ? scope.row.MenuCha.TieuDe : "" }}
              </template>
            </el-table-column>
            <el-table-column prop="TieuDe" label="Tiêu đề" width="250">
              <template slot-scope="scope">
                <text-highlight :queries="search" style="word-break: normal;">
                  {{ scope.row.TieuDe }}
                </text-highlight>
              </template>
            </el-table-column>
            <el-table-column prop="Link" label="Liên kết" min-width="200">
              <template slot-scope="scope">
                <text-highlight :queries="search" style="word-break: normal;">
                  {{ scope.row.Link }}
                </text-highlight>
              </template>
            </el-table-column>
            <!-- <el-table-column
              prop="Icon"
              label="Icon"
              width="100"
              align="center"
            >
              <template slot-scope="scope">
                {{ scope.row.Icon }}
              </template>
            </el-table-column> -->
            <el-table-column
              prop="ThuTu"
              label="Thứ tự"
              width="80"
              align="center"
            >
              <template slot-scope="scope">
                {{ scope.row.ThuTu }}
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
                  :type="scope.row.HieuLuc ? 'success' : 'warning'"
                  :title="scope.row.HieuLuc ? 'Đã kích hoạt' : 'Chưa kích hoạt'"
                  :icon="scope.row.HieuLuc ? 'el-icon-check' : 'el-icon-close'"
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
      title="Quản lý Menu"
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
        ><el-form-item label="Chuyên mục" prop="ChuyenMucId" v-show="false">
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
        <el-form-item label="Menu cha" prop="MenuChaId">
          <el-select
            v-model="formData.MenuChaId"
            placeholder="Chọn menu cha"
            class="w-100"
          >
            <el-option
              v-for="item in listData"
              :key="item.Id"
              :label="item.TieuDe"
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

        <el-form-item label="Liên kết" prop="Link">
          <el-input v-model="formData.Link" type="text" size="small"></el-input>
        </el-form-item>
        <el-form-item label="Thứ tự" prop="ThuTu">
          <el-input
            v-model="formData.ThuTu"
            type="number"
            size="small"
          ></el-input>
        </el-form-item>
        <el-form-item label="Hiệu lực">
          <el-switch v-model="formData.HieuLuc" size="small"></el-switch>
        </el-form-item>
      </el-form>
      <span slot="footer" class="dialog-footer">
        <el-button @click="resetForm" size="small">Bỏ qua</el-button>
        <el-button type="primary" size="small" @click="updateData"
          >Cập nhật</el-button
        >
      </span>
    </el-dialog>
  </div>
</template>
<script>
import moment from "moment";
import { getRole, getUser } from "../../store/common";
import {
  getListDanhMucNews,
  selectNewsMenu,
  addNewsMenu,
  updateNewsMenu,
  activeNewsMenu,
  deleteNewsMenu
} from "../../store/api";
export default {
  data() {
    return {
      title: null,
      dialogFormDisplay: false,
      loading: false,
      isEditor: false,
      search: "",
      ChuyenMucIdFilter: 0,
      formData: {
        ChuyenMucId: null,
        TieuDe: null,
        Icon: null,
        Link: null
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
        Link: [
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
      if (this.ChuyenMucIdFilter != null) {
        if (this.$refs.formData !== undefined) {
          this.$refs.formData.resetFields();
        }
        this.formData = {
          ChuyenMucId: this.ChuyenMucIdFilter,
          HieuLuc: true
        };
        this.isEditor = false;
        this.dialogFormDisplay = true;
      } else {
        alert("Chọn chuyên mục trước khi thêm mới!");
      }
    },
    handleEdit(index, row) {
      if (this.$refs.formData !== undefined) {
        this.$refs.formData.resetFields();
      }
      this.formData = Object.assign({}, row);
      this.isEditor = true;
      this.dialogFormDisplay = true;
    },
    handleActive(index, row) {
      activeNewsMenu(row).then(data => {
        //console.log(data);
        this.$message({
          type: "success",
          message: row.HieuLuc
            ? "Hủy kích hoạt menu thành công!"
            : "Kích hoạt menu thành công!"
        });
        this.getListData();
      });
    },
    handleDelete(row) {
      this.$confirm("Bạn có chắc chắn muốn xóa?", "Thông báo", {
        confirmButtonText: "OK",
        cancelButtonText: "Cancel",
        type: "warning"
      }).then(() => {
        deleteNewsMenu(row.Id).then(data => {
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
        selectNewsMenu(this.ChuyenMucIdFilter).then(data => {
          this.listData = data;
          this.total = data.length;

          this.loading = false;
        });
      }
    },
    updateData() {
      this.$refs.formData.validate(valid => {
        if (valid) {
          if (this.isEditor == 0) {
            addNewsMenu(this.formData).then(data => {
              //console.log(data);
              this.$message({
                type: "success",
                message: "Thêm mới thành công!"
              });
              this.getListData();
            });
          } else {
            updateNewsMenu(this.formData).then(data => {
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
          post.Link.toLowerCase().includes(this.search.toLowerCase())
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
    getListDanhMucNews().then(data => {
      if (data) {
        this.ListDMChuyenMuc = data.DMChuyenMucMenu;
      }
    });

    this.getListData();
  }
};
</script>
<style></style>
