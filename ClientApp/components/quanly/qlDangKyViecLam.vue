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
              <li class="breadcrumb-item active">
                Đăng ký việc làm ngoài nước
              </li>
            </ol>
          </div>
          <h4 class="page-title">Quản lý Đăng ký việc làm ngoài nước</h4>
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
              prop="ThiTruong"
              label="Đăng ký thị trường"
              min-width="200"
            >
              <template slot-scope="scope">
                <text-highlight :queries="search" style="word-break: normal;">
                  {{ scope.row.ThiTruong.TenThiTruong }}
                </text-highlight>
              </template>
            </el-table-column>
            <el-table-column prop="HoTen" label="Họ tên" width="180">
              <template slot-scope="scope">
                <text-highlight :queries="search" style="word-break: normal;">
                  {{ scope.row.HoTen }}
                </text-highlight>
              </template>
            </el-table-column>
            <el-table-column
              prop="SoDienThoai"
              label="Số điện thoại, Email"
              width="180"
            >
              <template slot-scope="scope">
                {{ scope.row.SoDienThoai }}<br />{{ scope.row.Email }}
              </template>
            </el-table-column>
            <el-table-column
              prop="NgayDangKy"
              label="Ngày đăng ký"
              width="120"
              align="center"
            >
              <template slot-scope="scope">
                {{ formatDate(scope.row.NgayDangKy) }}
              </template>
            </el-table-column>
            <el-table-column prop="TinhTrangId" label="Tình trạng" width="150">
              <template slot-scope="scope">
                {{
                  scope.row.TinhTrang ? scope.row.TinhTrang.TenTinhTrang : ""
                }}
              </template>
            </el-table-column>
            <el-table-column align="center" label="" width="125">
              <template slot="header">
                <el-button
                  type="success"
                  size="small"
                  icon="el-icon-download"
                  class="filter-item"
                  @click="handleExport"
                  >Xuất DS</el-button
                ></template
              >
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
      title="Quản lý Đăng ký việc làm ngoài nước"
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
      >
        <el-form-item label="Thị trường" prop="ThiTruongId">
          <el-select
            v-model="formData.ThiTruongId"
            placeholder="Chọn thị trường"
            class="w-100"
          >
            <el-option
              v-for="item in ListThiTruong"
              :key="item.Id"
              :label="item.TenThiTruong"
              :value="item.Id"
            >
            </el-option>
          </el-select>
        </el-form-item>
        <el-form-item label="Họ tên" prop="HoTen">
          <el-input
            v-model="formData.HoTen"
            type="text"
            size="small"
          ></el-input>
        </el-form-item>
        <el-row>
          <el-col :span="12">
            <el-form-item label="Số điện thoại" prop="SoDienThoai">
              <el-input
                v-model="formData.SoDienThoai"
                type="text"
                size="small"
                placeholder="Nhập số điện thoại"
              ></el-input>
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="Email" prop="Email">
              <el-input
                v-model="formData.Email"
                type="email"
                size="small"
                placeholder="Email"
              ></el-input>
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="12">
            <el-form-item label="Ngày đăng ký" prop="NgayDangKy">
              <el-date-picker
                v-model="formData.NgayDangKy"
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
          <el-col :span="12">
            <el-form-item label="Tình trạng" prop="TinhTrangId">
              <el-select
                v-model="formData.TinhTrangId"
                placeholder="Chọn tình trạng"
                class="w-100"
              >
                <el-option
                  v-for="item in ListDMTinhTrangDangKy"
                  :key="item.Id"
                  :label="item.TenTinhTrang"
                  :value="item.Id"
                >
                </el-option>
              </el-select>
            </el-form-item>
          </el-col>
        </el-row>
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
  getCauHinh,
  getListDanhMucSVL,
  selectDangKyViecLam,
  updateDangKyViecLam,
  deleteDangKyViecLam
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
        Id: null,
        ThiTruongId: null,
        HoTen: null,
        Email: null,
        SoDienThoai: null,
        NgayDangKy: null,
        TinhTrangId: null
      },
      formRules: {
        ThiTruongId: [
          {
            required: true,
            message: "Vui lòng chọn thị trường",
            trigger: "blur"
          }
        ],
        HoTen: [
          {
            required: true,
            message: "Vui lòng nhập họ tên",
            trigger: "blur"
          }
        ],
        NgayDangKy: [
          {
            required: true,
            message: "Vui lòng chọn ngày đăng ký",
            trigger: "blur"
          }
        ],
        TinhTrangId: [
          {
            required: true,
            message: "Vui lòng chọn tình trạng",
            trigger: "blur"
          }
        ]
      },
      RoleId: getRole(),
      listData: [],
      ListThiTruong: [],
      ListDMTinhTrangDangKy: [],
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
    formatJson(filterVal, jsonData) {
      return jsonData.map(data =>
        filterVal.map(f => {
          let result;
          if (f.includes(".")) {
            let f1 = f.split(".")[0];
            let f2 = f.split(".")[1];
            result = data[f1][f2];
          } else {
            if (f.startsWith("Ngay")) {
              return this.formatDate(data[f]);
            }
            result = data[f];
          }
          if (f == "ThiTruong") {
            return result.TenThiTruong;
          }
          if (f == "TinhTrang") {
            return result.TenTinhTrang;
          }
          return result;
        })
      );
    },
    changeTinh(val) {
      var tinh = this.ListDMTinh.find(obj => obj.Id == val);
      if (tinh) {
        this.ListDMHuyen = tinh.Huyen;
      } else {
        this.ListDMHuyen = [];
      }
      delete this.formData.DiaChiHuyenId;
    },
    handleAdd() {
      if (this.$refs.formData !== undefined) {
        this.$refs.formData.resetFields();
      }
      this.formData = {
        DiaChiTinhId: "80"
      };

      this.isEditor = false;
      this.dialogFormDisplay = true;
    },
    handleActive(index, row) {
      row.XacThuc = !row.XacThuc;
      activeDangKyViecLam(row).then(data => {
        //console.log(data);
        this.$message({
          type: "success",
          message: row.XacThuc
            ? "Xác thực thành công!"
            : "Hủy xác thực thành công!"
        });
        this.getListData();
      });
    },
    handleEdit(index, row) {
      if (this.$refs.formData !== undefined) {
        this.$refs.formData.resetFields();
      }
      this.formData = Object.assign({}, row);
      delete this.formData.NganhNghe;
      delete this.formData.LoaiHinh;
      delete this.formData.DiaChiTinh;
      delete this.formData.DiaChiHuyen;

      this.isEditor = true;
      this.dialogFormDisplay = true;
    },
    handleDelete(row) {
      this.$confirm("Bạn có chắc chắn muốn xóa?", "Thông báo", {
        confirmButtonText: "OK",
        cancelButtonText: "Cancel",
        type: "warning"
      }).then(() => {
        deleteDangKyViecLam(row.Id).then(data => {
          this.$message({
            type: "success",
            message: "Xóa thành công!"
          });
          this.getListData();
        });
      });
    },

    handleExport() {
      import("../../vendor/Export2Excel").then(excel => {
        const tHeader = [
          "Họ tên",
          "Số điện thoại",
          "Email",
          "Đăng ký thị trường",
          "Ngày đăng ký",
          "Tình trạng"
        ];
        const filterVal = [
          "HoTen",
          "SoDienThoai",
          "Email",
          "ThiTruong",
          "NgayDangKy",
          "TinhTrang"
        ];
        const data = this.formatJson(filterVal, this.listData);
        console.log(data);
        var filename = "DSViecLamNgoaiNuoc_" + moment().format("YYYYMMDD_hhmmss");
        excel.export_json_to_excel({
          header: tHeader,
          data,
          filename: filename
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
        selectDangKyViecLam().then(data => {
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
            updateDangKyViecLam(this.formData).then(data => {
              //console.log(data);
              this.$message({
                type: "success",
                message: "Thêm mới thành công!"
              });
              this.getListData();
            });
          } else {
            //delete this.formData.LinhVuc;
            updateDangKyViecLam(this.formData).then(data => {
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
          post.HoTen.toLowerCase().includes(this.search.toLowerCase()) ||
          post.ThiTruong.TenThiTruong.toLowerCase().includes(
            this.search.toLowerCase()
          )
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
    getListDanhMucSVL().then(data => {
      if (data) {
        this.ListThiTruong = data.DMThiTruong;
        this.ListDMTinhTrangDangKy = data.DMTinhTrangDangKy;
      }
    });
  }
};
</script>
<style>
.el-input.is-disabled .el-input__inner {
  background-color: #fff !important;
  color: #606266 !important;
}
</style>
