namespace day5
{
    class Program
    {
        static void Main(string[] args)
        {
            //string ordersFile = "/Users/kerryfinch/Documents/Projects/Advent_of_code/day5/testOrders.txt";
            //string updatesFile = "/Users/kerryfinch/Documents/Projects/Advent_of_code/day5/testUpdates.txt";
            string ordersFile = "/Users/kerryfinch/Documents/Projects/Advent_of_code/day5/orders.txt";
            string updatesFile = "/Users/kerryfinch/Documents/Projects/Advent_of_code/day5/updates.txt";
            string[] orders = File.ReadAllLines(ordersFile);
            string[] updates = File.ReadAllLines(updatesFile);
            string[] splitUpdate;
            List<string> thingsWrong = new List<string>();
            int total = 0;
            int totalp2 = 0;
            bool needsRearranging = false;

            //loop through updates 
            //loop through each page of updates
            //find in orders list
            //check condition

            for (int i = 0; i < updates.Length; i++)
            {
                needsRearranging = false;
                splitUpdate = updates[i].Split(',');
                thingsWrong.Clear();
                thingsWrong = CheckThingsWrong(splitUpdate, orders);
                if (thingsWrong.Count > 0)
                {
                    needsRearranging = true;
                }
                while (thingsWrong.Count > 0)
                {
                    for (int l = 0; l < thingsWrong.Count; l++)
                    {
                        List<string> sortedUpdate = splitUpdate.ToList();
                        string[] splitThingsWrong = thingsWrong[l].Split('|');
                        string holder;
                        holder = splitUpdate[Array.IndexOf(splitUpdate, splitThingsWrong[1])];
                        sortedUpdate.RemoveAt(Array.IndexOf(splitUpdate, splitThingsWrong[1]));
                        sortedUpdate.Insert(Array.IndexOf(splitUpdate, splitThingsWrong[0]), holder);
                        splitUpdate = sortedUpdate.ToArray();
                    }
                    thingsWrong = CheckThingsWrong(splitUpdate, orders);
                }
                if (needsRearranging == false)
                {
                    //find middle and add to total
                    int mid = Int32.Parse(splitUpdate[splitUpdate.Length / 2]);
                    total += mid;
                }
                else
                {
                    int mid = Int32.Parse(splitUpdate[splitUpdate.Length / 2]);
                    totalp2 += mid;
                }
            }
            Console.WriteLine(total);
            Console.WriteLine(totalp2);
        }
        static List<string> CheckThingsWrong(string[] splitUpdate, string[] orders)
        {
            List<string> thingsWrong = new List<string>();
            for (int j = 0; j < splitUpdate.Length; j++)
            {
                for (int k = 0; k < orders.Length; k++)
                {
                    string[] splitOrder = orders[k].Split('|');
                    //check first page in order
                    if (splitUpdate[j] == splitOrder[0])
                    {
                        if (splitUpdate.Contains(splitOrder[1]))
                        {
                            //check first page number comes before second
                            int indexPage2 = Array.IndexOf(splitUpdate, splitOrder[1]);
                            if (j > indexPage2)
                            {
                                thingsWrong.Add(orders[k]);
                            }
                        }
                    }
                }
            }
            
            return thingsWrong; //list of orders 
        }
    }
}