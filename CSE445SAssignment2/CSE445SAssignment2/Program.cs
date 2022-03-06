
// CSE 445 Brent Julius 03/04/2022

using System;
using System.Threading;

public delegate void priceCutEvent(string priceCut); // Define a delegate

namespace CSE445SAssignment2
{
    internal class myApplication
    {
        // Multicell buffer used for all threads
        public static MultiCellBuffer buffer = new MultiCellBuffer();
        public static bool working;
        static void Main(string[] args)
        {
            myApplication.working = true;

            // Airlines
            Airline a1 = new Airline();
            Airline a2 = new Airline();

            // Make into threads and start
            Thread a1Thread = new Thread(new ThreadStart(a1.startPriceChanges));
            a1Thread.Name = "Airline 1";
            a1Thread.Start();
            Thread a2Thread = new Thread(new ThreadStart(a2.startPriceChanges));
            a2Thread.Name = "Airline 2";
            a2Thread.Start();

            // Travel Agencies
            TravelAgency t1 = new TravelAgency();
            TravelAgency t2 = new TravelAgency();
            TravelAgency t3 = new TravelAgency();
            TravelAgency t4 = new TravelAgency();
            TravelAgency t5 = new TravelAgency();

            // Bind delegates --- Switched to using static delegation instead of object level
            Airline.priceCut += new priceCutEvent(t1.priceWasCut);
            //a2.priceCut += new priceCutEvent(t1.priceWasCut);
            Airline.priceCut += new priceCutEvent(t2.priceWasCut);
            //a2.priceCut += new priceCutEvent(t2.priceWasCut);
            Airline.priceCut += new priceCutEvent(t3.priceWasCut);
            //a2.priceCut += new priceCutEvent(t3.priceWasCut);
            Airline.priceCut += new priceCutEvent(t4.priceWasCut);
            //a2.priceCut += new priceCutEvent(t4.priceWasCut);
            Airline.priceCut += new priceCutEvent(t5.priceWasCut);
            //a2.priceCut += new priceCutEvent(t5.priceWasCut);


            // Make into threads and start
            Thread t1Thread = new Thread(new ThreadStart(t1.agencyStart));
            t1Thread.Name = "Travel Agency 1";
            t1Thread.Start();
            Thread t2Thread = new Thread(new ThreadStart(t2.agencyStart));
            t2Thread.Name = "Travel Agency 2";
            t2Thread.Start();
            Thread t3Thread = new Thread(new ThreadStart(t3.agencyStart));
            t3Thread.Name = "Travel Agency 3";
            t3Thread.Start();
            Thread t4Thread = new Thread(new ThreadStart(t4.agencyStart));
            t4Thread.Name = "Travel Agency 4";
            t4Thread.Start();
            Thread t5Thread = new Thread(new ThreadStart(t5.agencyStart));
            t5Thread.Name = "Travel Agency 5";
            t5Thread.Start();

            while (a1.working || a2.working)
            {
                Thread.Sleep(1000);
            }
            myApplication.working = false;
            Thread.Sleep(10);

            // Print Summary
            Console.WriteLine("\n\n\n============== Summary ==============");
            Console.WriteLine(a1.getName() + " had a total of " + a1.getNumCuts() + " price cuts.");
            Console.WriteLine(a2.getName() + " had a total of " + a2.getNumCuts() + " price cuts.");

            Console.WriteLine(t1.getName() + " placed a total of " + t1.getNumberOfOrders() + " orders.");
            Console.WriteLine(t1.getName() + " placed a total of " + t2.getNumberOfOrders() + " orders.");
            Console.WriteLine(t1.getName() + " placed a total of " + t3.getNumberOfOrders() + " orders.");
            Console.WriteLine(t1.getName() + " placed a total of " + t4.getNumberOfOrders() + " orders.");
            Console.WriteLine(t1.getName() + " placed a total of " + t5.getNumberOfOrders() + " orders.");

            Console.WriteLine(a1.getName() + " sucessfully processed " + a1.getNumAccepted() + " orders.");
            Console.WriteLine(a2.getName() + " sucessfully processed " + a2.getNumAccepted() + " orders.");
            Console.WriteLine(a1.getName() + " rejected " + a1.getNumRejected() + " orders.");
            Console.WriteLine(a2.getName() + " rejected " + a2.getNumRejected() + " orders.");


        }

    }
}



