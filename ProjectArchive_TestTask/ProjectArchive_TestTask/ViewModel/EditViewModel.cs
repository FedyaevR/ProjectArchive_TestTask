using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProjectArchive_TestTask.ViewModel
{
    public class EditViewModel
    {
        IEditViewModel _editViewModel;

        public INavigation Navigation { get; set; }
        public ICommand AddCommand { get; protected set; }
        public ICommand EditCommand { get; protected set; }
        public ICommand DeleteCommand { get; protected set; }


        public EditViewModel(IEditViewModel editViewModel)
        {

            _editViewModel = editViewModel;
            AddCommand = new Command(_editViewModel.Add);
            EditCommand = new Command(_editViewModel.Edit);
            DeleteCommand = new Command(_editViewModel.Delete);
        }

       
    }
}
