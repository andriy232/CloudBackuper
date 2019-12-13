using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace NigthKeeper
{
    public class InputBox
    {
        private readonly StackPanel _stackPanel = new StackPanel();
        private readonly Window _windowHost = new Window();
        private readonly TextBox _textBoxInput = new TextBox();
        private readonly Button _btnOk = new Button();

        private readonly FontFamily _font = new FontFamily("Tahoma");
        private readonly int _fontSize = 14;
        private readonly string _title = "InputBox";
        private readonly string _boxcontent;
        private readonly string _defaulttext = "Write here your name..."; //default textbox content
        private readonly string _errormessage = "Invalid answer"; //error messagebox content
        private readonly string _errortitle = "Error"; //error messagebox heading title
        private readonly string _okbuttontext = "OK"; //Ok button content
        private readonly Brush _boxBackgroundColor = Brushes.LightSteelBlue; // Window Background
        private readonly Brush _inputBackgroundColor = Brushes.Ivory; // Textbox Background
        private bool _clicked = false;
        private bool _inputreset = false;

        public InputBox(string content, string title, Func<string, bool> validationfunc)
        {
            try
            {
                _boxcontent = content;
            }
            catch
            {
                _boxcontent = "Error!";
            }

            try
            {
                _title = title;
            }
            catch
            {
                _title = "Error!";
            }

            Windowdef();
        }

        private void Windowdef()
        {
            _windowHost.Height = 100; 
            _windowHost.Width = 250;
            _windowHost.Background = _boxBackgroundColor;
            _windowHost.Title = _title;
            _windowHost.Content = _stackPanel;
            _windowHost.Closing += OnWindowHostClosing;
            var content = new TextBlock
            {
                TextWrapping = TextWrapping.Wrap,
                Background = null,
                HorizontalAlignment = HorizontalAlignment.Center,
                Text = _boxcontent,
                FontFamily = _font,
                FontSize = _fontSize
            };
            _stackPanel.Children.Add(content);

            _textBoxInput.Background = _inputBackgroundColor;
            _textBoxInput.FontFamily = _font;
            _textBoxInput.FontSize = _fontSize;
            _textBoxInput.HorizontalAlignment = HorizontalAlignment.Center;
            _textBoxInput.Text = _defaulttext;
            _textBoxInput.MinWidth = 200;
            _textBoxInput.MouseEnter += TextBoxInputMouseDown;
            _stackPanel.Children.Add(_textBoxInput);
            _btnOk.Width = 70;
            _btnOk.Height = 30;
            _btnOk.Click += OnBtnOkClick;
            _btnOk.Content = _okbuttontext;
            _btnOk.HorizontalAlignment = HorizontalAlignment.Center;
            _stackPanel.Children.Add(_btnOk);
        }

        void OnWindowHostClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!_clicked)
                e.Cancel = true;
        }

        private void TextBoxInputMouseDown(object sender, MouseEventArgs e)
        {
            if ((sender as TextBox).Text == _defaulttext && _inputreset == false)
            {
                (sender as TextBox).Text = null;
                _inputreset = true;
            }
        }

        void OnBtnOkClick(object sender, RoutedEventArgs e)
        {
            _clicked = true;

            if (_textBoxInput.Text == _defaulttext || _textBoxInput.Text == "")
            {
                MessageBox.Show(_errormessage, _errortitle);
            }
            else
            {
                _windowHost.Close();
            }

            _clicked = false;
        }

        public string ShowDialog()
        {
            _windowHost.ShowDialog();
            return _textBoxInput.Text;
        }
    }
}