using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace CohortAnalysisChart
{
    public class MatrixElement : INotifyPropertyChanged
    {
        public MatrixElement(int rowIndex, int columnIndex)
        {
            RowIndex = rowIndex;
            ColumnIndex = columnIndex;
        }

        public int RowIndex { get; }
        public int ColumnIndex { get; }
        public Order Order { get; set; }


        public string _value;
        public string Value
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyRaised(nameof(Value));
            }
        }

        public CellType? _cellType;
        public CellType? CellType
        {
            get => _cellType;
            set
            {
                _cellType = value;
                OnCellTypeChanged(value);
                OnPropertyRaised(nameof(CellType));
            }
        }

        public bool? _isSelected;
        public bool? IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnSelectionChanged(value);
                OnPropertyRaised(nameof(IsSelected));
            }
        }

        private Brush _backgroundColor;
        public Brush BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                _backgroundColor = value;
                OnPropertyRaised(nameof(BackgroundColor));
            }
        }

        private Brush? _foreground;
        public Brush? Foreground
        {
            get => _foreground;
            set
            {
                _foreground = value;
                OnPropertyRaised(nameof(Foreground));
            }
        }

        private Brush? _borderBrush = new SolidColorBrush(Colors.Black);
        public Brush? BorderBrush
        {
            get => _borderBrush;
            set
            {
                _borderBrush = value;
                OnPropertyRaised(nameof(BorderBrush));
            }
        }

        private Thickness? _borderThickness = new Thickness(0.5);
        public Thickness? BorderThickness
        {
            get => _borderThickness;
            set
            {
                _borderThickness = value;
                OnPropertyRaised(nameof(BorderThickness));
            }
        }

        private HorizontalAlignment _horizontalAlignment = HorizontalAlignment.Center;
        public HorizontalAlignment HorizontalAlignment
        {
            get => _horizontalAlignment;
            set
            {
                _horizontalAlignment = value;
                OnPropertyRaised(nameof(HorizontalAlignment));
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyRaised(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }


        private void OnSelectionChanged(bool? isSelected)
        {
            if (isSelected == null || CellType == null)
                return;

            switch (CellType)
            {
                case CohortAnalysisChart.CellType.MatrixValue:
                    if (!isSelected.GetValueOrDefault())
                    {
                        BackgroundColor = new SolidColorBrush(Colors.White);
                        Foreground = new SolidColorBrush(Colors.Black);
                    }
                    else
                    {
                        BackgroundColor = new SolidColorBrush(Colors.Black);
                        Foreground = new SolidColorBrush(Colors.Yellow);
                    }
                    break;
                case CohortAnalysisChart.CellType.RowHeader:
                case CohortAnalysisChart.CellType.ColumnHeader:
                    if (!isSelected.GetValueOrDefault())
                    {
                        BackgroundColor = new SolidColorBrush(Colors.DarkBlue);
                        Foreground = new SolidColorBrush(Colors.White);
                    }
                    else
                    {
                        BackgroundColor = new SolidColorBrush(Colors.Black);
                        Foreground = new SolidColorBrush(Colors.Yellow);
                    }
                    break;
            }
        }

        private void OnCellTypeChanged(CellType? cellType)
        {
            if (cellType == null)
                return;

            switch (CellType)
            {
                case CohortAnalysisChart.CellType.MatrixValue:
                    BackgroundColor = new SolidColorBrush(Colors.White);
                    Foreground = new SolidColorBrush(Colors.Black);
                    break;
                case CohortAnalysisChart.CellType.RowHeader:
                case CohortAnalysisChart.CellType.ColumnHeader:
                    BackgroundColor = new SolidColorBrush(Colors.DarkBlue);
                    Foreground = new SolidColorBrush(Colors.White);
                    break;
                case CohortAnalysisChart.CellType.FixingMonth:
                    BackgroundColor = new SolidColorBrush(Colors.DarkBlue);
                    Foreground = new SolidColorBrush(Colors.White);
                    break;
                case CohortAnalysisChart.CellType.FixingMatrixValue:
                    BackgroundColor = new SolidColorBrush(Colors.White);
                    Foreground = new SolidColorBrush(Colors.Black);
                    break;
                case CohortAnalysisChart.CellType.Timer:
                    BackgroundColor = new SolidColorBrush(Colors.LightSkyBlue);
                    Foreground = new SolidColorBrush(Colors.Black);
                    BorderThickness = ColumnIndex % 2 == 0 ? new Thickness(1,1,0,1) : new Thickness(0,1,1,1);
                    HorizontalAlignment = ColumnIndex % 2 == 0 ? HorizontalAlignment.Right : HorizontalAlignment.Left;
                    break;
                case CohortAnalysisChart.CellType.GrayBackGround:
                    BackgroundColor = new SolidColorBrush(Colors.LightSlateGray);
                    Foreground = new SolidColorBrush(Colors.Transparent);
                    BorderBrush = new SolidColorBrush(Colors.LightSlateGray);
                    BorderThickness = new Thickness(0);
                    break;
            }
        }

        private Color GenerateRGB(double X)
        {
            Color color;
            if (X >= 0.5) //red and half of green colors
            {
                byte Red = (byte)((2 * X - 1) * 255);
                byte Green = (byte)((2 - 2 * X) * 255);
                byte Blue = 0;
                color = Color.FromRgb(Red, Green, Blue);
            }
            else  // blue and half of green colors
            {
                byte Red = 0;
                byte Green = (byte)((2 * X) * 255);
                byte Blue = (byte)((1 - 2 * X) * 255);
                color = Color.FromRgb(Red, Green, Blue);
            }
            return color;
        }
    }
}
