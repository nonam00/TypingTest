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
using System.Windows.Threading;

namespace TypingTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IReadOnlyDictionary<string, string> parsingDict = new Dictionary<string, string>()
        {
            { "~", "Oem3" },
            { "-", "OemMinus" },
            { "+", "OemPlus" },
            { " ", "Space" },
            { "[", "OemOpenBrackets" },
            { "]", "Oem6" },
            { "\\", "Oem5" },
            { ";", "Oem1" },
            { "'", "OemQuotes" },
            { ",", "OemComma" },
            { ".", "OemPeriod" },
            { "?", "OemQuestion" },
            { "Caps lock", "Capital" },
            { "<-", "Back" },
            { "Tab", "Tab" },
            { "Enter", "Return" }

        };
        private Dictionary<string, Button> buttons = new Dictionary<string, Button>() { };
        public MainWindow()
        {
            InitializeComponent();

            InitializeDictionary();
        }

        private void input_KeyDown(object sender, KeyEventArgs e)
        {
            string key = e.Key.ToString();
            
            if(buttons.TryGetValue(key, out Button? button))
            {
                button.IsEnabled = true;

                var timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromMilliseconds(10);
                timer.Tick += (s, args) =>
                {
                    button.IsEnabled = false;
                    timer.Stop();
                };
                timer.Start();
            }

        }

        private string ParseSymbol(string s)
        {
            if (s == "Shift")
            {
                s = (buttons.ContainsKey("LeftShift")) ? "RightShift" : "LeftShift";
                return s;
            }
            else if (Char.IsNumber(s[0]))
            {
                return $"D{s[0]}";
            }
            if(parsingDict.ContainsKey(s))
            {
                return parsingDict[s];
            }
            return s;
        }
        private void InitializeDictionary()
        {
            foreach (var item in row1.Children)
            {
                string symbol;
                if (item is Button button)
                {
                    symbol = button.Content.ToString()!;
                    symbol = ParseSymbol(symbol);
                    buttons.Add(symbol, button);
                }
            }
            foreach (var item in row2.Children)
            {
                string symbol;
                if (item is Button button)
                {
                    symbol = button.Content.ToString()!;
                    symbol = ParseSymbol(symbol);
                    buttons.Add(symbol, button);
                }
            }
            foreach (var item in row3.Children)
            {
                string symbol;
                if (item is Button button)
                {
                    symbol = button.Content.ToString()!;
                    symbol = ParseSymbol(symbol);
                    buttons.Add(symbol, button);
                }
            }
            foreach (var item in row4.Children)
            {
                string symbol;
                if (item is Button button)
                {
                    symbol = button.Content.ToString()!;
                    symbol = ParseSymbol(symbol);
                    buttons.Add(symbol, button);
                }
            }
            foreach (var item in row5.Children)
            {
                string symbol;
                if (item is Button button)
                {
                    symbol = button.Content.ToString()!;
                    symbol = ParseSymbol(symbol);
                    buttons.Add(symbol, button);
                }
            }
        }
    }
}