using System.Text.Json;
using EmployeeClass;

namespace DataService
{
    public static class DataManager
    {
        private static string filePath = "employees.json";

        public static void SaveData(List<Employee> employees)
        {
            var options = new JsonSerializerOptions {WriteIndented = true};

            string jsonString = JsonSerializer.Serialize(employees, options);
            File.WriteAllText(filePath, jsonString);
            Console.WriteLine("\n[Дані успішно збережено у файл employees.json]");
        }

        public static List<Employee> LoadData()
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("\n[Файл збереження не знайдено. Створено новий список.]");
                return new List<Employee>();
            }

            string jsonString = File.ReadAllText(filePath);

            List<Employee> employees = JsonSerializer.Deserialize<List<Employee>>(jsonString);
            return employees;
        }
    };

    
}