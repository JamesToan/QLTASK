(function(n) {
    var gt = "mouseover",
        dt = "mouseout",
        kt = '<div class="',
        ii = '<v:image style="behavior:url(#default#VML);"></v:image>',
        l = "px; ",
        ti = "px; height:",
        ni = 'style="width:',
        yt = '" width="',
        vt = '<canvas height="',
        d = "px; width:",
        st = "unfocus",
        ot = "hidden",
        ut = "destroy",
        ht = "mouseover.qtip",
        y = ".qtip",
        hi = "mousemove.qtip",
        w = "disable",
        et = "div.qtip[qtip]",
        rt = "focus",
        ft = ":visible",
        it = "2d",
        h = "<canvas>",
        o = "string",
        v = "number",
        oi = "mousemove",
        p = "rel",
        nt = "undefined",
        f = "qtip",
        a = "mouse",
        lt = "hide",
        g = "fade",
        c = "api",
        ei = "div.qtip",
        u = "object",
        r = !0,
        k = "none",
        tt = "display",
        ct = "show",
        s = "static",
        e = null,
        t = !1;

    function wi(i, l, d) {
        var at = "loadContent",
            yt = ".qtip-betweenCorners",
            vt = "function",
            st = this;
        st.id = d, st.options = l, st.status = {
            animated: t,
            rendered: t,
            disabled: t,
            focused: t
        }, st.elements = {
            target: i.addClass(st.options.style.classes.target),
            tooltip: e,
            wrapper: e,
            content: e,
            contentWrapper: e,
            title: e,
            button: e,
            tip: e,
            bgiframe: e
        }, st.cache = {
            mouse: {},
            position: {},
            toggle: 0
        }, st.timers = {}, n.extend(st, st.options.api, {
            show: function(i) {
                function f() {
                    st.options.position.type !== s && st.focus(), st.onShow.call(st, i), n.browser.msie && st.elements.tooltip.get(0).style.removeAttribute("filter")
                }
                var h, o;
                if (!st.status.rendered) return n.fn.qtip.log.error.call(st, 2, n.fn.qtip.constants.TOOLTIP_NOT_RENDERED, ct);
                if (st.elements.tooltip.css(tt) !== k || (st.elements.tooltip.stop(r, t), h = st.beforeShow.call(st, i), h === t)) return st;
                if (st.cache.toggle = 1, st.options.position.type !== s && st.updatePosition(i, st.options.show.effect.length > 0), typeof st.options.show.solo == u ? o = n(st.options.show.solo) : st.options.show.solo === r && (o = n(ei).not(st.elements.tooltip)), o && o.each(function() {
                        n(this).qtip(c).status.rendered === r && n(this).qtip(c).hide()
                    }), typeof st.options.show.effect.type == vt) st.options.show.effect.type.call(st.elements.tooltip, st.options.show.effect.length), st.elements.tooltip.queue(function() {
                    f(), n(this).dequeue()
                });
                else {
                    switch (st.options.show.effect.type.toLowerCase()) {
                        case g:
                            st.elements.tooltip.fadeIn(st.options.show.effect.length, f);
                            break;
                        case "slide":
                            st.elements.tooltip.slideDown(st.options.show.effect.length, function() {
                                f(), st.options.position.type !== s && st.updatePosition(i, r)
                            });
                            break;
                        case "grow":
                            st.elements.tooltip.show(st.options.show.effect.length, f);
                            break;
                        default:
                            st.elements.tooltip.show(e, f)
                    }
                    st.elements.tooltip.addClass(st.options.style.classes.active)
                }
                return n.fn.qtip.log.error.call(st, 1, n.fn.qtip.constants.EVENT_SHOWN, ct)
            },
            hide: function(i) {
                function u() {
                    st.onHide.call(st, i)
                }
                var f;
                if (st.status.rendered) {
                    if (st.elements.tooltip.css(tt) === k) return st
                } else return n.fn.qtip.log.error.call(st, 2, n.fn.qtip.constants.TOOLTIP_NOT_RENDERED, lt);
                if (clearTimeout(st.timers.show), st.elements.tooltip.stop(r, t), f = st.beforeHide.call(st, i), f === t) return st;
                if (st.cache.toggle = 0, typeof st.options.hide.effect.type == vt) st.options.hide.effect.type.call(st.elements.tooltip, st.options.hide.effect.length), st.elements.tooltip.queue(function() {
                    u(), n(this).dequeue()
                });
                else {
                    switch (st.options.hide.effect.type.toLowerCase()) {
                        case g:
                            st.elements.tooltip.fadeOut(st.options.hide.effect.length, u);
                            break;
                        case "slide":
                            st.elements.tooltip.slideUp(st.options.hide.effect.length, u);
                            break;
                        case "grow":
                            st.elements.tooltip.hide(st.options.hide.effect.length, u);
                            break;
                        default:
                            st.elements.tooltip.hide(e, u)
                    }
                    st.elements.tooltip.removeClass(st.options.style.classes.active)
                }
                return n.fn.qtip.log.error.call(st, 1, n.fn.qtip.constants.EVENT_HIDDEN, lt)
            },
            updatePosition: function(i, u) {
                var d = "updatePosition",
                    v, e, l, h, tt, y, o, b, rt, ut, k, w, g, it;
                if (st.status.rendered) {
                    if (st.options.position.type == s) return n.fn.qtip.log.error.call(st, 1, n.fn.qtip.constants.CANNOT_POSITION_STATIC, d)
                } else return n.fn.qtip.log.error.call(st, 2, n.fn.qtip.constants.TOOLTIP_NOT_RENDERED, d);
                if (e = {
                        position: {
                            left: 0,
                            top: 0
                        },
                        dimensions: {
                            height: 0,
                            width: 0
                        },
                        corner: st.options.position.corner.target
                    }, l = {
                        position: st.getPosition(),
                        dimensions: st.getDimensions(),
                        corner: st.options.position.corner.tooltip
                    }, st.options.position.target !== a) {
                    if (st.options.position.target.get(0).nodeName.toLowerCase() == "area") {
                        for (h = st.options.position.target.attr("coords").split(","), v = 0; v < h.length; v++) h[v] = parseInt(h[v]);
                        tt = st.options.position.target.parent("map").attr("name"), y = n('img[usemap="#' + tt + '"]:first').offset(), e.position = {
                            left: Math.floor(y.left + h[0]),
                            top: Math.floor(y.top + h[1])
                        };
                        switch (st.options.position.target.attr("shape").toLowerCase()) {
                            case "rect":
                                e.dimensions = {
                                    width: Math.ceil(Math.abs(h[2] - h[0])),
                                    height: Math.ceil(Math.abs(h[3] - h[1]))
                                };
                                break;
                            case "circle":
                                e.dimensions = {
                                    width: h[2] + 1,
                                    height: h[2] + 1
                                };
                                break;
                            case "poly":
                                for (e.dimensions = {
                                        width: h[0],
                                        height: h[1]
                                    }, v = 0; v < h.length; v++) v % 2 == 0 ? (h[v] > e.dimensions.width && (e.dimensions.width = h[v]), h[v] < h[0] && (e.position.left = Math.floor(y.left + h[v]))) : (h[v] > e.dimensions.height && (e.dimensions.height = h[v]), h[v] < h[1] && (e.position.top = Math.floor(y.top + h[v])));
                                e.dimensions.width = e.dimensions.width - (e.position.left - y.left), e.dimensions.height = e.dimensions.height - (e.position.top - y.top);
                                break;
                            default:
                                return n.fn.qtip.log.error.call(st, 4, n.fn.qtip.constants.INVALID_AREA_SHAPE, d)
                        }
                        e.dimensions.width -= 2, e.dimensions.height -= 2
                    } else st.options.position.target.add(document.body).length === 1 ? (e.position = {
                        left: n(document).scrollLeft(),
                        top: n(document).scrollTop()
                    }, e.dimensions = {
                        height: n(window).height(),
                        width: n(window).width()
                    }) : (e.position = typeof st.options.position.target.attr(f) != nt ? st.options.position.target.qtip(c).cache.position : st.options.position.target.offset(), e.dimensions = {
                        height: st.options.position.target.outerHeight(),
                        width: st.options.position.target.outerWidth()
                    });
                    o = n.extend({}, e.position), e.corner.search(/right/i) !== -1 && (o.left += e.dimensions.width), e.corner.search(/bottom/i) !== -1 && (o.top += e.dimensions.height), e.corner.search(/((top|bottom)Middle)|center/) !== -1 && (o.left += e.dimensions.width / 2), e.corner.search(/((left|right)Middle)|center/) !== -1 && (o.top += e.dimensions.height / 2)
                } else e.position = o = {
                    left: st.cache.mouse.x,
                    top: st.cache.mouse.y
                }, e.dimensions = {
                    height: 1,
                    width: 1
                };
                if (l.corner.search(/right/i) !== -1 && (o.left -= l.dimensions.width), l.corner.search(/bottom/i) !== -1 && (o.top -= l.dimensions.height), l.corner.search(/((top|bottom)Middle)|center/) !== -1 && (o.left -= l.dimensions.width / 2), l.corner.search(/((left|right)Middle)|center/) !== -1 && (o.top -= l.dimensions.height / 2), b = n.browser.msie ? 1 : 0, rt = n.browser.msie && parseInt(n.browser.version.charAt(0)) === 6 ? 1 : 0, st.options.style.border.radius > 0 && (l.corner.search(/Left/) !== -1 ? o.left -= st.options.style.border.radius : l.corner.search(/Right/) !== -1 && (o.left += st.options.style.border.radius), l.corner.search(/Top/) !== -1 ? o.top -= st.options.style.border.radius : l.corner.search(/Bottom/) !== -1 && (o.top += st.options.style.border.radius)), b && (l.corner.search(/top/) !== -1 ? o.top -= b : l.corner.search(/bottom/) !== -1 && (o.top += b), l.corner.search(/left/) !== -1 ? o.left -= b : l.corner.search(/right/) !== -1 && (o.left += b), l.corner.search(/leftMiddle|rightMiddle/) !== -1 && (o.top -= 1)), st.options.position.adjust.screen === r && (o = ai.call(st, o, e, l)), st.options.position.target === a && st.options.position.adjust.mouse === r && (k = st.options.position.adjust.screen === r && st.elements.tip ? st.elements.tip.attr(p) : st.options.position.corner.tooltip, o.left += k.search(/right/i) !== -1 ? -6 : 6, o.top += k.search(/bottom/i) !== -1 ? -6 : 6), !st.elements.bgiframe && n.browser.msie && parseInt(n.browser.version.charAt(0)) == 6 && n("select, object").each(function() {
                        w = n(this).offset(), w.bottom = w.top + n(this).height(), w.right = w.left + n(this).width(), o.top + l.dimensions.height >= w.top && o.left + l.dimensions.width >= w.left && li.call(st)
                    }), o.left += st.options.position.adjust.x, o.top += st.options.position.adjust.y, g = st.getPosition(), o.left != g.left || o.top != g.top) {
                    if (it = st.beforePositionUpdate.call(st, i), it === t) return st;
                    st.cache.position = o, u === r ? (st.status.animated = r, st.elements.tooltip.animate(o, 200, "swing", function() {
                        st.status.animated = t
                    })) : st.elements.tooltip.css(o), st.onPositionUpdate.call(st, i), typeof i != nt && i.type && i.type !== oi && n.fn.qtip.log.error.call(st, 1, n.fn.qtip.constants.EVENT_POSITION_UPDATED, d)
                }
                return st
            },
            updateWidth: function(t) {
                var i = "updateWidth",
                    r;
                if (st.status.rendered) {
                    if (t && typeof t != v) return n.fn.qtip.log.error.call(st, 2, "newWidth must be of type number", i)
                } else return n.fn.qtip.log.error.call(st, 2, n.fn.qtip.constants.TOOLTIP_NOT_RENDERED, i);
                return r = st.elements.contentWrapper.siblings().add(st.elements.tip).add(st.elements.button), t || (typeof st.options.style.width.value == v ? t = st.options.style.width.value : (st.elements.tooltip.css({
                    width: "auto"
                }), r.hide(), n.browser.msie && st.elements.wrapper.add(st.elements.contentWrapper.children()).css({
                    zoom: "normal"
                }), t = st.getDimensions().width + 1, st.options.style.width.value || (t > st.options.style.width.max && (t = st.options.style.width.max), t < st.options.style.width.min && (t = st.options.style.width.min)))), t % 2 != 0 && (t -= 1), st.elements.tooltip.width(t), r.show(), st.options.style.border.radius && st.elements.tooltip.find(yt).each(function() {
                    n(this).width(t - st.options.style.border.radius * 2)
                }), n.browser.msie && (st.elements.wrapper.add(st.elements.contentWrapper.children()).css({
                    zoom: "1"
                }), st.elements.wrapper.width(t), st.elements.bgiframe && st.elements.bgiframe.width(t).height(st.getDimensions.height)), n.fn.qtip.log.error.call(st, 1, n.fn.qtip.constants.EVENT_WIDTH_UPDATED, i)
            },
            updateStyle: function(i) {
                var l = "fillcolor",
                    c = "div[rel]:first",
                    s = "updateStyle",
                    u, v, e, f, a;
                if (st.status.rendered) {
                    if (typeof i != o || !n.fn.qtip.styles[i]) return n.fn.qtip.log.error.call(st, 2, n.fn.qtip.constants.STYLE_NOT_DEFINED, s)
                } else return n.fn.qtip.log.error.call(st, 2, n.fn.qtip.constants.TOOLTIP_NOT_RENDERED, s);
                return st.options.style = ui.call(st, n.fn.qtip.styles[i], st.options.user.style), st.elements.content.css(b(st.options.style)), st.options.content.title.text !== t && st.elements.title.css(b(st.options.style.title, r)), st.elements.contentWrapper.css({
                    borderColor: st.options.style.border.color
                }), st.options.style.tip.corner !== t && (n(h).get(0).getContext ? (u = st.elements.tooltip.find(".qtip-tip canvas:first"), e = u.get(0).getContext(it), e.clearRect(0, 0, 300, 300), f = u.parent(c).attr(p), a = ri(f, st.options.style.tip.size.width, st.options.style.tip.size.height), pt.call(st, u, a, st.options.style.tip.color || st.options.style.border.color)) : n.browser.msie && (u = st.elements.tooltip.find('.qtip-tip [nodeName="shape"]'), u.attr(l, st.options.style.tip.color || st.options.style.border.color))), st.options.style.border.radius > 0 && (st.elements.tooltip.find(yt).css({
                    backgroundColor: st.options.style.border.color
                }), n(h).get(0).getContext ? (v = si(st.options.style.border.radius), st.elements.tooltip.find(".qtip-wrapper canvas").each(function() {
                    e = n(this).get(0).getContext(it), e.clearRect(0, 0, 300, 300), f = n(this).parent(c).attr(p), bt.call(st, n(this), v[f], st.options.style.border.radius, st.options.style.border.color)
                })) : n.browser.msie && st.elements.tooltip.find('.qtip-wrapper [nodeName="arc"]').each(function() {
                    n(this).attr(l, st.options.style.border.color)
                })), n.fn.qtip.log.error.call(st, 1, n.fn.qtip.constants.EVENT_STYLE_UPDATED, s)
            },
            updateContent: function(i, u) {
                var h = "updateContent";

                function c() {
                    st.updateWidth(), u !== t && (st.options.position.type !== s && st.updatePosition(st.elements.tooltip.is(ft), r), st.options.style.tip.corner !== t && ci.call(st))
                }
                var e, f, l;
                if (st.status.rendered) {
                    if (!i) return n.fn.qtip.log.error.call(st, 2, n.fn.qtip.constants.NO_CONTENT_PROVIDED, h)
                } else return n.fn.qtip.log.error.call(st, 2, n.fn.qtip.constants.TOOLTIP_NOT_RENDERED, h);
                if (e = st.beforeContentUpdate.call(st, i), typeof e == o) i = e;
                else if (e === t) return;
                return n.browser.msie && st.elements.contentWrapper.children().css({
                    zoom: "normal"
                }), i.jquery && i.length > 0 ? i.clone(r).appendTo(st.elements.content).show() : st.elements.content.html(i), f = st.elements.content.find("img[complete=false]"), f.length > 0 ? (l = 0, f.each(function() {
                    n('<img src="' + n(this).attr("src") + '" />').load(function() {
                        ++l == f.length && c()
                    })
                })) : c(), st.onContentUpdate.call(st), n.fn.qtip.log.error.call(st, 1, n.fn.qtip.constants.EVENT_CONTENT_UPDATED, at)
            },
            loadContent: function(i, r, u) {
                function f(t) {
                    st.onContentLoad.call(st), n.fn.qtip.log.error.call(st, 1, n.fn.qtip.constants.EVENT_CONTENT_LOADED, at), st.updateContent(t)
                }
                var e;
                return st.status.rendered ? (e = st.beforeContentLoad.call(st), e === t) ? st : (u == "post" ? n.post(i, r, f) : n.get(i, r, f), st) : n.fn.qtip.log.error.call(st, 2, n.fn.qtip.constants.TOOLTIP_NOT_RENDERED, at)
            },
            updateTitle: function(i) {
                var u = "updateTitle";
                if (st.status.rendered) {
                    if (!i) return n.fn.qtip.log.error.call(st, 2, n.fn.qtip.constants.NO_CONTENT_PROVIDED, u)
                } else return n.fn.qtip.log.error.call(st, 2, n.fn.qtip.constants.TOOLTIP_NOT_RENDERED, u);
                return (returned = st.beforeTitleUpdate.call(st), returned === t) ? st : (st.elements.button && (st.elements.button = st.elements.button.clone(r)), st.elements.title.html(i), st.elements.button && st.elements.title.prepend(st.elements.button), st.onTitleUpdate.call(st), n.fn.qtip.log.error.call(st, 1, n.fn.qtip.constants.EVENT_TITLE_UPDATED, u))
            },
            focus: function(i) {
                var f = "z-index",
                    o, e, u, h;
                if (st.status.rendered) {
                    if (st.options.position.type == s) return n.fn.qtip.log.error.call(st, 1, n.fn.qtip.constants.CANNOT_FOCUS_STATIC, rt)
                } else return n.fn.qtip.log.error.call(st, 2, n.fn.qtip.constants.TOOLTIP_NOT_RENDERED, rt);
                if (o = parseInt(st.elements.tooltip.css(f)), e = 6e3 + n(et).length - 1, !st.status.focused && o !== e) {
                    if (h = st.beforeFocus.call(st, i), h === t) return st;
                    n(et).not(st.elements.tooltip).each(function() {
                        var i = this;
                        n(i).qtip(c).status.rendered === r && (u = parseInt(n(i).css(f)), typeof u == v && u > -1 && n(i).css({
                            zIndex: parseInt(n(i).css(f)) - 1
                        }), n(i).qtip(c).status.focused = t)
                    }), st.elements.tooltip.css({
                        zIndex: e
                    }), st.status.focused = r, st.onFocus.call(st, i), n.fn.qtip.log.error.call(st, 1, n.fn.qtip.constants.EVENT_FOCUSED, rt)
                }
                return st
            },
            disable: function(i) {
                return st.status.rendered ? (i ? st.status.disabled ? n.fn.qtip.log.error.call(st, 1, n.fn.qtip.constants.TOOLTIP_ALREADY_DISABLED, w) : (st.status.disabled = r, n.fn.qtip.log.error.call(st, 1, n.fn.qtip.constants.EVENT_DISABLED, w)) : st.status.disabled ? (st.status.disabled = t, n.fn.qtip.log.error.call(st, 1, n.fn.qtip.constants.EVENT_ENABLED, w)) : n.fn.qtip.log.error.call(st, 1, n.fn.qtip.constants.TOOLTIP_ALREADY_ENABLED, w), st) : n.fn.qtip.log.error.call(st, 2, n.fn.qtip.constants.TOOLTIP_NOT_RENDERED, w)
            },
            destroy: function() {
                var r, e, i;
                if (e = st.beforeDestroy.call(st), e === t) return st;
                if (st.status.rendered ? (st.options.show.when.target.unbind(hi, st.updatePosition), st.options.show.when.target.unbind("mouseout.qtip", st.hide), st.options.show.when.target.unbind(st.options.show.when.event + y), st.options.hide.when.target.unbind(st.options.hide.when.event + y), st.elements.tooltip.unbind(st.options.hide.when.event + y), st.elements.tooltip.unbind(ht, st.focus), st.elements.tooltip.remove()) : st.options.show.when.target.unbind(st.options.show.when.event + ".qtip-create"), typeof st.elements.target.data(f) == u && (i = st.elements.target.data(f).interfaces, typeof i == u && i.length > 0))
                    for (r = 0; r < i.length - 1; r++) i[r].id == st.id && i.splice(r, 1);
                return delete n.fn.qtip.interfaces[st.id], typeof i == u && i.length > 0 ? st.elements.target.data(f).current = i.length - 1 : st.elements.target.removeData(f), st.onDestroy.call(st), n.fn.qtip.log.error.call(st, 1, n.fn.qtip.constants.EVENT_DESTROYED, ut), st.elements.target
            },
            getPosition: function() {
                var i, u;
                return st.status.rendered ? (i = st.elements.tooltip.css(tt) !== k ? t : r, i && st.elements.tooltip.css({
                    visiblity: ot
                }).show(), u = st.elements.tooltip.offset(), i && st.elements.tooltip.css({
                    visiblity: "visible"
                }).hide(), u) : n.fn.qtip.log.error.call(st, 2, n.fn.qtip.constants.TOOLTIP_NOT_RENDERED, "getPosition")
            },
            getDimensions: function() {
                var i, u;
                return st.status.rendered ? (i = st.elements.tooltip.is(ft) ? t : r, i && st.elements.tooltip.css({
                    visiblity: ot
                }).show(), u = {
                    height: st.elements.tooltip.outerHeight(),
                    width: st.elements.tooltip.outerWidth()
                }, i && st.elements.tooltip.css({
                    visiblity: "visible"
                }).hide(), u) : n.fn.qtip.log.error.call(st, 2, n.fn.qtip.constants.TOOLTIP_NOT_RENDERED, "getDimensions")
            }
        })
    }

    function at() {
        var e = "alt",
            s = "title",
            c = "render",
            a = "px solid ",
            l = "div:first",
            i, d, u, p, w, y, k;
        i = this, i.beforeRender.call(i), i.status.rendered = r, i.elements.tooltip = '<div qtip="' + i.id + '" class="qtip ' + (i.options.style.classes.tooltip || i.options.style) + '"style="display:none; -moz-border-radius:0; -webkit-border-radius:0; border-radius:0;position:' + i.options.position.type + ';">  <div class="qtip-wrapper" style="position:relative; overflow:hidden; text-align:left;">    <div class="qtip-contentWrapper" style="overflow:hidden;">       <div class="qtip-content ' + i.options.style.classes.content + '"></div></div></div></div>', i.elements.tooltip = n(i.elements.tooltip), i.elements.tooltip.appendTo(i.options.position.container), i.elements.tooltip.data(f, {
            current: 0,
            interfaces: [i]
        }), i.elements.wrapper = i.elements.tooltip.children(l), i.elements.contentWrapper = i.elements.wrapper.children(l).css({
            background: i.options.style.background
        }), i.elements.content = i.elements.contentWrapper.children(l).css(b(i.options.style)), n.browser.msie && i.elements.wrapper.add(i.elements.content).css({
            zoom: 1
        }), i.options.hide.when.event == st && i.elements.tooltip.attr(st, r), typeof i.options.style.width.value == v && i.updateWidth(), n(h).get(0).getContext || n.browser.msie ? (i.options.style.border.radius > 0 ? pi.call(i) : i.elements.contentWrapper.css({
            border: i.options.style.border.width + a + i.options.style.border.color
        }), i.options.style.tip.corner !== t && wt.call(i)) : (i.elements.contentWrapper.css({
            border: i.options.style.border.width + a + i.options.style.border.color
        }), i.options.style.border.radius = 0, i.options.style.tip.corner = t, n.fn.qtip.log.error.call(i, 2, n.fn.qtip.constants.CANVAS_VML_NOT_SUPPORTED, c)), typeof i.options.content.text == o && i.options.content.text.length > 0 || i.options.content.text.jquery && i.options.content.text.length > 0 ? u = i.options.content.text : typeof i.elements.target.attr(s) == o && i.elements.target.attr(s).length > 0 ? (u = i.elements.target.attr(s).replace("\\n", "<br />"), i.elements.target.attr(s, "")) : typeof i.elements.target.attr(e) == o && i.elements.target.attr(e).length > 0 ? (u = i.elements.target.attr(e).replace("\\n", "<br />"), i.elements.target.attr(e, "")) : (u = " ", n.fn.qtip.log.error.call(i, 1, n.fn.qtip.constants.NO_VALID_CONTENT, c)), i.options.content.title.text !== t && vi.call(i), i.updateContent(u), yi.call(i), i.options.show.ready === r && i.show(), i.options.content.url !== t && (p = i.options.content.url, w = i.options.content.data, y = i.options.content.method || "get", i.loadContent(p, w, y)), i.onRender.call(i), n.fn.qtip.log.error.call(i, 1, n.fn.qtip.constants.EVENT_RENDERED, c)
    }

    function pi() {
        var w = 'px; line-height:0.1px; font-size:1px; padding:0;">',
            c = "margin-left:",
            i, r, o, t, e, f, u, a, k, s, b, nt, g, v, y;
        i = this, i.elements.wrapper.find(".qtip-borderBottom, .qtip-borderTop").remove(), o = i.options.style.border.width, t = i.options.style.border.radius, e = i.options.style.border.color || i.options.style.tip.color, f = si(t), u = {};
        for (r in f) u[r] = '<div rel="' + r + '" style="' + (r.search(/Left/) !== -1 ? "left" : "right") + ":0; position:absolute; height:" + t + d + t + 'px; overflow:hidden; line-height:0.1px; font-size:1px">', n(h).get(0).getContext ? u[r] += vt + t + yt + t + '" style="vertical-align: top"></canvas>' : n.browser.msie && (a = t * 2 + 3, u[r] += '<v:arc stroked="false" fillcolor="' + e + '" startangle="' + f[r][0] + '" endangle="' + f[r][1] + '" ' + ni + a + ti + a + "px; margin-top:" + (r.search(/bottom/) !== -1 ? -2 : -1) + l + c + (r.search(/Right/) !== -1 ? f[r][2] - 3.5 : -1) + l + 'vertical-align:top; display:inline-block; behavior:url(#default#VML)"></v:arc>'), u[r] += "</div>";
        k = i.getDimensions().width - Math.max(o, t) * 2, s = '<div class="qtip-betweenCorners" style="height:' + t + d + k + l + "overflow:hidden; background-color:" + e + '; line-height:0.1px; font-size:1px;">', b = '<div class="qtip-borderTop" dir="ltr" style="height:' + t + l + c + t + w + u.topLeft + u.topRight + s, i.elements.wrapper.prepend(b), nt = '<div class="qtip-borderBottom" dir="ltr" style="height:' + t + l + c + t + w + u.bottomLeft + u.bottomRight + s, i.elements.wrapper.append(nt), n(h).get(0).getContext ? i.elements.wrapper.find("canvas").each(function() {
            g = f[n(this).parent("[rel]:first").attr(p)], bt.call(i, n(this), g, t, e)
        }) : n.browser.msie && i.elements.tooltip.append(ii), v = Math.max(t, t + (o - t)), y = Math.max(o - t, 0), i.elements.contentWrapper.css({
            border: "0px solid " + e,
            borderWidth: y + "px " + v + "px"
        })
    }

    function bt(n, i, r, u) {
        var f = n.get(0).getContext(it);
        f.fillStyle = u, f.beginPath(), f.arc(i[0], i[1], r, 0, Math.PI * 2, t), f.fill()
    }

    function wt(i) {
        var r, o, u, s, f;
        (r = this, r.elements.tip !== e && r.elements.tip.remove(), o = r.options.style.tip.color || r.options.style.border.color, r.options.style.tip.corner !== t) && (i || (i = r.options.style.tip.corner), u = ri(i, r.options.style.tip.size.width, r.options.style.tip.size.height), r.elements.tip = kt + r.options.style.classes.tip + '" dir="ltr" rel="' + i + '" style="position:absolute; height:' + r.options.style.tip.size.height + d + r.options.style.tip.size.width + l + 'margin:0 auto; line-height:0.1px; font-size:1px;">', n(h).get(0).getContext ? r.elements.tip += vt + r.options.style.tip.size.height + yt + r.options.style.tip.size.width + '"></canvas>' : n.browser.msie && (s = r.options.style.tip.size.width + "," + r.options.style.tip.size.height, f = "m" + u[0][0] + "," + u[0][1], f += " l" + u[1][0] + "," + u[1][1], f += " " + u[2][0] + "," + u[2][1], f += " xe", r.elements.tip += '<v:shape fillcolor="' + o + '" stroked="false" filled="true" path="' + f + '" coordsize="' + s + '" ' + ni + r.options.style.tip.size.width + ti + r.options.style.tip.size.height + l + "line-height:0.1px; display:inline-block; behavior:url(#default#VML); vertical-align:" + (i.search(/top/) !== -1 ? "bottom" : "top") + '"></v:shape>', r.elements.tip += ii, r.elements.contentWrapper.css("position", "relative")), r.elements.tooltip.prepend(r.elements.tip + "</div>"), r.elements.tip = r.elements.tooltip.find("." + r.options.style.classes.tip).eq(0), n(h).get(0).getContext && pt.call(r, r.elements.tip.find("canvas:first"), u, o), i.search(/top/) !== -1 && n.browser.msie && parseInt(n.browser.version.charAt(0)) === 6 && r.elements.tip.css({
            marginTop: -4
        }), ci.call(r, i))
    }

    function pt(n, t, i) {
        var r = n.get(0).getContext(it);
        r.fillStyle = i, r.beginPath(), r.moveTo(t[0][0], t[0][1]), r.lineTo(t[1][0], t[1][1]), r.lineTo(t[2][0], t[2][1]), r.fill()
    }

    function ci(i) {
        var o = "margin-top",
            r, u, e, s, f;
        (r = this, r.options.style.tip.corner !== t && r.elements.tip) && (i || (i = r.elements.tip.attr(p)), u = positionAdjust = n.browser.msie ? 1 : 0, r.elements.tip.css(i.match(/left|right|top|bottom/)[0], 0), i.search(/top|bottom/) !== -1 ? (n.browser.msie && (positionAdjust = parseInt(n.browser.version.charAt(0)) === 6 ? i.search(/top/) !== -1 ? -3 : 1 : i.search(/top/) !== -1 ? 1 : 2), i.search(/Middle/) !== -1 ? r.elements.tip.css({
            left: "50%",
            marginLeft: -(r.options.style.tip.size.width / 2)
        }) : i.search(/Left/) !== -1 ? r.elements.tip.css({
            left: r.options.style.border.radius - u
        }) : i.search(/Right/) !== -1 && r.elements.tip.css({
            right: r.options.style.border.radius + u
        }), i.search(/top/) !== -1 ? r.elements.tip.css({
            top: -positionAdjust
        }) : r.elements.tip.css({
            bottom: positionAdjust
        })) : i.search(/left|right/) !== -1 && (n.browser.msie && (positionAdjust = parseInt(n.browser.version.charAt(0)) === 6 ? 1 : i.search(/left/) !== -1 ? 1 : 2), i.search(/Middle/) !== -1 ? r.elements.tip.css({
            top: "50%",
            marginTop: -(r.options.style.tip.size.height / 2)
        }) : i.search(/Top/) !== -1 ? r.elements.tip.css({
            top: r.options.style.border.radius - u
        }) : i.search(/Bottom/) !== -1 && r.elements.tip.css({
            bottom: r.options.style.border.radius + u
        }), i.search(/left/) !== -1 ? r.elements.tip.css({
            left: -positionAdjust
        }) : r.elements.tip.css({
            right: positionAdjust
        })), e = "padding-" + i.match(/left|right|top|bottom/)[0], s = r.options.style.tip.size[e.search(/left|right/) !== -1 ? "width" : "height"], r.elements.tooltip.css("padding", 0), r.elements.tooltip.css(e, s), n.browser.msie && parseInt(n.browser.version.charAt(0)) == 6 && (f = parseInt(r.elements.tip.css(o)) || 0, f += parseInt(r.elements.content.css(o)) || 0, r.elements.tip.css({
            marginTop: f
        })))
    }

    function vi() {
        var i = this;
        i.elements.title !== e && i.elements.title.remove(), i.elements.title = n(kt + i.options.style.classes.title + '">').css(b(i.options.style.title, r)).css({
            zoom: n.browser.msie ? 1 : 0
        }).prependTo(i.elements.contentWrapper), i.options.content.title.text && i.updateTitle.call(i, i.options.content.title.text), i.options.content.title.button !== t && typeof i.options.content.title.button == o && (i.elements.button = n('<a class="' + i.options.style.classes.button + '" style="float:right; position: relative"></a>').css(b(i.options.style.button, r)).html(i.options.content.title.button).prependTo(i.elements.title).click(function(n) {
            i.status.disabled || i.hide(n)
        }))
    }

    function yi() {
        var f = ".qtip-inactive",
            e = "inactive";

        function v(t) {
            i.status.disabled !== r && (i.options.hide.when.event == e && (n(c).each(function() {
                u.bind(this + f, h), i.elements.content.bind(this + f, h)
            }), h()), clearTimeout(i.timers.show), clearTimeout(i.timers.hide), i.timers.show = setTimeout(function() {
                i.show(t)
            }, i.options.show.delay))
        }

        function l(u) {
            if (i.status.disabled !== r) {
                if (i.options.hide.fixed === r && i.options.hide.when.event.search(/mouse(out|leave)/i) !== -1 && n(u.relatedTarget).parents(et).length > 0) return u.stopPropagation(), u.preventDefault(), clearTimeout(i.timers.hide), t;
                clearTimeout(i.timers.show), clearTimeout(i.timers.hide), i.elements.tooltip.stop(r, r), i.timers.hide = setTimeout(function() {
                    i.hide(u)
                }, i.options.hide.delay)
            }
        }
        var i, o, u, c;
        if (i = this, o = i.options.show.when.target, u = i.options.hide.when.target, i.options.hide.fixed && (u = u.add(i.elements.tooltip)), i.options.hide.when.event == e) {
            c = ["click", "dblclick", "mousedown", "mouseup", oi, dt, "mouseenter", "mouseleave", gt];

            function h(t) {
                i.status.disabled !== r && (clearTimeout(i.timers.inactive), i.timers.inactive = setTimeout(function() {
                    n(c).each(function() {
                        u.unbind(this + f), i.elements.content.unbind(this + f)
                    }), i.hide(t)
                }, i.options.hide.delay))
            }
        } else i.options.hide.fixed === r && i.elements.tooltip.bind(ht, function() {
            i.status.disabled !== r && clearTimeout(i.timers.hide)
        });
        i.options.show.when.target.add(i.options.hide.when.target).length === 1 && i.options.show.when.event == i.options.hide.when.event && i.options.hide.when.event !== e || i.options.hide.when.event == st ? (i.cache.toggle = 0, o.bind(i.options.show.when.event + y, function(n) {
            i.cache.toggle == 0 ? v(n) : l(n)
        })) : (o.bind(i.options.show.when.event + y, v), i.options.hide.when.event !== e && u.bind(i.options.hide.when.event + y, l)), i.options.position.type.search(/(fixed|absolute)/) !== -1 && i.elements.tooltip.bind(ht, i.focus), i.options.position.target === a && i.options.position.type !== s && o.bind(hi, function(n) {
            i.cache.mouse = {
                x: n.pageX,
                y: n.pageY
            }, i.status.disabled === t && i.options.position.adjust.mouse === r && i.options.position.type !== s && i.elements.tooltip.css(tt) !== k && i.updatePosition(n)
        })
    }

    function ai(i, r, u) {
        var e, f, h, o, s, c;
        return (e = this, u.corner == "center") ? r.position : (f = n.extend({}, i), o = {
            x: t,
            y: t
        }, s = {
            left: f.left < n.fn.qtip.cache.screen.scroll.left,
            right: f.left + u.dimensions.width + 2 >= n.fn.qtip.cache.screen.width + n.fn.qtip.cache.screen.scroll.left,
            top: f.top < n.fn.qtip.cache.screen.scroll.top,
            bottom: f.top + u.dimensions.height + 2 >= n.fn.qtip.cache.screen.height + n.fn.qtip.cache.screen.scroll.top
        }, h = {
            left: s.left && (u.corner.search(/right/i) != -1 || u.corner.search(/right/i) == -1 && !s.right),
            right: s.right && (u.corner.search(/left/i) != -1 || u.corner.search(/left/i) == -1 && !s.left),
            top: s.top && u.corner.search(/top/i) == -1,
            bottom: s.bottom && u.corner.search(/bottom/i) == -1
        }, h.left ? (f.left = e.options.position.target !== a ? r.position.left + r.dimensions.width : e.cache.mouse.x, o.x = "Left") : h.right && (f.left = e.options.position.target !== a ? r.position.left - u.dimensions.width : e.cache.mouse.x - u.dimensions.width, o.x = "Right"), h.top ? (f.top = e.options.position.target !== a ? r.position.top + r.dimensions.height : e.cache.mouse.y, o.y = "top") : h.bottom && (f.top = e.options.position.target !== a ? r.position.top - u.dimensions.height : e.cache.mouse.y - u.dimensions.height, o.y = "bottom"), f.left < 0 && (f.left = i.left, o.x = t), f.top < 0 && (f.top = i.top, o.y = t), e.options.style.tip.corner !== t && (f.corner = new String(u.corner), o.x !== t && (f.corner = f.corner.replace(/Left|Right|Middle/, o.x)), o.y !== t && (f.corner = f.corner.replace(/top|bottom/, o.y)), f.corner !== e.elements.tip.attr(p) && wt.call(e, f.corner)), f)
    }

    function b(t, i) {
        var f, u;
        f = n.extend(r, {}, t);
        for (u in f) i === r && u.search(/(tip|classes)/i) !== -1 ? delete f[u] : i || u.search(/(width|border|tip|title|classes|user)/i) === -1 || delete f[u];
        return f
    }

    function fi(n) {
        return typeof n.tip != u && (n.tip = {
            corner: n.tip
        }), typeof n.tip.size != u && (n.tip.size = {
            width: n.tip.size,
            height: n.tip.size
        }), typeof n.border != u && (n.border = {
            width: n.border
        }), typeof n.width != u && (n.width = {
            value: n.width
        }), typeof n.width.max == o && (n.width.max = parseInt(n.width.max.replace(/([0-9]+)/i, "$1"))), typeof n.width.min == o && (n.width.min = parseInt(n.width.min.replace(/([0-9]+)/i, "$1"))), typeof n.tip.size.x == v && (n.tip.size.width = n.tip.size.x, delete n.tip.size.x), typeof n.tip.size.y == v && (n.tip.size.height = n.tip.size.y, delete n.tip.size.y), n
    }

    function ui() {
        var s, f, h, u, i, e;
        for (s = this, h = [r, {}], f = 0; f < arguments.length; f++) h.push(arguments[f]);
        for (u = [n.extend.apply(n, h)]; typeof u[0].name == o;) u.unshift(fi(n.fn.qtip.styles[u[0].name]));
        return u.unshift(r, {
            classes: {
                tooltip: "qtip-" + (arguments[0].name || "defaults")
            }
        }, n.fn.qtip.styles.defaults), i = n.extend.apply(n, u), e = n.browser.msie ? 1 : 0, i.tip.size.width += e, i.tip.size.height += e, i.tip.size.width % 2 > 0 && (i.tip.size.width += 1), i.tip.size.height % 2 > 0 && (i.tip.size.height += 1), i.tip.corner === r && (i.tip.corner = s.options.position.corner.tooltip === "center" ? t : s.options.position.corner.tooltip), i
    }

    function ri(n, t, i) {
        var r = {
            bottomRight: [
                [0, 0],
                [t, i],
                [t, 0]
            ],
            bottomLeft: [
                [0, 0],
                [t, 0],
                [0, i]
            ],
            topRight: [
                [0, i],
                [t, 0],
                [t, i]
            ],
            topLeft: [
                [0, 0],
                [0, i],
                [t, i]
            ],
            topMiddle: [
                [0, i],
                [t / 2, 0],
                [t, i]
            ],
            bottomMiddle: [
                [0, 0],
                [t, 0],
                [t / 2, i]
            ],
            rightMiddle: [
                [0, 0],
                [t, i / 2],
                [0, i]
            ],
            leftMiddle: [
                [t, 0],
                [t, i],
                [0, i / 2]
            ]
        };
        return r.leftTop = r.bottomRight, r.rightTop = r.bottomLeft, r.leftBottom = r.topRight, r.rightBottom = r.topLeft, r[n]
    }

    function si(t) {
        var i;
        return n(h).get(0).getContext ? i = {
            topLeft: [t, t],
            topRight: [0, t],
            bottomLeft: [t, 0],
            bottomRight: [0, 0]
        } : n.browser.msie && (i = {
            topLeft: [-90, 90, 0],
            topRight: [-90, 90, -t],
            bottomLeft: [90, 270, 0],
            bottomRight: [90, 270, -t]
        }), i
    }

    function li() {
        var n, i, t;
        n = this, t = n.getDimensions(), i = '<iframe class="qtip-bgiframe" frameborder="0" tabindex="-1" src="javascript:false" style="display:block; position:absolute; z-index:-1; filter:alpha(opacity=\'0\'); border: 1px solid red; height:' + t.height + d + t.width + 'px" />', n.elements.bgiframe = n.elements.wrapper.prepend(i).children(".qtip-bgiframe:first")
    }
    n.fn.qtip = function(i, e) {
        var d = "interfaces",
            k = this,
            h, p, l, v, b, a, s, y;
        if (typeof i == o) {
            if (typeof n(k).data(f) != u && n.fn.qtip.log.error.call(self, 1, n.fn.qtip.constants.NO_TOOLTIP_PRESENT, t), i == c) return n(k).data(f).interfaces[n(k).data(f).current];
            if (i == d) return n(k).data(f).interfaces
        } else i || (i = {}), (typeof i.content != u || i.content.jquery && i.content.length > 0) && (i.content = {
            text: i.content
        }), typeof i.content.title != u && (i.content.title = {
            text: i.content.title
        }), typeof i.position != u && (i.position = {
            corner: i.position
        }), typeof i.position.corner != u && (i.position.corner = {
            target: i.position.corner,
            tooltip: i.position.corner
        }), typeof i.show != u && (i.show = {
            when: i.show
        }), typeof i.show.when != u && (i.show.when = {
            event: i.show.when
        }), typeof i.show.effect != u && (i.show.effect = {
            type: i.show.effect
        }), typeof i.hide != u && (i.hide = {
            when: i.hide
        }), typeof i.hide.when != u && (i.hide.when = {
            event: i.hide.when
        }), typeof i.hide.effect != u && (i.hide.effect = {
            type: i.hide.effect
        }), typeof i.style != u && (i.style = {
            name: i.style
        }), i.style = fi(i.style), v = n.extend(r, {}, n.fn.qtip.defaults, i), v.style = ui.call({
            options: v
        }, v.style), v.user = n.extend(r, {}, i);
        return n(k).each(function() {
            var k = this;
            if (typeof i == o) {
                if (a = i.toLowerCase(), l = n(k).qtip(d), typeof l == u)
                    if (e === r && a == ut)
                        while (l.length > 0) l[l.length - 1].destroy();
                    else
                        for (e !== r && (l = [n(k).qtip(c)]), h = 0; h < l.length; h++) a == ut ? l[h].destroy() : l[h].status.rendered === r && (a == ct ? l[h].show() : a == lt ? l[h].hide() : a == rt ? l[h].focus() : a == w ? l[h].disable(r) : a == "enable" && l[h].disable(t))
            } else {
                for (s = n.extend(r, {}, v), s.hide.effect.length = v.hide.effect.length, s.show.effect.length = v.show.effect.length, s.position.container === t && (s.position.container = n(document.body)), s.position.target === t && (s.position.target = n(k)), s.show.when.target === t && (s.show.when.target = n(k)), s.hide.when.target === t && (s.hide.when.target = n(k)), p = n.fn.qtip.interfaces.length, h = 0; h < p; h++)
                    if (typeof n.fn.qtip.interfaces[h] == nt) {
                        p = h;
                        break
                    }
                b = new wi(n(k), s, p), n.fn.qtip.interfaces[p] = b, typeof n(k).data(f) == u ? (typeof n(k).attr(f) == nt && (n(k).data(f).current = n(k).data(f).interfaces.length), n(k).data(f).interfaces.push(b)) : n(k).data(f, {
                    current: 0,
                    interfaces: [b]
                }), s.content.prerender === t && s.show.when.event !== t && s.show.ready !== r ? s.show.when.target.bind(s.show.when.event + ".qtip-" + p + "-create", {
                    qtip: p
                }, function(t) {
                    y = n.fn.qtip.interfaces[t.data.qtip], y.options.show.when.target.unbind(y.options.show.when.event + ".qtip-" + t.data.qtip + "-create"), y.cache.mouse = {
                        x: t.pageX,
                        y: t.pageY
                    }, at.call(y), y.options.show.when.target.trigger(y.options.show.when.event)
                }) : (b.cache.mouse = {
                    x: s.show.when.target.offset().left,
                    y: s.show.when.target.offset().top
                }, at.call(b))
            }
        })
    }, n(document).ready(function() {
        n.fn.qtip.cache = {
            screen: {
                scroll: {
                    left: n(window).scrollLeft(),
                    top: n(window).scrollTop()
                },
                width: n(window).width(),
                height: n(window).height()
            }
        };
        var t;
        n(window).bind("resize scroll", function(u) {
            clearTimeout(t), t = setTimeout(function() {
                for (u.type === "scroll" ? n.fn.qtip.cache.screen.scroll = {
                        left: n(window).scrollLeft(),
                        top: n(window).scrollTop()
                    } : (n.fn.qtip.cache.screen.width = n(window).width(), n.fn.qtip.cache.screen.height = n(window).height()), i = 0; i < n.fn.qtip.interfaces.length; i++) {
                    var t = n.fn.qtip.interfaces[i];
                    t.status.rendered === r && (t.options.position.type !== s || t.options.position.adjust.scroll && u.type === "scroll" || t.options.position.adjust.resize && u.type === "resize") && t.updatePosition(u, r)
                }
            }, 100)
        }), n(document).bind("mousedown.qtip", function(t) {
            n(t.target).parents(ei).length === 0 && n(".qtip[unfocus]").each(function() {
                var i = n(this).qtip(c);
                n(this).is(ft) && !i.status.disabled && n(t.target).add(i.elements.target).length > 1 && i.hide(t)
            })
        })
    }), n.fn.qtip.interfaces = [], n.fn.qtip.log = {
        error: function() {
            return this
        }
    }, n.fn.qtip.constants = {}, n.fn.qtip.defaults = {
        content: {
            prerender: t,
            text: t,
            url: t,
            data: e,
            title: {
                text: t,
                button: t
            }
        },
        position: {
            target: t,
            corner: {
                target: "bottomRight",
                tooltip: "topLeft"
            },
            adjust: {
                x: 0,
                y: 0,
                mouse: r,
                screen: t,
                scroll: r,
                resize: r
            },
            type: "absolute",
            container: t
        },
        show: {
            when: {
                target: t,
                event: gt
            },
            effect: {
                type: g,
                length: 100
            },
            delay: 140,
            solo: t,
            ready: t
        },
        hide: {
            when: {
                target: t,
                event: dt
            },
            effect: {
                type: g,
                length: 100
            },
            delay: 0,
            fixed: t
        },
        api: {
            beforeRender: function() {},
            onRender: function() {},
            beforePositionUpdate: function() {},
            onPositionUpdate: function() {},
            beforeShow: function() {},
            onShow: function() {},
            beforeHide: function() {},
            onHide: function() {},
            beforeContentUpdate: function() {},
            onContentUpdate: function() {},
            beforeContentLoad: function() {},
            onContentLoad: function() {},
            beforeTitleUpdate: function() {},
            onTitleUpdate: function() {},
            beforeDestroy: function() {},
            onDestroy: function() {},
            beforeFocus: function() {},
            onFocus: function() {}
        }
    }, n.fn.qtip.styles = {
        defaults: {
            background: "white",
            color: "#111",
            overflow: ot,
            textAlign: "left",
            width: {
                min: 500,
                max: 500
            },
            padding: "5px 9px",
            border: {
                width: 1,
                radius: 0,
                color: "#d3d3d3"
            },
            tip: {
                corner: t,
                color: t,
                size: {
                    width: 13,
                    height: 13
                },
                opacity: 1
            },
            title: {
                background: "#e1e1e1",
                fontWeight: "bold",
                padding: "7px 12px"
            },
            button: {
                cursor: "pointer"
            },
            classes: {
                target: "",
                tip: "qtip-tip",
                title: "qtip-title",
                button: "qtip-button",
                content: "qtip-content",
                active: "qtip-active"
            }
        },
        cream: {
            border: {
                width: 1,
                radius: 0,
                color: "#f1f1f1"
            },
            title: {
                background: "#FFF",
                color: "#5287a6"
            },
            background: "#FFF",
            color: "#A27D35",
            classes: {
                tooltip: "qtip-cream"
            }
        },
        light: {
            border: {
                width: 3,
                radius: 0,
                color: "#E2E2E2"
            },
            title: {
                background: "#f1f1f1",
                color: "#454545"
            },
            background: "white",
            color: "#454545",
            classes: {
                tooltip: "qtip-light"
            }
        },
        dark: {
            border: {
                width: 3,
                radius: 0,
                color: "#303030"
            },
            title: {
                background: "#404040",
                color: "#f3f3f3"
            },
            background: "#505050",
            color: "#f3f3f3",
            classes: {
                tooltip: "qtip-dark"
            }
        },
        red: {
            border: {
                width: 3,
                radius: 0,
                color: "#CE6F6F"
            },
            title: {
                background: "#f28279",
                color: "#9C2F2F"
            },
            background: "#F79992",
            color: "#9C2F2F",
            classes: {
                tooltip: "qtip-red"
            }
        },
        green: {
            border: {
                width: 3,
                radius: 0,
                color: "#A9DB66"
            },
            title: {
                background: "#b9db8c",
                color: "#58792E"
            },
            background: "#CDE6AC",
            color: "#58792E",
            classes: {
                tooltip: "qtip-green"
            }
        },
        blue: {
            border: {
                width: 3,
                radius: 0,
                color: "#ADD9ED"
            },
            title: {
                background: "#D0E9F5",
                color: "#5E99BD"
            },
            background: "#E5F6FE",
            color: "#4D9FBF",
            classes: {
                tooltip: "qtip-blue"
            }
        }
    }
})(jQuery);