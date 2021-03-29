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


        //---[ Constructors ]---
        public Pizza(string pizzaBase, string topping) {
            this.pizzaBase = pizzaBase;
            this.topping = topping;
            this.isCooked = false;
        }

        //---[ Methods ]---
        public string getDescription() {
            if (this.isCooked) {
                string toReturn = "A " + this.pizzaBase + " pizza with " + this.topping + " topping.";
                return toReturn;
            } else {
                return "An uncooked pizza.";
            }
        }

        public void cook(int initialTime) {
            int cookTime = Convert.ToInt32(initialTime * Factory.getBases()[this.pizzaBase]);
            cookTime += Factory.getToppings()[this.topping];
            Thread.Sleep(cookTime);
            isCooked = true;
            return;
        }
    }
}