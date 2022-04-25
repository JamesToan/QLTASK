<template>
  <div class="left-side-menu">
    <div class="slimscroll-menu">
      <!--- Sidemenu -->
      <div id="sidebar-menu">
        <ul class="metismenu" id="side-menu">
          <template v-for="item in routes">
            <li class="mm-active"
              :key="item.name"
              v-if="item.children && item.meta.show == 'true'"
            >
              <a href="#" aria-expanded="true">
                <i :class="item.icon"></i>
                <span> {{ item.meta.display }} </span>
                <span class="menu-arrow"></span>
              </a>
              <ul class="nav-second-level" aria-expanded="true">
                <template v-for="item in item.children">
                  <li :key="item.name">
                    <router-link
                      v-if="item.meta.show == 'true'"
                      :to="{ name: item.name, params: item.params }"
                    >
                      {{ item.meta.display }}</router-link
                    >
                  </li>
                </template>
              </ul>
            </li>
            <li
              :key="item.name"
              v-if="!item.children && item.meta.show == 'true'"
            >
              <router-link :to="item.path">
                <i :class="item.icon" /><span>{{ item.meta.display }}</span>
              </router-link>
            </li>
          </template>
        </ul>
      </div>
      <!-- End Sidebar -->

      <div class="clearfix"></div>
    </div>
    <!-- Sidebar -left -->
  </div>
</template>

<script>
// import { routes } from "../router/routes";
import {
  getUser,
  removeUser,
  getRemain,
  getRole,
  removeToken
} from "../store/common";
import { getListPerRoleActive,  } from "../store/api";
import store from "../store";
import { routes } from "../router/routes";
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
      this.role = getRole();
      if (this.name != "admin@vnpt.vn") {
      //if (this.role == 3 || this.role == 2) {
        // this.routes = routes.filter(route => {
        //   return route.name != "Hethong";
        // });
        getListPerRoleActive(getRole()).then(data => {
          var per = data.map(r => r.Permission.Route);
          
          this.routes = routes.filter(route => {
            return per.includes(route.name);
          });
          //console.log(data);
          //console.log(this.routes);
          this.routes.forEach(route => {
            if (route.children) {
              route.children = route.children.filter(route => {
                return per.includes(route.name);
              });
            }
          });
        });
      } else {
      }
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
