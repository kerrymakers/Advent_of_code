using System.IO.Pipelines;
using Microsoft.VisualBasic;

class Program
{
    static void Main(string[] args)
    {
        string inputFile = "/Users/kerryfinch/Documents/Projects/Advent_of_code/day11/input.txt";
        string[] input = File.ReadAllLines(inputFile);
        List<string> stones  = input[0].Split(' ').ToList();
        List<string> newStones;
        double noStones;
        int noBlinks;
        var watch = System.Diagnostics.Stopwatch.StartNew();

        for (int x = 1; x <=25; x++)
        {
            newStones = [];
            for (int i = 0; i < stones.Count; i++)
            {
                newStones.AddRange(Blink(stones, i));
            }
            stones = newStones;
        }
        watch.Stop();
        Console.WriteLine($"part 1 answer: {stones.Count} completed in {watch.ElapsedMilliseconds} ms");
        
        watch = System.Diagnostics.Stopwatch.StartNew();
        noBlinks = 5000;
        stones = input[0].Split(' ').ToList();
        double answer = 0;
        var cache = new Dictionary<(double stone, double noBlinks), double>();
        foreach(string stone in stones)
        {
            noStones = BlinksReccursive(double.Parse(stone), noBlinks, cache);
            answer += noStones;
        }
        watch.Stop();
        Console.WriteLine($"part 2 answer: {answer} completed in {watch.ElapsedMilliseconds} ms");
    }
    static List<string> Blink(List<string> stones, int index)
    {
        List<string> newStones = [];
        if (stones[index] == "0")
        {
            newStones.Add("1");
        }
        else if (stones[index].Length % 2 == 0)
        {
            newStones.Add(double.Parse(stones[index].Substring(0, stones[index].Length/2)).ToString());
            newStones.Add(double.Parse(stones[index].Substring(stones[index].Length/2, stones[index].Length/2)).ToString());
        }
        else
        {
            newStones.Add((double.Parse(stones[index]) * 2024).ToString());
        }
        return newStones;
    }
    static double BlinksReccursive(double stone, int noBlinks, Dictionary<(double,double),double> cache)
    {
        if (noBlinks == 0)
        {
            return 1;
        }
        else 
        {
            if (cache.TryGetValue((stone, noBlinks), out double answer))
            {
                return answer;
            }
            else if (stone == 0)
            {
                answer = BlinksReccursive(1, noBlinks - 1, cache);
            }
            else if (stone.ToString().Length % 2 == 0)
            {
                answer += BlinksReccursive(double.Parse(stone.ToString().Substring(0, stone.ToString().Length/2)), noBlinks - 1, cache);
                answer += BlinksReccursive(double.Parse(stone.ToString().Substring(stone.ToString().Length/2, stone.ToString().Length/2)), noBlinks - 1, cache);
            }
            else
            {
                answer = BlinksReccursive(stone * 2024, noBlinks - 1, cache);
            }
            cache[(stone, noBlinks)] = answer;
            return answer;
        }
    }
}
