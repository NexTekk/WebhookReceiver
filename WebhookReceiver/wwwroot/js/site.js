'use strict';

var app = app || {};

(function (vm) {
    var payloads = [];

    vm.init = function() {

        console.info('SignalR Initializing...');

        var connection = new signalR.HubConnectionBuilder().withUrl("/sockethub").build();

        connection.on("ReceiveMessage", addMessageToTable);

        connection.start().catch(console.error);

        console.info('SignalR Initialized');

        $('div#tables-container').on('dblclick', 'tbody tr', handRowDblClick);
    }

    function addMessageToTable(payload) {
        console.log(payload);

        var event = payload.event || {
            timestampUtc: null,
            object: 'Unknown object',
            action: '',
            performedBy: { name: '' }
        };
        var index = payloads.push(payload) - 1;
        var date = new Date(event.timestampUtc);
        var timestamp = date.toLocaleString();
        var row = '';

        row += '<tr id="' + index + '">';
        row += '<td>' + event.object + '</td>';
        row += '<td>' + event.action + '</td>';
        row += '<td>' + timestamp + '</td>';
        row += '<td>' + event.performedBy.name + '</td>';
        row += '</tr >';

        $('table#webhook-posts tbody').append(row);
    }

    function handRowDblClick(e) {
        $('div#payload-modal').modal('show');

        var payloadIndex = Number(e.currentTarget.id);
        var payload = payloads[payloadIndex];
        var payloadString = JSON.stringify(payload, null, 4);

        $('#payload-content').html(payloadString);
    }
})(app);
