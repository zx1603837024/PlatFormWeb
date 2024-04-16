'use strict';

/**
 * Config for the router
 */
angular.module('app')
  .run(
    [          '$rootScope', '$state', '$stateParams',
      function ($rootScope,   $state,   $stateParams) {
          $rootScope.$state = $state;
          $rootScope.$stateParams = $stateParams;        
      }
    ]
  )
  .config(
    [          '$stateProvider', '$urlRouterProvider',
      function ($stateProvider,   $urlRouterProvider) {
          
          $urlRouterProvider
              .otherwise('/app/dashboard-v1');
          $stateProvider
              .state('app', {
                  abstract: true,
                  url: '/app',
                  templateUrl: '../Content/tpl/app.html'
              })
              .state('app.dashboard-v1', {
                  url: '/dashboard-v1',
                  templateUrl: '../Content/tpl/app_dashboard_v1.html',
                  resolve: {
                    deps: ['$ocLazyLoad',
                      function( $ocLazyLoad ){
                        return $ocLazyLoad.load(['../Content/js/controllers/chart.js']);
                    }]
                  }
              })
              .state('app.dashboard-v2', {
                  url: '/dashboard-v2',
                  templateUrl: '../Content/tpl/app_dashboard_v2.html',
                  resolve: {
                    deps: ['$ocLazyLoad',
                      function( $ocLazyLoad ){
                          return $ocLazyLoad.load(['../Content/js/controllers/chart.js']);
                    }]
                  }
              })
              .state('app.ui', {
                  url: '/ui',
                  template: '<div ui-view class="fade-in-up"></div>'
              })
              .state('app.ui.buttons', {
                  url: '/buttons',
                  templateUrl: '../Content/tpl/ui_buttons.html'
              })
              .state('app.ui.icons', {
                  url: '/icons',
                  templateUrl: '../Content/tpl/ui_icons.html'
              })
              .state('app.ui.grid', {
                  url: '/grid',
                  templateUrl: '../Content/tpl/ui_grid.html'
              })
              .state('app.ui.widgets', {
                  url: '/widgets',
                  templateUrl: '../Content/tpl/ui_widgets.html'
              })          
              .state('app.ui.bootstrap', {
                  url: '/bootstrap',
                  templateUrl: '../Content/tpl/ui_bootstrap.html'
              })
              .state('app.ui.sortable', {
                  url: '/sortable',
                  templateUrl: '../Content/tpl/ui_sortable.html'
              })
              .state('app.ui.portlet', {
                  url: '/portlet',
                  templateUrl: '../Content/tpl/ui_portlet.html'
              })
              .state('app.ui.timeline', {
                  url: '/timeline',
                  templateUrl: '../Content/tpl/ui_timeline.html'
              })
              .state('app.ui.tree', {
                  url: '/tree',
                  templateUrl: '../Content/tpl/ui_tree.html',
                  resolve: {
                      deps: ['$ocLazyLoad',
                        function( $ocLazyLoad ){
                          return $ocLazyLoad.load('angularBootstrapNavTree').then(
                              function(){
                                  return $ocLazyLoad.load('../Content/js/controllers/tree.js');
                              }
                          );
                        }
                      ]
                  }
              })
              .state('app.ui.toaster', {
                  url: '/toaster',
                  templateUrl: 'tpl/ui_toaster.html',
                  resolve: {
                      deps: ['$ocLazyLoad',
                        function( $ocLazyLoad){
                          return $ocLazyLoad.load('toaster').then(
                              function(){
                                  return $ocLazyLoad.load('../Content/js/controllers/toaster.js');
                              }
                          );
                      }]
                  }
              })
              .state('app.ui.jvectormap', {
                  url: '/jvectormap',
                  templateUrl: '../Content/tpl/ui_jvectormap.html',
                  resolve: {
                      deps: ['$ocLazyLoad',
                        function( $ocLazyLoad){
                            return $ocLazyLoad.load('../Content/js/controllers/vectormap.js');
                      }]
                  }
              })
              .state('app.ui.googlemap', {
                  url: '/googlemap',
                  templateUrl: '../Content/tpl/ui_googlemap.html',
                  resolve: {
                      deps: ['uiLoad',
                        function( uiLoad ){
                          return uiLoad.load( [
                            'js/app/map/load-google-maps.js',
                            'js/app/map/ui-map.js',
                            'js/app/map/map.js'] ).then(
                              function(){
                                return loadGoogleMaps(); 
                              }
                            );
                      }]
                  }
              })
              .state('app.chart', {
                  url: '/chart',
                  templateUrl: '../Content/tpl/ui_chart.html',
                  resolve: {
                      deps: ['uiLoad',
                        function( uiLoad){
                            return uiLoad.load('../Content/js/controllers/chart.js');
                      }]
                  }
              })
              // table
              .state('app.table', {
                  url: '/table',
                  template: '<div ui-view></div>'
              })
              .state('app.table.static', {
                  url: '/static',
                  templateUrl: '../Content/tpl/table_static.html'
              })
              .state('app.table.datatable', {
                  url: '/datatable',
                  templateUrl: '../Content/tpl/table_datatable.html'
              })
              .state('app.table.footable', {
                  url: '/footable',
                  templateUrl: '../Content/tpl/table_footable.html'
              })
              .state('app.table.grid', {
                  url: '/grid',
                  templateUrl: '../Content/tpl/table_grid.html',
                  resolve: {
                      deps: ['$ocLazyLoad',
                        function( $ocLazyLoad ){
                          return $ocLazyLoad.load('ngGrid').then(
                              function(){
                                  return $ocLazyLoad.load('../Content/js/controllers/grid.js');
                              }
                          );
                      }]
                  }
              })
              // form
              .state('app.form', {
                  url: '/form',
                  template: '<div ui-view class="fade-in"></div>',
                  resolve: {
                      deps: ['uiLoad',
                        function( uiLoad){
                            return uiLoad.load('../Content/js/controllers/form.js');
                      }]
                  }
              })
              .state('app.form.elements', {
                  url: '/elements',
                  templateUrl: '../Content/tpl/form_elements.html'
              })
              .state('app.form.validation', {
                  url: '/validation',
                  templateUrl: '../Content/tpl/form_validation.html'
              })
              .state('app.form.wizard', {
                  url: '/wizard',
                  templateUrl: '../Content/tpl/form_wizard.html'
              })
              .state('app.form.fileupload', {
                  url: '/fileupload',
                  templateUrl: '../Content/tpl/form_fileupload.html',
                  resolve: {
                      deps: ['$ocLazyLoad',
                        function( $ocLazyLoad){
                          return $ocLazyLoad.load('angularFileUpload').then(
                              function(){
                                  return $ocLazyLoad.load('../Content/js/controllers/file-upload.js');
                              }
                          );
                      }]
                  }
              })
              .state('app.form.imagecrop', {
                  url: '/imagecrop',
                  templateUrl: '../Content/tpl/form_imagecrop.html',
                  resolve: {
                      deps: ['$ocLazyLoad',
                        function( $ocLazyLoad){
                          return $ocLazyLoad.load('ngImgCrop').then(
                              function(){
                                  return $ocLazyLoad.load('../Content/js/controllers/imgcrop.js');
                              }
                          );
                      }]
                  }
              })
              .state('app.form.select', {
                  url: '/select',
                  templateUrl: '../Content/tpl/form_select.html',
                  controller: 'SelectCtrl',
                  resolve: {
                      deps: ['$ocLazyLoad',
                        function( $ocLazyLoad ){
                          return $ocLazyLoad.load('ui.select').then(
                              function(){
                                  return $ocLazyLoad.load('../Content/js/controllers/select.js');
                              }
                          );
                      }]
                  }
              })
              .state('app.form.slider', {
                  url: '/slider',
                  templateUrl: '../Content/tpl/form_slider.html',
                  controller: 'SliderCtrl',
                  resolve: {
                      deps: ['$ocLazyLoad',
                        function( $ocLazyLoad ){
                          return $ocLazyLoad.load('vr.directives.slider').then(
                              function(){
                                  return $ocLazyLoad.load('../Content/js/controllers/slider.js');
                              }
                          );
                      }]
                  }
              })
              .state('app.form.editor', {
                  url: '/editor',
                  templateUrl: '../Content/tpl/form_editor.html',
                  controller: 'EditorCtrl',
                  resolve: {
                      deps: ['$ocLazyLoad',
                        function( $ocLazyLoad ){
                          return $ocLazyLoad.load('textAngular').then(
                              function(){
                                  return $ocLazyLoad.load('../Content/js/controllers/editor.js');
                              }
                          );
                      }]
                  }
              })
              // pages
              .state('app.page', {
                  url: '/page',
                  template: '<div ui-view class="fade-in-down"></div>'
              })
              .state('app.page.profile', {
                  url: '/profile',
                  templateUrl: '../Content/tpl/page_profile.html'
              })
              .state('app.page.post', {
                  url: '/post',
                  templateUrl: '../Content/tpl/page_post.html'
              })
              .state('app.page.search', {
                  url: '/search',
                  templateUrl: '../Content/tpl/page_search.html'
              })
              .state('app.page.invoice', {
                  url: '/invoice',
                  templateUrl: '../Content/tpl/page_invoice.html'
              })
              .state('app.page.price', {
                  url: '/price',
                  templateUrl: '../Content/tpl/page_price.html'
              })
              .state('app.docs', {
                  url: '/docs',
                  templateUrl: '../Content/tpl/docs.html'
              })
              // others
              .state('lockme', {
                  url: '/lockme',
                  templateUrl: '../Content/tpl/page_lockme.html'
              })
              .state('access', {
                  url: '/access',
                  template: '<div ui-view class="fade-in-right-big smooth"></div>'
              })
              .state('access.signin', {
                  url: '/signin',
                  templateUrl: '../Content/tpl/page_signin.html',
                  resolve: {
                      deps: ['uiLoad',
                        function( uiLoad ){
                            return uiLoad.load(['../Content/js/controllers/signin.js']);
                      }]
                  }
              })
              .state('access.signup', {
                  url: '/signup',
                  templateUrl: '../Content/tpl/page_signup.html',
                  resolve: {
                      deps: ['uiLoad',
                        function( uiLoad ){
                            return uiLoad.load(['../Content/js/controllers/signup.js']);
                      }]
                  }
              })
              .state('access.forgotpwd', {
                  url: '/forgotpwd',
                  templateUrl: '../Content/tpl/page_forgotpwd.html'
              })
              .state('access.404', {
                  url: '/404',
                  templateUrl: '../Content/tpl/page_404.html'
              })

              // fullCalendar
              .state('app.calendar', {
                  url: '/calendar',
                  templateUrl: '../Content/tpl/app_calendar.html',
                  // use resolve to load other dependences
                  resolve: {
                      deps: ['$ocLazyLoad', 'uiLoad',
                        function( $ocLazyLoad, uiLoad ){
                          return uiLoad.load(
                            ['../Content/vendor/jquery/fullcalendar/fullcalendar.css',
                              '../Content/vendor/jquery/fullcalendar/theme.css',
                              '../Content/vendor/jquery/jquery-ui-1.10.3.custom.min.js',
                              '../Content/vendor/libs/moment.min.js',
                              '../Content/vendor/jquery/fullcalendar/fullcalendar.min.js',
                              '../Content/js/app/calendar/calendar.js']
                          ).then(
                            function(){
                              return $ocLazyLoad.load('ui.calendar');
                            }
                          )
                      }]
                  }
              })

              // mail
              .state('app.mail', {
                  abstract: true,
                  url: '/mail',
                  templateUrl: '../Content/tpl/mail.html',
                  // use resolve to load other dependences
                  resolve: {
                      deps: ['uiLoad',
                        function( uiLoad ){
                            return uiLoad.load(['../Content/js/app/mail/mail.js',
                                               '../Content/js/app/mail/mail-service.js',
                                               '../Content/vendor/libs/moment.min.js']);
                      }]
                  }
              })
              .state('app.mail.list', {
                  url: '/inbox/{fold}',
                  templateUrl: '../Content/tpl/mail.list.html'
              })
              .state('app.mail.detail', {
                  url: '/{mailId:[0-9]{1,4}}',
                  templateUrl: '../Content/tpl/mail.detail.html'
              })
              .state('app.mail.compose', {
                  url: '/compose',
                  templateUrl: '../Content/tpl/mail.new.html'
              })

              .state('layout', {
                  abstract: true,
                  url: '/layout',
                  templateUrl: '../Content/tpl/layout.html'
              })
              .state('layout.fullwidth', {
                  url: '/fullwidth',
                  views: {
                      '': {
                          templateUrl: '../Content/tpl/layout_fullwidth.html'
                      },
                      'footer': {
                          templateUrl: '../Content/tpl/layout_footer_fullwidth.html'
                      }
                  },
                  resolve: {
                      deps: ['uiLoad',
                        function( uiLoad ){
                            return uiLoad.load(['../Content/js/controllers/vectormap.js']);
                      }]
                  }
              })
              .state('layout.mobile', {
                  url: '/mobile',
                  views: {
                      '': {
                          templateUrl: '../Content/tpl/layout_mobile.html'
                      },
                      'footer': {
                          templateUrl: '../Content/tpl/layout_footer_mobile.html'
                      }
                  }
              })
              .state('layout.app', {
                  url: '/app',
                  views: {
                      '': {
                          templateUrl: '../Content/tpl/layout_app.html'
                      },
                      'footer': {
                          templateUrl: '../Content/tpl/layout_footer_fullwidth.html'
                      }
                  },
                  resolve: {
                      deps: ['uiLoad',
                        function( uiLoad ){
                            return uiLoad.load(['../Content/js/controllers/tab.js']);
                      }]
                  }
              })
              .state('apps', {
                  abstract: true,
                  url: '/apps',
                  templateUrl: '../Content/tpl/layout.html'
              })
              .state('apps.note', {
                  url: '/note',
                  templateUrl: '../Content/tpl/apps_note.html',
                  resolve: {
                      deps: ['uiLoad',
                        function( uiLoad ){
                            return uiLoad.load(['../Content/js/app/note/note.js',
                                               '../Content/vendor/libs/moment.min.js']);
                      }]
                  }
              })
              .state('apps.contact', {
                  url: '/contact',
                  templateUrl: '../Content/tpl/apps_contact.html',
                  resolve: {
                      deps: ['uiLoad',
                        function( uiLoad ){
                            return uiLoad.load(['../Content/js/app/contact/contact.js']);
                      }]
                  }
              })
              .state('app.weather', {
                  url: '/weather',
                  templateUrl: '../Content/tpl/apps_weather.html',
                  resolve: {
                      deps: ['$ocLazyLoad',
                        function( $ocLazyLoad ){
                          return $ocLazyLoad.load(
                              {
                                  name: 'angular-skycons',
                                  files: ['../Content/js/app/weather/skycons.js',
                                          '../Content/vendor/libs/moment.min.js',
                                          '../Content/js/app/weather/angular-skycons.js',
                                          '../Content/js/app/weather/ctrl.js']
                              }
                          );
                      }]
                  }
              })
              .state('music', {
                  url: '/music',
                  templateUrl: '../Content/tpl/music.html',
                  controller: 'MusicCtrl',
                  resolve: {
                      deps: ['$ocLazyLoad',
                        function( $ocLazyLoad ){
                          return $ocLazyLoad.load([
                            'com.2fdevs.videogular', 
                            'com.2fdevs.videogular.plugins.controls', 
                            'com.2fdevs.videogular.plugins.overlayplay',
                            'com.2fdevs.videogular.plugins.poster',
                            'com.2fdevs.videogular.plugins.buffering',
                            '../Content/js/app/music/ctrl.js',
                            '../Content/js/app/music/theme.css'
                          ]);
                      }]
                  }
              })
                .state('music.home', {
                    url: '/home',
                    templateUrl: '../Content/tpl/music.home.html'
                })
                .state('music.genres', {
                    url: '/genres',
                    templateUrl: '../Content/tpl/music.genres.html'
                })
                .state('music.detail', {
                    url: '/detail',
                    templateUrl: '../Content/tpl/music.detail.html'
                })
                .state('music.mtv', {
                    url: '/mtv',
                    templateUrl: '../Content/tpl/music.mtv.html'
                })
                .state('music.mtvdetail', {
                    url: '/mtvdetail',
                    templateUrl: '../Content/tpl/music.mtv.detail.html'
                })
                .state('music.playlist', {
                    url: '/playlist/{fold}',
                    templateUrl: '../Content/tpl/music.playlist.html'
                })
      }
    ]
  );