using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LiteNetLib;
using ServerCore;

namespace Server
{
	class Program
	{
		//static Listener _listener = new Listener();
		//public static GameRoom Room = new GameRoom();
		public static NetworkService mNetworkService = new NetworkService();

		//static void FlushRoom()
		//{
		//	Room.Push(() => Room.Flush());
		//	JobTimer.Instance.Push(FlushRoom, 250);
		//}

		static void Main(string[] args)
		{
            // DNS (Domain Name System)
            string host = Dns.GetHostName();
            IPHostEntry ipHost = Dns.GetHostEntry(host);
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint endPoint = new IPEndPoint(ipAddr, 9050);

            Func<Session> session = () => { return SessionManager.Instance.Generate(); };
            mNetworkService.Init(endPoint, session);
            mNetworkService.Start();
            mNetworkService.Stop();

            //_listener.Init(endPoint, () => { return SessionManager.Instance.Generate(); });
            //Console.WriteLine("Listening...");

            ////FlushRoom();
            //JobTimer.Instance.Push(FlushRoom);

            //while (true)
            //{
            //	JobTimer.Instance.Flush();
            //}
        }
	}
}
