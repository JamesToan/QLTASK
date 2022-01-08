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
          <h4 class="page-title">Hồ sơ tìm việc</h4>
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
            label-width="140px"
            class="m-auto"
            style="width: 620px"
            size="small"
          >
            <el-divider content-position="left"
              ><b
                ><i class="el-icon-star-on" /><i>Nguyện vọng công việc</i></b
              ></el-divider
            >
            <el-form-item label="Tìm công việc" prop="TenCongViec">
              <el-input
                v-model="formData.TenCongViec"
                type="text"
                size="small"
                placeholder="Tên công việc mong muốn"
              ></el-input>
            </el-form-item>
            <el-form-item label="Vị trí công việc" prop="ViTriCongViecId">
              <el-select
                v-model="formData.ViTriCongViecId"
                placeholder="Chọn vị trí công việc"
                class="w-100"
                clearable
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
            <el-form-item label="Ngành nghề công ty" prop="NganhNgheId">
              <el-select
                v-model="formData.NganhNgheId"
                placeholder="Chọn ngành nghề"
                class="w-100"
                clearable
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
            <el-form-item label="Loại hình công ty" prop="LoaiHinhId">
              <el-col :span="10"
                ><el-select
                  v-model="formData.LoaiHinhId"
                  placeholder="Chọn loại hình"
                  class="w-100"
                  clearable
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
              <el-col :span="4" class="text-right pr-1"
                ><label>Thời gian</label></el-col
              >
              <el-col :span="10"
                ><el-select
                  v-model="formData.ThoiGianLamViecId"
                  placeholder="Chọn thời gian làm việc"
                  class="w-100"
                  clearable
                >
                  <el-option
                    v-for="item in ListDMThoiGianLamViec"
                    :key="item.Id"
                    :label="item.TenThoiGianLamViec"
                    :value="item.Id"
                  >
                  </el-option>
                </el-select>
              </el-col>
            </el-form-item>
            <el-form-item label="Mức lương từ" prop="MucLuongTu">
              <el-col :span="10">
                <el-input
                  v-model="formData.MucLuongTu"
                  type="number"
                  size="small"
                  placeholder="(triệu)"
                ></el-input>
              </el-col>
              <el-col :span="4" class="text-right pr-1"
                ><label>đến</label></el-col
              >
              <el-col :span="10">
                <el-input
                  v-model="formData.MucLuongDen"
                  type="number"
                  size="small"
                  placeholder="(triệu)"
                ></el-input>
              </el-col>
            </el-form-item>
            <el-form-item label="Nơi làm việc" prop="NoiLamViecTinhId">
              <el-col :span="10">
                <el-select
                  v-model="formData.NoiLamViecTinhId"
                  placeholder="Chọn tỉnh"
                  class="w-100"
                  clearable
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
              <el-col :span="4" class="text-right pr-1"><label></label></el-col>
              <el-col :span="10">
                <el-input
                  v-model="formData.NoiLamViec"
                  type="text"
                  size="small"
                  placeholder="Nơi làm việc mong muốn"
                ></el-input>
              </el-col>
            </el-form-item>
            <el-divider content-position="left"
              ><b
                ><i class="el-icon-star-on" /><i class="el-icon-star-on" /><i
                  >Trình độ chuyên môn</i
                ></b
              ></el-divider
            >
            <el-form-item label="Văn bằng" prop="VanBangId">
              <el-col :span="10">
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
              </el-col>
              <el-col :span="4" class="text-right pr-0"
                ><label>Kinh nghiệm</label>
              </el-col>
              <el-col :span="10">
                <el-input
                  v-model="formData.KinhNghiem"
                  type="text"
                  size="small"
                ></el-input>
              </el-col>
            </el-form-item>
            <el-form-item label="Chuyên ngành" prop="ChuyenNganh">
              <el-input
                v-model="formData.ChuyenNganh"
                type="text"
                size="small"
              ></el-input>
            </el-form-item>
            <el-form-item label="Ngoại ngữ" prop="NgoaiNguId">
              <el-col :span="10">
                <el-select
                  v-model="formData.NgoaiNguId"
                  placeholder="Chọn chứng chỉ"
                  class="w-100"
                  clearable
                >
                  <el-option
                    v-for="item in ListDMNgoaiNgu"
                    :key="item.Id"
                    :label="item.TenChungChi"
                    :value="item.Id"
                  >
                  </el-option>
                </el-select>
              </el-col>
              <el-col :span="4" class="text-right pr-1"
                ><label>Tin học</label>
              </el-col>
              <el-col :span="10">
                <el-select
                  v-model="formData.TinHocId"
                  placeholder="Chọn chứng chỉ"
                  class="w-100"
                  clearable
                >
                  <el-option
                    v-for="item in ListDMTinHoc"
                    :key="item.Id"
                    :label="item.TenChungChi"
                    :value="item.Id"
                  >
                  </el-option>
                </el-select>
              </el-col>
            </el-form-item>
            <el-form-item label="Kỹ năng khác" prop="KyNang">
              <el-input
                v-model="formData.KyNang"
                type="text"
                size="small"
              ></el-input>
            </el-form-item>
            <el-divider content-position="left"
              ><b
                ><i class="el-icon-star-on" /><i class="el-icon-star-on" /><i
                  class="el-icon-star-on"
                /><i>Mô tả bản thân</i></b
              ></el-divider
            >
            <el-form-item label="Ưu điểm" prop="UuDiem">
              <el-input
                v-model="formData.UuDiem"
                type="textarea"
                :rows="3"
                size="small"
              ></el-input>
            </el-form-item>
            <el-form-item label="Khuyết điểm" prop="KhuyetDiem">
              <el-input
                v-model="formData.KhuyetDiem"
                type="textarea"
                :rows="3"
                size="small"
              ></el-input>
            </el-form-item> </el-form
          ><span slot="footer" class="dialog-footer">
            <el-button @click="handleCancel" size="small">Bỏ qua</el-button>
            <el-button
              type="primary"
              size="small"
              @click="updateData"
              v-if="isXacThuc"
              >Cập nhật</el-button
            >
          </span>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import {
  getListDanhMucSVL,
  getHoSoTimViec,
  updateHoSoTimViec
} from "../../store/api";
export default {
  data() {
    return {
      dialogFormDisplay: false,
      loading: false,
      isEditor: false,
      isXacThuc: false,
      search: null,
      formData: {
        ViTriCongViecId: null,
        TenCongViec: null,
        NganhNgheId: null,
        LoaiHinhId: null,
        MucLuongTu: null,
        MucLuongDen: null,
        NoiLamViec: null,
        NoiLamViecTinhId: null,
        ThoiGianLamViecId: null,
        NgayBatDau: null,
        VanBangId: null,
        ChuyenNganh: null,
        KinhNghiem: null,
        NgoaiNguId: null,
        TinHocId: null,
        KyNang: null,
        UuDiem: null,
        KhuyetDiem: null
      },
      Rules: {
        TenCongViec: [
          {
            required: true,
            message: "Vui lòng nhập tên công việc",
            trigger: "blur"
          }
        ]
      },
      ListDMTinh: [],
      ListDMNganhNghe: [],
      ListDMLoaiHinh: [],
      ListDMThoiGianLamViec: [],
      ListDMNgoaiNgu: [],
      ListDMTinHoc: [],
      ListDMVanBang: [],
      ListDMViTriCongViec: []
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
    handleCancel() {
      window.history.go(-1);
    },
    updateData() {
      this.$refs.formData.validate(valid => {
        if (valid) {
          updateHoSoTimViec(this.formData).then(data => {
            if (data > 0) {
              this.$message({
                type: "success",
                message: "Thông tin hồ sơ tìm việc thay đổi thành công."
              });
              //this.resetForm();
            } else {
              this.$message({
                type: "warning",
                message: "Có lỗi xảy ra!"
              });
            }
          });
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
        this.ListDMNganhNghe = data.DMNganhNghe;
        this.ListDMLoaiHinh = data.DMLoaiHinh;
        this.ListDMThoiGianLamViec = data.DMThoiGianLamViec;
        this.ListDMNgoaiNgu = data.DMNgoaiNgu;
        this.ListDMTinHoc = data.DMTinHoc;
        this.ListDMVanBang = data.DMVanBang;
        this.ListDMViTriCongViec = data.DMViTriCongViec;
      }
    });

    getHoSoTimViec()
      .then(reponse => {
        //console.log(reponse.data);
        if (reponse.data) {
          this.formData = Object.assign({}, reponse.data);
        }

        this.isXacThuc = true;
      })
      .catch(error => {
        this.$message.error("Chưa cập nhật thông tin tài khoản!");
      });
  }
};
</script>
<style></style>
