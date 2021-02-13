$(function () {
    $('[data-toggle="tooltip"]').tooltip();
    $('[data-toggle="popover"]').popover({
        placement: 'bottom',
        content: function () {
            return $("#notification-content").html();
        },
        html: true
    });

    $('body').append('<div id="notification-content" class ="hide"></div>')

    function getNotification() {
        var res = "<ul class='list-group'>";
        $.ajax({
            url: "/Administrator/Notification/GetNotification",
            method: "GET",
            success: function (result) {

                if (result.count != 0) {
                    $("#notificationCount").html(result.count);
                    $("#notificationCount").show('slow');
                }
                else {
                    $("#notificationCount").html();
                    $("#notificationCount").hide('slow');
                    $("#notificationCount").popover('hide');
                }


                var notifications = result.userNotifications;
                notifications.forEach(element => {
                    res = res + "<li class='list-group-item notification-text' data-id='" + element.notification.id +"'>" + element.notification.text + "</li>";
                });

                res = res + "</ul>";

                $("#notification-content").html(res);

                console.log(result);
            },
            error: function (error) {
                console.log(error);
            }
        });
    }

    $("ul").on('click', 'li.notification-text', function (e) {
        var target = e.target;
        var id = $(target).data('id');

        readNotification(id, target);
    })

    function readNotification(id, target) {
        $.ajax({
            url: "/Administrator/Notification/ReadNotification",
            method: "GET",
            data: { notificationId: id },
            success: function (result) {
                getNotification();
                $(target).fadeOut('slow');
            },
            error: function (error) {
                console.log(error);
            }
        })
    }

    getNotification();

    var connection = new signalR.HubConnectionBuilder().withUrl("/notificationHub").build();

    connection.on("sendToUser", () => {
        getNotification();
    });

    connection.start().catch(function (err) {
        return console.error(err.toString())
    });

});