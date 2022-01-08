(function(a, b) {
    dnnModal = {
        load: function() {
            try {
                if (parent.location.href !== undefined) {
                    var g = parent;
                    var f = g.parent;
                    if (typeof(f.$find) != "undefined") {
                        if (location.href.indexOf("popUp") == -1 || g.location.href.indexOf("popUp") > -1) {
                            var c = g.$("#iPopUp");
                            var d = c.dialog("option", "refresh");
                            var h = c.dialog("option", "closingUrl");
                            if (h == "") {
                                h = location.href
                            }
                            if (c.dialog("isOpen") === true) {
                                c.dialog("option", {
                                    close: function(i, j) {
                                        dnnModal.closePopUp(d, h)
                                    }
                                }).dialog("close")
                            }
                        } else {
                            g.$("#iPopUp").dialog({
                                title: document.title
                            })
                        }
                    }
                }
            } catch (e) {
                return false
            }
        },
        show: function(c, m, j, d, f, i) {
            var h = b("#iPopUp");
            if (h.length == 0) {
                h = b('<iframe id="iPopUp" src="about:blank" scrolling="auto" frameborder="0"></iframe>');
                b(document).find("html").css("overflow", "hidden");
                b(document).append(h)
            }
            var k = parent;
            h.dialog({
                modal: true,
                autoOpen: true,
                dialogClass: "dnnFormPopup",
                position: "center",
                minWidth: d,
                minHeight: j,
                maxWidth: 1920,
                maxHeight: 1080,
                resizable: true,
                closeOnEscape: true,
                zIndex: 100000,
                refresh: f,
                closingUrl: i,
                close: function(n, o) {
                    dnnModal.closePopUp(f, i)
                }
            }).width(d - 11).height(j - 11);
            if (b(".ui-dialog-title").next("a.dnnToggleMax").length === 0) {
                var g = b('<a href="#" class="dnnToggleMax"><span>Max</span></a>');
                b(".ui-dialog-title").after(g);
                g.click(function(r) {
                    r.preventDefault();
                    var s = b(a),
                        q = b(this),
                        n, p, o;
                    if (h.data("isMaximized")) {
                        n = h.data("height");
                        p = h.data("width");
                        o = h.data("position");
                        h.data("isMaximized", false)
                    } else {
                        h.data("height", h.dialog("option", "minHeight")).data("width", h.dialog("option", "minWidth")).data("position", h.dialog("option", "position"));
                        n = s.height() - 11;
                        p = s.width() - 11;
                        o = [0, 0];
                        h.data("isMaximized", true)
                    }
                    q.toggleClass("ui-dialog-titlebar-max");
                    h.dialog({
                        height: n,
                        width: p
                    });
                    h.dialog({
                        position: "center"
                    })
                })
            }
            var e = function() {
                var n = b('<div class="dnnLoading"></div>');
                n.css({
                    width: h.width(),
                    height: h.height()
                });
                h.before(n)
            };
            var l = function() {
                h.prev(".dnnLoading").remove()
            };
            e();
            h[0].src = c;
            h.bind("load", function() {
                l()
            });
            if (m.toString() == "true") {
                return false
            }
        },
        closePopUp: function(d, c) {
            var e = parent;
            if (typeof d === "undefined") {
                d = true
            }
            if ((typeof c === "undefined") || (c == "")) {
                c = e.location
            }
            if (d.toString() == "true") {
                e.location.href = c;
                b(this).hide()
            } else {
                b(this).remove()
            }
            b(e.document).find("html").css("overflow", "")
        }
    };
    dnnModal.load()
}(window, jQuery));