using System;
using System.Linq;
using System.Collections.Generic;

public static class SetCoveringProblem 
{

    private static HashSet<string> GetFullCoverage(HashSet<string> statesRequired, Dictionary<string, HashSet<string>> stations)
    {
        var result = new HashSet<string>();
        var iterations = 0;
        while(statesRequired.Any() && iterations < statesRequired.Count)
        {
            string bestStation = null;
            var statesCovered = new HashSet<string>();
            foreach(var station in stations)
            {
                var covered = statesRequired.Intersect(station.Value).ToHashSet();
                if(covered.Count() > statesCovered.Count())
                {
                    bestStation = station.Key;
                    statesCovered = covered;
                }
            }
            //This is horrid but I can't get linq to work on this plane and have no google >.<  
            foreach(var x in statesCovered)
            {
                statesRequired.Remove(x);
            }
            result.Add(bestStation);
            iterations++;
        }
        return result;
    }


    public static void RunAllTests()
    {
        var statesReq1 = new HashSet<string>{"Northamptonshire", "London"};
        var stations1 = new Dictionary<string, HashSet<string>>();
        stations1.Add("Radio1", new HashSet<string>{"Shropshire"});
        stations1.Add("Radio2", new HashSet<string>{"Northamptonshire"});
        stations1.Add("Radio3", new HashSet<string>{"London"});
        stations1.Add("Radiox", new HashSet<string>{"Northamptonshire", "London"});
        var potential1 = new List<HashSet<string>>();
        potential1.Add(new HashSet<string>{"Radiox"});
        potential1.Add(new HashSet<string>{"Radio3", "Radio2"});
        var testCase1 = new StateCoveringTestCase(statesReq1, stations1, potential1);

        var statesReq2 = new HashSet<string>{"mt", "wa","or","id","nv","ut","ca","az"};
        var stations2 = new Dictionary<string, HashSet<string>>();
        stations2.Add("Kone", new HashSet<string>{"id","nd","ut"});
        stations2.Add("Ktwo", new HashSet<string>{"wa","id","mt"});
        stations2.Add("Kthree", new HashSet<string>{"or","nv","ca"});
        stations2.Add("Kfour", new HashSet<string>{"nv", "ut"});
        stations2.Add("Kfive", new HashSet<string>{"ca","az"});
        var potential2 = new List<HashSet<string>>();
        potential2.Add(new HashSet<string>{"Kone", "Ktwo", "Kthree", "Kfive"});
        potential2.Add(new HashSet<string>{"Ktwo", "Kthree", "Kfour", "Kfive"});
        var testCase2 = new StateCoveringTestCase(statesReq2, stations2, potential2);

        var statesReq3 = new HashSet<string>{"mt", "wa","or","id","nv","ut","ca","az", "ThiswontWorks"};
        var stations3 = new Dictionary<string, HashSet<string>>();
        stations3.Add("Kone", new HashSet<string>{"id","nd","ut"});
        var potential3 = new List<HashSet<string>>();
        potential3.Add(new HashSet<string>{"Kone"});
        var testCase3 = new StateCoveringTestCase(statesReq3, stations3, potential3);

        var testCases = new List<StateCoveringTestCase>(){testCase1, testCase2, testCase3};
        foreach(var testcase in testCases)
        {
            var result = GetFullCoverage(testcase.StatesRequired, testcase.Stations);
            var overlaps = testcase.PossibleResults.Select(x => x.Intersect(result));
            if(overlaps.Any())
            {
                System.Console.WriteLine($"Test cased passed");
            } else 
            {
                System.Console.WriteLine($"Test Case failed");
            }
        }
        
    }

}

public class StateCoveringTestCase 
{
    public StateCoveringTestCase(HashSet<string> statesRequired, Dictionary<string, HashSet<string>> stations, List<HashSet<string>> possibleResults)
    {
        StatesRequired = statesRequired;
        Stations = stations;
        PossibleResults = possibleResults;
    }
    public HashSet<string> StatesRequired { get; set; }
    public Dictionary<string, HashSet<string>> Stations { get; set; }
    public List<HashSet<string>> PossibleResults { get; set; }
}