using System;
using System.Windows.Forms;

namespace ieProxy
{
    /// <summary>
    /// Contains information about which files were selected in an OpenFileDialog.
    /// </summary>
    public class FileSelectEventArgs : EventArgs
    {
        private readonly string _fileName;
        private readonly string[] _fileNames;

        /// <summary>
        /// Gets the path of the selected file.
        /// </summary>
        public string FileName
        {
            get { return _fileName; }
        }
        /// <summary>
        /// Gets the path of all selected files.
        /// </summary>
        public string[] FileNames
        {
            get
            {
                return (string[])_fileNames.Clone();
            }
        }

        /// <summary>
        /// Creates a new instance of this class.
        /// </summary>
        /// <param name="fName">The selected item.</param>
        /// <param name="fNames">All selected items.</param>
        public FileSelectEventArgs(string fName, string[] fNames)
        {
            _fileName = fName;
            _fileNames = fNames;
        }
    }

    /// <summary>
    /// A button that opens an OpenFileDialog when clicked.
    /// </summary>
    public partial class BrowseFileButton : Button
    {
        /// <summary>
        /// Gets or sets whether multiple files can be selected in the OpenFileDialog.
        /// </summary>
        public bool MultiSelect { get; set; }
        /// <summary>
        /// Gets or sets the filter that determines what files are displayed in the OpenFileDialog.
        /// </summary>
        public string Filter { get; set; }
        /// <summary>
        /// Gets or sets the directory shown in the OpenFileDialog when the button is pressed.
        /// </summary>
        public string InitialDirectory { get; set; }
        /// <summary>
        /// Gets or sets the title of the OpenFileDialog.
        /// </summary>
        public string DialogTitle { get; set; }

        /// <summary>
        /// Raised when the user presses OK in the OpenFileDialog.
        /// </summary>
        public event EventHandler<FileSelectEventArgs> FileSelect;

        /// <summary>
        /// Called when the button is clicked.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            using (var ofd = new OpenFileDialog())
            {
                ofd.Multiselect = MultiSelect;
                ofd.Filter = Filter;
                ofd.InitialDirectory = InitialDirectory;
                ofd.Title = DialogTitle;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    var fse = new FileSelectEventArgs(ofd.FileName, ofd.FileNames);
                    OnFileSelect(fse);
                }
            }
        }

        /// <summary>
        /// Called when the user presses OK in the OpenFileDialog.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnFileSelect(FileSelectEventArgs e)
        {
            EventHandler<FileSelectEventArgs> handler = FileSelect;
            if (handler != null) handler(this, e);
        }
    }
}
