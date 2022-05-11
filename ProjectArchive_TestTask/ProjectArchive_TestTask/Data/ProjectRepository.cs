using ProjectArchive_TestTask.Model;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ProjectArchive_TestTask.Data
{
    public class ProjectRepository
    {
        SQLiteConnection db;
        public ProjectRepository(string dbPath)
        {
            db = new SQLiteConnection(dbPath);
            db.CreateTable<Project>();
            db.CreateTable<Employee>();
            db.CreateTable<Info>();
   
        }
        /// <summary>
        /// Добавление нового проекта в БД.
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="customerCompany"></param>
        /// <param name="performerCompany"></param>
        /// <param name="employee"></param>
        /// <param name="start"></param>
        /// <param name="done"></param>
        /// <param name="priority"></param>
        /// <returns></returns>
        public bool AddNewProject(string projectName, string customerCompany, string performerCompany, Employee employee, DateTime start, DateTime done, int priority)
        {
            if (string.IsNullOrWhiteSpace(projectName) || string.IsNullOrWhiteSpace(customerCompany) || string.IsNullOrWhiteSpace(performerCompany) ||
                string.IsNullOrWhiteSpace(start.ToShortDateString()) || string.IsNullOrWhiteSpace(done.ToShortDateString()) || priority < 0)
            {
                throw new Exception("Incorrect input data");
            }
            var project = new Project()
            {
                ProjectName = projectName,
                CustomerCompanyName = customerCompany,
                PerfomerCompanyName = performerCompany,
                ProjectStartDate = start,
                ProjectDoneDate = done,
                Priority = priority,

            };

            if (employee != null)
            {
                project.Employees.Add(employee);
                
            }
            var res = db.Insert(project);
            if (res < 1) return false;
            db.UpdateWithChildren(project);
            return true;
        }/// <summary>
        /// Добавление нового сотрудника в БД.
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="secondName"></param>
        /// <param name="patronymic"></param>
        /// <param name="email"></param>
        /// <param name="headOfProject"></param>
        /// <param name="project"></param>
        /// <returns></returns>
        public bool AddNewEmployee(string firstName, string secondName, string patronymic, string email, Project headOfProject, Project project)
        {
            if(string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(secondName) || string.IsNullOrWhiteSpace(patronymic))
            {
                throw new Exception("Incorrect input data");
            }
            var employee = new Employee()
            {
                FirstName = firstName,
                SecondName = secondName,
                Patronymic = patronymic,
                Email = email,
                HeadOfproject = headOfProject,
            };


            if (project != null)
            {
                employee.Projects.Add(project);
            }
            var res = db.Insert(employee);
            if (res < 1) return false;
            db.UpdateWithChildren(employee);
            return true;
        }
        /// <summary>
        /// Обновить данные указанного проекта в БД.
        /// </summary>
        /// <param name="project"></param>
        public void EditProject(Project project)
        {
            db.UpdateWithChildren(project);       
        }
        /// <summary>
        /// Обновить данные указанного сотрдуника в БД.
        /// </summary>
        /// <param name="employee"></param>
        public void EditEmployee(Employee employee)
        {
            db.UpdateWithChildren(employee);
        }
        /// <summary>
        /// Удаление проекта из БД.
        /// </summary>
        /// <param name="project"></param>
        public void DeleteProject(Project project)
        {
            db.Delete<Project>(project.Id);
        }
        /// <summary>
        /// Удаление сотрудника из БД.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public void DeleteEmployee(Employee employee)
        {
            db.Delete<Employee>(employee.Id);
        }
        public List<Project> GetAllProject()
        {
            return db.GetAllWithChildren<Project>();
        }
        /// <summary>
        /// Метод получения сотрудников на указаном проекте
        /// </summary>
        /// <param name="selectedProject"></param>
        /// <returns>Список сотрудников на проекте</returns>
        public List<Employee> GetEmployeeAtProject(Project selectedProject)
        {
            return db.Query<Employee>($"select* from Employee inner join Info on {selectedProject.Id} = Info.ProjectId where Employee.Id = Info.EmployeeId");
        }
        /// <summary>
        /// Метод получения проектов над которыми работает сотрудник.
        /// </summary>
        /// <param name="selectedEmployee"></param>
        /// <returns>Список проектов над которыми работает указанный сотрудник</returns>
        public List<Project> GetProjectAtEmployee(Employee selectedEmployee)
        {
            return db.Query<Project>($"select* from Project inner join Info on {selectedEmployee.Id} = Info.EmployeeId where Project.Id = Info.ProjectId");
        }
        /// <summary>
        /// Список всех сотрудников.
        /// </summary>
        /// <returns></returns>
        public List<Employee> GetAllEmployees()
        {
            return db.GetAllWithChildren<Employee>();
        }
    }
}
