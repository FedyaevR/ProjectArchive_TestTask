using ProjectArchive_TestTask.Model;
using ProjectArchive_TestTask.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProjectArchive_TestTask.ViewModel
{
  
    public class EmployeeListViewModel : INotifyPropertyChanged, IListViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public List<Employee> _employees;
        private Employee _selectedEmployee;
        private string _employeeInfo;
        public INavigation Navigation { get; set; }

        public Employee SelectedEmployee
        {
            get
            {
                return _selectedEmployee;
            }
            set
            {
                try
                {
                    if (_selectedEmployee != value)
                    {
                        _selectedEmployee = value;
                        var text = "First name: " + _selectedEmployee.FirstName + Environment.NewLine + "SecondName: " + _selectedEmployee.SecondName
                          + Environment.NewLine + "Patronymic: " + _selectedEmployee.Patronymic + Environment.NewLine + "Project(s): ";
                        var projects = App.ProjectRepository.GetProjectAtEmployee(_selectedEmployee);
                        foreach (var item in projects)
                        {
                            text += item.ProjectName + Environment.NewLine;
                        }

                        if (_selectedEmployee.HeadOfproject != null) text += "HeadOfProject: " + _selectedEmployee.HeadOfproject.ProjectName + Environment.NewLine;
                        else text += "HeadOfProject: " + Environment.NewLine;

                        EmployeeInfo = text;
                        OnPropertyChanged("SelectedEmployee");
                    }
                }
                catch (Exception ex)
                {

                    EmployeeInfo = ex.Message;
                }
                
            }
        }

        public  List<Employee> Employees
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
      

        public string EmployeeInfo
        {
            get
            {
                return _employeeInfo;
            }
            set
            {
                if (_employeeInfo != value)
                {
                    _employeeInfo = value;
                    OnPropertyChanged("EmployeeInfo");
                }
            }
        }
        public EmployeeListViewModel()
        {
            try
            {
                Employees = App.ProjectRepository.GetAllEmployees();
                EmployeeInfo = "";
            }
            catch (Exception ex)
            {

                EmployeeInfo = ex.Message;
                return;
            }
           
            SubscribeToRefreshData();
        }
     
      
        /// <summary>
        /// Удаление сотрудника из списка и БД.
        /// </summary>
        public void Delete()
        {
            try
            {
                if (SelectedEmployee != null)
                {
                    App.ProjectRepository.DeleteEmployee(SelectedEmployee);
                    Employees = App.ProjectRepository.GetAllEmployees();
                    EmployeeInfo = "";
                }
            }
            catch (Exception ex)
            {

                EmployeeInfo = ex.Message;
            }
           
        }

        public void Edit()
        {
            if (SelectedEmployee != null) Navigation.PushAsync(new EmployeeEditPage(SelectedEmployee.Id));
        }

        public void Add()
        {
            Navigation.PushAsync(new EmployeeAddPage());
        }

        public void SubscribeToRefreshData()
        {
            MessagingCenter.Subscribe<Page>(this, "Back", (sender) => { Employees = App.ProjectRepository.GetAllEmployees(); });
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
