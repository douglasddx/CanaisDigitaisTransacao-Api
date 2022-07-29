using System;
using System.Collections.Generic;
using System.IO;
using canalTransacao.Entities;
using Microsoft.AspNetCore.Hosting;

namespace canalTransacao.Service
{
    public class MessageArchiveService
    {

        private readonly IWebHostEnvironment _webHostEnvironment;

        private string _pathFilesSource = @"AppData\ToProcessing";

        public MessageArchiveService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public IEnumerable<string> ToProcessing()
        {
            _pathFilesSource = Path.Combine(_webHostEnvironment.ContentRootPath, _pathFilesSource);

            if (!Directory.Exists(_pathFilesSource))
                Directory.CreateDirectory(_pathFilesSource);

            var _filesSources = Directory.EnumerateFiles(_pathFilesSource, "*.csv");

            List<string> _files = new List<string>();
            foreach (var _fs in _filesSources)
            {
                var _file = _fs.Split("\\ToProcessing\\");
                _files.Add(_file[1]);
            }

            return _files;
        }


        public IEnumerable<Message> Process(string file)
        {
            var _file = Path.Combine(_webHostEnvironment.ContentRootPath, _pathFilesSource, file);
            if (File.Exists(_file))
            {
                var _listMessage = new List<Message>();
                var _lines = File.ReadAllLines(_file);

                //...tratamento das informações
                foreach (var l in _lines)
                {
                    _listMessage.Add(this.ChangeLineMessage(l));
                }

                //...move arquivo processado
                this.MoveToProcessed(_file);

                return _listMessage;
            }

            return null;
        }

        private Message ChangeLineMessage(string line)
        {
            var _line = line.Replace('\t', ' ');
            var _dados = _line.Split(';');
            var _message = new Message
            (
                _dados[0].Trim(),
                _dados[1].Trim()
            );

            return _message;
        }

        private void MoveToProcessed(string file)
        {
            if (File.Exists(file))
            {
                var fileOld = file.Split("ToProcessing");
                var fileNew = string.Format("{0}Processed{1}", fileOld[0], fileOld[1]);
                File.Move(file, Path.ChangeExtension(fileNew, ".pro"));
            }
        }

    }
}