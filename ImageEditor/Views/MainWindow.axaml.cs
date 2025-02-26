using Avalonia.Controls;
using ImageEditor.ViewModels;
using ImageEditor;
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

    public void ClickHandler(object sender, RoutedEventArgs args)
        {
            try
            {
                string[] lines = File.ReadAllLines("../image.txt");
                // Get the ViewModel from DataContext
                if (DataContext is MainWindowViewModel viewModel)
                {
                    viewModel.Image = new ImageViewModel(lines[0], lines[1]); // Update the existing ImageViewModel
                }
                else
                {
                    Console.WriteLine("Error: DataContext is not set or is of the wrong type.");
                }
                // MainWindowViewModel.Image = new ImageViewModel(lines[0], lines[1]); // ideally we shouldn't create a new image, just change the other one (?)
                
            }    
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
            }
        }
    
}