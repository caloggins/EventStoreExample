﻿using System;

namespace EmployeesMonitor.Database
{
    public class Employee
    {
        public int EmployeeKey { get; set; }
        public Guid EmployeeId { get; set; }
        public decimal Salary { get; set; }
        public string EmployeeName { get; set; }
    }
}