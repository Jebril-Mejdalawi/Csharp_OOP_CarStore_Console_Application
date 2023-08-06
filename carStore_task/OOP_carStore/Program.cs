using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OOP_carStore
{



    //!TodO {
    //@1- make uml
    //@2- perfect the prints 
    //@3- make CaronSaleClass methods
    //@4- add features
    //@5- add a convenient main with loops and abilty to add new things
    //@6- make clean Code
    //@7- Exception Handling
    //@8- Ask Gpt for optimization (clean code, features, usage of concepts)
    //@9- make documentation and comments
        
    //!}
    internal class Program
    {


        //@CarOnSale class

        abstract class CarOnSale
        {
            protected double? discountPercentage;
            protected double? priceAfterDiscount;

            public CarOnSale(double ?discountPercentage=null, double? priceAfterDiscount=null)
            {
                this.discountPercentage = discountPercentage; 
                this.priceAfterDiscount = priceAfterDiscount;
            }

            public double? DiscountPercentage { set { discountPercentage = value; } get { return discountPercentage; } }
            public double? PriceAfterDiscount { set {  priceAfterDiscount = value; } get { return priceAfterDiscount; } }

             public virtual void print() {
                Console.WriteLine($"Discount percentage : {DiscountPercentage}" +
                 $"\nPrice after discount : {priceAfterDiscount}");
            }


        }

        //#--------------------------------------------------------------------------------------------------------
        //@Car Class
        class Car : CarOnSale
        {
            private string name;
            private double price;
            bool isOnSale;
            public Car(string name, double price, bool isOnSale,double?discountPercentage=null,double? priceAfterDiscount=null)
            {
                this.name = name;
                this.price = price;
                this.isOnSale = isOnSale;
                if (this.isOnSale)
                {
                    this.priceAfterDiscount = priceAfterDiscount;
                    this.discountPercentage=discountPercentage;
                }
            }


            public string Name { set { name = value; } get { return name; } }
            public double Price { set { price = value; } get { return price; } }
            public bool IsOnSale { set {  isOnSale = value; } get { return IsOnSale; } }

            public override void print() 
            {
                Console.WriteLine($"Car name : {name}\nCar price : {price}");
                if (isOnSale)
                {
                    base.print();
                }
                else
                {
                    Console.WriteLine("unfortunately, this car does not have a discount!");
                }
            }


        }



        //#--------------------------------------------------------------------------------------------------------
        //@CarStore Class

         class  CarStore
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
                foreach ( Car car in cars )
                {
                    car.print();
                }
                Console.WriteLine("\n---------------------------------------------------------------------\n");

            }
            
            public void AddCar()
            {
                string name; double price; bool isOnSale;
                double? discountPercentage, priceAfterDiscount;

                Console.WriteLine("Please enter the name and the price of the car you want to add: ");
                Console.Write("Name: ");
                name=Console.ReadLine();
                Console.Write("Price: ");
                price= Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("does this car have a discount? (y/n) ");
                char isOnsaleInput=Convert.ToChar((Console.ReadLine()).ToUpper());
                if(isOnsaleInput=='Y')
                {
                    isOnSale = true;
                    Console.WriteLine("Please enter the Discount Percentage : ");
                    discountPercentage= Convert.ToDouble(Console.ReadLine());

                    priceAfterDiscount = price + price * discountPercentage;  //!make a function for this
                }
                else
                {
                    isOnSale=false;
                    priceAfterDiscount = null;
                    discountPercentage = null;
                }
                cars.Add(new Car(name, price, isOnSale,discountPercentage,priceAfterDiscount));
            }

        }

        //#--------------------------------------------------------------------------------------------------------
        static void Main()
        {
            CarStore carST = new CarStore("gpt", "zarqa", "jebril");
            carST.AddCar();

            carST.print();
           







            Console.ReadKey();
        }
    }
}
