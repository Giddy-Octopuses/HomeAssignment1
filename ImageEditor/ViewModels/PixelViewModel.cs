using System.ComponentModel;
using System.Runtime.CompilerServices;
using Avalonia.Media;
using CommunityToolkit.Mvvm.Input;

namespace ImageEditor.ViewModels;

public class PixelViewModel : INotifyPropertyChanged
{
    private int _value;

    public int Value
    {
        get => _value;
        set
        {
            _value = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(Color)); // Notify UI when the color changes
        }
    }

    public IBrush Color => Value == 1 ? Brushes.Black : Brushes.White;

    public RelayCommand ToggleCommand { get; }

    public PixelViewModel(int value)
    {
        _value = value;
        ToggleCommand = new RelayCommand(Toggle);
    }

    private void Toggle()
    {
        Value = Value == 1 ? 0 : 1; // Toggle between 0 and 1
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
