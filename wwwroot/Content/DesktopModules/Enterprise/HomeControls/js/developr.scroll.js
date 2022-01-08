function mouseWheelHandler(n) {
    var e = n || window.event,
        t = e.originalEvent || e,
        f = [].slice.call(arguments, 1),
        i = 0,
        u = 0,
        r = 0;
    return n = $.event.fix(t), n.type = "mousewheel", t.wheelDelta && (i = t.wheelDelta / 120), t.detail && (i = -t.detail / 3), r = i, t.axis !== undefined && t.axis === t.HORIZONTAL_AXIS && (r = 0, u = -1 * i), t.wheelDeltaY !== undefined && (r = t.wheelDeltaY / 120), t.wheelDeltaX !== undefined && (u = t.wheelDeltaX / -120), f.unshift(n, i, u, r), $.event.handle.apply(this, f)
}
var types = ["DOMMouseScroll", "mousewheel"];
$.event.special.mousewheel = {
        setup: function() {
            if (this.addEventListener)
                for (var n = types.length; n;) this.addEventListener(types[--n], mouseWheelHandler, !1);
            else this.onmousewheel = mouseWheelHandler
        },
        teardown: function() {
            if (this.removeEventListener)
                for (var n = types.length; n;) this.removeEventListener(types[--n], mouseWheelHandler, !1);
            else this.onmousewheel = null
        }
    }, $.fn.extend({
        mousewheel: function(n) {
            var t = "mousewheel";
            return n ? this.on(t, n) : this.trigger(t)
        },
        unmousewheel: function(n) {
            return this.off("mousewheel", n)
        }
    }), $.fn.refreshInnerTrackedElements = function() {
        return this.find(".tracked").each(function() {
            var t = $(this);
            $.each(t.data("tracking-elements") || [], function() {
                $(this.element).stop(!0, !0), this.func.call(this.element, t)
            })
        }), this
    }, $.fn.parseCSSValue = function(n, t) {
        var i = parseInt(this.css(n), 10);
        return isNaN(i) ? t || 0 : i
    },
    function(n, t) {
        var k = "scroll sizechange scrollsizechange",
            w = "mousewheel",
            b = "mouseleave",
            p = "mouseenter",
            a = "touchend",
            h = "touchstart",
            y = "padding-right",
            v = "padding-top",
            rt = "padding-left",
            it = "padding-bottom",
            u = !1,
            g = "scroll-options",
            o = ".custom-scroll",
            s = "touch-scrolling",
            i = !0,
            e = "scroll-focus",
            r = "custom-scroll";

        function ut(n) {
            n.preventDefault()
        }

        function l() {
            var u = n(this),
                t = u.data(r);
            t && (u.data(e, i), t.hscrollbar() && t.hscrollbar().animate({
                opacity: 1
            }), t.vscrollbar() && t.vscrollbar().animate({
                opacity: 1
            }))
        }

        function c() {
            var i = n(this),
                t = i.data(r);
            t && (i.removeData(e), t.hscrollbar() && t.hscrollbar().animate({
                opacity: 0
            }), t.vscrollbar() && t.vscrollbar().animate({
                opacity: 0
            }))
        }

        function d(t, i, u, f) {
            if (object = n(this).data(r)) {
                var e = object.mousewheel(u, f);
                e.x == 0 && e.y == 0 && object.settings.continuousWheelScroll || t.preventDefault()
            }
        }

        function tt() {
            n(this).refreshCustomScroll()
        }

        function nt(t) {
            var c = "touchend touchcancel",
                l = "touchmove",
                u = n(this),
                o = u.data(r),
                a = t.originalEvent.touches[0].pageX,
                v = t.originalEvent.touches[0].pageY,
                f, e, h;
            if (o && !u.data(s)) {
                f = function(n) {
                    u.data(s, i);
                    var r = n.originalEvent.touches[0].pageX,
                        t = n.originalEvent.touches[0].pageY;
                    h = o.move(a - r, t - v, i), h.x == 0 && h.y == 0 && o.settings.continuousTouchScroll || n.preventDefault(), a = r, v = t
                }, e = function(n) {
                    n.stopPropagation(), u.off(l, f), u.off(c, e), u.removeData(s)
                };
                u.on(l, f);
                u.on(c, e)
            }
        }
        var f = n(t),
            ft = n("html").hasClass("touch");
        n.fn.customScroll = function(t) {
            var s = n.extend({}, n.fn.customScroll.defaults, t);
            this.filter(o).refreshCustomScroll();
            this.not(o).addClass(r).each(function() {
                var et = "px",
                    at = "css",
                    yt = "animate",
                    pt = "mouseup",
                    wt = "mousemove",
                    ri = "mousedown",
                    si = "selectstart",
                    hi = "<div></div>",
                    oi = "relative",
                    fi = "position";

                function ui(n, t, r) {
                    var f = k,
                        u = nt;
                    return k = Math.max(0, Math.min(k + n, w[0].scrollWidth - w.innerWidth())), nt = Math.max(0, Math.min(nt - t, w[0].scrollHeight - w.innerHeight())), o.animate && !r && ht ? w.stop(i).animate({
                        scrollLeft: k,
                        scrollTop: nt
                    }, {
                        step: function() {
                            w.refreshInnerTrackedElements()
                        }
                    }) : w.scrollLeft(k).scrollTop(nt).refreshInnerTrackedElements(), st && n != 0 && st(), ot && t != 0 && ot(), {
                        x: k - f,
                        y: nt - u
                    }
                }

                function ci(n, t, i) {
                    return n != 0 && (n = n > 0 ? Math.max(n, o.minWheelScroll) : Math.min(n, -o.minWheelScroll)), t != 0 && (t = t > 0 ? Math.max(t, o.minWheelScroll) : Math.min(t, -o.minWheelScroll)), ui(n * o.speed, t * o.speed, i)
                }

                function ei() {
                    st && d.hide(), ot && tt.hide(), kt = w[0].scrollWidth > w.innerWidth(), ni = w[0].scrollHeight > w.innerHeight(), k = w.scrollLeft(), nt = w.scrollTop(), st && (ii = !kt && o.autoHide, st()), ot && (ti = !ni && o.autoHide, ot())
                }
                var w = n(this),
                    bt = w.css(fi),
                    vt = w.is("ul, ol") ? "li" : "div",
                    o = n.extend({}, s, w.data(g)),
                    kt = w[0].scrollWidth > w.innerWidth(),
                    ni = w[0].scrollHeight > w.innerHeight(),
                    k = w.scrollLeft(),
                    nt = w.scrollTop(),
                    d, lt, tt, ct, dt = u,
                    gt = u,
                    st = u,
                    ot = u,
                    ii = u,
                    ti = u,
                    ht = u;
                if (bt !== oi && bt !== "absolute" && bt !== "fixed" && w.css(fi, oi), typeof o.padding != "object" && (o.padding = {
                        top: o.padding,
                        right: o.padding,
                        bottom: o.padding,
                        left: o.padding
                    }), o.padding = n.extend({
                        top: 0,
                        right: 0,
                        bottom: 0,
                        left: 0
                    }, o.padding), o.horizontal && (dt = function() {
                        d = n("<" + vt + ' class="custom-hscrollbar"></' + vt + ">").appendTo(w), lt = n(hi).appendTo(d), d.click(function(n) {
                            n.stopPropagation()
                        });
                        lt.on(si, ut);
                        lt.on(ri, function(t) {
                            function r(t) {
                                var r = d.width() - lt.innerWidth(),
                                    u = Math.max(0, Math.min(r, e + (t.pageX - s)));
                                d[0].style.display = "none", k = r > 0 ? Math.round(u / r * (w[0].scrollWidth - w.innerWidth())) : 0, d[0].style.display = "block", o.animate && ht ? w.stop(i).animate({
                                    scrollLeft: k
                                }, {
                                    step: function() {
                                        n(this).refreshInnerTrackedElements()
                                    }
                                }) : w.stop(i).scrollLeft(k).refreshInnerTrackedElements(), st && st(), ot && ot()
                            }

                            function u() {
                                f.off(wt, r), f.off(pt, u)
                            }
                            var s = t.pageX,
                                e = lt.parseCSSValue("left");
                            t.preventDefault();
                            f.on(wt, r);
                            f.on(pt, u)
                        })
                    }, dt(), st = function() {
                        if (!ii) {
                            d[0].parentNode || dt();
                            var u = w.width(),
                                f = w.innerWidth(),
                                h = o.vertical && ni && !ti ? o.cornerWidth : 0,
                                n = (o.usePadding ? u : f) - o.padding.top - o.padding.bottom - h,
                                t = n > o.minScrollerSize * 1.5 ? o.minScrollerSize : Math.round(n / 1.5),
                                c = n - t,
                                r = Math.round(c * (u / w[0].scrollWidth)) + t,
                                s = Math.round((n - r) * (k / (w[0].scrollWidth - f)));
                            d.show(), d.stop(i)[o.animate && ht ? yt : at]({
                                top: w.innerHeight() - (o.usePadding ? w.parseCSSValue(it) + o.padding.top : o.padding.bottom) - o.width + nt + et,
                                left: (o.usePadding ? w.parseCSSValue(rt) + o.padding.right : o.padding.left) + k + et,
                                width: n + et,
                                height: o.width + et,
                                opacity: w.data(e) || !o.showOnHover ? 1 : 0
                            }), lt.stop(i)[o.animate && ht ? yt : at]({
                                left: s + et,
                                width: Math.round(r) + et
                            })
                        }
                    }), o.vertical && (gt = function() {
                        tt = n("<" + vt + ' class="custom-vscrollbar"></' + vt + ">").appendTo(w), ct = n(hi).appendTo(tt), tt.click(function(n) {
                            n.stopPropagation()
                        });
                        ct.on(si, ut);
                        ct.on(ri, function(t) {
                            function r(t) {
                                var r = tt.height() - ct.innerHeight(),
                                    u = Math.max(0, Math.min(r, e + (t.pageY - s)));
                                tt[0].style.display = "none", nt = r > 0 ? Math.round(u / r * (w[0].scrollHeight - w.innerHeight())) : 0, tt[0].style.display = "block", o.animate && ht ? w.stop(i).animate({
                                    scrollTop: nt
                                }, {
                                    step: function() {
                                        n(this).refreshInnerTrackedElements()
                                    }
                                }) : w.stop(i).scrollTop(nt).refreshInnerTrackedElements(), st && st(), ot && ot()
                            }

                            function u(n) {
                                n.preventDefault(), f.off(wt, r), f.off(pt, u)
                            }
                            var s = t.pageY,
                                e = ct.parseCSSValue("top");
                            t.preventDefault();
                            f.on(wt, r);
                            f.on(pt, u)
                        })
                    }, gt(), ot = function() {
                        if (!ti) {
                            tt[0].parentNode || gt();
                            var u = w.height(),
                                f = w.innerHeight(),
                                h = o.horizontal && kt && !ii ? o.cornerWidth : 0,
                                n = (o.usePadding ? u : f) - o.padding.top - o.padding.bottom - h,
                                t = n > o.minScrollerSize * 1.5 ? o.minScrollerSize : Math.round(n / 1.5),
                                c = n - t,
                                r = c * (u / w[0].scrollHeight) + t,
                                s = Math.round((n - r) * (nt / (w[0].scrollHeight - f)));
                            tt.show(), tt.stop(i)[o.animate && ht ? yt : at]({
                                top: (o.usePadding ? w.parseCSSValue(v) + o.padding.top : o.padding.top) + nt + et,
                                left: w.innerWidth() - (o.usePadding ? w.parseCSSValue(y) + o.padding.right : o.padding.right) - o.width + k + et,
                                height: n + et,
                                width: o.width + et,
                                opacity: w.data(e) || !o.showOnHover ? 1 : 0
                            }), ct.stop(i)[o.animate && ht ? yt : at]({
                                top: s + et,
                                height: Math.round(r) + et
                            })
                        }
                    }), w.data(r, {
                        settings: o,
                        hscrollbar: function() {
                            return d
                        },
                        hscroller: function() {
                            return lt
                        },
                        vscrollbar: function() {
                            return tt
                        },
                        vscroller: function() {
                            return ct
                        },
                        refresh: ei,
                        refreshH: ot,
                        refreshV: ot,
                        move: ui,
                        mousewheel: ci
                    }), ei(), o.showOnHover)
                    if (d && d.css({
                            opacity: 0
                        }), tt && tt.css({
                            opacity: 0
                        }), ft) w.on(h, l).on(a, c);
                    else w.on(p, l).on(b, c);
                ht = i
            }).on(w, d).on(k, tt).on(h, nt);
            return this
        }, n.fn.removeCustomScroll = function() {
            return this.filter(o).off(w, d).off(k, tt).off(h, nt).off(h, l).off(a, c).off(p, l).off(b, c).removeData(g).removeData(s).removeClass(r).children(".custom-hscrollbar, .custom-vscrollbar").remove().scrollLeft(0).scrollTop(0), this
        }, n.fn.hasCustomScroll = function() {
            return this.data(r) ? i : u
        }, n.fn.refreshCustomScroll = function() {
            return this.each(function() {
                var i = n(this).data(r);
                i && i.refresh()
            }), this
        }, n.fn.moveCustomScroll = function(t, i, u) {
            return this.each(function() {
                var e = n(this).data(r);
                e && e.move(t, i, u)
            }), this
        }, n.fn.scrollToReveal = function() {
            return this.each(function() {
                var i = n(this),
                    u = i.parents(o);
                u.each(function() {
                    var f = n(this),
                        c, u, b, o, l, p, w, a, e, s = 0,
                        h = 0;
                    (o = f.data(r), o) && (b = f[0], u = i.offset(), c = f.offset(), u.top -= c.top + f.parseCSSValue("border-top-width"), u.left -= c.left + f.parseCSSValue("border-left-width"), l = i.outerWidth(), p = i.outerHeight(), e = {
                        top: o.settings.usePadding ? f.parseCSSValue(v) : 0,
                        right: o.settings.usePadding ? f.parseCSSValue(y) : 0,
                        bottom: o.settings.usePadding ? f.parseCSSValue(it) : 0,
                        left: o.settings.usePadding ? f.parseCSSValue(rt) : 0
                    }, w = f.innerWidth(), a = f.innerHeight(), u.left < e.left ? s = e.left - u.left : u.left + l > w - e.right && (s = w - e.right - u.left - l), u.top < e.top ? h = e.top - u.top : u.top + p > a - e.bottom && (h = a - e.bottom - u.top - p), (s !== 0 || h !== 0) && o.move(s, h))
                })
            }), this
        }, n.fn.customScroll.defaults = {
            horizontal: u,
            vertical: i,
            usePadding: u,
            padding: 4,
            width: 6,
            cornerWidth: 10,
            minScrollerSize: 30,
            minWheelScroll: .25,
            continuousWheelScroll: i,
            continuousTouchScroll: i,
            speed: 48,
            animate: u,
            showOnHover: i,
            autoHide: i
        }
    }(jQuery, document);