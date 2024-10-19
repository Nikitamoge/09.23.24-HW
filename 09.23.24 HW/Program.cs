using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


//Task 1
public static class StringExtensions
{
    public static int SentenceCount(this string text)
    {
        return Regex.Matches(text, @"[.!?]").Count;
    }
}


//Task 2
public static class StringExtensionsTask2
{
    public static int CountWordsSameStartEnd(this string text)
    {
        return text.Split(' ')
                   .Count(word => word.Length > 1 &&
                                  char.ToLower(word[0]) == char.ToLower(word[^1]));
    }
}


//Task 3
public class Backpack
{
    public string Color { get; set; }
    public string Brand { get; set; }
    public double Weight { get; set; }
    public double Volume { get; set; }
    private List<Item> contents = new List<Item>();

    public event Action<Item> ItemAdded;
    public event Action<Item> ItemRemoved;

    public void AddItem(Item item)
    {
        contents.Add(item);
        ItemAdded?.Invoke(item);
    }

    public void RemoveItem(Item item)
    {
        contents.Remove(item);
        ItemRemoved?.Invoke(item);
    }

    public class Item
    {
        public string Name { get; set; }
        public double Volume { get; set; }
    }
}


//Task 4
public record Person(string FirstName, string LastName, int Age);

public static class PersonExtensions
{
    public static Person FindOldest(this Person[] people)
    {
        return people.OrderByDescending(p => p.Age).First();
    }

    public static Person FindYoungest(this Person[] people)
    {
        return people.OrderBy(p => p.Age).First();
    }

    public static double AverageAge(this Person[] people)
    {
        return people.Average(p => p.Age);
    }
}


class Program
{
    static void Main()
    {
        //Task 1
        string text1 = "And forget to close the brackets.";
        Console.WriteLine($"Sentence count: {text1.SentenceCount()}");


        // Task 2 Test
        string text2 = "Use C#, better than Python.";
        Console.WriteLine($"Words starting and ending with the same character: {text2.CountWordsSameStartEnd()}");


        //Task 3
        Backpack myBackpack = new Backpack { Color = "Red", Brand = "Adidas", Weight = 0.3, Volume = 10.0 };
        myBackpack.ItemAdded += item => Console.WriteLine($"Added: {item.Name}, Volume: {item.Volume}");
        myBackpack.ItemRemoved += item => Console.WriteLine($"Removed: {item.Name}, Volume: {item.Volume}");
        var item1 = new Backpack.Item { Name = "Laptop", Volume = 2.0 };
        myBackpack.AddItem(item1);
        myBackpack.RemoveItem(item1);


        //Task 4
        Person[] people =
        {
            new Person("Dima", "Gorohov", 19),
            new Person("Nikita", "Demyanenko", 20),
            new Person("Ryan", "Gosling", 37)
        };
        Console.WriteLine($"Youngest: {people.FindYoungest()}");
        Console.WriteLine($"Oldest: {people.FindOldest()}");
        Console.WriteLine($"Average age: {people.AverageAge()}");
    }
}
