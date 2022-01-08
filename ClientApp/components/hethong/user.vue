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
              <li class="breadcrumb-item active">Người dùng</li>
            </ol>
          </div>
          <h4 class="page-title">Người dùng</h4>
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
            <!-- <el-table-column
              prop="UnitName"
              sortable
              label="Đơn vị"
              width="200"
            >
            </el-table-column> -->
            <el-table-column prop="UserName" label="UserName" width="200">
            </el-table-column>
            <el-table-column prop="FullName" label="Họ tên" min-width="250">
            </el-table-column>
            <el-table-column
              prop="RoleName"
              sortable
              label="Vai trò"
              width="125"
            >
            </el-table-column>
            <el-table-column align="center" label="" width="235">
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
                  :type="scope.row.isActive != 1 ? 'warning' : 'success'"
                  :title="
                    scope.row.isActive != 1 ? 'Chưa kích hoạt' : 'Đã kích hoạt'
                  "
                  icon="el-icon-check"
                  size="mini"
                ></el-button>
                <el-button
                  @click="handleReset(scope.row)"
                  type="default"
                  title="Reset mật khẩu"
                  icon="el-icon-key"
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
      title="Cập nhật User"
      :visible.sync="dialogFormDisplay"
      top="15px"
    >
      <el-form
        :model="formData"
        :rules="Rules"
        ref="formData"
        label-width="120px"
      >
        <el-form-item label="Đơn vị" prop="UnitId" size="small">
          <treeselect
            v-model="formData.UnitId"
            :multiple="false"
            placeholder="Chọn đơn vị"
            :searchable="false"
            :options="ListDonVi"
          />
        </el-form-item>
        <el-form-item label="Họ tên" prop="FullName">
          <el-input
            v-model="formData.FullName"
            type="text"
            size="small"
          ></el-input>
        </el-form-item>
        <el-form-item label="UserName" prop="UserName">
          <el-input
            v-model="formData.UserName"
            type="text"
            size="small"
          ></el-input>
        </el-form-item>
        <el-form-item label="Vai trò" prop="RoleId">
          <el-select
            style="margin-right:5px;width:100%"
            size="small"
            v-model="formData.RoleId"
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
        </el-form-item>
        <el-form-item label="Phone" prop="Phone">
          <el-input
            v-model="formData.Phone"
            type="text"
            size="small"
          ></el-input>
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
import {
  addUser,
  updateUser,
  deleteUser,
  getListUser,
  getListUnitAll,
  getListRole,
  resetUser
} from "../../store/api";
export default {
  components: { Treeselect },
  data() {
    return {
      dialogFormDisplay: false,
      loading: false,
      isEditor: 0,
      search: "",
      formData: {
        Id: null,
        FullName: null,
        UserName: null,
        Phone: null,
        UnitId: null,
        RoleId: null,
        IsActive: 0
      },
      Rules: {
        // UnitId: [
        //   { required: true, message: "Vui lòng chọn đơn vị", trigger: "blur" }
        // ],
        FullName: [
          { required: true, message: "Vui lòng nhập họ tên", trigger: "blur" }
        ],
        UserName: [
          { required: true, message: "Vui lòng nhập Username", trigger: "blur" }
        ],
        RoleId: [
          { required: true, message: "Vui lòng nhập vai trò", trigger: "blur" }
        ]
      },
      ListData: [],
      ListDonVi: [],
      ListRole: [],
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
      this.isEditor = 0;
      if (this.$refs.formData !== undefined) {
        this.$refs.formData.resetFields();
      }

      this.formData.IsActive = true;
      this.dialogFormDisplay = true;
    },
    handleEdit(index, row) {
      if (this.$refs.formData !== undefined) {
        this.$refs.formData.resetFields();
      }
      this.formData.FullName = row.FullName;
      this.formData.UserName = row.UserName;
      this.formData.Phone = row.Phone;
      this.formData.UnitId = row.UnitId;
      this.formData.RoleId = row.RoleId;

      this.formData.IsActive = row.isActive;
      this.formData.Id = row.Id;
      
      this.isEditor = 1;
      this.dialogFormDisplay = true;
    },
    handleActive(row) {
      updateUser(
        row.Id,
        row.UserName,
        row.FullName,
        row.Phone,
        row.RoleId,
        row.UnitId,
        !row.isActive
      ).then(data => {
        //console.log(data)
        this.listUser();
      });
    },
    handleDelete(row) {
      this.$confirm("Bạn có chắc chắn muốn xóa?", "Thông báo", {
        confirmButtonText: "OK",
        cancelButtonText: "Cancel",
        type: "warning"
      }).then(() => {
        deleteUser(row.Id).then(data => {
          this.$message({
            type: "success",
            message: "Delete completed"
          });
          this.listUser();
        });
      });
    },
    handleReset(row) {
      this.$confirm("Bạn có chắc chắn muốn reset mật khẩu?", "Thông báo", {
        confirmButtonText: "OK",
        cancelButtonText: "Cancel",
        type: "warning"
      }).then(() => {
        resetUser(row.Id).then(data => {
          if (data == 1) {
            this.$message({
              type: "success",
              message: "Reset mật khẩu thành công"
            });
          } else {
            this.$message({
              type: "warning",
              message: "Reset mật khẩu không thành công"
            });
          }
        });
      });
    },
    resetForm() {
      this.dialogFormDisplay = false;
      return true;
    },
    listUnit() {
      this.loading = true;
      getListUnitAll().then(data => {
        if (data != null) {
          this.ListDonVi = data;
        }
      });
    },
    listRole() {
      this.loading = true;
      getListRole().then(data => {
        if (data != null) {
          this.ListRole = data;
        }
      });
    },
    listUser() {
      this.loading = true;
      getListUser().then(data => {
        this.ListData = data;
        this.total = data.length;
        this.loading = false;
      });
    },

    updateData() {
      this.$refs.formData.validate(valid => {
        if (valid) {
          if (this.isEditor == 0) {
            addUser(
              this.formData.UserName,
              this.formData.FullName,
              this.formData.Phone,
              this.formData.RoleId,
              this.formData.UnitId,
              this.formData.IsActive
            ).then(data => {
              resetUser(data).then(data => {
                console.log(data);
                this.listUser();
              });
            });
          } else {
            updateUser(
              this.formData.Id,
              this.formData.UserName,
              this.formData.FullName,
              this.formData.Phone,
              this.formData.RoleId,
              this.formData.UnitId,
              this.formData.IsActive
            ).then(data => {
              console.log(data);
              this.listUser();
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
      var _data = this.ListData.filter(post => {
        return (
          post.UserName.toLowerCase().includes(this.search.toLowerCase()) ||
          post.FullName.toLowerCase().includes(this.search.toLowerCase())
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
    this.listUser();
    this.listUnit();
    this.listRole();
  }
};
</script>
<style></style>
