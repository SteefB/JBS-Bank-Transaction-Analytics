google.load('visualization', '1');

function drawChart(transactionGroup, data, options) {
    // Create the data table.
    var data = new google.visualization.DataTable(data);

    // Instantiate and draw the chart
    var wrapper = new google.visualization.ChartWrapper({
        chartType: 'ComboChart',
        dataTable: data,
        options: options,
        containerId: "transactionChart_" + transactionGroup
    });
    wrapper.draw();
}