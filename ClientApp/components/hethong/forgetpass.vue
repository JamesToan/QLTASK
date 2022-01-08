<template>
  <div style="padding-top:50px">
    <div class="row vertical-offset-100 ">
      <div class="col-md-4 col-md-offset-4 pnLogin">
        <div class="panel panel-default">
          <div class="panel-heading">
            <h3 class="panel-title">Reset Password</h3>
          </div>
          <div class="panel-body">
            <div v-if="loading" class="text-center">
              <p><em>Loading...</em></p>
              <h1><icon icon="spinner" pulse /></h1>
            </div>
            <el-form role="form" action="/post" :model="formLogin" ref="loginForm" :rules="loginRules" v-if="!loading">
              <el-form-item label="" prop="email" style="padding:2px">
                <el-input placeholder="Email" size="small" name="email" v-model="formLogin.email" type="text" />
              </el-form-item>
             
              <p></p>
              <input class="btn btn-sm btn-primary btn-block" type="submit" v-on:click.prevent="login" value="Send Password Reset Link">
            </el-form>

            <!--<Loading v-if="loading"></Loading>-->
          </div>
        </div>
      </div>
    </div>
  </div>
  
</template>
<script>
export default {
    name: 'forgetpass',
    data() {
      return {
        loading: false,
        formLogin: {
          email: "",
          password: "",
        },
        loginRules: {
          email: [
            { required: true, message: "Please input your email ", trigger: 'blur' },
            { type: 'email', message: 'Please input correct email address', trigger: ['blur', 'change'] }]
        },
      }
    },
    methods: {
        
        //login: function () {
        //    try {
        //        self = this;

        //        //if (this.username != '' && this.password != '') {
        //      this.$refs.loginForm.validate(valid => {
        //        if (valid) {
        //          this.loading = true;

        //          requestLogin(self.formLogin.username, sha1(self.formLogin.password)).then(data => {
        //            store.commit('SET_TOKEN', data.key)
        //            if (!data.key) {
        //              this.loading = false;
        //            }
        //            else {
        //              setToken(data.key)
        //              setUser(data.UserName)
        //              this.$router.push('/');
        //            }
        //          });
        //        }
        //        else {
        //          return false;
        //        }
        //      });
        //    } catch (error) {
        //        //console.log(error)
        //    }
        //},
        toggleBodyClass(addRemoveClass, className) {
          const el = document.body;

          if (addRemoveClass === 'addClass') {
            el.classList.add(className);
          } else {
            el.classList.remove(className);
          }
        },
    },

    async created() {
    },
    mounted() {
      //this.toggleBodyClass('addClass', 'clsLogin');
    },
    destroyed() {
      //this.toggleBodyClass('removeClass', 'clsLogin');
    }
}
</script>
<style>
  .main-content {
    margin: 0 auto;
    width: 500px;
  }
  .pnLogin {
    margin: 0px auto;
    background: #f7fafc;
    padding: 20px;
    border: 1px solid #e4eaf0;
  }
  .panel-title{
    font-size:18px;
    font-weight:400;
  }
</style>
