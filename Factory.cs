using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Threading;

namespace UCAS_Pizza_Factory {
    public class Factory {
        //---[ Members ]---
        private int baseCookingTime, interval;
        private string naming;
        private Pizza[] pizzasToCook;

        //---[ Static Members ]---
        private static IDictionary<string, int> availableToppings;
        private static IDictionary<string, double> availableBases;


        //---[ Constructors ]---
        public Factory(int baseCookingTime, int noPizzas, int interval, string naming) {
            if (availableToppings == null) {
                Factory.buildToppings();
            }
            if (availableBases == null) {
                Factory.buildBases();
            }
            this.baseCookingTime = baseCookingTime;
            this.interval = interval;
            this.naming = naming;
            this.pizzasToCook = new Pizza[noPizzas];
        }

        // Zero Arg. constructor
        public Factory() {
            if (availableToppings == null) {
                Factory.buildToppings();
            }
            if (availableBases == null) {
                Factory.buildBases();
            }
            // Default options as specified in doc
            this.baseCookingTime = 3000; 
            this.interval = 1000;
            this.naming = "pizza_";
            this.pizzasToCook = new Pizza[50];     
        }

        //---[ Methods ]---
        public void Start() {
            Console.WriteLine("Starting Pizza Factory...");
            CreatePizzas();
            CookPizzas();
            foreach(Pizza pizza in pizzasToCook) {
                Console.WriteLine(pizza.getDescription());
            }
        }
        private void CreatePizzas() {
            Random rand = new Random();
            for (int i=0; i < pizzasToCook.Length; i++) {
                // Generate random Pizza base + toppings
                string pizzaBase = Factory.availableBases.Keys.ElementAt(rand.Next(availableBases.Count));
                string topping = Factory.availableToppings.Keys.ElementAt(rand.Next(availableToppings.Count));
                Pizza newPizza = new Pizza(pizzaBase, topping);
                pizzasToCook[i] = newPizza;
            }
        }

        private void CookPizzas() {
            int counter = 1;
            foreach (Pizza p in pizzasToCook) {
                Console.WriteLine("Cooking next pizza... [" + counter + "/" + pizzasToCook.Length + "]");

                p.cook(this.baseCookingTime);
                Thread.Sleep(interval);
                string location = "Pizzas/" + this.naming + counter.ToString() + ".txt";

                Console.WriteLine("Pizza Done! Description available at: " + location);
                File.WriteAllTextAsync(location, p.getDescription());

                counter++;
            }
        }

        //---[ Static Methods ]---
        public static void buildToppings() {
            // Generate list of usable toppings from file       
            try {
                availableToppings = new Dictionary<string, int>();
                foreach(string line in File.ReadAllLines("toppings.txt")) {
                    int time = 0;
                    foreach (char c in line) {
                        if(!Char.IsWhiteSpace(c)) {
                            time += 100;
                        }
                    }
                    Factory.availableToppings.Add(line, time);
                }
            } catch (IOException e) {
                Console.WriteLine("Error: Unable to read / open the toppings file!");
                Console.WriteLine("Trace: " + e.StackTrace);
                Console.WriteLine("Terminating application...");
                System.Environment.Exit(0);
            }
        }

        public static IDictionary<string, int> getToppings() {
            return Factory.availableToppings;
        }

        public static void buildBases() {
            // Generate list of usable bases from file
            try {
                availableBases = new Dictionary<string, double>();
                foreach(string line in File.ReadAllLines("bases.txt")) {
                    string[] kvp = line.Split('=');
                    Factory.availableBases.Add(kvp[0], Convert.ToDouble(kvp[1]));
                }
            } catch (IOException e) {
                Console.WriteLine("Error: Unable to read / open the bases file!");
                Console.WriteLine("Trace: " + e.StackTrace);
                Console.WriteLine("Terminating application...");
                System.Environment.Exit(0);
            }
        }

        public static IDictionary<string, double> getBases() {
            return Factory.availableBases;            
        }

    }
}