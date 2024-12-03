using System;
using System.Net.Http.Headers;

namespace CSharp.Main
{
    class Program
    {
        static void Main(string[] args)
        {
            string textFile = "/Users/kerryfinch/Documents/Projects/Advent_of_code/day1/input.txt";
            string[] numbers = File.ReadAllLines(textFile);
            string[] splitNumbers;
            List<int> list1 = new List<int>();
            List<int> list2 = new List<int>();
            List<int> differences = new List<int>();
            
            for (int i = 0; i < numbers.Length; i++) 
            {
                splitNumbers = numbers[i].Split(' ');
                //Console.WriteLine(splitNumbers[0] + "and" + splitNumbers[1]);
                list1.Add(Int32.Parse(splitNumbers[0]));
                list2.Add(Int32.Parse(splitNumbers[1]));
                //Console.WriteLine(list1[i] + "and" + list2[i]);
            }

            list1.Sort();
            list2.Sort();

            for (int i = 0; i < numbers.Length; i++)
            {
                differences.Add(Math.Abs(list1[i] - list2[i]));
            }

            Console.WriteLine(differences.Sum());

            int counter;
            int similarity = 0;

            for (int i = 0; i < numbers.Length; i++)
            {
                counter = 0;
                for (int j = 0; j < numbers.Length; j++)
                {
                    if (list1[i] == list2[j])
                    {
                        counter++;
                    }
                }
                similarity += list1[i] * counter;
            }

            Console.WriteLine(similarity);
        }
    }
}