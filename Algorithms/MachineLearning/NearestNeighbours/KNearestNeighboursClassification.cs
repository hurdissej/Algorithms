using System;
using System.Collections.Generic;
using System.Linq;

public static class KNearestNeighbourClassification 
{
    private static bool IsFruitOrange(IEnumerable<Fruit> ClassifiedFruit, int Redness, int Size)
    {
        var distances = new Dictionary<Fruit, double>();
        foreach(var fruit in ClassifiedFruit)
        {
            var distance = Math.Sqrt((Math.Pow(Redness - fruit.Redness, 2) + (Math.Pow(Size - fruit.Size, 2))));
            distances.Add(fruit, distance);
        } 
        // Currently O(2N because of this order by - need to make the insert better)
        var orderedDistances = distances.OrderBy(x => x.Value);
        var nearestNeighbours = orderedDistances.Take(5);
        var oranges = nearestNeighbours.Where(x => x.Key.IsOrange);
        if(oranges.Count() > 2)
        {
            return true;
        } else{
            return false;
        }
    }

    public static void RunAllTests()
    {
        var fruit = new List<Fruit>{new Fruit(1,10,true),new Fruit(2,8,true),new Fruit(2,7,true),new Fruit(2,8,true),new Fruit(1,7,true),new Fruit(10,10,false),new Fruit(10,12,false),new Fruit(11,9,false),new Fruit(15,15,false),new Fruit(9,12,false)};
        var testCase1 = new KNearestNeighbourClassificationTestCase(fruit, 1, 9, true);
        var testCase2 = new KNearestNeighbourClassificationTestCase(fruit, 11, 18, false);
        var testCases = new List<KNearestNeighbourClassificationTestCase>{testCase1, testCase2};
        foreach(var testCase in testCases)
        {
            var result = IsFruitOrange(testCase.ClassifiedFruit, testCase.Redness, testCase.Size);
            if(result != testCase.ExpectedResult)
            {
                System.Console.WriteLine($"Test case failed. Expected {testCase.ExpectedResult}, actual: {result} ");
            } else 
            {
                System.Console.WriteLine($"Test case passed!");
            }
        }
    }
}

public class KNearestNeighbourClassificationTestCase 
{
    public KNearestNeighbourClassificationTestCase(IEnumerable<Fruit> classifiedData, int redness, int size, bool expectedResult)
    {
        ClassifiedFruit = classifiedData;
        Redness = redness;
        Size = size;
        ExpectedResult = expectedResult;
    }
    public IEnumerable<Fruit> ClassifiedFruit { get; set; }
    public int Redness { get; set; }
    public int Size { get; set; }
    public bool ExpectedResult { get; set; }
}

public class Fruit 
{
    public Fruit(int redness, int size, bool isOrange)
    {
        Redness = redness;
        Size = size;
        IsOrange = isOrange;
    }
    public int Redness { get; set; }
    public int Size { get; set; }
    public bool IsOrange { get; set; }
}