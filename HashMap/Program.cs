using System.Data;

namespace HashMap
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Hashmap<int, string> map = new Hashmap<int, string>(1);

            List<int> ints = new List<int>();
           



            map.Add(1, "rohan");
            map.Add(2, "roha");
            map.Add(3, "roh");
            map.Add(4, "ro");
            map.Add(5, "r");
            map.Add(6, "ro2");
            map.Add(7, "roh2");
            map.Add(8, "roha2");
            map.Add(9, "rohan2");


            map.Remove(7);
            

            var temp = map.ContainsKey(1);


        }
    }
}