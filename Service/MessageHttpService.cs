using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using canalTransacao.Entities;
using canalTransacao.ViewModel;

namespace canalTransacao.Service
{
    public class MessageHttpService
    {
        private readonly HttpClient _httpClient;

        public MessageHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<CallBackViewModel>> SendLote(IEnumerable<Message> messages)
        {
            int _counter = 0;
            int _counterMax = 0;
            int _lotLength = 2;
            var _countItens = messages.Count();
            IList<Message> _lotItens = new List<Message>();
            IList<CallBackViewModel> _returnCallBacks = new List<CallBackViewModel>();

            foreach (var m in messages)
            {
                _counter++; _counterMax++;
                if (_counter == _lotLength || _counterMax == _countItens)
                {
                    _lotItens.Add(m);
                    var _messageListVM = new MessageListViewModel("GESTOR", 38054, _lotItens);

                    var _listMessages = new StringContent(
                        JsonSerializer.Serialize(_messageListVM),
                        Encoding.UTF8,
                        "application/json"
                    );

                    var _request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Post,
                        RequestUri = new Uri("http://srvsuins/sms/api/envio/lote"),
                        Content = _listMessages,
                    };

                    var _response = await _httpClient.SendAsync(_request);

                    if (_response.IsSuccessStatusCode)
                        _returnCallBacks.Add(new CallBackViewModel((int)_response.StatusCode, "SMS's processados com sucesso!", ""));
                    else
                    {
                        _returnCallBacks.Add(new CallBackViewModel((int)_response.StatusCode, "", "Ocorreu um erro ao processar os SMS's"));
                    }
                    _counter = 0;
                    _lotItens.Clear();
                }
                else
                    _lotItens.Add(m);
            }

            return _returnCallBacks;
        }
    }
}