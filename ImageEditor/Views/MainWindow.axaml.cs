using Avalonia.Controls;
using ImageEditor.ViewModels;
using Avalonia.Interactivity;
using System.IO;
using System;
using System.Linq;


namespace ImageEditor.Views;

public partial class MainWindow : Window
{
    public string? FileNameText;
    public MainWindow()
    {
        InitializeComponent();
    }

    // This happens when you click 'load'
    public void LoadHandler(object sender, RoutedEventArgs args)
    {
        try
        {
            string[] lines = File.ReadAllLines("../image.txt");
            // Get the ViewModel from DataContext
            if (DataContext is MainWindowViewModel viewModel)
            {
                viewModel.Image = new ImageViewModel(lines[0], lines[1]) // Update the existing ImageViewModel
                {
                    FileNameText = "image"
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

    public void SaveHandler(object sender, RoutedEventArgs args)
    {
        try
        {
            // Get the ViewModel from DataContext
            if (DataContext is MainWindowViewModel viewModel)
            {

                try
                {
                    if (string.IsNullOrWhiteSpace(viewModel.Image.FileNameText))
                    {
                        viewModel.Image.FileNameText = "image";
                    }

                    string filePath = $"../Images/{viewModel.Image.FileNameText}.txt";
                    string size = $"{viewModel.Image.Height} {viewModel.Image.Width}";
                    string pixelData = string.Join("", viewModel.Image.Pixels.Select(Value => Value.Value.ToString()));

                    // Save image data
                    File.WriteAllLines(filePath, new[] { size, pixelData });

                    //Title = "ImageEditor";
                    //_isEdited = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error saving file: {ex.Message}");
                }
                message.Text = "The .txt file is now saved!";
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