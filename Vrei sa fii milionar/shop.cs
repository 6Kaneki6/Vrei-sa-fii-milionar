using System;
using System.Collections.Generic;

public class Shop
{
    public Dictionary<string, int> Prices { get; private set; }

    public Shop()
    {
        Prices = new Dictionary<string, int>
        {
            { "50-50", 100 },
            { "PhoneAFriend", 200 },
            { "AskTheAudience", 300 }
        };
    }

    public void DisplayItems()
    {
        Console.WriteLine("Magazin Lifeline-uri:");
        foreach (var item in Prices)
        {
            Console.WriteLine($"{item.Key}: {item.Value} RON");
        }
    }

    public bool PurchaseItem(string itemName, ref int balance, Lifelines lifelines)
    {
        if (Prices.ContainsKey(itemName) && balance >= Prices[itemName])
        {
            balance -= Prices[itemName];
            lifelines.AddLifeline(itemName);
            Console.WriteLine($"Ai achizitionat {itemName} pentru {Prices[itemName]} RON.");
            return true;
        }
        else
        {
            Console.WriteLine("Fonduri insuficiente sau lifeline invalid.");
            return false;
        }
    }
}