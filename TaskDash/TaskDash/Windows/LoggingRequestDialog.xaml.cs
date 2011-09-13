namespace TaskDash
{
    /// <summary>
    /// Interaction logic for LoggingRequestDialog.xaml
    /// </summary>
    public partial class LoggingRequestDialog
    {
        private readonly LoggingRequestDialogViewModel _viewModel;

        public LoggingRequestDialog(LoggingRequestDialogViewModel viewModel)
        {
            _viewModel = viewModel;

            InitializeComponent();
        }
    }
}
