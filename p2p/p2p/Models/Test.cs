using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace p2p.Models
{
    public class Test : INotifyPropertyChanged
    {
        private int _clock;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public int Clock
        {
            get { return _clock; }
            set
            {
                _clock = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Clock"));
            }
        }
    }
}
