using System.ComponentModel;
using Avalonia.Markup.Xaml.MarkupExtensions;
using System;

namespace ImageEditor.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
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

    public int GridHeight => (Image?.Height ?? 0) * 40 + 4;
    public int GridWidth => (Image?.Width ?? 0) * 40 + 4;


    public MainWindowViewModel()
    {
        Image = new ImageViewModel();
    }

    public event PropertyChangedEventHandler PropertyChanged;
    public virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }


    private string _title = "ImageEditor";
    public string Title
    {
        get => _title;
        set
        {
            SetProperty(ref _title, value);
            OnPropertyChanged(nameof(Title));
        }
    }

    Image.BoolEdited += (sender, newValue) =>
        {
            Console.WriteLine($"Bool value changed to: {newValue}");
            if (newValue)
            {
                Title += "*"; // Mark changes
            }
            else
            {
                Title = Title.Replace("*", ""); // Remove mark
            }
    };
    

}
