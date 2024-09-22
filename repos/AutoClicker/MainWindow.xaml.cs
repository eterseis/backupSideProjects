using System.Runtime.InteropServices;
using System.Windows;

namespace AutoClicker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, UIntPtr dwExtraInfo);
        [DllImport("user32.dll")]
        static extern UIntPtr GetMessageExtraInfo();

        bool isClicked = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Egg_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}