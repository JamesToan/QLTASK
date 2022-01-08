(function(n, t, i) {
    var r = null;

    function e(n) {
        return n
    }

    function f(n) {
        return decodeURIComponent(n.replace(u, " "))
    }
    var u = /\+/g;
    n.cookie = function(u, o, s) {
        var y, a, v, c, h, l;
        if (o !== i && !/Object/.test(Object.prototype.toString.call(o))) return s = n.extend({}, n.cookie.defaults, s), o === r && (s.expires = -1), typeof s.expires == "number" && (y = s.expires, a = s.expires = new Date, a.setDate(a.getDate() + y)), o = String(o), t.cookie = [encodeURIComponent(u), "=", s.raw ? o : encodeURIComponent(o), s.expires ? "; expires=" + s.expires.toUTCString() : "", s.path ? "; path=" + s.path : "", s.domain ? "; domain=" + s.domain : "", s.secure ? "; secure" : ""].join("");
        for (s = o || n.cookie.defaults || {}, v = s.raw ? e : f, c = t.cookie.split("; "), h = 0; l = c[h] && c[h].split("="); h++)
            if (v(l.shift()) === u) return v(l.join("="));
        return r
    }, n.cookie.defaults = {}, n.removeCookie = function(t, i) {
        return n.cookie(t, i) !== r ? (n.cookie(t, r, i), !0) : !1
    }
})(jQuery, document);