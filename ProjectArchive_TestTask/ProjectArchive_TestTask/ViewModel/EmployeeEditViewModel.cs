using ProjectArchive_TestTask.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProjectArchive_TestTask.ViewModel
{
    public class EmployeeEditViewModel : INotifyPropertyChanged, IEditViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _firstName;
        private string _secondName;
        private string _patronymic;
        private string _email;
        private Project _headOfProject;
        private Employee _employee;
        private Project _selectedProject;
        private List<Project> _projects;
        private string _allInfo;
        private string _error;
        public INavigation Navigation { get; set; }
        public ICommand AddProjectToHeadCommand { get; protected set; }

        #region Property
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                    OnPropertyChanged("FirstName");
                }
            }
        }
        public string SecondName
        {
            get
            {
                return _secondName;
            }
            set
            {
                if (_secondName != value)
                {
                    _secondName = value;
                    OnPropertyChanged("SecondName");
                }
            }
        }

        public string Patronymic
        {
            get
            {
                return _patronymic;
            }
            set
            {
                if (_patronymic != value)
                {
                    _patronymic = value;
                    OnPropertyChanged("Patronymic");
                }
            }
        }
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged("Email");
                }
            }
        }
        public Project HeadOfProject
        {
            get
            {
                return _headOfProject;
            }
            set
            {
                if (_headOfProject != value)
                {
                    _headOfProject = value;
                    OnPropertyChanged("HeadOfProject");
                }
            }
        }
        public Project SelectedProject
        {
            get
            {
                return _selectedProject;
            }
            set
            {
                if (_selectedProject != value)
                {
                    _selectedProject = value;
                    OnPropertyChanged("SelectedProject");
                }
            }
        }
        public List<Project> Projects
        {
            get
            {
                return _projects;
            }
            set
            {
                if (_projects != value)
                {
                    _projects = value;
                    OnPropertyChanged("Projects");
                }
            }
        }

        public string Error
        {
            get
            {
                return _error;
            }
            set
            {
                if (_error != value)
                {
                    _error = value;
                    OnPropertyChanged("Error");
                }
            }
        }
        public Employee Employee
        {
            get
            {
                return _employee;
            }
            set
            {
                if (Employee != value)
                {
                    _employee = value;
                    OnPropertyChanged("Employee");
                }
            }
        }

        public string AllInfo
        {
            get
            {
                return _allInfo;    
            }
            set
            {
                if (_allInfo != value)
                {
                    _allInfo = value;
                    OnPropertyChanged("AllInfo");
                }
            }
        }

#endregion

        public EmployeeEditViewModel()
        {
            AddProjectToHeadCommand = new Command(AddProjectToHead);
            try
            {
                Projects = App.ProjectRepository.GetAllProject();
                Error = "";
            }
            catch (Exception ex)
            {

                Error = ex.Message;
                return;
            }
           
        }
        /// <summary>
        /// Получение сотрудника, выбранного в EmployeeListView.
        /// </summary>
        /// <param name="employeeID"></param>
        public void SetEmployee(int employeeID)
        {
            try
            {
                Employee = App.ProjectRepository.GetAllEmployees().Find(i => i.Id == employeeID);
                Error = "";
                AllInfo = GetInfo();
            }
            catch (Exception ex)
            {

                Error = ex.Message;
                return;
            }
           
        }
        /// <summary>
        /// Удаление проекта из списка проектов сотрудника.
        /// </summary>
        public void Delete()
        {

            try
            {
                if (SelectedProject != null) Employee.Projects.Remove(Employee.Projects.Find(i => i.Id == SelectedProject.Id));
                if (HeadOfProject != null) Employee.HeadOfproject = null;
                Error = "";
                AllInfo = GetInfo();
            }
            catch (Exception ex)
            {

                Error = ex.Message;
                return;
            }
           
        }
        /// <summary>
        /// Добавление/замена проекта, которым сотрудник руководит.
        /// </summary>
        private void AddProjectToHead()
        {
            try
            {
                if (HeadOfProject == null) Error = "Project didnt't choose";
                else
                {
                    Error = "";
                    Employee.HeadOfproject = HeadOfProject;
                    AllInfo = GetInfo();
                }
            }
            catch (Exception ex)
            {

                Error = ex.Message;
                return;
            }
           
        }
        /// <summary>
        /// Добавление проекта над которым работает сотрудник.
        /// </summary>
        public void Add()
        {
            try
            {
                if (SelectedProject == null) Error = "Project didnt't choose";
                else
            if (Employee.Projects.Exists(i => i.Id == SelectedProject.Id) == true)
                {
                    Error = "Employee has work on this project.";
                    return;
                }
                else
                {
                    Error = "";
                    Employee.Projects.Add(SelectedProject);
                    AllInfo = GetInfo();
                }
            }
            catch (Exception ex)
            {

                Error = ex.Message;
                return;
            }
            

        }
        /// <summary>
        /// Поулчение общей информации о сотруднике.
        /// </summary>
        /// <returns></returns>
        private string GetInfo()
        {

            var text = "First name: " + Employee.FirstName + Environment.NewLine + "SecondName: " + Employee.SecondName
                     + Environment.NewLine + "Patronymic: " + Employee.Patronymic + Environment.NewLine + "Project(s): ";
            var projects = Employee.Projects;
            foreach (var item in projects)
            {
                text += item.ProjectName + Environment.NewLine;
            }

            if (Employee.HeadOfproject != null) text += "HeadOfProject: " + Employee.HeadOfproject.ProjectName + Environment.NewLine;
            else text += "HeadOfProject: " + Environment.NewLine;
            return text;
        }
        /// <summary>
        /// Изменение параметров сотрудника и возврат на предыдущию страницу.
        /// </summary>
        public void Edit()
        {

            try
            {
                App.ProjectRepository.EditEmployee(Employee);
                Error = "";
                MessagingCenter.Send(Navigation.NavigationStack.LastOrDefault(), "Back");
                Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return;
            }
          

        }

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
             
           
        }
       
    }
}
