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
        debugger
        var title = $('#getTitleinItem_'+id).text()
        var price = $('#getPriceinItem_'+id).text()
        var priceSale = $('#getPriceinItemSale_' + id).text()
        var img = $('#getImginItem_' + id).attr('src')
        if (priceSale == "") {
            price = price;
        } else {
            price = priceSale;
        }
        $.ajax({
            url: '/shoppingcart/addtocart',
            type: 'POST',
            data: {
                id:id,
                title: title,
                price: price,
                quantity: quantity,
                img :img
            },
            success: function (rs) {
                if (rs.success) {
                    $('.loadTable').append(`
                        <tr id="trow_${id}" class="text-center">
                            <td>${quantity}</td>
                            <td><img width="50" src="${img}" /></td>
                            <td><a href="/chi-tiet-san-pham/@item.Alias-${id}">${title}</a></td>
                            <td>${price}</td>
                            <td><input class="form-control" type="number" id="Quantity_${id}" value="${quantity}" /></td>
                            <td>
                                <a href="#" data-id="@item.ProductId" class="btn btn-sm btn-danger btnDelete">Xóa</a>
                                <a href="#" data-id="@item.ProductId" class="btn btn-sm btn-success btnUpdate">Cập nhật</a>
                            </td>
                        </tr>

                     `);
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