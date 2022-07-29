using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using canalTransacao.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace canalTransacao.Controllers
{
    [ApiController]
    [Route("v1/registry")]
    public class RegistryController : ControllerBase
    {
        private readonly TransactionArchiveService _transactionArchiveService;

        public RegistryController(TransactionArchiveService transactionArchiveService)
        {
            _transactionArchiveService = transactionArchiveService;
        }

        [HttpGet]
        [Route("")]
        public ActionResult<IEnumerable<string>> Get()
        {
            var _files = _transactionArchiveService.ToProcessing();
            if (_files.Count() > 0)
                return Ok(_files);
            else
                return NotFound(new { message = "Não há arquivos para processamento" });
        }

        [HttpPost]
        [Route("process/{file}")]
        public ActionResult PostProcess(string file)
        {
            var _valid = _transactionArchiveService.Process(file);
            if (_valid)
                return Ok(new { message = "Arquivo processado com sucesso!" });
            else
            {
                return NotFound(new { error = "Arquivo não encontrado!" });
            }
        }
    }
}
