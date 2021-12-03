using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPL.SMS.R2S.Training.Entities
{
    class Employee
    {
        private int employeeId;
        private string employeeName;
        private double salary;
        private int supervisorId;

        public int EmployeeId { get => employeeId; set => employeeId = value; }
        public string EmployeeName { get => employeeName; set => employeeName = value; }
        public double Salary { get => salary; set => salary = value; }
        public int SupervisorId { get => supervisorId; set => supervisorId = value; }

        public Employee()
        {

        }

        public Employee(int employeeId, string employeeName, double salary, int supervisorId)
        {
            EmployeeId = employeeId;
            EmployeeName = employeeName;
            Salary = salary;
            SupervisorId = supervisorId;
        }   
    }
}
