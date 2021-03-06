﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
window.onload = function () {
    $('#users-table').DataTable({
        dom: '<"datatable-header"l><"datatable-scroll"Bft><"datatable-footer"ip>',
        bSortCellsTop: true,//делаем сортировку по верхнему хеадеру
    });
    $('#report-table').DataTable({
        dom: '<"datatable-header"l><"datatable-scroll"Bft><"datatable-footer"ip>',
        bSortCellsTop: true,//делаем сортировку по верхнему хеадеру
    });
    $('#order-table').DataTable({
        dom: '<"datatable-header"l><"datatable-scroll"Bft><"datatable-footer"ip>',
        bSortCellsTop: true,//делаем сортировку по верхнему хеадеру
    });

    $("input:not([type=hidden])").attr("maxlength", 40);
    //$("input:not([type=hidden])").attr("required", true);
}