using DevExpress.Mvvm.DataAnnotations;
using System;
using System.Collections.ObjectModel;

namespace GridControlSample {
    [POCOViewModel]
    public class ViewModel {
        public virtual ObservableCollection<object> Items { get; set; }
        public ViewModel() {
            Items = new ObservableCollection<object>();
            for (int i = 0; i < 30; i++) {
                SampleItem item = new SampleItem() { Id = i, Name = "Item " + (i % 3).ToString(), Time = DateTime.Now.AddDays(i) };
                Items.Add(item);
            }
        }
    }
    public class SampleItem {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Time { get; set; }
    }
}