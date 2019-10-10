Cart = {
	_properties: {
		addToCartLink: "",
		decrementLink: "",
		removeFromCartLink: "",
		removeAllLink: "",
		getCartViewLink: ""
	},

	init: function (properties)
	{
		$.extend(Cart._properties, properties);

		Cart.initEvents();
	},

	initEvents: function ()
	{
		$("a.CallAddToCart").click(Cart.addToCart);
		$(".cart_quantity_up").click(Cart.incrementItem);
		$(".cart_quantity_down").click(Cart.decrementItem);
		$(".cart_quantity_delete").click(Cart.removeFromCart);
	},

	addToCart: function (event)
	{
		var button = $(this);
		event.preventDefault();
		var id = button.data("id");

		$.get(Cart._properties.addToCartLink + "/" + id)
			.done(function ()
			{
				Cart.showToolTip(button);
				Cart.refreshCartView();
			})
			.fail(function () { console.log("addToCart error"); });
	},

	showToolTip: function (button)
	{
		button.tooltip({ title: "Добавлено в корзину" }).tooltip("show");
		setTimeout(function ()
		{
			button.tooltip("destroy");
		}, 500);
	},

	refreshCartView: function ()
	{
		var container = $("#cartContainer");
		$.get(Cart._properties.getCartViewLink)
			.done(function (result) { container.html(result); })
			.fail(function () { console.log("refreshCartView error"); });
	},

	incrementItem: function (event)
	{
		var button = $(this);
		event.preventDefault();
		var id = button.data("id");
		var container = button.closest("tr");

		$.get(Cart._properties.addToCartLink + "/" + id)
			.done(function ()
			{
				var value = parseInt($(".cart_quantity_input", container).val());
				$(".cart_quantity_input", container).val(value + 1);
				Cart.refreshPrice(container);
				Cart.refreshCartView();
			})
			.fail(function () { console.log("incrementItem error"); });
	},

	decrementItem: function (event)
	{
		var button = $(this);
		event.preventDefault();
		var id = button.data("id");
		var container = button.closest("tr");

		$.get(Cart._properties.decrementLink + "/" + id)
			.done(function ()
			{
				var value = parseInt($(".cart_quantity_input", container).val());

				if (value > 1)
				{
					$(".cart_quantity_input", container).val(value - 1);
					Cart.refreshPrice(container);
				} else
				{
					container.remove();
					Cart.refreshTotalPrice();
				}
			})
			.fail(function () { console.log("decrementItem error"); });
	},

	removeFromCart: function (event)
	{
		var button = $(this);
		event.preventDefault();
		var id = button.data("id");

		$.get(Cart._properties.removeFromCartLink + "/" + id)
			.done(function ()
			{
				button.closest("tr").remove();
				Cart.refreshTotalPrice();
			})
			.fail(function () { console.log("removeFromCart error"); });
	},

	refreshPrice: function (container)
	{
		var quantity = parseInt($(".cart_quantity_input", container).val());
		var price = parseFloat($(".cart_price", container).data("price"));
		var totalPrice = quantity * price;

		var value = totalPrice.toLocaleString("ru-RU", { style: "currency", currency: "RUB" });

		$(".cart_total_price", container).data("price", totalPrice);
		$(".cart_total_price", container).html(value);

		Cart.refreshTotalPrice();
	},

	refreshTotalPrice: function ()
	{
		var total = 0;

		$(".cart_total_price").each(function ()
		{
			var price = parseFloat($(this).data("price"));
			total += price;
		});

		var value = total.toLocaleString("ru-RU", { style: "currency", currency: "RUB" });
		$("#totalOrderSum").html(value);
	}
};