using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Graphs
{
    public static class DjikstraShortestDistance
    {
        private static int FindShortestDistance(Dictionary<string, Dictionary<string, int>> graph)
        {
            // Set up
            var processed = new List<string>(); //ToDo Make this more object oriented
            var parentTable = new Dictionary<string, string>();
            var costTable = new Dictionary<string, int>();
            foreach(var node in graph)
            {
                if(node.Key == "Start")
                {
                    costTable.Add("Start", 0);
                    continue;
                }
                
                parentTable.Add(node.Key, "None");
                costTable.Add(node.Key, int.MaxValue);
            }
            var toProcess = FindLowestNode(costTable, processed);
            while(toProcess != null)
            {
                // Get its neighbours
                graph.TryGetValue(toProcess, out Dictionary<string, int> neighbours);
                // For each neighbour 
                var costToHere = costTable[toProcess];
                foreach(var neighbour in neighbours)
                {
                    // Add cost to neighbour
                    var potentialCost = costToHere + neighbour.Value;
                    var currentCost = costTable[neighbour.Key];
                    // Check if needs updating
                    if(potentialCost < currentCost)
                    {
                        // If it does, also update parent table
                        costTable[neighbour.Key] = potentialCost;
                        parentTable[neighbour.Key] = toProcess;
                    }
                }
                processed.Add(toProcess);
                //Set to process  
                toProcess = FindLowestNode(costTable, processed);
            }
            return costTable["Finish"];
        }

        private static string FindLowestNode(Dictionary<string, int> costTable, List<string> processed)
        {
            var unProcessedNodes = costTable.Where(x => !processed.Contains(x.Key));
            var cost = int.MaxValue;
            string node = null;
            foreach(var unProcessedNode in unProcessedNodes)
            {
                if(unProcessedNode.Value < cost)
                {
                    cost = unProcessedNode.Value;    
                    node = unProcessedNode.Key;
                }
            }
            return node;
        }
        public static void RunAllTests()
        {
            var graph1 = new Dictionary<string, Dictionary<string, int>>();
            graph1.Add("Start", new Dictionary<string, int>{{"A",6}, {"B",2}});
            graph1.Add("A", new Dictionary<string, int>{ {"Finish",1}});
            graph1.Add("B", new Dictionary<string, int>{{"A",3}, {"Finish",5}});
            graph1.Add("Finish", new Dictionary<string, int>());
            var testCase1 = new DjikstraBasedSearchTestCase(graph1, 6);
            
            var graph2 = new Dictionary<string, Dictionary<string, int>>();
            graph2.Add("Start", new Dictionary<string, int>{{"A",5}, {"B",2}});
            graph2.Add("A", new Dictionary<string, int>{ {"C",4}, {"D",2}});
            graph2.Add("B", new Dictionary<string, int>{{"A",8}, {"D",7}});
            graph2.Add("C", new Dictionary<string, int>{{"D",6}, {"Finish",3}});
            graph2.Add("D", new Dictionary<string, int>{{"Finish",1}});
            graph2.Add("Finish", new Dictionary<string, int>());
            var testCase2 = new DjikstraBasedSearchTestCase(graph2, 8);


            var graph3 = new Dictionary<string, Dictionary<string, int>>();
            graph3.Add("Start", new Dictionary<string, int>{{"A",10}});
            graph3.Add("A", new Dictionary<string, int>{ {"B",20}});
            graph3.Add("B", new Dictionary<string, int>{{"Finish",30}, {"C",1}});
            graph3.Add("C", new Dictionary<string, int>{{"B",1}});
            graph3.Add("Finish", new Dictionary<string, int>());
            var testCase3 = new DjikstraBasedSearchTestCase(graph3, 60);

            // add a few negatives and deal with that

            // add some where start isn't linked to finish - expected 

            //Print path out?

            var testCases = new List<DjikstraBasedSearchTestCase>{testCase1, testCase2, testCase3};
            for(var i = 0; i < testCases.Count; i++)
            {
                var result = FindShortestDistance(testCases[i].Graph);
                if(result != testCases[i].ExpectedResult)
                {
                    System.Console.WriteLine($"Test case {i+1} not passed expected result {testCases[i].ExpectedResult} actual result {result}");
                } else 
                {
                    System.Console.WriteLine($"Test case {i+1} passed");
                }
            }
        }
    }

    public class DjikstraBasedSearchTestCase 
    {
        public DjikstraBasedSearchTestCase(Dictionary<string, Dictionary<string, int>> graph, int expectedResult)
        {
            Graph = graph;
            ExpectedResult = expectedResult;
        }
        public Dictionary<string, Dictionary<string, int>> Graph { get; set; }
        public int ExpectedResult { get; set; }
    }
}