var historyTable = document.getElementById("historyTable");
var accountId;

$.ajax({
    url: "/history/getaccountidforcurrentuser",
    type: "get",
    dataType: "json",
}).done(function (data) {
    accountId = data.data;
    $.ajax({
        url: "/history/getentries",
        type: "get",
        dataType: "json"
    }).done(function (historyData) {
        historyData.forEach((entry) => {
            var row = historyTable.insertRow(historyTable.rows.length);
            var historyCell0 = row.insertCell(0);
            var historyCell1 = row.insertCell(1);
            var historyCell2 = row.insertCell(2);
            var historyCell3 = row.insertCell(3);
            var historyCell4 = row.insertCell(4);
            entry.date = entry.date.replace("T", " ");
            entry.date = entry.date.slice(0, 19);
            var text0 = document.createTextNode(entry.date);
            var text1;
            var text4;
            if (entry.senderID == accountId) {
                row.classList.add("table-danger")
                text4 = document.createTextNode("Sent");
                historyCell4.appendChild(text4)
                if (entry.receiverID == 99999) {
                    text1 = document.createTextNode("SYSTEM");
                    historyCell1.appendChild(text1)
                } else {
                    $.ajax({
                        url: "/history/getusernameforaccountid",
                        type: "get",
                        data: {
                            accountId: entry.receiverID
                        },
                        dataType: "json",
                    }).done(function (username) {
                        text1 = document.createTextNode(username.data);
                        historyCell1.appendChild(text1)
                    })
                }
            } else if (entry.receiverID == accountId) {
                row.classList.add("table-success")
                text4 = document.createTextNode("Received");
                historyCell4.appendChild(text4)
                if (entry.senderID == 99999) {
                    text1 = document.createTextNode("SYSTEM");
                    historyCell1.appendChild(text1)
                } else {
                    $.ajax({
                        url: "/history/getusernameforaccountid",
                        type: "get",
                        data: {
                            accountId: entry.senderID
                        },
                        dataType: "json",
                    }).done(function (username) {
                        text1 = document.createTextNode(username.data);
                        historyCell1.appendChild(text1)
                    })
                }
            }
            var text2 = document.createTextNode(entry.amount);
            var text3 = document.createTextNode(entry.currency);
            historyCell0.appendChild(text0)
            historyCell2.appendChild(text2)
            historyCell3.appendChild(text3)
        })
    })
})