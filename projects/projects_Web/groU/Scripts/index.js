function modal(modalNum) {
    var modal = document.getElementsByClassName("modal");
    modal[modalNum].style.display = "block";
}

window.onclick = function (event) {
    var modal = document.getElementsByClassName("modal");

    if (event.target == modal[0]) {
        modal[0].style.display = "none";
    }
    else if (event.target == modal[1]) {
        modal[1].style.display = "none";
    }
}