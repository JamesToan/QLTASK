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
export function updateProfile(FullName, Phone, JiraAcount, JiraPass) {
  const data = {
    FullName,
    Phone,
    JiraAcount,
    JiraPass
  };
  return request({
    url: "api/user/UpdateProfile",
    method: "post",
    data
  });
}

export function getUserUnitId() {
  return request({
    url: "api/user/getUnitId",
    method: "post"
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

//ThongKe
export function getThongKe(DichVuId) {
  const params = { DichVuId };

  return request({
    url: "api/ThongKe/Dashboard",
    method: "get",
    params: params
  });
}

// States
export function getStates() {
 
  return request({
    url: "api/States/Get",
    method:'get'

  });
}

export function selectStates() {

  return request({
    url: "api/States/Select",
    method: "get"
  });
}

export function addStates(data) {
  //const data = { };
  return request({
    url: "api/States/Add",
    method: "post",
    data: data
  });
}

export function updateStates(data) {

  return request({
    url: "api/States/Update",
    method: "post",
    data: data
  });
}

export function deleteStates(id) {
  const params = { id };
  return request({
    url: "api/States/delete",
    method: "post",
    params: params
  });
}


// Status
export function getStatus() {

  return request({
    url: "api/Status/get",
    method: 'get'

  });
}

export function selectStatus() {

  return request({
    url: "api/Status/Select",
    method: "get"
  });
}

export function addStatus(data) {
  //const data = { };
  return request({
    url: "api/Status/add",
    method: "post",
    data: data
  });
}

export function updateStatus(data) {

  return request({
    url: "api/Status/update",
    method: "post",
    data: data
  });
}

export function deleteStatus(id) {
  const params = { id };
  return request({
    url: "api/Status/delete",
    method: "post",
    params: params
  });
}


// DichVu
export function getDichVu() {

  return request({
    url: "api/DichVu/Get",
    method: 'get'

  });
}

export function selectDichVu() {

  return request({
    url: "api/DichVu/Select",
    method: "get"
  });
}

export function addDichVu(data) {
  //const data = { };
  return request({
    url: "api/DichVu/add",
    method: "post",
    data: data
  });
}

export function updateDichVu(data) {

  return request({
    url: "api/DichVu/Update",
    method: "post",
    data: data
  });
}

export function deleteDichVu(id) {
  const params = { id };
  return request({
    url: "api/DichVu/delete",
    method: "post",
    params: params
  });
}

// NhanSu
export function getNhanSu() {

  return request({
    url: "api/NhanSu/Get",
    method: 'get'

  });
}

export function selectNhanSu() {

  return request({
    url: "api/NhanSu/Select",
    method: "get"
  });
}

export function addNhanSu(data) {
  //const data = { };
  return request({
    url: "api/NhanSu/add",
    method: "post",
    data: data
  });
}

export function updateNhanSu(data) {

  return request({
    url: "api/NhanSu/update",
    method: "post",
    data: data
  });
}

export function deleteNhanSu(id) {
  const params = { id };
  return request({
    url: "api/NhanSu/delete",
    method: "post",
    params: params
  });
}

export function getCurrentNS() {
  
  return request({
    url: "api/NhanSu/getCurrentNS",
    method: "post",
   
  });
}

// YeuCau
export function getYeuCau() {

  return request({
    url: "api/yeucau/Get",
    method: 'get'

  });
}

export function selectYeuCau(StateId, DichVuId) {
  const params = { StateId, DichVuId };
  return request({
    url: "api/yeucau/Select",
    method: "get",
    params: params
  });
}

export function selectYeuCauAll(StateId, DichVuId) {
  const params = { StateId, DichVuId };
  return request({
    url: "api/yeucau/SelectAll",
    method: "get",
    params: params
  });
}

export function addYeuCau(data) {
  //const data = { };
  return request({
    url: "api/yeucau/add",
    method: "post",
    data: data
  });
}

export function updateYeuCau(data) {

  return request({
    url: "api/yeucau/update",
    method: "post",
    data: data
  });
}

export function deleteYeuCau(id) {
  const params = { id };
  return request({
    url: "api/yeucau/delete",
    method: "post",
    params: params
  });
}

export function getTrangThai(majira) {
  const params = { majira };
  return request({
    url: "api/yeucau/GetTrangThai",
    method: 'get',
    params: params
  });
}

export function sendTeleAsync(id) {
  const params = { id };
  return request({
    url: "api/yeucau/sendTeleAsync",
    method: 'get',
    params: params
  });
}

export function getYCUnitId(id) {
  const params = { id };
  return request({
    url: "api/yeucau/getYCUnitId",
    method: "post",
    params: params
  });
}


export function completeYeuCau(data) {

  return request({
    url: "api/yeucau/Completed",
    method: "post",
    data: data
  });
}

export function acceptYeuCau(Id) {
  const params = { Id };
  return request({
    url: "api/yeucau/Accept",
    method: "post",
    params: params
  });
}

export function forwardYeuCau(Id) {
  const params = { Id };
  return request({
    url: "api/yeucau/Forward",
    method: "post",
    params: params
  });
}

export function deniesYeuCau(Id) {
  const params = { Id };
  return request({
    url: "api/yeucau/Denies",
    method: "post",
    params: params
  });
}
// Jira

export function getJira() {

  return request({
    url: "api/jira/Get",
    method: 'get'

  });
}

export function selectJira() {
  
  return request({
    url: "api/jira/Select",
    method: "get"
  });
}

export function addJira(data) {
  //const data = { };
  return request({
    url: "api/jira/add",
    method: "post",
    data: data
  });
}

export function updateJira(data) {

  return request({
    url: "api/jira/update",
    method: "post",
    data: data
  });
}

export function deleteJira(id) {
  const params = { id };
  return request({
    url: "api/jira/delete",
    method: "post",
    params: params
  });
}


// danh muc
export function getListDanhMucYeuCau() {
  const params = {};
  return request({
    url: "api/dmdanhmuc/yeucau",
    method: "get",
    params: params
  });
}


// DonViYeuCau

export function getDonVi() {

  return request({
    url: "api/donviyeucau/Get",
    method: 'get'

  });
}

export function selectDonVi() {

  return request({
    url: "api/donviyeucau/Select",
    method: "get"
  });
}

export function addDonVi(data) {
  //const data = { };
  return request({
    url: "api/donviyeucau/add",
    method: "post",
    data: data
  });
}

export function updateDonVi(data) {

  return request({
    url: "api/donviyeucau/update",
    method: "post",
    data: data
  });
}

export function deleteDonVi(id) {
  const params = { id };
  return request({
    url: "api/donviyeucau/delete",
    method: "post",
    params: params
  });
}

// Comments

export function getComments() {

  return request({
    url: "api/comment/Get",
    method: 'get'

  });
}

export function selectComments(ycId) {
  const params = { ycId};
  return request({
    url: "api/comment/Select",
    method: "get",
    params: params
  });
}

export function addComments(comments, ycid) {
  const params = { comments, ycid };
  return request({
    url: "api/comment/Add",
    method: "post",
    params: params
  });
}

export function updateComments(comments, id) {
  const params = { comments, id };
  return request({
    url: "api/comment/update",
    method: "post",
    params: params
  });
}

export function deleteComments(id) {
  const params = { id };
  return request({
    url: "api/comment/delete",
    method: "post",
    params: params
  });
}
