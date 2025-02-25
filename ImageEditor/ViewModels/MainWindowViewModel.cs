using System;
using System.IO;

namespace ImageEditor.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public ImageViewModel Image { get; set; }

    public MainWindowViewModel()
    {
        try
        {
            string[] lines = File.ReadAllLines("../image.txt");

            Image = new Image(lines[0], lines[1]);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading file: {ex.Message}");
        }
    }
    
}
