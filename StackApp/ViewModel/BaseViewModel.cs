using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackApp.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        private string selectedName;

        public string SelectedName
        {
            get { return selectedName; }
            set { selectedName = value; }
        }

        private string selectedLanguage;

        public string SelectedLanguage
        {
            get { return selectedLanguage; }
            set { selectedLanguage = value; }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
