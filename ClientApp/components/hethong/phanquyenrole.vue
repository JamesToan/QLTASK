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
              <li class="breadcrumb-item active">Phân quyền vai trò</li>
            </ol>
          </div>
          <h4 class="page-title">Phân quyền vai trò</h4>
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
              style="width: 240px; float: right; margin-left: 15px;"
            ></el-input>
            <el-select
              style="width: 240px; float: right;"
              v-model="RoleIdFilter"
              @change="changeRoleFilter"
              placeholder="Chọn vai trò"
            >
              <el-option
                v-for="item in ListRole"
                :key="item.Id"
                :label="item.RoleName"
                :value="item.Id"
              >
              </el-option>
            </el-select>
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
            <el-table-column prop="Permission.Route" label="Route" sortable>
            </el-table-column>
            <el-table-column prop="Permission.Path" label="Path" sortable>
            </el-table-column>
            <el-table-column align="center" width="180">
              <template slot="header">
                <el-button
                  type="primary"
                  title="Thêm mới"
                  size="small"
                  icon="el-icon-plus"
                  class="filter-item"
                  @click="handleAdd"
                ></el-button>
                <el-button
                  type="success"
                  title="Sao chép"
                  size="small"
                  icon="el-icon-refresh"
                  class="filter-item"
                  @click="handleCopy"
                ></el-button>
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
      :title="dialogFormTitle"
      :visible.sync="dialogFormDisplay"
      top="15px"
    >
      <el-form
        :model="formData"
        :rules="Rules"
        ref="formData"
        label-width="120px"
      >
        <el-form-item label="Vai trò" prop="RoleId">
          <el-select
            v-model="formData.RoleId"
            filterable
            placeholder="Chọn vai trò"
            size="small"
            style="width: 100%;"
            disabled
          >
            <el-option
              v-for="item in ListRole"
              :key="item.Id"
              :label="item.RoleName"
              :value="item.Id"
            >
            </el-option>
          </el-select>
        </el-form-item>
        <el-form-item label="Route" prop="PermissionId" v-if="!isCopy">
          <el-select
            v-model="formData.PermissionId"
            filterable
            placeholder="Chọn route"
            size="small"
            style="width: 100%;"
          >
            <el-option
              v-for="item in ListRoute"
              :key="item.Id"
              :label="item.Route"
              :value="item.Id"
            >
            </el-option>
          </el-select>
        </el-form-item>
        <el-form-item label="Kích hoạt" v-if="!isCopy">
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
import {
  getListRole,
  getListNotPer,
  getListPerRole,
  updatePerRole,
  deletePerRole,
  addPerRole,
  copyPerRole
} from "../../store/api";
export default {
  data() {
    return {
      dialogFormDisplay: false,
      dialogFormTitle: null,
      loading: false,
      isEditor: 0,
      isCopy: 0,
      RoleIdFilter: null,
      search: "",
      formData: {
        Id: null,
        RoleId: null,
        PermissionId: null
      },
      Rules: {
        RoleId: [
          { required: true, message: "Vui lòng chọn Vai trò", trigger: "blur" }
        ],
        PermissionId: [
          { required: true, message: "Vui lòng chọn Route", trigger: "blur" }
        ]
      },
      ListData: [],
      ListRole: [],
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
    changeRoleFilter(val) {
      this.getListPerRole();
      this.getListRoute();
    },
    handleAdd() {
      if (this.RoleIdFilter) {
        if (this.$refs.formData !== undefined) {
          this.$refs.formData.resetFields();
        }
        this.getListRoute();

        this.formData.RoleId = this.RoleIdFilter;
        this.formData.PermissionId = null;
        this.formData.IsActive = true;

        this.isEditor = 0;
        this.isCopy = 0;
        this.dialogFormDisplay = true;
        this.dialogFormTitle = "Thêm mới phân quyền";
      } else {
        alert("Chọn vai trò trước khi thêm mới!");
      }
    },
    handleCopy() {
      if (this.RoleIdFilter) {
        if (this.$refs.formData !== undefined) {
          this.$refs.formData.resetFields();
        }
        this.getListRoute();

        this.isEditor = 0;
        this.isCopy = 1;
        this.dialogFormDisplay = true;
        var role = this.ListRole.find(r => r.Id == this.RoleIdFilter);
        this.dialogFormTitle =
          "Sao chép phân quyền cho " + role.RoleName + " từ";
      } else {
        alert("Chọn vai trò trước khi sao chép!");
      }
    },
    handleEdit(index, row) {
      if (this.$refs.formData !== undefined) {
        this.$refs.formData.resetFields();
      }
      this.formData.Id = row.Id;
      this.formData.PermissionId = row.PermissionId;
      this.formData.RoleId = row.RoleId;
      this.formData.IsActive = row.IsActive;

      this.isEditor = 1;
      this.isCopy = 0;
      this.dialogFormDisplay = true;
      this.dialogFormTitle = "Cập nhật phân quyền";
    },
    handleActive(row) {
      updatePerRole(row.Id, row.PermissionId, row.RoleId, !row.IsActive).then(
        data => {
          this.getListPerRole();
        }
      );
    },
    handleDelete(row) {
      this.$confirm("Bạn có chắc chắn muốn xóa?", "Thông báo", {
        confirmButtonText: "OK",
        cancelButtonText: "Cancel",
        type: "warning"
      }).then(() => {
        deletePerRole(row.Id).then(data => {
          this.$message({
            type: "success",
            message: "Delete completed"
          });
          this.getListPerRole();
        });
      });
    },

    resetForm() {
      this.dialogFormDisplay = false;
      return true;
    },
    getListPerRole() {
      this.loading = true;
      getListPerRole(this.RoleIdFilter).then(data => {
        this.ListData = data;
        this.total = data.length;
        this.loading = false;
      });
    },
    getListRole() {
      this.loading = true;
      getListRole().then(data => {
        this.ListRole = data;
        this.loading = false;
      });
    },
    getListRoute() {
      this.loading = true;
      getListNotPer(this.RoleIdFilter).then(data => {
        this.ListRoute = data;
        this.loading = false;
      });
    },
    updateData() {
      this.$refs.formData.validate(valid => {
        if (valid) {
          if (this.isCopy) {
            copyPerRole(this.formData.RoleId, this.RoleIdFilter).then(data => {
              this.getListPerRole();
            });
          } else {
            if (this.isEditor == 0) {
              addPerRole(
                this.formData.PermissionId,
                this.formData.RoleId,
                this.formData.IsActive
              ).then(data => {
                this.getListPerRole();
              });
            } else {
              updatePerRole(
                this.formData.Id,
                this.formData.PermissionId,
                this.formData.RoleId,
                this.formData.IsActive
              ).then(data => {
                this.getListPerRole();
              });
            }
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
      var _data = this.ListData.filter(post => {
        return post.Permission.Route.toLowerCase().includes(
          this.search.toLowerCase()
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
    this.getListPerRole();
    this.getListRole();
    //this.getListRoute();
  }
};
</script>
<style></style>
