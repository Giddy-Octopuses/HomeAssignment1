using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ImageEditor.ViewModels;

public class ImageViewModel : ObservableObject, INotifyPropertyChanged
{
    public int Height { get; set; } = 6;
    public int Width { get; set; } = 7;

    public List<PixelViewModel> Pixels { get; set; } = new();

    public string SizeText => $"size: {Height}x{Width}"; 
    private string? _fileNameText;
    public string? FileNameText
    {
        get => _fileNameText;
        set => SetProperty(ref _fileNameText, value);
    }

    
    public ImageViewModel()
    {
        // Stand in image before loading
        Console.WriteLine("Without parameters");
        for (int i = 0; i < Height * Width; i++)
        {
            Pixels.Add(new PixelViewModel(1));
        }
    } 

    public ImageViewModel(string size, string pixelData)
    {
        Console.WriteLine("With parameters");
        // Parse height and width from the size string
        var dimensions = size.Split(' ');
        if (int.TryParse(dimensions[0], out int height) && int.TryParse(dimensions[1], out int width))
        {
            Height = height;
            Width = width;
            Pixels.Clear(); // delete the stand in image
        }
        else
        {
            throw new ArgumentException("Invalid size format. Expected format: \"height width\"");
        }

        foreach (char c in pixelData)
        {
            Pixels.Add(new PixelViewModel(c == '1' ? 1 : 0)); 
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void VFlip()
    {
        Pixels = Pixels
            .Select((pixel, index) => Pixels[(Height - 1 - index / Width) * Width + index % Width])
            .ToList();
        OnPropertyChanged(nameof(Pixels));
    }

    public void HFlip()
    {
        Pixels = Pixels
            .Select((pixel, index) => Pixels[index / Width * Width + Width - 1 - index % Width])
            .ToList();
        OnPropertyChanged(nameof(Pixels));
    }
}
