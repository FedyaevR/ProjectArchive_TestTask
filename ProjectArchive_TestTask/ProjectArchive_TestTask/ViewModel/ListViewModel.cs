using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProjectArchive_TestTask.ViewModel
{
    public class ListViewModel
    {

        IListViewModel _listView;

        public INavigation Navigation { get; set; }
        public ICommand AddCommand { get; protected set; }
        public ICommand EditCommand { get; protected set; }
        public ICommand DeleteCommand { get; protected set; }


        public ListViewModel(IListViewModel listView)
        {
            
            _listView = listView;
            AddCommand = new Command(_listView.Add);
            EditCommand = new Command(_listView.Edit);
            DeleteCommand = new Command(_listView.Delete);
        }
  
    }
}
