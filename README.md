# How to set the content based column width when merge cell applied in WPF DataGrid (SfDataGrid)?

## About the sample
This example illustrates how to set the content based column width when merge cell applied in [WPF DataGrid](https://www.syncfusion.com/wpf-controls/datagrid) (SfDataGrid)?

[WPF DataGrid](https://www.syncfusion.com/wpf-controls/datagrid) (SfDataGrid) does not provide the direct support to set the content based column width when a cell is merged. You can set the content based column width when a cell is merged by customizing the column sizing operations by overriding [GridColumnSizer](https://help.syncfusion.com/cr/wpf/Syncfusion.UI.Xaml.Grid.GridColumnSizer.html) and set it to [SfDataGrid.GridColumnSizer](https://help.syncfusion.com/cr/wpf/Syncfusion.UI.Xaml.Grid.SfDataGrid.html#Syncfusion_UI_Xaml_Grid_SfDataGrid_GridColumnSizer).

```C#

this.dataGrid.GridColumnSizer = new GridColumnSizerExt();

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

```

Refresh the autosize calculation after the merge cell operation performed in [WPF DataGrid](https://www.syncfusion.com/wpf-controls/datagrid) (SfDataGrid).

```C#

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

```

![Content based column width for merged cell in SfDataGrid](MergeCellColumnWidth.gif)

Take a moment to peruse the [WPF DataGrid – AutoSize Columns](https://help.syncfusion.com/wpf/datagrid/autosize-columns), where you can find about autosize columns with code examples.

KB article - [How to set the content based column width when merge cell applied in WPF DataGrid (SfDataGrid)?](https://www.syncfusion.com/kb/12540/how-to-set-the-content-based-column-width-when-merge-cell-applied-in-wpf-datagrid)

## Requirements to run the demo
Visual Studio 2015 and above versions
