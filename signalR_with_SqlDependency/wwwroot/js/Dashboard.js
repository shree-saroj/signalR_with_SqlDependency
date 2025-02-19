"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/dashboardHub").build();

$(function () {
    connection.start().then(function () {
        InvokeProducts();
        InvokeSales();
    }).catch(function (err) {
        return console.error(err.toString());
    });
});

// For Products Section
function InvokeProducts() {
    connection.invoke("sendProduct").catch(function (err) {
        console.error(err.toString());
    });
}

connection.on("ReceivedProducts", function (products) {
    BindProductsToGrid(products);
});

function BindProductsToGrid(products) {
    $("#tblProduct tbody").empty();
    var tblProductDom = "";
    $.each(products, function (index, product) {
        tblProductDom += "<tr><td>" + product.productId + "</td><td>" + product.productName + "</td><td>" + product.productCategory + "</td><td>" + product.productPrice + "</td></tr>";
    });

    $("#tblProduct tbody").append(tblProductDom);
}

// For Sales Section

function InvokeSales() {
    connection.invoke("sendSales").catch(function (err) {
        console.error(err.toString());
    });
}

connection.on("ReceivedSales", function (sales) {
    BindSalesInfo(sales);
});

function BindSalesInfo(sales) {
    $("#tblSales tbody").empty();
    var tblSalesDom = "";
    $.each(sales, function (index, sale) {
        tblSalesDom += `<tr>
            <td>${sale.saleId}</td>
            <td>${sale.customer}</td>
            <td>${sale.saleAmount}</td>
            <td>${sale.saleDate}</td>
        </tr>`;
    });
    $("#tblSales tbody").append(tblSalesDom);
}