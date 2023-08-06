﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OOP_carStore
{



    //!TodO {
    //@1- make uml //<done>
    //@2- perfect the prints  
    //@3- make CaronSaleClass methods //<done>
    //@4- add features //<done>
    //@5- add a convenient main with loops and abilty to add new things <done>
    //@6- make clean Code 
    //@7- Exception Handling
    //@8- Ask Gpt for optimization (clean code, exception handling ----> forget (features(0),  usage of concepts(0)))
    //@9- make documentation and comments and screenshots
        
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

           public  double PriceAfterDiscountCalculater(double price)
            {
                if (discountPercentage < 1)
                    return price + price * discountPercentage.GetValueOrDefault();
                else
                    return price + price * (discountPercentage.GetValueOrDefault() / 100);
            }


        }

        //#--------------------------------------------------------------------------------------------------------
        //@Car Class
        class Car : CarOnSale
        {
            private int iD;
            private string name;
            private double price;
            bool isOnSale;
            public Car(int iD=0,string name="car", double price=0, bool isOnSale=false,double?discountPercentage=null,double? priceAfterDiscount=null)
            {
                this.name = name;
                this.price = price;
                this.iD = iD;
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

            public int ID { set { iD=value; } get { return iD; } }

            public override void print() 
            {
                Console.WriteLine($"Car ID : {iD}\nCar name : {name}\nCar price : {price}");
                if (isOnSale)
                {
                    base.print();
                }
                else
                {
                    Console.WriteLine("unfortunately, this car does not have a discount!");
                }
            }

            public void makeDiscount(double discountPercentage)
            {
                isOnSale = true;
                this.discountPercentage=discountPercentage;
                priceAfterDiscount = PriceAfterDiscountCalculater(price);
                Console.WriteLine($"A discount is made for the car {name},\n" +
                    $"Discount percentage = {discountPercentage},\n"+
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
                Console.WriteLine($"The new for the car {name} is as the following: \n" +
                    $"Discount percentage = {discountPercentage},\n" +
                    $"Price after discount = {priceAfterDiscount}");

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
                    Console.WriteLine("\n==========================================\n");
                }
                Console.WriteLine("\n---------------------------------------------------------------------\n");

            }
            
            protected bool idDuplicatesFinder(int id)
            {
                foreach (Car car in cars)
                {
                    if (car.ID==id)
                        return true;
                }

                return false;
            }
            public void AddCar()
            {   
                int id; string name; double price; bool isOnSale;
                double? discountPercentage, priceAfterDiscount;

                Console.WriteLine("Please enter the ID, the name and the price of the car you want to add: ");
                Console.Write("ID: ");
               
                while (true) {
                    id = Convert.ToInt32(Console.ReadLine());
                    if (idDuplicatesFinder(id) == true)
                        Console.WriteLine("A car with the same ID is in the store, please enter different ID :");
                    else
                        break;
                }

                
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
                cars.Add(new Car(id ,name, price, isOnSale,discountPercentage,priceAfterDiscount));
            }

            public void RemoveCar(int id)
            {
                foreach(Car car in cars)
                {
                    if (car.ID == id)
                    {
                        cars.Remove(car); 
                    }
                }
            }

            public Car getCarByID(int id)
            {
                foreach(Car car in cars)
                {
                    if (car.ID == id)
                        return car;
                }
                
                 return null; 
                
                
            }

            public void discountOperations( string op)
            {

                int id;
                double? discountPercentage;
                if (op.ToLower() == "add")
                {
                    Console.Write("enter id :");
                    id = Convert.ToInt32(Console.ReadLine());
                    Console.Write("enter Discount percentage :");
                    discountPercentage= Convert.ToDouble(Console.ReadLine());
                    getCarByID(id).makeDiscount(discountPercentage.GetValueOrDefault());
                    
                }

                else if (op.ToLower() == "modify")
                {
                    Console.Write("enter id :");
                    id = Convert.ToInt32(Console.ReadLine());
                    Console.Write("enter Discount percentage :");
                    discountPercentage = Convert.ToDouble(Console.ReadLine());
                    getCarByID(id).changeDiscount(discountPercentage.GetValueOrDefault());
                }

                else
                {
                    Console.Write("enter id :");
                    id = Convert.ToInt32(Console.ReadLine());
                    getCarByID(id).removeDiscount();

                }
            }

        }

        //#--------------------------------------------------------------------------------------------------------
        static void Main()
        {
           
            List<CarStore> carStores = new List<CarStore>();

            // adding 2 car stores
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
               

                //adding cars to the stores
                for (int carIndex = 1; flag.ToUpper()=="Y"; carIndex++)
                {
                    Console.WriteLine($"\nEnter details for Car {carIndex} of Store {storeIndex}:");

                    
                    carStore.AddCar();

                    Console.WriteLine("Do you want to add another car? (Y/N)");
                    flag=Console.ReadLine();    


                }

                carStores.Add(carStore);
            }

            // Printing before operations
            Console.WriteLine("\n\n=========================================");
            Console.WriteLine("============== CAR STORES DETAILS ==========");
            Console.WriteLine("=========================================\n");
            foreach (CarStore store in carStores)
            {
                store.print();
            }

            // Using the discountOperations method
            Console.WriteLine("\nPerforming discount operations on the cars...");
            Console.WriteLine("Enter the number of your store :\noptions:");
            int c = 0;
            foreach (CarStore carStore in carStores) { 
                c++;
                Console.Write($"{c} ");
                
            }
            Console.WriteLine();
            int index = Convert.ToInt32(Console.ReadLine());    
            Console.Write("Enter the id of the car you want to perform a discount operation on : ");
            int carIdToOperateOn = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("what operation do you want to make? (add, remove, modify)");
            string Op = Console.ReadLine();
            carStores[index].discountOperations( Op);
            Console.WriteLine("what operation do you want to make? (add, remove, modify)");
            Op = Console.ReadLine();
            carStores[index].discountOperations( Op);
            Console.WriteLine("what operation do you want to make? (add, remove, modify)");
            Op = Console.ReadLine();
            carStores[index].discountOperations( Op);


            //Removing a car : 

            Console.WriteLine("enter id of the car you want to remove ");
            int id = Convert.ToInt32(Console.ReadLine());

            carStores[index].RemoveCar(id);
           
            // Printing after operations
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

            Console.ReadKey();
        }
    }
}
