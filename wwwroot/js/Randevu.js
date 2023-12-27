// randevu.js
$(document).ready(function () {
    // Attach change event to the polyclinic dropdown
    $("#PoliklinikId").change(function () {
        var polyclinicId = $(this).val();

        // Make an AJAX request to fetch doctors based on the selected polyclinic
        $.ajax({
            url: "/RandevuAl/GetDoktorlar",
            type: "POST",
            data: { poliklinikId: polyclinicId },
            success: function (data) {
                // Populate the doctor dropdown with the returned data
                var doctorsDropdown = $("#DoktorId");
                doctorsDropdown.empty();
                $.each(data, function (index, item) {
                    doctorsDropdown.append($("<option></option>").val(item.DoktorId).text(item.AdSoyad));
                });
            },
            error: function (error) {
                console.log(error);
            }
        });
    });
});
