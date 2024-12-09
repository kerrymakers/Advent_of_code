using System.Globalization;
using System.Runtime.InteropServices;

class Program
{
    static void Main(string[] args)
    {
        string inputFile = "/Users/kerryfinch/Documents/Projects/Advent_of_code/day9/input.txt";
        string[] input = File.ReadAllLines(inputFile);
        List<string> layout;
        List<string> p1Layout;
        List<string> p2Layout;
        List<int[]> emptyBlocks;
        List<int[]> blocks;
        var watch = System.Diagnostics.Stopwatch.StartNew();

        (layout, emptyBlocks, blocks) = MakeLayout(input);
        p1Layout = Part1(layout);
        CalculateP1Output(p1Layout);
        watch.Stop();
        Console.WriteLine($"Execution Time for part 1: {watch.ElapsedMilliseconds} ms");
        
        watch = System.Diagnostics.Stopwatch.StartNew();
        (layout, emptyBlocks, blocks) = MakeLayout(input);
        p2Layout = Part2(layout, emptyBlocks, blocks, input[0]);
        CalculateP2Output(p2Layout);
        watch.Stop();
        Console.WriteLine($"Execution Time for part 2: {watch.ElapsedMilliseconds} ms");
    }
    static List<string> AddToLayout(List<string> layout, string stringToAdd, int length)
    {
        for (int j = 0; j < length; j++)
        {
            layout.Add(stringToAdd);
        }
        return layout;
    }

    static List<int[]> CalculateEmptyBlocks(List<string> layout)
    {
        List<int[]> emptyBlocks = [];
        int count = 1;
        for (int i = 0; i < layout.Count-1; i++)
        {
            if (layout[i] == ".")
            {
                if (layout[i+1] == ".")
                {
                    count++;
                }
                else
                {
                    emptyBlocks.Add([i + 1 - count,count]);
                    count = 1;
                }
            }
        }
        return emptyBlocks;
    }
    static (List<string>, List<int[]>, List<int[]>) MakeLayout(string[] input)
    {
        List<string> layout = [];
        //stating index, size 
        List<int[]> emptyBlocks = [];
        //starting index, size, number
        List<int[]> blocks = [];
        int fileId = 0;
        for (int i = 0; i < input[0].Length; i++)
        {
            if (i % 2 == 1)
            {
                if (int.Parse(input[0][i].ToString()) > 0)
                {
                    emptyBlocks.Add([layout.Count, int.Parse(input[0][i].ToString())]);
                    layout = AddToLayout(layout, ".", int.Parse(input[0][i].ToString()));
                }
            }
            else
            {
                if (int.Parse(input[0][i].ToString()) > 0)
                {
                    blocks.Add([layout.Count, int.Parse(input[0][i].ToString()), fileId]);
                    layout = AddToLayout(layout, fileId.ToString(), int.Parse(input[0][i].ToString()));
                    fileId++;
                }
            }
        }
        return (layout, emptyBlocks, blocks);
    }

    static List<string> Part1(List<string> layout)
    {
        int gapsLeft;
        for (int i = layout.Count - 1; i >= 0; i--)
        {
            gapsLeft = 0;
            for (int j = 0; j < i; j++)
            {
                if (layout[j] == ".")
                {
                    gapsLeft++;
                }
            }
            if (gapsLeft > 0)
            {
                if (layout[i] != ".")
                {
                    for (int j = 0; j < layout.Count; j++)
                    {
                        if (layout[j] == ".")
                        {
                            layout[j] = layout[i];
                            layout[i] = ".";
                            break;
                        }
                    }
                }
            }
        }
        return layout;
    }

    static void PrintLayout(List<string> layout)
    {
        foreach(string c in layout)
        {
            Console.Write(c);
        }
        Console.WriteLine();
    }

    static void CalculateP1Output(List<string> layout)
    {
        decimal total = 0;
        for (int i = 0; i < layout.Count; i++)
        {
            if (layout[i] == ".")
            {
                break;
            }
            else
            {
                total += i * int.Parse(layout[i]);
            }
        }
        Console.WriteLine("Part 1 answer: " + total);
    }

    static void CalculateP2Output(List<string> layout)
    {
        decimal total = 0;
        for (int i = 0; i < layout.Count; i++)
        {
            if (layout[i] != ".")
            {    
                total += i * int.Parse(layout[i]);
            }
        }
        Console.WriteLine("Part 2 answer: " + total);
    }

    static List<string> Part2(List<string> layout, List<int[]> emptyBlocks, List<int[]> blocks, string input)
    {
        //loop through non empty blocks from right to left
        //  loop through empty blocks from left to right 
        //      if empty block size > block size 
        //          for 0 to block size
        //              layout[empty block start + k] = block number
        //          empty block size -= block size
        //          empty block size start = empty block size start + block size 
        //          break
        for (int i = blocks.Count - 1; i > 0; i--)
        {
            for (int j = 0; j < emptyBlocks.Count; j++)
            {
                if (emptyBlocks[j][0] < blocks[i][0])
                {    
                    if (emptyBlocks[j][1] >= blocks[i][1])
                    {
                        for (int k = 0; k < blocks[i][1]; k++)
                        {
                            layout[emptyBlocks[j][0] + k] = blocks[i][2].ToString();
                            layout[blocks[i][0] + k] = ".";
                        }
                        emptyBlocks = CalculateEmptyBlocks(layout);
                        break;
                    }
                }
            }
        }
        return layout;
    }
}