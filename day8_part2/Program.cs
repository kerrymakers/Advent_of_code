using System.Reflection.Emit;
using System.Runtime.CompilerServices;

class Program
{
    static void Main(string[] args)
    {
        string inputFile = "/Users/kerryfinch/Documents/Projects/Advent_of_code/day8_part2/input.txt";
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
                    antinodes.Add([x,y]);
                    if (!chars.Contains(input[y][x]))
                    {
                        chars.Add(input[y][x]);
                    }
                }
            }
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
            if (currentAntennas.Count > 1)
            {
                antinodes = FindAntinodeLocations(currentAntennas, antinodes, input[0].Length, input.Length);
            }
        }
        Console.WriteLine("number of antinodes: " + antinodes.Count());
    }
    static List<int[]> FindAntinodeLocations(List<string[]> antennas, List<int[]> antinodes, int maxX, int maxY)
    {
        for(int i = 0; i < antennas.Count; i++)
        {
            for (int j = 0; j < antennas.Count; j++)
            {
                //find orientation of 2 coords 
                //if x1 > x2 and y1 > y2 -> antinode = (x1 + xDiff, y1 + yDiff) and (x2 - xDiff, y2 - yDiff)
                //if x1 > x2 and y1 < y2 -> antinode = (x1 + xDiff, y1 - yDiff) and (x2 - xDiff, y2 + yDiff)
                //if x1 < x2 and y1 > y2 -> antinode = (x1 - xDiff, y1 + yDiff) and (x2 + xDiff, y2 - yDiff)
                //if x1 < x2 and y1 < y2 -> antinode = (x1 - xDiff, y1 - yDiff) and (x2 + xDiff, y2 + yDiff)
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
                        x1 = x1+xDiff;
                        y1 = y1+yDiff;
                        while(x1 < maxX && y1 < maxY)
                        {
                            antinodes = AddAntinode(x1, y1, antinodes);
                            x1 = x1+xDiff;
                            y1 = y1+yDiff;
                        }
                        x2 = x2-xDiff;
                        y2 = y2-yDiff;
                        while(x2 >= 0 && y2 >= 0)
                        {
                            antinodes = AddAntinode(x2, y2, antinodes);
                            x2 = x2-xDiff;
                            y2 = y2-yDiff;
                        }
                    }
                    else if (x1 > x2 && y1 < y2)
                    {
                        x1 = x1+xDiff;
                        y1 = y1-yDiff;
                        while(x1 < maxX && y1 >= 0)
                        {
                            antinodes = AddAntinode(x1, y1, antinodes);
                            x1 = x1+xDiff;
                            y1 = y1-yDiff;
                        }
                        x2 = x2-xDiff;
                        y2 = y2+yDiff;
                        while(x2 >= 0 && y2 < maxY)
                        {
                            antinodes = AddAntinode(x2, y2, antinodes);
                            x2 = x2-xDiff;
                            y2 = y2+yDiff;
                        }
                    }
                    else if (x1 < x2 && y1 > y2)
                    {
                        x1 = x1-xDiff;
                        y1 = y1+yDiff;
                        while(x1 >= 0 && y1 < maxY)
                        {
                            antinodes = AddAntinode(x1, y1, antinodes);
                            x1 = x1-xDiff;
                            y1 = y1+yDiff;
                        }
                        x2 = x2+xDiff;
                        y2 = y2-yDiff;
                        while(x2 < maxX && y2 >= 0)
                        {
                            antinodes = AddAntinode(x2, y2, antinodes);
                            x2 = x2+xDiff;
                            y2 = y2-yDiff;
                        }
                    }
                    else if (x1 < x2 && y1 < y2)
                    {
                        x1 = x1-xDiff;
                        y1 = y1-yDiff;
                        while(x1 >= 0 && y1 >= 0)
                        {
                            antinodes = AddAntinode(x1, y1, antinodes);
                            x1 = x1-xDiff;
                            y1 = y1-yDiff;
                        }
                        x2 = x2+xDiff;
                        y2 = y2+yDiff;
                        while(x2 < maxX && y2 < maxY)
                        {
                            antinodes = AddAntinode(x2, y2, antinodes);
                            x2 = x2+xDiff;
                            y2 = y2+yDiff;
                        }
                    }
                }
            }
        }
        return antinodes;
    }
    static List<int[]> AddAntinode(int x, int y, List<int[]> antinodes)
    {
        if (!InList(antinodes, [x,y]))
        {
            antinodes.Add([x,y]);
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
