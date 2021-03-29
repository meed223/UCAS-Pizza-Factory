using System;
using System.IO;
using System.Threading;

namespace UCAS_Pizza_Factory {
    public class Factory {
        //---[ Members ]---
        private int baseCookingTime, interval;
        private string naming;
        private Pizza[] pizzasToCook;


        //---[ Constructors ]---
        public Factory(int baseCookingTime, int noPizzas, int interval, string naming) {
            this.baseCookingTime = baseCookingTime;
            this.interval = interval;
            this.naming = naming;
            this.pizzasToCook = new Pizza[noPizzas];
        }

        // Zero Arg. constructor
        public Factory() {
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
            for (int i=0; i < pizzasToCook.Length; i++) {
                Pizza newPizza = new Pizza();
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
    }
}