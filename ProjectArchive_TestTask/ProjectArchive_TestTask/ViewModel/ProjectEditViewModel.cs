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
    public class ProjectEditViewModel : INotifyPropertyChanged,IEditViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _projectName;
        private string _customerCompanyName;
        private string _performerCompanyName;
        private List<Employee> _employees;
        private DateTime _startDate;
        private DateTime _doneDate;
        private int[] _priority;
        private string _error;
        private Project _project;
        private Employee _selectedEmployee;
        private int _selectedPriority;
        private string _projectInfo;
        public INavigation Navigation { get; set; }

        #region Property
        public string ProjectName
        {
            get
            {
                return _projectName;
            }
            set
            {
                if (_projectName != value)
                {
                    _projectName = value;
                    OnPropertyChanged("ProjectName");
                }
            }
        }
        public string ProjectInfo
        {
            get
            {
                return _projectInfo;
            }
            set
            {
                if (_projectInfo != value)
                {
                    _projectInfo = value;
                    OnPropertyChanged("ProjectInfo");
                }
            }
        }
        public string CustomerCompanyName
        {
            get
            {
                return _customerCompanyName;
            }
            set
            {
                if (_customerCompanyName != value)
                {
                    _customerCompanyName = value;
                    OnPropertyChanged("CustomerCompanyName");
                }
            }
        }
        public string PerformerCompanyName
        {
            get
            {
                return _performerCompanyName;
            }
            set
            {
                if (_performerCompanyName != value)
                {
                    _performerCompanyName = value;
                    OnPropertyChanged("PerformerCompanyName");
                }
            }
        }
        public Project Project
        {
            get
            {
                return _project;
            }
            set
            {
                if (_project != value)
                {
                    _project = value;
                    OnPropertyChanged("Project");
                }
            }
        }
        public Employee SelectedEmployee
        {
            get
            {
                return _selectedEmployee;
            }
            set
            {
                if (_selectedEmployee != value)
                {
                    _selectedEmployee = value;
                    OnPropertyChanged("SelectedEmployee");
                }
            }
        }
        public List<Employee> Employees
        {
            get
            {
                return _employees;
            }
            set
            {
                if (_employees != value)
                {
                    _employees = value;
                    OnPropertyChanged("Employees");
                }
            }
        }
        public DateTime StartDate
        {
            get
            {
                return _startDate;
            }
            set
            {
                if (_startDate != value)
                {
                    _startDate = value;
                    OnPropertyChanged("StartDate");
                }
            }
        }
        public DateTime DoneDate
        {
            get
            {
                return _doneDate;
            }
            set
            {
                if (_doneDate != value)
                {
                    _doneDate = value;
                    OnPropertyChanged("DoneDate");
                }
            }
        }
        public int[] Priority
        {
            get
            {
                return _priority;
            }
            set
            {
                if (_priority != value)
                {
                    _priority = value;
                    OnPropertyChanged("Priority");
                }
            }
        }
        public int SelectedPriority
        {
            get
            {
                return _selectedPriority;
            }
            set
            {
                if (_selectedPriority != value)
                {
                    _selectedPriority = value;
                    OnPropertyChanged("SelectedPriority");
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
#endregion

        public ProjectEditViewModel()
        {
            _priority = new int[] { 1, 2, 3, 4 };
            try
            {
                Employees = App.ProjectRepository.GetAllEmployees();
                Error = "";
            }
            catch (Exception ex)
            {

                Error = ex.Message;
                return;
            }
           
            
        }
        /// <summary>
        /// Получение выбранного проекта в projectListViewModel.
        /// </summary>
        /// <param name="projectId"></param>
        public void SetProject(int projectId)
        {
            try
            {
                Project = App.ProjectRepository.GetAllProject().Find(i => i.Id == projectId);
                Error = "";
                ProjectInfo = GetInfo();
            }
            catch (Exception ex)
            {

                Error = ex.Message;
                return;
            }
          
        }
        /// <summary>
        /// Удаление сотрудника из списка сотрудников проекта.
        /// </summary>
        public void Delete()
        {
            if (SelectedEmployee != null)
            {
                try
                {
                    Project.Employees.Remove(Project.Employees.Find(i => i.Id == SelectedEmployee.Id));
                    Error = "";
                    ProjectInfo = GetInfo();
                }
                catch (Exception ex)
                {

                    Error = ex.Message;
                    return;
                }
               
            }
        }
        /// <summary>
        /// Поулчение общей информации о проекте.
        /// </summary>
        /// <returns></returns>
        private string GetInfo()
        {
            var text = Project.ProjectName + Environment.NewLine + "Performer: " + Project.PerfomerCompanyName
                        + Environment.NewLine + "Customer: " + Project.CustomerCompanyName + Environment.NewLine + "Priority: " + Project.Priority
                        + Environment.NewLine + "Date of start project: " + Project.ProjectStartDate + Environment.NewLine + "Date of end project: " + Project.ProjectDoneDate
                        + Environment.NewLine + "Employee(s): ";
            var employees = Project.Employees;
            foreach (var item in employees)
            {
                text += item.FirstName + " " + item.SecondName + Environment.NewLine;
            }
            ProjectInfo = text;
            return text;
        }
        /// <summary>
        /// Добавление сотрудника в работу над проектом.
        /// </summary>
        public void Add()
        {
            try
            {
                if (SelectedEmployee == null) Error = "Employee didnt't choose";
                else
                if (Project.Employees.Exists(i => i.Id == SelectedEmployee.Id) == true)
                {
                    Error = "Employee has work on this project.";
                    return;
                }
                else
                {
                    Error = "";
                    Project.Employees.Add(SelectedEmployee);
                    ProjectInfo = GetInfo();
                }
            }
            catch (Exception ex)
            {

                Error = ex.Message;
                return;
            }
           
        }
        /// <summary>
        /// Обновление информации о проекте в БД.
        /// </summary>
        public void Edit()
        {
            Project.Priority = SelectedPriority;
            try
            {
                App.ProjectRepository.EditProject(Project);
                MessagingCenter.Send(Navigation.NavigationStack.LastOrDefault(), "Back");
                Error = "";
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
