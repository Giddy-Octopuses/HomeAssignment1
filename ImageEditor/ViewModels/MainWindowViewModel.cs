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
    private string _fileName = "default_name";

    public string FileName
    {
        get => _fileName;
        set => SetProperty(ref _fileName, value);
    }

    private bool _isEdited = false;

    public ICommand SaveCommand { get; }

    public MainWindowViewModel()
{
    try
    {
        // Load the last saved file name
        if (File.Exists("../last_filename.txt"))
        {
            FileName = File.ReadAllText("../last_filename.txt");
        }
        else
        {
            FileName = "default_name";
        }

        string[] lines = File.ReadAllLines("../image.txt");
        Image = new ImageViewModel(lines[0], lines[1], this);
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
        if (string.IsNullOrWhiteSpace(FileName))
        {
            FileName = "default_name"; 
        }

        string filePath = $"../{FileName}.txt";
        string size = $"{Image.Width} {Image.Height}";
        string pixelData = string.Join("", Image.Pixels.Select(p => p.Value.ToString()));

        // Save image data
        File.WriteAllLines(filePath, new[] { size, pixelData });

        // Save the file name to a settings file
        File.WriteAllText("../last_filename.txt", FileName);

        Title = "ImageEditor"; 
        _isEdited = false;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error saving file: {ex.Message}");
    }
}




}
