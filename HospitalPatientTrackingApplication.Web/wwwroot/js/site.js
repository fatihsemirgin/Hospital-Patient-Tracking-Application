// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
setTimeout(function () {

    // Closing the alert
    if (document.querySelector("#liveToast") != null)
        document.querySelector("#liveToast").classList.remove("show");
}, 2500);

function limitInputLength(input) {
    var maxLength = parseInt(input.getAttribute('maxlength'));
    if (input.value.length > maxLength) {
        input.value = input.value.slice(0, maxLength);
    }
}



let footer_date = document.querySelectorAll(".footer_date");
footer_date.forEach(x => x.innerHTML = ` &copy; ${new Date().getFullYear()} - DEU Patient Tracking System - `)

var today = new Date().toISOString().split("T")[0];

// Tarih girişinin max özelliğini bugünün tarihiyle güncelle
let date_prevent = document.querySelectorAll(".prevent_future");
console.log(date_prevent)
date_prevent.forEach(x => x.setAttribute("max", today))


var now = new Date();
now.setTime(now.getTime() + 3 * 60 * 60 * 1000 + 1 * 60 * 1000);
var nowISO = now.toISOString().slice(0, 16);
document.querySelectorAll(".prevent_future_clock").forEach(x => x.setAttribute("max", nowISO));

