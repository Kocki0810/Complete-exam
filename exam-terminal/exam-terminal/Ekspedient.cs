using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace exam_terminal
{
    public class Ekspedient : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        int _id;
        public int id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged();
            }
        }
        string _navn;
        public string navn
        {
            get
            {
                return _navn;
            }
            set
            {
                _navn = value;
                NotifyPropertyChanged();
            }
        }
        string _kortnummer;
        public string kortnummer
        {
            get
            {
                return _kortnummer;
            }
            set
            {
                _kortnummer = value;
                NotifyPropertyChanged();
            }
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

    }
}