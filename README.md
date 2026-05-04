# Cohort Analysis Chart - WPF

A modern, interactive **Cohort Analysis Chart** built with **WPF (.NET 8)** featuring a highly customizable matrix grid with advanced cell selection and popup details.

![Demo Screenshot](https://github.com/user-attachments/assets/17a3bab3-c773-461d-b8ca-3abf14c3edab)

## ✨ Features

- **Interactive Cohort Matrix** (13×14 grid)
- Mouse + **Keyboard navigation** (Arrow keys + Enter)
- Visual highlighting of selected row and column headers
- **Popup window** on double-click or Enter key with detailed view
- Live updating timer cells
- Clean MVVM-style architecture with data binding
- Fully customizable cell styling and colors
- Buy/Sell order simulation interface in popup

![Popup Screenshot](https://github.com/user-attachments/assets/a4de1db7-9d4f-4259-b4a6-9025b9c6c09a)

## 🎯 Use Cases

- Trading & Financial Analytics
- User Retention / Cohort Analysis
- Time-based Performance Dashboards
- Custom Matrix-style Data Visualization

## 🛠️ Tech Stack

- **Framework**: WPF (.NET 8)
- **Language**: C#
- **Architecture**: MVVM (light)
- **UI Controls**: ItemsControl + UniformGrid

## 🚀 How to Run

1. Clone the repository:
   ```bash
   git clone https://github.com/kathan555/CohortAnalysisChart.git
   ```
2. Open the solution in Visual Studio 2022 (or later)
3. Set .NET 8.0 Desktop workload if not already installed
4. Build and run (F5)
   
## 📁 Project Structure

CohortAnalysisChart/
├── CohortAnalysisChart/          # Main WPF Project
│   ├── MainWindow.xaml           # Main UI layout
│   ├── MainWindow.xaml.cs        # Core logic & interactions
│   ├── MatrixElement.cs          # Cell model
│   ├── Order.cs                  # Order details model
│   ├── Styles.xaml               # Custom styles & effects
│   ├── Enum.cs                   # Cell type definitions
│   └── ...
