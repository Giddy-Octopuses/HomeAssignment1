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

    public int GridHeight => (Image?.Height ?? 0) * 40 + 4;
    public int GridWidth => (Image?.Width ?? 0) * 40 + 4;


    public MainWindowViewModel()
    {
        Image = new ImageViewModel();
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }



    
    private string _title = "ImageEditor";
    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    private bool _isEdited = false;

    public void MarkAsEdited()
    {
        if (!_isEdited)
        {
            Title += "*"; // Mark changes
            _isEdited = true;
        }
    }

}
