'use strict';

(function () {

    function init() {

        console.info('SignalR Initializing...');

        var connection = new signalR.HubConnectionBuilder().withUrl("/sockethub").build();

        connection.on("ReceiveHaloMessage", function (message) { addMessageToTable(message, 'halo') });

        connection.on("ReceiveAuraMessage", function (message) { addMessageToTable(message, 'aura') });

        connection.on("ReceiveCamMessage", function (message) { addMessageToTable(message, 'cam') });

        connection.on("ReceiveClientBaseMessage", function (message) { addMessageToTable(message, 'clientbase') });

        connection.start().catch(console.error);

        console.info('SignalR Initialized');
    }

    function addMessageToTable(message, tablePrefix) {
        console.log(message);

        var objectName = (message.request || {}).title;

        objectName = objectName || message.artifact.fileName;

        var date = new Date(message.event.timestampUtc);
        var timestamp = date.toLocaleString();

        var row = `<tr><td>${message.event.object}</td><td>${message.event.action}</td><td>${objectName}</td><td>${timestamp}</td><td>${message.event.actionedBy.displayName}</td></tr>`;

        $(`table#${tablePrefix}-table tbody`).append(row);
    }

    init();
})();
