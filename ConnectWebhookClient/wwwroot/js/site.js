'use strict';

var app = app || {};

(function (vm) {
    var payloads = [];

    vm.init = function() {

        console.info('SignalR Initializing...');

        var connection = new signalR.HubConnectionBuilder().withUrl("/sockethub").build();

        connection.on("ReceiveHaloMessage", function (payload) { addMessageToTable(payload, 'halo') });

        connection.on("ReceiveAuraMessage", function (payload) { addMessageToTable(payload, 'aura') });

        connection.on("ReceiveCamMessage", function (payload) { addMessageToTable(payload, 'cam') });

        connection.on("ReceiveClientBaseMessage", function (payload) { addMessageToTable(payload, 'clientbase') });

        connection.start().catch(console.error);

        console.info('SignalR Initialized');

        $('div#tables-container').on('dblclick', 'tbody tr', handRowDblClick);
    }

    function addMessageToTable(payload, tablePrefix) {
        cleanupPayload(payload);

        var index = payloads.push(payload) - 1;
        var objectName = (payload.request || {}).title || payload.artifact.fileName;
        var date = new Date(payload.event.timestampUtc);
        var timestamp = date.toLocaleString();
        var row = '';

        row += '<tr id="' + index + '">';
        row += '<td>' + payload.event.object + '</td>';
        row += '<td>' + payload.event.action + '</td>';
        row += '<td>' + objectName + '</td>';
        row += '<td>' + timestamp + '</td>';
        row += '<td>' + payload.event.actionedBy.displayName + '</td>';
        row += '</tr >';

        $('table#' + tablePrefix + '-table tbody').append(row);
    }

    function handRowDblClick(e) {
        $('div#payload-modal').modal('show');

        var payloadIndex = Number(e.currentTarget.id);
        var payload = payloads[payloadIndex];
        var payloadString = JSON.stringify(payload, null, 4);

        $('#payload-content').html(payloadString);
    }

    function cleanupPayload(payload) {
        if (!payload.artifact) {
            delete payload.artifact;
        }

        if (!payload.request) {
            delete payload.request;
        }
    }
})(app);
