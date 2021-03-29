# Pizza Factory
This repository contains code for a "Pizza Factory" application, as technical demonstration for an interview.

## Brief
Produce a pizza factory application that generates a fixed number of pizzas using a range of 3 different pizza bases with 3 different toppings.  
  
Each pizza base and topping has different cooking times. The pizza must use a base cooking time in milliseconds taken from a config file. Each pizza base will then adjust that cooking time based on its type.

## Usage:
### Setup:
#### Requirements:
* Visual Studio Code (or equivalent C# capable IDE)
* [.NET SDK 5.0](https://dotnet.microsoft.com/download)  
  
See this [guide](https://www.alphr.com/vs-code-how-to-create-a-new-project/) on setting up Visual-Studio and the .NET SDK on Windows

#### Opening this project in VS:
1. Either download this repository as a zip (and unpack it) or clone this repo into a folder
2. Open the folder where you have downloaded the code in Visual-Studio Code

### Application Usage:
#### Configuration
The 'config.txt' file contains all configuration information for the program including:
* `base_time` - this specifies how long a pizza needs to be cooked for (in milliseconds)
* `no_pizzas` - how many pizzas the factory should produce
* `interval` - how much time (in milliseconds) there should be between cooking pizzas
* `naming` - the naming convention used for files containing pizza descriptions  
i.e: the default is `pizza_` so files generated will be named: `pizza_1`, `pizza_2` and so on
  
The 'bases.txt' and 'toppings.txt' files both contain lists of pizza-bases and pizza-toppings that the factory will apply. Each new-line is considered a separate base / topping.

#### Usage
In Visual-Studio Code, open the integrated terminal in the root project directory
1. Use the `dotnet run` command to run the application. This will start the application, where the factory will randomly generat the number of pizzas specified in the config file before shutting down.
2. View the generated Pizzas in the 'Pizzas' folder. (this folder will be automatically generated on running the program if it doesn't already exist)

## Acknowledgements
* **UCAS** - For generating application spec.