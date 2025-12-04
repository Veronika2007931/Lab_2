using System.IO.Compression;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

using System.Text;
using EmployeeClass;
using DeveloperClass;
using TesterClass;
using ManagerClass;
using TeamLeadClass;
using System.Security.Cryptography;
using CollectionClass;

namespace Laboratorna2

{

   
    class Program
    {
        static void Main(string[] args)
        {
            // статичний конструктор
            // Console.WriteLine($"Стандартна тривалість тесту: {Tester.testDurationMinutes} хвилин.");
            // параметризований конструктор
            // Developer dev = new Developer("Ivan", "Holod", 20, "C#");
            // Конструктор за замовчуваннням
            Tester tester = new Tester();
            // Console.WriteLine($"Тестер: {tester.Name} {tester.LastName} ({tester.Age} років).");
            // приватний конструктор 
            // TeamLead leader1 = TeamLead.GetInstance("Karina", "Dorohiy");


            // Створюємо змінну базового типу Employee
            // Employee employee1;
            // Присвоюємо їй об'єкт Developer
            // employee1 = new Developer("Diana", "Kravchenko", 21, "JavaScript");
            // employee1.dailyTask();

            // employee1 = new Tester();
            // employee1.dailyTask();

            // long memoryBefore = GC.GetTotalMemory(false);
            // Developer dev1;
            // using (dev1 = new Developer("cофія", "Савицька", 25, "JS"))
            // {
            //     Console.WriteLine($" Покоління dev1:{GC.GetGeneration(dev1)}");
            // }
            // тут ми виклкаємо цей метод щоб він повернув створеного девелопера до черги фіналізції 
            // але через те що вже в діспоуз спрацював цей метод  GC.SuppressFinalize(this); який видалив
            // його з черги цей виклик буде проігноровано
            // GC.ReRegisterForFinalize(dev1);

            // Scenary2

            // Developer dev2 = new Developer("Sasha", "Stone", 34, "C#");
            // Console.WriteLine($"Покоління dev2: {GC.GetGeneration(dev2)}");
            // createGarbage();

            // dev2 = null;
            // примусовий иклик 
            // GC.Collect();
            // GC.WaitForPendingFinalizers();

            // long memoryAfter = GC.GetTotalMemory(false);
            // Console.WriteLine($"Пам'ять після GC: {memoryAfter / 1024.0:F2} KB");
            // Console.WriteLine($"Звільнено пам'яті: {(memoryBefore - memoryAfter) / 1024.0:F2} KB");


            // Обробник подій 
            Developer eventDev = new Developer("Олексій", "Грицько", 23, "Python");
            eventDev.OnWorkEnded += EventHandler;
            eventDev.OnWorkEnded += delegate (string msg)
            {
                Console.WriteLine($"Отримано повідомлення {msg}");
            };
            eventDev.OnWorkEnded += (msg) => Console.WriteLine($"Прочитано повідомлення {msg}");

            Func<int, string> expirience = (age) =>
            {
                if(age < 25) return "Джуніор";
                if(age < 35) return "Мідл";
                return "Сеньйор";
            };

            eventDev.OnWorkEnded += (msg) =>
            {
                string status = expirience(eventDev.Age);
                Console.WriteLine($"Розробник має статус {status}");
            };

            eventDev.dailyTask();
            
            // Колекції, індексатори, сортування
            Section ITSection = new Section("IT Section");
            ITSection.AddUser(new Developer("Олег", "Ярмоленко", 25, "C++"));
            ITSection.AddUser(new Tester());
            ITSection.AddUser(new Developer("Анна", "Бойко", 22, "C#"));

            Employee found = ITSection["Костюк"];
            if(found != null)
            {
                found.dailyTask();
            }
            else
            {
                Console.WriteLine("Співробітника не знайдено");
            }

            Console.WriteLine("Співробітники до сортування");
            foreach(var emp in ITSection)
            {
                Console.WriteLine($"{emp.LastName} {emp.Name}");
            }
            ITSection.SortStuff();

             Console.WriteLine("Співробітники після сортування");
            foreach(var emp in ITSection)
            {
                Console.WriteLine($"{emp.LastName} {emp.Name}");
            }

            // Extentions method
        Developer dev3 = new Developer("Dmytro", "Kovalenko", 24, "Python");
        string email = dev3.GetCorporateEmail("lll.kpi.ua");
        Console.WriteLine(email);
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
}


    

 

    
    
    