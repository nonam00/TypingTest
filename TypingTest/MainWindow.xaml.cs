﻿using System.Windows;
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
        // Accuracy stat
        private int allSymbolsCount = 0;
        private int rightSymbolsCount = 0;
        private double accuracy = 0;

        // Test timer
        private DispatcherTimer globalTimer = new DispatcherTimer();
        private int timeLeft;

        // The inscription on the key -- Key.ToString() special value
        private readonly IReadOnlyDictionary<string, string> parsingDict = new Dictionary<string, string>()
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
        // Key.ToString() value -- UI Button
        private Dictionary<string, Button> buttons = new Dictionary<string, Button>() { };

        public MainWindow()
        {
            InitializeComponent();

            InitializeDictionary();
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            speedText.Text = "";
            accuracyText.Text = "";

            // Disabling an already running timer
            if (globalTimer.IsEnabled)
            {
                GameEnd();
                return;
            }
            
            globalTimer = new DispatcherTimer();
            
            // UI reset

            rightSymbolsCount = 0;
            allSymbolsCount = 0;

            input.IsEnabled = true;
            input.Text = RandomTextList.GetRandomText();
            input.Focus();

            timeLeft = 60;

            startButton.Content = "Stop";

            globalTimer.Interval = TimeSpan.FromSeconds(1);
            globalTimer.Tick += timer_Tick;
            globalTimer.Start();
        }

        private void timer_Tick(object? sender, EventArgs e)
        {
            if (timeLeft > 0)
            {
                timeText.Text = $"Time Left: {timeLeft--}";
            }
            else
            {
                GameEnd();
            }
        }

        private void GameEnd()
        {
            globalTimer.Stop();
            input.IsEnabled = false;
            timeText.Text = "";
            input.Text = "";
            startButton.Content = "Start";
        }

        private void input_KeyDown(object sender, KeyEventArgs e)
        {
            // Additional check for space, since pressing it does not fire the TextInput event
            if (e.Key == Key.Space)
            {
                CheckForSymbol(' ');
                e.Handled = true;
            }
            // To ignore changes made by pressing the delete key
            else if(e.Key == Key.Back || e.Key == Key.Delete)
            {
                e.Handled = true;
            }

            // Animation
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
                ++rightSymbolsCount;
            }
            ++allSymbolsCount;

            accuracy = Math.Round((double)rightSymbolsCount / allSymbolsCount * 100, 1);
            accuracyText.Text = $"Accuracy: {accuracy}%";

            if (timeLeft < 60)
            {
                speedText.Text = $"Speed: {Math.Round(rightSymbolsCount / ((60 - ((double)timeLeft)) /60), 1)} BPM";
            }

            if (input.Text.Length == 0)
            {
                globalTimer.Stop();
                GameEnd();
            }
        }

        // Logic for initializing Key -- UI Button dictionary 
        private void InitializeDictionary()
        {
            InitRow(row1.Children);
            InitRow(row2.Children);
            InitRow(row3.Children);
            InitRow(row4.Children);
            InitRow(row5.Children);
        }
        private void InitRow(UIElementCollection row)
        {
            foreach (var item in row)
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
    }
}