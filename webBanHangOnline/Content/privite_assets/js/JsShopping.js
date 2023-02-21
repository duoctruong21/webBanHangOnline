$(document).ready(function () {
    showCount();
    $('body').on('click', '.btnAddToCart', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var quantity = 1;
        var tQuantity = $('#quantity_value').text();
        if (tQuantity != '') {
            quantity = parseInt(tQuantity);
        }

        $.ajax({
            url: '/shoppingcart/addtocart',
            type: 'POST',
            data: { id: id, quantity: quantity },
            success: function (rs) {
                if (rs.success) {
                    $('.checkout_items').html(rs.count); 
                    alert(rs.msg);
                }
            }
        })
    })

    $('body').on('click', '.btnUpdate', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var quantity = $('#Quantity_' + id).val();
        var conf = confirm('Bạn có muốn cập nhật số lượng sản phẩm không?');
        if (conf === true) {
            update(id, quantity);
        }
        
    })

    $('body').on('click', '.btnDelete', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var conf = confirm('Bạn có muốn xóa sản phẩm ra khỏi giỏ hàng không?');
        if (conf === true) {
            $.ajax({
                url: '/shoppingcart/delete',
                type: 'POST',
                data: { id: id },
                success: function (rs) {
                    if (rs.success) {
                        $('.checkout_items').html(rs.count);
                        $('#trow_' + id).remove();
                        loadCart();
                    }
                }
            })
        }

    })

    $('body').on('click', '.btnDelAll', function (e) {
        e.preventDefault();
        var conf = confirm('Bạn có muốn xóa giỏ hàng không?');
        if (conf === true) {
            deleteAll();
        }

    })
})

function showCount() {
    $.ajax({
        url: '/shoppingcart/ShowCount',
        type: 'GET',
        success: function (rs) {
            $('.checkout_items').html(rs.count);
        }
    })
}

function deleteAll() {
    $.ajax({
        url: '/shoppingcart/deleteall',
        type: 'POST',
        success: function (rs) {
            if (rs.success) {
                loadCart();
                $('.checkout_items').html(rs.count=0);
            }
        }
    })
}

function update(id,quantity) {
    $.ajax({
        url: '/shoppingcart/update',
        type: 'POST',
        data: { id: id, quantity: quantity },
        success: function (rs) {
            if (rs.success) {
                loadCart();
                $('.checkout_items').html(rs.count = 0);
            }
        }
    })
}

function loadCart() {
    $.ajax({
        url: '/shoppingcart/PartialItemcart',
        type: 'GET',
        success: function (rs) {
            $('#load-data').html(rs);
        }
    })
}