import { logs } from "Services";

$(function () {
    const newConnection = new SignalR.HubConnectionBuilder()
        .withUrl("/MyHub")
        .withAutomaticReconnect()
        .build();

    newConnection.start();

    $('SubChat').onClick(() => {
        console.log('sending' + $('SubChat').val());
        newConnection.invoke("Changed", $('SubChat').val());
    });

    newConnection.on("ReceiveMessage", (message) => {
        const l = logs.getLog(message.from, message.to);
        l.newMessage("text", message.content, message.from);
    }
    });