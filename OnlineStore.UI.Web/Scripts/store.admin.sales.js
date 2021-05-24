var digit = digit || {};
digit.admon = digit.admon || {};

digit.admon.users = (function () {
    "use strict";

    var UsersLoad = function () {

        var $divUsersTable;
        var AjaxCallShowUsers;

        this.initialize = function () {
            ActBtnShowCart();

        };


     
        var ActBtnShowCart = function () {
            $("#btnShowCart").off('click').click(function (event) {
                event.preventDefault();
                ShowCart();
                alert("entro al clic");
            });
        }

       

        var ShowCart = function () {

            AjaxCallShowUsers = $.ajax({
                type: "GET",
                cache: false,
                async: true,
                url: '/Sales/Index',
                data: {},
                dataType: 'html',
                beforeSend: function () {
                    if (AjaxCallShowUsers) {
                        AjaxCallShowUsers.abort();
                    }
                },
                success: function (data) {
                    AjaxCallShowUsers = null;
                    //$divUsersTable.empty();
                    //$divUsersTable.html(data);
                },
                error: function (xhr, ajaxOptions, thrownError) {

                    AjaxCallShowUsers = null;
                },
                complete: function () {
                    //ActiveCelShowUserDetail();
                    //ActivarBtnNewUser();
                    //ActivarBtnDelete();
                    //IniDataTable();
                }
            });
        }

      

    };

    return new UsersLoad();

})();

(function ($, window, document) {
    "use strict";

    $(function () {
        digit.admon.users.initialize();
    });

}(window.jQuery, window, document));