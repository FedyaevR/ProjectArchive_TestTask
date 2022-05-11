using ProjectArchive_TestTask.Data;
using ProjectArchive_TestTask.View;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectArchive_TestTask
{
    public partial class App : Application
    {
        string dbPath_Projects => FileAccesHelper.GetLocalFilePath("projects.db3");
        public static ProjectRepository ProjectRepository { get; set; }

        public App()
        {
            InitializeComponent();
            ProjectRepository = new ProjectRepository(dbPath_Projects);
            MainPage = new NavigationPage(new MainPage());
            
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
