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
    public partial class ProjectsPage : ContentPage
    {
        public ProjectsPage()
        {
            InitializeComponent();
            ProjectsList.Navigation = this.Navigation;
            BindingContext = new ListViewModel(ProjectsList) {Navigation = this.Navigation }; 
        }
    }
}