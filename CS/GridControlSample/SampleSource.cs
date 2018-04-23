using DevExpress.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace GridControlSample {
    public class SampleSource : INotifyPropertyChanged {
        ObservableCollection<object> items;
        public ObservableCollection<object> Items {
            get { return items; }
            set {
                if(items == value) return;
                items = value;
                RaisePropertyChanged("Items");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public SampleSource() {
            InitItems();
        }
        void InitItems() {
            Items = new ObservableCollection<object>();
            for(int i = 0; i < 30; i++) {
                SampleItem item = new SampleItem() { Id = i, Name = "item " + (i % 3).ToString(), Time = DateTime.Now.AddDays(i) };
                Items.Add(item);
            }
        }
        void RaisePropertyChanged(string propertyName) {
            if(PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class SampleItem : INotifyPropertyChanged {
        int id;
        string name;
        DateTime time;
        public int Id {
            get { return id; }
            set {
                if(id == value) return;
                id = value;
                RaisePropertyChanged("Id");
            }
        }
        public string Name {
            get { return name; }
            set {
                if(name == value) return;
                name = value;
                RaisePropertyChanged("Name");
            }
        }
        public DateTime Time {
            get { return time; }
            set {
                if(time == value) return;
                time = value;
                RaisePropertyChanged("Time");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        void RaisePropertyChanged(string fieldName) {
            if(PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(fieldName));
        }
    }
}
