using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using FingerPaintSummer24.Models;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Devices.Sensors;

namespace FingerPaintSummer24.ViewModels
{
    public class DrawingViewModel : BaseViewModel
    {
        private DrawnLine _currentLine;
        private int _red;
        private int _green;
        private int _blue;

        public ObservableCollection<DrawnLine> Lines
        {
            get => GetProperty<ObservableCollection<DrawnLine>>();
            set => SetProperty(value);
        }

        public Color CurrentColor
        {
            get => Color.FromRgb(Red, Green, Blue);
            set
            {
                Red = (int)(value.Red * 255);
                Green = (int)(value.Green * 255);
                Blue = (int)(value.Blue * 255);
                OnPropertyChanged();
            }
        }

        public int Red
        {
            get => _red;
            set
            {
                if (value != _red)
                {
                    _red = value;
                    OnPropertyChanged(nameof(Red));
                    OnPropertyChanged(nameof(CurrentColor));
                }
            }
        }

        public int Green
        {
            get => _green;
            set
            {
                if (value != _green)
                {
                    _green = value;
                    OnPropertyChanged(nameof(Green));
                    OnPropertyChanged(nameof(CurrentColor));
                }
            }
        }

        public int Blue
        {
            get => _blue;
            set
            {
                if (value != _blue)
                {
                    _blue = value;
                    OnPropertyChanged(nameof(Blue));
                    OnPropertyChanged(nameof(CurrentColor));
                }
            }
        }

        public float CurrentLineWidth
        {
            get => GetProperty<float>();
            set
            {
                if (SetProperty(value))
                {
                    OnPropertyChanged(nameof(CurrentLineWidth));
                }
            }
        }

        public ObservableCollection<Color> AvailableColors { get; }

        public ICommand ClearCommand { get; }
        public ICommand UndoCommand { get; }
        public ICommand ChangeColorCommand { get; }
        public ICommand ExportCommand { get; }

        public DrawingViewModel()
        {
            Lines = new ObservableCollection<DrawnLine>();
            CurrentColor = Colors.Black;
            CurrentLineWidth = 2f;
            StartAccelerometer();

            AvailableColors = new ObservableCollection<Color>
            {
                Colors.Black, Colors.Red, Colors.Green, Colors.Blue, Colors.Yellow
            };

            ClearCommand = new Command(ClearDrawing);
            UndoCommand = new Command(UndoLastLine, CanUndo);
            ChangeColorCommand = new Command<Color>(OnColorChanged);
            //ExportCommand = new Command(async () => await ExportDrawingAsync());
        }

        private void OnColorChanged(Color color)
        {
            CurrentColor = color;
        }

        public void StartLine(float x, float y)
        {
            _currentLine = new DrawnLine(CurrentColor, CurrentLineWidth);
            _currentLine.AddPoint(x, y);
        }

        public void ContinueLine(float x, float y)
        {
            if (_currentLine != null)
            {
                _currentLine.AddPoint(x, y);
                OnPropertyChanged(nameof(Lines));
            }
        }

        public void EndLine()
        {
            if (_currentLine != null)
            {
                Lines.Add(_currentLine);
                _currentLine = null;
                (UndoCommand as Command)?.ChangeCanExecute();
            }
        }

        private void ClearDrawing()
        {
            Lines.Clear();
            (UndoCommand as Command)?.ChangeCanExecute();
        }

        private void UndoLastLine()
        {
            if (Lines.Count > 0)
            {
                Lines.RemoveAt(Lines.Count - 1);
                (UndoCommand as Command)?.ChangeCanExecute();
            }
        }

        private bool CanUndo() => Lines.Count > 0;

        private void StartAccelerometer()
        {
            if (Accelerometer.Default.IsSupported)
            {
                if(!Accelerometer.Default.IsMonitoring)
                {
                    Accelerometer.Default.ShakeDetected += Accelerometer_ShakeDetected;
                    Accelerometer.Default.Start(SensorSpeed.Game);
                }
            }
        }

        private void Accelerometer_ShakeDetected(object sender, EventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                this.ClearDrawing();
            });
        }

        private void StopAccelerometer()
        {
            if(Accelerometer.Default.IsSupported)
            {
                if(Accelerometer.Default.IsMonitoring)
                {
                    Accelerometer.Default.Stop();
                    Accelerometer.Default.ShakeDetected -= Accelerometer_ShakeDetected;
                }
            }
        }

/*        private async Task ExportDrawingAsync()
        {
            string fileName = await App.Current.MainPage.DisplayPromptAsync("Save Drawing", "Enter a name for your drawing:");

            if (!string.IsNullOrWhiteSpace(fileName))
            {
                var drawingCanvas = App.Current.MainPage.FindByName<Views.DrawingCanvas>("DrawingCanvasView");
                await drawingCanvas.SaveDrawingAsync(fileName);
            }
        }*/
    }
}
