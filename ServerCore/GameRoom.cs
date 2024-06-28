using LiteNetLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerCore
{
    public abstract class GameRoom : IJobQueue
    {
        public GameRoomManager mGameRoomManager;
        public byte mGameRoomNumber;

        List<Session> mSessions = new List<Session>();
        JobQueue mJobQueue = new JobQueue();
        List<ArraySegment<byte>> mPendingList = new List<ArraySegment<byte>>();

        public abstract void Init();
        public abstract void LogicUpdate();
        public abstract void Start();
        public abstract void Stop();
        public abstract void Clear();

        //
        public void Push(Action job)
        {
            mJobQueue.Push(job);
        }

        public void Flush()
        {
            foreach (Session session in mSessions)
            {
                session.Send(mPendingList, DeliveryMethod.ReliableOrdered);
            }
            mPendingList.Clear();
        }

        public void Broadcast(ArraySegment<byte> segment)
        {
            mPendingList.Add(segment);
        }
    }
}
