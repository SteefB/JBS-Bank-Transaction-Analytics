// TODO: Refactor and un-uglify
function showChart(elemId, caller) {
    caller.innerHTML = caller.innerHTML == "Draw me a chart..." ? "Hide this chart..." : "Draw me a chart...";

    switchClassName(document.getElementById(elemId), "hide", "show");
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