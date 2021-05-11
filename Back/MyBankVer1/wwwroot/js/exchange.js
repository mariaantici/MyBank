function GetSelectedValue(currencyType) {
    var e = document.getElementById(currencyType);
    var text = e.options[e.selectedIndex].text;
    return text;
}

function GetExchangeRate(fromCurrency, toCurrency) {
    switch (true) {
        case (fromCurrency == "EUR" && toCurrency == "RON"):
            return 5;
        case (fromCurrency == "RON" && toCurrency == "EUR"):
            return 0.2;
        case (fromCurrency == "EUR" && toCurrency == "USD"):
            return 1.20;
        case (fromCurrency == "USD" && toCurrency == "EUR"):
            return 0.80;
        case (fromCurrency == "RON" && toCurrency == "USD"):
            return 0.25;
        case (fromCurrency == "USD" && toCurrency == "RON"):
            return 4;
        case (fromCurrency == "RON" && toCurrency == "RON"):
            return 1;
        case (fromCurrency == "USD" && toCurrency == "USD"):
            return 1;
        case (fromCurrency == "EUR" && toCurrency == "EUR"):
            return 1;
    }
}

document.getElementById('fromCurrency').onchange = function () {
    var fromCurrency = this.value;
    var toCurrency = GetSelectedValue("toCurrency");
    var amount = document.getElementById("amount").value;
    var exchangeRate = GetExchangeRate(fromCurrency, toCurrency);
    console.log(exchangeRate);
    console.log(fromCurrency);
    document.getElementById("finalAmount").value = (exchangeRate * amount).toFixed(2);
}

document.getElementById('toCurrency').onchange = function () {
    var toCurrency = this.value;
    var fromCurrency = GetSelectedValue("fromCurrency");
    var amount = document.getElementById("amount").value;
    var exchangeRate = GetExchangeRate(fromCurrency, toCurrency);
    console.log(exchangeRate);
    console.log(toCurrency);
    document.getElementById("finalAmount").value = (exchangeRate * amount).toFixed(2);
}

$("#amount").on("input", function () {
    var amount = $(this).val();
    console.log(amount);
    var toCurrency = GetSelectedValue("toCurrency");
    var fromCurrency = GetSelectedValue("fromCurrency");
    var exchangeRate = GetExchangeRate(fromCurrency, toCurrency);
    document.getElementById("finalAmount").value = (exchangeRate * amount).toFixed(2);
});

function terms_changed(termsCheckBox) {
    if (termsCheckBox.checked) {
        document.getElementById("SubmitBtn").disabled = false;
    } else {
        document.getElementById("SubmitBtn").disabled = true;
    }
}