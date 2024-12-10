class Program
{
    static void Main(string[] args)
    {
        string inputFile = "/Users/kerryfinch/Documents/Projects/Advent_of_code/day10/input.txt";
        string[] map = File.ReadAllLines(inputFile);
        List<int[]> trailheads = FindTrailheads(map, '0');
        List<int[]> trailtails = FindTrailheads(map, '9');
        int total = 0;
        int trailTotal;
        int count = 0;
        for (int i = 0; i < trailheads.Count; i++)
        {
            
            RemoveTrailheads(map, trailheads,i);
            trailTotal = 0;
            for (int j = 0; j < trailtails.Count; j++)
            {
                map = File.ReadAllLines(inputFile);
                RemoveTrailheads(map, trailheads,i);
                RemoveTrailheads(map, trailtails, j);
                count = GetTrailCount(map,"0123456789",trailheads[i]);
                total+=count;
                trailTotal+=count;
                
            }
        }
        Console.WriteLine("Part 2: " + total);
    }
    
    static void RemoveTrailheads(string[] map, List<int[]> trailheads, int index)
    {
        for (int i = 0; i < trailheads.Count; i++)
        {
            if (i != index)
            {
                char[] mapRow = map[trailheads[i][0]].ToCharArray();
                mapRow[trailheads[i][1]] = '.';
                map[trailheads[i][0]] = new string(mapRow);
            }
        }
    }
    static void PrintMap(string[] map)
    {
        for (int i = 0; i < map.Length; i++)
        {
            for (int j = 0; j < map[i].Length; j++)
            {
                Console.Write(map[i][j]);
            }
            Console.WriteLine();
        }
    }
    static List<int[]> FindTrailheads(string[] input, char toFind)
    {
        List<int[]> trailheads = [];
        for (int i = 0; i < input.Length; i++)
        {
            for (int j = 0; j < input[i].Length; j++)
            {
                if (input[i][j] == toFind)
                {
                    trailheads.Add([i,j]);
                }
            }
        }
        return trailheads;
    }

    static int GetTrailCount(string[] map, string word, int[] start)
    {
        bool found;
        int count = 0;
        (count, found) = FindTrails(map, word, start[0], start[1], 0, count);
        return count;
    }

    static (int, bool) FindTrails(string[] map, string word, int i, int j, int index, int count)
    {
        bool found = false;
        if (index == word.Length-1) 
        {
            count++;
            return (count, true);
        }
        if (i < 0 || j < 0 || i >= map.Length || j >= map[i].Length || map[i][j] != word[index])
            return (count, false);
        
        if (index < word.Length -1 && i + 1 < map.Length && map[i+1][j] == word[index+1])
        {
            (count, found) = FindTrails(map, word, i + 1, j, index + 1, count);
        }
        if (index < word.Length -1 && i > 0 && map[i-1][j] == word[index+1])
        {
            (count, found) = FindTrails(map, word, i - 1, j, index + 1, count);
        }
        if (index < word.Length -1 && j + 1 < map[i].Length && map[i][j+1] == word[index+1])
        {
            (count, found) = FindTrails(map, word, i, j + 1, index + 1, count);
        }
        if (index < word.Length -1 && j > 0 && map[i][j-1] == word[index+1])
        {
            (count, found) = FindTrails(map, word, i, j - 1, index + 1, count);
        }

        return (count, found);
    }
}
