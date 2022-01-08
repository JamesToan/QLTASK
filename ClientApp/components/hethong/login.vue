<template>
  <div class="account-pages mt-5 pt-5 mb-5">
    <div class="container">
      <div class="row justify-content-center">
        <div class="col-md-8 col-lg-6 col-xl-5">
          <div class="card bg-pattern">
            <div class="card-body p-4">
              <div class="text-center w-75 m-auto">
                <a href="/">
                  <span><img src="/dist/logo.png" alt="" height="48"/></span>
                </a>
                <h5 class="text-uppercase text-center font-bold">Đăng nhập</h5>
              </div>

              <el-form
                role="form"
                action="/post"
                :model="form"
                ref="loginForm"
                :rules="rules"
                v-if="!loading"
              >
                <el-form-item prop="username">
                  <div class="form-group mb-1">
                    <label for="emailaddress">Email</label>
                    <input
                      class="form-control"
                      type="text"
                      v-model="form.username"
                      id="emailaddress"
                      required=""
                      placeholder="Nhập email"
                    />
                  </div>
                </el-form-item>
                <el-form-item prop="password">
                  <div class="form-group mb-1">
                    <label for="password">Mật khẩu</label>
                    <input
                      class="form-control"
                      type="password"
                      required=""
                      v-model="form.password"
                      id="password"
                      placeholder="Nhập mật khẩu"
                    />
                  </div>
                </el-form-item>
                <div class="form-group mb-0 text-center">
                  <button
                    class="btn btn-gradient btn-block"
                    type="submit"
                    v-on:click.prevent="login"
                  >
                    Đăng nhập
                  </button>
                </div>
              </el-form>
            </div>
            <!-- end card-body -->
          </div>
          <!-- end card -->
        </div>
        <!-- end col -->
      </div>
      <!-- end row -->
    </div>
    <!-- end container -->
  </div>
</template>

<script>
import { requestLogin } from "../../store/api";
import store from "../../store";
import { getToken, setToken, removeToken, setUser } from "../../store/common";
const sha1 = require("js-sha1");
import firebase from "firebase";

export default {
  name: "signup",
  data() {
    return {
      loading: false,
      form: {
        username: "",
        password: ""
      },
      rules: {
        username: [
          {
            required: true,
            message: "Vui lòng nhập email",
            trigger: "blur"
          }
        ],
        password: [
          { required: true, message: "Vui lòng nhập mật khẩu", trigger: "blur" }
        ]
      }
    };
  },
  methods: {
    login: function() {
      try {
        self = this;

        //if (this.username != '' && this.password != '') {
        this.$refs.loginForm.validate(valid => {
          if (valid) {
            this.loading = true;

            requestLogin(self.form.username, sha1(self.form.password)).then(
              data => {
                store.commit("SET_TOKEN", data.Key);
                if (!data.Key) {
                  this.loading = false;
                } else {
                  setToken(data.Key);
                  setUser(data.UserName);
                  this.$router.push("/admin/");
                }
              }
            );
            // firebase
            //   .auth()
            //   .signInWithEmailAndPassword(
            //     self.form.username,
            //     self.form.password
            //   )
            //   .then(userCredential => {
            //     userCredential.user.getIdToken().then(token => {
            //       setToken(token);
            //       setUser(userCredential.user.email);
            //       this.$router.push("/");
            //     });
            //   })
            //   .catch(error => {
            //     this.loading = false;
            //     console.log(error.message);
            //   });
          } else {
            return false;
          }
        });
      } catch (error) {
        //console.log(error)
      }
    },
    toggleBodyClass(addRemoveClass, className) {
      const el = document.body;

      if (addRemoveClass === "addClass") {
        el.classList.add(className);
      } else {
        el.classList.remove(className);
      }
    }
  },

  async created() {},
  mounted() {
    this.toggleBodyClass("addClass", "clsLogin");
  },
  destroyed() {
    this.toggleBodyClass("removeClass", "clsLogin");
  }
};
</script>
<style>
.clsLogin {
  background: url("../../assets/bg2.jpg");
  background-repeat: no-repeat;
  background-position: center center;
  background-size: cover;
  background-attachment: fixed;
}
.clsLogin footer {
  display: none;
}
.clsLogin .body-content {
  background: none !important;
  box-shadow: none !important;
}
.clsLogin .content-page {
  margin: 0px;
  padding: 0px;
}
.clsLogin .navbar-custom {
  display: none;
}
.clsLogin .left-side-menu {
  display: none;
}
.main-content {
  margin: 0 auto;
  width: 500px;
}
.pnLogin {
  margin: 0px auto;
  background: #f7fafc;
  padding: 20px;
  border: 1px solid #ccc !important;
}
.panel-title {
  font-size: 18px;
  font-weight: 400;
}
.card-header:not(.content-center) > .c-icon:first-child {
  margin-right: 0.1rem;
  margin-top: 0.1rem;
  vertical-align: top;
  width: 1.2rem;
  height: 1.2rem;
  font-size: 1.2rem;
}
/*.panel-heading {
    padding: 10px 15px;
    border-bottom: 1px solid transparent;
    border-top-left-radius: 3px;
    border-top-right-radius: 3px;
  }
  .panel-default > .panel-heading {
    color: #333;
    background-color: #f5f5f5;
    border-color: #ddd;
  }*/
</style>
