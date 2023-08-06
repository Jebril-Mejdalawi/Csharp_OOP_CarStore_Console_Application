using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_carStore
{
    internal class Program
    {
        
        //@Car Class
        class Car {
            private string name;
            private double price;
            public Car(string name, double price)
            {
                this.name = name;
                this.price = price;
            }

            public string Name { set { name = value; } get { return name; } }
            public double Price { set { price = value; } get { return price; } }
        }

        //@CarStore Class
        class CarStore
        {
            private string storeName, storeLocation, ownerName;

            public CarStore(string storeName = "temp", string storeLocation = "city", string ownerName = "joe")
            {
                this.storeName = storeName;
                this.storeLocation = storeLocation;
                this.ownerName = ownerName;

            }

            public string StoreName { set { storeName = value; } get { return storeName; } }
            public string StoreLocation { set { storeLocation = value; } get { return storeLocation; } }
            public string OwnerName { set { ownerName = value; } get { return ownerName; } }

            public void print()
            {
                Console.WriteLine("\n---------------------------------------------------------------------\n");
                Console.WriteLine($"Store name : {storeName}\nStore location : {storeLocation}\nOwner name : {ownerName}\n");
                Console.WriteLine("\n---------------------------------------------------------------------\n");
            }


        }

        static void Main()
        {
            CarStore car = new CarStore("gpt", "zarqa", "jebril");
            CarStore car2 = new CarStore();
            car.print();
            car2.print();







            Console.ReadKey();
        }
    }
}
