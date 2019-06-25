var cart = {
    init: function () {
        cart.regEvents();   
    },
    regEvents: function () {
        $('.btn-delete').off('click').on('click', function (e) {
            e.preventDefault();
            $.ajax({
                data: { id: $(this).data('id') },
                url: '/ShoppingCart/Delete',
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/ShoppingCart/Order"
                    }
                }
            })
        });
    }
}