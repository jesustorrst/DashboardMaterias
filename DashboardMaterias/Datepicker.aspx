<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Datepicker.aspx.cs" Inherits="DashboardMaterias.Datepicker" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%--    <script type="text/javascript" src="/scripts/jquery-3.4.1.min.js"></script>
    <script type="text/javascript" src="/scripts/moment.min.js"></script>
    <script type="text/javascript" src="/scripts/bootstrap-datetimepicker.js"></script>
    <link href="Content/bootstrap-datetimepicker.css" rel="stylesheet">--%>

    <link href="Content/Datepicker/bootstrap-datetimepicker.min.css" rel="stylesheet">
    <link href="Content/Datepicker/bootstrap.min.css" rel="stylesheet">
    <link href="Content/Datepicker/font-awesome.min.css" rel="stylesheet">
    <script type="text/javascript" src="Content/Datepicker/bootstrap-datetimepicker.min.js"></script>
    <script type="text/javascript" src="Content/Datepicker/bootstrap.min.js"></script>
    <script type="text/javascript" src="Content/Datepicker/jquery.min.js"></script>
    <script type="text/javascript" src="Content/Datepicker/moment.min.js"></script>
    <script type="text/javascript" src="Content/Datepicker/nl.js"></script>

    <div class="container">
        
        <div class="row">
            <div class="col-xs-4">
                <h3>Date</h3>
                <div class="form-group">
                    <div class="input-group datepicker">
                        <input type="text" class="form-control" readonly>
                        <span class="input-group-addon">
                            <span class="fa fa-calendar"></span>
                        </span>
                    </div>
                </div>
            </div>
            <div class="col-xs-4">
                <h3>Time</h3>
                <div class="form-group">
                    <div class="input-group timepicker">
                        <input type="text" class="form-control" readonly>
                        <span class="input-group-addon">
                            <span class="fa fa-clock-o"></span>
                        </span>
                    </div>
                </div>
            </div>
            <div class="col-xs-4">
                <h3>Date & Time</h3>
                <div class="form-group">
                    <div class="input-group datetimepicker">
                        <input type="text" class="form-control" readonly>
                        <span class="input-group-addon">
                            <span class="fa fa-calendar"></span>
                            +
						<span class="fa fa-clock-o"></span>
                        </span>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <style>
        
    </style>
    <script>
        var defaults = {
            calendarWeeks: true,
            showClear: true,
            showClose: true,
            allowInputToggle: true,
            useCurrent: false,
            ignoreReadonly: true,
            minDate: new Date(),
            toolbarPlacement: 'top',
            locale: 'nl',
            icons: {
                time: 'fa fa-clock-o',
                date: 'fa fa-calendar',
                up: 'fa fa-angle-up',
                down: 'fa fa-angle-down',
                previous: 'fa fa-angle-left',
                next: 'fa fa-angle-right',
                today: 'fa fa-dot-circle-o',
                clear: 'fa fa-trash',
                close: 'fa fa-times'
            }
        };

        $(function () {
            var optionsDatetime = $.extend({}, defaults, { format: 'DD-MM-YYYY HH:mm' });
            var optionsDate = $.extend({}, defaults, { format: 'DD-MM-YYYY' });
            var optionsTime = $.extend({}, defaults, { format: 'HH:mm' });

            $('.datepicker').datetimepicker(optionsDate);
            $('.timepicker').datetimepicker(optionsTime);
            $('.datetimepicker').datetimepicker(optionsDatetime);
        });
    </script>
</asp:Content>
