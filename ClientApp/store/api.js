import axios from "axios";
import { getUser, getToken } from "./common";
import request from "./request";
let base = "api";

export function requestSignup(Username, Password, Role) {
  const data = {
    Username,
    Password,
    Role
  };
  return request({
    url: "api/token/signup",
    method: "post",
    data
  });
}
export function requestLogin(Username, Password) {
  const data = {
    Username,
    Password
  };
  return request({
    url: "api/token/login",
    method: "post",
    data
  });
}
export function requestGetInfo() {
  return request({
    url: "api/user/info",
    method: "post"
  });
}
export function requestGetProfile(UserName) {
  const data = { UserName };
  return request({
    url: "api/user/getInfo",
    method: "post",
    data
  });
}
//User
export function addUser(UserName, FullName, Phone, RoleId, UnitId, isActive) {
  const data = {
    UserName,
    FullName,
    Phone,
    RoleId,
    UnitId,
    isActive
  };
  return request({
    url: "api/user/add",
    method: "post",
    data
  });
}

export function updateUser(
  Id,
  UserName,
  FullName,
  Phone,
  RoleId,
  UnitId,
  isActive
) {
  const data = {
    Id,
    UserName,
    FullName,
    Phone,
    RoleId,
    UnitId,
    isActive
  };
  return request({
    url: "api/user/update",
    method: "post",
    data
  });
}
export function deleteUser(Id) {
  const data = { Id };
  return request({
    url: "api/user/delete",
    method: "post",
    data
  });
}
export function getListUser() {
  const data = {};
  return request({
    url: "api/user/list",
    method: "get",
    params: data
  });
}
export function resetUser(Id) {
  const data = { Id };
  return request({
    url: "api/user/ResetPass",
    method: "post",
    data
  });
}
export function changePass(PasswordOld, PasswordNew) {
  const data = {
    PasswordOld,
    PasswordNew
  };
  return request({
    url: "api/user/Changepass",
    method: "post",
    data
  });
}
export function updateProfile(FullName, Phone) {
  const data = {
    FullName,
    Phone
  };
  return request({
    url: "api/user/UpdateProfile",
    method: "post",
    data
  });
}
//Permis
export function addPer(Route, Path, IsActive) {
  const data = { Route, Path, IsActive };
  return request({
    url: "api/permission/add",
    method: "post",
    data
  });
}
export function updatePer(Id, Route, Path, IsActive) {
  const data = { Id, Route, Path, IsActive };
  return request({
    url: "api/permission/update",
    method: "post",
    data
  });
}
export function deletePer(Id) {
  const data = { Id };
  return request({
    url: "api/permission/delete",
    method: "post",
    data
  });
}
export function activePer(Id, Route, Path, IsActive) {
  const data = { Id, Route, Path, IsActive };
  return request({
    url: "api/permission/active",
    method: "post",
    data
  });
}
export function getListPer() {
  const params = {};
  return request({
    url: "api/permission/list",
    method: "get",
    params: params
  });
}
export function getListNotPer(RoleId) {
  const params = { RoleId };
  return request({
    url: "api/permission/listNotPer",
    method: "get",
    params: params
  });
}
export function getListPerActive() {
  const params = {};
  return request({
    url: "api/permission/listActive",
    method: "get",
    params: params
  });
}
//PermisRole
export function addPerRole(PermissionId, RoleId, IsActive) {
  const data = { PermissionId, RoleId, IsActive };
  return request({
    url: "api/permissionrole/add",
    method: "post",
    data
  });
}
export function updatePerRole(Id, PermissionId, RoleId, IsActive) {
  const data = { Id, PermissionId, RoleId, IsActive };
  return request({
    url: "api/permissionrole/update",
    method: "post",
    data
  });
}
export function deletePerRole(Id) {
  const data = { Id };
  return request({
    url: "api/permissionrole/delete",
    method: "post",
    data
  });
}
export function getListPerRole(RoleId) {
  const data = { RoleId };
  return request({
    url: "api/permissionrole/list",
    method: "get",
    params: data
  });
}
export function getListPerRoleActive(RoleId) {
  const data = { RoleId };
  return request({
    url: "api/permissionrole/listActive",
    method: "get",
    params: data
  });
}
export function copyPerRole(roleFrom, roleTo) {
  const data = { roleFrom, roleTo };
  return request({
    url: "api/permissionrole/copy",
    method: "post",
    data
  });
}
//Unit
export function addUnit(UnitName, ParentId, IsActive) {
  const data = { UnitName, ParentId, IsActive };
  return request({
    url: "api/unit/add",
    method: "post",
    data
  });
}

export function updateUnit(Id, UnitName, ParentId, IsActive) {
  const data = { Id, UnitName, ParentId, IsActive };
  return request({
    url: "api/unit/update",
    method: "post",
    data
  });
}
export function deleteUnit(Id) {
  const data = { Id };
  return request({
    url: "api/unit/delete",
    method: "post",
    data
  });
}
export function getListUnit() {
  const data = {};
  return request({
    url: "api/unit/list",
    method: "get",
    params: data
  });
}
export function getListUnitAll() {
  const data = {};
  return request({
    url: "api/unit/listall",
    method: "get",
    params: data
  });
}
//Unit
export function addRole(RoleName, IsActive) {
  const data = { RoleName, IsActive };
  return request({
    url: "api/role/add",
    method: "post",
    data
  });
}

export function updateRole(Id, RoleName, IsActive) {
  const data = { Id, RoleName, IsActive };
  return request({
    url: "api/role/update",
    method: "post",
    data
  });
}
export function deleteRole(Id) {
  const data = { Id };
  return request({
    url: "api/role/delete",
    method: "post",
    data
  });
}
export function getListRole() {
  const data = {};
  return request({
    url: "api/role/list",
    method: "get",
    params: data
  });
}
//DMDanhMuc
export function getListDanhMucNews() {
  const params = {};
  return request({
    url: "api/dmdanhmuc/news",
    method: "get",
    params: params
  });
}
export function getListDanhMucSVL() {
  const params = {};
  return request({
    url: "api/dmdanhmuc/svl",
    method: "get",
    params: params
  });
}
//ThongKe
export function getThongKe(nam) {
  const params = { nam };

  return request({
    url: "api/ThongKe/Dashboard",
    method: "get",
    params: params
  });
}
//CauHinh
export function getCauHinh(ma) {
  const params = { "filter[MaCauHinh]": ma };
  return request({
    url: "api/data/CauHinh",
    method: "get",
    params: params
  });
}
export function getListCauHinh(inc) {
  const params = { include: inc };
  return request({
    url: "api/data/CauHinh",
    method: "get",
    params: params
  });
}
export function updateCauHinh(id, data) {
  //const data = {};
  return request({
    url: "api/data/CauHinh/" + id,
    method: "put",
    data
  });
}
//NewsChuyenMuc
export function getListChuyenMuc(inc) {
  const params = { include: inc };
  return request({
    url: "api/data/NewsChuyenMuc",
    method: "get",
    params: params
  });
}
export function addChuyenMuc(data) {
  //const data = {};
  return request({
    url: "api/data/NewsChuyenMuc",
    method: "post",
    data
  });
}
export function updateChuyenMuc(id, data) {
  //const data = {};
  return request({
    url: "api/data/NewsChuyenMuc/" + id,
    method: "put",
    data
  });
}
export function deleteChuyenMuc(id) {
  //const data = {};
  return request({
    url: "api/data/NewsChuyenMuc/" + id,
    method: "delete"
  });
}
//DMLinhVuc
export function getListLinhVuc(inc) {
  const params = { include: inc };
  return request({
    url: "api/data/DMLinhVuc",
    method: "get",
    params: params
  });
}
export function addLinhVuc(data) {
  //const data = {};
  return request({
    url: "api/data/DMLinhVuc",
    method: "post",
    data
  });
}
export function updateLinhVuc(id, data) {
  //const data = {};
  return request({
    url: "api/data/DMLinhVuc/" + id,
    method: "put",
    data
  });
}
export function deleteLinhVuc(id) {
  //const data = {};
  return request({
    url: "api/data/DMLinhVuc/" + id,
    method: "delete"
  });
}
//DMLoaiVanBan
export function getListLoaiVanBan(inc) {
  const params = { include: inc };
  return request({
    url: "api/data/DMLoaiVanBan",
    method: "get",
    params: params
  });
}
export function addLoaiVanBan(data) {
  //const data = {};
  return request({
    url: "api/data/DMLoaiVanBan",
    method: "post",
    data
  });
}
export function updateLoaiVanBan(id, data) {
  //const data = {};
  return request({
    url: "api/data/DMLoaiVanBan/" + id,
    method: "put",
    data
  });
}
export function deleteLoaiVanBan(id) {
  //const data = {};
  return request({
    url: "api/data/DMLoaiVanBan/" + id,
    method: "delete"
  });
}
//DMNganhNghe
export function getListNganhNghe(inc) {
  const params = { include: inc };
  return request({
    url: "api/data/DMNganhNghe",
    method: "get",
    params: params
  });
}
export function addNganhNghe(data) {
  //const data = {};
  return request({
    url: "api/data/DMNganhNghe",
    method: "post",
    data
  });
}
export function updateNganhNghe(id, data) {
  //const data = {};
  return request({
    url: "api/data/DMNganhNghe/" + id,
    method: "put",
    data
  });
}
export function deleteNganhNghe(id) {
  //const data = {};
  return request({
    url: "api/data/DMNganhNghe/" + id,
    method: "delete"
  });
}
//DMNganhNghe
export function getListLoaiHinh(inc) {
  const params = { include: inc };
  return request({
    url: "api/data/DMLoaiHinh",
    method: "get",
    params: params
  });
}
export function addLoaiHinh(data) {
  //const data = {};
  return request({
    url: "api/data/DMLoaiHinh",
    method: "post",
    data
  });
}
export function updateLoaiHinh(id, data) {
  //const data = {};
  return request({
    url: "api/data/DMLoaiHinh/" + id,
    method: "put",
    data
  });
}
export function deleteLoaiHinh(id) {
  //const data = {};
  return request({
    url: "api/data/DMLoaiHinh/" + id,
    method: "delete"
  });
}
//DMGioiTinh
export function getListGioiTinh(inc) {
  const params = { include: inc };
  return request({
    url: "api/data/DMGioiTinh",
    method: "get",
    params: params
  });
}
export function addGioiTinh(data) {
  //const data = {};
  return request({
    url: "api/data/DMGioiTinh",
    method: "post",
    data
  });
}
export function updateGioiTinh(id, data) {
  //const data = {};
  return request({
    url: "api/data/DMGioiTinh/" + id,
    method: "put",
    data
  });
}
export function deleteGioiTinh(id) {
  //const data = {};
  return request({
    url: "api/data/DMGioiTinh/" + id,
    method: "delete"
  });
}
//DMNgoaiNgu
export function getListNgoaiNgu(inc) {
  const params = { include: inc };
  return request({
    url: "api/data/DMNgoaiNgu",
    method: "get",
    params: params
  });
}
export function addNgoaiNgu(data) {
  //const data = {};
  return request({
    url: "api/data/DMNgoaiNgu",
    method: "post",
    data
  });
}
export function updateNgoaiNgu(id, data) {
  //const data = {};
  return request({
    url: "api/data/DMNgoaiNgu/" + id,
    method: "put",
    data
  });
}
export function deleteNgoaiNgu(id) {
  //const data = {};
  return request({
    url: "api/data/DMNgoaiNgu/" + id,
    method: "delete"
  });
}
//DMTinHoc
export function getListTinHoc(inc) {
  const params = { include: inc };
  return request({
    url: "api/data/DMTinHoc",
    method: "get",
    params: params
  });
}
export function addTinHoc(data) {
  //const data = {};
  return request({
    url: "api/data/DMTinHoc",
    method: "post",
    data
  });
}
export function updateTinHoc(id, data) {
  //const data = {};
  return request({
    url: "api/data/DMTinHoc/" + id,
    method: "put",
    data
  });
}
export function deleteTinHoc(id) {
  //const data = {};
  return request({
    url: "api/data/DMTinHoc/" + id,
    method: "delete"
  });
}
//DMVanBang
export function getListVanBang(inc) {
  const params = { include: inc };
  return request({
    url: "api/data/DMVanBang",
    method: "get",
    params: params
  });
}
export function addVanBang(data) {
  //const data = {};
  return request({
    url: "api/data/DMVanBang",
    method: "post",
    data
  });
}
export function updateVanBang(id, data) {
  //const data = {};
  return request({
    url: "api/data/DMVanBang/" + id,
    method: "put",
    data
  });
}
export function deleteVanBang(id) {
  //const data = {};
  return request({
    url: "api/data/DMVanBang/" + id,
    method: "delete"
  });
}
//DMViTriCongViec
export function getListViTriCongViec(inc) {
  const params = { include: inc };
  return request({
    url: "api/data/DMViTriCongViec",
    method: "get",
    params: params
  });
}
export function addViTriCongViec(data) {
  //const data = {};
  return request({
    url: "api/data/DMViTriCongViec",
    method: "post",
    data
  });
}
export function updateViTriCongViec(id, data) {
  //const data = {};
  return request({
    url: "api/data/DMViTriCongViec/" + id,
    method: "put",
    data
  });
}
export function deleteViTriCongViec(id) {
  //const data = {};
  return request({
    url: "api/data/DMViTriCongViec/" + id,
    method: "delete"
  });
}
//NguoiLaoDong
export function getNguoiLaoDong() {
  //const params = { };
  return request({
    url: "api/NguoiLaoDong/get",
    method: "get"
    //params: params
  });
}
export function selectNguoiLaoDong() {
  //const params = { };
  return request({
    url: "api/NguoiLaoDong/Select",
    method: "get"
    //params: params
  });
}
export function updateNguoiLaoDong(data) {
  //const data = { };
  return request({
    url: "api/NguoiLaoDong/update",
    method: "post",
    data: data
  });
}
export function deleteNguoiLaoDong(id) {
  const params = { id };
  return request({
    url: "api/NguoiLaoDong/delete",
    method: "post",
    params: params
  });
}
//DoanhNghiep
export function getDoanhNghiep() {
  //const params = { };
  return request({
    url: "api/DoanhNghiep/get",
    method: "get"
    //params: params
  });
}
export function selectDoanhNghiep() {
  //const params = { };
  return request({
    url: "api/DoanhNghiep/Select",
    method: "get"
    //params: params
  });
}
export function updateDoanhNghiep(data) {
  //const data = { };
  return request({
    url: "api/DoanhNghiep/update",
    method: "post",
    data: data
  });
}
export function activeDoanhNghiep(data) {
  //const data = { };
  return request({
    url: "api/DoanhNghiep/active",
    method: "post",
    data: data
  });
}
export function deleteDoanhNghiep(id) {
  const params = { id };
  return request({
    url: "api/DoanhNghiep/delete",
    method: "post",
    params: params
  });
}
//HoSoTimViec
export function getHoSoTimViec() {
  //const params = { };
  return axios.get("/api/HoSoTimViec/get", {
    headers: { Authorization: "Bearer " + getToken() }
  });
}
export function selectHoSoTimViec() {
  //const params = { };
  return request({
    url: "api/HoSoTimViec/select",
    method: "get"
    //params: params
  });
}
export function addHoSoTimViec(data) {
  //const data = { };
  return request({
    url: "api/HoSoTimViec/add",
    method: "post",
    data: data
  });
}
export function updateHoSoTimViec(data) {
  //const data = { };
  return request({
    url: "api/HoSoTimViec/update",
    method: "post",
    data: data
  });
}
export function activeHoSoTimViec(data) {
  //const data = { };
  return request({
    url: "api/HoSoTimViec/active",
    method: "post",
    data: data
  });
}
export function deleteHoSoTimViec(id) {
  const params = { id };
  return request({
    url: "api/HoSoTimViec/delete",
    method: "post",
    params: params
  });
}
//HoSoTuyenDung
export function getHoSoTuyenDung(conHan) {
  //const params = { };
  return axios.get("/api/HoSoTuyenDung/get?conHan=" + conHan, {
    headers: { Authorization: "Bearer " + getToken() }
  });
}
export function selectHoSoTuyenDung() {
  //const params = { };
  return request({
    url: "api/HoSoTuyenDung/Select",
    method: "get"
    //params: params
  });
}
export function addHoSoTuyenDung(data) {
  //const data = { };
  return request({
    url: "api/HoSoTuyenDung/add",
    method: "post",
    data: data
  });
}
export function updateHoSoTuyenDung(data) {
  //const data = { };
  return request({
    url: "api/HoSoTuyenDung/update",
    method: "post",
    data: data
  });
}
export function activeHoSoTuyenDung(data) {
  //const data = { };
  return request({
    url: "api/HoSoTuyenDung/active",
    method: "post",
    data: data
  });
}
export function deleteHoSoTuyenDung(id) {
  const params = { id };
  return request({
    url: "api/HoSoTuyenDung/delete",
    method: "post",
    params: params
  });
}
//DaoTaoNghe
export function selectDaoTaoNghe() {
  //const params = { };
  return request({
    url: "api/DaoTaoNghe/Select",
    method: "get"
    //params: params
  });
}
export function addDaoTaoNghe(data) {
  //const data = { };
  return request({
    url: "api/DaoTaoNghe/add",
    method: "post",
    data: data
  });
}
export function updateDaoTaoNghe(data) {
  //const data = { };
  return request({
    url: "api/DaoTaoNghe/update",
    method: "post",
    data: data
  });
}
export function deleteDaoTaoNghe(id) {
  const params = { id };
  return request({
    url: "api/DaoTaoNghe/delete",
    method: "post",
    params: params
  });
}

//DangKyHocNghe
export function selectDangKyHocNghe() {
  //const params = { };
  return request({
    url: "api/DangKyHocNghe/Select",
    method: "get"
    //params: params
  });
}
export function addDangKyHocNghe(data) {
  //const data = { };
  return request({
    url: "api/DangKyHocNghe/add",
    method: "post",
    data: data
  });
}
export function updateDangKyHocNghe(data) {
  //const data = { };
  return request({
    url: "api/DangKyHocNghe/update",
    method: "post",
    data: data
  });
}
export function deleteDangKyHocNghe(id) {
  const params = { id };
  return request({
    url: "api/DangKyHocNghe/delete",
    method: "post",
    params: params
  });
}

//DangKyViecLam
export function selectDangKyViecLam() {
  //const params = { };
  return request({
    url: "api/DangKyViecLam/Select",
    method: "get"
    //params: params
  });
}
export function addDangKyViecLam(data) {
  //const data = { };
  return request({
    url: "api/DangKyViecLam/add",
    method: "post",
    data: data
  });
}
export function updateDangKyViecLam(data) {
  //const data = { };
  return request({
    url: "api/DangKyViecLam/update",
    method: "post",
    data: data
  });
}
export function deleteDangKyViecLam(id) {
  const params = { id };
  return request({
    url: "api/DangKyViecLam/delete",
    method: "post",
    params: params
  });
}
//SanViecLam
export function getSanViecLam() {
  //const params = { };
  return axios.get("/api/SanViecLam/get", {
    headers: { Authorization: "Bearer " + getToken() }
  });
}
export function selectSanViecLam(dangDienRa) {
  const params = { dangDienRa };
  return request({
    url: "api/SanViecLam/Select",
    method: "get",
    params: params
  });
}
export function addSanViecLam(data) {
  //const data = { };
  return request({
    url: "api/SanViecLam/add",
    method: "post",
    data: data
  });
}
export function updateSanViecLam(data) {
  //const data = { };
  return request({
    url: "api/SanViecLam/update",
    method: "post",
    data: data
  });
}
export function deleteSanViecLam(id) {
  const params = { id };
  return request({
    url: "api/SanViecLam/delete",
    method: "post",
    params: params
  });
}
export function getSVLDoanhNghiep(id) {
  const params = { id };
  return request({
    url: "api/SanViecLam/GetHoSoTuyenDung",
    method: "get",
    params: params
  });
}
export function getSVLNguoiLaoDong(id) {
  const params = { id };
  return request({
    url: "api/SanViecLam/GetHoSoUngTuyen",
    method: "get",
    params: params
  });
}
export function joinSVLDoanhNghiep(data) {
  //const data = { };
  return request({
    url: "api/SanViecLam/joinSVLDoanhNghiep",
    method: "post",
    data: data
  });
}
export function removeSVLDoanhNghiep(data) {
  //const data = { };
  return request({
    url: "api/SanViecLam/removeSVLDoanhNghiep",
    method: "post",
    data: data
  });
}
export function joinSVLNguoiLaoDong(data) {
  //const data = { };
  return request({
    url: "api/SanViecLam/joinSVLNguoiLaoDong",
    method: "post",
    data: data
  });
}
export function removeSVLNguoiLaoDong(data) {
  //const data = { };
  return request({
    url: "api/SanViecLam/removeSVLNguoiLaoDong",
    method: "post",
    data: data
  });
}
//NewsMenu
export function selectNewsMenu(ChuyenMucId) {
  const params = { ChuyenMucId };
  return request({
    url: "api/NewsMenu/Select",
    method: "get",
    params: params
  });
}
export function addNewsMenu(data) {
  //const data = { };
  return request({
    url: "api/NewsMenu/add",
    method: "post",
    data: data
  });
}
export function updateNewsMenu(data) {
  //const data = { };
  return request({
    url: "api/NewsMenu/update",
    method: "post",
    data: data
  });
}
export function activeNewsMenu(data) {
  //const data = { };
  return request({
    url: "api/NewsMenu/active",
    method: "post",
    data: data
  });
}
export function deleteNewsMenu(id) {
  const params = { id };
  return request({
    url: "api/NewsMenu/delete",
    method: "post",
    params: params
  });
}

//NewsHoiDap
export function getNewsHoiDap(uid) {
  const params = { uid };
  return request({
    url: "api/NewsHoiDap/get",
    method: "get",
    params: params
  });
}
export function selectNewsHoiDap() {
  //const params = { };
  return request({
    url: "api/NewsHoiDap/Select",
    method: "get"
    //params: params
  });
}
export function addNewsHoiDap(data) {
  //const data = { };
  return request({
    url: "api/NewsHoiDap/add",
    method: "post",
    data: data
  });
}
export function updateNewsHoiDap(data) {
  //const data = { };
  return request({
    url: "api/NewsHoiDap/update",
    method: "post",
    data: data
  });
}
export function activeNewsHoiDap(data) {
  //const data = { };
  return request({
    url: "api/NewsHoiDap/active",
    method: "post",
    data: data
  });
}
export function deleteNewsHoiDap(id) {
  const params = { id };
  return request({
    url: "api/NewsHoiDap/delete",
    method: "post",
    params: params
  });
}

//NewsVanBan
export function getNewsVanBan(uid) {
  const params = { uid };
  return request({
    url: "api/NewsVanBan/get",
    method: "get",
    params: params
  });
}
export function selectNewsVanBan(ChuyenMucId) {
  const params = { ChuyenMucId };
  return request({
    url: "api/NewsVanBan/Select",
    method: "get",
    params: params
  });
}
export function addNewsVanBan(data) {
  //const data = { };
  return request({
    url: "api/NewsVanBan/add",
    method: "post",
    data: data
  });
}
export function updateNewsVanBan(data) {
  //const data = { };
  return request({
    url: "api/NewsVanBan/update",
    method: "post",
    data: data
  });
}
export function activeNewsVanBan(data) {
  //const data = { };
  return request({
    url: "api/NewsVanBan/active",
    method: "post",
    data: data
  });
}
export function deleteNewsVanBan(id) {
  const params = { id };
  return request({
    url: "api/NewsVanBan/delete",
    method: "post",
    params: params
  });
}

//NewsBanner
export function getNewsBanner(uid) {
  const params = { uid };
  return request({
    url: "api/NewsBanner/get",
    method: "get",
    params: params
  });
}
export function selectNewsBanner(ChuyenMucId) {
  const params = { ChuyenMucId };
  return request({
    url: "api/NewsBanner/Select",
    method: "get",
    params: params
  });
}
export function addNewsBanner(data) {
  //const data = { };
  return request({
    url: "api/NewsBanner/add",
    method: "post",
    data: data
  });
}
export function updateNewsBanner(data) {
  //const data = { };
  return request({
    url: "api/NewsBanner/update",
    method: "post",
    data: data
  });
}
export function activeNewsBanner(data) {
  //const data = { };
  return request({
    url: "api/NewsBanner/active",
    method: "post",
    data: data
  });
}
export function deleteNewsBanner(id) {
  const params = { id };
  return request({
    url: "api/NewsBanner/delete",
    method: "post",
    params: params
  });
}

//NewsVideo
export function getNewsVideo(uid) {
  const params = { uid };
  return request({
    url: "api/NewsVideo/get",
    method: "get",
    params: params
  });
}
export function selectNewsVideo(ChuyenMucId) {
  const params = { ChuyenMucId };
  return request({
    url: "api/NewsVideo/Select",
    method: "get",
    params: params
  });
}
export function addNewsVideo(data) {
  //const data = { };
  return request({
    url: "api/NewsVideo/add",
    method: "post",
    data: data
  });
}
export function updateNewsVideo(data) {
  //const data = { };
  return request({
    url: "api/NewsVideo/update",
    method: "post",
    data: data
  });
}
export function activeNewsVideo(data) {
  //const data = { };
  return request({
    url: "api/NewsVideo/active",
    method: "post",
    data: data
  });
}
export function deleteNewsVideo(id) {
  const params = { id };
  return request({
    url: "api/NewsVideo/delete",
    method: "post",
    params: params
  });
}

//NewsBanTin
export function getNewsBanTin(uid) {
  const params = { uid };
  return request({
    url: "api/NewsBanTin/get",
    method: "get",
    params: params
  });
}
export function selectNewsBanTin(ChuyenMucId) {
  const params = { ChuyenMucId };
  return request({
    url: "api/NewsBanTin/Select",
    method: "get",
    params: params
  });
}
export function addNewsBanTin(data) {
  //const data = { };
  return request({
    url: "api/NewsBanTin/add",
    method: "post",
    data: data
  });
}
export function updateNewsBanTin(data) {
  //const data = { };
  return request({
    url: "api/NewsBanTin/update",
    method: "post",
    data: data
  });
}
export function activeNewsBanTin(data) {
  //const data = { };
  return request({
    url: "api/NewsBanTin/active",
    method: "post",
    data: data
  });
}
export function deleteNewsBanTin(id) {
  const params = { id };
  return request({
    url: "api/NewsBanTin/delete",
    method: "post",
    params: params
  });
}
