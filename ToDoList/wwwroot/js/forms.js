$(document).ready(function () {
    $(document).on('submit', '.post-form', function (event) {
        event.preventDefault();

        const formClass = '.' + $(this).attr('class');

        $.ajax({
            url: $(this).attr('action'),
            type: 'POST',
            dataType: 'json',
            data: $(this).serialize(),
            success: function (response) {
                if (response.success === true && response.href) {
                    window.location.href = response.href;
                }
                console.error('Incorrect response format from the server.');
            },
            error: function (xhr) {
                if (xhr.getResponseHeader('Content-Type').indexOf('html') !== -1) {
                    const responseHtml = $(xhr.responseText);
                    const form = responseHtml.find(formClass);

                    $(formClass).replaceWith(form);
                } else {
                    console.error('Error executing form.');
                }
            }
        });
    });

    $(document).on('submit', '.put-form', function (event) {
        event.preventDefault();

        const formClass = '.' + $(this).attr('class');

        $.ajax({
            url: $(this).attr('action'),
            type: 'PUT',
            dataType: 'json',
            data: $(this).serialize(),
            success: function (response) {
                if (response.success === true && response.href) {
                    window.location.href = response.href;
                }
                console.error('Incorrect response format from the server.');
            },
            error: function (xhr) {
                if (xhr.getResponseHeader('Content-Type').indexOf('html') !== -1) {
                    const responseHtml = $(xhr.responseText);
                    const form = responseHtml.find(formClass);

                    $(formClass).replaceWith(form);
                } else {
                    console.error('Error executing form.');
                }
            }
        });
    });

    $(document).on('submit', '.delete-form', function (event) {
        event.preventDefault();

        const formClass = '.' + $(this).attr('class');

        $.ajax({
            url: $(this).attr('action'),
            type: 'DELETE',
            dataType: 'json',
            data: $(this).serialize(),
            success: function (response) {
                if (response.success === true && response.href) {
                    window.location.href = response.href;
                }
                console.error('Incorrect response format from the server.');
            },
            error: function (xhr) {
                if (xhr.getResponseHeader('Content-Type').indexOf('html') !== -1) {
                    const responseHtml = $(xhr.responseText);
                    const form = responseHtml.find(formClass);

                    $(formClass).replaceWith(form);
                } else {
                    console.error('Error executing form.');
                }
            }
        });
    });
});