using System;
using System.Collections.Generic;
using System.Linq;

public static class LongestCommonSubstring
{
    private static string GetClosestWord(List<string> dictionary, string enteredWord)
    {
        var closest = new KeyValuePair<string, int>(null, 0);
        foreach(var word in dictionary)
        {
            var grid = new int[word.Length , enteredWord.Length];
            var longestSubstring = 0;
            for(var i = 0; i < word.Length; i++)
            {
                for(var j = 0; j < enteredWord.Length; j++)
                {
                    if(word[i] == enteredWord[j])
                    {
                        if(i ==0 || j==0)
                        {
                            grid[i,j] = 1;
                        } 
                        grid[i,j] = grid[i-1, j-1] + 1;
                    } else {
                        grid[i,j] = 0;
                    }
                    longestSubstring = grid[i,j] > longestSubstring ? grid[i,j] : longestSubstring;
                }
            }
            if(longestSubstring > closest.Value)
                closest = new KeyValuePair<string, int>(word, longestSubstring);
        }

        return closest.Key;
    }
    public static void RunAllTests()
    {
        var mockDictionary = new List<string>{"Dog", "Cat", "Fish", "Unicorn"};
        var testCase1 = new LongestCommonSubstringTestCase("Hish", mockDictionary, "Fish");
        var testCase2 = new LongestCommonSubstringTestCase("born", mockDictionary, "Unicorn");
        var testCase3 = new LongestCommonSubstringTestCase("x", mockDictionary, null);
        var testCases = new List<LongestCommonSubstringTestCase>{testCase1, testCase2, testCase3};
        foreach(var testcase in testCases)
        {
            var result = GetClosestWord(testcase.Dictionary, testcase.WordEntered);
            if(result != testcase.ExpectedClosestWord)
            {
                System.Console.WriteLine($"Test case failed. Result = {result}. Expected result: {testcase.ExpectedClosestWord}");
            } else 
            {
                System.Console.WriteLine("Test Case Passed!");
            }
        }

    }
}

public class LongestCommonSubstringTestCase 
{
    public LongestCommonSubstringTestCase(string word, List<string> mockDictionary, string expected)
    {
        WordEntered = word;
        Dictionary = mockDictionary;
        ExpectedClosestWord = expected;
    }
    public string WordEntered { get; set; }
    public List<string> Dictionary { get; set; }
    public string ExpectedClosestWord { get; set; }
}