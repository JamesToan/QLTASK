import HomePage from "components/home-page";
import ParentCom from "components/parentcom";
import Login from "components/hethong/login";
import Signup from "components/hethong/signup";
import Changepass from "components/hethong/changepass";
import Profile from "components/hethong/profile";
//import tags from 'components/danhmuc/tags'
//import status from 'components/danhmuc/status'
import unit from "components/hethong/unit";
import role from "components/hethong/role";
import user from "components/hethong/user";
import permis from "components/hethong/phanquyen";
import permisrole from "components/hethong/phanquyenrole";


////danh muc

import dichvu from "components/danhmuc/dichvu";
import nhansu from "components/danhmuc/nhansu";
import state from "components/danhmuc/state";
import status from "components/danhmuc/status";
import donviyeucau from "components/danhmuc/donviyeucau";
import quanlydichvu from "components/danhmuc/quanlydichvu";
//// quan ly
import baocao from "components/thongke/baocao";
import yeucau from "components/quanly/yeucau";
import baocaodiaban from "components/thongke/baocaodiaban";
import thongkecanhan from "components/thongke/thongkecanhan";
import thongkediaban from "components/thongke/thongkediaban";
import thongkeloaiyc from "components/thongke/thongkeloaiyc";


export const routes = [
  {
    name: "HomePage",
    path: "/admin/",
    component: HomePage,
    icon: "fe-home",
    meta: {
      display: "Trang chủ",
      show: "true"
    }
  },
  {
    name: "HeThong",
    path: "/admin/",
    component: ParentCom,
    icon: "fe-settings",
    meta: {
      display: "Hệ thống",
      show: "true"
    },
    children: [
      {
        name: "role",
        path: "role",
        component: role,
        meta: {
          display: "Vai trò",
          show: "true"
        }
      },
      {
        name: "nguoidung",
        path: "nguoidung",
        component: user,
        meta: {
          display: "Người dùng",
          show: "true"
        }
      },
      {
        name: "phanquyen",
        path: "phanquyen",
        component: permis,
        meta: {
          display: "Phân quyền",
          show: "true"
        }
      },
      {
        name: "phanquyenrole",
        path: "phanquyenrole",
        component: permisrole,
        meta: {
          display: "Phân quyền vai trò",
          show: "true"
        }
      }
    ]
  },
  {
    name: "DanhMuc",
    path: "/admin/",
    component: ParentCom,
    icon: "fe-code",
    meta: {
      display: "Danh mục",
      show: "true"
    },
    children: [
      {
        name: "dichvu",
        path: "dichvu",
        component: dichvu,
        meta: {
          display: "Dịch vụ",
          show: "true"
        }
      },
      {
        name: "nhansu",
        path: "nhansu",
        component: nhansu,
        meta: {
          display: "Nhân sự",
          show: "true"
        }
      },
      {
        name: "quanlydichvu",
        path: "quanlydichvu",
        component: quanlydichvu,
        meta: {
          display: "Quản lý dịch vụ",
          show: "true"
        }
      },
      {
        name: "state",
        path: "state",
        component: state,
        meta: {
          display: "Trạng thái",
          show: "true"
        }
      },
      {
        name: "donviyeucau",
        path: "donviyeucau",
        component: donviyeucau,
        meta: {
          display: "Đơn vị yêu cầu",
          show: "true"
        }
      },
      
    ]
  },
  {
    name: "QuanLy",
    path: "/admin/",
    component: ParentCom,
    icon: "fe-list",
    meta: {
      display: "Quản lý yêu cầu",
      show: "true"
    },
    children: [
      {
        name: "yeucau",
        path: "yeucau",
        component: yeucau,
        meta: {
          display: "Tất cả yêu cầu",
          show: "true"
        }
      },
      {
        name: "yeucau1",
        path: "yeucau/:StateId",
        params: { StateId: 10},
        component: yeucau,
        meta: {
          display: "Chưa tiếp nhận",
          show: "true"
        }
      },
     
      {
        name: "yeucau3",
        path: "yeucau/:StateId",
        params: { StateId: 7 },
        component: yeucau,
        meta: {
          display: "Đang xử lý",
          show: "true"
        }
      },
      {
        name: "yeucau4",
        path: "yeucau/:StateId",
        params: { StateId: 6 },
        component: yeucau,
        meta: {
          display: "Đã hoàn thành",
          show: "true"
        }
      },
       {
        name: "yeucau2",
        path: "yeucau/:StateId",
        params: { StateId: 9 },
        component: yeucau,
        meta: {
          display: "Không xử lý",
          show: "true"
        }
      },
    ]
  },
  {
    name: "thongke",
    path: "/amin/",
    component: ParentCom,
    icon: "fe-file",
    meta: {
      display: "Thống kê",
      show: "true"
    },
    children: [
      {
        name: "baocao",
        path: "baocao",
        component: baocao,
        meta: {
          display: "Thống kê theo ngày",
          show: "true"
        }
      },
      {
        name: "baocaodiaban",
        path: "baocaodiaban",
        component: baocaodiaban,
        meta: {
          display: "Thống kê theo địa bàn",
          show: "true"
        }
      },
      {
        name: "thongkecanhan",
        path: "thongkecanhan",
        component: thongkecanhan,
        meta: {
          display: "Thống kê cá nhân",
          show: "true"
        }
      },
      {
        name: "thongkediaban",
        path: "thongkediaban",
        component: thongkediaban,
        meta: {
          display: "Thống kê địa bàn", 
          show: "true"
        }
      },
      {
        name: "thongkeloaiyc",
        path: "thongkeloaiyc",
        component: thongkeloaiyc,
        meta: {
          display: "Thống kê loại yêu cầu",
          show: "true"
        }
      },
    ]
  },


  {
    name: "Login",
    path: "/admin/login",
    component: Login,
    icon: "home",
    meta: {
      display: "Login",
      show: "false",
      loadMenu: false
    }
  },
  {
    name: "Signup",
    path: "/admin/signup",
    component: Signup,
    icon: "home",
    meta: {
      display: "Signup",
      show: "false",
      loadMenu: false
    }
  },
  {
    name: "Changepass",
    path: "/admin/changepass",
    component: Changepass,
    icon: "home",
    meta: {
      display: "Change Password",
      show: "false",
      loadMenu: true
    }
  },
  {
    name: "Profile",
    path: "/admin/profile",
    component: Profile,
    icon: "home",
    meta: {
      display: "Profile",
      show: "false",
      loadMenu: true
    }
  }
];
