namespace TesteDeal
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> ints = new List<int>();
            ints.Add(13);
            ints.Add(16);
            ints.Add(23);
            ints.Add(87);
            ints.Add(109);
            ints.Add(113);

            teste(ints,87);           
        }

        public static Boolean teste(List<int> ints, int n)
        {
            for (int i = 0; i < ints.Capacity; i++)
            {
                if (ints[i] == n)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
