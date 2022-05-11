using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ProjectArchive_TestTask.ViewModel
{
    public interface IListViewModel
    {
        INavigation Navigation{get;set;}
        void Delete();
        void Edit();
        void Add();
    }
}
