import router from "./router";
import store from "./store";
import { getToken, removeToken, setRole } from "./store/common"; // getToken from cookie
import { requestGetInfo, requestGetProfile } from "./store/api";
import firebase from "firebase";
const whiteList = ["/admin/login", "/admin/signup"]; // no redirect whitelist
const mainTitle = "Sàn việc làm";
router.beforeEach((to, from, next) => {
  //console.log(getToken() === undefined)
  //console.log(getToken());
  if (getToken() === undefined) {
    if (whiteList.indexOf(to.path) !== -1) {
      document.title = mainTitle + " - " + to.meta.display;
      next();
    } else {
      next("/admin/login");
    }
  } else {
    if (store.state.name == "") {
      // firebase.auth().onAuthStateChanged(user => {
      //   if (user) {
      //     // User is signed in, see docs for a list of available properties
      //     // https://firebase.google.com/docs/reference/js/firebase.User
      //     requestGetProfile(user.email).then(data => {
      //       if (data !== undefined) {
      //         store.commit("SET_NAME", data.UserName);
      //         store.commit("SET_ROLE", data.RoleId);
      //         setRole(data.RoleId);
      //         document.title = mainTitle + " - " + to.meta.display;
      //         next();
      //       }
      //     });
      //   } else {
      //     // User is signed out
      //     removeToken();
      //     next("/admin/login");
      //   }
      // });
      requestGetInfo().then(data => {
        if (data !== undefined) {
          store.commit("SET_NAME", data.UserName);
          store.commit("SET_ROLE", data.RoleId);
          setRole(data.RoleId);
          document.title = mainTitle + " - " + to.meta.display;
          next();
        } else {
          removeToken();
          next("/admin/login");
        }
      });
    } else {
      document.title = mainTitle + " - " + to.meta.display;
      next();
    }
  }
});
