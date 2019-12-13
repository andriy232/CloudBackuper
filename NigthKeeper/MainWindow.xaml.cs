﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NightKeeper.Helper;
using NightKeeper.Helper.Backups;
using NightKeeper.Helper.Core;

namespace NigthKeeper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Core _core;

        public MainWindow()
        {
            _core = Core.GetInstance();
            InitializeComponent();

            Title = _core.GetTitle();
            lvLogOutput.ItemsSource = null;
            _core.RegisterOutput(LogOutput, RequestStringInput);
        }

        private string RequestStringInput(string message, Func<string, bool> validationfunc)
        {
            return new InputBox("text").ShowDialog();
        }

        private async Task LoadData()
        {
            _core.Load();

            RemoteBackupsState remoteBackupsState = null;

            foreach (var provider in _core.Providers)
            {
                _core.Log($"Available provider: {provider}");
            }

            foreach (var connection in _core.Connections)
            {
                remoteBackupsState = await connection.GetRemoteBackups();
                _core.Log($"{connection}, {remoteBackupsState}");
            }

            foreach (var script in _core.Scripts)
            {
                _core.Log(script.ToString());
            }
        }

        private void LogOutput(object sender, LogEntry logEntry)
        {
            if (logEntry == null)
                return;

            lvLogOutput.Items.Insert(0, logEntry);
        }

        private async void OnRootGridLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                await LoadData();
            }
            catch (Exception ex)
            {
                _core?.Log(ex);
            }
        }
    }
    public class InputBox
{

    Window Box = new Window();//window for the inputbox
    FontFamily font = new FontFamily("Tahoma");//font for the whole inputbox
    int FontSize=30;//fontsize for the input
    StackPanel sp1=new StackPanel();// items container
    string title = "InputBox";//title as heading
    string boxcontent;//title
    string defaulttext = "Write here your name...";//default textbox content
    string errormessage = "Invalid answer";//error messagebox content
    string errortitle="Error";//error messagebox heading title
    string okbuttontext = "OK";//Ok button content
    Brush BoxBackgroundColor = Brushes.GreenYellow;// Window Background
    Brush InputBackgroundColor = Brushes.Ivory;// Textbox Background
    bool clicked = false;
    TextBox input = new TextBox();
    Button ok = new Button();
    bool inputreset = false;

    public InputBox(string content)
    {
        try
        {
            boxcontent = content;
        }
        catch { boxcontent = "Error!"; }
        windowdef();
    }

    public InputBox(string content,string Htitle, string DefaultText)
    {
        try
        {
            boxcontent = content;
        }
        catch { boxcontent = "Error!"; }
        try
        {
            title = Htitle;
        }
        catch 
        {
            title = "Error!";
        }
        try
        {
            defaulttext = DefaultText;
        }
        catch
        {
            DefaultText = "Error!";
        }
        windowdef();
    }

    public InputBox(string content, string Htitle,string Font,int Fontsize)
    {
        try
        {
            boxcontent = content;
        }
        catch { boxcontent = "Error!"; }
        try
        {
            font = new FontFamily(Font);
        }
        catch { font = new FontFamily("Tahoma"); }
        try
        {
            title = Htitle;
        }
        catch
        {
            title = "Error!";
        }
        if (Fontsize >= 1)
            FontSize = Fontsize;
        windowdef();
    }

    private void windowdef()// window building - check only for window size
    {
        Box.Height = 500;// Box Height
        Box.Width = 300;// Box Width
        Box.Background = BoxBackgroundColor;
        Box.Title = title;
        Box.Content = sp1;
        Box.Closing += Box_Closing;
        TextBlock content=new TextBlock();
        content.TextWrapping = TextWrapping.Wrap;
        content.Background = null;
        content.HorizontalAlignment = HorizontalAlignment.Center;
        content.Text = boxcontent;
        content.FontFamily = font;
        content.FontSize = FontSize;
        sp1.Children.Add(content);

        input.Background = InputBackgroundColor;
        input.FontFamily = font;
        input.FontSize = FontSize;
        input.HorizontalAlignment = HorizontalAlignment.Center;
        input.Text = defaulttext;
        input.MinWidth = 200;
        input.MouseEnter += input_MouseDown;
        sp1.Children.Add(input);
        ok.Width=70;
        ok.Height=30;
        ok.Click += ok_Click;
        ok.Content = okbuttontext;
        ok.HorizontalAlignment = HorizontalAlignment.Center;
        sp1.Children.Add(ok);

    }

    void Box_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        if(!clicked)
        e.Cancel = true;
    }

    private void input_MouseDown(object sender, MouseEventArgs e)
    {
        if ((sender as TextBox).Text == defaulttext && inputreset==false)
        {
            (sender as TextBox).Text = null;
            inputreset = true;
        }
    }

    void ok_Click(object sender, RoutedEventArgs e)
    {
        clicked = true;
        if (input.Text == defaulttext||input.Text == "")
            MessageBox.Show(errormessage,errortitle);
        else
        {
            Box.Close();
        }
        clicked = false;
    }

    public string ShowDialog()
    {
        Box.ShowDialog();
        return input.Text;
    }
}
}