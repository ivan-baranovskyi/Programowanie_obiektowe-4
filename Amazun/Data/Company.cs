using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazun
{
    public class Company
    {
        public List<Employee> Employees = new List<Employee>();
        private string FilePath { get; set; } = "CompanyWorkers.json";

        /// <summary>
        /// Adds new employee
        /// </summary>
        /// <param name="newEmployee"></param>
        public void AddEmployee(Employee newEmployee)
        {
            Employees = GetListEmployees(FilePath);
            if (newEmployee is Manager manager)
            {
                manager.Bonus = 150;
            }

            do
            {
                newEmployee.EmployeeId = GenerateUniqueEmployeeId();
            } while (Employees.Any(e => e.EmployeeId == newEmployee.EmployeeId));

            Employees.Add(newEmployee);
            SerializeToJson(Employees);
        }

        /// <summary>
        /// Removes employees
        /// </summary>
        /// <param name="employeeId"></param>
        public void RemoveEmployee(string employeeId)
        {
            Employees = GetListEmployees(FilePath);
            Employee employeeToRemove = Employees.FirstOrDefault(e => e.EmployeeId == employeeId);

            if (employeeToRemove != null)
            {
                Employees.Remove(employeeToRemove);

                SerializeToJson(Employees);

                Console.WriteLine($"Employee with EmployeeId {employeeId} was deleted.");
            }
            else
            {
                Console.WriteLine($"Employee with EmployeeId {employeeId} not found.");
            }
        }

        /// <summary>
        /// Calculating salary
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="countOfHours"></param>
        public void CalculateSalary(string employeeId, int countOfHours)
        {
            Employees = GetListEmployees(FilePath);
            Employee employee = Employees.FirstOrDefault(e => e.EmployeeId == employeeId);
            if (employee == null)
            {
                Console.WriteLine($"Employee with EmployeeId {employeeId} not found.");
            }

            double salary = countOfHours * employee.HourlyRate;
            if (employee.Position.Equals("Manager"))
            {
                salary += 150;
            }
            Console.WriteLine($"Salary in this month will be {salary}");
        }

        /// <summary>
        /// Deserializing employees
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public List<Employee> GetListEmployees(string filePath)
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<List<Employee>>(json);
            }
            else
            {
                Console.WriteLine($"File {filePath} not found. Сreated new employees list.");
                return new List<Employee>();
            }
        }

        /// <summary>
        /// Serializing employees
        /// </summary>
        /// <param name="company"></param>
        public void SerializeToJson(List<Employee> company)
        {
            string json = JsonConvert.SerializeObject(company, Formatting.Indented);
            File.WriteAllText(FilePath, json);
        }

        /// <summary>
        /// Generating unique employee Id
        /// </summary>
        /// <returns></returns>
        private string GenerateUniqueEmployeeId()
        {
            var random = new Random();
            string employeeId;

            do
            {
                employeeId = "AN" + random.Next(100000, 999999).ToString();
            } while (Employees.Any(e => e.EmployeeId == employeeId));

            return employeeId;
        }
    }
}
