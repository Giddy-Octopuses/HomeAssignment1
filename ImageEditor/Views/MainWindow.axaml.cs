using Avalonia.Controls;
using ImageEditor.ViewModels;
using Avalonia.Interactivity;
using System.IO;
using System;


namespace ImageEditor.Views;

public partial class MainWindow : Window
{
    public string? FileNameText;
    public MainWindow()
    {
        InitializeComponent();
    }

    // This happens when you click 'load'
    public void ClickHandler(object sender, RoutedEventArgs args)
    {
        try
        {
            string[] lines = File.ReadAllLines("../image.txt");
            // Get the ViewModel from DataContext
            if (DataContext is MainWindowViewModel viewModel)
            {
                viewModel.Image = new ImageViewModel(lines[0], lines[1]) // Update the existing ImageViewModel
                {
                    FileNameText = "image.txt"
                };
                message.Text = "The .txt file is now loaded!";
            }
            else
            {
                Console.WriteLine("Error: DataContext is not set or is of the wrong type.");
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading file: {ex.Message}");
        }
    }

}