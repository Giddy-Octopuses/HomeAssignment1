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
        DataContext = new MainWindowViewModel();
    }

    // This happens when you click 'load'
    public void LoadHandler(object sender, RoutedEventArgs args)
{
    try
    {
        string[] lines = File.ReadAllLines("../image.txt");

        if (DataContext is MainWindowViewModel viewModel)
        {
            viewModel.Image.Pixels.Clear(); // Clear existing pixels

            var dimensions = lines[0].Split(' ');
            if (int.TryParse(dimensions[0], out int height) && int.TryParse(dimensions[1], out int width))
            {
                viewModel.Image.Height = height;
                viewModel.Image.Width = width;
            }

            foreach (char c in lines[1])
            {
                viewModel.Image.Pixels.Add(new PixelViewModel(c == '1' ? 1 : 0));
            }

            viewModel.Image.FileNameText = "image";
            viewModel.Image.OnPropertyChanged(nameof(viewModel.Image.Pixels));

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

    public void VFlipHandler(object sender, RoutedEventArgs args)
    {
        try
        {
            // Flip the content of Image.Pixels vertically
            if (DataContext is MainWindowViewModel viewModel)
            {
                viewModel.Image.VFlip();
                message.Text = "The image is now flipped vertically!";
            }
            else
            {
                Console.WriteLine("Error: DataContext is not set or is of the wrong type.");
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error flipping Image: {ex.Message}");
        }
    }

    public void HFlipHandler(object sender, RoutedEventArgs args)
    {
        try
        {
            // Flip the content of Image.Pixels horizontally
            if (DataContext is MainWindowViewModel viewModel)
            {
                viewModel.Image.HFlip();
                message.Text = "The image is now flipped horizontally!";
            }
            else
            {
                Console.WriteLine("Error: DataContext is not set or is of the wrong type.");
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error flipping Image: {ex.Message}");
        }
    }

}