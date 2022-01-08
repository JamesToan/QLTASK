<template>
  <div>
    <!-- start page title -->
    <div class="row">
      <div class="col-12">
        <div class="page-title-box">
          <div class="page-title-right">
            <ol class="breadcrumb m-0">
              <li class="breadcrumb-item">
                <a href="javascript: void(0);">Hồ sơ</a>
              </li>
              <li class="breadcrumb-item active">Đào tạo nghề</li>
            </ol>
          </div>
          <h4 class="page-title">Đào tạo nghề</h4>
        </div>
      </div>
    </div>
    <!-- end page title -->
    <div class="row" v-loading="loading">
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
              prop="TenCongViec"
              label="Tên khóa học"
              min-width="150"
            >
              <template slot-scope="scope">
                <text-highlight :queries="search" style="word-break: normal;">
                  {{ scope.row.TenKhoaHoc }}
                </text-highlight>
              </template>
            </el-table-column>
            <el-table-column
              prop="LoaiHinhDaoTao"
              label="Loại hình đào tạo"
              width="150"
            >
              <template slot-scope="scope">
                {{ scope.row.LoaiHinhDaoTao.TenLoaiHinh }}
              </template>
            </el-table-column>
            <el-table-column
              prop="ThoiGianDaoTao"
              label="Thời gian đào tạo"
              width="140"
            >
              <template slot-scope="scope"
                >{{ scope.row.ThoiGianDaoTao }}
              </template>
            </el-table-column>

            <el-table-column prop="Học phí" label="Học phí" width="200">
              <template slot-scope="scope">
                {{ formatNumber(scope.row.HocPhi) }}
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
      title="Khóa học đào tạo nghề"
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
        style="width: 620px"
        size="small"
      >
        <el-form-item label="Tên khóa học" prop="TenKhoaHoc">
          <el-input
            v-model="formData.TenKhoaHoc"
            type="text"
            size="small"
          ></el-input>
        </el-form-item>
        <el-row>
          <el-col :span="12">
            <el-form-item label="Thời gian đào tạo" prop="ThoiGianDaoTao">
              <el-input
                v-model="formData.ThoiGianDaoTao"
                type="number"
                size="small"
                placeholder="(tháng)"
              ></el-input>
            </el-form-item>
          </el-col>
          <el-col :span="12"
            ><el-form-item label="Học phí" prop="HocPhi">
              <el-input
                v-model="formData.HocPhi"
                type="number"
                size="small"
                placeholder="(đồng)"
              ></el-input>
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="12">
            <el-form-item label="Chứng chỉ" prop="VanBangId">
              <el-select
                v-model="formData.VanBangId"
                placeholder="Chọn chứng chỉ"
                class="w-100"
                clearable
              >
                <el-option
                  v-for="item in ListDMVanBang"
                  :key="item.Id"
                  :label="item.TenVanBang"
                  :value="item.Id"
                >
                </el-option>
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="Hạn đăng ký" prop="HanDangKy"
              ><el-date-picker
                v-model="formData.HanDangKy"
                type="date"
                placeholder="Chọn ngày"
                format="dd/MM/yyyy"
                size="small"
                style="width: 100%"
                value-format="yyyy-MM-dd"
              >
              </el-date-picker>
            </el-form-item>
          </el-col>
        </el-row>
        <el-form-item label="Mô tả khóa học" prop="MoTaKhoaHoc">
          <el-input
            v-model="formData.MoTaKhoaHoc"
            type="textarea"
            :rows="5"
            size="small"
          ></el-input>
        </el-form-item>
        <el-form-item label="Yêu cầu khóa học" prop="YeuCauKhoaHoc">
          <el-input
            v-model="formData.YeuCauKhoaHoc"
            type="textarea"
            :rows="5"
            size="small"
          ></el-input>
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
  getListDanhMucSVL,
  addDaoTaoNghe,
  updateDaoTaoNghe,
  deleteDaoTaoNghe,
  selectDaoTaoNghe
} from "../../store/api";
export default {
  data() {
    return {
      title: null,
      dialogFormDisplay: false,
      loading: false,
      isEditor: false,
      isXacThuc: false,
      search: "",
      formData: {
        UID: null,
        TenKhoaHoc: null,
        ThoiGianDaoTao: null,
        HocPhi: null,
        HanDangKy: null,
        VanBangId: null,
        YeuCauKhoaHoc: null
      },
      formRules: {
        TenKhoaHoc: [
          {
            required: true,
            message: "Vui lòng nhập tên khóa học",
            trigger: "blur"
          }
        ],
        ThoiGianDaoTao: [
          {
            required: true,
            message: "Vui lòng nhập thời gian đào tạo",
            trigger: "blur"
          }
        ],
        HocPhi: [
          {
            required: true,
            message: "Vui lòng nhập học phí",
            trigger: "blur"
          }
        ],
        VanBangId: [
          {
            required: true,
            message: "Vui lòng chọn chứng chỉ",
            trigger: "blur"
          }
        ]
      },
      RoleId: getRole(),
      listData: [],
      ListDMVanBang: [],
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
    formatNumber(num) {
      if (num) {
        return num.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
      } else return null;
    },
    formatDate(date) {
      if (date) {
        return moment(date).format("DD/MM/YYYY");
      } else return null;
    },
    formatDiaChi(tinh, huyen, noi) {
      return (
        (tinh ? tinh.TenTinh : "") +
        (huyen ? ", " + huyen.TenHuyen : "") +
        (noi ? ", " + noi : "")
      );
    },
    changeTinh(val) {
      var tinh = this.ListDMTinh.find(obj => obj.Id == val);
      if (tinh) {
        this.ListDMHuyen = tinh.Huyen;
      } else {
        this.ListDMHuyen = [];
      }
      delete this.formData.NoiLamViecHuyenId;
    },
    handleAdd() {
      if (this.$refs.formData !== undefined) {
        this.$refs.formData.resetFields();
      }
      this.formData = {
        NoiLamViecTinhId: "80"
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
    handleView(index, row) {
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
        deleteDaoTaoNghe(row.Id).then(data => {
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
        selectDaoTaoNghe().then(data => {
          this.listData = data;
          this.total = data.length;

          this.isXacThuc = true;
          this.loading = false;
        });
      }
    },

    updateData() {
      this.$refs.formData.validate(valid => {
        if (valid) {
          if (this.isEditor == 0) {
            //delete this.formData.LinhVuc;
            addDaoTaoNghe(this.formData).then(data => {
              //console.log(data);
              this.getListData();
            });
          } else {
            //delete this.formData.LinhVuc;
            updateDaoTaoNghe(this.formData).then(data => {
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
        return post.TenKhoaHoc.toLowerCase().includes(
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
    getListDanhMucSVL().then(data => {
      if (data) {
        this.ListDMVanBang = data.DMVanBang;
      }
    });

    this.getListData();
  }
};
</script>
<style></style>
