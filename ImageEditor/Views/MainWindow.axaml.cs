using Avalonia.Controls;
using ImageEditor.ViewModels;
using Avalonia.Interactivity;
using System.IO;
using System;
using System.Collections.Generic;


namespace ImageEditor.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {     
        InitializeComponent();
    }

    public ImageViewModel Image { get; set; }
    public List<PixelViewModel> Pixels { get; set;} //= new();

    // when you click the 'load' button:
    public void ClickHandler(object sender, RoutedEventArgs args)
    {
        try
        {
            string[] lines = File.ReadAllLines("../image.txt");

            Image = new ImageViewModel(lines[0], lines[1]); // ideally we shouldn't create a new image, just change the other one (?)
            {
                // Parse width and length from the size string
                var dimensions = lines[0].Split(' ');
                if (int.TryParse(dimensions[0], out int height) && int.TryParse(dimensions[1], out int width))
                {
                    Height = height;
                    Width = width;
                    //Pixels.Clear(); // delete the stand in image
                    message.Text = "The .txt file is now loaded!";
                }
                else
                {
                    throw new ArgumentException("Invalid size format. Expected format: \"height width\"");
                }

                foreach (char c in lines[1])
                {
                    Pixels.Add(new PixelViewModel(c == '1' ? 1 : 0));
                }
            }

        }    
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading file: {ex.Message}");
        }
    }
}