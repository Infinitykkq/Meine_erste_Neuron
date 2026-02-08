using System;  
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Meine_erste_Neuron
{
    internal class Program
    {

        public class Neuron
        {
            private decimal weight = 0.5m; //Gewicht wird automatisch ausgewählt

            public decimal lastError {  get; private set; }

            public decimal smoothing { get; set; } = 0.000001m; //Wie genau ist die Ausbildung?
            public decimal ProcessInputData (decimal input)  //Die richtige Antwort wird erwartet.
            {
               return input * weight;
            }

            public decimal RestoreInputData(decimal output) //konvertiert umgekehrt
            {
                return output / weight; 
            }


            public void Train(decimal input, decimal expectedResult)  //Hier findet das Training des Neurons selbst statt.
            {
                if (input == 0) return;

                var actualResult = input * weight;

                lastError = expectedResult - actualResult;
                var correction = lastError * smoothing;
                weight += correction;
            }

        }
        static void Main(string[] args)
        {
            decimal km = 100;
            decimal miles = 62.1371m;

            Neuron neuron = new Neuron();

            int i = 0;
            do
            {
                i++;
                neuron.Train(km, miles);
                Console.WriteLine($"Iteration: {i}\tError:\t{neuron.lastError}");

            }
            while (neuron.lastError > neuron.smoothing || neuron.lastError < -neuron.smoothing);

            Console.WriteLine("Ausbildung abgeschlossen");

            Console.WriteLine($"{neuron.ProcessInputData(100)}: Meilen in {100} km");
            Console.WriteLine($"{neuron.ProcessInputData(86)}: Meilen in {86} km");
            Console.WriteLine($"{neuron.RestoreInputData(23)}: km in {23} Meilen");

        }
    }
}
