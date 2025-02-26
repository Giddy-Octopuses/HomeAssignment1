using System;
using System.IO;

namespace ImageEditor.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public ImageViewModel Image { get; set; }

    public MainWindowViewModel()
    {
        // this shouldn't actually be here, I just left it here bc the axaml is connected to this class,
        // so if I delete it, the pixels won't be displayed
        try
        {
            string[] lines = File.ReadAllLines("../image.txt");

            Image = new ImageViewModel(lines[0], lines[1]);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading file: {ex.Message}");
        }
    }
    
}
