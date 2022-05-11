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
    public partial class ProjectAddPage : ContentPage
    {
        public ProjectAddPage()
        {
            InitializeComponent();
            BindingContext = new ProjectAddViewModel() { Navigation = this.Navigation };
        }
    }
}