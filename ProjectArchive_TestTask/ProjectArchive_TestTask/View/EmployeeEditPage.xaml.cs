using ProjectArchive_TestTask.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectArchive_TestTask.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmployeeEditPage : ContentPage
    {
       
        public EmployeeEditPage(int employeeId)
        {
            InitializeComponent();
            EmployeeEdit.Navigation = this.Navigation;
            EmployeeEdit.SetEmployee(employeeId);
            BindingContext = new EditViewModel(EmployeeEdit) { Navigation = this.Navigation };
        }


    }
}