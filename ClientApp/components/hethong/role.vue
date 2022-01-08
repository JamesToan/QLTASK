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
              <li class="breadcrumb-item active">Vai trò</li>
            </ol>
          </div>
          <h4 class="page-title">Vai trò</h4>
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
            <el-table-column width="50" label="" align="center">
              <template slot-scope=""> </template>
            </el-table-column>
            <el-table-column width="50" label="STT" align="center">
              <template slot-scope="scope">
                {{ renderIndex(scope.$index) }}
              </template>
            </el-table-column>
            <el-table-column prop="RoleName" sortable label="Tên vai trò">
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
      title="Cập nhật vai trò"
      :visible.sync="dialogFormDisplay"
      top="15px"
    >
      <el-form
        :model="formData"
        :rules="Rules"
        ref="formData"
        label-width="120px"
      >
        <el-form-item label="Tên vai trò" prop="RoleName">
          <el-input
            v-model="formData.RoleName"
            type="text"
            size="small"
          ></el-input>
        </el-form-item>
        <el-form-item label="Hiệu lực">
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
import { addRole, updateRole, deleteRole, getListRole } from "../../store/api";
export default {
  data() {
    return {
      dialogFormDisplay: false,
      loading: false,
      isEditor: 0,
      search: "",
      formData: {
        Id: null,
        RoleName: null,
        ParentId: null,
        IsActive: null
      },
      Rules: {
        RoleName: [
          {
            required: true,
            message: "Vui lòng nhập tên vai trò",
            trigger: "blur"
          }
        ]
      },
      ListData: [],
      ListDonVi: [],
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
      this.isEditor = 1;
      this.dialogFormDisplay = true;
      this.formData.RoleName = row.RoleName;
      this.formData.ParentId = row.ParentId;

      this.formData.IsActive = row.IsActive;
      this.formData.Id = row.Id;
    },

    handleDelete(row) {
      this.$confirm("Bạn có chắc chắn muốn xóa?", "Thông báo", {
        confirmButtonText: "OK",
        cancelButtonText: "Cancel",
        type: "warning"
      }).then(() => {
        deleteRole(row.Id).then(data => {
          this.$message({
            type: "success",
            message: "Delete completed"
          });
          this.listRole();
        });
      });
    },

    resetForm() {
      this.dialogFormDisplay = false;
      return true;
    },
    listRole() {
      this.loading = true;
      getListRole().then(data => {
        this.ListData = data;
        if (data != null) {
          this.ListDonVi = data;
        }
        this.total = data.length;
        this.loading = false;
      });
    },
    //listRole() {
    //  this.loading = true
    //  getListRole().then(data => {
    //    this.ListDonVi = data
    //    this.total = data.length
    //    this.loading = false
    //  });
    //},

    updateData() {
      this.$refs.formData.validate(valid => {
        if (valid) {
          if (this.isEditor == 0) {
            addRole(
              this.formData.RoleName,
              this.formData.IsActive
            ).then(data => {
              console.log(data);
              this.listRole();
            });
          } else {
            updateRole(
              this.formData.Id,
              this.formData.RoleName,
              this.formData.IsActive
            ).then(data => {
              console.log(data);
              this.listRole();
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
        return post.RoleName.toLowerCase().includes(this.search.toLowerCase());
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
    this.listRole();
  }
};
</script>
<style></style>
