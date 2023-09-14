using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazun
{
    public class Screen
    {
        private Company company = new Company();

        private string FilePath = "CompanyWorkers.json";

        public void Show()
        {
            Console.WriteLine("\t\t\tAmazon company");
            while (true)
            {
                Console.WriteLine("1. Add employee");
                Console.WriteLine("2. Remove employee");
                Console.WriteLine("3. Modify employee");
                Console.WriteLine("4. Calculate salary");
                Console.WriteLine("5. Show all employees");
                Console.WriteLine("0. Exit");
                string choiceAsString = Console.ReadLine();
                ScreenChoices choice = (ScreenChoices)Convert.ToInt32(choiceAsString);
                switch(choice)
                {
                    case ScreenChoices.AddEmployee:
                        AddNewEmployee();
                        break;
                    case ScreenChoices.RemoveEmployee:
                        RemoveEmployeeFromCompany();
                        break;

                    case ScreenChoices.ShowEmployees:
                        ShowEmployees();
                        break;

                    case ScreenChoices.CalculateSalary:
                        CalculateEmployeeSalary();
                        break;
                    case ScreenChoices.Exit:
                        Console.WriteLine("Goodbye!");
                        return;
                }
            }
        }


        #region Private Methods
        private void CalculateEmployeeSalary()
        {
            Console.Write("Enter an employee Id at first: ");
            string employeeId = Console.ReadLine();
            Console.Write("Enter how many hours the employee worked: ");
            int hoursWorked = Convert.ToInt32(Console.ReadLine());
            company.CalculateSalary(employeeId, hoursWorked);
        }

        private void ShowEmployees()
        {
            List<Employee> employees = company.GetListEmployees(FilePath);
            foreach (Employee emp in employees) 
            {
                Console.WriteLine($"{emp.EmployeeId}[] {emp.Name, -20}[] {emp.Position,-7}[] {emp.HourlyRate}$ ");
            }
        }

        private void RemoveEmployeeFromCompany()
        {
            Console.Write("Enter Id of employee, you want to delete: ");
            string employeeIdToDelete = Console.ReadLine();
            company.RemoveEmployee(employeeIdToDelete);
        }

        private void AddNewEmployee()
        {
            Console.Write("Enter name of employee: ");
            string name = Console.ReadLine();

            Console.Write("Enter position of employee (Manager или Worker): ");
            string position = Console.ReadLine();

            Employee newEmployee;
            if (position.Equals("Manager", StringComparison.OrdinalIgnoreCase))
            {
                newEmployee = new Manager();
            }
            else
            {
                newEmployee = new Employee();
            }

            newEmployee.Name = name;
            newEmployee.HourlyRate = 35.0;
            newEmployee.Position = position;
            company.AddEmployee(newEmployee);

            Console.WriteLine("New employee added.");
        }

        #endregion // Private Methods
    }
}
