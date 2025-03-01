using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ImageEditor.ViewModels;

public class ImageViewModel : ViewModelBase, INotifyPropertyChanged
{
    public int Height { get; set; } = 6;
    public int Width { get; set; } = 7;

    public ObservableCollection<PixelViewModel> Pixels { get; set; } = new();

    public string SizeText => $"size: {Height}x{Width}";
    private string? _fileNameText;
    public string? FileNameText
    {
        get => _fileNameText;
        set => SetProperty(ref _fileNameText, value);
    }

    // Default to all black pixels
    public ImageViewModel()
    {
        Pixels = new ObservableCollection<PixelViewModel>();

        for (int i = 0; i < Height * Width; i++)
        {
            Pixels.Add(new PixelViewModel(1));
        }
    }

    // Loading the image based on the txt file
    public void update(string size, string pixelData, string? fileName = null)
    {
        Pixels = new ObservableCollection<PixelViewModel>();

        var dimensions = size.Split(' ');
        if (int.TryParse(dimensions[0], out int height) && int.TryParse(dimensions[1], out int width))
        {
            Height = height;
            Width = width;
        }
        else
        {
            throw new ArgumentException("Invalid size format. Expected format: \"height width\"");
        }

        Pixels.Clear();
        foreach (char c in pixelData)
        {
            Pixels.Add(new PixelViewModel(c == '1' ? 1 : 0));
        }

        FileNameText = fileName;
    }

    public event PropertyChangedEventHandler PropertyChanged;
    public virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    // Flipping the image vertically 
    public void VFlip()
    {
        var flippedPixels = new ObservableCollection<PixelViewModel>();

        for (int row = Height - 1; row >= 0; row--)
        {
            for (int col = 0; col < Width; col++)
            {
                flippedPixels.Add(new PixelViewModel(Pixels[row * Width + col].Value));
            }
        }

        Pixels = flippedPixels;
        OnPropertyChanged(nameof(Pixels)); // Update UI
    }

    // Flipping the image horizontally
    public void HFlip()
    {
        var flippedPixels = new ObservableCollection<PixelViewModel>();

        for (int row = 0; row < Height; row++)
        {
            for (int col = Width - 1; col >= 0; col--)
            {
                flippedPixels.Add(new PixelViewModel(Pixels[row * Width + col].Value));
            }
        }

        Pixels = flippedPixels;
        OnPropertyChanged(nameof(Pixels)); // Update UI
    }
}