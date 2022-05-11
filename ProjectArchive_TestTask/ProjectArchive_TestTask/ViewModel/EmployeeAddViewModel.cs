using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using ProjectArchive_TestTask.Model;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Linq;

namespace ProjectArchive_TestTask.ViewModel
{
    public class EmployeeAddViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _firstName;
        private string _secondName;
        private string _patronymic;
        private string _email;
        private Project _headOfProject;
        private Project _selectedProject;
        private List<Project> _projects;
        private string _error;
        public INavigation Navigation {get;set;}
        public ICommand AddEmployeeCommand { get; protected set; }

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
                if(_secondName != value)
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
                if(_selectedProject != value)
                {
                    _selectedProject = value;
                    //_selectedProjects.Add(value);
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

        #endregion

        public EmployeeAddViewModel()
        {
            AddEmployeeCommand = new Command(AddEmployee);
            try
            {
                Projects = App.ProjectRepository.GetAllProject();
            }
            catch (Exception ex)
            {

                Error = ex.Message;
                return;
            }
            
        }

        private void AddEmployee()
        {
            try
            {
                App.ProjectRepository.AddNewEmployee(_firstName, _secondName, _patronymic, _email, _headOfProject, _selectedProject);
                Error = "";
                OnPropertyChanged("Error");
                MessagingCenter.Send(Navigation.NavigationStack.LastOrDefault(), "Back");
                Navigation.PopAsync();
            }
            catch (Exception ex)
            {

                _error = ex.Message;
                OnPropertyChanged("Error");
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
