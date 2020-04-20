using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextEditor
{
    public interface IMainForm
    {
        string FilePath { get; }
        string Content { get; set; }
        string FindText { get; set; }
        string InsertText { get; set; }
        void SetSymbolCount(int count);

        event EventHandler btnOKClick;
        event EventHandler FileOpenClick;
        event EventHandler FileSaveClick;
        event EventHandler ContentChanged;
    }

    public partial class MainForm : Form, IMainForm
    {
        public MainForm()
        {
            InitializeComponent();
            btnOpenFile.Click += new EventHandler(BtnOpenFile_Click);
            btnSaveFile.Click += BtnSaveFile_Click;
            fldContent.TextChanged += FldContent_TextChanged;
            btnSelectFile.Click += BtnSelectFile_Click;
            numFont.ValueChanged += NumFont_ValueChanged;
            btnOK.Click += BtnOK_Click;
        }

        #region Проброс событий

        private void FldContent_TextChanged(object sender, EventArgs e)
        {
            ContentChanged?.Invoke(this, EventArgs.Empty);
        }

        private void BtnSaveFile_Click(object sender, EventArgs e)
        {
            if (FileSaveClick != null) FileSaveClick(this, EventArgs.Empty);
        }

        private void BtnOpenFile_Click(object sender, EventArgs e)
        {
            if (FileOpenClick != null) FileOpenClick(this, EventArgs.Empty);
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            btnOKClick?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region реализация интерфейса

        public string FilePath
        {
            get { return fldFilePath.Text; }
        }

        public string Content
        {
            get { return fldContent.Text; }
            set { fldContent.Text = value; }
        }

        public string FindText
        {
            get { return fldFindText.Text; }
            set { fldFindText.Text = value; }
        }

        public string InsertText
        {
            get { return fldInsertText.Text; }
            set { fldInsertText.Text = value; }
        }

        public void SetSymbolCount(int count)
        {
            lblSymbolCount.Text = count.ToString();
        }

        #endregion

        public event EventHandler FileOpenClick;
        public event EventHandler FileSaveClick;
        public event EventHandler ContentChanged;
        public event EventHandler btnOKClick;

        private void BtnSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Текстовые файлы|*.txt|Все файлы|*.*";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                fldFilePath.Text = dlg.FileName;

                if (FileOpenClick != null) FileOpenClick(this, EventArgs.Empty);
            }
        }

        private void NumFont_ValueChanged(object sender, EventArgs e)
        {
            fldContent.Font = new Font("Calibri", (float)numFont.Value);
        }

    }
}
