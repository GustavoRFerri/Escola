namespace InheritanceApp
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Dog MyDog = new Dog();
            MyDog.Bark();
            MyDog.Eat();

            Console.ReadKey();            
        }
    }


    class Animal
    {
        public void Eat()
        {
            Console.WriteLine("Eating...");

        }
    }

    class Dog: Animal
    {

        public void Bark() 
        {
            Console.WriteLine("Barking...");        
        }
    }
}
