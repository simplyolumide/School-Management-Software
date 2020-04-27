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
    public partial class frmstudents : Window
    {
        public frmstudents()
        {
            InitializeComponent();
        }

        private bool AddMode = false;
        private bool EditMode = false;
        private bool DeleteMode = false;
        private int Row = 0;

        SchoolData clsSchoolData = new SchoolData();
        
        private void frmstudents_Loaded(object sender, RoutedEventArgs e)
        {
            cmbFields.Items.Add("Student ID");
            cmbFields.Items.Add("Class ID");
            cmbFields.Items.Add("Full Name");
            cmbFields.Items.Add("Date Of Birth");
            cmbFields.Items.Add("Home Address");
            cmbFields.Items.Add("Gender");
            cmbFields.Items.Add("Father");
            cmbFields.Items.Add("Mother");
            cmbFields.Items.Add("Parent Contact");

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
            Loadstudents_studentclassComboBox();
            LoadGrid(true);
            tiGrid.IsSelected = true;
        }

        private void Loadstudents_studentclassComboBox()
        {
            using (new WaitCursor())
            {
                List<students_studentclass> students_studentclassList = new  List<students_studentclass>();
                try
                {
                    students_studentclassList = students_studentclassData.List();
                    cbClassId.ItemsSource = students_studentclassList;
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
                    DataTable dt = studentsData.SelectAll();
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
            if (headername == "Date Of Birth")
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

                students clsstudents = new students();
                clsstudents.StudentId = System.Convert.ToInt32((Grid.SelectedCells[0].Column.GetCellContent(Grid.SelectedItem) as TextBlock).Text);
                clsstudents = studentsData.Select_Record(clsstudents);

                if ((clsstudents != null))
                {
                    try
                    {
                        nudStudentId.Text = System.Convert.ToInt32(clsstudents.StudentId).ToString();
                        if (clsstudents.ClassId is null) { cbClassId.SelectedValue = default(int); } else { cbClassId.SelectedValue = System.Convert.ToInt32(clsstudents.ClassId); }
                        if (clsstudents.FullName is null) { tbFullName.Text = default(string); } else { tbFullName.Text = Convert.ToString(clsstudents.FullName); }
                        if (clsstudents.DateOfBirth is null) { dtpDateOfBirth.SelectedDate = DateTime.Now; } else { dtpDateOfBirth.SelectedDate = System.Convert.ToDateTime(clsstudents.DateOfBirth); }
                        if (clsstudents.HomeAddress is null) { tbHomeAddress.Text = default(string); } else { tbHomeAddress.Text = Convert.ToString(clsstudents.HomeAddress); }
                        tbGender.Text = Convert.ToString(clsstudents.Gender);
                        if (clsstudents.Father is null) { tbFather.Text = default(string); } else { tbFather.Text = Convert.ToString(clsstudents.Father); }
                        if (clsstudents.Mother is null) { tbMother.Text = default(string); } else { tbMother.Text = Convert.ToString(clsstudents.Mother); }
                        if (clsstudents.ParentContact is null) { tbParentContact.Text = default(string); } else { tbParentContact.Text = Convert.ToString(clsstudents.ParentContact); }
                    }
                    catch
                    {
                    }
                }
            }
        }

        private void SetData(students clsstudents)
        {
            using (new WaitCursor())
            {
                if (string.IsNullOrEmpty(cbClassId.SelectedValue.ToString())) {
                    clsstudents.ClassId = null;
                } else {
                    clsstudents.ClassId = System.Convert.ToInt32(cbClassId.SelectedValue); }
                if (string.IsNullOrEmpty(tbFullName.Text)) {
                    clsstudents.FullName = null;
                } else {
                    clsstudents.FullName = tbFullName.Text; }
                if (string.IsNullOrEmpty(dtpDateOfBirth.Text)) {
                    clsstudents.DateOfBirth = null;
                } else {
                    clsstudents.DateOfBirth = System.Convert.ToDateTime(dtpDateOfBirth.Text); }
                if (string.IsNullOrEmpty(tbHomeAddress.Text)) {
                    clsstudents.HomeAddress = null;
                } else {
                    clsstudents.HomeAddress = tbHomeAddress.Text; }
                clsstudents.Gender = System.Convert.ToString(tbGender.Text);
                if (string.IsNullOrEmpty(tbFather.Text)) {
                    clsstudents.Father = null;
                } else {
                    clsstudents.Father = tbFather.Text; }
                if (string.IsNullOrEmpty(tbMother.Text)) {
                    clsstudents.Mother = null;
                } else {
                    clsstudents.Mother = tbMother.Text; }
                if (string.IsNullOrEmpty(tbParentContact.Text)) {
                    clsstudents.ParentContact = null;
                } else {
                    clsstudents.ParentContact = tbParentContact.Text; }
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
            nudStudentId.Text = System.Convert.ToString(clsSchoolData.getAutoID("New", "students"));
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
            dtpDateOfBirth.IsEnabled = YesNo;
            tbHomeAddress.IsEnabled = YesNo;
            tbGender.IsEnabled = YesNo;
            tbFather.IsEnabled = YesNo;
            tbMother.IsEnabled = YesNo;
            tbParentContact.IsEnabled = YesNo;
        }

        private void ClearRecord()
        {
            nudStudentId.Text = "0";
            cbClassId.SelectedIndex = -1;
            tbFullName.Text = null;
            dtpDateOfBirth.Text = null;
            tbHomeAddress.Text = null;
            tbGender.Text = null;
            tbFather.Text = null;
            tbMother.Text = null;
            tbParentContact.Text = null;
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
            students clsstudents = new students();
                if (VerifyData() == true) {
                    SetData(clsstudents);
                    Boolean bSucess = new Boolean();
                    bSucess = studentsData.Add(clsstudents);
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
                students oclsstudents = new students();
                students clsstudents = new students();

                oclsstudents.StudentId = System.Convert.ToInt32((Grid.SelectedCells[0].Column.GetCellContent(Grid.SelectedItem) as TextBlock).Text);
                oclsstudents = studentsData.Select_Record(oclsstudents);

                if (VerifyData() == true) {
                    SetData(clsstudents);
                    Boolean bSucess = new Boolean();
                    bSucess = studentsData.Update(oclsstudents, clsstudents);
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
                students clsstudents = new students();
                clsstudents.StudentId = System.Convert.ToInt32((Grid.SelectedCells[0].Column.GetCellContent(Grid.SelectedItem) as TextBlock).Text);
                if (MessageBox.Show("Are you sure? Delete this record?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes) {
                    SetData(clsstudents);
                    Boolean bSucess = new Boolean();
                    bSucess = studentsData.Delete(clsstudents);
                    if (bSucess == true) {
                        GoBack_To_Grid(); }
                    else {
                        MessageBox.Show("Delete failed.", "Error"); }
                }
            }
        }  

        private Boolean VerifyData()
        {
            if (tbGender.Text == "") {
                MessageBox.Show("Gender is Required", "Error");
                tbGender.Focus();
                return false;}
            return true;
        }

        private void nudStudentId_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
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
                                                     "Dbo.Students", Grid, (DataRowView)Grid.CurrentItem, Row);
                        }
                        else if (tiDetail.IsSelected)
                        {
                            clsSchoolData.ExportToPdf("One", 
                                                     "Dbo.Students", Grid, (DataRowView)Grid.CurrentItem, Row);
                        }
                    }
                    else if (cbiExcel.IsSelected == true)
                    {
                        if (tiGrid.IsSelected)
                        {
                            clsSchoolData.ExportToExcel("Many", 
                                                     "Dbo.Students", Grid, (DataRowView)Grid.CurrentItem, Row);
                        }
                        else if (tiDetail.IsSelected)
                        {
                            clsSchoolData.ExportToExcel("One", 
                                                     "Dbo.Students", Grid, (DataRowView)Grid.CurrentItem, Row);
                        }
                    }
                    else if (cbiWord.IsSelected == true)
                    {
                        if (tiGrid.IsSelected)
                        {
                            clsSchoolData.ExportToWord("Many", 
                                                     "Dbo.Students", Grid, (DataRowView)Grid.CurrentItem, Row);
                        }
                        else if (tiDetail.IsSelected)
                        {
                            clsSchoolData.ExportToWord("One", 
                                                     "Dbo.Students", Grid, (DataRowView)Grid.CurrentItem, Row);
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
                    DataTable dt = studentsData.Search(cmbFields.Text, cmbCondition.Text, txtSearch.Text);
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





 
