<template>
  <div>
    <!-- start page title -->
    <div class="row">
      <div class="col-12">
        <div class="page-title-box">
          <div class="page-title-right">
            <ol class="breadcrumb m-0">
              <li class="breadcrumb-item">
                <a href="javascript: void(0);">Quản lý yêu cầu</a>
              </li>
              <li class="breadcrumb-item active">Dashboard</li>
            </ol>
          </div>
          <h4 class="page-title">Dashboard</h4>
        </div>
      </div>
    </div>
    <el-row class="row">
      <div class="col-4">
        <div class="card-box table-responsive" style="height: 310px;background-color:dodgerblue; color:white">
          <div class="header-title">
            Tổng số Yêu cầu
          </div>
          <div align="center"
               style="font-size: 64px; font-weight: bold"
               class="mt-5">
            {{ ThongKe.Tong }}
          </div>
        </div>
      </div>
      <div class="col-8">
        <el-row class="row hidden-md-and-down">
          <div class="col-4">
            <div class="card-box table-responsive" style="height: 145px;background-color:mediumseagreen; color:black">
              <div class="header-title">
                Đã hoàn thành
              </div>
              <div align="center" style="font-size: 26px; font-weight: bold">
                {{ ThongKe.DaHT }}
              </div>
            </div>
          </div>
          <div class="col-4">
            <div class="card-box table-responsive" style="height: 145px;background-color:orange; color:black">
              <div class="header-title">
                Đang xử lý
              </div>
              <div align="center" style="font-size: 26px; font-weight: bold">
                {{ ThongKe.DangXL }}
              </div>
            </div>
          </div>
          <div class="col-4">
            <div class="card-box table-responsive" style="height: 145px;background-color:lightblue; color:black">
              <div class="header-title">
                Chưa hoàn thành
              </div>
              <div align="center" style="font-size: 26px; font-weight: bold">
                {{ ThongKe.ChuaHT }}
              </div>
            </div>
          </div>
        </el-row>
        <el-row class="row hidden-md-and-down">
          <div class="col-4">
            <div class="card-box table-responsive" style="height: 140px;background-color:mediumseagreen; color:black">
              <div class="header-title">Trong hạn</div>
              <div align="center" style="font-size: 26px; font-weight: bold">
                {{ ThongKe.TrongHan }}
              </div>
            </div>
          </div>
          <div class="col-4">
            <div class="card-box table-responsive" style="height: 140px;background-color:orange; color:black">
              <div class="header-title">Trễ hạn</div>
              <div align="center" style="font-size: 26px; font-weight: bold">
                {{ ThongKe.TreHan }}
              </div>
            </div>
          </div>
          <div class="col-4">
            <div class="card-box table-responsive" style="height: 140px;background-color:lightblue; color:black">
              <div class="header-title">Chưa xử lý</div>
              <div align="center" style="font-size: 26px; font-weight: bold">
                {{ ThongKe.ChuaXL }}
              </div>
            </div>
          </div>
        </el-row>

      </div>
    </el-row>
    <!-- <el-row class="row">
    <div class="col-12">
      <div class="card-box table-responsive">
        <map-loader
          :map-config="mapConfig"
          :markers="markers"
          apiKey="AIzaSyAIxFqHiyHTq9XdDnYTIThpJnpuFWC1-Zw"
        >
          <template v-for="marker in markers">
            <child-marker
              :position="marker.position"
              :info="marker.info"
              :key="marker.info.Id"
            />
          </template>
        </map-loader>
      </div>
    </div>
  </el-row> -->
    <el-row>
      <el-col :span="24">
        <highcharts :options="options"
                    ref="highcharts"
                    style="float:left;width:100%"></highcharts>
      </el-col>
    </el-row>
    <!--<el-row>
      <el-col :span="24">
        <highcharts :options="options1"
                    ref="highcharts"
                    style="float:left;width:100%"></highcharts>
      </el-col>
    </el-row>-->
  </div>
  <!-- end page title -->
</template>

<script>
import $ from "jquery";
import { getThongKe } from "../store/api";
import "element-ui/lib/theme-chalk/display.css";
import MapLoader from "./map/MapLoader.vue";
import ChildMarker from "./map/ChildMarker.vue";
require("metismenu");
export default {
  components: {
    "map-loader": MapLoader,
    "child-marker": ChildMarker
  },
  data() {
    return {
      ThongKe: [],
      arrayPie: [],
      arrayColumn: [],
      markers: [],
      mapConfig: {
        zoom: 13,
        center: { lat: 10.53661, lng: 106.413002 }
      },
      options: {
        credits: false,
        chart: {
          type: "column"
        },
        title: {
          text: "Thống kê số yêu cầu theo trạng thái"
        },
        xAxis: {
          type: "category"
        },
        yAxis: {
          title: {
            text: "Số lượng"
          }
        },
        legend: {
          enabled: false
        },
        plotOptions: {
          series: {
            borderWidth: 0,
            dataLabels: {
              enabled: true,
              format: "{point.y:1f}"
            }
          }
        },

        tooltip: {
          headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
          pointFormat:
            '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:1f}</b> <br/>'
        },
        series: [
          {
            name: "Việc làm",
            colorByPoint: true,
            data: []
          }
        ]
      },
      //options1: {
      //  credits: false,
      //  chart: {
      //    type: "column"
      //  },
      //  title: {
      //    text: "Thống kê số yêu cầu theo tình trạng"
      //  },
      //  xAxis: {
      //    type: "category"
      //  },
      //  yAxis: {
      //    title: {
      //      text: "Số lượng"
      //    }
      //  },
      //  legend: {
      //    enabled: false
      //  },
      //  plotOptions: {
      //    series1: {
      //      borderWidth: 0,
      //      dataLabels: {
      //        enabled: true,
      //        format: "{point.y:1f}"
      //      }
      //    }
      //  },

      //  tooltip: {
      //    headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
      //    pointFormat:
      //      '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:1f}</b> <br/>'
      //  },
      //  series: [
      //    {
      //      name: "Việc làm",
      //      colorByPoint: true,
      //      data: []
      //    }
      //  ]
      //}
    };
  },
  methods: {
    menuMobile() {
      $(".button-menu-mobile").on("click", function(event) {
        //var $this = $(this);
        event.preventDefault();
        $("body").toggleClass("sidebar-enable");
        if ($(window).width() >= 768) {
          $("body").toggleClass("enlarged");
        } else {
          $("body").removeClass("enlarged");
        }

        // sidebar - scroll container
      });
      $("#side-menu").metisMenu();

      // sidebar - scroll container

      // right side-bar toggle

      // activate the menu in left side bar based on url
      $("#side-menu a").each(function() {
        var pageUrl = window.location.href.split(/[?#]/)[0];
        if (this.href == pageUrl) {
          //$(this).addClass("active");
          //$(this).parent().addClass("mm-active"); // add active to li of the current link
          //$(this).parent().parent().addClass("mm-show");
          //$(this).parent().parent().prev().addClass("active"); // add active class to an anchor
          //$(this).parent().parent().parent().addClass("mm-active");
          //$(this).parent().parent().parent().parent().addClass("mm-show"); // add active to li of the current link
          //$(this).parent().parent().parent().parent().parent().addClass("mm-active");
        }
      });

      // Topbar - main menu
      $(".navbar-toggle").on("click", function(event) {
        $(this).toggleClass("open");
        $("#navigation").slideToggle(400);
      });
      $(window).on("resize", function(e) {
        if ($(window).width() >= 768 && $(window).width() <= 1024) {
          $("body").addClass("enlarged");
        } else {
          if ($("body").data("keep-enlarged") != true) {
            $("body").removeClass("enlarged");
          }
        }
      });
    },

    getThongKe() {
      // this.arrayPie = [];
      // this.arrayColumn = [];

      getThongKe().then(data => {
        this.ThongKe = data;
        this.options.series[0].data = data.Chart;
        //this.options.series[0].data = data.Chart;
        //this.markers = data.markers;
      });
    }
  },
  mounted() {
    this.menuMobile();
  },
  created() {
    this.menuMobile();
    this.getThongKe();
  }
};
</script>

<style></style>
