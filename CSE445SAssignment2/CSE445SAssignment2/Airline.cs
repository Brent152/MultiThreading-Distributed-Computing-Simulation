
// CSE 445 Brent Julius 03/04/2022

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSE445SAssignment2
{
    internal class Airline
    {
        // Constants
        private const double
            SALES_TAX = .10;
        private const Int32
            MIN_PRICE = 50,
            MAX_PRICE = 300;

        public bool working = true;
        private double currentUnitPrice = 300;
        private int numCuts = 0, numRejected = 0, numAccepted = 0;
        string airlineName;

        private static Random rng = new Random(); // For random number generation

        public static event priceCutEvent priceCut; // Link event to a delegate

        // Checks if credit card is valid and computes total charge
        private double OrderProcessing(OrderClass order)
        {
            Console.WriteLine(DateTime.Now.ToString("T") + ": " + this.airlineName + " is processing an order with card number " + order.getCardNo() + ".");
            // If card number is not valid return -1
            if (!(order.getCardNo() > 5000 && order.getCardNo() < 7000))
            {
                Console.WriteLine(DateTime.Now.ToString("T") + ": " + this.airlineName + " declined an order because of an invalid credit card " + order.getCardNo() + "!");
                this.numRejected++;
                return -1;
            }
            // Calculate charge
            double totalCharge = this.getCurrentUnitPrice() * order.getNumSeats();
            totalCharge += totalCharge * SALES_TAX;

            Console.WriteLine(DateTime.Now.ToString("T") + ": " + this.airlineName + " just accepted order with card number " + order.getCardNo() + ".");
            this.numAccepted++;

            return totalCharge;
        }

        // Changes price once a second for 10 seconds
        public void startPriceChanges()
        {
            this.airlineName = Thread.CurrentThread.Name;
            //Console.WriteLine(this.airlineName + " Started.");

            while (this.numCuts < 10)
            {
                var pollTask = new Task(pollBuffer);
                pollTask.Start();
                //Console.WriteLine("Creating poll task");

                this.changeCurrentUnitPrice(pricingModel());

                Thread.Sleep(1000); // Wait 1 second
                //Console.WriteLine(this.airlineName + " cut: " + this.numCuts);
            }
            Thread.Sleep(3000); // Wait for Travel Agencies to finish
            Console.WriteLine(DateTime.Now.ToString("T") + ": " + "================ " + this.airlineName + " STOPPED =============");
            this.working = false;
        }

        // Picks a new random price and returns it
        private double pricingModel()
        {
            double price = rng.Next(MIN_PRICE, MAX_PRICE); // Pick random new price between 50 and 300
            return price;
        }

        // Called every time price changes, emits priceCut event if new price is lower
        public void changeCurrentUnitPrice(double newPrice)
        {
            //Console.WriteLine(airlineName + " PRICE CHANGED - Num Cuts: " + numCuts);
            if (newPrice < this.getCurrentUnitPrice())
            {
                Console.WriteLine(DateTime.Now.ToString("T") + ": " + this.airlineName + " price cut event! New Price: $" + newPrice + ".");

                // Call priceCut if it has any subscribers
                if (priceCut != null)
                {
                    priceCut(Thread.CurrentThread.Name);
                    this.numCuts++;
                    //Console.WriteLine("CUTS++");
                } else
                {
                    //Console.WriteLine("SUBS EMPTY!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                }
            }
            this.currentUnitPrice = newPrice;
        }

        // Polls the multicell buffer constantly
        private void pollBuffer()
        {
            Thread.Sleep(1000);
            while (true)
            {
                string encryptedOrder = myApplication.buffer.getOneCell(this.airlineName);
                // Check if it actually removed a cell from the buffer
                if (encryptedOrder.Equals("WRONGID"))
                {
                    //Console.WriteLine("Wrong ID found.");
                    Thread.Sleep(500); // Wait a bit so it doesn't mega spam the buffer
                }
                else
                {
                    EncoderDecoder coder = new EncoderDecoder();
                    string decryptedOrder = coder.Decrypt(encryptedOrder, "ABCDEFGHIJKLMNOP");
                    //Console.WriteLine(decryptedOrder);
                    // Parse into order object and pass into orderProcessing here
                    //OrderProcessing()
                    string[] orderArr = decryptedOrder.Split("|");

                    OrderClass order = new OrderClass(orderArr[0], orderArr[1]);
                    order.setCardNo(Int32.Parse(orderArr[2]));
                    order.setNumSeats(Int32.Parse(orderArr[3]));
                    //Console.WriteLine(airlineName + " processed an order");
                    // Process order
                    this.OrderProcessing(order);
                }
            }
        }

        // Getter and Setter methods
        public double getCurrentUnitPrice()
        {
            return this.currentUnitPrice;
        }
        public string getName()
        {
            return this.airlineName;
        }
        
        public int getNumCuts()
        {
            return this.numCuts;
        }

        public int getNumAccepted()
        {
            return this.numAccepted;
        }

        public int getNumRejected()
        {
            return this.numRejected;
        }
    }
}
