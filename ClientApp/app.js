import Vue from "vue";
import App from "components/app-root";
import router from "./router/index";
import store from "./store";
import { sync } from "vuex-router-sync";
sync(store, router);

import axios from "axios";
Vue.prototype.$http = axios;

import VueHighcharts from "vue-highcharts";
Vue.use(VueHighcharts);

import VueSlimScroll from "vue-slimscroll";
Vue.use(VueSlimScroll);

import "bootstrap";
import "element-ui/lib/theme-chalk/index.css";
import ElementUI from "element-ui";
import locale from "element-ui/lib/locale/lang/vi";
Vue.use(ElementUI, { locale });

import { FontAwesomeIcon } from "./icons";
Vue.component("icon", FontAwesomeIcon);

import TextHighlight from "vue-text-highlight";
Vue.component("text-highlight", TextHighlight);

import CKEditor from '@ckeditor/ckeditor5-vue2';
Vue.use( CKEditor );

//import * as VueGoogleMaps from 'vue2-google-maps'
//Vue.use(VueGoogleMaps, {
//  load: {
//    key: 'AIzaSyAIxFqHiyHTq9XdDnYTIThpJnpuFWC1-Zw',
//    libraries: 'places', // This is required if you use the Autocomplete plugin
//  }
//})
import firebase from "firebase";
Vue.config.productionTip = false;
const firebaseConfig = {
  apiKey: "AIzaSyCZSGn7t0WRbCd55ye7tjQlypByDOs7toQ",
  projectId: "auth-vnpt",
};
firebase.initializeApp(firebaseConfig);


import "./permission";
// Registration of global components
const app = new Vue({
  store,
  router,
  ...App
});

export { app, router, store };
