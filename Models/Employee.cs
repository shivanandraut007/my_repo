namespace EmployeenewAPI.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public decimal Salary { get; set; }
    }
}
// this is modal class directly use employee object instead of the parametere id name age salary 
