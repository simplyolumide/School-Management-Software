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
    public partial class frmMain : Window
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void exam_Click(object sender, RoutedEventArgs e)
        {
            Schoolmanagement.Presentation.frmexam m = new Schoolmanagement.Presentation.frmexam();
            m.Owner = this;
            m.Show();
        }

        private void result_Click(object sender, RoutedEventArgs e)
        {
            Schoolmanagement.Presentation.frmresult m = new Schoolmanagement.Presentation.frmresult();
            m.Owner = this;
            m.Show();
        }

        private void studentclass_Click(object sender, RoutedEventArgs e)
        {
            Schoolmanagement.Presentation.frmstudentclass m = new Schoolmanagement.Presentation.frmstudentclass();
            m.Owner = this;
            m.Show();
        }

        private void students_Click(object sender, RoutedEventArgs e)
        {
            Schoolmanagement.Presentation.frmstudents m = new Schoolmanagement.Presentation.frmstudents();
            m.Owner = this;
            m.Show();
        }

        private void teacher_Click(object sender, RoutedEventArgs e)
        {
            Schoolmanagement.Presentation.frmteacher m = new Schoolmanagement.Presentation.frmteacher();
            m.Owner = this;
            m.Show();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
 
