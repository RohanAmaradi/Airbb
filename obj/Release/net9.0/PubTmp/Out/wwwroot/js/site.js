// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
    $('#ActiveCheckInDate').daterangepicker({
        singleDatePicker: true,
        showDropdowns: true,
        locale: {
            format: 'MM/DD/YYYY'
        },
        minYear: new Date().getFullYear(),
        maxYear: new Date().getFullYear() + 4
    });

    $('#ActiveCheckOutDate').daterangepicker({
        singleDatePicker: true,
        showDropdowns: true,
        locale: {
            format: 'MM/DD/YYYY'
        },
        minYear: new Date().getFullYear(),
        maxYear: new Date().getFullYear() + 4
    });
});