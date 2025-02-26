using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Avalonia;

namespace ImageEditor.ViewModels;

public class ImageViewModel : ObservableObject
{
    public int Height { get; set; } = 0;
    public int Width { get; set; } = 0;

    public List<PixelViewModel> Pixels { get; set;} = new();

    public string? FileName { get; set; } = "image.txt";
    public string SizeText => $"size: {Height}x{Width}";

    // I changed this part, bc I want to display a stand in picture before clicking 'load'
    // but then I'm not really sure how to change the picture afterwards.. - tried doing it in the ClickHandler method but it doesn't work as intended
    public ImageViewModel(string size, string pixelData)
    {
        Height = 6;
        Width = 7;

        foreach (char c in pixelData)
        {
            Pixels.Add(new PixelViewModel(1));
        }
    } 
    // this was the original code: if we would display the correct picture automatically
    /* public ImageViewModel(string size, string pixelData)
    {
        // Parse width and length from the size string
        var dimensions = size.Split(' ');
        if (int.TryParse(dimensions[1], out int height) && int.TryParse(dimensions[0], out int width))
        {
            Height = height;
            Width = width;
            Pixels.Clear(); // delete the stand in image
        }
        else
        {
            throw new ArgumentException("Invalid size format. Expected format: \"width length\"");
        }

        foreach (char c in pixelData)
        {
            Pixels.Add(new PixelViewModel(c == '1' ? 1 : 0));
        }
    } */
}
