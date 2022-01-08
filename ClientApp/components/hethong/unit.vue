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
              <li class="breadcrumb-item active">Đơn vị</li>
            </ol>
          </div>
          <h4 class="page-title">Đơn vị</h4>
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
            <el-table-column prop="UnitName" sortable label="Tên đơn vị">
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
      title="Cập nhật đơn vị"
      :visible.sync="dialogFormDisplay"
      top="15px"
    >
      <el-form
        :model="formData"
        :rules="Rules"
        ref="formData"
        label-width="120px"
      >
        <el-form-item label="Đơn vị cha" prop="ParentId">
          <el-select
            style="margin-right:5px;width:100%"
            size="small"
            v-model="formData.ParentId"
            placeholder="Chọn đơn vị cha"
          >
            <el-option
              v-for="item in ListDonVi"
              :key="item.Id"
              :label="item.UnitName"
              :value="item.Id"
            >
            </el-option>
          </el-select>
        </el-form-item>
        <el-form-item label="Tên đơn vị" prop="UnitName">
          <el-input
            v-model="formData.UnitName"
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
import { addUnit, updateUnit, deleteUnit, getListUnit } from "../../store/api";
export default {
  data() {
    return {
      dialogFormDisplay: false,
      loading: false,
      isEditor: 0,
      search: "",
      formData: {
        Id: null,
        UnitName: null,
        ParentId: null,
        IsActive: null
      },
      Rules: {
        UnitName: [
          {
            required: true,
            message: "Vui lòng nhập tên đơn vị",
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
      this.formData.UnitName = row.UnitName;
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
        deleteUnit(row.Id).then(data => {
          this.$message({
            type: "success",
            message: "Delete completed"
          });
          this.listUnit();
        });
      });
    },

    resetForm() {
      this.dialogFormDisplay = false;
      return true;
    },
    listUnit() {
      this.loading = true;
      getListUnit().then(data => {
        this.ListData = data;
        if (data != null) {
          this.ListDonVi = data;
        }
        this.total = data.length;
        this.loading = false;
      });
    },
    //listUnit() {
    //  this.loading = true
    //  getListUnit().then(data => {
    //    this.ListDonVi = data
    //    this.total = data.length
    //    this.loading = false
    //  });
    //},

    updateData() {
      this.$refs.formData.validate(valid => {
        if (valid) {
          if (this.isEditor == 0) {
            addUnit(
              this.formData.UnitName,
              this.formData.ParentId,
              this.formData.IsActive
            ).then(data => {
              console.log(data);
              this.listUnit();
            });
          } else {
            updateUnit(
              this.formData.Id,
              this.formData.UnitName,
              this.formData.ParentId,
              this.formData.IsActive
            ).then(data => {
              console.log(data);
              this.listUnit();
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
        return post.UnitName.toLowerCase().includes(this.search.toLowerCase());
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
    this.listUnit();
  }
};
</script>
<style></style>
