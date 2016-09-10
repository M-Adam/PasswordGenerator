using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using PasswordGenerator.Properties;
using static PasswordGenerator.PasswordCode;

namespace PasswordGenerator
{
    public partial class MainWindow : Window
    {
        private static readonly Settings AppSettings = Settings.Default;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void generateBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var capitals = GetCheckBoxValue(CapitalsCb);
                var digits = GetCheckBoxValue(DigitsCb);
                var symbols = GetSymbols(SymbolsCb, AllowedSymbolsBox);
                var length = GetLength(LengthBox);
                var password = CreatePassword(length, capitals, digits, symbols.ToArray());
                PasswordBox.Text = password;
                Clipboard.SetText(password);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error", exc.Message, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }
        }

        private void SymbolsCb_Checked(object sender, RoutedEventArgs e)
        {
            if (AllowedSymbolsBox == null)
            {
                return;
            }
            AllowedSymbolsBox.IsEnabled = true;
        }

        private void SymbolsCb_Unchecked(object sender, RoutedEventArgs e)
        {
            if (AllowedSymbolsBox == null)
            {
                return;
            }
            AllowedSymbolsBox.IsEnabled = false;
        }

        private void LengthUpBtn_Click(object sender, RoutedEventArgs e)
        {
            var result = GetLength(LengthBox);
            LengthBox.Text = (++result).ToString();
        }

        private void LengthDownBtn_Click(object sender, RoutedEventArgs e)
        {
            var result = GetLength(LengthBox);
            if (result <= 0)
            {
                return;
            }
            LengthBox.Text = (--result).ToString();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                AppSettings.Capitals = GetCheckBoxValue(CapitalsCb);
                AppSettings.Digits = GetCheckBoxValue(DigitsCb);
                AppSettings.Symbols = GetCheckBoxValue(SymbolsCb);
                AppSettings.Length = GetLength(LengthBox);
                AppSettings.SymbolsString = GetSymbols(SymbolsCb, AllowedSymbolsBox).ToString();
                AppSettings.Save();
            }
            catch (Exception)
            {
                // ignored
            }
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CapitalsCb.IsChecked = AppSettings.Capitals;
            DigitsCb.IsChecked = AppSettings.Digits;
            SymbolsCb.IsChecked = AppSettings.Symbols;
            LengthBox.Text = AppSettings.Length.ToString();
            AllowedSymbolsBox.Text = AppSettings.SymbolsString;
        }
    }
}