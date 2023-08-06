﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_carStore
{

    internal class Program
    {


        //@CarOnSale class

        class CarOnSale
        {
            double? discountPercentage;
            double? priceAfterDiscount;

            public CarOnSale(double ?discountPercentage=null, double? priceAfterDiscount=null)
            {
                this.discountPercentage = discountPercentage; 
                this.priceAfterDiscount = priceAfterDiscount;
            }

            public double? DiscountPercentage { set { discountPercentage = value; } get { return discountPercentage; } }
            public double? PriceAfterDiscount { set {  priceAfterDiscount = value; } get { return priceAfterDiscount; } }


        }

        //#--------------------------------------------------------------------------------------------------------
        //@Car Class
        class Car : CarOnSale
        {
            private string name;
            private double price;
            bool isOnSale;
            public Car(string name, double price, bool isOnSale,)
            {
                this.name = name;
                this.price = price;
                this.isOnSale = isOnSale;
            }

            public string Name { set { name = value; } get { return name; } }
            public double Price { set { price = value; } get { return price; } }
            public bool IsOnSale { set {  isOnSale = value; } get { return IsOnSale; } }
        }



        //#--------------------------------------------------------------------------------------------------------
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

        //#--------------------------------------------------------------------------------------------------------
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
