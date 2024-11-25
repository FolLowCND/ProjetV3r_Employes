const connection = new signalR.HubConnectionBuilder()
    .withUrl("/connectionHub", { withCredentials: true })
    .build();

connection.start()
    .then(() => console.log("Connecté au hub SignalR"))
    .catch(err => console.error("Erreur lors de la connexion au hub : ", err));

connection.on("ReceiveLoginStatus", (message) => {
    console.log("Statut de connexion : " + message);
});
