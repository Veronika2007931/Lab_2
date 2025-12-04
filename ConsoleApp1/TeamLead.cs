
using Interfaces;
using ManagerClass;

namespace TeamLeadClass
{
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

