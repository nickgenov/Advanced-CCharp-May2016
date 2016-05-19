using System;
using System.Collections;
using System.Linq;

namespace TruckTour
{
    public class Pump
    {
        public Pump(double petrol, double distance) 
            : this()
        {
            this.Petrol = petrol;
            this.Distance = distance;
        }

        public double Petrol { get; set; }

        public double Distance { get; set; }
    }

    public class TruckTourMain
    {
        public static void Main()
        {
            var pumps = new Queue();

            string input = Console.ReadLine();
            int numberOfPetrolPumps = int.Parse(input);

            for (int i = 0; i < numberOfPetrolPumps; i++)
            {
                input = Console.ReadLine();
                double[] pumpInfo = input.Split(' ').Select(double.Parse).ToArray();

                double petrol = pumpInfo[0];
                double distance = pumpInfo[1];

                var pump = new Pump(petrol, distance);
                pumps.Enqueue(pump);
            }

            double tankPetrol = 0;

            while (true)
            {
                Pump pump = pumps.Dequeue() as Pump;
                if (pump == null)
                {
                    break;
                }

                tankPetrol += pump.Petrol;




                pumps.Enqueue(pump);
            }
        }
    }
}