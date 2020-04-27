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
    public partial class frmteacher : Window
    {
        public frmteacher()
        {
            InitializeComponent();
        }

        private bool AddMode = false;
        private bool EditMode = false;
        private bool DeleteMode = false;
        private int Row = 0;

        SchoolData clsSchoolData = new SchoolData();
        
        private void frmteacher_Loaded(object sender, RoutedEventArgs e)
        {
            cmbFields.Items.Add("Teacher ID");
            cmbFields.Items.Add("Class ID");
            cmbFields.Items.Add("Full Name");
            cmbFields.Items.Add("Date Of Join");
            cmbFields.Items.Add("Home Address");
            cmbFields.Items.Add("Phone Number");

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
            Loadteacher_studentclassComboBox();
            LoadGrid(true);
            tiGrid.IsSelected = true;
        }

        private void Loadteacher_studentclassComboBox()
        {
            using (new WaitCursor())
            {
                List<teacher_studentclass> teacher_studentclassList = new  List<teacher_studentclass>();
                try
                {
                    teacher_studentclassList = teacher_studentclassData.List();
                    cbClassId.ItemsSource = teacher_studentclassList;
                    cbClassId.DisplayMemberPath = "ClassId";
                    cbClassId.SelectedValuePath = "ClassId";
                }
                catch (System.Exception ex)
                {
                     MessageBox.Show(ex.Message, ex.GetType().ToString());
                }
            }
        }

        private void LoadGrid(bool SelectRow)
        {
            using (new WaitCursor())
            {
                try
                {
                    DataTable dt = teacherData.SelectAll();
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
            if (headername == "Date Of Join")
            {
                txtCol.Binding.StringFormat = "{0:d}";
            }
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

                teacher clsteacher = new teacher();
                clsteacher.TeacherId = System.Convert.ToInt32((Grid.SelectedCells[0].Column.GetCellContent(Grid.SelectedItem) as TextBlock).Text);
                clsteacher = teacherData.Select_Record(clsteacher);

                if ((clsteacher != null))
                {
                    try
                    {
                        nudTeacherId.Text = System.Convert.ToInt32(clsteacher.TeacherId).ToString();
                        if (clsteacher.ClassId is null) { cbClassId.SelectedValue = default(int); } else { cbClassId.SelectedValue = System.Convert.ToInt32(clsteacher.ClassId); }
                        if (clsteacher.FullName is null) { tbFullName.Text = default(string); } else { tbFullName.Text = Convert.ToString(clsteacher.FullName); }
                        if (clsteacher.DateOfJoin is null) { dtpDateOfJoin.SelectedDate = DateTime.Now; } else { dtpDateOfJoin.SelectedDate = System.Convert.ToDateTime(clsteacher.DateOfJoin); }
                        if (clsteacher.HomeAddress is null) { tbHomeAddress.Text = default(string); } else { tbHomeAddress.Text = Convert.ToString(clsteacher.HomeAddress); }
                        if (clsteacher.PhoneNumber is null) { tbPhoneNumber.Text = default(string); } else { tbPhoneNumber.Text = Convert.ToString(clsteacher.PhoneNumber); }
                    }
                    catch
                    {
                    }
                }
            }
        }

        private void SetData(teacher clsteacher)
        {
            using (new WaitCursor())
            {
                if (string.IsNullOrEmpty(cbClassId.SelectedValue.ToString())) {
                    clsteacher.ClassId = null;
                } else {
                    clsteacher.ClassId = System.Convert.ToInt32(cbClassId.SelectedValue); }
                if (string.IsNullOrEmpty(tbFullName.Text)) {
                    clsteacher.FullName = null;
                } else {
                    clsteacher.FullName = tbFullName.Text; }
                if (string.IsNullOrEmpty(dtpDateOfJoin.Text)) {
                    clsteacher.DateOfJoin = null;
                } else {
                    clsteacher.DateOfJoin = System.Convert.ToDateTime(dtpDateOfJoin.Text); }
                if (string.IsNullOrEmpty(tbHomeAddress.Text)) {
                    clsteacher.HomeAddress = null;
                } else {
                    clsteacher.HomeAddress = tbHomeAddress.Text; }
                if (string.IsNullOrEmpty(tbPhoneNumber.Text)) {
                    clsteacher.PhoneNumber = null;
                } else {
                    clsteacher.PhoneNumber = tbPhoneNumber.Text; }
            }
        }

        private void Add()
        {
            this.AddMode = true;
            this.EditMode = false;
            this.DeleteMode = false;

            ClearRecord();
            IsReadOnlyRecord(true);
            cbClassId.Focus();
            nudTeacherId.Text = System.Convert.ToString(clsSchoolData.getAutoID("New", "teacher"));
        }

        private void Edit()
        {
            AddMode = false;
            EditMode = true;
            DeleteMode = false;

            GetData();

            IsReadOnlyRecord(true);
            cbClassId.Focus();
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
            cbClassId.IsEnabled = YesNo;
            tbFullName.IsEnabled = YesNo;
            dtpDateOfJoin.IsEnabled = YesNo;
            tbHomeAddress.IsEnabled = YesNo;
            tbPhoneNumber.IsEnabled = YesNo;
        }

        private void ClearRecord()
        {
            nudTeacherId.Text = "0";
            cbClassId.SelectedIndex = -1;
            tbFullName.Text = null;
            dtpDateOfJoin.Text = null;
            tbHomeAddress.Text = null;
            tbPhoneNumber.Text = null;
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
            teacher clsteacher = new teacher();
                if (VerifyData() == true) {
                    SetData(clsteacher);
                    Boolean bSucess = new Boolean();
                    bSucess = teacherData.Add(clsteacher);
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
                teacher oclsteacher = new teacher();
                teacher clsteacher = new teacher();

                oclsteacher.TeacherId = System.Convert.ToInt32((Grid.SelectedCells[0].Column.GetCellContent(Grid.SelectedItem) as TextBlock).Text);
                oclsteacher = teacherData.Select_Record(oclsteacher);

                if (VerifyData() == true) {
                    SetData(clsteacher);
                    Boolean bSucess = new Boolean();
                    bSucess = teacherData.Update(oclsteacher, clsteacher);
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
                teacher clsteacher = new teacher();
                clsteacher.TeacherId = System.Convert.ToInt32((Grid.SelectedCells[0].Column.GetCellContent(Grid.SelectedItem) as TextBlock).Text);
                if (MessageBox.Show("Are you sure? Delete this record?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes) {
                    SetData(clsteacher);
                    Boolean bSucess = new Boolean();
                    bSucess = teacherData.Delete(clsteacher);
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

        private void nudTeacherId_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
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
                                                     "Dbo.Teacher", Grid, (DataRowView)Grid.CurrentItem, Row);
                        }
                        else if (tiDetail.IsSelected)
                        {
                            clsSchoolData.ExportToPdf("One", 
                                                     "Dbo.Teacher", Grid, (DataRowView)Grid.CurrentItem, Row);
                        }
                    }
                    else if (cbiExcel.IsSelected == true)
                    {
                        if (tiGrid.IsSelected)
                        {
                            clsSchoolData.ExportToExcel("Many", 
                                                     "Dbo.Teacher", Grid, (DataRowView)Grid.CurrentItem, Row);
                        }
                        else if (tiDetail.IsSelected)
                        {
                            clsSchoolData.ExportToExcel("One", 
                                                     "Dbo.Teacher", Grid, (DataRowView)Grid.CurrentItem, Row);
                        }
                    }
                    else if (cbiWord.IsSelected == true)
                    {
                        if (tiGrid.IsSelected)
                        {
                            clsSchoolData.ExportToWord("Many", 
                                                     "Dbo.Teacher", Grid, (DataRowView)Grid.CurrentItem, Row);
                        }
                        else if (tiDetail.IsSelected)
                        {
                            clsSchoolData.ExportToWord("One", 
                                                     "Dbo.Teacher", Grid, (DataRowView)Grid.CurrentItem, Row);
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
                    DataTable dt = teacherData.Search(cmbFields.Text, cmbCondition.Text, txtSearch.Text);
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





 
