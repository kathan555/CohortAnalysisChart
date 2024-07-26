using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CohortAnalysisChart
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private List<DateTime> dateTimes = new List<DateTime> { new DateTime(2023, 3, 1), new DateTime(2023, 4, 1), new DateTime(2023, 5, 1), new DateTime(2023, 6, 1), new DateTime(2023, 7, 1), new DateTime(2023, 8, 1), new DateTime(2023, 9, 1), new DateTime(2023, 10, 1), new DateTime(2023, 11, 1), new DateTime(2023, 12, 1), new DateTime(2024, 1, 1), new DateTime(2024, 2, 1) };

        private const int rows = 13;
        private const int columns = 14;


        public string _hour;
        public string Hour
        {
            get => _hour;
            set
            {
                _hour = value;
                OnPropertyRaised(nameof(Hour));
            }
        }

        public string _minute;
        public string Minute
        {
            get => _minute;
            set
            {
                _minute = value;
                OnPropertyRaised(nameof(Minute));
            }
        }


        public MainWindow()
        {
            InitializeComponent();
            LoadMatrix();

            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();


            var aaa = DataGridGrid.ColumnDefinitions.Count();
            DataGridGrid.ColumnDefinitions[0].Width = new GridLength(0, GridUnitType.Pixel);
            DataGridGrid.ColumnDefinitions[1].Width = new GridLength(0, GridUnitType.Pixel);
            DataGridGrid.ColumnDefinitions[2].Width = new GridLength(1, GridUnitType.Star);
        }

        private void dispatcherTimer_Tick(object? sender, EventArgs e)
        {
            if (Board.ItemsSource is ObservableCollection<MatrixElement> collection)
            {
                IEnumerable<MatrixElement> timerCells = collection.Where(a => a.CellType == CellType.Timer);

                timerCells.First(d => d.ColumnIndex == 0).Value = $"{DateTime.Now.ToString("HH")}:";
                timerCells.First(d => d.ColumnIndex == 1).Value = $"{DateTime.Now.ToString("mm")}:{DateTime.Now.ToString("ss")}";
            }
        }

        /// <summary>
        /// create matrix grid on window load.
        /// </summary>
        private void LoadMatrix()
        {
            ObservableCollection<MatrixElement> _board = new ObservableCollection<MatrixElement>();

            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    if (row == 0)
                    {
                        if (column > 2)
                            _board.Add(new MatrixElement(row, column)
                            {
                                CellType = CellType.RowHeader,
                                Value = dateTimes[column - 2].ToString("MMM-yy")
                            });
                        else if (column == 2)
                            _board.Add(new MatrixElement(row, column)
                            {
                                CellType = CellType.GrayBackGround,
                                Value = string.Empty
                            });
                        else
                            _board.Add(new MatrixElement(row, column)
                            {
                                CellType = CellType.Timer,
                                Value = column % 2 == 0 ? $"{Hour}:" : Minute
                            });
                    }
                    else
                    {
                        if (column == 0)
                            _board.Add(new MatrixElement(row, column)
                            {
                                CellType = CellType.FixingMonth,
                                Value = dateTimes[row - 1].ToString("MMM-yy")
                            });
                        else if (column == 1)
                            _board.Add(new MatrixElement(row, column)
                            {
                                CellType = CellType.FixingMatrixValue,
                                Value = new Random().Next(1234, 5420).ToString()
                            });
                        else if (row + 1 == column)
                            _board.Add(new MatrixElement(row, column)
                            {
                                CellType = CellType.ColumnHeader,
                                Value = dateTimes[column - 2].ToString("MMM-yy")
                            });
                        else if (row + 1 < column)
                            _board.Add(new MatrixElement(row, column)
                            {
                                CellType = CellType.MatrixValue,
                                Value = new Random().Next(1234, 5420).ToString()
                            });
                        else
                            _board.Add(new MatrixElement(row, column)
                            {
                                CellType = CellType.GrayBackGround,
                                Value = string.Empty
                            });
                    }
                }
            }
            Board.ItemsSource = _board;
        }

        /// <summary>
        /// mouse up event on each cell of matrix.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border border && border.Tag is MatrixElement matrixElement)
            {
                //keep this condition on top, because we use this to open a popup.
                if (matrixElement.IsSelected != null && matrixElement.IsSelected.GetValueOrDefault() && matrixElement.CellType == CellType.MatrixValue)
                {
                    flyoutControl.PlacementTarget = border;
                    flyoutControl.Focus();
                    flyoutControl.IsOpen = true;
                }

                if ((matrixElement.IsSelected == null || !matrixElement.IsSelected.GetValueOrDefault()) && matrixElement.CellType == CellType.MatrixValue)
                {
                    if (Board.ItemsSource is ObservableCollection<MatrixElement> collection)
                        SetCellSelection(matrixElement, collection);
                }
            }

            Board.Focus();
            Keyboard.Focus(Board);
        }

        /// <summary>
        /// On keyboard arrow key navigate user selection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (!(e.OriginalSource is TextBlock))
            {
                if (flyoutControl.IsOpen)
                {
                    List<TextBox> textBoxs = GetLogicalChildCollection<TextBox>(flyoutControl);

                    if (textBoxs != null)
                    {
                        TextBox? buyControl = textBoxs.FirstOrDefault(s => s.Name.Equals("PopUpBuyVolumeTextBox"));

                        if (buyControl != null)
                        {
                            buyControl.Focus();
                            Keyboard.Focus(buyControl);
                        }
                    }
                }
                else
                {
                    if (e.Key == Key.Left || e.Key == Key.Right || e.Key == Key.Up || e.Key == Key.Down || e.Key == Key.Enter)
                    {
                        if (Board.ItemsSource is ObservableCollection<MatrixElement> matrixCollection && matrixCollection.Any(s => s.IsSelected == true))
                        {
                            var selectedMatrix = matrixCollection.First(s => s.CellType == CellType.MatrixValue && s.IsSelected == true);

                            switch (e.Key)
                            {
                                case Key.Right:
                                    {
                                        MatrixElement? nextMatrix = matrixCollection.FirstOrDefault(s => s.CellType == CellType.MatrixValue && s.RowIndex == selectedMatrix.RowIndex && s.ColumnIndex == selectedMatrix.ColumnIndex + 1);

                                        SetCellSelection(nextMatrix, matrixCollection);
                                    }
                                    break;
                                case Key.Left:
                                    {
                                        var nextMatrix = matrixCollection.FirstOrDefault(s => s.CellType == CellType.MatrixValue && s.RowIndex == selectedMatrix.RowIndex && s.ColumnIndex == selectedMatrix.ColumnIndex - 1);

                                        SetCellSelection(nextMatrix, matrixCollection);
                                    }
                                    break;
                                case Key.Up:
                                    {
                                        MatrixElement? nextMatrix = matrixCollection.FirstOrDefault(s => s.CellType == CellType.MatrixValue && s.RowIndex == selectedMatrix.RowIndex - 1 && s.ColumnIndex == selectedMatrix.ColumnIndex);

                                        SetCellSelection(nextMatrix, matrixCollection);
                                    }
                                    break;
                                case Key.Down:
                                    {
                                        var nextMatrix = matrixCollection.FirstOrDefault(s => s.CellType == CellType.MatrixValue && s.RowIndex == selectedMatrix.RowIndex + 1 && s.ColumnIndex == selectedMatrix.ColumnIndex);

                                        SetCellSelection(nextMatrix, matrixCollection);
                                    }
                                    break;
                                case Key.Enter:
                                    {
                                        if (selectedMatrix.Order == null)
                                            selectedMatrix.Order = new Order() { InstrumentId = 22, Volume = selectedMatrix.ColumnIndex * 2 };

                                        //flyoutControl.PlacementTarget = border;
                                        flyoutControl.Focus();
                                        flyoutControl.IsOpen = true;
                                    }
                                    break;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// make cell selected by given matrix object.
        /// </summary>
        /// <param name="matrixObject"></param>
        /// <param name="matrixCollection"></param>
        private void SetCellSelection(MatrixElement? matrixObject, ObservableCollection<MatrixElement> matrixCollection)
        {
            if (matrixObject != null)
            {
                matrixCollection.ToList().ForEach(s => s.IsSelected = false);

                MatrixElement? selectedHeader = matrixCollection.FirstOrDefault(r => r.RowIndex == 0 && r.ColumnIndex == matrixObject.ColumnIndex);
                if (selectedHeader != null)
                    selectedHeader.IsSelected = true;

                MatrixElement? selectedColumn = matrixCollection.FirstOrDefault(r => r.RowIndex == matrixObject.RowIndex && r.ColumnIndex == matrixObject.RowIndex + 1);
                if (selectedColumn != null)
                    selectedColumn.IsSelected = true;

                matrixObject.IsSelected = true;

                SelectedValue.Text = matrixObject.Value;

                if (selectedHeader != null && selectedColumn != null)
                    selectedCellsSpeadMonths.Text = $"{selectedHeader.Value}/{selectedColumn.Value}";
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

        /// <summary>
        /// Looks for a child control within a parent by type
        /// </summary>
        public T? FindChild<T>(DependencyObject parent) where T : DependencyObject
        {
            // Confirm parent is valid.
            if (parent == null) return null;

            T? foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                // If the child is not of the request child type child
                T? childType = child as T;
                if (childType == null)
                {
                    // recursively drill down the tree
                    foundChild = FindChild<T>(child);

                    // If the child is found, break so we do not overwrite the found child.
                    if (foundChild != null) break;
                }
                else
                {
                    // child element found.
                    foundChild = (T)child;
                    break;
                }
            }
            return foundChild;
        }

        public List<T> GetLogicalChildCollection<T>(DependencyObject parent) where T : DependencyObject
        {
            List<T> logicalCollection = new List<T>();
            GetLogicalChildCollection(parent, logicalCollection);
            return logicalCollection;
        }

        private void GetLogicalChildCollection<T>(DependencyObject? parent, List<T> logicalCollection) where T : DependencyObject
        {
            IEnumerable children = LogicalTreeHelper.GetChildren(parent);
            foreach (object child in children)
            {
                if (child is DependencyObject)
                {
                    DependencyObject? depChild = child as DependencyObject;
                    if (child is T)
                    {
                        logicalCollection.Add(child as T);
                    }
                    GetLogicalChildCollection(depChild, logicalCollection);
                }
            }
        }

    }
}