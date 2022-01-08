<template>
  <div>
    <!-- start page title -->
    <div class="row">
      <div class="col-12">
        <div class="page-title-box">
          <div class="page-title-right">
            <ol class="breadcrumb m-0">
              <li class="breadcrumb-item">
                <a href="javascript: void(0);">Danh mục</a>
              </li>
              <li class="breadcrumb-item active">Chuyên mục</li>
            </ol>
          </div>
          <h4 class="page-title">Chuyên mục</h4>
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
              prop="TenChuyenMuc"
              label="Tên Chuyên mục"
              sortable
              min-width="225"
            >
              <template slot-scope="scope">
                <text-highlight :queries="search" style="word-break: normal;">
                  {{ scope.row.TenChuyenMuc }}
                </text-highlight>
              </template>
            </el-table-column>
            <el-table-column
              prop="LoaiChuyenMuc"
              label="Loại Chuyên mục"
              sortable
              min-width="225"
            >
              <template slot-scope="scope">
                <text-highlight :queries="search" style="word-break: normal;">
                  {{ scope.row.LoaiChuyenMuc }}
                </text-highlight>
              </template>
            </el-table-column>
            <el-table-column align="center" label="" width="125">
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
      title="Cập nhật Chuyên mục"
      :visible.sync="dialogFormDisplay"
      top="15px"
    >
      <el-form
        :model="formData"
        :rules="formRules"
        ref="formData"
        label-width="150px"
      >
        <!-- <el-form-item label="Mã Chuyên mục" prop="MaChuyenMuc">
          <el-input
            v-model="formData.MaChuyenMuc"
            type="text"
            size="small"
            :readonly="isEditor"
          ></el-input>
        </el-form-item> -->
        <el-form-item label="Tên Chuyên mục" prop="TenChuyenMuc">
          <el-input
            v-model="formData.TenChuyenMuc"
            type="text"
            size="small"
          ></el-input>
        </el-form-item>
        <el-form-item label="Loại Chuyên mục" prop="LoaiChuyenMuc">
          <el-input
            v-model="formData.LoaiChuyenMuc"
            type="text"
            size="small"
          ></el-input>
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
import {
  addChuyenMuc,
  updateChuyenMuc,
  deleteChuyenMuc,
  getListChuyenMuc
} from "../../store/api";
export default {
  data() {
    return {
      dialogFormDisplay: false,
      loading: false,
      isEditor: false,
      search: "",
      formData: {
        TenChuyenMuc: null,
        ThuTu: null,
        HieuLuc: null
      },
      formRules: {
        TenChuyenMuc: [
          {
            required: true,
            message: "Vui lòng nhập Tên Chuyên mục",
            trigger: "blur"
          }
        ],
        LoaiChuyenMuc: [
          {
            required: true,
            message: "Vui lòng nhập Loại Chuyên mục",
            trigger: "blur"
          }
        ]
      },
      listData: [],
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
      if (this.$refs.formData !== undefined) {
        this.$refs.formData.resetFields();
      }
      this.formData = {
        HieuLuc: true
      };

      this.isEditor = false;
      this.dialogFormDisplay = true;
    },
    handleEdit(index, row) {
      if (this.$refs.formData !== undefined) {
        this.$refs.formData.resetFields();
      }
      this.formData = Object.assign({}, row);

      this.isEditor = true;
      this.dialogFormDisplay = true;
    },

    handleDelete(row) {
      this.$confirm("Bạn có chắc chắn muốn xóa?", "Thông báo", {
        confirmButtonText: "OK",
        cancelButtonText: "Cancel",
        type: "warning"
      }).then(() => {
        deleteChuyenMuc(row.Id).then(data => {
          this.$message({
            type: "success",
            message: "Delete completed"
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
      getListChuyenMuc().then(data => {
        this.listData = data;
        this.total = data.length;
        this.loading = false;
      });
    },

    updateData() {
      this.$refs.formData.validate(valid => {
        if (valid) {
          if (this.isEditor == 0) {
            addChuyenMuc(this.formData).then(data => {
              //console.log(data);
              this.getListData();
            });
          } else {
            updateChuyenMuc(this.formData.Id, this.formData).then(data => {
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
          post.TenChuyenMuc.toLowerCase().includes(this.search.toLowerCase()) ||
          post.LoaiChuyenMuc.toLowerCase().includes(this.search.toLowerCase())
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
    this.getListData();
  }
};
</script>
<style></style>
