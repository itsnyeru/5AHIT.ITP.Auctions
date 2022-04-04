function clearPayPal() {
    const container = document.getElementById("paypal-button-container");
    container.innerHTML = '';
}

function renderPayPal(userId, token, balance) {
    clearPayPal();
    // Render the PayPal button into #paypal-button-container
    paypal.Buttons({
        // Set up the transaction
        createOrder: function (data, actions) {
            return actions.order.create({
                purchase_units: [{
                    amount: {
                        value: balance
                    }
                }]
            });
        },

        // Finalize the transaction
        onApprove: function (data, actions) {
            return actions.order.capture().then(function (orderData) {
                // Successful capture! For demo purposes:
                let xhr = new XMLHttpRequest();
                xhr.open("POST", "./api/transaction/approve/"+userId+"/"+token);

                xhr.setRequestHeader("Accept", "application/json");
                xhr.setRequestHeader("Content-Type", "application/json");

                xhr.send();

                // Replace the above to show a success message within this page, e.g.
                const element = document.getElementById('paypal-button-container');
                element.innerHTML = '<h3>Thank you for your payment!</h3>';
            });
        }


    }).render('#paypal-button-container');
}