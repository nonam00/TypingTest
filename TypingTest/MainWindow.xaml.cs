using Microsoft.VisualBasic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace TypingTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int RightSymbolsCount = 0;
        private int WrongSymbolsCount = 0;
        private int timeLeft;

        private const string Text = "Anyway, the presiding judge said he was going to proceed with the calling of witnesses. " +
            "The bailiff read off some names that caught my attention. " +
            "In the middle of what until then had been a shapeless mass of spectators, " +
            "I saw them stand up one by one, only to disappear again through a side door: " +
            "the director and the caretaker from the home, old Thomas Perez, Raymond, Masson, Salamano, and Marie. " +
            "She waved to me, anxiously. I was still feeling surprised that I hadn't seen them before when Celeste, " +
            "the last to be called, stood up. I recognized next to him the little woman from the restaurant, " +
            "with her jacket and her stiff and determined manner. She was staring right at me. " +
            "But I didn't have time to think about them, because the presiding judge started speaking. " +
            "He said that the formal proceedings were about to begin and that he didn't think he needed to remind the public to remain silent. " +
            "According to him, he was there to conduct in an impartial manner the proceedings of a case which he would consider objectively. " +
            "The verdict returned by the jury would be taken in a spirit of justice, and, in any event, " +
            "he would have the courtroom cleared at the slightest disturbance.";
        
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
            if(e.Key == Key.Space)
            {
                CheckForSymbol(' ');
                e.Handled = true;
            }

            else if(e.Key == Key.Back || e.Key == Key.Delete)
            {
                e.Handled = true;
            }

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

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            input.IsEnabled = true;
            input.Text = Text;
            input.Focus();

            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timeLeft = 60;

            timer.Tick += (s, args) =>
            {
                if(timeLeft > 0) 
                {
                    timeLeft--;
                    timeText.Text = $"Time Left: {timeLeft}";
                }
                else
                {
                    timer.Stop();
                    input.IsEnabled = false;
                    speedText.Text = $"Speed: {RightSymbolsCount} BPM";
                    input.Text = Text;
                }
            };
            timer.Start();
        }
        private void input_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            CheckForSymbol(e.Text[0]);
            e.Handled = true;
        }

        private void CheckForSymbol(char symbol)
        {
            input.Select(0, 0);
            if (symbol == input.Text[0])
            {
                input.Select(0, 0);
                input.Text = input.Text.Remove(0, 1);
                ++RightSymbolsCount;
            }
            ++WrongSymbolsCount;
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