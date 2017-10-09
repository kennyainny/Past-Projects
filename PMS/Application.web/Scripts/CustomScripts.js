
//$(document).ready(function () {

//    $(".tip").tooltip();

//    $("[rel=tooltip]").tooltip();

//    $(".sidebar-nav > .nav-list > li").css("class","active");
//    });

    //$(".sidebar-nav > .nav-list > li").click(function () {
    //    $(this).addClass("active")
    //});


//$("nav li a").click(function()
//{
//    $(this).toggleClass(active, passive)
//});
//.sidebar-nav .nav-list > li
//$('.datatable').dataTable({
//    "sDom": "<'row-fluid'<'span6'l><'span6'f>r>t<'row-fluid'<'span12'i><'span12 center'p>>",
//    "sPaginationType": "bootstrap",
//    "oLanguage": {
//        "sLengthMenu": "_MENU_ records per page"
//    }
//});




// <script type="text/javascript">

//        //var deleteLinkObj;
//        var targetUrl;

////delete Link
//$('#delete-all').click(function () {
//    //deleteLinkObj = $(this); // for future use
//    targetUrl = $(this).attr("href");
//    $('#delete-dialog').dialog('open');
//    return false;
//});

//$('#delete-dialog').dialog({
//    resizeable: false, modal: true, autoOpen: false, //dialog options - height:140, width: 400, 
//    buttons: {
//        "Delete All": function () {
//            window.location.href = targetUrl;
//            //$.post(deleteLinkObj[0].href)
//            $(this).dialog("close");
//        },
//        "Cancel": function () { $(this).dialog("close"); }
//    }
//});
//</script>




//(function () {
//    var isEmpty = true;
//    var no_of_pages = 1;

//    $('#more-button').on('click', function () {
//        if (isEmpty) {
//            $('div#loading').html('<p><img src="/Images/imgs/loading.gif"></p>');
//            $.get("/Home/Index/" + no_of_pages, function (data) {
//                if (data == null || data == "") {
//                    isEmpty = false;
//                    $('#more-button').hide();
//                    $('div#loading').html('<h3>...End...</h3><hr />');
//                }
//                else {
//                    isEmpty = true;
//                    $("#probFeed").append(data);
//                    no_of_pages++;
//                    $('div#loading').empty();
//                }

//            });
//        }
//    });
//})();

//$(document).ready(HideProblem());

//function HideProblem() {
//    $('a.hide-probs').t
//}

//$('a.more-btn').on(onclick, function () {
//(function () {
//    $('.more-btn').click(function () {
//        var $this = $(this).siblings(".prob-color");
//            fulp = $this.data('fp');
//            $this.text(fulp)
//        .end()
//        .hide();
//    });
//    //$(this).closest('p.prob-color').text("hurray, it works"); //closest
//})();

//$('span#load-more').click(loadProblems());

//$(window).scroll(function () {
//    if ($(window).scrollTop() == $(document).height() - $(window).height()) {

//        loadProblems();
//    }
//});