// TODO: Refactor and un-uglify
function toggleChartText(caller) {
    caller.innerHTML = caller.innerHTML == "Draw me a chart..." ? "Hide this chart..." : "Draw me a chart...";
}

function toggleVisibility(elemId, caller) {
    caller.innerHTML = caller.innerHTML == "Show more..." ? "Show less..." : "Show more...";

    var rows = document.getElementById(elemId).rows;
    for (i = 0; i < rows.length; i++) {
        switchClassName(rows[i], "hide_row", "show_row");
    }
}

/**
* TODO: Refactor into something proper.
**/
function switchClassName(elem, a, b) {
    if (elem.className != undefined && elem.className.indexOf(a) != -1) {
        elem.className = elem.className.replace(a, b);
    } else if (elem.className != undefined && elem.className.indexOf(b) != -1) {
        elem.className = elem.className.replace(b, a);
    }
}

$(document).ready(function () {
    $("#accordion > li > div").click(function () {
        $("#accordion > li > div").removeClass("opened");
        $(this).addClass("opened");
        if (false == $(this).next().is(':visible')) {
            $('#accordion ul').slideUp(1);
        }
        $(this).next().slideToggle(1);
    });
    $("#accordion > li > ul > li").click(function () {
        $("#accordion > li > ul > li").removeClass("strong");
        $(this).addClass("strong");
    });
	window.onscroll = function () {
        document.getElementById('content_menu').style.top = (document.body.scrollTop + 165) + "px";
    }
       
});