
// CSE 445 Brent Julius 03/04/2022

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSE445SAssignment2
{
    internal class TravelAgency
    {
        private int numberOfOrders = 0;
        private int currentPrice, previousPrice;
        private string agencyName = "";
        public TravelAgency()
        {
            
            currentPrice = 0;
            previousPrice = 100000;
        }

        public void agencyStart()
        {
            this.agencyName += Thread.CurrentThread.Name;
            //Console.WriteLine(this.agencyName + " Started.");
            
            while (myApplication.working)
            {

            }
            Console.WriteLine(DateTime.Now.ToString("T") + ": " + "---------------- " +  this.agencyName + " STOPPED -------------------");
            
        }

        public void priceWasCut(string airlineName="")
        {
            //Console.WriteLine(this.agencyName + " got a price cut.");
            if (Thread.CurrentThread.Name == null)
            {
                Console.WriteLine(DateTime.Now.ToString("T") + ": " + "Error in priceWasCut " + agencyName + " call, no airline thread.");
            }
            //Console.WriteLine(Thread.CurrentThread.Name + " ABOUT TO MAKE AN ORDER");
            OrderClass o = new OrderClass(agencyName, airlineName); // Make a new order 
            //Console.WriteLine(Thread.CurrentThread.Name + " JUST MADE AN ORDER");
            EncoderDecoder coder = new EncoderDecoder();

            // Put encrypted string into buffer with airlineId
            bufferObject b = new bufferObject(coder.Encrypt(o.ToString(), "ABCDEFGHIJKLMNOP"), o.getReceiverId());

            Console.WriteLine(DateTime.Now.ToString("T") + ": " + this.agencyName + " just placed an order with card number " + o.getCardNo()+ ".");
            myApplication.buffer.setOneCell(b);
            this.numberOfOrders++;

        }

        public string getName()
        {
            return this.agencyName;
        }
        
        public int getNumberOfOrders()
        {
            return this.numberOfOrders;
        }

    }
}
