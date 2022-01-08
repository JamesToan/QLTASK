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
              <li class="breadcrumb-item active">Thông tin tài khoản</li>
            </ol>
          </div>
          <h4 class="page-title">Thông tin tài khoản</h4>
        </div>
      </div>
    </div>
    <!-- end page title -->
    <div class="row">
      <div class="col-12">
        <div class="card-box table-responsive text-center">
          <el-form
            :model="formData"
            :rules="Rules"
            ref="formData"
            label-width="120px"
            class="m-auto"
            style="width: 620px"
            size="small"
          >
            <el-form-item label="Email" prop="UserName">
              <el-input
                v-model="formData.UserName"
                type="text"
                size="small"
                readonly
              ></el-input>
            </el-form-item>
            <el-form-item label="Họ tên" prop="HoTen">
              <el-input
                v-model="formData.HoTen"
                type="text"
                size="small"
              ></el-input>
            </el-form-item>
            <el-form-item label="Số điện thoại" prop="SoDienThoai">
              <el-input
                v-model="formData.SoDienThoai"
                type="text"
                size="small"
              ></el-input>
            </el-form-item>
            <el-form-item
              label="Giới tính"
              prop="GioiTinhId"
              v-if="RoleId == 3"
            >
              <el-col :span="10">
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
              </el-col>
              <el-col :span="4" class="text-right pr-1"
                ><label>Ngày sinh</label></el-col
              >
              <el-col :span="10">
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
              </el-col>
            </el-form-item>
            <el-form-item
              label="Số CMND/CCCD"
              prop="SoDinhDanh"
              v-if="RoleId == 3"
            >
              <el-input
                v-model="formData.SoDinhDanh"
                type="text"
                size="small"
              ></el-input>
            </el-form-item>
            <el-form-item
              label="Doanh nghiệp"
              prop="TenDoanhNghiep"
              v-if="RoleId == 4"
            >
              <el-input
                v-model="formData.TenDoanhNghiep"
                type="text"
                size="small"
              ></el-input>
            </el-form-item>
            <el-form-item
              label="Mã số thuế"
              prop="MaSoThue"
              v-if="RoleId == 4"
            >
              <el-input
                v-model="formData.MaSoThue"
                type="text"
                size="small"
              ></el-input>
            </el-form-item>
            <el-form-item
              label="Ngành kinh tế"
              prop="NganhKinhTeId"
              v-if="RoleId == 4"
            >
              <el-select
                v-model="formData.NganhKinhTeId"
                placeholder="Chọn ngành kinh tế"
                class="w-100"
              >
                <el-option
                  v-for="item in ListDMNganhKinhTeId"
                  :key="item.Id"
                  :label="item.TenNganhKinhTe"
                  :value="item.Id"
                >
                </el-option>
              </el-select>
            </el-form-item>
            <el-form-item
              label="Loại hình"
              prop="LoaiHinhId"
              v-if="RoleId == 4"
            >
              <el-col :span="10">
                <el-select
                  v-model="formData.LoaiHinhId"
                  placeholder="Chọn loại hình"
                  class="w-100"
                >
                  <el-option
                    v-for="item in ListDMLoaiHinh"
                    :key="item.Id"
                    :label="item.TenLoaiHinh"
                    :value="item.Id"
                  >
                  </el-option>
                </el-select>
              </el-col>
              <el-col :span="4" class="text-right pr-0"
                ><label>Số lao động</label></el-col
              >
              <el-col :span="10">
                <el-input
                  v-model="formData.TongSoLaoDong"
                  type="number"
                  size="small"
                  placeholder="Tổng số lao động"
                ></el-input>
              </el-col>
            </el-form-item>
            <el-form-item label="Địa chỉ" prop="DiaChi">
              <el-col :span="10">
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
              </el-col>
              <el-col :span="4" class="text-right pr-2">&nbsp;</el-col>
              <el-col :span="10">
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
              </el-col>
              <el-input
                v-model="formData.DiaChi"
                type="text"
                size="small"
                class="pt-2"
              ></el-input>
            </el-form-item> </el-form
          ><span slot="footer" class="dialog-footer">
            <el-button @click="handleCancel" size="small">Bỏ qua</el-button>
            <el-button type="primary" size="small" @click="updateData"
              >Cập nhật</el-button
            >
          </span>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { getRole, getUser } from "../../store/common";
import {
  requestGetProfile,
  getListDanhMucSVL,
  updateProfile,
  getNguoiLaoDong,
  updateNguoiLaoDong,
  getDoanhNghiep,
  updateDoanhNghiep
} from "../../store/api";
export default {
  data() {
    return {
      dialogFormDisplay: false,
      loading: false,
      isEditor: 0,
      search: null,
      formData: {
        UserName: null,
        HoTen: null,
        SoDienThoai: null,
        Email: null,
        GioiTinhId: 1,
        NgaySinh: null,
        SoDinhDanh: null,
        TenDoanhNghiep: null,
        MaSoThue: null,
        NganhKinhTeId: null,
        LoaiHinhId: null,
        TongSoLaoDong: null,
        DiaChi: null,
        DiaChiTinhId: "80",
        DiaChiHuyenId: null
      },
      Rules: {
        UserName: [
          {
            required: true,
            message: "Vui lòng nhập email",
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
        SoDienThoai: [
          {
            required: true,
            message: "Vui lòng nhập số điện thoại",
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
        TenDoanhNghiep: [
          {
            required: true,
            message: "Vui lòng nhập tên doanh nghiệp",
            trigger: "blur"
          }
        ],
        MaSoThue: [
          {
            required: true,
            message: "Vui lòng nhập mã số thuế",
            trigger: "blur"
          }
        ]
      },
      RoleId: getRole(),
      ListDMTinh: [],
      ListDMHuyen: [],
      ListDMGioiTinh: [],
      ListDMNganhKinhTe: [],
      ListDMLoaiHinh: []
    };
  },

  computed: {
    //...mapState({
    //  currentCount: state => state.counter
    //})
  },

  methods: {
    handleCancel() {
      window.history.go(-1);
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
    updateData() {
      this.$refs.formData.validate(valid => {
        if (valid) {
          updateProfile(this.formData.HoTen, this.formData.SoDienThoai).then(
            data => {
              if (data == 1) {
                //NguoiLaoDong
                if (this.RoleId == 3) {
                  this.formData.Email = this.formData.UserName;
                  updateNguoiLaoDong(this.formData).then(data => {
                    if (data > 0) {
                      this.$message({
                        type: "success",
                        message: "Thông tin tài khoản đổi thành công."
                      });
                    }
                  });
                }
                //DoanhNghiep
                if (this.RoleId == 4) {
                  this.formData.Email = this.formData.UserName;
                  updateDoanhNghiep(this.formData).then(data => {
                    if (data > 0) {
                      this.$message({
                        type: "success",
                        message: "Thông tin tài khoản đổi thành công."
                      });
                    }
                  });
                }
              } else {
                this.$message({
                  type: "warning",
                  message: "Có lỗi xảy ra!"
                });
              }
            }
          );
        } else {
          return false;
        }
      });
    }
  },

  created() {
    getListDanhMucSVL().then(data => {
      if (data) {
        this.ListDMTinh = data.DMTinh;
        this.ListDMHuyen = data.DMHuyen;
        this.ListDMGioiTinh = data.DMGioiTinh;
        this.ListDMNganhKinhTe = data.DMNganhKinhTe;
        this.ListDMLoaiHinh = data.DMLoaiHinh;
      }
    });
    requestGetProfile(getUser()).then(data => {
      if (data) {
        this.formData.UserName = getUser();
        this.formData.HoTen = data.FullName;
        this.formData.SoDienThoai = data.Phone;
      }
    });
    //NguoiLaoDong
    if (this.RoleId == 3) {
      getNguoiLaoDong().then(data => {
        if (data) {
          this.formData.GioiTinhId = data.GioiTinhId;
          this.formData.NgaySinh = data.NgaySinh;
          this.formData.SoDinhDanh = data.SoDinhDanh;
          this.formData.DiaChi = data.DiaChi;
          this.formData.DiaChiTinhId = data.DiaChiTinhId;
          this.formData.DiaChiHuyenId = data.DiaChiHuyenId;
        }
      });
    }
    //DoanhNghiep
    if (this.RoleId == 4) {
      getDoanhNghiep().then(data => {
        if (data) {
          this.formData.TenDoanhNghiep = data.TenDoanhNghiep;
          this.formData.MaSoThue = data.MaSoThue;
          this.formData.NganhKinhTeId = data.NganhKinhTeId;
          this.formData.LoaiHinhId = data.LoaiHinhId;
          this.formData.TongSoLaoDong = data.TongSoLaoDong;
          this.formData.DiaChi = data.DiaChi;
          this.formData.DiaChiTinhId = data.DiaChiTinhId;
          this.formData.DiaChiHuyenId = data.DiaChiHuyenId;
        }
      });
    }
  }
};
</script>
<style></style>
