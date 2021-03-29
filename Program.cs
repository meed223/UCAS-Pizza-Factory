using System;
using System.IO;
using System.Collections.Generic;

namespace UCAS_Pizza_Factory {
    class Program
    {
        public static void Main(string[] args) {
            // TODO read config file, initialize a factory instance, cook pizzaz
            IDictionary<string, string> configOptions = new Dictionary<string, string>();
            try {
                foreach(string line in File.ReadAllLines("config.txt")) {
                    // Split each line by the '=' to get a key-value pair                  
                    string[] kvp = line.Split('=');
                    configOptions.Add(kvp[0], kvp[1]);
                }
                // Create 'Pizzas' folder (if doesn't exist)
                if (!Directory.Exists("Pizzas")) {
                    Directory.CreateDirectory("Pizzas");
                }
                Pizza.buildToppings();
                Pizza.buildBases();
                // Initialize and start factory
                Factory pizzaFactory = new Factory(
                    Int32.Parse(configOptions["base_time"]),
                    Int32.Parse(configOptions["no_pizzas"]),
                    Int32.Parse(configOptions["interval"]),
                    configOptions["naming"]
                );

                Console.WriteLine("Factory Initialized with the following parameters:");
                Console.WriteLine("Base cooking time: " + configOptions["base_time"]);
                Console.WriteLine("Number of Pizzas (to cook): " + configOptions["no_pizzas"]);
                Console.WriteLine("Interval: " + configOptions["interval"] + "\n");

                pizzaFactory.Start();

            } catch (IOException e) {
                Console.WriteLine("Error: Unable to read / open the config file!");
                Console.WriteLine("Trace: " + e.StackTrace);
                Console.WriteLine("Terminating application...");
                System.Environment.Exit(0);
            } catch (Exception e) {
                Console.WriteLine("Error: General exception caught!");
                Console.WriteLine("Trace: " + e.StackTrace);
                Console.WriteLine("Message: " + e.Message);
                Console.WriteLine("Terminating application...");
                System.Environment.Exit(0);
            }
        }
    }
}
