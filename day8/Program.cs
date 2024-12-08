using System.Reflection.Emit;
using System.Runtime.CompilerServices;

class Program
{
    static void Main(string[] args)
    {
        string inputFile = "/Users/kerryfinch/Documents/Projects/Advent_of_code/day8/input.txt";
        string[] input = File.ReadAllLines(inputFile);
        List<int[]> antinodes = [];
        //char, x location, y location
        List<string[]> antennas = [];
        List<char> chars = [];
        List<string[]> currentAntennas = [];
        
        for (int y = 0; y < input.Length; y++)
        {
            for (int x = 0; x < input[y].Length; x++)
            {
                
                if (input[y][x] != '.')
                {
                    antennas.Add([input[y][x].ToString(), x.ToString(), y.ToString()]);
                    if (!chars.Contains(input[y][x]))
                    {
                        chars.Add(input[y][x]);
                    }
                }
            }
        }

        foreach (string[] c in antennas)
        {
            Console.Write("(" + c[0] + "," + c[1]+ "," + c[2] + ") ");
        }
        Console.WriteLine();
        foreach(char c in chars)
        {
            Console.Write(c + ",");
        }

        foreach(char c in chars)
        {
            currentAntennas = [];
            foreach(string[] antenna in antennas)
            {
                if (antenna[0] == c.ToString())
                {
                    currentAntennas.Add(antenna);
                }
            }
            Console.WriteLine(c + ": " + currentAntennas.Count);
            if (currentAntennas.Count > 1)
            {
                antinodes = FindAntinodeLocations(currentAntennas, antinodes, input[0].Length, input.Length);
            }
        }
        Console.WriteLine("number of antinodes: " + antinodes.Count());
        foreach(int[] a in antinodes)
        {
            Console.Write("(" + a[0] + ","+ a[1] + ") ");
        }
    }
    static List<int[]> FindAntinodeLocations(List<string[]> antennas, List<int[]> antinodes, int maxX, int maxY)
    {
        int newX1 = 0;
        int newY1 = 0;
        int newX2 = 0;
        int newY2 = 0;
        int[] antinode = [];
        bool inList = false;
        for(int i = 0; i < antennas.Count; i++)
        {
            for (int j = 0; j < antennas.Count; j++)
            {
                //find orientation of 2 coords 
                //if x1 > x2 and y1 > y2 -> antinode = (x1 + xDiff, y1 + yDiff) and (x2 - xDiff, y2 - yDiff)
                //if x1 > x2 and y1 < y2 -> antinode = (x1 + xDiff, y1 - yDiff) and (x2 - xDiff, y2 + yDiff)
                //if x1 < x2 and y1 > y2 -> antinode = (x1 - xDiff, y1 + yDiff) and (x2 + xDiff, y2 - yDiff)
                //if x1 < x2 and y1 < y2 -> antinode = (x1 - xDiff, y1 - yDiff) and (x2 + xDiff, y2 + yDiff)
                //.1..
                //..2.
                //
                if (i != j)
                {
                    int xDiff = Math.Abs(int.Parse(antennas[i][1])-int.Parse(antennas[j][1]));
                    int yDiff = Math.Abs(int.Parse(antennas[i][2])-int.Parse(antennas[j][2]));
                    int x1 = int.Parse(antennas[i][1]);
                    int x2 = int.Parse(antennas[j][1]);
                    int y1 = int.Parse(antennas[i][2]);
                    int y2 = int.Parse(antennas[j][2]);

                    if (x1 >= x2 && y1 >= y2)
                    {
                        newX1 = x1+xDiff;
                        newY1 = y1+yDiff;
                        newX2 = x2-xDiff;
                        newY2 = y2-yDiff;
                    }
                    else if (x1 > x2 && y1 < y2)
                    {
                        newX1 = x1+xDiff;
                        newY1 = y1-yDiff;
                        newX2 = x2-xDiff;
                        newY2 = y2+yDiff;
                    }
                    else if (x1 < x2 && y1 > y2)
                    {
                        newX1 = x1-xDiff;
                        newY1 = y1+yDiff;
                        newX2 = x2+xDiff;
                        newY2 = y2-yDiff;
                    }
                    else if (x1 < x2 && y1 < y2)
                    {
                        newX1 = x1-xDiff;
                        newY1 = y1-yDiff;
                        newX2 = x2+xDiff;
                        newY2 = y2+yDiff;
                    }
                    if (newX1 >= 0 && newX1 < maxX && newY1 >= 0 && newY1 < maxY)
                    {
                        //Console.Write("(" + newX1 + ","+ newY1 + ") ");
                        antinode = [newX1,newY1];
                        if (!InList(antinodes, antinode))
                        {
                            antinodes.Add([newX1,newY1]);
                        }
                    }
                    if (newX2 >= 0 && newX2 < maxX && newY2 >= 0 && newY2 < maxY)
                    {
                        //Console.Write("(" + newX2 + ","+ newY2 + ") ");
                        antinode = [newX2,newY2];
                        if (!InList(antinodes, antinode))
                        {
                            antinodes.Add([newX2,newY2]);
                        }
                    }
                }
            }
        }
        return antinodes;
    }
    static bool InList(List<int[]> list, int[] toFind)
   {
     foreach(int[] l in list)
     {
        if (l[0] == toFind[0] && l[1] == toFind[1])
        {
            return true;
        }
     }
     return false;
   }
}
