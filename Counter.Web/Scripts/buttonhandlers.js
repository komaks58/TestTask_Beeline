    $('#submitCount').click(function() { //On click of your button

        var value = $('#value').val(); //Get the values from the page you want to post
        var calculationType = $('#calculationType').val();


        var jsonObject = { // Create JSON object to pass through AJAX
            value: value, //Make sure these names match the properties in VM
            calculationType: calculationType
        };

        $.ajax({ //Do an ajax post to the controller
            type: 'POST',
            url: '/Home/Count',
            data: JSON.stringify(jsonObject),
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        });
    });

    $('#submitUpdateTotal').click(function() { //On click of your button
        var res = $.ajax({ //Do an ajax post to the controller
            type: 'POST',
            url: '/Home/UpdateCurrentValue',
            success : function (response) {
                $('#currentTotalValue').val = response.val;
            }
        });
    });