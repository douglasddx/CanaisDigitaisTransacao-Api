using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using canalTransacao.Data.Repositories;
using canalTransacao.Entities;
using Microsoft.AspNetCore.Hosting;

namespace canalTransacao.Service
{
    public class TransactionArchiveService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly RegistryRepository _registryRepository;
        private string _pathFilesSource = @"AppData\ToProcessing";

        public TransactionArchiveService(IWebHostEnvironment webHostEnvironment, RegistryRepository registryRepository)
        {
            _webHostEnvironment = webHostEnvironment;
            _registryRepository = registryRepository;
        }

        public IEnumerable<string> ToProcessing()
        {
            _pathFilesSource = Path.Combine(_webHostEnvironment.ContentRootPath, _pathFilesSource);

            if (!Directory.Exists(_pathFilesSource))
                Directory.CreateDirectory(_pathFilesSource);

            var _filesSources = Directory.EnumerateFiles(_pathFilesSource, "*.txt");

            List<string> _files = new List<string>();
            foreach (var _fs in _filesSources)
            {
                var _file = _fs.Split("\\ToProcessing\\");
                _files.Add(_file[1]);
            }

            return _files;
        }

        public bool Process(string file)
        {
            bool _return = false;

            var _file = Path.Combine(_webHostEnvironment.ContentRootPath, _pathFilesSource, file);
            if (File.Exists(_file))
            {
                var _listRegistry = new List<Registry>();
                var _lines = File.ReadAllLines(_file);

                //...tratamento das informações
                foreach (var l in _lines)
                {
                    _listRegistry.Add(this.ChangeLineRegistry(l));
                }

                //...persistencia aqui
                _registryRepository.AddRegistrys(_listRegistry);
                _registryRepository.Salve();

                //...move arquivo processado
                this.MoveToProcessed(_file);

                _return = true;
            }

            return _return;
        }

        private Registry ChangeLineRegistry(string line)
        {
            var _line = line.Replace('\t', ' ');
            var _dados = _line.Split(';');
            var _registry = new Registry
            (
                Convert.ToDateTime(_dados[0].Trim()),
                _dados[1].Trim(),
                _dados[2].Trim(),
                Convert.ToInt32(_dados[3].Trim()),
                string.IsNullOrEmpty(_dados[4]) ? 0 : Convert.ToDecimal(_dados[4].Replace("R$", "").Trim()),
                string.IsNullOrEmpty(_dados[5]) ? null : Convert.ToInt32(_dados[5].Trim())
            );

            return _registry;
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