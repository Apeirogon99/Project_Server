using LiteNetLib;
using LiteNetLib.Utils;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;

namespace ServerCore
{
    public class NetworkService
    {
        private NetManager              mManager;
        private EventBasedNetListener   mListener;

        private IPEndPoint              mEndPoint;
        private Func<Session>           mSessionFactory;

        public NetworkService()
        {
           
        }

        ~NetworkService()
        {

        }

        public void Init(IPEndPoint endPoint, Func<Session> sessionFactory, int register = 10, int backlog = 100)
        {

            mListener   = new EventBasedNetListener();
            mManager    = new NetManager(mListener);

            mEndPoint = endPoint;
            mSessionFactory += sessionFactory;



            mListener.ConnectionRequestEvent += request =>
            {

                //최대 연결
                if (mManager.ConnectedPeersCount < register)
                {
                    //Accept Key가 맞을 경우
                    request.AcceptIfKey("SomeConnectionKey");
                }
                else
                {
                    request.Reject();
                }
            };

            //연결 시
            mListener.PeerConnectedEvent += peer =>
            {
                Console.WriteLine("We got connection: {0}", peer);  // Show peer ip
                NetDataWriter writer = new NetDataWriter();         // Create writer class
                writer.Put("Hello client!");                        // Put some string

                SendBuffer buffer;

                peer.Send(writer, DeliveryMethod.ReliableOrdered);  // Send with reliability
            };

        }

        public void Start()
        {
            mManager.Start(mEndPoint.Port);

            while (!Console.KeyAvailable)
            {
                mManager.PollEvents();
                Thread.Sleep(15);
            }
        }

        public void Stop()
        {
            mManager.Stop();
        }
    }
}
