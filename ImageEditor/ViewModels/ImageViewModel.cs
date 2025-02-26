using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ImageEditor.ViewModels;

public class ImageViewModel : ObservableObject
{
    public int Height { get; set; } = 0;
    public int Width { get; set; } = 0;

    public List<PixelViewModel> Pixels { get; set; } = new();

    private string? _fileName = "image.txt";  // Private field to store file name

    public string FileName
    {
        get => _fileName ?? "image.txt";  // Ensure a single file name is used
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                _fileName = value;
            }
        }
    }

    private MainWindowViewModel _mainViewModel;  // Store reference

    public ImageViewModel(string size, string pixelData, MainWindowViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel; // Assign MainWindowViewModel instance

        // Parse width and height
        var dimensions = size.Split(' ');
        if (dimensions.Length == 2 &&
            int.TryParse(dimensions[0], out int width) &&
            int.TryParse(dimensions[1], out int height))
        {
            Height = height;
            Width = width;
        }
        else
        {
            throw new ArgumentException("Invalid size format. Expected format: \"width height\"");
        }

        foreach (char c in pixelData)
        {
            Pixels.Add(new PixelViewModel(c == '1' ? 1 : 0, _mainViewModel)); // Pass _mainViewModel
        }
    }
}
