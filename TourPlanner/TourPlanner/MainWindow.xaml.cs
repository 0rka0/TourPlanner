using System.Windows;

namespace TourPlanner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            log4net.Config.XmlConfigurator.Configure();
            InitializeComponent();
        }
    }
}
