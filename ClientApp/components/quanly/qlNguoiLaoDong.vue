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
              <li class="breadcrumb-item active">Người lao động</li>
            </ol>
          </div>
          <h4 class="page-title">Quản lý Người lao động</h4>
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
            <el-table-column prop="HoTen" label="Họ tên" width="200">
              <template slot-scope="scope">
                <text-highlight :queries="search" style="word-break: normal;">
                  {{ scope.row.HoTen }}
                </text-highlight>
              </template>
            </el-table-column>
            <el-table-column
              prop="GioiTinhId"
              label="Giới tính"
              width="80"
              align="center"
            >
              <template slot-scope="scope">
                {{
                  scope.row.GioiTinh
                    ? scope.row.GioiTinh.TenGioiTinh
                    : "Nam / Nữ"
                }}
              </template>
            </el-table-column>
            <el-table-column
              prop="NgaySinh"
              label="Ngày sinh"
              width="120"
              align="center"
            >
              <template slot-scope="scope">
                {{ formatDate(scope.row.NgaySinh) }}
              </template>
            </el-table-column>
            <el-table-column
              prop="SoDienThoai"
              label="Số điện thoại"
              width="120"
            >
              <template slot-scope="scope">
                <text-highlight :queries="search" style="word-break: normal;">
                  {{ scope.row.SoDienThoai }}
                </text-highlight>
              </template>
            </el-table-column>
            <!-- <el-table-column prop="Email" label="Email" width="150">
              <template slot-scope="scope">
                <text-highlight :queries="search" style="word-break: normal;">
                  {{ scope.row.Email }}
                </text-highlight>
              </template>
            </el-table-column> -->

            <el-table-column prop="DiaChi" label="Địa chỉ" min-width="225">
              <template slot-scope="scope">
                {{
                  formatDiaChi(
                    scope.row.DiaChiTinh,
                    scope.row.DiaChiHuyen,
                    scope.row.DiaChi
                  )
                }}
              </template>
            </el-table-column>
            <el-table-column prop="TinhTrang" label="Tình trạng" width="125">
              <template slot-scope="scope">
                {{
                  scope.row.TinhTrang ? scope.row.TinhTrang.TenTinhTrang : ""
                }}
              </template>
            </el-table-column>
            <el-table-column align="center" label="" width="185">
              <template slot="header" slot-scope="scope">
                <el-button
                  type="primary"
                  size="small"
                  icon="el-icon-plus"
                  class="filter-item"
                  :title="scope.row"
                  @click="handleAdd"
                  v-if="allowEdit"
                  >Thêm</el-button
                >
                <el-button
                  type="success"
                  size="small"
                  icon="el-icon-download"
                  class="filter-item"
                  title="Xuất DS"
                  @click="handleExport"
                  >Xuất</el-button
                >
              </template>
              <template slot-scope="scope">
                <el-button
                  @click="handleActive(scope.$index, scope.row)"
                  :type="scope.row.XacThuc ? 'success' : 'warning'"
                  :title="scope.row.XacThuc ? 'Đã xác thực' : 'Chưa xác thực'"
                  :icon="scope.row.XacThuc ? 'el-icon-check' : 'el-icon-close'"
                  size="mini"
                ></el-button>
                <el-button
                  @click="handleEdit(scope.$index, scope.row)"
                  type="primary"
                  :title="allowEdit ? 'Cập nhật' : 'Xem chi tiết'"
                  :icon="allowEdit ? 'el-icon-edit' : 'el-icon-view'"
                  size="mini"
                ></el-button>
                <el-button
                  @click="handleDelete(scope.row)"
                  type="danger"
                  icon="el-icon-delete"
                  title="Xóa"
                  size="mini"
                  v-if="allowEdit"
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
      title="Quản lý Người lao động"
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
        :disabled="!allowEdit"
      >
        <el-form-item label="Họ tên" prop="HoTen">
          <el-input
            v-model="formData.HoTen"
            type="text"
            size="small"
          ></el-input>
        </el-form-item>
        <el-row>
          <el-col :span="12">
            <el-form-item label="Giới tính" prop="GioiTinhId">
              <el-select
                v-model="formData.GioiTinhId"
                placeholder="Chọn giới tính"
                class="w-100"
              >
                <el-option
                  v-for="item in ListDMGioiTinh"
                  :key="item.Id"
                  :label="item.TenGioiTinh"
                  :value="item.Id"
                >
                </el-option>
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="Ngày sinh" prop="NgaySinh">
              <el-date-picker
                v-model="formData.NgaySinh"
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

        <el-row>
          <el-col :span="12"
            ><el-form-item label="Số CMND/CCCD" prop="SoDinhDanh">
              <el-input
                v-model="formData.SoDinhDanh"
                type="text"
                size="small"
              ></el-input>
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
                  v-for="item in ListDMTinhTrang"
                  :key="item.Id"
                  :label="item.TenTinhTrang"
                  :value="item.Id"
                >
                </el-option>
              </el-select>
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="12"
            ><el-form-item label="Số điện thoại" prop="SoDienThoai">
              <el-input
                v-model="formData.SoDienThoai"
                type="text"
                size="small"
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
              ></el-input
            ></el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="12">
            <el-form-item label="Địa chỉ" prop="DiaChiTinhId">
              <el-select
                v-model="formData.DiaChiTinhId"
                placeholder="Chọn tỉnh"
                class="w-100"
                @change="changeTinh(formData.DiaChiTinhId)"
                filterable
              >
                <el-option
                  v-for="item in ListDMTinh"
                  :key="item.Id"
                  :label="item.TenTinh"
                  :value="item.Id"
                >
                </el-option>
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item prop="DiaChiHuyenId">
              <el-select
                v-model="formData.DiaChiHuyenId"
                placeholder="Chọn huyện"
                class="w-100"
                filterable
              >
                <el-option
                  v-for="item in ListDMHuyen"
                  :key="item.Id"
                  :label="item.TenHuyen"
                  :value="item.Id"
                >
                </el-option>
              </el-select>
            </el-form-item>
          </el-col>
        </el-row>
        <el-form-item prop="DiaChi">
          <el-input
            v-model="formData.DiaChi"
            type="text"
            size="small"
          ></el-input>
        </el-form-item>
      </el-form>
      <span slot="footer" class="dialog-footer" v-if="allowEdit">
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
  selectNguoiLaoDong,
  updateNguoiLaoDong,
  activeNguoiLaoDong,
  deleteNguoiLaoDong
} from "../../store/api";
export default {
  data() {
    return {
      title: null,
      dialogFormDisplay: false,
      loading: false,
      isEditor: false,
      isXacThuc: false,
      allowEdit: true,
      search: "",
      formData: {
        Id: null,
        UserId: null,
        HoTen: null,
        MaSoThue: null,
        NganhNgheId: null,
        LoaiHinhId: null,
        TongSoLaoDong: null,
        MaKhachHang: null,
        Email: null,
        SoDienThoai: null,
        DiaChi: null,
        DiaChiTinhId: "80",
        DiaChiHuyenId: null,
        XacThuc: null
      },
      formRules: {
        UserId: [
          {
            required: true,
            message: "Vui lòng chọn user",
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
        GioiTinhId: [
          {
            required: true,
            message: "Vui lòng chọn giới tính",
            trigger: "blur"
          }
        ],
        NgaySinh: [
          {
            required: true,
            message: "Vui lòng chọn ngày sinh",
            trigger: "blur"
          }
        ],
        SoDinhDanh: [
          {
            required: true,
            message: "Vui lòng nhập CMND/CCCD",
            trigger: "blur"
          }
        ],
        SoDienThoai: [
          {
            required: true,
            message: "Vui lòng nhập số điện thoại",
            trigger: "blur"
          }
        ],
        DiaChiTinhId: [
          {
            required: true,
            message: "Vui lòng chọn tỉnh",
            trigger: "blur"
          }
        ]
      },
      RoleId: getRole(),
      listData: [],
      ListDMTinh: [],
      ListDMHuyen: [],
      ListDMGioiTinh: [],
      ListDMTinhTrang: [],
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
      delete this.formData.DiaChiHuyenId;
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
          if (f == "GioiTinh") {
            return result ? result.TenGioiTinh : "";
          }
          if (f == "DiaChiTinh") {
            return result ? result.TenTinh : "";
          }
          if (f == "DiaChiHuyen") {
            return result ? result.TenHuyen : "";
          }
          if (f == "TinhTrang") {
            return result ? result.TenTinhTrang : "";
          }
          return result;
        })
      );
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
      activeNguoiLaoDong(row).then(data => {
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
      delete this.formData.TinhTrang;
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
        deleteNguoiLaoDong(row.Id).then(data => {
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
          "Giới tính",
          "Ngày sinh",
          "Số điện thoại",
          "Email",
          "Địa chỉ",
          "Huyện",
          "Tỉnh",
          "Tình trạng"
        ];
        const filterVal = [
          "HoTen",
          "GioiTinh",
          "NgaySinh",
          "SoDienThoai",
          "Email",
          "DiaChi",
          "DiaChiHuyen",
          "DiaChiTinh",
          "TinhTrang"
        ];
        const data = this.formatJson(filterVal, this.listData);
        console.log(data);
        var filename = "DSNguoiLaoDong_" + moment().format("YYYYMMDD_hhmmss");
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
        selectNguoiLaoDong().then(data => {
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
            updateNguoiLaoDong(this.formData).then(data => {
              //console.log(data);
              this.$message({
                type: "success",
                message: "Thêm mới thành công!"
              });
              this.getListData();
            });
          } else {
            //delete this.formData.LinhVuc;
            updateNguoiLaoDong(this.formData).then(data => {
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
        return post.HoTen.toLowerCase().includes(this.search.toLowerCase());
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
    getCauHinh("ChoPhepCapNhatDN").then(data => {
      if (data) {
        this.allowEdit = data[0].GiaTri == "1";
        console.log(this.allowEdit);
      }
    });
    getListDanhMucSVL().then(data => {
      if (data) {
        this.ListDMTinh = data.DMTinh;
        this.ListDMHuyen = data.DMHuyen;
        this.ListDMGioiTinh = data.DMGioiTinh;
        this.ListDMTinhTrang = data.DMTinhTrang;
      }
    });

    this.getListData();
  }
};
</script>
<style>
.el-input.is-disabled .el-input__inner {
  background-color: #fff !important;
  color: #606266 !important;
}
</style>
