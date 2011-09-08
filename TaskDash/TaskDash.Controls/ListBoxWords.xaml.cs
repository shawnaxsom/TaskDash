using System.Windows.Data;
using System.Windows.Input;
using TaskDash.Core.Models.Tasks;
using TaskDash;

namespace TaskDash.Controls
{
    /// <summary>
    /// Interaction logic for ListBoxLinks.xaml
    /// </summary>
    public partial class ListBoxWords : ListBoxWithAddRemove
    {
        public ListBoxWords()
        {
            InitializeComponent();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (!this.IsEditingSelectedItem
                       && e.Key == Key.Enter)
            {
                if (Keyboard.IsKeyDown(Key.LeftShift)
                    || Keyboard.IsKeyDown(Key.RightShift))
                {
                    CopyToClipboard();
                }
                else
                {
                    OpenEditWindow();
                }
            }
        }

        private void OpenEditWindow()
        {
            CollectionViewSource source = this.DataContext as CollectionViewSource;
            ListCollectionView view = (ListCollectionView) source.View;


            Word word = MyListBox.SelectedItem as Word;

            if (word == null || word.EditableValue == null) return;

            ZoomValue zoomWindow = new ZoomValue(view, word);
            zoomWindow.Show();
        }
    }
}
