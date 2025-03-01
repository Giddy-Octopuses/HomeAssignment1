using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Avalonia.Media; // Needed for Color conversion

namespace ImageEditor.ViewModels;

public class PixelViewModel : ViewModelBase
{
    private int _value;

    public int Value
    {
        get => _value;
        set
        {
            if (SetProperty(ref _value, value))
            {
                OnPropertyChanged(nameof(Color)); // Update color
                MainWindowViewModel.Instance.IsEdited = true; // Mark as edited - for the *
            }
        }
    }

    // Convert 1 to Black and 0 to White
    public IBrush Color => Value == 1 ? Brushes.Black : Brushes.White;

    public ICommand ToggleCommand { get; }

    public PixelViewModel(int value)
    {
        _value = value;
        ToggleCommand = new RelayCommand(Toggle);
    }

    private void Toggle()
    {
        Value = Value == 1 ? 0 : 1; // Toggle between 1 and 0
    }
}
