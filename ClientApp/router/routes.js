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

//danh muc
import dmChuyenMuc from "components/danhmuc/dmChuyenMuc";
import dmLinhVuc from "components/danhmuc/dmLinhVuc";
import dmLoaiVanBan from "components/danhmuc/dmLoaiVanBan";
import dmNganhNghe from "components/danhmuc/dmNganhNghe";
import dmLoaiHinh from "components/danhmuc/dmLoaiHinh";
import dmViTriCongViec from "components/danhmuc/dmViTriCongViec";
// import dmGioiTinh from "components/danhmuc/dmGioiTinh";
import dmNgoaiNgu from "components/danhmuc/dmNgoaiNgu";
import dmTinHoc from "components/danhmuc/dmTinHoc";
import dmVanBang from "components/danhmuc/dmVanBang";
import dmDaoTaoNghe from "components/danhmuc/dmDaoTaoNghe";
//Quản lý
import newsMenu from "components/quanly/newsMenu";
import newsBanTin from "components/quanly/newsBanTin";
import newsHoiDap from "components/quanly/newsHoiDap";
import newsVanBan from "components/quanly/newsVanBan";
import newsBanner from "components/quanly/newsBanner";
import newsVideo from "components/quanly/newsVideo";
import hsTimViec from "components/quanly/hsTimViec";
import hsTuyenDung from "components/quanly/hsTuyenDung";
import qlDoanhNghiep from "components/quanly/qlDoanhNghiep";
import qlNguoiLaoDong from "components/quanly/qlNguoiLaoDong";
import qlSanViecLam from "components/quanly/qlSanViecLam";
import qlDangKyHocNghe from "components/quanly/qlDangKyHocNghe";
import qlDangKyViecLam from "components/quanly/qlDangKyViecLam";
import qlCauHinh from "components/quanly/qlCauHinh";
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
        name: "dmNganhNghe",
        path: "dmNganhNghe",
        component: dmNganhNghe,
        meta: {
          display: "Ngành nghề",
          show: "true"
        }
      },
      {
        name: "dmViTriCongViec",
        path: "dmViTriCongViec",
        component: dmViTriCongViec,
        meta: {
          display: "Vị trí công việc",
          show: "true"
        }
      },
      {
        name: "dmNgoaiNgu",
        path: "dmNgoaiNgu",
        component: dmNgoaiNgu,
        meta: {
          display: "Ngoại ngữ",
          show: "true"
        }
      },
      {
        name: "dmTinHoc",
        path: "dmTinHoc",
        component: dmTinHoc,
        meta: {
          display: "Tin học",
          show: "true"
        }
      },
      {
        name: "dmVanBang",
        path: "dmVanBang",
        component: dmVanBang,
        meta: {
          display: "Văn bằng",
          show: "true"
        }
      },
      {
        name: "dmDaoTaoNghe",
        path: "dmDaoTaoNghe",
        component: dmDaoTaoNghe,
        meta: {
          display: "Đào tạo nghề",
          show: "true"
        }
      }
    ]
  },
  {
    name: "Website",
    path: "/admin/",
    component: ParentCom,
    icon: "fe-list",
    meta: {
      display: "Website",
      show: "true"
    },
    children: [
      {
        name: "dmChuyenMuc",
        path: "dmChuyenMuc",
        component: dmChuyenMuc,
        meta: {
          display: "Chuyên mục",
          show: "true"
        }
      },
      {
        name: "newsMenu",
        path: "newsMenu",
        component: newsMenu,
        meta: {
          display: "Thanh menu",
          show: "true"
        }
      },
      {
        name: "dmLinhVuc",
        path: "dmLinhVuc",
        component: dmLinhVuc,
        meta: {
          display: "Lĩnh vực",
          show: "true"
        }
      },
      {
        name: "dmLoaiVanBan",
        path: "dmLoaiVanBan",
        component: dmLoaiVanBan,
        meta: {
          display: "Loại văn bản",
          show: "true"
        }
      },
      {
        name: "newsBanTin",
        path: "newsBanTin",
        component: newsBanTin,
        meta: {
          display: "Bản tin",
          show: "true"
        }
      },
      {
        name: "newsVanBan",
        path: "newsVanBan",
        component: newsVanBan,
        meta: {
          display: "Văn bản",
          show: "true"
        }
      },
      {
        name: "newsHoiDap",
        path: "newsHoiDap",
        component: newsHoiDap,
        meta: {
          display: "Hỏi đáp",
          show: "true"
        }
      },
      {
        name: "newsBanner",
        path: "newsBanner",
        component: newsBanner,
        meta: {
          display: "Banner",
          show: "true"
        }
      },
      {
        name: "newsVideo",
        path: "newsVideo",
        component: newsVideo,
        meta: {
          display: "Video",
          show: "true"
        }
      }
    ]
  },
  {
    name: "QuanLy",
    path: "/admin/",
    component: ParentCom,
    icon: "fe-list",
    meta: {
      display: "Quản lý",
      show: "true"
    },
    children: [
      {
        name: "qlDangKyHocNghe",
        path: "qlDangKyHocNghe",
        component: qlDangKyHocNghe,
        meta: {
          display: "Đăng ký học nghề",
          show: "true"
        }
      },
      {
        name: "qlDangKyViecLam",
        path: "qlDangKyViecLam",
        component: qlDangKyViecLam,
        meta: {
          display: "Việc làm ngoài nước",
          show: "true"
        }
      },
      {
        name: "qlDoanhNghiep",
        path: "qlDoanhNghiep",
        component: qlDoanhNghiep,
        meta: {
          display: "Doanh nghiệp",
          show: "true"
        }
      },
      {
        name: "qlTuyenDung",
        path: "qlTuyenDung",
        component: hsTuyenDung,
        meta: {
          display: "Tuyển dụng",
          show: "true"
        }
      },
      {
        name: "qlNguoiLaoDong",
        path: "qlNguoiLaoDong",
        component: qlNguoiLaoDong,
        meta: {
          display: "Người lao động",
          show: "true"
        }
      },
      {
        name: "hsTimViec",
        path: "hsTimViec",
        component: hsTimViec,
        meta: {
          display: "Tìm việc",
          show: "true"
        }
      },
      {
        name: "qlSanViecLam",
        path: "qlSanViecLam",
        component: qlSanViecLam,
        meta: {
          display: "Sàn việc làm",
          show: "true"
        }
      },
      {
        name: "qlCauHinh",
        path: "qlCauHinh",
        component: qlCauHinh,
        meta: {
          display: "Cấu hình",
          show: "true"
        }
      }
    ]
  },
  // {
  //   name: "Thống kê - Báo cáo",
  //   path: "/thongke",
  //   component: ParentCom,
  //   icon: "fe-pie-chart",
  //   meta: {
  //     display: "Thống kê - Báo cáo",
  //     show: "true"
  //   },
  //   children: []
  // },
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
