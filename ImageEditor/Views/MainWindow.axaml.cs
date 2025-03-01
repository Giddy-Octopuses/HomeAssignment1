using Avalonia.Controls;
using ImageEditor.ViewModels;
using Avalonia.Interactivity;
using System.IO;
using System;
using System.Linq;


namespace ImageEditor.Views;

public partial class MainWindow : Window
{
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
            string[] lines = File.ReadAllLines("./Assets/image.txt");

            if (DataContext is MainWindowViewModel viewModel)
            {
                viewModel.Image.update(lines[0], lines[1], "image");

                viewModel.Image.OnPropertyChanged(nameof(viewModel.Image.Pixels));
                viewModel.Image.OnPropertyChanged(nameof(viewModel.Image.FileNameText));

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

                    viewModel.IsEdited = false; // Remove the star after saving
                    message.Text = "The .txt file is now saved!";
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error saving file: {ex.Message}");
                }
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
                viewModel.IsEdited = true;
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
                viewModel.IsEdited = true;
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