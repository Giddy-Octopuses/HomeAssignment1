using System.ComponentModel;

namespace ImageEditor.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public static MainWindowViewModel Instance { get; private set; }

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

    private string _fileName;
    public string FileName
    {
        get => _fileName;
        set 
        {
            SetProperty(ref _fileName, value);
            Image.FileNameText = value;
            IsEdited = true;
        }
    }

    public int GridHeight => (Image?.Height ?? 0) * 40 + 4;
    public int GridWidth => (Image?.Width ?? 0) * 40 + 4;

    public MainWindowViewModel()
    {
        Instance = this;
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
        set => SetProperty(ref _title, value);
    }

    private bool _isEdited = false;

    public bool IsEdited
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
