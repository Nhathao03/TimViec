// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const myBtn = document.getElementById('myBtn');
const myOverlay = document.getElementById('myOverlay');
const closeBtn = document.getElementById('closeBtn');

function translateEN() {
    window.location.href = 'https://translate.google.com/translate?h1=en&s1=vi&t1=en&u=${window.location.href}';
}

//myBtn.addEventListener('click', function () {
//    myOverlay.style.display = 'block'; // Hiển thị overlay
//});

//closeBtn.addEventListener('click', function () {
//    myOverlay.style.display = 'none'; // Ẩn overlay
//});