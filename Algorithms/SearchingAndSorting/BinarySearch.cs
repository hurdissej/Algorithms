using System;
using System.Collections.Generic;

public static class BinarySearch 
{
    public static void RunTests()
    {
        var testsToRun = new List<BinarySearchTestCase>();
        testsToRun.Add(new BinarySearchTestCase(new List<int>{1,2,3,4,5}, 2, 1));
        testsToRun.Add(new BinarySearchTestCase(new List<int>{1,5,22,81,96}, 96, 4));
        testsToRun.Add(new BinarySearchTestCase(new List<int>{1,2,8,20,50}, 90, -1));
        for(var i = 0; i < testsToRun.Count; i++) { 
            var result = SearchList(testsToRun[i].ListToSearch, testsToRun[i].ValueToFind);
            var expectedResult = testsToRun[i].ExpectedResult;
            if(result != expectedResult) {
                System.Console.WriteLine($"Test case {i+1} failed. actual value: {result}, expected value: {expectedResult}");
                continue;
            }
            System.Console.WriteLine($"Test case {i+1} passed!");
        }
    }

    public static int SearchList(List<int> ListToSearch, int valueToFind)
    {
        var low = 0;
        var high = ListToSearch.Count;
        while(low < high)
        {
            int mid = (int)(low + high)/2;
            var guess = ListToSearch[mid];
            if(guess == valueToFind)
                return mid;
            if(guess > valueToFind)
                high = mid;
            else
                low = mid+1;
        }
        return -1;
    }
}

public class BinarySearchTestCase 
{
    public BinarySearchTestCase(List<int> listToSearch, int valueToFine, int expectedResult)
    {
        ListToSearch = listToSearch;
        ValueToFind = valueToFine;
        ExpectedResult = expectedResult;
    }
    public List<int> ListToSearch { get; set; }
    public int ValueToFind { get; set; }
    public int ExpectedResult { get; set; }
}
