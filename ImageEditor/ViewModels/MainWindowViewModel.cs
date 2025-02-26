using CommunityToolkit.Mvvm.Input;
using System;
using System.IO;
using System.Windows.Input;
using System.Linq;


namespace ImageEditor.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public ImageViewModel Image { get; set; }
    
    private string _title = "ImageEditor";
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

    private bool _isEdited = false;

        public ICommand SaveCommand { get; }

        public MainWindowViewModel()
    {
        try
        {
            string[] lines = File.ReadAllLines("../image.txt");
            Image = new ImageViewModel(lines[0], lines[1], this); // Pass `this`
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading file: {ex.Message}");
        }

        SaveCommand = new RelayCommand(SaveFile);
    }

        public void MarkAsEdited()
        {
        if (!_isEdited)
        {
            Title += "*"; // Append * to indicate unsaved changes
            _isEdited = true;
        }
    }

    private void SaveFile()
    {
        if (Image == null) return;

        try
        {
            string filePath = "../image.txt";
            string size = $"{Image.Width} {Image.Height}";
            string pixelData = string.Join("", Image.Pixels.Select(p => p.Value.ToString()));

            File.WriteAllLines(filePath, new[] { size, pixelData });

            Title = "ImageEditor"; // Reset title after saving
            _isEdited = false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving file: {ex.Message}");
        }
    }
}
