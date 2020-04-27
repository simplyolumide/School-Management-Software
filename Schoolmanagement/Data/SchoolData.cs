using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Input;
using Microsoft.Office.Interop;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using System.Windows.Controls;

public class SchoolData
{
    public static string connectionString
            = "Data Source=TOPEWEALTH-PC\\SQLEXPRESS;Initial Catalog=School;Integrated Security=SSPI;";

    public static SqlConnection GetConnection()
    {
        SqlConnection connection = new SqlConnection(connectionString);
        return connection;
    }
    
    public int getIdent_Current(string Table)
    {
        string query = null;
        SqlConnection connection = new SqlConnection();
        SqlCommand command = new SqlCommand();
        SqlDataReader reader = default(SqlDataReader);
        int returnValue = 0;

        query = "SELECT IDENT_CURRENT('" + Table + "')";
        connection = GetConnection();
        command = new SqlCommand(query, connection);
        command.CommandType = CommandType.Text;
        try
        {
            connection.Open();
            reader = command.ExecuteReader();
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    returnValue = Convert.ToInt32(reader.GetValue(0));
                }
            }
            reader.Close();
            connection.Close();
        }
        catch //(SqlException ex)
        {
            //MessageBox.Show(ex.Message);
        }
        finally
        {
            connection.Close();
        }
        return returnValue;
    }

    public int getIdent_Incr(string Table)
    {
        string query = null;
        SqlConnection connection = new SqlConnection();
        SqlCommand command = new SqlCommand();
        SqlDataReader reader = default(SqlDataReader);
        int returnValue = 0;

        query = "SELECT IDENT_INCR('" + Table + "')";
        connection = GetConnection();
        command = new SqlCommand(query, connection);
        command.CommandType = CommandType.Text;
        try
        {
            connection.Open();
            reader = command.ExecuteReader();
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    returnValue = Convert.ToInt32(reader.GetValue(0));
                }
            }
            reader.Close();
            connection.Close();
        }
        catch //(SqlException ex)
        {
            //MessageBox.Show(ex.Message);
        }
        finally
        {
            connection.Close();
        }
        return returnValue;
    }

    public int getAutoID(string Mode, string Table)
    {
        int Ident_Current = getIdent_Current(Table);
        if (Mode == "Last")
        {
            return Ident_Current;
        }
        else if (Mode == "New")
        {
            return Ident_Current = Ident_Current + getIdent_Incr(Table);
        }
        return Ident_Current;
    }

    public void ExportToExcel(string Type, string Head, DataGrid Grid, DataRowView GridRow, int RowNum)
    {
        try
        {
            DataTable dt;
            if (Type == "One")
            {
                dt = DataGridRow_To_DataTable(Grid, GridRow, Head, RowNum);
            }
            else
            {
                dt = DataGrid_To_DataTable(Grid, Head);
            }

            if ((dt.Columns.Count > 0))
            {
                Microsoft.Office.Interop.Excel.Application oExcel;
                Microsoft.Office.Interop.Excel.Workbook oBook;
                Microsoft.Office.Interop.Excel.Worksheet oSheet;

                oExcel = new Microsoft.Office.Interop.Excel.Application();
                oExcel.Visible = false;

                oBook = (Microsoft.Office.Interop.Excel.Workbook)(oExcel.Workbooks.Add(System.Type.Missing));
                oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oBook.ActiveSheet;

                oSheet.Range["B2"].Value = Head;
                oSheet.Range["B2"].Font.Bold = true;


                if (Type == "One")
                {
                    for (int s = 0; s <= dt.Rows.Count - 1; s++)
                    {
                        oSheet.Range["B" + (4 + s)].Value = Convert.ToString(dt.Rows[s][0]);
                        oSheet.Range["C" + (4 + s)].Value = Convert.ToString(dt.Rows[s][1]);
                    }

                    oSheet.Columns.AutoFit();
                }
                else if (Type == "Many")
                {
                    object[,] DataArrayHead = new object[1, Grid.Columns.Count];
                    for (int s = 0; s <= Grid.Columns.Count - 1; s++)
                    {
                        DataArrayHead[0, s] = Grid.Columns[s].Header;
                    }

                    oSheet.Range["B4"].Resize[1, Grid.Columns.Count].Value = DataArrayHead;
                    oSheet.Range["B4"].Resize[1, Grid.Columns.Count].Font.Bold = true;

                    object[,] DataArray = new object[Grid.Items.Count, Grid.Columns.Count];
                    for (int r = 0; r <= dt.Rows.Count - 1; r++)
                    {
                        for (int s = 0; s <= dt.Columns.Count - 1; s++)
                        {
                            DataArray[r, s] = dt.Rows[r][s];
                        }
                    }
                    oSheet.Range["B5"].Resize[Grid.Items.Count, Grid.Columns.Count].Value = DataArray;
                    oSheet.Columns.AutoFit();
                }
                oExcel.Visible = true;
                oExcel = null;
                oSheet = null;
            }
        }
        catch
        {
        }
    }

    public void ExportToWord(string Type, string Head, DataGrid Grid, DataRowView GridRow, int RowNum)
    {
        try
        {
            DataTable dt = default(DataTable);
            if (Type == "One")
            {
                dt = DataGridRow_To_DataTable(Grid, GridRow, Head, RowNum);
            }
            else
            {
                dt = DataGrid_To_DataTable(Grid, Head);
            }

            if ((dt.Columns.Count > 0))
            {
                Microsoft.Office.Interop.Word.Application oWord;
                Microsoft.Office.Interop.Word.Document oDoc;
                Microsoft.Office.Interop.Word.Table oTable;
                Microsoft.Office.Interop.Word.Paragraph oPara1;

                oWord = new Microsoft.Office.Interop.Word.Application();
                oWord.Visible = false;
                oDoc = (Microsoft.Office.Interop.Word.Document)(oWord.Documents.Add(System.Type.Missing));
                var _with1 = oDoc.PageSetup;
                _with1.PaperSize = Microsoft.Office.Interop.Word.WdPaperSize.wdPaperA4;
                _with1.Orientation = Microsoft.Office.Interop.Word.WdOrientation.wdOrientLandscape;
                _with1.LeftMargin = oWord.CentimetersToPoints(1);
                _with1.BottomMargin = oWord.CentimetersToPoints(1);
                _with1.TopMargin = oWord.CentimetersToPoints(1);

                oPara1 = (Microsoft.Office.Interop.Word.Paragraph)(oDoc.Content.Paragraphs.Add(System.Type.Missing));
                oPara1.Range.Text = Head;
                oPara1.Range.Font.Bold = 0;
                oPara1.Range.Font.Size = 8;
                oPara1.Format.SpaceAfter = 8;
                oPara1.Range.InsertParagraphAfter();

                if (Type == "One")
                {
                    oTable = oDoc.Tables.Add(oDoc.Bookmarks.get_Item("\\endofdoc").Range, dt.Rows.Count, 2);
                    oTable.Range.ParagraphFormat.SpaceAfter = 2;

                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        oTable.Cell(i + 1, 1).Range.InsertAfter(Convert.ToString(dt.Rows[i][0]));
                        oTable.Cell(i + 1, 2).Range.Text = Convert.ToString(dt.Rows[i][1]);
                    }

                    oTable.Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                    oTable.Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleDouble;

                    oTable.AllowAutoFit = true;
                    oTable.Columns.AutoFit();
                }
                else if (Type == "Many")
                {
                    oTable = oDoc.Tables.Add(oDoc.Bookmarks.get_Item("\\endofdoc").Range, dt.Rows.Count + 1, dt.Columns.Count);
                    oTable.Range.ParagraphFormat.SpaceAfter = 2;

                    for (int i = 0; i <= dt.Columns.Count - 1; i++)
                    {
                        oTable.Cell(1, i + 1).Range.InsertAfter(Convert.ToString(dt.Columns[i].Caption));
                    }

                    for (int r = 0; r <= dt.Rows.Count - 1; r++)
                    {
                        for (int s = 0; s <= dt.Columns.Count - 1; s++)
                        {
                            oTable.Cell(r + 2, s + 1).Range.Text = Convert.ToString(dt.Rows[r][s]);
                        }
                    }

                    oTable.Rows[1].Cells.Shading.BackgroundPatternColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdGray25;

                    oTable.Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                    oTable.Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleDouble;

                    oTable.AllowAutoFit = true;
                    oTable.Columns.AutoFit();
                }
                oWord.Visible = true;
                oWord.NormalTemplate.Saved = true;
                oWord = null;
                oDoc = null;
            }
        }
        catch
        {
        }
    }

    public void ExportToPdf(string Type, string Head, DataGrid Grid, DataRowView GridRow, int RowNum)
    {
        try
        {
            DataTable dt = default(DataTable);
            if (Type == "One")
            {
                dt = DataGridRow_To_DataTable(Grid, GridRow, Head, RowNum);
            }
            else
            {
                dt = DataGrid_To_DataTable(Grid, Head);
            }
            PDFform pdfForm = new PDFform(dt, Head, Type);

            Document document = pdfForm.CreateDocument();
            document.UseCmykColor = true;
            PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(true);
            pdfRenderer.Document = document;
            pdfRenderer.RenderDocument();

            string FilePath = System.IO.Path.GetTempPath() + "\\" + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + ".pdf";
            pdfRenderer.Save(FilePath);
            System.Diagnostics.Process.Start(FilePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.ReadLine();
        }
    }

    //Converts the DataGrid to DataTable
    public DataTable DataGrid_To_DataTable(System.Windows.Controls.DataGrid dg, String tblName, int minRow = 0)
    {
        DataTable dt = new DataTable(tblName);

        foreach (DataGridColumn column in dg.Columns)
        {
            DataColumn dc = new DataColumn(column.Header.ToString());
            dt.Columns.Add(dc);
        }

        for (int i = 0; i <= dg.Items.Count - 1; i++)
        {
            DataRowView row = (DataRowView)dg.Items.GetItemAt(i);
            //'dg.Items(i)
            DataRow dr = dt.NewRow();
            for (int j = 0; j <= dg.Columns.Count - 1; j++)
            {
                dr[j] = (row[j] == null) ? "" : row[j];
            }
            dt.Rows.Add(dr);
        }

        // Related to the bug around min size when using ExcelLibrary for export
        for (int i = dg.Items.Count; i <= minRow - 1; i++)
        {
            DataRow dr = dt.NewRow();
            for (int j = 0; j <= dt.Columns.Count - 1; j++)
            {
                dr[j] = "  ";
            }
            dt.Rows.Add(dr);
        }
        return dt;
    }

    //Converts the DataGridRow to DataTable
    public DataTable DataGridRow_To_DataTable(System.Windows.Controls.DataGrid dg, DataRowView dgr, String tblName, int RowNum, int minRow = 0)
    {
        try
        {
            DataTable dt = new DataTable(tblName);

            // Header columns
            DataColumn dc1 = new DataColumn("Column");
            dt.Columns.Add(dc1);
            DataColumn dc2 = new DataColumn("Value");
            dt.Columns.Add(dc2);

            DataRowView row = (DataRowView)dg.Items.GetItemAt(RowNum);
            for (int j = 0; j <= dg.Columns.Count - 1; j++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = dg.Columns[j].Header.ToString();
                dr[1] = (row[j] == null) ? "" : row[j].ToString();
                dt.Rows.Add(dr);
            }
            return dt;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.ReadLine();
        }
        return null;
    }

}

public class WaitCursor : IDisposable
{
    private Cursor _previousCursor;

    public WaitCursor() 
    {
        _previousCursor = Mouse.OverrideCursor;

        Mouse.OverrideCursor = Cursors.Wait;
    }

    public void Dispose()
    {
        Mouse.OverrideCursor = _previousCursor;
    }
}





 
