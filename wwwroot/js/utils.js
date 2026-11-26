$(document).ready(() => {
    $.ajaxSetup({
        beforeSend: function (xhr) {
            const token = localStorage.getItem("token");

            if (token) {
                xhr.setRequestHeader(
                    "Authorization",
                    "Bearer " + token
                );
            }
        }
    });

    function checkSession() {
        const token = localStorage.getItem("token");

        if (!token) {

            if (location.pathname.toLowerCase().startsWith('user')) {
                return;
            }

            if (!location.pathname.toLowerCase().startsWith('/login') ) {
                location.href = '/login';
            }
            return;
        }

        $.ajax({
            url: '/Home/CheckToken',
            method: 'GET',
            dataType: 'json',

            error: () => {
                const currentPage =
                    location.pathname
                        .split('/')
                        .filter(Boolean)[0]
                        ?.toLowerCase() ?? '';

                if (
                    currentPage !== 'user' &&
                    !currentPage.startsWith('login')
                ) {
                    localStorage.removeItem("token");
                    location.href = '/login';
                }
            }
        });
    }

    checkSession();

    setInterval(checkSession, 60000);
});

function showDialog(html) {
    $('.content').html(html);
    $('.dialog').modal('show');
}

function openLoader() {
    $('#loader').removeClass('visually-hidden');
}

function closeLoader() {
    $('#loader').addClass('visually-hidden');
}

$("#logout").click(() => {
    localStorage.removeItem("token");
    location.href = "/login";
});