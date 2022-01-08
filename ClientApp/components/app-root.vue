<template>
  <div id="app">

    <div id="wrapper">
      <nav-menu :key="$route.meta.loadMenu" v-if="!$route.meta.layout != '0'"></nav-menu>
      <left-menu :key="'l' + $route.meta.loadMenu" params="route: route" v-if="!$route.meta.layout != '0'"></left-menu>
      <div class="content-page">
        <div class="content">
          <!-- Start Content-->
          <div class="container-fluid">
            <router-view :key="$route.path"></router-view>
          </div>
        </div>

        <footer class="footer">
          <div class="container-fluid">
            <div class="row">
              <div class="col-md-12">
                2021 &copy; VNPT Long An
              </div>

            </div>
          </div>
        </footer>
      </div>
      </div>
    </div>
        <!--<nav-menu params="route: route" v-if="!$route.meta.layout != '0'"></nav-menu>-->
        <!--<nav-menu params="route: route"></nav-menu>-->
        <!--<div class="container body-content">
    <ol class="breadcrumb" style="border-bottom:1px solid #ddd" v-if="!$route.meta.layout != '0'">
      <li>
        <router-link to="/" class="fa fa-home">
          Home
        </router-link>
      </li>
      <li class="active" v-if="$route.path!='/'">{{$route.meta.display}}</li>
    </ol>-->
        <!--<footer style="width:100%;clear:both;margin-top:5px;float:left">
    <hr style="margin-top:5px;margin-bottom:7px" />
    <span>&copy; 2020 - HSKD</span>
  </footer>-->
</template>
<script>
  import $ from "jquery"
  import NavMenu from './nav-menu'
  import LeftMenu from './left-menu'
  require('metismenu');

  export default {
    components: {
      'nav-menu': NavMenu,
      'left-menu': LeftMenu
    },

    data() {
      return {}
    },
    methods: {
      menuMobile() {
        
        $('.button-menu-mobile').on('click', function (event) {
          //var $this = $(this);
          event.preventDefault();
          $('body').toggleClass('sidebar-enable');
          if ($(window).width() >= 768) {
            $('body').toggleClass('enlarged');
          } else {
            $('body').removeClass('enlarged');
          }


          // sidebar - scroll container
        });
        $("#side-menu").metisMenu();

        // sidebar - scroll container

        // right side-bar toggle

        $(document).on('click', 'body', function (e) {
          if ($(e.target).closest('.left-side-menu, .side-nav').length > 0 || $(e.target).hasClass('button-menu-mobile')
            || $(e.target).closest('.button-menu-mobile').length > 0) {
            return;
          }
          $('body').removeClass('sidebar-enable');
          return;
        });

        // activate the menu in left side bar based on url
        $("#side-menu a").each(function () {
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
        $('.navbar-toggle').on('click', function (event) {
          $(this).toggleClass('open');
          $('#navigation').slideToggle(400);
        });
        $(window).on('resize', function (e) {
          if ($(window).width() >= 768 && $(window).width() <= 1024) {
            $('body').addClass('enlarged');
          } else {
            if ($('body').data('keep-enlarged') != true) {
              $('body').removeClass('enlarged');
            }
          }
        });
      }
    },
    mounted() {
      this.menuMobile()
    },
    created() {
      this.menuMobile()
    }
  }
</script>
<style>
  .body-content {
    /*margin-top: 20px;*/
    background: #fff;
    box-shadow: 0 1px 3px 0 rgba(0, 0, 0, 0.2);
  }

    .body-content .center-content {
      margin-top: 20px;
      min-height: 500px;
    }
</style>
