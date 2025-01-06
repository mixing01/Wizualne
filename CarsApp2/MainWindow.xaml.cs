using Interfaces;
using System.Collections.ObjectModel;
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

namespace GamesDesktopApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
      

        public MainWindow()
        {
            InitializeComponent();
            // Ustawiamy DataContext, żeby MainWindow mógł korzystać z danych w CarListViewModel
            DataContext = new ViewModels.GameListViewModel();
        }


    }
}