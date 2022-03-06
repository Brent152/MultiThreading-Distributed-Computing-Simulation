using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// CSE 445 Brent Julius 03/04/2022

namespace CSE445SAssignment2
{
    internal class OrderClass
    {
        private string senderId, receiverId;
        private Int32 cardNo, numSeats;

        private ReaderWriterLock rwlock = new ReaderWriterLock();

        private Random rng = new Random();

        public OrderClass(string senderId, string receiverId)
        {
            //Console.WriteLine("MADE AN ORDER IN ORDERCLASS");
            this.senderId = senderId;
            this.receiverId = receiverId;
            this.cardNo = rng.Next(5000, 7200); // Set random card number
            this.numSeats = rng.Next(1, 5); // Set random number of seats to be ordered
        }
        
        // ToString
        public override string ToString()
        {
            return (senderId + "|" + receiverId + "|" + cardNo + "|" + numSeats);
        }
        
        // Setters
        public void setCardNo(Int32 cardNo)
        {
            try
            {
                //Console.WriteLine(Thread.CurrentThread.Name + " trying to set CARD NUMBER");
                rwlock.AcquireWriterLock(500);
                //Console.WriteLine(Thread.CurrentThread.Name + " successfully set CARD NUMBER");
                this.cardNo = cardNo;
            } finally
            {
                rwlock.ReleaseWriterLock();
            }
        }
        public void setNumSeats(Int32 numSeats)
        {
            try
            {
                //Console.WriteLine(Thread.CurrentThread.Name + " trying to set NUM SEATS");
                rwlock.AcquireWriterLock(500);
                //Console.WriteLine(Thread.CurrentThread.Name + " successfully set NUM SEATS");
                this.numSeats = numSeats;
            } finally
            {
                rwlock.ReleaseWriterLock();
            }
        }
        
        // Getters
        public string getSenderId()
        {
            try
            {
                //Console.WriteLine(Thread.CurrentThread.Name + " trying to get SENDER ID");
                rwlock.AcquireReaderLock(500);
                //Console.WriteLine(Thread.CurrentThread.Name + " successfully got SENDER ID");
                return this.senderId;
            } finally
            {
                rwlock.ReleaseReaderLock();
            }
        }
        public string getReceiverId()
        {
            try
            {
                //Console.WriteLine(Thread.CurrentThread.Name + " trying to get RECIEVER ID");
                rwlock.AcquireReaderLock(500);
                //Console.WriteLine(Thread.CurrentThread.Name + " successfully got RECIEVER ID");
                return this.receiverId;
            }
            finally
            {
                rwlock.ReleaseReaderLock();
            }
        }
        public Int32 getCardNo()
        {
            try
            {
                //Console.WriteLine(Thread.CurrentThread.Name + " trying to get CARD NUM");
                rwlock.AcquireReaderLock(500);
                //Console.WriteLine(Thread.CurrentThread.Name + " successfully got CARD NUM");
                return this.cardNo;
            }
            finally
            {
                rwlock.ReleaseReaderLock();
            }
        }
        public Int32 getNumSeats()
        {
            try
            {
                //Console.WriteLine(Thread.CurrentThread.Name + " trying to get NUM SEATS");
                rwlock.AcquireReaderLock(500);
                //Console.WriteLine(Thread.CurrentThread.Name + " successfully got NUM SEATS");
                return this.numSeats;
            }
            finally
            {
                rwlock.ReleaseReaderLock();
            }
        }
     }
}
