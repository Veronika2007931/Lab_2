using EmployeeClass;
using Interfaces;

namespace DeveloperClass
{
    public class Developer : Employee, IDisposable,IDeveloperWork, Report
    {
        protected string ProgrammingLanguage;
        private bool _isDisposed = false;
        // імітація некерованого ресурсу(файл)
        private System.IntPtr _fileHandle;

        public event Action<string> OnWorkEnded;

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
}