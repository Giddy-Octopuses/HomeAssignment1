using System.ComponentModel;

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
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }


}