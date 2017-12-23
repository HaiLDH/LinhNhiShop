var cart = {
    init: function () {
        cart.loadData();
        cart.registerEvent();
    },
    registerEvent: function () {
        $('#frmPayment').validate({
            rules: {
                name: "required",
                address: "required",
                email: {
                    required: true,
                    email: true
                },
                phone: {
                    required: true,
                    number: true
                }
            },
            messages: {
                name: "Yêu cầu nhập tên",
                address: "Yêu cầu nhập địa chỉ",
                email: {
                    required: "Bạn cần nhập email",
                    email: "Định dạng email chưa đúng"
                },
                phone: {
                    required: "Yêu cầu nhập số điện thoại",
                    number: "Số điện thoại chưa đúng"
                }
            }
        });

        $('.btnDeleteItem').off('click').on('click', function (e) {
            e.preventDefault();
            var productId = parseInt($(this).data('id'));
            cart.deleteItem(productId);
        });
        $('.txtQuantity').off('keyup').on('keyup', function () {
            var quantity = parseInt($(this).val());
            var productid = parseInt($(this).data('id'));
            var price = parseFloat($(this).data('price'));

            if (isNaN(quantity) == false) {
                var amount = quantity * price;

                $('#amount_' + productid).text(numeral(amount).format('0,0'));
            }
            else {
                $('#amount_' + productid).text(0);
            }
            $('#lblTotalOrder').text(numeral(cart.getTotalOrder()).format('0,0'));

            cart.UpdateAll();
        });

        $('#btnContinue').off('click').on('click', function (e) {
            e.preventDefault();
            window.location.href = "/";
        });
        $('#btnDelete').off('click').on('click', function (e) {
            e.preventDefault();
            cart.deleteAll();
        });
        $('#btnCheckout').off('click').on('click', function (e) {
            e.preventDefault();
            $('#divCheckout').show();
        });
        $('#chkUserLoginInfor').off('click').on('click', function () {
            if ($(this).prop('checked')) {
                cart.getLoginUser();
            }
            else {
                $('#txtName').val('');
                $('#txtAddress').val('');
                $('#txtEmail').val('');
                $('#txtPhone').val('');
            }
        });
        $('#btnCreateOrder').off('click').on('click', function (e) {
            e.preventDefault();
            var isValid = $('#frmPayment').valid();
            if (isValid)
                cart.createOrder();
        });
    },

    deleteItem: function (productId) {
        $.ajax({
            url: '/ShoppingCart/DeleteItem',
            type: 'POST',
            dataType: 'json',
            data: {
                productId: productId
            },
            success: function (res) {
                if (res.status) {
                    cart.loadData();
                }
            }
        });
    },
    loadData: function () {
        $.ajax({
            url: '/ShoppingCart/GetAll',
            type: 'GET',
            dataType: 'json',
            success: function (res) {
                if (res.status) {
                    var template = $("#tplCart").html();
                    var html = '';
                    var data = res.data;
                    $.each(data, function (i, item) {
                        html += Mustache.render(template, {
                            ProductId: item.ProductId,
                            ProductName: item.Product.Name,
                            Image: item.Product.Image,
                            Price: item.Product.Price,
                            PriceF: numeral(item.Product.Price).format('0,0'),
                            Quantity: item.Quantity,
                            Amount: numeral(item.Quantity * item.Product.Price).format('0,0')
                        });
                    });
                    $('#cartBody').html(html);

                    if (html == '') {
                        $('#cartContent').html('Không có sản phẩm nào trong giỏ hàng.');
                    }

                    $('#lblTotalOrder').text(numeral(cart.getTotalOrder()).format('0,0'));
                    cart.registerEvent();
                }
            }
        })
    },
    getTotalOrder: function () {
        var listTextBox = $('.txtQuantity');
        var total = 0;
        $.each(listTextBox, function (i, item) {
            total += parseInt($(item).val() * $(item).data('price'));
        });
        return total;
    },

    deleteAll: function () {
        $.ajax({
            url: '/ShoppingCart/DeleteAll',
            type: 'POST',
            dataType: 'json',
            success: function (res) {
                if (res.status) {
                    cart.loadData();
                }
            }
        });
    },
    UpdateAll: function () {
        var cartList = [];
        $.each($('.txtQuantity'), function (i, item) {
            cartList.push({
                ProductId: $(item).data('id'),
                Quantity: $(item).val()
            });
        });
        $.ajax({
            url: '/ShoppingCart/UpdateCart',
            type: 'POST',
            dataType: 'json',
            data: {
                cartData: JSON.stringify(cartList)
            },
            success: function (res) {
                if (res.status) {
                    console.log('Update cart success');
                }
            }
        });
    },
    getLoginUser: function () {
        $.ajax({
            url: '/ShoppingCart/GetUser',
            type: 'POST',
            dataType: 'json',
            success: function (res) {
                if (res.status) {
                    var user = res.data;

                    $('#txtName').val(user.FullName);
                    $('#txtAddress').val(user.Address);
                    $('#txtEmail').val(user.Email);
                    $('#txtPhone').val(user.PhoneNumber);
                }
            }
        });
    },
    createOrder: function () {
        var order = {
            CustomerName: $('#txtName').val(),
            CustomerEmail: $('#txtEmail').val(),
            CustomerAddress: $('#txtAddress').val(),
            CustomerMobile: $('#txtPhone').val(),
            CustomerMessage: $('#txtMessage').val(),
            Paymentmethod: "Thanh toán qua chuyển khoản.",
            Status: false
        }

        $.ajax({
            url: '/ShoppingCart/CreateOrder',
            type: 'POST',
            dataType: 'json',
            data: {
                orderViewModel: JSON.stringify(order)
            },
            success: function (res) {
                if (res.status) {
                    $('#divCheckout').hide();
                    cart.deleteAll();
                    setTimeout(function () {
                        $('#cartContent').html("Cảm ơn bạn đã đặt hàng thành công, chúng tôi sẽ sớm liên hệ lại với bạn.");
                    }, 1000);
                }
            }
        });
    }
}
cart.init();