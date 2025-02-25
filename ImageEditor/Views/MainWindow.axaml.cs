using Avalonia.Controls;
using System;
using System.IO;

namespace ImageEditor.Views;

public partial class MainWindow : Window
{
    public Image? image;
    public MainWindow()
    {
    
        try
        {
            // Read a text file line by line.
            string[] lines = File.ReadAllLines("../image.txt");

            image = new Image(lines[0], lines[1]);
            Console.WriteLine(image.FileName);
            var filename = image.FileName;

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading file: {ex.Message}");
        }
        

        InitializeComponent();
    }
}