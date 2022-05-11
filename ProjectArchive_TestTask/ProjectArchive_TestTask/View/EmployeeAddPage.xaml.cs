using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectArchive_TestTask.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectArchive_TestTask.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmployeeAddPage : ContentPage
    {
        public EmployeeAddPage()
        {
            InitializeComponent();
            BindingContext = new EmployeeAddViewModel() { Navigation = this.Navigation };
        }
    }
}