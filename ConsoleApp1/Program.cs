using System.IO.Compression;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

using System.Text;


namespace Laboratorna1

{

    public delegate void WorkEnded(string message);
    public interface IDeveloperWork
    {
        void Create();
    }

    public interface IManagerWork
    {
        void Create();
    }

    public interface Report
    {
        void Report();
    }
    class Program
    {
        static void Main(string[] args)
        {
            // статичний конструктор
            Console.WriteLine($"Стандартна тривалість тесту: {Tester.testDurationMinutes} хвилин.");
            // параметризований конструктор
            Developer dev = new Developer("Ivan", "Holod", 20, "C#");
            // Конструктор за замовчуваннням
            Tester tester = new Tester();
            Console.WriteLine($"Тестер: {tester.Name} {tester.LastName} ({tester.Age} років).");
            // приватний конструктор 
            TeamLead leader1 = TeamLead.GetInstance("Karina", "Dorohiy");


            // Створюємо змінну базового типу Employee
            Employee employee1;
            // Присвоюємо їй об'єкт Developer
            employee1 = new Developer("Diana", "Kravchenko", 21, "JavaScript");
            employee1.dailyTask();

            employee1 = new Tester();
            employee1.dailyTask();

            long memoryBefore = GC.GetTotalMemory(false);
            Developer dev1;
            using (dev1 = new Developer("cофія", "Савицька", 25, "JS"))
            {
                Console.WriteLine($" Покоління dev1:{GC.GetGeneration(dev1)}");
            }
            // тут ми виклкаємо цей метод щоб він повернув створеного девелопера до черги фіналізції 
            // але через те що вже в діспоуз спрацював цей метод  GC.SuppressFinalize(this); який видалив
            // його з черги цей виклик буде проігноровано
            GC.ReRegisterForFinalize(dev1);

            // Scenary2

            Developer dev2 = new Developer("Sasha", "Stone", 34, "C#");
            Console.WriteLine($"Покоління dev2: {GC.GetGeneration(dev2)}");
            createGarbage();

            dev2 = null;
            // примусовий иклик 
            GC.Collect();
            GC.WaitForPendingFinalizers();

            long memoryAfter = GC.GetTotalMemory(false);
            Console.WriteLine($"Пам'ять після GC: {memoryAfter / 1024.0:F2} KB");
            Console.WriteLine($"Звільнено пам'яті: {(memoryBefore - memoryAfter) / 1024.0:F2} KB");


            // Обробник подій 
            Developer eventDev = new Developer("Олексій", "Грицько", 23, "Python");
            eventDev.OnWorkEnded += EventHandler;
            eventDev.dailyTask();
            eventDev.OnWorkEnded -= EventHandler;
        }

        static void EventHandler(string message)
        {
            Console.WriteLine("Менеджер почув: " + message);
            Console.WriteLine("Менеджер: Чудово, можеш йти додому.");
        }

        static void createGarbage()
        {
            for(int i = 0; i<2000; i++)
            {
                Developer tempDev = new Developer("Сміттєвий", "Об'єкт", 20, "C#");
            }
        }
    }



    public abstract class Employee
    {
        public string Name;
        public string LastName;
        private int age;

        public abstract void dailyTask();

        // public Employee()
        // {
        //     Name = "Veronika";
        //     lastName = "Niema";
        //     Age = 18;
        //     Console.WriteLine("Employee: Викликано конструктор за замовчанням.");
        // }
         public Employee(string name, string lastName, int age)
        {
            Name = name;
            LastName = lastName;
            Age = age;
            // Console.WriteLine("Викликано конструктор з параметрами ");
        }
        public int Age
        {
            get { return age; }
            set
            {
                if (value >= 18 && value <= 50)
                {
                    age = value;
                }    
                  else
                    throw new ArgumentException("Вік має бути в межах 18–50");
            }
        }



    }

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
    

    public class Developer : Employee, IDisposable,IDeveloperWork, Report
    {
        protected string ProgrammingLanguage;
        private bool _isDisposed = false;
        // імітація некерованого ресурсу(файл)
        private System.IntPtr _fileHandle;

        public event WorkEnded OnWorkEnded;

        ~Developer()
        {
            // Console.WriteLine("Finalizer: Деструктор викликано GC");
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);


        }

          protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed) return;
            if (disposing)
            {
                //  Тут має бути логіка звільнення IDisposable об'єктів, 
                // які цей клас міг би створювати. У цьому класі їх немає, тому:
                Console.WriteLine("Dispose: Перевірка керованих об'єктів завершена.");
            }


            if (_fileHandle != System.IntPtr.Zero)
            {

                _fileHandle = System.IntPtr.Zero; // Імітуємо закриття
            }
            _isDisposed = true;

         
        }

        public Developer(string name, string lastName, int age, string programmingLanguage) : base(name, lastName, age)
        {
            this.ProgrammingLanguage = programmingLanguage;
            _fileHandle = new System.IntPtr(12345);
            // Console.WriteLine(" Викликано конструктор з параметрами.");
        }

        public override void dailyTask()
        {
            Console.WriteLine($"{Name}: Пише код на {ProgrammingLanguage}");

            OnWorkEnded?.Invoke($"{Name} Завершив свою роботу ");
        }

           public void Create()
        {
            Console.WriteLine($"Developer {Name}: Створює новий функціонал (код).");
        }

        public void Report()
        {
            Console.WriteLine("Я написав сто рядків коду");
        }

     
    }
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
    public class TeamLead : Meneger, IDeveloperWork, IManagerWork
    {
        protected string TeamTask;
        // Статичне поле для зберігання єдиного екземпляру для приватного конструктора 
        private static TeamLead _instance;

        private TeamLead(string name, string lastName) : base(name, lastName, 40)
        {
            TeamTask = "Керування розробкою";
            Console.WriteLine($"TeamLead: Викликано приватний конструктор для {name}.");
        }
        
        public static TeamLead GetInstance(string name, string lastName)
        {
            if (_instance == null)
            {
                _instance = new TeamLead(name, lastName);
            }
            else
            {
                Console.WriteLine($"TeamLead: Екземпляр вже існує. Повертаємо {_instance.Name}.");
            }
            return _instance;
        }

        void IDeveloperWork.Create()
        {
            Console.WriteLine($"TeamLead {Name} (як розробник): Пише складну архітектуру.");
        }

        void IManagerWork.Create()
        {
            Console.WriteLine($"TeamLead {Name} (як менеджер): Створює команду та розподіляє задачі.");
        }
    }
}
