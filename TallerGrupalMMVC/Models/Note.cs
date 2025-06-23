using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TallerGrupalMMVC.Models
{
    public class Note : INotifyPropertyChanged
    {
        private string _text = string.Empty;
        private DateTime _date = DateTime.Now;

        public string Filename { get; set; } = string.Empty;

        public string Text
        {
            get => _text;
            set
            {
                if (_text != value)
                {
                    _text = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime Date
        {
            get => _date;
            set
            {
                if (_date != value)
                {
                    _date = value;
                    OnPropertyChanged();
                }
            }
        }

        public string DateDisplay => Date.ToString("MMM dd, yyyy HH:mm");

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
