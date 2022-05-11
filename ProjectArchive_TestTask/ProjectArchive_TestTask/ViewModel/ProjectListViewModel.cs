using ProjectArchive_TestTask.Data;
using ProjectArchive_TestTask.Model;
using ProjectArchive_TestTask.View;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProjectArchive_TestTask.ViewModel
{
    public class ProjectListViewModel : INotifyPropertyChanged,IListViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        List<Project> _projects;
        private string _projectInfo;
        private Project _selectedProject;
        private int[] _priority;
        private int _selectedPriorityFrom;
        private int _selectedPriorityTo;
        private string[] _sortMethodList;
        private string _selectedSortMethod;
        private DateTime _firstDate;
        private DateTime _secondDate;
        public INavigation Navigation { get; set; }
        public ICommand Choose { get; protected set; }
        public ICommand FilterCommand { get; protected set; }
        public ICommand SortCommand { get; protected set; }

        #region Property
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
                    var temp = _selectedProject.ProjectName + Environment.NewLine + "Performer: " + _selectedProject.PerfomerCompanyName
                        + Environment.NewLine + "Customer: " + _selectedProject.CustomerCompanyName + Environment.NewLine + "Priority: " + _selectedProject.Priority
                        + Environment.NewLine + "Date of start project: " + _selectedProject.ProjectStartDate.ToShortDateString() + Environment.NewLine + "Date of end project: " + _selectedProject.ProjectDoneDate.ToShortDateString()
                        + Environment.NewLine + "Employee(s): ";
                    var employees = App.ProjectRepository.GetEmployeeAtProject(_selectedProject);
                    foreach (var item in employees)
                    {
                        temp += item.FirstName + " " + item.SecondName + Environment.NewLine;
                    }
                    ProjectInfo = temp;
                    OnPropertyChanged("SelectedEProject");
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
        public int SelectedPriorityFrom
        {
            get
            {
                return _selectedPriorityFrom;
            }
            set
            {
                if (_selectedPriorityFrom != value)
                {
                    _selectedPriorityFrom = value;
                    OnPropertyChanged("SelectedPriorityFrom");
                }
            }
        }
        public int SelectedPriorityTo
        {
            get
            {
                return _selectedPriorityTo;
            }
            set
            {
                if (_selectedPriorityTo != value)
                {
                    _selectedPriorityTo = value;
                    OnPropertyChanged("SelectedPriorityTo");
                }
            }
        }
        public DateTime FirstDate
        {
            get
            {
                return _firstDate;
            }
            set
            {
                if (_firstDate != value)
                {
                    _firstDate = value;
                    OnPropertyChanged("FirstDate");
                }
            }
        }
        public DateTime SecondDate
        {
            get
            {
                return _secondDate;
            }
            set
            {
                if (_secondDate != value)
                {
                    _secondDate = value;
                    OnPropertyChanged("SecondDate");
                }
            }
        }
        public string[] SortMethodList
        {
            get
            {
                return _sortMethodList;
            }
            set
            {
                if (_sortMethodList != value)
                {
                    _sortMethodList = value;
                    OnPropertyChanged("SortMethodList");
                }
            }
        }
        public string SelectedSortMethod
        {
            get
            {
                return _selectedSortMethod;
            }
            set
            {
                if (_selectedSortMethod != value)
                {
                    _selectedSortMethod = value;
                    OnPropertyChanged("SelectedSortMethod");
                }
            }
        }
        #endregion
        public ProjectListViewModel()
        {
            Action filter = FilterByPriority;
            filter += FilterByDate;
            FilterCommand = new Command(filter);
            SortCommand = new Command(Sort);
            try
            {
                Projects = App.ProjectRepository.GetAllProject();
                ProjectInfo = "";
            }
            catch (Exception ex)
            {

                ProjectInfo = ex.Message;
            }
           
            Priority = new int[] { 1, 2, 3, 4 };
            SortMethodList = new string[]{ "Priority ↓", "Priority ↑", "Employees count ↓", "Employees count ↑"};
            SubscribeToRefreshData();
        }

        private void SubscribeToRefreshData()
        {
            MessagingCenter.Subscribe<Page>(this, "Back", (sender) => { Projects = App.ProjectRepository.GetAllProject(); });
        }
        /// <summary>
        /// Удаление проекта из списка и БД.
        /// </summary>
        public void Delete()
        {
            try
            {
                if (SelectedProject != null)
                {
                    App.ProjectRepository.DeleteProject(SelectedProject);
                    Projects = App.ProjectRepository.GetAllProject();
                    ProjectInfo = "";
                }
            }
            catch (Exception ex)
            {

                ProjectInfo = ex.Message;
                return;
            }
           
        }
        /// <summary>
        /// Фильтр по приоритетам проекта.
        /// </summary>
        private void FilterByPriority()
        {
            try
            {
                if (SelectedPriorityFrom < SelectedPriorityTo)
                {
                    var temp = App.ProjectRepository.GetAllProject().FindAll(i => i.Priority >= SelectedPriorityFrom && i.Priority <= SelectedPriorityTo);
                    Projects = temp;
                 
                  
                    ProjectInfo = "";
                }
            }
            catch (Exception ex)
            {

                ProjectInfo = ex.Message;
                return;
            }
           
        }
        /// <summary>
        /// Фильтр по дате начала и окончания проекта.
        /// </summary>
        private void FilterByDate()
        {
            try
            {
                if (FirstDate.ToShortDateString() != "01.01.1976" && SecondDate > FirstDate)
                {
                    var temp = App.ProjectRepository.GetAllProject()?.FindAll(i => i.ProjectStartDate >= FirstDate && i.ProjectStartDate <= SecondDate);
                    if (temp.Count > 0)
                        Projects = temp;
                    ProjectInfo = "";
                }
            }
            catch (Exception ex)
            {

                ProjectInfo = ex.Message;
                return;
            }
          
        }
        /// <summary>
        /// Сортировка списка проектов.
        /// </summary>
        private void Sort()
        {
            try
            {
                switch (SelectedSortMethod)
                {
                    case "Priority ↓":
                        Projects = (from item in App.ProjectRepository.GetAllProject()
                                    orderby item.Priority
                                    select item).ToList();

                        break;
                    case "Employees count ↓":
                        Projects = (from item in App.ProjectRepository.GetAllProject()
                                    orderby item.Employees.Count()
                                    select item).ToList();
                        break;
                    case "Priority ↑":
                        Projects = (from item in App.ProjectRepository.GetAllProject()
                                    orderby item.Priority descending
                                    select item).ToList();

                        break;
                    case "Employees count ↑":
                        Projects = (from item in App.ProjectRepository.GetAllProject()
                                    orderby item.Employees.Count() descending
                                    select item).ToList();
                        break;
                    default:
                        break;
                }
                ProjectInfo = "";
            }
            catch (Exception ex)
            {
                ProjectInfo = ex.Message;
                return;
            }
          
            
        }
        public void Edit()
        {
            if(SelectedProject != null) Navigation.PushAsync(new ProjectEditPage(SelectedProject.Id));
        }

        public void Add()
        {
            Navigation.PushAsync(new ProjectAddPage());
        }

        public void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
