<template>
  <div class="navbar-custom">
    <ul class="list-unstyled topnav-menu float-right mb-0">
      <li class="dropdown notification-list">
        <a
          class="nav-link dropdown-toggle nav-user mr-0 waves-effect waves-light"
          data-toggle="dropdown"
          href="#"
          role="button"
          aria-haspopup="false"
          aria-expanded="false"
        >
          <img src="/dist/avatar.png" alt="user-image" class="rounded-circle" />
          <span class="ml-1">
            Xin chào {{ nameDisplay }} <i class="mdi mdi-chevron-down"></i>
          </span>
        </a>
        <div class="dropdown-menu dropdown-menu-right profile-dropdown ">
          <!-- item-->
          <!-- <div class="dropdown-header noti-title">
            <h6 class="text-overflow m-0">Welcome !</h6>
          </div> -->

          <!-- item-->
          <a
            href="javascript:void(0);"
            @click="profile"
            class="dropdown-item notify-item"
          >
            <i class="fe-user"></i>
            <span>Thông tin</span>
          </a>

          <!-- item-->
          <a
            href="javascript:void(0);"
            @click="changepass"
            class="dropdown-item notify-item"
          >
            <i class="fe-settings"></i>
            <span>Đổi mật khẩu</span>
          </a>

          <!-- item-->
          <!-- <a href="javascript:void(0);" class="dropdown-item notify-item">
            <i class="fe-lock"></i>
            <span>Lock Screen</span>
          </a> -->

          <div class="dropdown-divider"></div>

          <!-- item-->
          <a
            href="javascript:void(0);"
            @click="logout"
            class="dropdown-item notify-item"
          >
            <i class="fe-log-out"></i>
            <span>Đăng xuất</span>
          </a>
        </div>
      </li>
    </ul>

    <!-- LOGO -->
    <div class="logo-box">
      <router-link class="logo text-center" to="/admin/">
        <!--<a href="index.html" class="logo text-center">-->
        <span class="logo-lg">
          <img src="/dist/logo_header.png" alt="" height="64" />
          <!-- <span class="logo-lg-text-light">UBold</span> -->
        </span>
        <span class="logo-sm">
          <!-- <span class="logo-sm-text-dark">U</span> -->
          <img src="/dist/logo-sm2.png" alt="" height="28" />
        </span>
        <!--</a>-->
      </router-link>
    </div>

    <ul class="list-unstyled topnav-menu topnav-menu-left m-0">
      <li>
        <button class="button-menu-mobile waves-effect waves-light">
          <i class="fe-menu"></i>
        </button>
      </li>

      <li class="d-none d-sm-block">
        <!-- <form class="app-search">
          <div class="app-search-box">
            <div class="input-group">
              <input type="text" class="form-control" placeholder="Search...">
              <div class="input-group-append">
                <button class="btn" type="submit">
                  <i class="fe-search"></i>
                </button>
              </div>
            </div>
          </div>
        </form> -->
      </li>
    </ul>
  </div>
</template>

<script>
import { routes } from "../router/routes";
import { getUser, getRemain, setRemain } from "../store/common";
import { removeToken, removeUser, baseUrl } from "../store/common";
import store from "../store";
export default {
  data() {
    return {
      routes,
      collapsed: true,
      name: null,
      remain: null,
      nameDisplay: getUser()
    };
  },
  methods: {
    toggleCollapsed: function(event) {
      this.collapsed = !this.collapsed;
    },
    profile: function(event) {
      this.$router.push("/admin/profile");
    },
    changepass: function(event) {
      this.$router.push("/admin/changepass");
    },
    logout: function(event) {
      store.commit("SET_TOKEN", "");
      store.commit("SET_NAME", "");
      removeToken();
      removeUser();
      this.$router.push("/admin/login");
    }
  },
  created() {
    if (typeof getUser() != "undefined") {
      this.name = getUser();
    } else {
      this.name = null;
    }
    if (typeof getRemain() != "undefined") {
      this.remain = getRemain();
    } else {
      this.remain = null;
    }
  },
  mounted: function() {
    this.$root.$on("updateName", text => {
      // here you need to use the arrow function
      this.name = text;
    });
    this.$root.$on("updateRemain", text => {
      // here you need to use the arrow function
      this.remain = text;
    });
    this.$root.$on("updateTime", () => {
      // here you need to use the arrow function
    });
  }
};
</script>

<style scoped>
.ml-auto .dropdown-menu {
  left: auto !important;
  right: 0px;
}
</style>
