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
              <li class="breadcrumb-item active">Sàn việc làm</li>
            </ol>
          </div>
          <h4 class="page-title">Sàn việc làm</h4>
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
              style="width: 240px; float: right; z-index: 1"
              size="small"
            ></el-input>
          </div>
          <el-tabs type="card">
            <el-tab-pane label="Sàn đang diễn ra">
              <el-table
                :data="listData"
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
                  prop="TenSanViecLam"
                  label="Phiên giao dịch"
                  width="200"
                >
                  <template slot-scope="scope">
                    <text-highlight
                      :queries="search"
                      style="word-break: normal;"
                    >
                      {{ scope.row.TenSanViecLam }}
                    </text-highlight>
                  </template>
                </el-table-column>
                <el-table-column
                  prop="NoiToChuc"
                  label="Nơi tổ chức"
                  min-width="200"
                >
                  <template slot-scope="scope">
                    <text-highlight
                      :queries="search"
                      style="word-break: normal;"
                    >
                      {{ scope.row.NoiToChuc }}
                    </text-highlight>
                  </template>
                </el-table-column>
                <el-table-column prop="LoaiSanId" label="Loại sàn" width="100">
                  <template slot-scope="scope">
                    {{ scope.row.LoaiSan.TenLoaiSan }}
                  </template>
                </el-table-column>
                <el-table-column
                  prop="NgayBatDau"
                  label="Thời gian"
                  width="170"
                >
                  <template slot-scope="scope">
                    Từ: {{ formatDate(scope.row.NgayBatDau) }} <br />
                    Đến: {{ formatDate(scope.row.NgayKetThuc) }}
                  </template>
                </el-table-column>
                <!-- <el-table-column
                  prop="NgayKetThuc"
                  label="Ngày kết thúc"
                  width="120"
                  align="center"
                >
                  <template slot-scope="scope">
                    {{ formatDate(scope.row.NgayKetThuc) }}
                  </template>
                </el-table-column> -->
                <el-table-column label="Thống kê" width="150">
                  <template slot-scope="scope">
                    <el-button
                      type="text"
                      @click="handleViewDN(scope.$index, scope.row)"
                      >HS tuyển dụng:
                      {{ sumHS(scope.row.DoanhNghiepThamGia) }}</el-button
                    ><br />
                    <el-button
                      type="text"
                      @click="handleViewNLD(scope.$index, scope.row)"
                      >HS ứng tuyển:
                      {{ scope.row.NguoiLaoDongThamGia.length }}</el-button
                    >
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
            </el-tab-pane>
            <el-tab-pane label="Sàn đã kết thúc">
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
                  prop="TenSanViecLam"
                  label="Phiên giao dịch"
                  width="200"
                >
                  <template slot-scope="scope">
                    <text-highlight
                      :queries="search"
                      style="word-break: normal;"
                    >
                      {{ scope.row.TenSanViecLam }}
                    </text-highlight>
                  </template>
                </el-table-column>
                <el-table-column
                  prop="NoiToChuc"
                  label="Nơi tổ chức"
                  min-width="200"
                >
                  <template slot-scope="scope">
                    <text-highlight
                      :queries="search"
                      style="word-break: normal;"
                    >
                      {{ scope.row.NoiToChuc }}
                    </text-highlight>
                  </template>
                </el-table-column>
                <el-table-column prop="LoaiSanId" label="Loại sàn" width="100">
                  <template slot-scope="scope">
                    {{ scope.row.LoaiSan.TenLoaiSan }}
                  </template>
                </el-table-column>
                <el-table-column
                  prop="NgayBatDau"
                  label="Thời gian"
                  width="170"
                >
                  <template slot-scope="scope">
                    Từ: {{ formatDate(scope.row.NgayBatDau) }} <br />
                    Đến: {{ formatDate(scope.row.NgayKetThuc) }}
                  </template>
                </el-table-column>
                <!-- <el-table-column
                  prop="NgayKetThuc"
                  label="Ngày kết thúc"
                  width="120"
                  align="center"
                >
                  <template slot-scope="scope">
                    {{ formatDate(scope.row.NgayKetThuc) }}
                  </template>
                </el-table-column> -->

                <el-table-column label="Thống kê" width="150">
                  <template slot-scope="scope">
                    HS tuyển dụng: {{ sumHS(scope.row.DoanhNghiepThamGia)
                    }}<br />
                    HS ứng tuyển: {{ scope.row.NguoiLaoDongThamGia.length
                    }}<br />
                  </template>
                </el-table-column>
                <el-table-column align="center" label="" width="65">
                  <template slot="header"> </template>
                  <template slot-scope="scope">
                    <el-button
                      @click="handleEdit(scope.$index, scope.row)"
                      type="success"
                      title="Báo cáo"
                      icon="el-icon-download"
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
            </el-tab-pane>
          </el-tabs>
        </div>
      </div>
    </div>
    <el-dialog
      title="Quản lý Sàn việc làm"
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
        <el-form-item label="Phiên giao dịch" prop="TenSanViecLam">
          <el-input
            v-model="formData.TenSanViecLam"
            type="text"
            size="small"
          ></el-input>
        </el-form-item>
        <el-form-item label="Loại sàn" prop="LoaiSanId">
          <el-select
            v-model="formData.LoaiSanId"
            placeholder="Chọn loại sàn"
            class="w-100"
          >
            <el-option
              v-for="item in ListDMLoaiSan"
              :key="item.Id"
              :label="item.TenLoaiSan"
              :value="item.Id"
            >
            </el-option>
          </el-select>
        </el-form-item>
        <el-form-item label="Nơi tổ chức" prop="NoiToChuc">
          <el-input
            v-model="formData.NoiToChuc"
            type="text"
            size="small"
          ></el-input>
        </el-form-item>
        <el-row>
          <el-col :span="12"
            ><el-form-item label="Ngày bắt đầu" prop="NgayBatDau">
              <el-date-picker
                v-model="formData.NgayBatDau"
                type="datetime"
                placeholder="Chọn ngày"
                format="dd/MM/yyyy HH:mm:ss"
                size="small"
                style="width: 100%"
                value-format="yyyy-MM-ddTHH:mm:ss"
              >
              </el-date-picker>
            </el-form-item>
          </el-col>

          <el-col :span="12"
            ><el-form-item label="Ngày kết thúc" prop="NgayKetThuc">
              <el-date-picker
                v-model="formData.NgayKetThuc"
                type="datetime"
                placeholder="Chọn ngày"
                format="dd/MM/yyyy HH:mm:ss"
                size="small"
                style="width: 100%"
                value-format="yyyy-MM-ddTHH:mm:ss"
              >
              </el-date-picker>
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
    <el-dialog
      title="Tham gia Sàn việc làm"
      :visible.sync="dialogFormDisplayDN"
      top="15px"
      center
    >
      <el-input
        clearable
        v-model="searchHs"
        placeholder="Tìm kiếm"
        style="width: 240px; float: right; z-index: 1"
        size="small"
      ></el-input>
      <el-tabs type="card">
        <el-tab-pane label="Đã tham gia">
          <el-table
            :data="ListHsDaThamGia1"
            @selection-change="handleSelectionChange"
            border
            v-loading="loading"
            default-expand-all
            row-key="Id"
            style="width: 100%"
          >
            <!-- <el-table-column type="selection" width="50px" align="center">
            </el-table-column> -->
            <el-table-column width="50" label="STT" align="center">
              <template slot-scope="scope">
                {{ scope.$index + 1 }}
              </template>
            </el-table-column>
            <el-table-column
              prop="DoanhNghiep"
              label="Doanh nghiệp"
              width="250"
            >
              <template slot-scope="scope">
                {{ scope.row.DoanhNghiep.TenDoanhNghiep }}
              </template>
            </el-table-column>
            <el-table-column
              prop="HoSoTuyenDung"
              label="Hồ sơ tuyển dụng"
              min-width="200"
            >
              <template slot-scope="scope">
                {{ scope.row.HoSoTuyenDung.TenCongViec }}
              </template>
            </el-table-column>
            <el-table-column
              prop="SoLuong"
              label="Số lượng"
              width="80"
              align="right"
            >
              <template slot-scope="scope">
                {{ scope.row.HoSoTuyenDung.SoLuong }}
              </template>
            </el-table-column>
            <el-table-column align="center" label="" width="65">
              <template slot-scope="scope">
                <el-button
                  @click="removeHoSo(scope.$index, scope.row)"
                  type="danger"
                  title="Tham gia"
                  icon="el-icon-remove"
                  size="mini"
                ></el-button>
              </template>
            </el-table-column>
          </el-table>
        </el-tab-pane>
        <el-tab-pane label="Chưa tham gia">
          <el-table
            :data="renderData1()"
            @selection-change="handleSelectionChange"
            border
            v-loading="loading"
            default-expand-all
            row-key="Id"
            style="width: 100%"
          >
            <!-- <el-table-column type="selection" width="50px" align="center">
            </el-table-column> -->
            <el-table-column width="50" label="STT" align="center">
              <template slot-scope="scope">
                {{ scope.$index + 1 }}
              </template>
            </el-table-column>
            <el-table-column
              prop="DoanhNghiep"
              label="Doanh nghiệp"
              width="250"
            >
              <template slot-scope="scope">
                <text-highlight :queries="searchHs" style="word-break: normal;">
                  {{ scope.row.DoanhNghiep.TenDoanhNghiep }}
                </text-highlight>
              </template>
            </el-table-column>
            <el-table-column
              prop="TenSanViecLam"
              label="Hồ sơ tuyển dụng"
              min-width="200"
            >
              <template slot-scope="scope">
                <text-highlight :queries="searchHs" style="word-break: normal;">
                  {{ scope.row.TenCongViec }}
                </text-highlight>
              </template>
            </el-table-column>
            <el-table-column
              prop="SoLuong"
              label="Số lượng"
              width="80"
              align="right"
            >
              <template slot-scope="scope">
                {{ scope.row.SoLuong }}
              </template>
            </el-table-column>
            <el-table-column align="center" label="" width="65">
              <template slot-scope="scope">
                <el-button
                  @click="joinHoSo(scope.$index, scope.row)"
                  type="success"
                  title="Tham gia"
                  icon="el-icon-plus"
                  size="mini"
                ></el-button>
              </template>
            </el-table-column>
          </el-table>
        </el-tab-pane>
      </el-tabs>
      <span slot="footer" class="dialog-footer">
        <el-button @click="resetForm" size="small">Bỏ qua</el-button>
        <!-- <el-button
          type="primary"
          size="small"
          @click="joinDN()"
          >Cập nhật</el-button
        > -->
      </span>
    </el-dialog>
    <el-dialog
      title="Tham gia Sàn việc làm"
      :visible.sync="dialogFormDisplayNLD"
      top="15px"
      width="60%"
      center
    >
      <el-input
        clearable
        v-model="searchHs"
        placeholder="Tìm kiếm"
        style="width: 240px; float: right; z-index: 1"
        size="small"
      ></el-input>
      <el-tabs type="card">
        <el-tab-pane label="Đã tham gia">
          <el-table
            :data="ListHsDaThamGia2"
            @selection-change="handleSelectionChange"
            border
            v-loading="loading"
            default-expand-all
            row-key="Id"
            style="width: 100%"
          >
            <!-- <el-table-column type="selection" width="50px" align="center">
            </el-table-column> -->
            <el-table-column width="50" label="STT" align="center">
              <template slot-scope="scope">
                {{ scope.$index + 1 }}
              </template>
            </el-table-column>
            <el-table-column prop="HoTen" label="Họ tên" width="200">
              <template slot-scope="scope">
                {{ scope.row.NguoiLaoDong.HoTen }}
              </template>
            </el-table-column>
            <el-table-column
              prop="HoSoTuyenDung"
              label="Hồ sơ ứng tuyển"
              min-width="200"
            >
              <template slot-scope="scope">
                {{ scope.row.HoSoTuyenDung.TenCongViec }}
              </template>
            </el-table-column>
            <el-table-column
              prop="DoanhNghiep"
              label="Doanh nghiệp"
              width="180"
            >
              <template slot-scope="scope">
                {{ scope.row.DoanhNghiep.TenDoanhNghiep }}
              </template>
            </el-table-column>
            <el-table-column align="center" label="" width="65">
              <template slot-scope="scope">
                <el-button
                  @click="removeHoSo2(scope.$index, scope.row)"
                  type="danger"
                  title="Tham gia"
                  icon="el-icon-remove"
                  size="mini"
                ></el-button>
              </template>
            </el-table-column>
          </el-table>
        </el-tab-pane>
        <el-tab-pane label="Chưa tham gia">
          <el-table
            :data="renderData2()"
            @selection-change="handleSelectionChange"
            border
            v-loading="loading"
            default-expand-all
            row-key="Id"
            style="width: 100%"
          >
            <!-- <el-table-column type="selection" width="50px" align="center">
            </el-table-column> -->
            <el-table-column width="50" label="STT" align="center">
              <template slot-scope="scope">
                {{ scope.$index + 1 }}
              </template>
            </el-table-column>
            <el-table-column prop="HoTen" label="Họ tên" width="250">
              <template slot-scope="scope">
                <text-highlight :queries="searchHs" style="word-break: normal;">
                  {{ scope.row.HoTen }} - {{ formatYear(scope.row.NgaySinh) }}
                </text-highlight>
              </template>
            </el-table-column>
            <el-table-column
              prop="TenSanViecLam"
              label="Hồ sơ ứng tuyển"
              min-width="200"
            >
              <template slot-scope="scope">
                <el-select
                  v-model="scope.row.HoSoTuyenDung"
                  placeholder="Chọn hồ sơ ứng tuyển"
                  class="w-100"
                >
                  <el-option
                    v-for="item in ListHoSoTuyenDung"
                    :key="item.Id"
                    :label="
                      item.DoanhNghiep.TenDoanhNghiep +
                        ' - ' +
                        item.HoSoTuyenDung.TenCongViec
                    "
                    :value="item.HoSoTuyenDung"
                  >
                  </el-option>
                </el-select>
              </template>
            </el-table-column>
            <el-table-column align="center" label="" width="65">
              <template slot-scope="scope">
                <el-button
                  @click="joinHoSo2(scope.$index, scope.row)"
                  type="success"
                  title="Tham gia"
                  icon="el-icon-plus"
                  size="mini"
                ></el-button>
              </template>
            </el-table-column>
          </el-table>
        </el-tab-pane>
      </el-tabs>
      <span slot="footer" class="dialog-footer">
        <el-button @click="resetForm" size="small">Bỏ qua</el-button>
        <!-- <el-button
          type="primary"
          size="small"
          @click="joinDN()"
          >Cập nhật</el-button
        > -->
      </span>
    </el-dialog>
  </div>
</template>
<script>
import moment from "moment";
import { getRole, getUser } from "../../store/common";
import {
  getListDanhMucSVL,
  addSanViecLam,
  updateSanViecLam,
  deleteSanViecLam,
  selectSanViecLam,
  getSVLDoanhNghiep,
  getSVLNguoiLaoDong,
  joinSVLDoanhNghiep,
  removeSVLDoanhNghiep,
  joinSVLNguoiLaoDong,
  removeSVLNguoiLaoDong
} from "../../store/api";
export default {
  data() {
    return {
      title: null,
      dialogFormDisplay: false,
      dialogFormDisplayDN: false,
      dialogFormDisplayNLD: false,
      loading: false,
      isEditor: false,
      isXacThuc: false,
      search: "",
      searchHs: "",
      formData: {
        TenSanViecLam: null,
        NgayBatDau: null,
        NgayKetThuc: null
      },
      formRules: {
        TenSanViecLam: [
          {
            required: true,
            message: "Vui lòng nhập Tên phiên giao dịch",
            trigger: "blur"
          }
        ],
        LoaiSanId: [
          {
            required: true,
            message: "Vui lòng chọn loại sàn",
            trigger: "blur"
          }
        ],
        NgayBatDau: [
          {
            required: true,
            message: "Vui lòng nhập Ngày bắt đầu",
            trigger: "blur"
          }
        ],
        NgayKetThuc: [
          {
            required: true,
            message: "Vui lòng nhập Ngày kết thúc",
            trigger: "blur"
          }
        ]
      },
      RoleId: getRole(),
      SanViecLamId: null,
      DoanhNghiepId: null,
      listData: [],
      listData2: [],
      ListDMTinh: [],
      ListDMHuyen: [],
      ListDMLoaiSan: [],
      pagination: 10,
      total: 10,
      activePage: 1,
      dataFilter: null,
      ListHsChuaThamGia1: [],
      ListHsDaThamGia1: [],
      ListHsChuaThamGia2: [],
      ListHsDaThamGia2: [],
      ListHoSoTuyenDung: [],
      multipleSelection: []
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
        return moment(date).format("DD/MM/YYYY HH:mm");
      } else return null;
    },
    formatYear(date) {
      if (date) {
        return moment(date).format("YYYY");
      } else return null;
    },
    sumHS(dn) {
      if (dn) {
        var hs = dn.map(e => e.HoSoTuyenDung.SoLuong);
        return hs.reduce((a, b) => a + b, 0);
      } else return 0;
    },
    handleAdd() {
      if (this.$refs.formData !== undefined) {
        this.$refs.formData.resetFields();
      }
      this.formData = {};

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
    handleViewDN(index, row) {
      this.SanViecLamId = row.Id;
      getSVLDoanhNghiep(row.Id).then(response => {
        this.ListHsDaThamGia1 = response.DaThamGia;
        this.ListHsChuaThamGia1 = response.ChuaThamGia;
        this.dialogFormDisplayDN = true;
      });
    },
    handleViewNLD(index, row) {
      this.SanViecLamId = row.Id;
      getSVLNguoiLaoDong(row.Id).then(response => {
        this.ListHsDaThamGia2 = response.DaThamGia;
        this.ListHsChuaThamGia2 = response.ChuaThamGia;
        this.ListHoSoTuyenDung = response.TuyenDung;
        this.dialogFormDisplayNLD = true;
      });
    },
    handleDelete(row) {
      this.$confirm("Bạn có chắc chắn muốn xóa?", "Thông báo", {
        confirmButtonText: "OK",
        cancelButtonText: "Cancel",
        type: "warning"
      }).then(() => {
        deleteSanViecLam(row.Id).then(data => {
          this.$message({
            type: "success",
            message: "Xóa thành công!"
          });
          this.getListData();
        });
      });
    },
    handleSelectionChange(val) {
      this.multipleSelection = val;
    },
    joinHoSo(index, row) {
      var data = {
        SanViecLamId: this.SanViecLamId,
        DoanhNghiepId: row.DoanhNghiepId,
        HoSoTuyenDungId: row.Id
      };
      joinSVLDoanhNghiep(data).then(response => {
        getSVLDoanhNghiep(this.SanViecLamId).then(response => {
          this.ListHsDaThamGia1 = response.DaThamGia;
          this.ListHsChuaThamGia1 = response.ChuaThamGia;
        });
      });
      this.getListData();
    },
    removeHoSo(index, row) {
      var data = {
        Id: row.Id
      };
      removeSVLDoanhNghiep(data).then(response => {
        getSVLDoanhNghiep(this.SanViecLamId).then(response => {
          this.ListHsDaThamGia1 = response.DaThamGia;
          this.ListHsChuaThamGia1 = response.ChuaThamGia;
        });
      });
      this.getListData();
    },
    removeHoSo2(index, row) {
      var data = {
        Id: row.Id
      };
      removeSVLNguoiLaoDong(data).then(response => {
        getSVLNguoiLaoDong(this.SanViecLamId).then(response => {
          this.ListHsDaThamGia2 = response.DaThamGia;
          this.ListHsChuaThamGia2 = response.ChuaThamGia;
        });
      });
      this.getListData();
    },
    joinHoSo2(index, row) {
      //console.log(row.HoSoTuyenDung);
      var data = {
        SanViecLamId: this.SanViecLamId,
        DoanhNghiepId: row.HoSoTuyenDung.DoanhNghiepId,
        HoSoTuyenDungId: row.HoSoTuyenDung.Id,
        NguoiLaoDongId: row.Id
      };
      joinSVLNguoiLaoDong(data).then(response => {
        getSVLNguoiLaoDong(this.SanViecLamId).then(response => {
          this.ListHsDaThamGia2 = response.DaThamGia;
          this.ListHsChuaThamGia2 = response.ChuaThamGia;
        });
      });
      this.getListData();
    },
    resetForm() {
      this.dialogFormDisplay = false;
      this.dialogFormDisplayDN = false;
      return true;
    },
    getListData() {
      this.loading = true;
      //Quản lý
      selectSanViecLam(true).then(data => {
        this.listData = data;
        this.total = data.length;
        this.loading = false;
      });
    },
    getListData2() {
      this.loading = true;
      //Quản lý
      selectSanViecLam(false).then(data => {
        this.listData2 = data;
        this.total = data.length;
        this.loading = false;
      });
    },
    updateData() {
      this.$refs.formData.validate(valid => {
        if (valid) {
          if (this.isEditor == 0) {
            //delete this.formData.LinhVuc;
            addSanViecLam(this.formData).then(data => {
              //console.log(data);
              this.getListData();
            });
          } else {
            //delete this.formData.LinhVuc;
            updateSanViecLam(this.formData).then(data => {
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
      var _data = this.listData2.filter(post => {
        return (
          post.TenSanViecLam.toLowerCase().includes(
            this.search.toLowerCase()
          ) || post.NoiToChuc.toLowerCase().includes(this.search.toLowerCase())
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
    },
    renderData1() {
      var _data = this.ListHsChuaThamGia1.filter(post => {
        return (
          post.TenCongViec.toLowerCase().includes(
            this.searchHs.toLowerCase()
          ) ||
          post.DoanhNghiep.TenDoanhNghiep.toLowerCase().includes(
            this.searchHs.toLowerCase()
          )
        );
      });
      //this.total = _data.length;
      return _data;
    },
    renderData2() {
      var _data = this.ListHsChuaThamGia2.filter(post => {
        return post.HoTen.toLowerCase().includes(this.searchHs.toLowerCase());
      });
      //this.total = _data.length;
      return _data;
    }
  },

  created() {
    getListDanhMucSVL().then(data => {
      if (data) {
        this.ListDMTinh = data.DMTinh;
        this.ListDMHuyen = data.DMHuyen;
        this.ListDMLoaiSan = data.DMLoaiSan;
      }
    });

    this.getListData();
    this.getListData2();
  }
};
</script>
<style></style>
