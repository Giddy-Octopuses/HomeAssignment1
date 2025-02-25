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

    public List<PixelViewModel> Pixels { get; set;} = new();

    public string? FileName { get; set; } = "image.txt";

    public ImageViewModel(string size, string pixelData)
    {
        // Parse width and length from the size string
        var dimensions = size.Split(' ');
        if (int.TryParse(dimensions[1], out int height) && int.TryParse(dimensions[0], out int width))
        {
            Height = height;
            Width = width;
        }
        else
        {
            throw new ArgumentException("Invalid size format. Expected format: \"width length\"");
        }

        foreach (char c in pixelData)
        {
            Pixels.Add(new PixelViewModel(c == '1' ? 1 : 0));
        }
    }
}
