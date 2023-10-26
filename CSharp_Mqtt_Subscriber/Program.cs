// See https://aka.ms/new-console-template for more information

using System.Text;
using MQTTnet;
using MQTTnet.Client;

Console.WriteLine("Hello, World!");

var broker = "10.126.128.158";
var port = 1883;
var clientId = "NicolaiLaptop";
var topic = "xovis/data";
// var topic = "ubisense/data";
var username = "semantic";
var password = "s3mant1c";

// Create a MQTT client factory
var factory = new MqttFactory();

// Create a MQTT client instance
var mqttClient = factory.CreateMqttClient();

// Create MQTT client options
var options = new MqttClientOptionsBuilder()
    .WithTcpServer(broker, port) // MQTT broker address and port
    .WithCredentials(username, password) // Set username and password
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
  mqttClient.ApplicationMessageReceivedAsync += async e =>
  {
    // Decode the received message
    string receivedMessage = Encoding.UTF8.GetString(e.ApplicationMessage.PayloadSegment);

    Console.WriteLine($"Received message: {receivedMessage}");
    Console.WriteLine();

    // Define the file path
    string filePath = "./receivedDataUbisense.json";

    try
    {
      // Save the received message to a JSON file
      await File.WriteAllTextAsync(filePath, receivedMessage);
      Console.WriteLine($"Saved message to: {filePath}");
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Failed to save message to file. Error: {ex.Message}");
    }
  };
}


Console.ReadLine();