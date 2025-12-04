using EmployeeClass;
using Interfaces;

namespace ManagerClass
{
      public class Meneger : Employee, Report
    {
        protected int Subordinates;

        public Meneger(string name, string lastName, int age) : base(name, lastName, age) 
    {
        
        Subordinates = 0; 
        Console.WriteLine("Meneger: Викликано конструктор з 3 аргументами.");
    }

        public override void dailyTask()
        {
            Console.WriteLine($"{Name} {LastName}: Керує підлеглими та роздає задачі.");
        }

    public void Report()
        {
            Console.WriteLine("Провів два міти ");
        }
       }
}