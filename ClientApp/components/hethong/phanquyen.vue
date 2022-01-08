<template>
  <div>
    <!-- start page title -->
    <div class="row">
      <div class="col-12">
        <div class="page-title-box">
          <div class="page-title-right">
            <ol class="breadcrumb m-0">
              <li class="breadcrumb-item">
                <a href="javascript: void(0);">Hệ thống</a>
              </li>
              <li class="breadcrumb-item active">Phân quyền</li>
            </ol>
          </div>
          <h4 class="page-title">Phân quyền</h4>
        </div>
      </div>
    </div>
    <!-- end page title -->
    <div class="row">
      <div class="col-12">
        <div class="card-box table-responsive">
          <div class="header-title" style="margin-bottom:10px;float:right">
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
            <el-table-column width="50" label="STT" align="center">
              <template slot-scope="scope">
                {{ renderIndex(scope.$index) }}
              </template>
            </el-table-column>

            <el-table-column prop="Route" sortable label="Route">
            </el-table-column
            ><el-table-column prop="Path" sortable label="Path">
            </el-table-column>
            <el-table-column align="center" label="Operations" width="180">
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
                  @click="handleActive(scope.row)"
                  :type="scope.row.IsActive ? 'success' : 'warning'"
                  :title="
                    scope.row.IsActive ? 'Đã kích hoạt' : 'Chưa kích hoạt'
                  "
                  :icon="scope.row.IsActive ? 'el-icon-check' : 'el-icon-close'"
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
            style="width:100%"
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
      title="Cập nhật phân quyền"
      :visible.sync="dialogFormDisplay"
      top="15px"
    >
      <el-form
        :model="formData"
        :rules="Rules"
        ref="formData"
        label-width="120px"
      >
        <el-form-item label="Route name" prop="Route">
          <treeselect
            v-model="formData.Route"
            filterable
            placeholder="Chọn route"
            size="small"
            style="width: 100%;"
            :options="ListRoute"
            :default-expand-level="1"
            :disable-branch-nodes="true"
          >
          </treeselect>
        </el-form-item>
        <el-form-item label="Path" prop="Path">
          <el-input v-model="formData.Path" type="text" size="small"></el-input>
        </el-form-item>
        <el-form-item label="Kích hoạt">
          <el-switch v-model="formData.IsActive" size="small"></el-switch>
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
import Treeselect from "@riophae/vue-treeselect";
// import the styles
import "@riophae/vue-treeselect/dist/vue-treeselect.css";
import { routes } from "../../router/routes";
import {
  getListPer,
  updatePer,
  deletePer,
  addPer,
  activePer
} from "../../store/api";
export default {
  components: { Treeselect },
  data() {
    return {
      dialogFormDisplay: false,
      loading: false,
      isEditor: 0,
      search: "",
      UserIdFilter: null,
      formData: {
        Id: null,
        Route: null,
        Path: null,
        IsActive: 0
      },
      Rules: {
        Route: [
          { required: true, message: "Vui lòng nhập Route", trigger: "blur" }
        ]
      },
      ListData: [],
      ListRoute: [],
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
    handleAdd() {
      this.getListRoute();
      this.isEditor = 0;
      if (this.$refs.formData !== undefined) {
        this.$refs.formData.resetFields();
      }

      this.formData.IsActive = true;
      this.dialogFormDisplay = true;
    },
    handleEdit(index, row) {
      this.getListRoute();
      if (this.$refs.formData !== undefined) {
        this.$refs.formData.resetFields();
      }
      this.formData.Id = row.Id;
      this.formData.Route = row.Route;
      this.formData.Path = row.Path;
      this.formData.IsActive = row.IsActive;

      this.isEditor = 1;
      this.dialogFormDisplay = true;
    },
    handleActive(row) {
      activePer(row.Id, row.Route, row.Path, !row.IsActive).then(data => {
        this.getListPer();
      });
    },
    handleDelete(row) {
      this.$confirm("Bạn có chắc chắn muốn xóa?", "Thông báo", {
        confirmButtonText: "OK",
        cancelButtonText: "Cancel",
        type: "warning"
      }).then(() => {
        deletePer(row.Id).then(data => {
          this.$message({
            type: "success",
            message: "Delete completed"
          });
          this.getListPer();
        });
      });
    },
    resetForm() {
      this.dialogFormDisplay = false;
      return true;
    },
    getListPer() {
      this.loading = true;
      getListPer().then(data => {
        this.ListData = data;
        this.total = data.length;
        this.loading = false;
      });
    },
    getListRoute() {
      this.ListRoute = [];
      var list = this.ListData.map(r => r.Route);
      routes.forEach(route => {
        if (route.meta.show == "true") {
          var newR = {
            id: route.name,
            label: route.name,
            children: []
          };
          if (route.children) {
            route.children.forEach(child => {
              if (!list.includes(child.name) && child.meta.show == "true") {
                newR.children.push({ id: child.name, label: child.name });
              }
            });
          }
          this.ListRoute.push(newR);
        }
      });
    },

    updateData() {
      this.$refs.formData.validate(valid => {
        if (valid) {
          if (this.isEditor == 0) {
            addPer(
              this.formData.Route,
              this.formData.Path,
              this.formData.IsActive
            ).then(data => {
              this.getListPer();
            });
          } else {
            updatePer(
              this.formData.Id,
              this.formData.Route,
              this.formData.Path,
              this.formData.IsActive
            ).then(data => {
              this.getListPer();
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
      //var _data = this.ListData;
      if (this.typeFilter != null) {
        var _data = this.ListData.filter(post => {
          return post.Route.includes(this.typeFilter);
        });
        this.total = _data.length;
        return _data.slice(
          (this.activePage - 1) * this.pagination,
          (this.activePage - 1) * this.pagination + this.pagination
        );
      }

      var _data = this.ListData.filter(post => {
        return post.Route.toLowerCase().includes(this.search.toLowerCase());
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
    this.getListPer();
  }
};
</script>
<style></style>
