// lazyload config

angular.module('app')
    /**
   * jQuery plugin config use ui-jq directive , config the js and css files that required
   * key: function name of the jQuery plugin
   * value: array of the css js file located
   */
  .constant('JQ_CONFIG', {
      easyPieChart: ['../Content/vendor/jquery/charts/easypiechart/jquery.easy-pie-chart.js'],
      sparkline:      ['../Content/vendor/jquery/charts/sparkline/jquery.sparkline.min.js'],
      plot: ['../Content/vendor/jquery/charts/flot/jquery.flot.min.js',
                          '../Content/vendor/jquery/charts/flot/jquery.flot.resize.js',
                          '../Content/vendor/jquery/charts/flot/jquery.flot.tooltip.min.js',
                          '../Content/vendor/jquery/charts/flot/jquery.flot.spline.js',
                          '../Content/vendor/jquery/charts/flot/jquery.flot.orderBars.js',
                          '../Content/vendor/jquery/charts/flot/jquery.flot.pie.min.js'],
      slimScroll: ['../Content/vendor/jquery/slimscroll/jquery.slimscroll.min.js'],
      sortable: ['../Content/vendor/jquery/sortable/jquery.sortable.js'],
      nestable: ['../Content/vendor/jquery/nestable/jquery.nestable.js',
                          '../Content/vendor/jquery/nestable/nestable.css'],
      filestyle: ['../Content/vendor/jquery/file/bootstrap-filestyle.min.js'],
      slider: ['../Content/vendor/jquery/slider/bootstrap-slider.js',
                          '../Content/vendor/jquery/slider/slider.css'],
      chosen: ['../Content/vendor/jquery/chosen/chosen.jquery.min.js',
                          '../Content/vendor/jquery/chosen/chosen.css'],
      TouchSpin: ['../Content/vendor/jquery/spinner/jquery.bootstrap-touchspin.min.js',
                          '../Content/vendor/jquery/spinner/jquery.bootstrap-touchspin.css'],
      wysiwyg: ['../Content/vendor/jquery/wysiwyg/bootstrap-wysiwyg.js',
                          '../Content/vendor/jquery/wysiwyg/jquery.hotkeys.js'],
      dataTable: ['../Content/vendor/jquery/datatables/jquery.dataTables.min.js',
                          '../Content/vendor/jquery/datatables/dataTables.bootstrap.js',
                          '../Content/vendor/jquery/datatables/dataTables.bootstrap.css'],
      vectorMap: ['../Content/vendor/jquery/jvectormap/jquery-jvectormap.min.js',
                          '../Content/vendor/jquery/jvectormap/jquery-jvectormap-world-mill-en.js',
                          '../Content/vendor/jquery/jvectormap/jquery-jvectormap-us-aea-en.js',
                          '../Content/vendor/jquery/jvectormap/jquery-jvectormap.css'],
      footable: ['../Content/vendor/jquery/footable/footable.all.min.js',
                          '../Content/vendor/jquery/footable/footable.core.css']
      }
  )
  // oclazyload config
  .config(['$ocLazyLoadProvider', function($ocLazyLoadProvider) {
      // We configure ocLazyLoad to use the lib script.js as the async loader
      $ocLazyLoadProvider.config({
          debug:  false,
          events: true,
          modules: [
              {
                  name: 'ngGrid',
                  files: [
                      '../Content/vendor/modules/ng-grid/ng-grid.min.js',
                      '../Content/vendor/modules/ng-grid/ng-grid.min.css',
                      '../Content/vendor/modules/ng-grid/theme.css'
                  ]
              },
              {
                  name: 'ui.select',
                  files: [
                      '../Content/vendor/modules/angular-ui-select/select.min.js',
                      '../Content/vendor/modules/angular-ui-select/select.min.css'
                  ]
              },
              {
                  name:'angularFileUpload',
                  files: [
                    '../Content/vendor/modules/angular-file-upload/angular-file-upload.min.js'
                  ]
              },
              {
                  name:'ui.calendar',
                  files: ['../Content/vendor/modules/angular-ui-calendar/calendar.js']
              },
              {
                  name: 'ngImgCrop',
                  files: [
                      '../Content/vendor/modules/ngImgCrop/ng-img-crop.js',
                      '../Content/vendor/modules/ngImgCrop/ng-img-crop.css'
                  ]
              },
              {
                  name: 'angularBootstrapNavTree',
                  files: [
                      '../Content/vendor/modules/angular-bootstrap-nav-tree/abn_tree_directive.js',
                      '../Content/vendor/modules/angular-bootstrap-nav-tree/abn_tree.css'
                  ]
              },
              {
                  name: 'toaster',
                  files: [
                      '../Content/vendor/modules/angularjs-toaster/toaster.js',
                      '../Content/vendor/modules/angularjs-toaster/toaster.css'
                  ]
              },
              {
                  name: 'textAngular',
                  files: [
                      '../Content/vendor/modules/textAngular/textAngular-sanitize.min.js',
                      '../Content/vendor/modules/textAngular/textAngular.min.js'
                  ]
              },
              {
                  name: 'vr.directives.slider',
                  files: [
                      '../Content/vendor/modules/angular-slider/angular-slider.min.js',
                      '../Content/vendor/modules/angular-slider/angular-slider.css'
                  ]
              },
              {
                  name: 'com.2fdevs.videogular',
                  files: [
                      '../Content/vendor/modules/videogular/videogular.min.js'
                  ]
              },
              {
                  name: 'com.2fdevs.videogular.plugins.controls',
                  files: [
                      '../Content/vendor/modules/videogular/plugins/controls.min.js'
                  ]
              },
              {
                  name: 'com.2fdevs.videogular.plugins.buffering',
                  files: [
                      '../Content/vendor/modules/videogular/plugins/buffering.min.js'
                  ]
              },
              {
                  name: 'com.2fdevs.videogular.plugins.overlayplay',
                  files: [
                      '../Content/vendor/modules/videogular/plugins/overlay-play.min.js'
                  ]
              },
              {
                  name: 'com.2fdevs.videogular.plugins.poster',
                  files: [
                      '../Content/vendor/modules/videogular/plugins/poster.min.js'
                  ]
              },
              {
                  name: 'com.2fdevs.videogular.plugins.imaads',
                  files: [
                      '../Content/vendor/modules/videogular/plugins/ima-ads.min.js'
                  ]
              }
          ]
      });
  }])
;