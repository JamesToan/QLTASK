(function(n) {
    var u = "hover",
        r = !1,
        f = "string",
        e = "original-title",
        i = "title",
        t = "tipsy",
        o = "function";

    function s(n, t) {
        return typeof n == o ? n.call(t) : n
    }

    function h(t, i) {
        var r = this;
        r.$element = n(t), r.options = i, r.enabled = !0, r.fixTitle()
    }
    h.prototype = {
        show: function() {
            var i = this,
                h, u;
            if (n(".tipsy").remove(), h = i.getTitle(), h && i.enabled) {
                u = i.tip(), u.find(".tipsy-inner")[i.options.html ? "html" : "text"](h), u[0].className = t, u.remove().css({
                    top: 0,
                    left: 0,
                    visibility: "hidden",
                    display: "block"
                }).prependTo(document.body);
                var r = n.extend({}, i.$element.offset(), {
                        width: i.$element[0].offsetWidth,
                        height: i.$element[0].offsetHeight
                    }),
                    o = u[0].offsetWidth,
                    c = u[0].offsetHeight,
                    e = s(i.options.gravity, i.$element[0]),
                    f;
                switch (e.charAt(0)) {
                    case "n":
                        f = {
                            top: r.top + r.height + i.options.offset,
                            left: r.left + r.width / 2 - o / 2
                        };
                        break;
                    case "s":
                        f = {
                            top: r.top - c - i.options.offset,
                            left: r.left + r.width / 2 - o / 2
                        };
                        break;
                    case "e":
                        f = {
                            top: r.top + r.height / 2 - c / 2,
                            left: r.left - o - i.options.offset
                        };
                        break;
                    case "w":
                        f = {
                            top: r.top + r.height / 2 - c / 2,
                            left: r.left + r.width + i.options.offset
                        }
                }
                e.length == 2 && (f.left = e.charAt(1) == "w" ? r.left + r.width / 2 - 15 : r.left + r.width / 2 - o + 15), u.css(f).addClass("tipsy-" + e), u.find(".tipsy-arrow")[0].className = "tipsy-arrow tipsy-arrow-" + e.charAt(0), i.options.className && u.addClass(s(i.options.className, i.$element[0])), i.options.fade ? u.stop().css({
                    opacity: 0,
                    display: "block",
                    visibility: "visible"
                }).animate({
                    opacity: i.options.opacity
                }) : u.css({
                    visibility: "visible",
                    opacity: i.options.opacity
                })
            }
        },
        hide: function() {
            this.options.fade ? this.tip().stop().fadeOut(function() {
                n(this).remove()
            }) : this.tip().remove()
        },
        fixTitle: function() {
            var n = this.$element;
            (n.attr(i) || typeof n.attr(e) != f) && n.attr(e, n.attr(i) || "").removeAttr(i)
        },
        getTitle: function() {
            var r = this,
                u = r.$element,
                n = r.options,
                t;
            return r.fixTitle(), n = r.options, typeof n.title == f ? t = u.attr(n.title == i ? e : n.title) : typeof n.title == o && (t = n.title.call(u[0])), t = ("" + t).replace(/(^\s*|\s*$)/, ""), t || n.fallback
        },
        tip: function() {
            return this.$tip || (this.$tip = n('<div class="tipsy"></div>').html('<div class="tipsy-arrow"></div><div class="tipsy-inner"></div>')), this.$tip
        },
        validate: function() {
            var n = this;
            n.$element[0].parentNode || (n.hide(), n.$element = null, n.options = null)
        },
        enable: function() {
            this.enabled = !0
        },
        disable: function() {
            this.enabled = r
        },
        toggleEnabled: function() {
            this.enabled = !this.enabled
        }
    }, n.fn.tipsy = function(i) {
        var r = this;

        function o(r) {
            var u = n.data(r, t);
            return u || (u = new h(r, n.fn.tipsy.elementOptions(r, i)), n.data(r, t, u)), u
        }

        function a() {
            var n = o(this);
            n.hoverState = "in", i.delayIn == 0 ? n.show() : (n.fixTitle(), setTimeout(function() {
                n.hoverState == "in" && n.show()
            }, i.delayIn))
        }

        function l() {
            var n = o(this);
            n.hoverState = "out", i.delayOut == 0 ? n.hide() : setTimeout(function() {
                n.hoverState == "out" && n.hide()
            }, i.delayOut)
        }
        var e;
        if (i === !0) return r.data(t);
        if (typeof i == f) return e = r.data(t), e && e[i](), r;
        if (i = n.extend({}, n.fn.tipsy.defaults, i), i.live || r.each(function() {
                o(this)
            }), i.trigger != "manual") {
            var s = i.live ? "live" : "bind",
                v = i.trigger == u ? "mouseenter" : "focus",
                c = i.trigger == u ? "mouseleave" : "blur";
            r[s](v, a)[s](c, l)
        }
        return r
    }, n.fn.tipsy.defaults = {
        className: null,
        delayIn: 0,
        delayOut: 0,
        fade: r,
        fallback: "",
        gravity: "n",
        html: r,
        live: r,
        offset: 0,
        opacity: 1,
        title: i,
        trigger: u
    }, n.fn.tipsy.elementOptions = function(t, i) {
        return n.metadata ? n.extend({}, i, n(t).metadata()) : i
    }, n.fn.tipsy.autoNS = function() {
        return n(this).offset().top > n(document).scrollTop() + n(window).height() / 2 ? "s" : "n"
    }, n.fn.tipsy.autoWE = function() {
        return n(this).offset().left > n(document).scrollLeft() + n(window).width() / 2 ? "e" : "w"
    }, n.fn.tipsy.autoBounds = function(t, i) {
        return function() {
            var u = {
                    ns: i[0],
                    ew: i.length > 1 ? i[1] : r
                },
                e = n(document).scrollTop() + t,
                o = n(document).scrollLeft() + t,
                f = n(this);
            return f.offset().top < e && (u.ns = "n"), f.offset().left < o && (u.ew = "w"), n(window).width() + n(document).scrollLeft() - f.offset().left < t && (u.ew = "e"), n(window).height() + n(document).scrollTop() - f.offset().top < t && (u.ns = "s"), u.ns + (u.ew ? u.ew : "")
        }
    }, n.fn.tipsy.autoHide = function() {
        return n(this).tip().stop().fadeOut(function() {
            n(this).remove()
        })
    }
})(jQuery);