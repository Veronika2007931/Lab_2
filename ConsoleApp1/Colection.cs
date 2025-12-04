using EmployeeClass;
using System.Collections;
namespace CollectionClass
{
    public class Section : IEnumerable<Employee>
    {
        private List<Employee> _stuff = new List<Employee>();

        public string  SectionName{ get; set; }

        public Section(string name)
        {
            SectionName = name;
        }

        public void AddUser(Employee emp)
        {
            _stuff.Add(emp);
        }

        public void SortStuff()
        {
            _stuff.Sort();
        }
        public Employee this[string lastName]
        {
            get
            {
                return  _stuff.Find(e => e.LastName == lastName);
                
            }
            set
            {
                _stuff.Add(value);
            }
        }

        public IEnumerator<Employee> GetEnumerator()
        {
            return  _stuff.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
            }
}