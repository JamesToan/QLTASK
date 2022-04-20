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
    <el-row>
      <el-select style="width: 240px; float: left; margin-bottom: 10px"
                 v-model="DichVuIdFilter"
                 @change="changeDichVuIdFilter"
                 placeholder="Chọn Dịch Vụ">
        <el-option v-for="item in ListDMDichVu"
                   :key="item.Id"
                   :label="item.TenDichVu"
                   :value="item.Id">
        </el-option>
      </el-select>
    </el-row>
    <el-row class="row">
      <div class="col-4">
        <a href="/admin/yeucau">
          <div class="card-box table-responsive" style="height: 310px;background-color:dodgerblue; color:white">

            <div class="header-title ">
              Tổng số yêu cầu
            </div>

            <div align="center"
                 style="font-size: 64px; font-weight: bold"
                 class="mt-5">
              {{ ThongKe.Tong }}
            </div>
          </div>
        </a>
        
      </div>
      <div class="col-8">
        <el-row class="row ">
          <div class="col-4">
            <a href="/admin/yeucau/6">
              <div class="card-box table-responsive" style="height: 310px;background-color:mediumseagreen; color:black">
                <div class="header-title header-tt" >
                  Đã hoàn thành
                </div>
                <div align="center" class="num_tt" style="font-size: 26px; font-weight: bold; margin-bottom:30px">
                  {{ ThongKe.DaHT }}
                </div>
                <div class="row">
                  <div class="sub-tk" align="center">
                    <div class="header-title-tk">
                      Đúng hạn
                    </div>
                    <div class="num_tk">
                      {{ ThongKe.DungHanHT }}
                    </div>
                  </div>
                  <div class=" sub-tk"  align="center">
                    <div class="header-title-tk">
                      Trễ hạn
                    </div>
                    <div class="num_tk">
                      {{ ThongKe.TreHanHT }}
                    </div>
                  </div>
                </div>
              </div>
            </a>
           
          </div>
          <div class="col-4">
            <a href="/admin/yeucau/7">
              <div class="card-box table-responsive" style="height: 310px;background-color:orange; color:black">
                <div class="header-title header-tt" >
                  Đang xử lý
                </div>
                <div align="center" class="num_tt" style="font-size: 26px; font-weight: bold; margin-bottom:30px">
                  {{ ThongKe.DangXL }}
                </div>
                <div class="row">
                  <div class="sub-tk" align="center">
                    <div class="header-title-tk">
                      Trong hạn
                    </div>
                    <div class="num_tk">
                      {{ ThongKe.TrongHanXL }}
                    </div>
                  </div>
                  <div class="sub-tk" align="center">
                    <div class="header-title-tk">
                      Trễ hạn
                    </div>
                    <div class="num_tk">
                      {{ ThongKe.TreHanXL }}
                    </div>
                  </div>
                </div>
              </div>
            </a>
           
          </div>
          <div class="col-4">
            <a href="/admin/yeucau/10">
              <div class="card-box table-responsive" style="height: 310px;background-color:lightblue; color:black">
                <div class="header-title header-tt" >
                  Chưa tiếp nhận
                </div>
                <div align="center" class="num_tt" style="font-size: 26px; font-weight: bold; margin-bottom:30px">
                  {{ ThongKe.MoiTao }}
                </div>
                <div class="row">
                  <div class="sub-tk" align="center">
                    <div class="header-title-tk">
                      Trong hạn
                    </div>
                    <div class="num_tk">
                      {{ ThongKe.TrongHanCTN }}
                    </div>
                  </div>
                  <div class=" sub-tk" align="center">
                    <div class="header-title-tk">
                      Quá hạn
                    </div>
                    <div class="num_tk">
                      {{ ThongKe.TreHanCTN }}
                    </div>
                  </div>
                </div>
              </div>
            </a>
            
          </div>
        </el-row>
        <!--<el-row class="row ">
          
          <div class="col-4">
            <div class="card-box table-responsive" style="height: 140px;background-color:mediumseagreen; color:black">
              <div class="header-title header-tt">Trong hạn</div>
              <div align="center" class="num_tt" style="font-size: 26px; font-weight: bold">
                {{ ThongKe.TrongHan }}
              </div>
            </div>
          </div>
          <div class="col-4">
            <div class="card-box table-responsive" style="height: 140px;background-color:orange; color:black">
              <div class="header-title header-tt">Trễ hạn</div>
              <div align="center" class="num_tt" style="font-size: 26px; font-weight: bold">
                {{ ThongKe.TreHan }}
              </div>
            </div>
          </div>

        </el-row>-->

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
  import {
    getThongKe,
    getListDanhMucYeuCau,

  } from "../store/api";
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
      ListDMDichVu: [],
      DichVuIdFilter: 1,
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
    changeDichVuIdFilter() {
      
      this.getThongKe();
    },
    getThongKe() {
      
      this.loading = true;
      if (this.DichVuIdFilter != 1) {
        getThongKe(this.DichVuIdFilter).then(data => {
          this.ThongKe = data;
          this.options.series[0].data = data.Chart;
          this.loading = false;
        });
      }
      else {
        getThongKe().then(data => {
          this.ThongKe = data;
          this.options.series[0].data = data.Chart;
          this.loading = false;
        });
      }
      
    }
  },
  mounted() {
    this.menuMobile();
  },
    created() {

      getListDanhMucYeuCau().then(data => {
        if (data) {
          this.ListDMNhanSu = data.DMNhanSu;
          this.ListDMTinhTrang = data.DMTinhTrang;
          this.ListDMTrangThai = data.DMTrangThai;
          this.ListDMDichVu = data.DMDichVu;
          this.ListDMJira = data.DMJira;
          this.ListDMDonVi = data.DMDonVi;
        }
      });

    this.menuMobile();
    this.getThongKe();
  }
};
</script>

<style>

  @media (max-width:480px){
    .card-box {
      padding-top: 2rem !important ;
      padding-left: 1rem !important;
    }
    .header-title{
      margin-top:0px !important;
      margin-bottom: 0px !important;
    }
    .num_tt{
      font-size: 18px !important;
    }
    .mt-5 {
      font-size: 32px !important;
    }
    .header-tt {
      font-size: 11px !important;
    }
    .sub-tk {
      padding: 0px !important;
      -webkit-box-flex: 0;
      -ms-flex: 0 0 100% !important;
      flex: 0 0 100% !important;
      max-width: 100% !important;
    }
    .header-title-tk {
      font-size: 10px !important;
    }
  }
  @media (max-width:650px)and (min-width: 481px) {
    .mt-5 {
      font-size: 40px !important;
    }

    .header-title {
      margin-top: 30px;
    }

    .sub-tk {
      padding: 0px !important;
      -webkit-box-flex: 0;
      -ms-flex: 0 0 100% !important;
      flex: 0 0 100% !important;
      max-width: 100% !important;
    }

    .num_tk {
      margin-bottom: 5px;
    }

    .header-tt {
      margin-top: 0px !important;
    }

    .card-box {
      padding: 1rem;
    }
    .header-title-tk{
      font-size:12px !important;
    }
  }

  .header-tt {
    margin-bottom: 30px;
    margin-top: 30px;
    text-align: center;
  }
  .header-title-tk {
    margin: 0 0 7px 0;
    text-transform: uppercase;
    letter-spacing: .02em;
    font-size: 14px;
    font-weight: 700;
  }
  .num_tk {
    font-size: 18px;
    font-weight: 600;
  }
  .sub-tk {
    padding: 0px !important;
    -webkit-box-flex: 0;
    -ms-flex: 0 0 50%;
    flex: 0 0 50%;
    max-width: 50%;
  }
</style>
