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
              <li class="breadcrumb-item active">Đổi mật khẩu</li>
            </ol>
          </div>
          <h4 class="page-title">Đổi mật khẩu</h4>
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
            class="w-50 m-auto"
          >
            <el-form-item label="Mật khẩu cũ" prop="PasswordOld">
              <el-input
                v-model="formData.PasswordOld"
                type="password"
                size="small"
              ></el-input>
            </el-form-item>
            <el-form-item label="Mật khẩu mới" prop="PasswordNew">
              <el-input
                v-model="formData.PasswordNew"
                type="password"
                size="small"
              ></el-input>
            </el-form-item>
            <el-form-item label="Nhập lại mật khẩu" prop="checkPass">
              <el-input
                v-model="formData.checkPass"
                type="password"
                size="small"
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
// import { getUser } from "../../store/common";
import { changePass } from "../../store/api";
const sha1 = require("js-sha1");
export default {
  data() {
    var validatePass = (rule, value, callback) => {
      if (value === "") {
        callback(new Error("Vui lòng nhập lại mật khẩu mới"));
      } else if (value !== this.formData.PasswordNew) {
        callback(new Error("Mật khẩu mới không giống nhau"));
      } else {
        callback();
      }
    };
    return {
      dialogFormDisplay: false,
      loading: false,
      isEditor: 0,
      search: "",
      formData: {
        PasswordOld: null,
        PasswordNew: null,
        checkPass: ""
      },
      Rules: {
        PasswordOld: [
          {
            required: true,
            message: "Vui lòng nhập mật khẩu cũ",
            trigger: "blur"
          }
        ],
        PasswordNew: [
          {
            required: true,
            message: "Vui lòng nhập mật khẩu mới",
            trigger: "blur"
          }
        ],
        checkPass: [
          {
            required: true,
            message: "Vui lòng nhập lại mật khẩu mới",
            trigger: "blur"
          },
          { validator: validatePass, trigger: "blur" }
        ]
      },
      kq: null
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
          changePass(
            sha1(this.formData.PasswordOld),
            sha1(this.formData.PasswordNew)
          ).then(data => {
            this.kq = data;
            if (this.kq == 1) {
              this.$message({
                type: "success",
                message: "Mật khẩu được đổi thành công."
              });
              this.resetForm();
            } else {
              this.$message({
                type: "warning",
                message: "Mật khẩu cũ không đúng, vui lòng kiểm tra lại"
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
    //this.listTypeFilter()
  }
};
</script>
<style></style>
