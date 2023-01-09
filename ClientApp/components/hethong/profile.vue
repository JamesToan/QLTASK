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
          <el-form :model="formData1"
                   :rules="Rules"
                   ref="formData1"
                   label-width="120px"
                   class="m-auto"
                   style="width: 620px"
                   size="small">
            <el-form-item label="UserName" prop="UserName">
              <el-input v-model="formData1.UserName"
                        type="text"
                        size="small"
                        readonly></el-input>
            </el-form-item>
            <el-form-item label="Họ tên" prop="FullName">
              <el-input v-model="formData1.FullName"
                        type="text"
                        size="small"></el-input>
            </el-form-item>
            <el-form-item label="Số điện thoại" prop="Phone">
              <el-input v-model="formData1.Phone"
                        type="text"
                        size="small"></el-input>
            </el-form-item>
            <el-form-item label="Tên đăng nhập jira" prop="JiraAcount">
              <el-input v-model="formData1.JiraAcount"
                        type="text" 
                        size="small"
                        ></el-input>
            </el-form-item>
            <el-form-item label="Mật khẩu jira" prop="JiraPass">
              <el-input v-model="formData1.JiraPass"
                        type="password"
                        size="small" show-password></el-input>
            </el-form-item>
            
            <el-form-item label="Số CMND/CCCD"
                          prop="SoDinhDanh"
                          v-if="RoleId == 3">
              <el-input v-model="formData1.SoDinhDanh"
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
      formData1: {
        UserName: null,
        FullName: null,
        Phone: null,
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
      this.$refs.formData1.validate(valid => {
        if (valid) {
          updateProfile(this.formData1).then(
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
    this.formData1.JiraAcount = "";
    this.formData1.JiraPass = "";
    requestGetProfile(getUser()).then(data => {
      if (data) {
        this.formData1.UserName = getUser();
        this.formData1.FullName = data.FullName;
        this.formData1.Phone = data.Phone;
        this.formData1.JiraAcount = data.JiraAcount;
        this.formData1.JiraPass = data.JiraPass;
      }
    });
    
    
  }
};
</script>
<style></style>
