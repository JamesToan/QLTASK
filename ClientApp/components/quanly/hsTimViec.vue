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
              <li class="breadcrumb-item active">Tìm việc</li>
            </ol>
          </div>
          <h4 class="page-title">Hồ sơ Tìm việc</h4>
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
            <el-table-column prop="NguoiLaoDongId" label="Họ tên" width="200">
              <template slot-scope="scope">
                <text-highlight :queries="search" style="word-break: normal;">
                  {{ scope.row.NguoiLaoDong.HoTen }}
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
                  scope.row.NguoiLaoDong.GioiTinh
                    ? scope.row.NguoiLaoDong.GioiTinh.TenGioiTinh
                    : "Nam / Nữ"
                }}
              </template>
            </el-table-column>
            <el-table-column
              prop="TenCongViec"
              label="Tên công việc"
              min-width="250"
            >
              <template slot-scope="scope">
                <text-highlight :queries="search" style="word-break: normal;">
                  {{ scope.row.TenCongViec }}
                </text-highlight>
              </template>
            </el-table-column>
            <!-- <el-table-column prop="ViTriCongViecId" label="Vị trí" width="180">
              <template slot-scope="scope">
                {{ scope.row.ViTriCongViec.TenViTriCongViec }}
              </template>
            </el-table-column> -->
            <el-table-column prop="NoiLamViec" label="Nơi làm việc" width="130">
              <template slot-scope="scope">
                {{
                  formatDiaChi(
                    scope.row.NoiLamViecTinh,
                    scope.row.NoiLamViecHuyen,
                    scope.row.NoiLamViec
                  )
                }}
              </template>
            </el-table-column>
            <el-table-column prop="TinhTrang" label="Tình trạng" width="125">
              <template slot-scope="scope">
                {{
                  scope.row.NguoiLaoDong.TinhTrang
                    ? scope.row.NguoiLaoDong.TinhTrang.TenTinhTrang
                    : ""
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
                  @click="handleView(scope.$index, scope.row)"
                  type="info"
                  title="Xem chi tiết"
                  icon="el-icon-view"
                  size="mini"
                  v-if="!allowEdit"
                ></el-button>
                <el-button
                  @click="handleEdit(scope.$index, scope.row)"
                  type="primary"
                  title="Cập nhật"
                  icon="el-icon-edit"
                  size="mini"
                  v-if="allowEdit"
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
      title="Hồ sơ Tìm việc"
      :visible.sync="dialogFormDisplay"
      top="15px"
      center
    >
      <el-form
        :model="formData"
        :rules="formRules"
        :disabled="!allowEdit"
        ref="formData"
        label-width="140px"
        class="m-auto"
        size="small"
      >
        <el-form-item label="Người lao động" prop="NguoiLaoDongId">
          <el-select
            v-model="formData.NguoiLaoDongId"
            placeholder="Chọn người lao động"
            class="w-100"
            filterable
          >
            <el-option
              v-for="item in listNguoiLaoDong"
              :key="item.Id"
              :label="item.HoTen + ' - ' + item.SoDienThoai"
              :value="item.Id"
            >
            </el-option>
          </el-select>
        </el-form-item>
        <el-form-item label="Tên công việc" prop="NganhNgheId">
          <el-input
            v-model="formData.TenCongViec"
            type="text"
            size="small"
          ></el-input>
        </el-form-item>
        <el-form-item label="Nghề nghiệp" prop="NganhNgheId">
          <el-select
            v-model="formData.NganhNgheId"
            placeholder="Chọn nghề nghiệp"
            class="w-100"
            clearable
            filterable
          >
            <el-option
              v-for="item in ListDMNganhNghe"
              :key="item.Id"
              :label="item.TenNganhNghe"
              :value="item.Id"
            >
            </el-option>
          </el-select>
        </el-form-item>
        <el-row>
          <el-col :span="12"
            ><el-form-item label="Vị trí công việc" prop="ViTriCongViecId">
              <el-select
                v-model="formData.ViTriCongViecId"
                placeholder="Chọn vị trí công việc"
                class="w-100"
                clearable
                filterable
              >
                <el-option
                  v-for="item in ListDMViTriCongViec"
                  :key="item.Id"
                  :label="item.TenViTriCongViec"
                  :value="item.Id"
                >
                </el-option>
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="Mức lương" prop="MucLuongId">
              <el-select
                v-model="formData.MucLuongId"
                placeholder="Chọn mức lương"
                class="w-100"
                clearable
                filterable
              >
                <el-option
                  v-for="item in ListDMMucLuong"
                  :key="item.Id"
                  :label="item.TenMucLuong"
                  :value="item.Id"
                >
                </el-option>
              </el-select>
            </el-form-item>
          </el-col>
        </el-row>

        <el-row>
          <el-col :span="12">
            <el-form-item label="Nơi làm việc" prop="NoiLamViecTinhId">
              <el-select
                v-model="formData.NoiLamViecTinhId"
                placeholder="Chọn tỉnh"
                class="w-100"
                @change="changeTinh(formData.NoiLamViecTinhId)"
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
            <el-form-item prop="NoiLamViecHuyenId">
              <el-select
                v-model="formData.NoiLamViecHuyenId"
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
        <el-form-item label="" prop="NoiLamViec">
          <el-input
            v-model="formData.NoiLamViec"
            type="text"
            size="small"
          ></el-input>
        </el-form-item>
        <el-row>
          <el-col :span="12">
            <el-form-item label="Văn hóa" prop="VanHoaId">
              <el-select
                v-model="formData.VanHoaId"
                placeholder="Chọn văn hóa"
                class="w-100"
                clearable
              >
                <el-option
                  v-for="item in ListDMVanHoa"
                  :key="item.Id"
                  :label="item.TenVanHoa"
                  :value="item.Id"
                >
                </el-option>
              </el-select> </el-form-item
          ></el-col>
          <el-col :span="12">
            <el-form-item label="Văn bằng" prop="VanBangId">
              <el-select
                v-model="formData.VanBangId"
                placeholder="Chọn văn bằng"
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
        </el-row>
        <el-row>
          <el-col :span="12">
            <el-form-item label="Ngoại ngữ" prop="NgoaiNguId">
              <el-select
                v-model="formData.NgoaiNguId"
                placeholder="Chọn chứng chỉ ngoại ngữ"
                class="w-100"
                clearable
              >
                <el-option
                  v-for="item in ListDMNgoaiNgu"
                  :key="item.Id"
                  :label="item.TenNgoaiNgu"
                  :value="item.Id"
                >
                </el-option>
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="Tin học" prop="TinHocId">
              <el-select
                v-model="formData.TinHocId"
                placeholder="Chọn chứng chỉ tin học"
                class="w-100"
                clearable
              >
                <el-option
                  v-for="item in ListDMTinHoc"
                  :key="item.Id"
                  :label="item.TenTinHoc"
                  :value="item.Id"
                >
                </el-option>
              </el-select> </el-form-item
          ></el-col>
        </el-row>
        <el-form-item label="Chuyên ngành" prop="ChuyenNganh">
          <el-input
            v-model="formData.ChuyenNganh"
            type="text"
            size="small"
          ></el-input>
        </el-form-item>
        <el-form-item label="Kinh nghiệm" prop="KinhNghiem">
          <ckeditor
            :editor="editor"
            v-model="formData.KinhNghiem"
            :config="editorConfig"
            :disabled="!allowEdit"
          ></ckeditor>
        </el-form-item>
        <el-form-item label="Kỹ năng" prop="KyNang">
          <ckeditor
            :editor="editor"
            v-model="formData.KyNang"
            :config="editorConfig"
            :disabled="!allowEdit"
          ></ckeditor>
        </el-form-item>
        <!-- <el-form-item label="Ưu điểm" prop="UuDiem">
          <ckeditor
            :editor="editor"
            v-model="formData.UuDiem"
            :config="editorConfig"
            :disabled="!allowEdit"
          ></ckeditor>
        </el-form-item>
        <el-form-item label="Khuyết điểm" prop="KhuyetDiem">
          <ckeditor
            :editor="editor"
            v-model="formData.KhuyetDiem"
            :config="editorConfig"
            :disabled="!allowEdit"
          ></ckeditor>
        </el-form-item> -->
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
import ClassicEditor from "@ckeditor/ckeditor5-build-classic";
import moment from "moment";
import { getRole, getUser } from "../../store/common";
import {
  getCauHinh,
  getListDanhMucSVL,
  addHoSoTimViec,
  activeHoSoTimViec,
  updateHoSoTimViec,
  deleteHoSoTimViec,
  getHoSoTimViec,
  selectHoSoTimViec,
  selectNguoiLaoDong
} from "../../store/api";
export default {
  data() {
    return {
      title: null,
      dialogFormDisplay: false,
      loading: false,
      isEditor: false,
      isXacThuc: false,
      allowEdit: false,
      search: "",
      formData: {
        UID: null,
        ViTriCongViecId: null,
        TenCongViec: null,
        SoLuong: null,
        MucLuongTu: null,
        MucLuongDen: null,
        NoiLamViec: null,
        NoiLamViecTinhId: "80",
        NoiLamViecHuyenId: null,
        ThoiGianLamViecId: null,
        HanNop: null,
        MoTaCongViec: null,
        YeuCauCongViec: null,
        PhucLoi: null
      },
      formRules: {
        NguoiLaoDongId: [
          {
            required: true,
            message: "Vui lòng chọn người lao động",
            trigger: "blur"
          }
        ],
        TenCongViec: [
          {
            required: true,
            message: "Vui lòng nhập tên công việc",
            trigger: "blur"
          }
        ],
        NganhNgheId: [
          {
            required: true,
            message: "Vui lòng chọn nghề nghiệp",
            trigger: "blur"
          }
        ],
        ViTriCongViecId: [
          {
            required: true,
            message: "Vui lòng chọn vị trí công việc",
            trigger: "blur"
          }
        ],
        NoiLamViecTinhId: [
          {
            required: true,
            message: "Vui lòng chọn tỉnh",
            trigger: "blur"
          }
        ]
      },
      editor: ClassicEditor,
      editorConfig: {
        // The configuration of the editor.
        ckfinder: {
          // Upload the images to the server using the CKFinder QuickUpload command.
          uploadUrl:
            "/api/TapTin/CKUpload?command=QuickUpload&type=Images&responseType=json"
        }
      },
      RoleId: getRole(),
      listData: [],
      listNguoiLaoDong: [],
      ListDMGioiTinh: [],
      ListDMTinh: [],
      ListDMHuyen: [],
      ListDMNganhNghe: [],
      ListDMMucLuong: [],
      ListDMThoiGianLamViec: [],
      ListDMViTriCongViec: [],
      ListDMVanHoa: [],
      ListDMNgoaiNgu: [],
      ListDMTinHoc: [],
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

    formatJson(filterVal, jsonData) {
      return jsonData.map(data =>
        filterVal.map(f => {
          let result;
          if (f.includes(".")) {
            let f1 = f.split(".")[0];
            let f2 = f.split(".")[1];
            result = data[f1][f2];

            if (f2 == "GioiTinh") {
              return result ? result.TenGioiTinh : "";
            }
            if (f2 == "NgaySinh") {
              return this.formatDate(result);
            }
            if (f2 == "DiaChiTinh") {
              return result ? result.TenTinh : "";
            }
            if (f2 == "DiaChiHuyen") {
              return result ? result.TenHuyen : "";
            }
            if (f2 == "TinhTrang") {
              return result ? result.TenTinhTrang : "";
            }
          } else {
            if (f.startsWith("Ngay")) {
              return this.formatDate(data[f]);
            }
            result = data[f];
          }
          if (f == "NganhNghe") {
            return result ? result.TenNganhNghe : "";
          }
          if (f == "NoiLamViecHuyen") {
            return result ? result.TenHuyen : "";
          }
          if (f == "VanHoa") {
            return result ? result.TenVanHoa : "";
          }
          if (f == "NgoaiNgu") {
            return result ? result.TenChungChi : "";
          }
          if (f == "TinHoc") {
            return result ? result.TenChungChi : "";
          }
          if (f == "VanBang") {
            return result ? result.TenVanBang : "";
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
        NoiLamViecTinhId: "80"
      };

      this.isEditor = false;
      this.dialogFormDisplay = true;
    },
    handleActive(index, row) {
      row.XacThuc = !row.XacThuc;
      activeHoSoTimViec(row).then(data => {
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

      this.isEditor = true;
      this.dialogFormDisplay = true;
    },
    handleView(index, row) {
      if (this.$refs.formData !== undefined) {
        this.$refs.formData.resetFields();
      }
      this.formData = Object.assign({}, row);
      this.dialogFormDisplay = true;
    },
    handleDelete(row) {
      this.$confirm("Bạn có chắc chắn muốn xóa?", "Thông báo", {
        confirmButtonText: "OK",
        cancelButtonText: "Cancel",
        type: "warning"
      }).then(() => {
        deleteHoSoTimViec(row.Id).then(data => {
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
          "Ngành nghề",
          "Công việc",
          "Tìm việc ở",
          "Văn hóa",
          "Ngoại ngữ",
          "Tin học",
          "Văn bằng",
          "Kinh nghiệm",
          "Tình trạng"
        ];
        const filterVal = [
          "NguoiLaoDong.HoTen",
          "NguoiLaoDong.GioiTinh",
          "NguoiLaoDong.NgaySinh",
          "NguoiLaoDong.SoDienThoai",
          "NguoiLaoDong.Email",
          "NguoiLaoDong.DiaChi",
          "NguoiLaoDong.DiaChiHuyen",
          "NguoiLaoDong.DiaChiTinh",
          "NganhNghe",
          "TenCongViec",
          "NoiLamViecHuyen",
          "VanHoa",
          "NgoaiNgu",
          "TinHoc",
          "VanBang",
          "KinhNghiem",
          "NguoiLaoDong.TinhTrang"
        ];
        const data = this.formatJson(filterVal, this.listData);
        console.log(data);
        var filename = "DSTimViec_" + moment().format("YYYYMMDD_hhmmss");
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
        selectHoSoTimViec().then(data => {
          this.listData = data;
          this.total = data.length;

          this.isXacThuc = true;
          this.loading = false;
        });
      }
      if (this.RoleId == 4) {
        //DoanhNghiep
        getHoSoTimViec(false)
          .then(response => {
            this.listData = response.data;
            this.total = response.data.length;

            this.isXacThuc = true;
            this.loading = false;
          })
          .catch(error => {
            this.$message.error(
              "Chưa cập nhật thông tin tài khoản doanh nghiệp hoặc chưa được xác nhận!"
            );
            this.loading = false;
          });
      }
    },

    updateData() {
      this.$refs.formData.validate(valid => {
        if (valid) {
          if (this.isEditor == 0) {
            //delete this.formData.LinhVuc;
            addHoSoTimViec(this.formData).then(data => {
              //console.log(data);
              this.getListData();
            });
          } else {
            //delete this.formData.LinhVuc;
            updateHoSoTimViec(this.formData).then(data => {
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
          post.NguoiLaoDong.HoTen.toLowerCase().includes(
            this.search.toLowerCase()
          ) ||
          post.TenCongViec.toLowerCase().includes(this.search.toLowerCase())
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
    getCauHinh("ChoPhepCapNhatDN").then(data => {
      if (data) {
        this.allowEdit = data[0].GiaTri == "1";
        console.log(this.allowEdit);
      }
    });

    this.getListData();
    getListDanhMucSVL().then(data => {
      if (data) {
        this.ListDMGioiTinh = data.DMGioiTinh;
        this.ListDMTinh = data.DMTinh;
        this.ListDMHuyen = data.DMHuyen;
        this.ListDMNganhNghe = data.DMNganhNghe;
        this.ListDMMucLuong = data.DMMucLuong;
        this.ListDMThoiGianLamViec = data.DMThoiGianLamViec;
        this.ListDMViTriCongViec = data.DMViTriCongViec;
        this.ListDMVanHoa = data.DMVanHoa;
        this.ListDMNgoaiNgu = data.DMNgoaiNgu;
        this.ListDMTinHoc = data.DMTinHoc;
        this.ListDMVanBang = data.DMVanBang;
      }
    });

    selectNguoiLaoDong().then(data => {
      if (data) {
        this.listNguoiLaoDong = data;
      }
    });
  }
};
</script>
<style></style>
