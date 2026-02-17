// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function toggleStar(id) {
    // 1. Controller'a istek at
    $.get("/Message/ChangeStarStatus/" + id, function () {

        // 2. İlgili ikonu seç
        var icon = $("#star-icon-" + id);

        // 3. İkonu değiştir (Dolu <-> Boş)
        if (icon.hasClass("far")) {
            // Boştu, doldur (Yıldızla)
            icon.removeClass("far").addClass("fas");
        } else {
            // Doluydu, boşalt (Yıldızı Kaldır)
            icon.removeClass("fas").addClass("far");

            // EKSTRA: Eğer şu an "Starred" sayfasındaysak, yıldızı kaldırılan mesajı listeden sil (Efektli)
            if (window.location.pathname.toLowerCase().includes("starred")) {
                icon.closest("tr").fadeOut(500, function () { $(this).remove(); });
            }
        }
    }).fail(function () {
        alert("İşlem sırasında bir hata oluştu.");
    });
}