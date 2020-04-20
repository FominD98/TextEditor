using System;
using TextEditor.BL;
using System.Text.RegularExpressions;

namespace TextEditor
{
    public class MainPresenter
    {
        private readonly IMainForm _view;
        private readonly IFileManager _manager;
        private readonly IMessageService _messageService;

        public MainPresenter(IMainForm view, IFileManager manager, IMessageService service)
        {
            _view = view;
            _manager = manager;
            _messageService = service;

            _view.SetSymbolCount(0);

            _view.ContentChanged += new EventHandler(_view_ContentChanged);
            _view.FileOpenClick += new EventHandler(_view_FileOpenClick);
            _view.FileSaveClick += new EventHandler(_view_FileSaveClick);
            _view.btnOKClick += new EventHandler(_view_btnOKClick);
            
        }

        void _view_btnOKClick(object sender, EventArgs e)
        {
            try
            {
                if (_view.FindText != null && _view.InsertText != null)
                {       
                    string filePath = _view.FilePath;

                    _view.Content = Regex.Replace(_manager.GetContent(filePath), _view.FindText, _view.InsertText);
                }
            }
            catch (Exception ex)
            {
                _messageService.ShowError("Заполните поля");
            }
        }

        void _view_FileOpenClick(object sender, EventArgs e)
        {
            try
            {
                string filePath = _view.FilePath;

                if (!_manager.IsExist(filePath)) return;

                string content = _manager.GetContent(filePath);

                _view.Content = content;
                _view.SetSymbolCount(_manager.GetSymbolCount(content));
            }
            catch (Exception ex)
            {
                _messageService.ShowError(ex.Message);
            }
        }

        void _view_FileSaveClick(object sender, EventArgs e)
        {
            try
            {
                _manager.SaveContent(_view.Content, _view.FilePath);
                _messageService.ShowMessage("Файл успешно сохранен");
            }
            catch (Exception ex)
            {
                _messageService.ShowError(ex.Message);
            }
        }

        void _view_ContentChanged(object sender, EventArgs e)
        {
            _view.SetSymbolCount(_manager.GetSymbolCount(_view.Content));
        }

    }
}
