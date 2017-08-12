# ByteNetwork [![Build Status](https://travis-ci.org/Ryozuki/ByteNetwork.svg?branch=master)](https://travis-ci.org/Ryozuki/ByteNetwork)
A simple C# networking library, UDP.

## Example Server

```csharp
static NetServer server;

static void Main(string[] args)
{
	Console.WriteLine("Server started");
	server = new NetServer(8706);
	server.OnRecieve += Server_OnRecieve;

	Thread ListenThread = new Thread(server.Listen);
	ListenThread.Start();
}

private static void Server_OnRecieve(System.Net.IPEndPoint address, NetPacket packet)
{
	var reader = new NetPacketReader(packet);
	var type = reader.Read<byte>();
	if (type == 1)
	{
		string result = reader.Read<string>();
		Console.WriteLine("Recieved data from {0}:{1}: {2}", address.Address, address.Port, result);

		NetPacket send_packet = new NetPacket();
		send_packet.Write((byte)1);
		send_packet.Write("Hello client");
		server.Send(address, send_packet);
	}
}
```

## Example Client

```csharp
static void Main(string[] args)
{
	Console.WriteLine("Client started");
	Thread.Sleep(100);
	NetClient client = new NetClient("127.0.0.1", 8706);
	client.OnRecieve += Client_OnRecieve;

	Thread ClientThread = new Thread(client.Listen);
	ClientThread.Start();

	NetPacket send_packet = new NetPacket();
	send_packet.Write((byte)1);
	send_packet.Write("Hello world!");
	client.Send(send_packet);
}

private static void Client_OnRecieve(NetPacket packet)
{
	var reader = new NetPacketReader(packet);
	var type = reader.Read<byte>();
	if (type == 1)
	{
		string result = reader.Read<string>();
		Console.WriteLine("Recieved data: {0}", result);
	}
}
```
