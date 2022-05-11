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
    public partial class EmployeePage : ContentPage
    {
        public EmployeePage()
        {
            InitializeComponent();
            EmployeesList.Navigation = this.Navigation;
            BindingContext = new ListViewModel(EmployeesList) { Navigation = this.Navigation };
            
        }
    }
}