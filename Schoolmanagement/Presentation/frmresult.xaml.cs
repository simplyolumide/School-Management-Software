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
    public partial class frmresult : Window
    {
        public frmresult()
        {
            InitializeComponent();
        }

        private bool AddMode = false;
        private bool EditMode = false;
        private bool DeleteMode = false;
        private int Row = 0;

        SchoolData clsSchoolData = new SchoolData();
        
        private void frmresult_Loaded(object sender, RoutedEventArgs e)
        {
            cmbFields.Items.Add("Result ID");
            cmbFields.Items.Add("Exam ID");
            cmbFields.Items.Add("Student ID");
            cmbFields.Items.Add("Mathematics");
            cmbFields.Items.Add("English Language");
            cmbFields.Items.Add("Quantitative Reasoning");
            cmbFields.Items.Add("Elementary Science");
            cmbFields.Items.Add("Social Studies");
            cmbFields.Items.Add("Vocational Aptitude");
            cmbFields.Items.Add("Health Education");
            cmbFields.Items.Add("Computer");
            cmbFields.Items.Add("Home Economics");
            cmbFields.Items.Add("Moral Instruction");
            cmbFields.Items.Add("Total Score");
            cmbFields.Items.Add("Grade");

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
            Loadresult_examComboBox4();
            Loadresult_studentsComboBox5();
            LoadGrid(true);
            tiGrid.IsSelected = true;
        }

        private void Loadresult_examComboBox4()
        {
            using (new WaitCursor())
            {
                List<result_exam4> result_examList = new  List<result_exam4>();
                try
                {
                    result_examList = result_examData4.List();
                    cbExamId.ItemsSource = result_examList;
                    cbExamId.DisplayMemberPath = "ExamId";
                    cbExamId.SelectedValuePath = "ExamId";
                }
                catch (System.Exception ex)
                {
                     MessageBox.Show(ex.Message, ex.GetType().ToString());
                }
            }
        }

        private void Loadresult_studentsComboBox5()
        {
            using (new WaitCursor())
            {
                List<result_students5> result_studentsList = new  List<result_students5>();
                try
                {
                    result_studentsList = result_studentsData5.List();
                    cbStudentId.ItemsSource = result_studentsList;
                    cbStudentId.DisplayMemberPath = "StudentId";
                    cbStudentId.SelectedValuePath = "StudentId";
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
                    DataTable dt = resultData.SelectAll();
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
            if (headername == "Total Score")
            {
                txtCol.Binding.StringFormat = "{0:N0}";
                txtCol.ElementStyle = (System.Windows.Style)Grid.FindResource("rightAlignment");
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

                result clsresult = new result();
                clsresult.ResultId = System.Convert.ToInt32((Grid.SelectedCells[0].Column.GetCellContent(Grid.SelectedItem) as TextBlock).Text);
                clsresult = resultData.Select_Record(clsresult);

                if ((clsresult != null))
                {
                    try
                    {
                        nudResultId.Text = System.Convert.ToInt32(clsresult.ResultId).ToString();
                        if (clsresult.ExamId is null) { cbExamId.SelectedValue = default(int); } else { cbExamId.SelectedValue = System.Convert.ToInt32(clsresult.ExamId); }
                        if (clsresult.StudentId is null) { cbStudentId.SelectedValue = default(int); } else { cbStudentId.SelectedValue = System.Convert.ToInt32(clsresult.StudentId); }
                        if (clsresult.Mathematics is null) { tbMathematics.Text = default(string); } else { tbMathematics.Text = Convert.ToString(clsresult.Mathematics); }
                        if (clsresult.EnglishLanguage is null) { tbEnglishLanguage.Text = default(string); } else { tbEnglishLanguage.Text = Convert.ToString(clsresult.EnglishLanguage); }
                        if (clsresult.QuantitativeReasoning is null) { tbQuantitativeReasoning.Text = default(string); } else { tbQuantitativeReasoning.Text = Convert.ToString(clsresult.QuantitativeReasoning); }
                        if (clsresult.ElementaryScience is null) { tbElementaryScience.Text = default(string); } else { tbElementaryScience.Text = Convert.ToString(clsresult.ElementaryScience); }
                        if (clsresult.SocialStudies is null) { tbSocialStudies.Text = default(string); } else { tbSocialStudies.Text = Convert.ToString(clsresult.SocialStudies); }
                        if (clsresult.VocationalAptitude is null) { tbVocationalAptitude.Text = default(string); } else { tbVocationalAptitude.Text = Convert.ToString(clsresult.VocationalAptitude); }
                        if (clsresult.HealthEducation is null) { tbHealthEducation.Text = default(string); } else { tbHealthEducation.Text = Convert.ToString(clsresult.HealthEducation); }
                        if (clsresult.Computer is null) { tbComputer.Text = default(string); } else { tbComputer.Text = Convert.ToString(clsresult.Computer); }
                        if (clsresult.HomeEconomics is null) { tbHomeEconomics.Text = default(string); } else { tbHomeEconomics.Text = Convert.ToString(clsresult.HomeEconomics); }
                        if (clsresult.MoralInstruction is null) { tbMoralInstruction.Text = default(string); } else { tbMoralInstruction.Text = Convert.ToString(clsresult.MoralInstruction); }
                        if (clsresult.TotalScore is null) { mtbTotalScore.Text = default(string); } else { mtbTotalScore.Text = Convert.ToString(clsresult.TotalScore); }
                        if (clsresult.Grade is null) { mtbGrade.Text = default(string); } else { mtbGrade.Text = Convert.ToString(clsresult.Grade); }
                    }
                    catch
                    {
                    }
                }
            }
        }

        private void SetData(result clsresult)
        {
            using (new WaitCursor())
            {
                if (string.IsNullOrEmpty(cbExamId.SelectedValue.ToString())) {
                    clsresult.ExamId = null;
                } else {
                    clsresult.ExamId = System.Convert.ToInt32(cbExamId.SelectedValue); }
                if (string.IsNullOrEmpty(cbStudentId.SelectedValue.ToString())) {
                    clsresult.StudentId = null;
                } else {
                    clsresult.StudentId = System.Convert.ToInt32(cbStudentId.SelectedValue); }
                if (string.IsNullOrEmpty(tbMathematics.Text)) {
                    clsresult.Mathematics = null;
                } else {
                    clsresult.Mathematics = tbMathematics.Text; }
                if (string.IsNullOrEmpty(tbEnglishLanguage.Text)) {
                    clsresult.EnglishLanguage = null;
                } else {
                    clsresult.EnglishLanguage = tbEnglishLanguage.Text; }
                if (string.IsNullOrEmpty(tbQuantitativeReasoning.Text)) {
                    clsresult.QuantitativeReasoning = null;
                } else {
                    clsresult.QuantitativeReasoning = tbQuantitativeReasoning.Text; }
                if (string.IsNullOrEmpty(tbElementaryScience.Text)) {
                    clsresult.ElementaryScience = null;
                } else {
                    clsresult.ElementaryScience = tbElementaryScience.Text; }
                if (string.IsNullOrEmpty(tbSocialStudies.Text)) {
                    clsresult.SocialStudies = null;
                } else {
                    clsresult.SocialStudies = tbSocialStudies.Text; }
                if (string.IsNullOrEmpty(tbVocationalAptitude.Text)) {
                    clsresult.VocationalAptitude = null;
                } else {
                    clsresult.VocationalAptitude = tbVocationalAptitude.Text; }
                if (string.IsNullOrEmpty(tbHealthEducation.Text)) {
                    clsresult.HealthEducation = null;
                } else {
                    clsresult.HealthEducation = tbHealthEducation.Text; }
                if (string.IsNullOrEmpty(tbComputer.Text)) {
                    clsresult.Computer = null;
                } else {
                    clsresult.Computer = tbComputer.Text; }
                if (string.IsNullOrEmpty(tbHomeEconomics.Text)) {
                    clsresult.HomeEconomics = null;
                } else {
                    clsresult.HomeEconomics = tbHomeEconomics.Text; }
                if (string.IsNullOrEmpty(tbMoralInstruction.Text)) {
                    clsresult.MoralInstruction = null;
                } else {
                    clsresult.MoralInstruction = tbMoralInstruction.Text; }
                if (string.IsNullOrEmpty(mtbTotalScore.Text)) {
                    clsresult.TotalScore = null;
                } else {
                    clsresult.TotalScore = System.Convert.ToInt32(mtbTotalScore.Text); }
                if (string.IsNullOrEmpty(mtbGrade.Text)) {
                    clsresult.Grade = null;
                } else {
                    clsresult.Grade = mtbGrade.Text; }
            }
        }

        private void Add()
        {
            this.AddMode = true;
            this.EditMode = false;
            this.DeleteMode = false;

            ClearRecord();
            IsReadOnlyRecord(true);
            cbExamId.Focus();
            nudResultId.Text = System.Convert.ToString(clsSchoolData.getAutoID("New", "result"));
        }

        private void Edit()
        {
            AddMode = false;
            EditMode = true;
            DeleteMode = false;

            GetData();

            IsReadOnlyRecord(true);
            cbExamId.Focus();
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
            cbExamId.IsEnabled = YesNo;
            cbStudentId.IsEnabled = YesNo;
            tbMathematics.IsEnabled = YesNo;
            tbEnglishLanguage.IsEnabled = YesNo;
            tbQuantitativeReasoning.IsEnabled = YesNo;
            tbElementaryScience.IsEnabled = YesNo;
            tbSocialStudies.IsEnabled = YesNo;
            tbVocationalAptitude.IsEnabled = YesNo;
            tbHealthEducation.IsEnabled = YesNo;
            tbComputer.IsEnabled = YesNo;
            tbHomeEconomics.IsEnabled = YesNo;
            tbMoralInstruction.IsEnabled = YesNo;
            mtbTotalScore.IsEnabled = YesNo;
            mtbGrade.IsEnabled = YesNo;
        }

        private void ClearRecord()
        {
            nudResultId.Text = "0";
            cbExamId.SelectedIndex = -1;
            cbStudentId.SelectedIndex = -1;
            tbMathematics.Text = null;
            tbEnglishLanguage.Text = null;
            tbQuantitativeReasoning.Text = null;
            tbElementaryScience.Text = null;
            tbSocialStudies.Text = null;
            tbVocationalAptitude.Text = null;
            tbHealthEducation.Text = null;
            tbComputer.Text = null;
            tbHomeEconomics.Text = null;
            tbMoralInstruction.Text = null;
            mtbTotalScore.Text = null;
            mtbGrade.Text = null;
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
            result clsresult = new result();
                if (VerifyData() == true) {
                    SetData(clsresult);
                    Boolean bSucess = new Boolean();
                    bSucess = resultData.Add(clsresult);
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
                result oclsresult = new result();
                result clsresult = new result();

                oclsresult.ResultId = System.Convert.ToInt32((Grid.SelectedCells[0].Column.GetCellContent(Grid.SelectedItem) as TextBlock).Text);
                oclsresult = resultData.Select_Record(oclsresult);

                if (VerifyData() == true) {
                    SetData(clsresult);
                    Boolean bSucess = new Boolean();
                    bSucess = resultData.Update(oclsresult, clsresult);
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
                result clsresult = new result();
                clsresult.ResultId = System.Convert.ToInt32((Grid.SelectedCells[0].Column.GetCellContent(Grid.SelectedItem) as TextBlock).Text);
                if (MessageBox.Show("Are you sure? Delete this record?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes) {
                    SetData(clsresult);
                    Boolean bSucess = new Boolean();
                    bSucess = resultData.Delete(clsresult);
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

        private void nudResultId_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
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
                                                     "Dbo.Result", Grid, (DataRowView)Grid.CurrentItem, Row);
                        }
                        else if (tiDetail.IsSelected)
                        {
                            clsSchoolData.ExportToPdf("One", 
                                                     "Dbo.Result", Grid, (DataRowView)Grid.CurrentItem, Row);
                        }
                    }
                    else if (cbiExcel.IsSelected == true)
                    {
                        if (tiGrid.IsSelected)
                        {
                            clsSchoolData.ExportToExcel("Many", 
                                                     "Dbo.Result", Grid, (DataRowView)Grid.CurrentItem, Row);
                        }
                        else if (tiDetail.IsSelected)
                        {
                            clsSchoolData.ExportToExcel("One", 
                                                     "Dbo.Result", Grid, (DataRowView)Grid.CurrentItem, Row);
                        }
                    }
                    else if (cbiWord.IsSelected == true)
                    {
                        if (tiGrid.IsSelected)
                        {
                            clsSchoolData.ExportToWord("Many", 
                                                     "Dbo.Result", Grid, (DataRowView)Grid.CurrentItem, Row);
                        }
                        else if (tiDetail.IsSelected)
                        {
                            clsSchoolData.ExportToWord("One", 
                                                     "Dbo.Result", Grid, (DataRowView)Grid.CurrentItem, Row);
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
                    DataTable dt = resultData.Search(cmbFields.Text, cmbCondition.Text, txtSearch.Text);
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





 
