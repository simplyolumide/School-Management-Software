using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Schoolmanagement.Presentation
{
    public partial class frmexam : Window
    {
        public frmexam()
        {
            InitializeComponent();
        }

        private bool AddMode = false;
        private bool EditMode = false;
        private bool DeleteMode = false;
        private int Row = 0;

        SchoolData clsSchoolData = new SchoolData();
        
        private void frmexam_Loaded(object sender, RoutedEventArgs e)
        {
            cmbFields.Items.Add("Exam ID");
            cmbFields.Items.Add("Exam Type");

            cmbCondition.Items.Add("Contains");
            cmbCondition.Items.Add("Equals");
            cmbCondition.Items.Add("Starts with...");
            cmbCondition.Items.Add("More than...");
            cmbCondition.Items.Add("Less than...");
            cmbCondition.Items.Add("Equal or more than...");
            cmbCondition.Items.Add("Equal or less than...");

            InitToolBarItems();
            tiGrid.Height = 0;
            tiDetail.Height = 0;
            LoadGrid(true);
            tiGrid.IsSelected = true;
        }

        private void LoadGrid(bool SelectRow)
        {
            using (new WaitCursor())
            {
                try
                {
                    DataTable dt = examData.SelectAll();
                    Grid.ItemsSource = dt.DefaultView;
                    Grid.CanUserAddRows = false;
                    Grid.CanUserDeleteRows = false;
                    Grid.IsReadOnly = true;
                    if (Grid.Items.Count > 0 & SelectRow == true)
                    {
                        Grid.SelectedItem = Grid.Items[0];
                        Grid.ScrollIntoView(Grid.SelectedItem);
                    }
                }
                catch (System.Exception ex)
                {
                     MessageBox.Show(ex.Message, ex.GetType().ToString());
                }
                finally
                {
                }
            }
        }

        private void Grid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string headername = e.Column.Header.ToString();
            DataGridTextColumn txtCol = e.Column as DataGridTextColumn;
        }

        private void tbiAdd_Click(object sender, RoutedEventArgs e)
        {
            Add();
            tiDetail.IsSelected = true;

            InitToolBarItems();
            tbiAdd.IsEnabled = false;
            tbiEdit.IsEnabled = false;
            tbiDelete.IsEnabled = false;
            tbiApply.IsEnabled = true;
            tbiCancel.IsEnabled = true;
            tbiRefresh.IsEnabled = false;
        }

        private void tbiEdit_Click(object sender, RoutedEventArgs e)
        {
            Edit();
            tiDetail.IsSelected = true;

            InitToolBarItems();
            tbiAdd.IsEnabled = false;
            tbiEdit.IsEnabled = false;
            tbiDelete.IsEnabled = false;
            tbiApply.IsEnabled = true;
            tbiCancel.IsEnabled = true;
            tbiRefresh.IsEnabled = false;
        }

        private void tbiDelete_Click(object sender, RoutedEventArgs e)
        {
            Delete();
            tiDetail.IsSelected = true;

            InitToolBarItems();
            tbiAdd.IsEnabled = false;
            tbiEdit.IsEnabled = false;
            tbiDelete.IsEnabled = false;
            tbiApply.IsEnabled = true;
            tbiCancel.IsEnabled = true;
            tbiRefresh.IsEnabled = false;
        }

        private void tbiApply_Click(object sender, RoutedEventArgs e)
        {
            if (this.AddMode == true)
            {
                this.InsertRecord();
            }
            else if (this.EditMode == true)
            {
                this.UpdateRecord();
            }
            else if (this.DeleteMode == true)
            {
                this.DeleteRecord();
            }
        }

        private void tbiCancel_Click(object sender, RoutedEventArgs e)
        {
            tiGrid.IsSelected = true;
            InitToolBarItems();
        }

        private void tbiRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadGrid(true);
        }

        private void tbiClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Row = Grid.SelectedIndex;
        }
        
        private void InitToolBarItems()
        {
            tbiAdd.IsEnabled = true;
            tbiEdit.IsEnabled = true;
            tbiDelete.IsEnabled = true;
            tbiApply.IsEnabled = false;
            tbiCancel.IsEnabled = false;
            tbiRefresh.IsEnabled = true;
            tbiClose.IsEnabled = true;
        }

        private void GetData()
        {
            using (new WaitCursor())
            {
                ClearRecord();

                exam clsexam = new exam();
                clsexam.ExamId = System.Convert.ToInt32((Grid.SelectedCells[0].Column.GetCellContent(Grid.SelectedItem) as TextBlock).Text);
                clsexam = examData.Select_Record(clsexam);

                if ((clsexam != null))
                {
                    try
                    {
                        nudExamId.Text = System.Convert.ToInt32(clsexam.ExamId).ToString();
                        if (clsexam.ExamType is null) { tbExamType.Text = default(string); } else { tbExamType.Text = Convert.ToString(clsexam.ExamType); }
                    }
                    catch
                    {
                    }
                }
            }
        }

        private void SetData(exam clsexam)
        {
            using (new WaitCursor())
            {
                if (string.IsNullOrEmpty(tbExamType.Text)) {
                    clsexam.ExamType = null;
                } else {
                    clsexam.ExamType = tbExamType.Text; }
            }
        }

        private void Add()
        {
            this.AddMode = true;
            this.EditMode = false;
            this.DeleteMode = false;

            ClearRecord();
            IsReadOnlyRecord(true);
            tbExamType.Focus();
            nudExamId.Text = System.Convert.ToString(clsSchoolData.getAutoID("New", "exam"));
        }

        private void Edit()
        {
            AddMode = false;
            EditMode = true;
            DeleteMode = false;

            GetData();

            IsReadOnlyRecord(true);
            tbExamType.Focus();
        }

        private void Delete()
        {
            AddMode = false;
            EditMode = false;
            DeleteMode = true;

            GetData();

            IsReadOnlyRecord(false);
        }

        private void IsReadOnlyRecord(bool YesNo)
        {
            tbExamType.IsEnabled = YesNo;
        }

        private void ClearRecord()
        {
            nudExamId.Text = "0";
            tbExamType.Text = null;
        }

        private void GoBack_To_Grid()
        {
            int LastRow = Row;
            bool gridOK = false;
            try
            {
                LoadGrid(false);
                Grid.SelectedItems.Clear();
                Grid.SelectedItem = Grid.Items[LastRow];
                Grid.ScrollIntoView(Grid.Items[LastRow]);
            	tiGrid.IsSelected = true;
            	InitToolBarItems();
            	gridOK = true;
            }
            catch
            {
                //Hide error message.
            }
            finally
            {
                if (gridOK == false)
                {
                    LoadGrid(true);
                    tiGrid.IsSelected = true;
                    InitToolBarItems();
                }
            }
        }

        private void InsertRecord()
        {
            using (new WaitCursor())
            {
            exam clsexam = new exam();
                if (VerifyData() == true) {
                    SetData(clsexam);
                    Boolean bSucess = new Boolean();
                    bSucess = examData.Add(clsexam);
                    if (bSucess == true) {
                        GoBack_To_Grid(); }
                    else {
                        MessageBox.Show("Insert failed.", "Error"); }
                }
            }
        }

        private void UpdateRecord()
        {
            using (new WaitCursor())
            {
                exam oclsexam = new exam();
                exam clsexam = new exam();

                oclsexam.ExamId = System.Convert.ToInt32((Grid.SelectedCells[0].Column.GetCellContent(Grid.SelectedItem) as TextBlock).Text);
                oclsexam = examData.Select_Record(oclsexam);

                if (VerifyData() == true) {
                    SetData(clsexam);
                    Boolean bSucess = new Boolean();
                    bSucess = examData.Update(oclsexam, clsexam);
                    if (bSucess == true) {
                        GoBack_To_Grid(); }
                    else {
                        MessageBox.Show("Update failed.", "Error");}
                }
            }
        } 

        private void DeleteRecord()
        {
            using (new WaitCursor())
            {
                exam clsexam = new exam();
                clsexam.ExamId = System.Convert.ToInt32((Grid.SelectedCells[0].Column.GetCellContent(Grid.SelectedItem) as TextBlock).Text);
                if (MessageBox.Show("Are you sure? Delete this record?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes) {
                    SetData(clsexam);
                    Boolean bSucess = new Boolean();
                    bSucess = examData.Delete(clsexam);
                    if (bSucess == true) {
                        GoBack_To_Grid(); }
                    else {
                        MessageBox.Show("Delete failed.", "Error"); }
                }
            }
        }  

        private Boolean VerifyData()
        {
            return true;
        }

        private void nudExamId_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!char.IsDigit(Convert.ToChar(e.Text)))
                e.Handled = true;
        }


        private void cbExport_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (new WaitCursor())
            {
                try
                {
                    SchoolData clsSchoolData = new SchoolData();
                    if (cbiPDF.IsSelected == true)
                    {
                        if (tiGrid.IsSelected)
                        {
                            clsSchoolData.ExportToPdf("Many", 
                                                     "Dbo.Exam", Grid, (DataRowView)Grid.CurrentItem, Row);
                        }
                        else if (tiDetail.IsSelected)
                        {
                            clsSchoolData.ExportToPdf("One", 
                                                     "Dbo.Exam", Grid, (DataRowView)Grid.CurrentItem, Row);
                        }
                    }
                    else if (cbiExcel.IsSelected == true)
                    {
                        if (tiGrid.IsSelected)
                        {
                            clsSchoolData.ExportToExcel("Many", 
                                                     "Dbo.Exam", Grid, (DataRowView)Grid.CurrentItem, Row);
                        }
                        else if (tiDetail.IsSelected)
                        {
                            clsSchoolData.ExportToExcel("One", 
                                                     "Dbo.Exam", Grid, (DataRowView)Grid.CurrentItem, Row);
                        }
                    }
                    else if (cbiWord.IsSelected == true)
                    {
                        if (tiGrid.IsSelected)
                        {
                            clsSchoolData.ExportToWord("Many", 
                                                     "Dbo.Exam", Grid, (DataRowView)Grid.CurrentItem, Row);
                        }
                        else if (tiDetail.IsSelected)
                        {
                            clsSchoolData.ExportToWord("One", 
                                                     "Dbo.Exam", Grid, (DataRowView)Grid.CurrentItem, Row);
                        }
                    }
                    cbiExport.IsSelected = true;
                }
                catch
                {
                }
            }
        }

        private void butShowAll_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Text = null;
            LoadGrid(true);
            tiGrid.IsSelected = true;
        }

        private void butSearch_Click(object sender, RoutedEventArgs e)
        {
            using (new WaitCursor())
            {
                try
                {
                    DataTable dt = examData.Search(cmbFields.Text, cmbCondition.Text, txtSearch.Text);
                    Grid.ItemsSource = dt.DefaultView;
                    Grid.CanUserAddRows = false;
                    Grid.CanUserDeleteRows = false;
                    Grid.IsReadOnly = true;
                    if (Grid.Items.Count > 0)
                    {
                        Grid.SelectedItem = Grid.Items[0];
                        Grid.ScrollIntoView(Grid.SelectedItem);
                    }
                }
                catch
                {
                    MessageBox.Show("An error occurred in butSearch_Click...", "Error");
                }
                finally
                {
                }
            }
        }

    }
}





 
