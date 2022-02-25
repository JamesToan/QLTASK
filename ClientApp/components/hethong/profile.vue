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
          <el-form :model="formData"
                   :rules="Rules"
                   ref="formData"
                   label-width="120px"
                   class="m-auto"
                   style="width: 620px"
                   size="small">
            <el-form-item label="Email" prop="UserName">
              <el-input v-model="formData.UserName"
                        type="text"
                        size="small"
                        readonly></el-input>
            </el-form-item>
            <el-form-item label="Họ tên" prop="HoTen">
              <el-input v-model="formData.HoTen"
                        type="text"
                        size="small"></el-input>
            </el-form-item>
            <el-form-item label="Số điện thoại" prop="SoDienThoai">
              <el-input v-model="formData.SoDienThoai"
                        type="text"
                        size="small"></el-input>
            </el-form-item>
            <el-form-item label="Tên đăng nhập jira" prop="JiraAcount">
              <el-input v-model="formData.JiraAcount"
                        type="text"
                        size="small"></el-input>
            </el-form-item>
            <el-form-item label="Mật khẩu jira" prop="JiraPass">
              <el-input v-model="formData.JiraPass"
                        type="password"
                        size="small" show-password></el-input>
            </el-form-item>
            
            <el-form-item label="Số CMND/CCCD"
                          prop="SoDinhDanh"
                          v-if="RoleId == 3">
              <el-input v-model="formData.SoDinhDanh"
                        type="text"
                        size="small"></el-input>
            </el-form-item>
            
            
            
          </el-form><span slot="footer" class="dialog-footer">
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
  updateProfile, 
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
        JiraAcount: null,
        JiraPass: null,
        
       
      },
      Rules: {
        
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
        JiraAcount: [
          {
            required: true,
            message: "Vui lòng chọn giới tính",
            trigger: "blur"
          }
        ],
        JiraPass: [
          {
            required: true,
            message: "Vui lòng nhập tên doanh nghiệp",
            trigger: "blur"
          }
        ],
        
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
    
    updateData() {
      this.$refs.formData.validate(valid => {
        if (valid) {
          updateProfile(this.formData.HoTen, this.formData.SoDienThoai,this.formData.JiraAcount, this.formData.JiraPass).then(
            data => {
              if (data == 1) {
                
                this.$message({
                  type: "success",
                  message: "Thông tin tài khoản đổi thành công."
                });
                
                
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
        this.formData.JiraAcount = data.JiraAcount;
        this.formData.JiraPass = data.JiraPass;
      }
    });
    
    
  }
};
</script>
<style></style>
