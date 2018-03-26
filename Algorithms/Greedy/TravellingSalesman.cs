//ToDo
using System;
using System.Linq;
using System.Collections.Generic;

public static class TravellingSalesman 
{
    private static int GetMinimumDistance(List<City> graph, string startCity)
    {
        var currentCity = graph.SingleOrDefault(x => x.Name == startCity);
        var milesTravelled = 0;
        while(graph.Where(x => !x.Visited).Any())
        {
            // set current city as vistied 
            currentCity.Visited = true;
            // Get distance to closest city where visited false
            var closestCity =  getClosestCity(currentCity.Connections, graph);
            if(closestCity.Key == null)
                break;
            // increment miles travelled 
            milesTravelled+= closestCity.Value;
            // change current city
            currentCity =  graph.SingleOrDefault(x => x.Name == closestCity.Key);
        }
        return milesTravelled;
    }

    private static KeyValuePair<string, int> getClosestCity(Dictionary<string, int> connections, List<City> cities)
    {

        var availableCities = cities.Where(x => !x.Visited).Select(x => x.Name);
        var unProcessedNodes = connections.Where(x => availableCities.Contains(x.Key));
        int distance = int.MaxValue;
        string closesCity = null;
        foreach(var city in unProcessedNodes)
        {
            if(city.Value < distance)
            {
                distance = city.Value;
                closesCity = city.Key;
            }
        }
        return new KeyValuePair<string, int>(closesCity, distance);
    }
    public static void RunAllTests()
    {
        var london = new City("London", new Dictionary<string, int>(){{"Northampton", 50}, {"Cardiff", 60}, {"Devon", 6000}});
        var devon = new City("Devon", new Dictionary<string, int>(){{"Cardiff", 600}});
        var cardiff = new City("Cardiff", new Dictionary<string, int>(){{"London", 10}, {"Devon", 500}});
        var northampton = new City("Northampton", new Dictionary<string, int>(){{"Cardiff", 100}, {"London", 50}, {"Devon", 500}});
        var testCase1 = new TravellingSalesmanTestCase(new List<City>{london, devon, cardiff, northampton}, "Cardiff", 560);
        var testCase2 = new TravellingSalesmanTestCase(new List<City>{london, devon, cardiff, northampton}, "London", 650);
        var result = GetMinimumDistance(testCase1.Graph, testCase1.StartCity);
        if(result != testCase1.MaximumResult)
        {
            System.Console.WriteLine($"Test case failed result: {result}. Expected Result {testCase1.MaximumResult}");
        } else 
        {
            System.Console.WriteLine("Test case passed");
        }        
    }
}

public class TravellingSalesmanTestCase 
{
    public TravellingSalesmanTestCase(List<City> graph, string start, int max)
    {
        Graph = graph;
        StartCity = start;
        MaximumResult = max;
    }
    public List<City> Graph { get; set; }    
    public string StartCity { get; set; }
    public int MaximumResult { get; set; }
}

public class City 
{
    public City(string name, Dictionary<string, int> connections)
    {
        Name = name;
        Connections = connections;
        Visited = false;
    }
    public string Name { get; set; }
    public Dictionary<string, int> Connections { get; set; }
    public bool Visited { get; set; }
}