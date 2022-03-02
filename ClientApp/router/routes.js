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

import yeucau from "components/quanly/yeucau";


//import dmChuyenMuc from "components/danhmuc/dmChuyenMuc";
//import dmLinhVuc from "components/danhmuc/dmLinhVuc";
//import dmLoaiVanBan from "components/danhmuc/dmLoaiVanBan";
//import dmNganhNghe from "components/danhmuc/dmNganhNghe";
//import dmLoaiHinh from "components/danhmuc/dmLoaiHinh";
//import dmViTriCongViec from "components/danhmuc/dmViTriCongViec";
//// import dmGioiTinh from "components/danhmuc/dmGioiTinh";
//import dmNgoaiNgu from "components/danhmuc/dmNgoaiNgu";
//import dmTinHoc from "components/danhmuc/dmTinHoc";
//import dmVanBang from "components/danhmuc/dmVanBang";
//import dmDaoTaoNghe from "components/danhmuc/dmDaoTaoNghe";
////Quản lý
//import newsMenu from "components/quanly/newsMenu";
//import newsBanTin from "components/quanly/newsBanTin";
//import newsHoiDap from "components/quanly/newsHoiDap";
//import newsVanBan from "components/quanly/newsVanBan";
//import newsBanner from "components/quanly/newsBanner";
//import newsVideo from "components/quanly/newsVideo";
//import hsTimViec from "components/quanly/hsTimViec";
//import hsTuyenDung from "components/quanly/hsTuyenDung";
//import qlDoanhNghiep from "components/quanly/qlDoanhNghiep";
//import qlNguoiLaoDong from "components/quanly/qlNguoiLaoDong";
//import qlSanViecLam from "components/quanly/qlSanViecLam";
//import qlDangKyHocNghe from "components/quanly/qlDangKyHocNghe";
//import qlDangKyViecLam from "components/quanly/qlDangKyViecLam";
//import qlCauHinh from "components/quanly/qlCauHinh";
//Thống kê
// import tkThanhKiemTra from "components/thongke/tkThanhKiemTra";
// import tkKiemDinh from "components/thongke/tkKiemDinh";

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
          display: "Yêu cầu mới tạo",
          show: "true"
        }
      },
      {
        name: "yeucau2",
        path: "yeucau/:StateId",
        params: { StateId: 9 },
        component: yeucau,
        meta: {
          display: "Yêu cầu chưa xử lý",
          show: "true"
        }
      },
      {
        name: "yeucau3",
        path: "yeucau/:StateId",
        params: { StateId: 7 },
        component: yeucau,
        meta: {
          display: "Yêu cầu đang xử lý",
          show: "true"
        }
      },
      {
        name: "yeucau4",
        path: "yeucau/:StateId",
        params: { StateId: 6 },
        component: yeucau,
        meta: {
          display: "Yêu cầu đã hoàn thành",
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
