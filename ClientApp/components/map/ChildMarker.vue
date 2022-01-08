<template></template>
<script>
import Vue from "vue";

export default {
  inject: ["google", "map"],
  props: {
    position: Object,
    info: Object
  },
  data() {
    return { marker: null };
  },
  mounted() {
    if (this.info) {
      var mst = this.info.MaSoThue ? this.info.MaSoThue : "";
      var sđt = this.info.SoDienThoai ? this.info.SoDienThoai : "";
      var đc = this.info.DiaChi ? this.info.DiaChi : "";
      var content =
        "<h4>Thông tin Doanh nghiệp xăng dầu</h4>" +
        '<span class="minfo">Doanh nghiệp</span>: <b>' +
        this.info.TenDoanhNghiep +
        "</b><br>" +
        '<span class="minfo">Mã số thuế</span>: ' +
        mst +
        "<br>" +
        '<span class="minfo">Số điện thoại</span>: ' +
        sđt +
        "<br>" +
        '<span class="minfo">Địa chỉ</span>: ' +
        đc +
        "<br>";
      const infowindow = new google.maps.InfoWindow({
        content: content
      });

      const { Marker } = this.google.maps;
      this.marker = new Marker({
        position: this.position,
        map: this.map,
        title: this.info.TenDoanhNghiep
      });
      this.marker.addListener("click", () => {
        infowindow.open(map, this.marker);
        if (Vue.prototype.$preInfowindow) {
          Vue.prototype.$preInfowindow.close();
        }

        Vue.prototype.$preInfowindow = infowindow;
      });
    }
  }
};
</script>
