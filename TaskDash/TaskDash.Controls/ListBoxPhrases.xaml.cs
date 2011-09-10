using System.Windows.Data;
using System.Windows.Input;
using TaskDash.Core.Models.Tasks;
using TaskDash;

namespace TaskDash.CustomControls
{
    /// <summary>
    /// Interaction logic for ListBoxLinks.xaml
    /// </summary>
    public partial class ListBoxPhrases : ListBoxWithAddRemove
    {
        public ListBoxPhrases()
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
                    HideToolTip();
                    OpenEditWindow();
                }
            }
        }

        private void OpenEditWindow()
        {
            CollectionViewSource source = this.DataContext as CollectionViewSource;
            ListCollectionView view = (ListCollectionView) source.View;


            Phrase phrase = MyListBox.SelectedItem as Phrase;

            if (phrase.EditableValue == null) return;

            ZoomValue zoomWindow = new ZoomValue(view, phrase);
            zoomWindow.Show();
        }
    }
}
