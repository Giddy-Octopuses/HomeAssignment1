using System;
using System.Collections.Generic;
using System.Linq;
public class Image 
{
    public int Height { get; set; } = 0;
    public int Width { get; set; } = 0;

    public List<int>? Pixel { get; set; } = null;

    public Image(string size, string pixelData)
    {
        // Parse width and length from the size string
        var dimensions = size.Split(' ');
        if (int.TryParse(dimensions[0], out int height) && int.TryParse(dimensions[1], out int width))
        {
            Height = height;
            Width = width;
        }
        else
        {
            throw new ArgumentException("Invalid size format. Expected format: \"width length\"");
        }

        // Convert pixelData string into a list of integers
        Pixel = pixelData.Select(c => c == '1' ? 1 : 0).ToList();
    }
}
