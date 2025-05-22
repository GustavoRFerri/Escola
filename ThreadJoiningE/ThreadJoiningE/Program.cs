namespace ThreadJoiningE
{
    internal class Program
    {
        static void Main(string[] args)
        {            
            Console.WriteLine("Hello, World!");

            Thread thread1 = new Thread(Thread1Function);
            Thread thread2 = new Thread(Thread2Function);

            thread1.Start();
            thread2.Start();

            //
            //thread1.Join();
            //Console.WriteLine(" Thread 1 Function DONE ");
            //


            if (thread1.Join(1000))            
                Console.WriteLine(" Thread 1 Function DONE ");
            else            
                Console.WriteLine(" Thread 1 Function wasn´t done within 1 sec ");

            thread2.Join();
            Console.WriteLine(" Thread 2 Function DONE ");

            for (int i = 0; i < 10; i++)
            {
                if (thread1.IsAlive)
                {
                    Console.WriteLine(" Thread 1 is still doing stuff {0}",i);
                    Thread.Sleep(300);
                }
                else
                    Console.WriteLine(" Thread completed {0}",i);
                
            }

            Console.WriteLine(" Main Thread Ended ");

        }


        public static void Thread1Function()
        {
            Console.WriteLine(" Thread 1 Function started ");
            Thread.Sleep(3000);

            Console.WriteLine(" Thread 1 Function coming back to caller ");
        }


        public static void Thread2Function()
        {
            Console.WriteLine(" Thread 2 Function started ");
        }
    }
}
