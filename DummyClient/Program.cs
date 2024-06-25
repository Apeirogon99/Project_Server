using LiteNetLib;
using ServerCore;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace DummyClient
{
	

	class Program
	{
		static void Main(string[] args)
		{

            EventBasedNetListener listener = new EventBasedNetListener();
            NetManager client = new NetManager(listener);
            client.Start();
            client.Connect("localhost" /* host ip or name */, 9050 /* port */, "SomeConnectionKey" /* text key or NetDataWriter */);
            listener.NetworkReceiveEvent += (fromPeer, dataReader, deliveryMethod, channel) =>
            {
                Console.WriteLine("We got: {0}", dataReader.GetString(100 /* max length of string */));
                dataReader.Recycle();
            };

            while (!Console.KeyAvailable)
            {
                client.PollEvents();
                Thread.Sleep(15);
            }

            client.Stop();

            // DNS (Domain Name System)
            //string host = Dns.GetHostName();
            //IPHostEntry ipHost = Dns.GetHostEntry(host);
            //IPAddress ipAddr = ipHost.AddressList[0];
            //IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);

            //Connector connector = new Connector();

            //connector.Connect(endPoint, 
            //	() => { return SessionManager.Instance.Generate(); },
            //	500);

            //while (true)
            //{
            //	try
            //	{
            //		SessionManager.Instance.SendForEach();
            //	}
            //	catch (Exception e)
            //	{
            //		Console.WriteLine(e.ToString());
            //	}

            //	Thread.Sleep(250);
            //}
        }
	}
}
