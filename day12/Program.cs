using System.Formats.Asn1;
using System.Xml.Schema;

class Program
{
    static void Main(string[] args)
    {
        string inputFile = "/Users/kerryfinch/Documents/Projects/Advent_of_code/day12/testInput3.txt";
        string[] garden = File.ReadAllLines(inputFile);
        bool found;
        char plotChar;
        List<List<(int,int)>> plots = [];
        List<(int,int)> plot = [];
        int perimeter = 0;
        int bulkCost = 0;
        int cost = 0;
        for (int i = 0; i < garden.Length; i++)
        {
            for (int j = 0; j < garden[i].Length; j++)
            {
                if (CheckNotInPlots(plots, i, j))
                {
                    plot = [(i, j)];
                    plotChar = garden[i][j];
                    found = FindPlot(garden, plotChar, i, j, plot);
                    perimeter = CalculatePerimeter(plot);
                    cost += plot.Count * perimeter;
                    plots.Add(plot);
                }
            }
        }
        foreach(List<(int,int)> p in plots)
        {
            bulkCost += p.Count * CalculateNoFences(p);
        }
        Console.WriteLine($"Total cost: {cost}");
        Console.WriteLine($"Total bulk cost: {bulkCost}");

    }
    static void PrintGarden(string[] garden)
    {
        foreach(string row in garden)
        {
            Console.WriteLine(row);
        }
    }
    static bool FindPlot(string[] garden, char c, int i, int j, List<(int, int)> plot)
    {
        bool found = false;
        if (i < 0 || j < 0 || i >= garden.Length || j >= garden[i].Length)
            return false;
        
        if (i + 1 < garden.Length && garden[i+1][j] == c)
        {
            if (CheckNotInPlot(plot, i+1, j))
            {
                plot.Add((i+1,j));
                found = FindPlot(garden, c, i + 1, j, plot);
            }
        }
        if (i > 0 && garden[i-1][j] == c)
        {
            if (CheckNotInPlot(plot, i-1, j))
            {
                plot.Add((i-1,j));
                found = FindPlot(garden, c, i - 1, j, plot);
            }
        }
        if (j + 1 < garden[i].Length && garden[i][j+1] == c)
        {
            if (CheckNotInPlot(plot, i, j+1))
            {
                plot.Add((i,j+1));
                found = FindPlot(garden, c, i, j + 1, plot);
            }
        }
        if (j > 0 && garden[i][j-1] == c)
        {
            if (CheckNotInPlot(plot, i, j-1))
            {
                plot.Add((i,j-1));
                found = FindPlot(garden, c, i, j - 1, plot);
            }
        }
        return found;
    }
    static bool CheckNotInPlot(List<(int,int)> plot, int i, int j)
    {
        foreach((int,int) p in plot)
        {
            if (p == (i,j))
            {
                return false;
            }
        }
        return true;
    }
    static bool CheckNotInPlots(List<List<(int,int)>> plots, int i, int j)
    {
        foreach(List<(int,int)> plot in plots)
        {
            foreach((int,int) p in plot)
            {
                if (p == (i,j))
                {
                    return false;
                }
            }
        }
        return true;
    }
    static int CalculatePerimeter(List<(int,int)> plot)
    {
        //inital perimeter is 4 * plot count 
        // for each item in plot check if touching each coord
        // if it is subtract 1 from perimeter 
        int perimeter = 0;
        foreach((int,int) p in plot)
        {
            if (CheckNotInPlot(plot, p.Item1+1, p.Item2))
            {
                perimeter++;
            }
            if (CheckNotInPlot(plot, p.Item1-1, p.Item2))
            {
                perimeter++;
            }
            if (CheckNotInPlot(plot, p.Item1, p.Item2+1))
            {
                perimeter++;
            }
            if (CheckNotInPlot(plot, p.Item1, p.Item2-1))
            {
                perimeter++;
            }
        }
        return perimeter;
    }
    static (int,int) FindMax(List<(int,int)> plot)
    {
        int maxi = 0;
        int maxj = 0;
        foreach((int,int) p in plot)
        {
            if (p.Item1 > maxi)
            {
                maxi = p.Item1;
            }
            if (p.Item2 > maxj)
            {
                maxj = p.Item2;
            }
        }
        return (maxi,maxj);
    }
    static (int,int) FindMin(List<(int,int)> plot)
    {
        int mini = plot[0].Item1;
        int minj = plot[0].Item2;
        foreach((int,int) p in plot)
        {
            if (p.Item1 < mini)
            {
                mini = p.Item1;
            }
            if (p.Item2 < minj)
            {
                minj = p.Item2;
            }
        }
        return (mini,minj);
    }
    static int CalculateNoFences(List<(int,int)> plot)
    {
        int fences = 0;
        (int i, int j) max = FindMax(plot);
        (int i, int j) min = FindMin(plot);
        for (int i = min.i; i <= max.i; i++)
        {
            for (int j = min.j; j <= max.j; j++)
            {
                if (!CheckNotInPlot(plot,i,j))
                {
                    if (CheckNotInPlot(plot,i,j-1))
                    {
                        if(CheckNotInPlot(plot,i-1,j))
                        {
                            fences++;
                        }
                        else if(!CheckNotInPlot(plot,i-1,j-1))
                        {
                            fences++;
                        }
                    }
                    if (CheckNotInPlot(plot,i-1,j))
                    {
                        if(CheckNotInPlot(plot,i,j+1))
                        {
                            fences++;
                        }
                        else if(!CheckNotInPlot(plot,i-1,j+1))
                        {
                            fences++;
                        }
                    }
                    if (CheckNotInPlot(plot,i+1,j))
                    {
                        if(CheckNotInPlot(plot,i,j-1))
                        {
                            fences++;
                        }
                        else if(!CheckNotInPlot(plot,i+1,j-1))
                        {
                            fences++;
                        }
                    }
                    if (CheckNotInPlot(plot,i,j+1))
                    {
                        if(CheckNotInPlot(plot,i-1,j))
                        {
                            fences++;
                        }
                        else if(!CheckNotInPlot(plot,i-1,j+1))
                        {
                            fences++;
                        }
                    }
                }
            }
        }
        return fences;
    }
}
