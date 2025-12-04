using EmployeeClass;

public static class CorporateExtentions
{
    public static string GetCorporateEmail(this Employee emp, string domain)
    {
         string cleanName = emp.Name.Trim().ToLower();
         string cleanLastName = emp.LastName.Trim().ToLower();

         return $"{cleanName}.{cleanLastName}@{domain}";
    }
}