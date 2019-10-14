Cart = {
	_properties: {
		addToCartLink: "",
		decrementLink: "",
		removeFromCartLink: "",
		removeAllLink: "",
		getCartViewLink: ""
	},

	init: properties =>
	{
		$.extend(Cart._properties, properties);

		Cart.initEvents();
	},

	initEvents: () =>
	{
		$(".add-to-cart").click(Cart.addToCart);
		$(".cart_quantity_up").click(Cart.incrementItem);
		$(".cart_quantity_down").click(Cart.decrementItem);
		$(".cart_quantity_delete").click(Cart.removeFromCart);
	},

	addToCart: function (event)
	{
		var button = $(this);
		event.preventDefault();
		const id = button.data("id");

		$.get(`${Cart._properties.addToCartLink}/${id}`)
			.done(() =>
			{
				Cart.showToolTip(button);
				Cart.refreshCartView();
			})
			.fail(() => console.log("addToCart error"));
	},

	showToolTip: button =>
	{
		button.tooltip({ title: "Добавлено в корзину" }).tooltip("show");
		setTimeout(() => button.tooltip("destroy"), 500);
	},

	refreshCartView: () =>
	{
		var container = $("#cartContainer");
		$.get(Cart._properties.getCartViewLink)
			.done(result =>  container.html(result))
			.fail(() => console.log("refreshCartView error"));
	},

	incrementItem: function (event)
	{
		const button = $(this);
		event.preventDefault();
		const id = button.data("id");
		var container = button.closest("tr");

		$.get(`${Cart._properties.addToCartLink}/${id}`)
			.done(() =>
			{
				const quantityInput = $(".cart_quantity_input", container);
				var value = parseInt(quantityInput.val());
				quantityInput.val(value + 1);
				Cart.refreshPrice(container);
				Cart.refreshCartView();
			})
			.fail(() => console.log("incrementItem error"));
	},

	decrementItem: function (event)
	{
		const button = $(this);
		event.preventDefault();
		const id = button.data("id");
		var container = button.closest("tr");

		$.get(`${Cart._properties.decrementLink}/${id}`)
			.done(() =>
			{
				const quantityInput = $(".cart_quantity_input", container);
				var value = parseInt(quantityInput.val());

				if (value > 1)
				{
					quantityInput.val(value - 1);
					Cart.refreshPrice(container);
				} else
				{
					container.remove();
					Cart.refreshTotalPrice();
				}
			})
			.fail(() => { console.log("decrementItem error"); });
	},

	removeFromCart: function (event)
	{
		var button = $(this);
		event.preventDefault();
		const id = button.data("id");

		$.get(`${Cart._properties.removeFromCartLink}/${id}`)
			.done(() =>
			{
				button.closest("tr").remove();
				Cart.refreshTotalPrice();
			})
			.fail(() => console.log("removeFromCart error"));
	},

	refreshPrice: container =>
	{
		var quantity = parseInt($(".cart_quantity_input", container).val());
		var price = parseFloat($(".cart_price", container).data("price"));
		var totalPrice = quantity * price;

		const priceInfo = $(".cart_total_price", container);
		priceInfo.data("price", totalPrice);
		priceInfo.html(totalPrice.toLocaleString("ru-RU", { style: "currency", currency: "RUB" }));

		Cart.refreshTotalPrice();
	},

	refreshTotalPrice: () =>
	{
		var total = 0;

		$(".cart_total_price").each(function () {
			total += parseFloat($(this).data("price"));
		});

		$("#totalOrderSum").html(total.toLocaleString("ru-RU", { style: "currency", currency: "RUB" }));
	}
};