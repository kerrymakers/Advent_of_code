using System.Data;
using System.Linq.Expressions;

namespace day6_2
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputFile = "/Users/kerryfinch/Documents/Projects/Advent_of_code/day6_2/testInput.txt";
            // guard postion 
            // guard direction 
            // while guard position not a i = 0, j = 0, i = width, j = length
            //  check next square in direction
            //  if obstacle turn right 90 degrees
            //  else move to next position in direction
            //       replace previous position with X
            //       counter++

            //up, down, left, right
            string[] map = File.ReadAllLines(inputFile);
            int[] guardPosition = FindGuard(map);
            char guardDirection = map[guardPosition[0]][guardPosition[1]];
            int[] initialGuardPosition = FindGuard(map);
            bool guardCanContinue = true;
            int xCounter = 0;
            char[] currentMapRow;
            char[] nextMapRow;
            int rowLength = map[0].Length - 1;
            int mapLength = map.Length - 1;
            int xCounter2 = 0;
            int counter = 0;
            int mapCount = 0;
            List<string[]> alreadyVisited;

            for (int a = 0; a < map.Length; a++)
            {
                Console.WriteLine("row " + a + "/" + map.Length);
                for (int b = 0; b < map[0].Length; b++)
                {
                    map = File.ReadAllLines(inputFile);
                    guardPosition = FindGuard(map);
                    guardDirection = map[guardPosition[0]][guardPosition[1]];
                    xCounter = 0; 
                    alreadyVisited = [];
                    currentMapRow = map[a].ToCharArray();
                    guardCanContinue = true;
                    if (currentMapRow[b] != '#' && currentMapRow[b] != '^' && currentMapRow[b] != 'v' && currentMapRow[b] != '>' && currentMapRow[b] != '<')
                    {
                        mapCount++;
                        currentMapRow[b] = '#';
                        map[a] = new string(currentMapRow);
                        while (guardCanContinue)
                        {
                            // Console.Write("Already visited: ");
                            foreach(string[] visit in alreadyVisited)
                            {
                                if(visit[0] == guardPosition[0].ToString() && visit[1] == guardPosition[1].ToString() && visit[2] == guardDirection.ToString())
                                {
                                    counter++;
                                    goto End;
                                }
                            }
                            currentMapRow = map[guardPosition[0]].ToCharArray();
                            if (guardDirection == '^')
                            {
                                if (guardPosition[0] > 0 && map[guardPosition[0]-1][guardPosition[1]] == '#')
                                {
                                    guardDirection = '>';
                                }
                                else
                                {
                                    currentMapRow[guardPosition[1]] = 'X';
                                    xCounter++;
                                    alreadyVisited.Add([guardPosition[0].ToString(),guardPosition[1].ToString(),guardDirection.ToString()]);
                                    map[guardPosition[0]] = new string(currentMapRow);
                                    if (guardPosition[0] == 0)
                                    {
                                        guardCanContinue = false;
                                    }
                                    else
                                    {
                                        nextMapRow = map[guardPosition[0]-1].ToCharArray();
                                        nextMapRow[guardPosition[1]] = '^';
                                        guardPosition[0] = guardPosition[0] - 1;
                                        map[guardPosition[0]] = new string(nextMapRow);
                                    }
                                }
                            }
                            if (guardDirection == 'v')
                            {
                                if (guardPosition[0] < mapLength && (map[guardPosition[0]+1][guardPosition[1]] == '#'))
                                {
                                    guardDirection = '<';
                                }
                                else
                                {
                                    currentMapRow[guardPosition[1]] = 'X';
                                    xCounter++;
                                    alreadyVisited.Add([guardPosition[0].ToString(),guardPosition[1].ToString(),guardDirection.ToString()]);
                                    map[guardPosition[0]] = new string(currentMapRow);
                                    if (guardPosition[0] == mapLength)
                                    {
                                        guardCanContinue = false;
                                    }
                                    else
                                    {
                                        nextMapRow = map[guardPosition[0]+1].ToCharArray();
                                        nextMapRow[guardPosition[1]] = 'v';
                                        guardPosition[0] = guardPosition[0] + 1;
                                        map[guardPosition[0]] = new string(nextMapRow);
                                    }
                                }
                            }
                            else if (guardDirection == '>')
                            {
                                if (guardPosition[1] < rowLength && map[guardPosition[0]][guardPosition[1]+1] == '#')
                                {
                                    guardDirection = 'v';
                                }
                                else
                                {
                                    currentMapRow[guardPosition[1]] = 'X';
                                    xCounter++;
                                    alreadyVisited.Add([guardPosition[0].ToString(),guardPosition[1].ToString(),guardDirection.ToString()]);
                                    if (guardPosition[1] == rowLength)
                                    {
                                        guardCanContinue = false;
                                    }
                                    else
                                    {
                                        currentMapRow[guardPosition[1]+1] = '>';
                                        guardPosition[1] = guardPosition[1] + 1;
                                    }
                                    map[guardPosition[0]] = new string(currentMapRow);
                                }
                            }
                            else if (guardDirection == '<')
                            {
                                if (guardPosition[1] > 0 && map[guardPosition[0]][guardPosition[1]-1] == '#')
                                {
                                    guardDirection = '^';
                                }
                                else
                                {
                                    currentMapRow[guardPosition[1]] = 'X';
                                    xCounter++;
                                    alreadyVisited.Add([guardPosition[0].ToString(),guardPosition[1].ToString(),guardDirection.ToString()]);
                                    if (guardPosition[1] == 0)
                                    {
                                        guardCanContinue = false;
                                    }
                                    else
                                    {
                                        currentMapRow[guardPosition[1]-1] = '<';
                                        guardPosition[1] = guardPosition[1] - 1;
                                    }
                                    map[guardPosition[0]] = new string(currentMapRow);
                                }
                            }
                        }
                        End:;
                    }
                }
            }
            

            foreach(string row in map)
            {
                foreach(char c in row)
                {
                    if (c == 'X')
                    {
                        xCounter2++;
                    }
                }
            }
            Console.WriteLine(counter);
        }

        static int[] FindGuard(string[] map)
        {
            int[] guardPosition = [0,0];
            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[i].Length; j++)
                {
                    if (map[i][j] == '^' || map[i][j] == '>' || map[i][j] == '<' || map[i][j] == 'v')
                    {
                        guardPosition = [i,j];
                    }
                }
            }
            return guardPosition;
        }
    }
}