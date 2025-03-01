using System.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.IO;
using System.Windows.Input;
using System.Linq;

namespace ImageEditor.ViewModels;

public partial class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
{

    private ImageViewModel _image;
    public ImageViewModel Image
    {
        get => _image;
        set
        {
            _image = value;
            OnPropertyChanged(nameof(Image)); // Notify UI of the change
        }
    }


    public MainWindowViewModel()
    {
        Image = new ImageViewModel();
         SaveCommand = new RelayCommand(SaveFile);
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }


}
    
    private string _title = "ImageEditor";
    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    private bool _isEdited = false;
    public ICommand SaveCommand { get; }

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
