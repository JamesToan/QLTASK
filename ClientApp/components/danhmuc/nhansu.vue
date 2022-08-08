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
              <li class="breadcrumb-item active">Nhân sự</li>
            </ol>
          </div>
          <h4 class="page-title">Nhân sự</h4>
        </div>
      </div>
    </div>
    <!-- end page title -->
    <div class="row">
      <div class="col-12">
        <div class="card-box table-responsive">
          <div class="header-title" style="margin-bottom: 10px; float: right">
            <el-input clearable
                      v-model="search"
                      placeholder="Tìm kiếm"
                      style="width: 240px; float: right;"></el-input>
          </div>
          <el-table :data="renderData()"
                    border
                    v-loading="loading"
                    default-expand-all
                    row-key="Id"
                    style="width: 100%">
            <!-- <el-table-column width="50" label="" align="center">
            <template></template>
          </el-table-column> -->
            <el-table-column width="50" label="STT" align="center">
              <template slot-scope="scope">
                {{ renderIndex(scope.$index) }}
              </template>
            </el-table-column>
            <el-table-column prop="TenNhanSu"
                             label="Tên Nhân Sự"
                             sortable
                             min-width="225">
              <template slot-scope="scope">
                <text-highlight :queries="search" style="word-break: normal;">
                  {{ scope.row.TenNhanSu }}
                </text-highlight>
              </template>
            </el-table-column>
            <!--<el-table-column
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
          </el-table-column>-->
            <el-table-column align="center" label="" width="125">
              <template slot="header">
                <el-button type="primary"
                           size="small"
                           icon="el-icon-plus"
                           class="filter-item"
                           @click="handleAdd">Thêm mới</el-button>
              </template>
              <template slot-scope="scope">
                <el-button @click="handleEdit(scope.$index, scope.row)"
                           type="primary"
                           title="Cập nhật"
                           icon="el-icon-edit"
                           size="mini"></el-button>
                <el-button @click="handleDelete(scope.row)"
                           type="danger"
                           icon="el-icon-delete"
                           title="Xóa"
                           size="mini"></el-button>
              </template>
            </el-table-column>
          </el-table>
          <el-pagination class="pt-2 pl-0"
                         :page-size="pagination"
                         background
                         style="width: 100%"
                         @size-change="handleSizeChange"
                         :current-page.sync="activePage"
                         :page-sizes="[10, 20, 50, 100, 500]"
                         layout="total,sizes,prev, pager, next"
                         :total="total">
          </el-pagination>
        </div>
      </div>
    </div>
    <el-dialog title="Thêm Nhân Sự"
               :visible.sync="dialogFormDisplayAdd"
               top="15px">
      <el-form :model="formData1"
               :rules="formRules"
               ref="formData1"
               label-width="150px">

        <el-form-item label="Tên Nhân Sự" prop="TenNhanSu">
          <el-input v-model="formData1.TenNhanSu"
                    type="text"
                    size="small"></el-input>
        </el-form-item>

        <el-form-item label="Đơn vị" prop="UnitId" size="small">
          <treeselect v-model="formData1.UnitId"
                      :multiple="false"
                      placeholder="Chọn đơn vị"
                      :searchable="false"
                      :options="ListDonVi" />
        </el-form-item>
        <el-form-item label="Dịch vụ" prop="DichVuId">
          <el-select v-model="formData1.DichVuId"
                     placeholder="Chọn dịch vụ"
                     class="w-100" filterable>
            <el-option v-for="item in ListDMDichVu"
                       :key="item.Id"
                       :label="item.TenDichVu"
                       :value="item.Id">
            </el-option>
          </el-select>
        </el-form-item>
        <el-form-item label="User" prop="UserId">
          <el-select v-model="formData1.UserId"
                     placeholder="Chọn user"
                     class="w-100" filterable>
            <el-option v-for="item in ListDMUser"
                       :key="item.Id"
                       :label="item.FullName"
                       :value="item.Id">
            </el-option>
          </el-select>
        </el-form-item>
        
      </el-form>
      <span slot="footer" class="dialog-footer">
        <el-button @click="resetForm1" size="small">Bỏ qua</el-button>
        <el-button type="primary" size="small" @click="addData">Cập nhật</el-button>
      </span>
    </el-dialog>

    <!--///////////////////////////////////////////-->
    <el-dialog title="Cập nhật Nhân Sự"
               :visible.sync="dialogFormDisplay"
               top="15px">
      <el-form :model="formData"
               :rules="formRules"
               ref="formData"
               label-width="150px">

        <el-form-item label="Tên Nhân Sự" prop="TenNhanSu">
          <el-input v-model="formData.TenNhanSu"
                    type="text"
                    size="small"></el-input>
        </el-form-item>

        <el-form-item label="Đơn vị" prop="UnitId" size="small">
          <treeselect v-model="formData.UnitId"
                      :multiple="false"
                      placeholder="Chọn đơn vị"
                      :searchable="false"
                      :options="ListDonVi" />
        </el-form-item>
        <el-form-item label="Dịch vụ" prop="DichVuId">
          <el-select v-model="formData.DichVuId"
                     placeholder="Chọn dịch vụ"
                     class="w-100" filterable>
            <el-option v-for="item in ListDMDichVu"
                       :key="item.Id"
                       :label="item.TenDichVu"
                       :value="item.Id">
            </el-option>
          </el-select>
        </el-form-item>
        <el-form-item label="User" prop="UserId">
          <el-select v-model="formData.UserId"
                     placeholder="Chọn user"
                     class="w-100" filterable>
            <el-option v-for="item in ListDMUser"
                       :key="item.Id"
                       :label="item.FullName"
                       :value="item.Id">

            </el-option>

          </el-select>
        </el-form-item>
      </el-form>
      <span slot="footer" class="dialog-footer">
        <el-button @click="resetForm" size="small">Bỏ qua</el-button>
        <el-button type="primary" size="small" @click="updateData">Cập nhật</el-button>
      </span>
    </el-dialog>
  </div>
</template>
<script>
  import Treeselect from "@riophae/vue-treeselect";
  // import the styles
  import "@riophae/vue-treeselect/dist/vue-treeselect.css";
import {
  addNhanSu,
  updateNhanSu,
  deleteNhanSu,
  getNhanSu,
  selectNhanSu,
  getListUnitAll,
  getListDanhMucYeuCau,
  getListUser,
} from "../../store/api";
  export default {
  components: { Treeselect },
  data() {
    return {
      dialogFormDisplay: false,
      dialogFormDisplayAdd: false,
      loading: false,
      isEditor: false,
      search: "",
      formData: {
        TenNhanSu: null,
        ThuTu: null,
        HieuLuc: null,
        UnitId: null,
        DichVuId: null,
        UserId : null,
      },
      formData1: {
        TenNhanSu: null,
        ThuTu: null,
        HieuLuc: null,
        UnitId: null,
        DichVuId: null,
        UserId: null,
      },
      formRules: {
        TenNhanSu: [
          {
            required: true,
            message: "Vui lòng nhập Tên Nhân Sự",
            trigger: "blur"
          }
        ],
        UnitId: [
          {
            required: true,
            message: "Vui lòng chọn Đơn Vị",
            trigger: "blur"
          }
        ],
        //UserId: [
        //  {
        //    required: true,
        //    message: "Vui lòng chọn user",
        //    trigger: "blur"
        //  }
        //],
        
      },
      listData: [],
      ListDonVi: [],
      ListDMDichVu: [],
      ListDMUser:[],
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
      this.formData1.UserId = null;
      this.formData1.TenNhanSu = null;
      this.formData1.DichVuId = null;
      this.formData1.UnitId = null;
      this.isEditor = false;
      this.dialogFormDisplayAdd = true;
    },
    handleEdit(index, row) {
     
      this.formData.UserId = row.UserId;
      this.formData.TenNhanSu = row.TenNhanSu;
      this.formData.DichVuId = row.DichVuId;
      this.formData.UnitId = row.UnitId;
      this.isEditor = true;
      this.dialogFormDisplay = true;
    },

    handleDelete(row) {
      this.$confirm("Bạn có chắc chắn muốn xóa?", "Thông báo", {
        confirmButtonText: "OK",
        cancelButtonText: "Cancel",
        type: "warning"
      }).then(() => {
        deleteNhanSu(row.Id).then(data => {
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
    resetForm1() {
      this.dialogFormDisplayAdd = false;
      return true;
    },
    getListData() {
      this.loading = true;
      selectNhanSu().then(data => {
        this.listData = data;
        this.total = data.length;
        this.loading = false;
      });
    },
    addData() {
      this.$refs.formData1.validate(valid => {
        if (valid) {
          if (this.isEditor == 0) {
            addNhanSu(this.formData1).then(data => {
              //console.log(data);
              this.getListData();
            });
          } 
          this.dialogFormDisplayAdd = false;
          
        } else {
          return false;
        }
      });
    },

    updateData() {
      this.$refs.formData.validate(valid => {
        if (valid) {
          if (this.isEditor == 0) {
            addNhanSu(this.formData).then(data => {
              //console.log(data);
              this.getListData();
            });
          } else {
            updateNhanSu(this.formData).then(data => {
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
    listUnit() {
      this.loading = true;
      getListUnitAll().then(data => {
        if (data != null) {
          this.ListDonVi = data;
        }
      });
    },
    handleSizeChange(val) {
      this.pagination = val;
    },
    renderData() {
      var _data = this.listData.filter(post => {
        return (
          post.TenNhanSu.toLowerCase().includes(this.search.toLowerCase()) 
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
    this.listUnit();
    getListDanhMucYeuCau().then(data => {
      if (data) {
        
        this.ListDMDichVu = data.DMDichVu;
        
      }
    });
    getListUser().then(data => {
      if (data) {

        this.ListDMUser = data;

      }
    });
  }
};
</script>
<style></style>
