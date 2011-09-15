using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TaskDash.Core.Models.Tasks;

namespace TaskDash
{
    /// <summary>
    /// Interaction logic for LoggingRequestDialog.xaml
    /// </summary>
    public partial class LoggingRequestDialog : Window
    {
        private readonly LoggingRequestDialogViewModel _viewModel;

        public LoggingRequestDialog(Logs<Log> logs)
        {
            _viewModel = new LoggingRequestDialogViewModel(this, logs);

            InitializeComponent();

            DataContext = _viewModel;
        }
    }
}
