namespace EmployeeClass
{
    public abstract class Employee: IComparable<Employee>
    {
        public string Name;
        public string LastName;
        private int age;

        public abstract void dailyTask();
     
        public int CompareTo(Employee other)
        {
            if(other == null) return 1;
            return String.Compare(this.LastName, other.LastName, StringComparison.Ordinal);
        }
        
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
}

