using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Syncfusion.UI.Xaml.Grid;

namespace SfDataGridDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<MainVm> MainVms { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            MainVms = new List<MainVm> { new MainVm(), new MainVm(), new MainVm() { Value1 = "Value1" } };
            DataContext = this;
            this.dataGrid.GridColumnSizer = new GridColumnSizerExt();
        }

        private void SfDataGrid_OnQueryCoveredRange(object sender, GridQueryCoveredRangeEventArgs e)
        {
            //merge the columns in a row by setting the column range using Left and Right properties of CoveredCellInfo.
            if (e.RowColumnIndex.RowIndex == 3)
            {
                e.Range = new CoveredCellInfo(0, 2, 3, 3);
                e.Handled = true;
            }

            //Refresh the autosize calculation after the merge cell operation performed in SfDataGrid
            this.dataGrid.GridColumnSizer.ResetAutoCalculationforAllColumns();
            this.dataGrid.GridColumnSizer.Refresh();
        }
    }

    public class GridColumnSizerExt : GridColumnSizer
    {

        public GridColumnSizerExt()
        {

        }

        protected override Size GetCellSize(Size rect, GridColumn column, object data, GridQueryBounds bounds)
        {
            //return empty size for merged cell
            var rowIndex = this.DataGrid.ResolveToRowIndex(data);
            var columnIndex = DataGrid.Columns.IndexOf(column);
            var covererdCellInfo = this.DataGrid.CoveredCells.GetCoveredCellInfo(rowIndex, columnIndex);
            if (covererdCellInfo != null)
                return Size.Empty;
            return base.GetCellSize(rect, column, data, bounds);
        }
    }

    public class MainVm
    {
        public string Value1 { get; set; } = "1";
        public string Value2 { get; set; } = "2";
        public string Value3 { get; set; } = "3";
    }
}
