using System;
using System.Collections.Generic;
using System.Linq;

public static class ClassroomScheduling 
{
    public static int GetMaxNumberOfClasses(IEnumerable<Class> classes)
    {
        var result = new List<Class>();
        var availableClasses = classes;
        while(availableClasses.Any())
        {
            result.Add(availableClasses.OrderBy(x => x.End).First());
            availableClasses = availableClasses.Where(x => x.Start >= result.Select(y => y.End).Last());
        }
        return result.Count();
    }
    public static void RunAllTests()
    {
        var art = new Class("Art", new DateTime(2018,01,01,09,00,00), new DateTime(2018,01,01,10,00,00));
        var english = new Class("English", new DateTime(2018,01,01,09,30,00), new DateTime(2018,01,01,10,30,00));
        var maths = new Class("Maths", new DateTime(2018,01,01,10,00,00), new DateTime(2018,01,01,11,00,00));
        var cs = new Class("CS", new DateTime(2018,01,01,10,30,00), new DateTime(2018,01,01,11,30,00));
        var music = new Class("Music", new DateTime(2018,01,01,11,00,00), new DateTime(2018,01,01,12,00,00));
        var testcase1 = new ClassroomSchedulingTestCase(new List<Class>{art, english, maths, cs, music}, 3);
        var result = GetMaxNumberOfClasses(testcase1.AllClasses);
        if(result != testcase1.ExpectedResult)
        {
            System.Console.WriteLine($"Test case failed. Result = {result}. Expected result: {testcase1.ExpectedResult}");
        } else 
        {
            System.Console.WriteLine("Test Case Passed!");
        }
    }
}

public class ClassroomSchedulingTestCase 
{
    public ClassroomSchedulingTestCase(List<Class> classes, int expectedResult)
    {
        AllClasses = classes;
        ExpectedResult = expectedResult;
    }

    public List<Class> AllClasses { get; set; }
    public int ExpectedResult { get; set; }
}

public class Class 
{ 
    public Class(string name, DateTime start, DateTime end)
    {
        Name = name;
        Start = start;
        End = end;
    }
    public string Name { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}
