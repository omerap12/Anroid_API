function(){
    var connection = new signalR.HubConnectionBuilder().withUrl("/myHub").build();
    connection.start();

}