namespace day2
{
    class Program
    {
        static void Main(string[] args)
        {
            string textFile = "/Users/kerryfinch/Documents/Projects/Advent_of_code/day2/input.txt";
            //string textFile = "/Users/kerryfinch/Documents/Projects/Advent_of_code/day2/testinput.txt";
            string[] inputData = File.ReadAllLines(textFile);

            //loop through each report
            //check increase or decrease and set flag (i > i+1)
            //loop through each level
            //if i+1 
            //  if flag dec -> if i < i+1 -> exit loop
            //  else -> if i > i+1 -> exit loop
            //  equals case
            //  if abs(i - i+1) > 3 -> exit loop 
            //  safeTally++

            string[] reportArray = [];
            List<int> report = new List<int>();
            List<int> newReport = new List<int>();
            int flag;
            // inc = 1, dec = 2, equal = 3
            int safeTally = 0;
            int thingsWrong;

            for (int i = 0; i < inputData.Length; i++) 
            {
                //clear array
                report.Clear();
                thingsWrong = 0;
                //split 
                report = inputData[i].Split(' ').Select(int.Parse).ToList();
                flag = CheckFlag(report[0], report[1]);
                thingsWrong = CountIssues(report,flag);
                if (thingsWrong == 0)
                {
                    safeTally++;
                }
                //part 2
                else
                {
                    
                    for (int k = 0; k < report.Count; k++)
                    {
                        thingsWrong = 0;
                        newReport = inputData[i].Split(' ').Select(int.Parse).ToList();
                        //remove element sequentially and run checks again 
                        newReport.RemoveAt(k);
                        flag = CheckFlag(newReport[0], newReport[1]);
                        thingsWrong += CountIssues(newReport, flag);
                        if (thingsWrong == 0)
                        {
                            safeTally++;
                            break;
                        }
                    }
                }
            }

            Console.WriteLine(safeTally + " " + inputData.Length);
        }

        static int CheckFlag(int level1, int level2)
        {
            int flag;
            if (level1 < level2)
            {
                flag = 1;
            }
            else if (level1 > level2)
            {
                flag = 2;
            }
            else 
            {
                flag = 3;
            }

            return flag;
        }
        
        static int CountIssues(List<int> report, int flag)
        {
            int thingsWrong = 0;
            for (int j = 0; j < report.Count - 1; j++)
                {
                    if (report[j] == report[j+1])
                        {
                            thingsWrong++;
                        }
                    if (flag == 1)
                    {
                        if (report[j] > report[j+1])
                        {
                            thingsWrong++;
                        }
                    }
                    else if (flag == 2)
                    {
                        if (report[j] < report[j+1])
                        {
                            thingsWrong++;
                        }
                    }
                    else if (flag == 3)
                    {
                        thingsWrong++;
                    }

                    if (Math.Abs(report[j] - report[j+1]) > 3)
                    {
                        thingsWrong++;
                    }
                }
            return thingsWrong;
        }
    }
}
