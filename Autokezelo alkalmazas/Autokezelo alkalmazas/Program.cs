// See https://aka.ms/new-console-template for more information

using static System.Formats.Asn1.AsnWriter;
using System.Security;
using System;
using System.Runtime.ConstrainedExecution;
using System.Collections.Generic;

while (true)
{
    Console.Clear();
    Console.WriteLine("\n\tAutókezelő alkalmazás\n\n Válasszon:\n----------------------------------------- \n 1. Új autó hozzáadása \n 2.Autók listázása \n 3.Autók módosítása az adatbázisban \n 4.Autók törlése az adatbázisból \n 5.Kilépés\n----------------------------------------- \n\n Választott menüpont : ");
    string valasztas = Console.ReadLine();
    switch(valasztas)
    {
        case "1":
            string newbrand;
            string newtype;
            string newcolor;
            int newyear;
            Console.Clear();
            Console.WriteLine("Új autó hozzáadása:\n-----------------------------------------\n");
            Console.WriteLine("Kérem a márkát: ");
            newbrand = Console.ReadLine();
            Console.WriteLine("Kérem a tipust: ");
            newtype = Console.ReadLine();
            Console.WriteLine("Kérem a szint: ");
            newcolor = Console.ReadLine();
            Console.WriteLine("Kérem az évjáratot: ");
            try
            {
                newyear = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Az új autó hozzáadása megtörtént.");
                List<car> cars = new List<car>();
                cars.Add(new car(newbrand, newtype, newcolor, newyear));
                SaveCar(cars);
                Thread.Sleep(2000);
            }
            catch
            {
                Console.WriteLine("Hibás bemenet");
                Thread.Sleep(2000);
            }         
            break;
      
        case "2": 
                Console.Clear();
                List<car> carList = LoadCars();

                int szamlalo = 1;
                foreach (car car in carList)
                {
                    Console.Write("\n-----------------------------------------\n" + szamlalo + ". autó adatai: \n");
                    Console.WriteLine($"Márka: {car.Brand}, Tipus: {car.Type}, Szin: {car.Color}, Évszám: {car.Year}");
                    szamlalo++;
                }
                Console.WriteLine("\n-----------------------------------------\n0 - Vissza");
                string menu2 = Console.ReadLine();
                if (menu2 == "0")
                {
                    break;
                }
            break;
        
        case "3":
            string search;
            string mod;
            Console.Clear();
            Console.WriteLine("Melyik autót módositanád?:\n-----------------------------------------\n");
            Console.WriteLine("Formátum: Márka,tipus,szin,évjárat:");
            search = Console.ReadLine();
            Console.WriteLine("Mire módositanád?");
            mod = Console.ReadLine();
            ModCar(search,mod);
            break;
        
        case "4":
            string delete;
            Console.Clear();
            Console.WriteLine("Melyik autót törölnéd?:\n-----------------------------------------\n");
            Console.WriteLine("Formátum: Márka,tipus,szin,évjárat:");
            delete = Console.ReadLine();
            DeleteCar(delete);
            break;
        
        case "5":
          return;
    }    
}

/// FÜGGVÉNYEK/// 

List<car> LoadCars()
{
    List<car> cars = new List<car>();

    try
    {
        string[] lines = File.ReadAllLines("cars.csv");
        if (lines.Length > 0)
        {
            for (int i = 1; i < lines.Length; i++)
            {
                string[] data = lines[i].Split(",");

                if (data.Length == 4)
                {
                    if (int.TryParse(data[3], out int year))
                    {
                        car newCar = new car(data[0], data[1], data[2], year);
                        cars.Add(newCar);
                    }
                    else
                    {
                        Console.WriteLine($"Hiba: Nem sikerült az évjárat konvertálása: {lines[i]}");
                    }
                }
            }
        }
        else
        {
            Console.WriteLine("A CSV fájl üres.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Hiba történt a fájl olvasása során");
    }

    return cars;
}

void SaveCar (List<car> cars)
{
    List<string> carstring = new List<string>();

    foreach (car newcar in cars)
    {
        carstring.Add($"{newcar.Brand},{newcar.Type},{newcar.Color},{newcar.Year}");
    }
    File.AppendAllLines("cars.csv", carstring);
}


void ModCar(string search, string modded)
{
    string[] carsdata = File.ReadAllLines("cars.csv");
    bool found = false;

    for (int i = 0; i < carsdata.Length; i++)
    {
        if (carsdata[i] == search)
        {
            carsdata[i] = modded;
            found = true;
            break;
        }
 
    }
    if (found)
    {
        File.WriteAllLines("cars.csv", carsdata);
        Console.WriteLine("Az autó módosítása sikeres volt.");
        Thread.Sleep(2000);
    }
    else
    {
        Console.WriteLine("Nem található ilyen autó.");
        Thread.Sleep(2000);
    }
}

void DeleteCar(string search)
{
    string[] carsdata = File.ReadAllLines("cars.csv");
    bool found = false;

    for (int i = 0; i < carsdata.Length; i++)
    {
        if (carsdata[i] == search)
        {
            carsdata[i] = null;
            found = true;
            break;
        }

    }
    if (found)
    {
        File.WriteAllLines("cars.csv", carsdata);
        Console.WriteLine("Törölve");
        Thread.Sleep(2000);
    }
    else
    {
        Console.WriteLine("Nem található ilyen autó.");
        Thread.Sleep(2000);
    }
}
