using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Threading;

namespace UCAS_Pizza_Factory {
    public class Pizza {
        //---[ Members ]---
        private bool isCooked;
        private string topping, pizzaBase;
        private static string[] availableToppings;
        private static IDictionary<string, double> availableBases;

        //---[ Constructors ]---
        public Pizza(string pizzaBase, string topping) {
            if (availableToppings == null) {
                Pizza.buildToppings();
            }
            if (availableBases == null) {
                Pizza.buildBases();
            }
            this.pizzaBase = pizzaBase;
            this.topping = topping;
            this.isCooked = false;
        }

        // Zero Arg. constructor
        public Pizza() {
            this.isCooked = false;
            if (availableToppings == null) {
                Pizza.buildToppings();
            }
            if (availableBases == null) {
                Pizza.buildBases();
            }
            // Generate random Pizza base + toppings
            Random rand = new Random();
            this.pizzaBase = availableBases.Keys.ElementAt(rand.Next(availableBases.Count));
            // TODO throw error if base not in list of available
            this.topping = availableToppings[rand.Next(availableToppings.Length)];
        }

        //---[ Methods ]---
        public static void buildToppings() {
            // Generate list of usable toppings from file       
            try {
                Pizza.availableToppings = File.ReadAllLines("toppings.txt");
            } catch (IOException e) {
                Console.WriteLine("Error: Unable to read / open the toppings file!");
                Console.WriteLine("Trace: " + e.StackTrace);
                Console.WriteLine("Terminating application...");
                System.Environment.Exit(0);
            }
        }

        public static void buildBases() {
            // Generate list of usable bases from file
            try {
                availableBases = new Dictionary<string, double>();
                foreach(string line in File.ReadAllLines("bases.txt")) {
                    string[] kvp = line.Split('=');
                    Pizza.availableBases.Add(kvp[0], Convert.ToDouble(kvp[1]));
                }
            } catch (IOException e) {
                Console.WriteLine("Error: Unable to read / open the bases file!");
                Console.WriteLine("Trace: " + e.StackTrace);
                Console.WriteLine("Terminating application...");
                System.Environment.Exit(0);
            }
        }

        public static string[] getAvailableToppings() {
            return availableToppings;
        }

        public static IDictionary<string, double> getAvailableBases() {
            return availableBases;
        }

        private int getToppingTime() {
            // Increase cooking time by 100ms for each letter contained in the name of the topping
            int time = 0;
            foreach (char c in this.topping) {
                if(!Char.IsWhiteSpace(c)) {
                    time += 100;
                }
            }
            return time;
        }

        public string getDescription() {
            if (this.isCooked) {
                string toReturn = "A " + this.pizzaBase + " pizza with " + this.topping + " topping.";
                return toReturn;
            } else {
                return "An uncooked pizza.";
            }
        }

        public void cook(int baseTime) {
            int cookTime = Convert.ToInt32(baseTime * availableBases[this.pizzaBase]);
            cookTime += getToppingTime();
            Thread.Sleep(cookTime);
            isCooked = true;
            return;
        }
    }
}