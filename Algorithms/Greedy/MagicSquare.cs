using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
public static class MagicSquare {

    public static int formingMagicSquare(int[,] s) {
        var sums = GetSumsOfSquare(s);
        // Are all Items the same 
        // While they aren't
            //Change one element from 0-9
        return 1;
    }

    private static SumsOfSquare GetSumsOfSquare(int[,] s)
    {
        var length = Math.Sqrt(s.Length);
        var horizontalSums = new List<int>();
        var verticalSums = new List<int>();    
        var diagonalSums = new List<int>();

        var LeftDiagonalValues = new List<int>();
        var RightDiagonalValues = new List<int>();
        // Get horizontal Sums
        for(var i = 0; i < length; i++)
        {
            LeftDiagonalValues.Add(s[i,i]);
            RightDiagonalValues.Add(s[(int)length-1-i,(int)length-1-i]);
            var Horizontalvalues = new List<int>();
            var VerticalValues = new List<int>();          
            for(var j = 0; j < length; j++)
            {
                Horizontalvalues.Add(s[i,j]);
                VerticalValues.Add(s[j,i]);
                
            }
            horizontalSums.Add(Horizontalvalues.Sum());
            verticalSums.Add(VerticalValues.Sum());
        }
        // Get Diagon Sums
        diagonalSums.AddRange(new List<int>{LeftDiagonalValues.Sum(), RightDiagonalValues.Sum()});
        return new SumsOfSquare(horizontalSums, verticalSums, diagonalSums);
    }

    
}

public class SumsOfSquare 
{
    public SumsOfSquare(List<int> horizontalSums, List<int> verticalSums, List<int> diagonalSums)
    {
        HorizontalSums = horizontalSums;
        VerticalSums = verticalSums;
        DiagonalSums = diagonalSums;
    }
    public List<int> HorizontalSums { get; set; }
    public List<int> VerticalSums { get; set; }
    public List<int> DiagonalSums { get; set; }
}
