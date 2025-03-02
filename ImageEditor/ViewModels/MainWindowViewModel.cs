using System.ComponentModel;

namespace ImageEditor.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public static MainWindowViewModel? Instance { get; private set; }

    private ImageViewModel _image = new();
    public ImageViewModel Image
    {
        get => _image;
        set
        {
            _image = value;
            OnPropertyChanged(nameof(Image)); // Notify UI of the change
        }
    }

    private string _fileName = string.Empty;
    public string FileName
    {
        get => _fileName;
        set
        {
            SetProperty(ref _fileName, value);
            Image.FileNameText = value; // This will be shown in the UI
            IsEdited = true; // For the *
        }
    }
    public new event PropertyChangedEventHandler? PropertyChanged;
    protected new void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    // Size of the image based on how many pixels it contains
    public int GridHeight => (Image?.Height ?? 0) * 40 + 4;
    public int GridWidth => (Image?.Width ?? 0) * 40 + 4;

    public MainWindowViewModel()
    {
        Instance = this;
        Image = new ImageViewModel();
    }

    private string _title = "ImageEditor";
    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    private bool _isEdited = false;
    public bool IsEdited // variable to determine whether the image was changed without saving
    {
        get => _isEdited;
        set
        {
            if (_isEdited != value)
            {
                _isEdited = value;
                OnPropertyChanged(nameof(IsEdited)); // Notify UI
                UpdateWindowTitle();
            }
        }
    }

    private void UpdateWindowTitle()
    {
        if (_isEdited && !Title.EndsWith("*"))
        {
            Title += "*"; // Add star if there are unsaved changes
        }
        else if (!_isEdited)
        {
            Title = "ImageEditor"; // Reset title when saved
        }
    }
}
