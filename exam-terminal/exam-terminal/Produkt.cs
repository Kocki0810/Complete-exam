using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace exam_terminal
{
    public class Produkt : INotifyPropertyChanged, ICloneable
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        string _Navn;
        public string Navn
        {
            get
            {
                return _Navn;
            }
            set
            {
                _Navn = value;
                NotifyPropertyChanged();
            }
        }
        int _antal;
        public int antal
        {
            get
            {
                return _antal;
            }
            set
            {
                _antal = value;
                NotifyPropertyChanged();
            }
        }
        decimal _pris;
        public decimal pris
        {
            get
            {
                return _pris;
            }
            set
            {
                _pris = value;
                NotifyPropertyChanged();
            }
        }
        
        public string firma_id
        {
            get { return DataStore.Firmaid; }
        }
        public int produktgruppe_id { get; set; }
        public int id { get; set; }

        public string Btn_Text
        {
            get { return Navn + "\r\n" + pris; }
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
        
    }
}