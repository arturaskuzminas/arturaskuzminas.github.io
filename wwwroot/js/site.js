// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function openNav() {
    open();
    wt();
}

function open() {
    document.getElementById("mySidenav").style.width = "260px";
    document.getElementById("links").style.opacity = "0";
}

function wt() {
    setTimeout(function () {
        document.getElementById("links").style.opacity = "3";
    }, 280);
}

function openCartNav() {
    openCart();
    wtCart();
}

function openCart() {
    document.getElementById("mySideCart").style.width = "400px";
    document.getElementById("mySideCart").style.zIndex = 4;
    document.getElementById("cartText").style.opacity = "0";
}

function closeCart() {
    document.getElementById("mySideCart").style.width = "0";
    document.getElementById("cartText").style.opacity = "0";
}

function wtCart() {
    setTimeout(function () {
        document.getElementById("cartText").style.opacity = "3";
    }, 280);
}


function closeNav() {
    document.getElementById("mySidenav").style.width = "0";
    document.getElementById("links").style.opacity = "0";
    //document.getElementById("mySidenav").style.zIndex = "-1";
}

function confirmDelete(uniqueId, isDeleteClicked) {
    var deleteSpan = 'deleteSpan_' + uniqueId;
    var confirmDeleteSpan = 'confirmDeleteSpan_' + uniqueId;

    if (isDeleteClicked) {
        $('#' + deleteSpan).hide();
        $('#' + confirmDeleteSpan).show();
    } else {
        $('#' + deleteSpan).show();
        $('#' + confirmDeleteSpan).hide();
    }
}