using System;
using System.Collections.Generic;
using System.Linq;

namespace OOP_carStore
{
    internal class Program
    {
        abstract class CarOnSale
        {
            protected double? discountPercentage;
            protected double? priceAfterDiscount;

            public CarOnSale(double? discountPercentage = null, double? priceAfterDiscount = null)
            {
                this.discountPercentage = discountPercentage;
                this.priceAfterDiscount = priceAfterDiscount;
            }

            public double? DiscountPercentage
            {
                set { discountPercentage = value; }
                get { return discountPercentage; }
            }

            public double? PriceAfterDiscount
            {
                set { priceAfterDiscount = value; }
                get { return priceAfterDiscount; }
            }

            public virtual void print()
            {
                Console.WriteLine($"Discount percentage : {DiscountPercentage}" +
                 $"\nPrice after discount : {priceAfterDiscount}");
            }

            public double PriceAfterDiscountCalculater(double price)
            {
                if (discountPercentage < 1)
                    return price - price * discountPercentage.GetValueOrDefault();
                else
                    return price - price * (discountPercentage.GetValueOrDefault() / 100);
            }
        }

        class Car : CarOnSale
        {
            private int iD;
            private string name;
            private double price;
            bool isOnSale;

            public Car(int iD = 0, string name = "car", double price = 0, bool isOnSale = false, double? discountPercentage = null, double? priceAfterDiscount = null)
            {
                this.name = name;
                this.price = price;
                this.iD = iD;
                this.isOnSale = isOnSale;
                if (this.isOnSale)
                {
                    this.priceAfterDiscount = priceAfterDiscount;
                    this.discountPercentage = discountPercentage;
                }
            }

            public string Name { set { name = value; } get { return name; } }
            public double Price { set { price = value; } get { return price; } }
            public bool IsOnSale { set { isOnSale = value; } get { return isOnSale; } }
            public int ID { set { iD = value; } get { return iD; } }

            public override void print()
            {
                Console.WriteLine($"Car ID : {iD}\nCar name : {name}\nCar price : {price}");
                if (isOnSale)
                {
                    base.print();
                }
                else
                {
                    Console.WriteLine("Unfortunately, this car does not have a discount!");
                }
            }

            public void makeDiscount(double discountPercentage)
            {
                isOnSale = true;
                this.discountPercentage = discountPercentage;
                priceAfterDiscount = PriceAfterDiscountCalculater(price);
                Console.WriteLine($"A discount is made for the car {name},\n" +
                    $"Discount percentage = {discountPercentage},\n" +
                    $"Price after discount = {priceAfterDiscount}");
            }

            public void removeDiscount()
            {
                isOnSale = false;
                discountPercentage = null;
                priceAfterDiscount = null;
            }

            public void changeDiscount(double discountPercentage)
            {
                this.discountPercentage = discountPercentage;
                priceAfterDiscount = PriceAfterDiscountCalculater(price);
                Console.WriteLine($"The new discount for the car {name} is as follows: \n" +
                    $"Discount percentage = {discountPercentage},\n" +
                    $"Price after discount = {priceAfterDiscount}");
            }
        }

        class CarStore
        {
            private string storeName, storeLocation, ownerName;
            List<Car> cars = new List<Car>();

            public CarStore(string storeName = "temp", string storeLocation = "city", string ownerName = "joe")
            {
                this.storeName = storeName;
                this.storeLocation = storeLocation;
                this.ownerName = ownerName;
                cars.Clear();
            }

            public string StoreName { set { storeName = value; } get { return storeName; } }
            public string StoreLocation { set { storeLocation = value; } get { return storeLocation; } }
            public string OwnerName { set { ownerName = value; } get { return ownerName; } }

            public void print()
            {
                Console.WriteLine("\n---------------------------------------------------------------------\n");
                Console.WriteLine($"Store name : {storeName}\nStore location : {storeLocation}\nOwner name : {ownerName}\n");
                foreach (Car car in cars)
                {
                    car.print();
                    Console.WriteLine("\n==========================================\n");
                }
                Console.WriteLine("\n---------------------------------------------------------------------\n");
            }

            protected bool idDuplicatesFinder(int id)
            {
                return cars.Any(car => car.ID == id);
            }

            public void AddCar()
            {
                int id; string name; double price; bool isOnSale;
                double? discountPercentage, priceAfterDiscount;

                Console.WriteLine("Please enter the ID, name, and price of the car you want to add:");
                id = SafeInputInt("ID: ");
                while (idDuplicatesFinder(id))
                {
                    Console.WriteLine("A car with the same ID is in the store, please enter a different ID:");
                    id = SafeInputInt("ID: ");
                }

                Console.Write("Name: ");
                name = Console.ReadLine();
                price = SafeInputDouble("Price: ");
                Console.WriteLine("Does this car have a discount? (y/n) ");
                char isOnSaleInput = SafeInputChar("Choice (y/n): ");
                if (isOnSaleInput == 'Y')
                {
                    isOnSale = true;
                    discountPercentage = SafeInputDouble("Please enter the Discount Percentage: ");
                    priceAfterDiscount = price + price * discountPercentage;
                }
                else
                {
                    isOnSale = false;
                    priceAfterDiscount = null;
                    discountPercentage = null;
                }
                cars.Add(new Car(id, name, price, isOnSale, discountPercentage, priceAfterDiscount));
            }

            public void RemoveCar(int id)
            {
                Car carToRemove = cars.FirstOrDefault(car => car.ID == id);
                if (carToRemove != null)
                {
                    cars.Remove(carToRemove);
                }
            }

            public Car getCarByID(int id)
            {
                return cars.FirstOrDefault(car => car.ID == id);
            }

            public void discountOperations(string op)
            {
                int id = SafeInputInt("Enter the ID of the car you want to perform a discount operation on: ");
                Car car = getCarByID(id);
                if (car == null)
                {
                    Console.WriteLine("No car found with this ID!");
                    return;
                }

                switch (op.ToLower())
                {
                    case "add":
                        double discountToAdd = SafeInputDouble("Enter Discount percentage: ");
                        car.makeDiscount(discountToAdd);
                        break;
                    case "modify":
                        double discountToModify = SafeInputDouble("Enter new Discount percentage: ");
                        car.changeDiscount(discountToModify);
                        break;
                    case "remove":
                        car.removeDiscount();
                        break;
                    default:
                        Console.WriteLine("Invalid operation!");
                        break;
                }
            }
        }

        static int SafeInputInt(string message)
        {
            int value;
            while (true)
            {
                Console.Write(message);
                if (int.TryParse(Console.ReadLine(), out value))
                {
                    return value;
                }
                Console.WriteLine("Please enter a valid integer.");
            }
        }

        static double SafeInputDouble(string message)
        {
            double value;
            while (true)
            {
                Console.Write(message);
                if (double.TryParse(Console.ReadLine(), out value))
                {
                    return value;
                }
                Console.WriteLine("Please enter a valid number.");
            }
        }

        static char SafeInputChar(string message)
        {
            char value;
            while (true)
            {
                Console.Write(message);
                if (char.TryParse(Console.ReadLine().ToUpper(), out value) && (value == 'Y' || value == 'N'))
                {
                    return value;
                }
                Console.WriteLine("Please enter a valid choice (Y/N).");
            }
        }

        static void Main()
        {
            List<CarStore> carStores = new List<CarStore>();
            for (int storeIndex = 1; storeIndex <= 2; storeIndex++)
            {
                Console.WriteLine($"Enter details for Car Store {storeIndex}:");
                Console.Write("Store name: ");
                string storeName = Console.ReadLine();
                Console.Write("Store location: ");
                string storeLocation = Console.ReadLine();
                Console.Write("Owner name: ");
                string ownerName = Console.ReadLine();

                CarStore carStore = new CarStore(storeName, storeLocation, ownerName);
                string flag = "Y";
                while (flag.ToUpper() == "Y")
                {
                    carStore.AddCar();
                    Console.WriteLine("Do you want to add another car? (Y/N)");
                    flag = Console.ReadLine();
                }
                carStores.Add(carStore);
            }

            PrintCarStoresDetails(carStores);

            int index = SafeInputInt("Enter the number of your store: ");
            if (index <= 0 || index > carStores.Count)
            {
                Console.WriteLine("Invalid store number!");
                return;
            }
            CarStore selectedStore = carStores[index - 1];
            for (int i = 0; i < 3; i++)
            {
                string Op = SafeInputOperation("What operation do you want to make? (add, remove, modify): ");
                selectedStore.discountOperations(Op);
            }

            int carIdToRemove = SafeInputInt("Enter the ID of the car you want to remove: ");
            selectedStore.RemoveCar(carIdToRemove);

            PrintCarStoresDetails(carStores);
            Console.ReadKey();
        }

        static string SafeInputOperation(string message)
        {
            while (true)
            {
                Console.Write(message);
                string op = Console.ReadLine().ToLower();
                if (op == "add" || op == "remove" || op == "modify")
                {
                    return op;
                }
                Console.WriteLine("Please enter a valid operation (add, remove, modify).");
            }
        }

        static void PrintCarStoresDetails(List<CarStore> carStores)
        {
            Console.WriteLine("\n\n=========================================");
            Console.WriteLine("============== CAR STORES DETAILS ==========");
            Console.WriteLine("=========================================\n");
            foreach (CarStore store in carStores)
            {
                store.print();
            }
            Console.WriteLine("\n=========================================");
            Console.WriteLine("======= END OF CAR STORES DETAILS =========");
            Console.WriteLine("=========================================\n");

            
        }
    }
}
