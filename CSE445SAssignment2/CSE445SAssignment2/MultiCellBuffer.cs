
// CSE 445 Brent Julius 03/04/2022

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSE445SAssignment2
{
    internal class bufferObject
    {
        private string encodedString, airlineId;
        public bufferObject(string encodedString, string airlineId)
        {
            this.encodedString = encodedString;
            this.airlineId = airlineId;
        }

        public string getAirlineId()
        {
            return this.airlineId;
        }
        public string getEncodedString()
        {
            return encodedString;
        }
    }
    internal class MultiCellBuffer
    {
        const int SIZE = 3;
        bufferObject[] orders = new bufferObject[SIZE];

        int head = 0, tail = 0, n = 0;
        private Object BufferLock = new Object();

        public int getNumOrders()
        {
            return n;
        }
        public void setOneCell(bufferObject order)
        {

            lock (BufferLock)
            {

                while (n == SIZE)
                {
                    //Console.WriteLine("Blocking thread " + Thread.CurrentThread.Name + " from writing a cell.");
                    //Console.WriteLine(Thread.CurrentThread.Name + " is waiting to set a cell! =================================!!!");
                    Monitor.Wait(BufferLock);
                }

                orders[tail] = order;
                tail = (tail + 1) % SIZE;
                n++;

                //Console.WriteLine(" " + Thread.CurrentThread.Name + " set a cell! " + order.getEncodedString() + " " + "currentSize: " + n);
                Monitor.PulseAll(BufferLock);

            }
        }

        public string getOneCell(string idOfRetrievingAirline)
        {
            lock (BufferLock)
            {


                while (n == 0)
                {
                    //Console.WriteLine(Thread.CurrentThread.Name + " is waiting to get a cell!");
                    Monitor.Wait(BufferLock);
                }
                bufferObject order = orders[head];

                if (order.getAirlineId().Equals(idOfRetrievingAirline))
                {

                    head = (head + 1) % SIZE;
                    n--;

                    //Console.WriteLine("Reading thread " + Thread.CurrentThread.Name + " " + order.getEncodedString + " " + n);
                    Monitor.PulseAll(BufferLock);

                    return order.getEncodedString();
                } else
                {
                    //Console.WriteLine("BUFFER SIZE: " + n);
                    //Console.WriteLine("Wrong Airline: | " + order.getAirlineId() + " | not | " + idOfRetrievingAirline);
                    return "WRONGID";
                }

            }
        }

    }
}
