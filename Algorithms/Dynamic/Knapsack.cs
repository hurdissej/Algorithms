using System;
using System.Collections.Generic;
using System.Linq;

public static class Knapsack 
{

    //This is filthy and I know it - one day i'll tidy it up... but it does work
    private static int GetMaxValueOfKnapsack(List<Item> items, int totalWeight)
    {
        int[,] grid = new int[items.Count, totalWeight]; // Create grid

        for(var i = 0; i< items.Count; i++) //for each item in the list
        {
            for(var j = 0; j < totalWeight; j++) // for each size knapsack
            {
                var currentItem = items[i];
                if(i == 0) // First row is just the first items value
                {
                    grid[i,j] = currentItem.Weight <= j+1 ? items[i].Value : 0; 
                } else {
                    var previousValue = grid[i-1,j];
                    if(currentItem.Weight <= j+1) // If the item can fit in the bag
                    {
                        if(j - currentItem.Weight > 0) // can two other items fit better?
                        {
                            var potentialValue = currentItem.Value + grid[i-1, j-currentItem.Weight];
                            grid[i,j] = previousValue > potentialValue ? previousValue : potentialValue;
                            continue;
                        } 
                        grid[i,j] = currentItem.Value > previousValue ? currentItem.Value : previousValue;
                    } else 
                    {
                        grid[i,j] = previousValue;
                    }
                    
                }
            }
        }
        return grid[items.Count-1, totalWeight-1];
    }

    public static void RunAllTests()
    {
        var stereo = new Item("Stereo", 4, 3000);
        var laptop = new Item("Laptop", 3, 2000);
        var guitar = new Item("Guitar", 1, 1500);
        var iPhone = new Item("Iphone", 1, 2000);
        var testCase1 = new KnapsackTestCase(new List<Item>{stereo, laptop, guitar, iPhone}, 4, 4000);

        var diamond = new Item("Diamond", 1, 10000);
        var testCase2 = new KnapsackTestCase(new List<Item>{diamond, stereo, laptop, guitar, iPhone}, 5, 10000);

        var testCases = new List<KnapsackTestCase>{testCase1, testCase2};
        foreach(var testCase in testCases)
        {
            var result = GetMaxValueOfKnapsack(testCase.Items, testCase.TotalWeight);
            if(result < testCase.ExpectedResult)
            {
                System.Console.WriteLine($"Test case failed. Result = {result}. Expected result: {testCase.ExpectedResult}");
            } else 
            {
                System.Console.WriteLine("Test Case Passed!");
            }
        }
    }

}

public class KnapsackTestCase 
{
    public KnapsackTestCase(List<Item> items, int totalWeight, int expectedResult)
    {
        Items = items;
        TotalWeight = totalWeight;
        ExpectedResult = expectedResult;
    }
    public List<Item> Items { get; set; }
    public int TotalWeight { get; set; }
    public int ExpectedResult { get; set; }
}

public class Item 
{
    public Item(string name, int weight, int value)
    {
        Name = name;
        Weight = weight;
        Value = value;
    }
    public string Name { get; set; }
    public int Weight { get; set; }
    public int Value { get; set; }
}