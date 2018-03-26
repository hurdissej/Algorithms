using System;
using System.Collections.Generic;
using System.Linq;
using Algorithms.Graphs;

public static class FewestConnectionsBetweenPeople 
{
    private static int FindShortestDistance(List<PersonNode> Graph, PersonNode Start, PersonNode End)
    {
        int connections = 0;
        var q = new Queue<PersonNode>();
        q.Enqueue(Graph.Single(x => x.Name == Start.Name));
        while(q.Count > 0)
        {
            connections ++;
            var person = q.Dequeue();
            var friends = Graph.Where(x => person.Friends.Contains(x.Name) && !x.Visited);
            foreach(var friend in friends)
            {
                if(friend.Name == End.Name)
                {
                    return connections;
                } else {
                    q.Enqueue(friend);
                }
            }
            person.Visited = true;
        }
        return -1;
    }
    public static void RunAllTests()
    {
        var node1 = new PersonNode("Elliot", new List<string>{"Steph"});
        var node2 = new PersonNode("Steph", new List<string>{"Ally", "Kate"});
        var node3 = new PersonNode("Kate", new List<string>{"Steph"});
        var node4 = new PersonNode("Ally", new List<string>{"Pete", "Andy"});
        var node5 = new PersonNode("Jamie", new List<string>{"Richard"});
        var node6 = new PersonNode("Pete", new List<string>{});
        var node7 = new PersonNode("Andy", new List<string>{});
        var testsToRun = new List<PersonSearchTestCase>();
        testsToRun.Add(new PersonSearchTestCase(new List<PersonNode>{node1, node2, node3, node4, node5}, node2, node3, 1));
        testsToRun.Add(new PersonSearchTestCase(new List<PersonNode>{node1, node2, node3, node4, node5}, node1, node3, 2));
        testsToRun.Add(new PersonSearchTestCase(new List<PersonNode>{node1, node2, node3, node4, node5, node6, node7}, node1, node7, 4));
        testsToRun.Add(new PersonSearchTestCase(new List<PersonNode>{node1, node2, node3, node4, node5, node6, node7}, node1, node5, -1));

        for(var i = 0; i < testsToRun.Count; i++)
        {
            var distance = FindShortestDistance(testsToRun[i].Graph, testsToRun[i].StartPerson, testsToRun[i].EndPerson);
            if(distance != testsToRun[i].ExpectedResult)
            {
                System.Console.WriteLine($"Test {i+1} failed, expected distance {testsToRun[i].ExpectedResult} actual distance {distance}");
            } else {
                System.Console.WriteLine($"Test {i+1} passed!");
            }
        }
    }
}

public class PersonSearchTestCase 
{
    public PersonSearchTestCase(List<PersonNode> graph, PersonNode startPer, PersonNode endPer, int expectedResult)
    {
           Graph = graph;
           StartPerson = startPer;
           EndPerson = endPer;
           ExpectedResult = expectedResult;
    }
    public List<PersonNode> Graph { get; set; }
    public PersonNode StartPerson { get; set; }
    public PersonNode EndPerson { get; set; }
    public int ExpectedResult { get; set; }
}