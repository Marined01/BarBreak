using System.Windows;
using BarBreak.Presentation.ViewModels;

namespace BarBreak.Presentation
{
    public partial class CoursesView : Window
    {
        public CoursesView()
        {
            InitializeComponent();
            this.DataContext = new CoursesViewModel();
        }
    }
}
