using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Grid.Native;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace GridControlSample {
    public class ColumnsLayoutHelper {
        public static string GetAutoWidthColumnFieldName(DependencyObject obj) {
            return (string)obj.GetValue(AutoWidthColumnFieldNameProperty);
        }
        public static void SetAutoWidthColumnFieldName(DependencyObject obj, string value) {
            obj.SetValue(AutoWidthColumnFieldNameProperty, value);
        }
        public static readonly DependencyProperty AutoWidthColumnFieldNameProperty = DependencyProperty.RegisterAttached("AutoWidthColumnFieldName", typeof(string), typeof(ColumnsLayoutHelper), new PropertyMetadata(null, AutoWidthColumnFieldNamePropertyChangedCallback));
        static void AutoWidthColumnFieldNamePropertyChangedCallback(DependencyObject obj, DependencyPropertyChangedEventArgs e) {
            if(obj is TableView) {
                var view = obj as TableView;
                if(!string.IsNullOrEmpty((string)e.NewValue)) {
                    view.LayoutCalculatorFactory = new MyGridTableViewLayoutCalculatorFactory();
                } else {
                    view.LayoutCalculatorFactory = new GridTableViewLayoutCalculatorFactory();
                }
            }
        }
    }

    public class MyGridTableViewLayoutCalculatorFactory : GridTableViewLayoutCalculatorFactory {
        public override ColumnsLayoutCalculator CreateCalculator(GridViewInfo viewInfo, bool autoWidth) {
            return new MyColumnsLayoutCalculator(viewInfo);
        }
    }

    public class MyColumnsLayoutCalculator : ColumnsLayoutCalculator {
        public MyColumnsLayoutCalculator(GridViewInfo viewInfo) : base(viewInfo) { }
        protected override void CalcActualLayoutCore(double arrangeWidth, LayoutAssigner layoutAssigner, bool showIndicator, bool needRoundingLastColumn, bool ignoreDetailButtons) {
            base.CalcActualLayoutCore(arrangeWidth, layoutAssigner, showIndicator, needRoundingLastColumn, ignoreDetailButtons);

            var grid = this.ViewInfo.Grid as GridControl;
            var columnsOnHeadersPanel = GetColumnsOnHeadersPanel(grid);

            var autoWidthColumnFieldName = ColumnsLayoutHelper.GetAutoWidthColumnFieldName(grid.View);
            var autoWidthColumn = columnsOnHeadersPanel.FirstOrDefault((c) => c.FieldName == autoWidthColumnFieldName);
            if(autoWidthColumn == null) return;

            double visibleColumnsTotalWidth = 0.0;
            foreach(var column in columnsOnHeadersPanel) {
                visibleColumnsTotalWidth += layoutAssigner.GetWidth(column);
            }

            double delta = arrangeWidth - visibleColumnsTotalWidth;
            double newWidth = layoutAssigner.GetWidth(autoWidthColumn) + delta;
            autoWidthColumn.Width = newWidth;
        }
        IEnumerable<GridColumn> GetColumnsOnHeadersPanel(GridControl grid) {
            var res = grid.Columns.Where((c) => c.Visible);
            bool shouldExcludeGroupedColumns = (grid.View is TableView) && !(grid.View as TableView).ShowGroupedColumns;
            if(shouldExcludeGroupedColumns) {
                res = res.Where((c) => c.GroupIndex == -1);
            }
            return res;
        }
    }
}
