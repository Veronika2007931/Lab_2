using EmployeeClass;
using Interfaces;
namespace TesterClass
{
    public class Tester : Employee, Report
    {
        protected string method1 = "Функціональне тестування";
        protected string method2 = "Тестування продуктивності";
        protected string method3 = "Регресійне тестування";
        protected string testMethod;

        public static int testDurationMinutes;

        public Tester() : base("Володимир", "Костюк", 35)
        {
            TestMethod = method1;

        }
        public override void dailyTask()
        {
            Console.WriteLine($"{Name}: Виконує {TestMethod} і фіксує баги.");
        }

        static Tester()
        {
            testDurationMinutes = 60;
            Console.WriteLine("Tester: Викликано статичний конструктор.");
        }

        public string TestMethod
        {
            get { return testMethod; }
            set {
                if (value == method1 || value == method2 || value == method3)
                {
                    testMethod = value;
                }
                else
                {

                    throw new ArgumentException($"Метод '{value}' не використовується нашою компанією");
                }
            }
        }
        public void Report()
        {
            Console.WriteLine("Перевірив одну програму");
        }
    }
}