using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace ProjectArchive_TestTask.Model
{
    [Table("Project")]
    public class Project
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        [ForeignKey(typeof(Employee))]
        public int EmployeeId { get; set; }
        public string ProjectName { get; set; }
        public string CustomerCompanyName { get; set; }
        public string PerfomerCompanyName { get; set; }
        [ManyToMany(typeof(Info), CascadeOperations =  CascadeOperation.All), Unique(Name = "Id")]
        public List<Employee> Employees { get; set; } = new List<Employee>();
        public DateTime ProjectStartDate { get; set; }
        public DateTime ProjectDoneDate { get; set; }
        public int Priority { get; set; }

    }
    [Table("Info")]
    public class Info
    {

        [ForeignKey(typeof(Project))]
        public int ProjectId { get; set; }
        [ForeignKey(typeof(Employee))]
        public int EmployeeId { get; set; }

    }
    [Table("Employee")]
    public class Employee
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        [ForeignKey(typeof(Project))]
        public int ProjectId { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Patronymic { get; set; }
        public string Email { get; set; }

        [ManyToOne]
        public Project HeadOfproject { get; set; }
        [ManyToMany(typeof(Info), CascadeOperations =  CascadeOperation.All), Unique(Name = "Id")]
        public List<Project> Projects { get; set; } = new List<Project>();
    } 
}
