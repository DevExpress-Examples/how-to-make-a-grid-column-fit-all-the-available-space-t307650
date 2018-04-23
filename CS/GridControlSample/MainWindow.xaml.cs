using DevExpress.Data.Filtering;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Printing;
using DevExpress.XtraPrinting;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace GridControlSample {
    public partial class MainWindow : Window {
        SampleSource source = new SampleSource();
        public MainWindow() {
            InitializeComponent();
            DataContext = this.source;
        }
    }
}
