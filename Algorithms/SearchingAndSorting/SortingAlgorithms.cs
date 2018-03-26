using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Algorithms
{
    public static class SelectionSort
    {
        private static IEnumerable<int> SortWithQuickSort(IEnumerable<int> list)
        {
            var smaller = list.Where(x => x < list.First());
            var bigger = list.Where(x => x > list.First());
            if(smaller.Count() > 1)
                smaller = SortWithQuickSort(smaller);
            if(bigger.Count() > 1)
                bigger = SortWithQuickSort(bigger);
            var equal = list.Where(x => x == list.First());
            return smaller.Concat(equal).Concat(bigger);
        }

        private static List<int> SortWithSelection(List<int> list)
        {
           var sortedList =  new List<int>();
           while(list.Any()){
              var smallest = FindSmallest(list);
              sortedList.Add(smallest);
              list.Remove(smallest);
           }

           return sortedList;
        }

        private static int FindSmallest(List<int> list)
        {
            var smallestIndex = 0;
            for(var i = 1; i < list.Count; i++)
            {
                if(list[i] < list[smallestIndex])
                    smallestIndex = i;
            }
            return list[smallestIndex];
        }


        public static void RunTests()
        {
            var testsToRun = new List<SelectionSortTestCase>();
            testsToRun.Add(new SelectionSortTestCase(new List<int>{1,2,3,4,5}, new List<int>{1,2,3,4,5}));
            testsToRun.Add(new SelectionSortTestCase(new List<int>{96,22,5,81,1}, new List<int>{1,5,22,81,96}));
            var sw = new Stopwatch();
            for(var i = 0; i < testsToRun.Count; i++) { 
                sw.Start();
                var result = SortWithQuickSort(testsToRun[i].ListToInsert);
                var expectedResult = testsToRun[i].ExpectedResult;
                if(!result.SequenceEqual(expectedResult)) {
                    System.Console.WriteLine($"Test case {i+1} failed in {sw.ElapsedTicks}");
                    continue;
                }
                System.Console.WriteLine($"Test case {i+1} passed in {sw.ElapsedMilliseconds}!");
                sw.Stop();
                sw.Reset();
            }
        }

    }

    public class SelectionSortTestCase 
    {
        public SelectionSortTestCase(List<int> listToSearch, List<int> expectedResult)
        {
            ListToInsert = listToSearch;
            ExpectedResult = expectedResult;
        }
        public List<int> ListToInsert { get; set; }
        public List<int> ExpectedResult { get; set; }
    }
}