// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $('#searchInput').on('input', function () {
        var searchValue = $(this).val();
        updateCarList(searchValue);
    });
});

function updateCarList(searchValue) {
    if (searchValue.trim() !== '') {
        $.ajax({
            url: '/Home/SearchCars',
            method: 'GET',
            data: { search: searchValue },
            success: function (response) {
                // Clear the car list
                $('#carList').empty();

                // Access the car data from the `$values` array
                $.each(response.$values, function (index, car) {
                    $('#carList').append('<div><a href="/Order/CarInfo?carId=' + car.Id + '" class="btn btn-primary col-12 p-2 m-2 mt-0 mb-0">' + car.BrandName + ' - ' + car.ModelName + ' - ' + car.Price + '$</a></div>');
                });
            },
            error: function (xhr, status, error) {
                console.error(error);
            }
        });
    } else {
        // Clear the car list if the input is empty
        $('#carList').empty();
    }
}