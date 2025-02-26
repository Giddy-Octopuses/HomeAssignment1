using CommunityToolkit.Mvvm.Input;
using System;
using System.IO;
using System.Windows.Input;
using System.Linq;

namespace ImageEditor.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public ImageViewModel Image { get; private set; }

    private string _title = "ImageEditor";
    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    private string _fileName = "image"; // Default file name
    public string FileName
    {
        get => _fileName;
        set
        {
            if (SetProperty(ref _fileName, value))
            {
                LoadImageData(); // Load pixels when FileName changes
            }
        }
    }

    private bool _isEdited = false;
    public ICommand SaveCommand { get; }

    public MainWindowViewModel()
    {
        try
        {
            // Load last used file name if it exists
            if (File.Exists("../last_filename.txt"))
            {
                FileName = File.ReadAllText("../last_filename.txt").Trim();
            }

            LoadImageData();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error initializing: {ex.Message}");
        }

        SaveCommand = new RelayCommand(SaveFile);
    }

    private void LoadImageData()
    {
        try
        {
            string filePath = $"../{FileName}.txt";

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                if (lines.Length >= 2)
                {
                    Image = new ImageViewModel(lines[0], lines[1], this);
                }
                else
                {
                    Console.WriteLine($"File {filePath} is incomplete. Using default values.");
                    CreateDefaultFile(filePath);
                }
            }
            else
            {
                Console.WriteLine($"File {filePath} does not exist. Creating a new file.");
                CreateDefaultFile(filePath);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading image: {ex.Message}");
        }
    }

    private void CreateDefaultFile(string filePath)
    {
        string defaultSize = "6 7"; // 6 rows, 7 columns
        string defaultPixels = "010001000000000000000000000010000010111110";

        File.WriteAllLines(filePath, new[] { defaultSize, defaultPixels });

        Image = new ImageViewModel(defaultSize, defaultPixels, this);
    }

    public void MarkAsEdited()
    {
        if (!_isEdited)
        {
            Title += "*"; // Mark changes
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
                FileName = "image";
            }

            string filePath = $"../{FileName}.txt";
            string size = $"{Image.Width} {Image.Height}";
            string pixelData = string.Join("", Image.Pixels.Select(p => p.Value.ToString()));

            // Save image data
            File.WriteAllLines(filePath, new[] { size, pixelData });

            // Save last used filename
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
