
using Microsoft.Data.SqlClient; // Use this instead of System.Data.SqlClient
using EmployeenewAPI.Models;
using EmployeenewAPI.Controllers;

namespace EmployeenewAPI.DAL
{
    public class EmployeeDAL
    {
       private readonly string _connectionString;

public EmployeeDAL(string connectionString)
{
    // If connectionString is null, throw an exception
    _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
}


        public List<Employee> GetAllEmployees()
        {
            var employees = new List<Employee>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Employees", con);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read()) 
                {
                    employees.Add(new Employee
                    {
                        Id = Convert.ToInt32(rdr["Id"]),
                        Name = rdr["Name"].ToString(),
                        Age = Convert.ToInt32(rdr["Age"]),
                        Salary = Convert.ToDecimal(rdr["Salary"])
                    });
                }
            }
            return employees;
        }

        public void AddEmployee(Employee employee)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Employees (Id,Name, Age, Salary) VALUES (@Id, @Name, @Age, @Salary)", con);
                cmd.Parameters.AddWithValue("@Id", employee.Id);
                cmd.Parameters.AddWithValue("@Name", employee.Name);
                cmd.Parameters.AddWithValue("@Age", employee.Age);
                cmd.Parameters.AddWithValue("@Salary", employee.Salary);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateEmployee(int id, Employee employee)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Employees SET Name=@Name, Age=@Age, Salary=@Salary WHERE Id=@Id", con);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Name", employee.Name);
                cmd.Parameters.AddWithValue("@Age", employee.Age);
                cmd.Parameters.AddWithValue("@Salary", employee.Salary);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteEmployee(int id)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Employees WHERE Id=@Id", con);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
//this is data access layer file

