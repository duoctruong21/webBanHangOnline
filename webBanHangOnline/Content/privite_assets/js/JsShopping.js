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

function allowOnlyNumbers(event) {
    // Get the ASCII value of the key that was pressed
    const key = event.keyCode || event.which;

    // If the key is not a number (0-9) and not a special key (e.g. Backspace)
    if (key < 48 || key > 57) {
        // Prevent the event from being executed
        event.preventDefault();
    }
}

function loadDataInTable(id) {
    var data = {
        img: $('#getImginItem').val(),
        title: $('#getTitleinItem').val(),
        price: $('#getPriceinItem').val()
    }

    $.ajax({
        type: 'post',
        url: '/gio-hang/'
    })
}