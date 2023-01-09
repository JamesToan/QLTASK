import Cookies from 'js-cookie'
const TokenKey = 'UToken'
const UserKey = 'UUser'
const Remain = 'URemain'
const Code = 'Code'
const RoleKey = 'URole'
const Unit = 'UUnit'

export function getToken() {
  return Cookies.get(TokenKey)
}

export function setToken(token) {
  return Cookies.set(TokenKey, token, {
    expires: 7
  })
}

export function getRole() {
  return Cookies.get(RoleKey)
}

export function setRole(role) {
  return Cookies.set(RoleKey, role, {
    expires: 7
  })
}

export function removeRole() {
  return Cookies.remove(RoleKey)
}

export function removeToken() {
  return Cookies.remove(TokenKey)
}

export function getUser() {
  return Cookies.get(UserKey)
}

export function setUser(user) {
  return Cookies.set(UserKey, user, {
    expires: 7
  })
}

export function getRemain() {
  return Cookies.get(Remain)
}

export function setRemain(remain) {
  return Cookies.set(Remain, remain, {
    expires: 7
  })
}
export function removeRemain() {
  return Cookies.remove(Remain)
}
export function removeUser() {
  return Cookies.remove(UserKey)
}

export function getCode() {
  return Cookies.get(Code)
}

export function setCode(code) {
  return Cookies.set(Code, code, {
    expires: 7
  })
}

export function getUnit() {
  return Cookies.get(Unit)
}

export function setUnit(unit) {
  return Cookies.set(Unit, unit, {
    expires: 7
  })
}


export function removeCode() {
  return Cookies.remove(Code)
}
export function dGkxtz(str) {
  var _cpt = require('cryptlib');
  var izv = 'BcAMtt0czHZzV382';
  var eyk = _cpt.getHashSha256('khzktowqsg', 32);
  var orxt = _cpt.decrypt(str, eyk, izv);
  return orxt;
}
export function eGkxtz(str) {
  var _cpt = require('cryptlib');
  var izv = 'BcAMtt0czHZzV382';
  var eyk = _cpt.getHashSha256('khzktowqsg', 32);
  var cyxt = _cpt.encrypt(str, eyk, izv);
  cyxt = replaceAll(cyxt, "+", "[add]");
  return cyxt;
}
function replaceAll(str, find, replace) {
  return str.replace(new RegExp(escapeRegExp(find), 'g'), replace);
}
function escapeRegExp(str) {
  return str.replace(/([.*+?^=!:${}()|\[\]\/\\])/g, "\\$1");
}

export function baseUrl() {
  return "/"
}
