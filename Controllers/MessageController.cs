using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using canalTransacao.Service;
using Microsoft.AspNetCore.Mvc;

namespace canalTransacao.Controllers
{
    [ApiController]
    [Route("v1/message")]
    public class MessageController : ControllerBase
    {
        private readonly MessageArchiveService _messageArchiveService;

        private readonly MessageHttpService _messageHttpService;

        public MessageController(MessageArchiveService messageArchiveService, MessageHttpService messageHttpService)
        {
            _messageArchiveService = messageArchiveService;
            _messageHttpService = messageHttpService;
        }

        [HttpGet]
        [Route("")]
        public ActionResult<IEnumerable<string>> GetAllFiles()
        {
            var _files = _messageArchiveService.ToProcessing();
            if (_files.Count() > 0)
                return Ok(_files);
            else
                return NotFound(new { message = "Não há arquivo de mensagens para processamento" });
        }

        [HttpPost]
        [Route("process/{file}")]
        public async Task<ActionResult> PostMessage(string file)
        {
            var _messages = _messageArchiveService.Process(file);
            if (_messages != null)
            {
                var _callbacks = await _messageHttpService.SendLote(_messages);
                return Ok(_callbacks);
            }
            else
            {
                return NotFound(new { error = "Arquivo não encontrado!" });
            }
        }
    }
}