<link href="../../assets/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="../../assets/css/font-awesome.min.css" />

    <!--[if IE 7]>
      <link rel="stylesheet" href="../../assets/css/font-awesome-ie7.min.css" />
    <![endif]-- >
    < !--page specific plugin styles-- >
    < !--fonts -->

    <link rel="stylesheet" href="~/assets/font/fontawesome-webfont.woff" />

    <link rel="stylesheet" href="~/assets/css/jquery-ui-1.10.3.full.min.css" />


    <link href="~/assets/global/plugins/bootstrap-datetimepicker/css/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="~/assets/css/ui.jqgrid.css" />


    <!--< link rel = "stylesheet" href = "https://fonts.css.network/css?family=Open+Sans:400,300" /> -->
    < !--ace styles-- >
    @RenderSection("styles", required: false)

    <link rel="stylesheet" href="~/assets/css/ace.min.css" />
    <link rel="stylesheet" href="~/assets/css/ace-rtl.min.css" />
    <link rel="stylesheet" href="~/assets/css/ace-skins.min.css" />

    <!--[if lte IE 8]>
      <link rel="stylesheet" href="../../assets/css/ace-ie.min.css" />
    <![endif]-- >
    < !--inline styles related to this page-- >
    < !--ace settings handler-- >

    <script src="~/assets/js/ace-extra.min.js"></script>

    <!--HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries-- >
    < !--[if lt IE 9]>
    <script src="../../assets/js/html5shiv.js"></script>
    <script src="../../assets/js/respond.min.js"></script>
    <![endif]-- >
    <script src="../../assets/js/jquery-2.0.3.min.js"></script>

@* 右下角提示栏 - begin *@
    <script src="~/assets/global/plugins/bootstrap-toastr/toastr.js" type="text/javascript"></script>
    <script src="~/assets/global/plugins/bootstrap-toastr/p4.toastr.js" type="text/javascript"></script>
    <link href="~/assets/global/plugins/bootstrap-toastr/toastr.css" rel="stylesheet" />
@* 右下角提示栏 - end *@


    @* 提示框 - begin *@
    <script src="~/assets/global/plugins/jquery-sweetalert/sweetalert-dev.js"></script>
    <link rel="stylesheet" href="~/assets/global/plugins/jquery-sweetalert/sweetalert.css" />
@* 提示框 - end *@

    <script type="text/javascript">
    window.add = false;
    window.edit = false;
    window.delete = false;
    window.search = false;
    window.refresh = false;
    window.view = false;
    window.rowedit = true;
    window.field1 = false;
    window.field2 = false;
    window.field3 = false;
    window.field4 = false;
    @ViewBag.TenantField
    @ViewBag.Permissions
        if (window.edit == true && window.delete == true) {
        window.rowedit = false;
        }
    </script>