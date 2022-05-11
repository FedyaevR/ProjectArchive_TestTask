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
    public class ProjectAddViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _projectName;
        private string _customerCompanyName;
        private string _performerCompanyName;
        private List<Employee> _employees;
        private DateTime _startDate;
        private DateTime _doneDate;
        private int[] _priority;
        private string  _error;
        private Employee _selectedEmployee;
        private int _selectedPriority;
        public INavigation Navigation { get; set; }
        public ICommand AddProjectCommand { get; protected set; }
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
                    OnPropertyChanged("Selectedpriority");
                }
            }
        }

        #endregion
        public ProjectAddViewModel()
        {
            AddProjectCommand = new Command(AddProject);
            Employees = App.ProjectRepository.GetAllEmployees();
            Priority = new int[]{1,2,3,4 };
        }

        private void AddProject()
        {
            try
            {
                App.ProjectRepository.AddNewProject(_projectName, _customerCompanyName, _performerCompanyName, _selectedEmployee, _startDate, _doneDate, _selectedPriority);
                Error = "";
                OnPropertyChanged("Error");
                MessagingCenter.Send(Navigation.NavigationStack.LastOrDefault(),"Back");
                Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                OnPropertyChanged("Error");
            }
           
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
