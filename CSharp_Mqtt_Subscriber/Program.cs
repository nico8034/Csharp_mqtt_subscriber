// See https://aka.ms/new-console-template for more information

using System.Text;
using MQTTnet;
using MQTTnet.Client;

Console.WriteLine("Hello, World!");

var broker = "127.0.0.1";
var port = 1883;
var clientId = "Nicolai Desktop";
var topic = "XovisData";
// var username = "emqxtest";
// var password = "******";

// Create a MQTT client factory
var factory = new MqttFactory();

// Create a MQTT client instance
var mqttClient = factory.CreateMqttClient();

// Create MQTT client options
var options = new MqttClientOptionsBuilder()
    .WithTcpServer(broker, port) // MQTT broker address and port
                                 // .WithCredentials(username, password) // Set username and password
    .WithClientId(clientId)
    .WithCleanSession()
    .Build();

var connectResult = await mqttClient.ConnectAsync(options);

if (connectResult.ResultCode == MqttClientConnectResultCode.Success)
{
  Console.WriteLine("Connected to MQTT broker successfully.");

  // Subscribe to a topic
  await mqttClient.SubscribeAsync(topic);

  // Callback function when a message is received
  mqttClient.ApplicationMessageReceivedAsync += e =>
  {

    // Console.WriteLine($"Received message: {Encoding.UTF8.GetString(e.ApplicationMessage.PayloadSegment)}");
    Console.WriteLine($"Received message: {Encoding.UTF8.GetString(e.ApplicationMessage.PayloadSegment)}");
    Console.WriteLine();
    return Task.CompletedTask;
  };
};

Console.ReadLine();