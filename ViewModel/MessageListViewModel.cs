using System;
using System.Collections.Generic;
using canalTransacao.Entities;

namespace canalTransacao.ViewModel
{
    public class MessageListViewModel
    {
        public string Legado { get; set; }

        public int Agregador { get; set; }

        public IEnumerable<Message> Mensagens { get; set; }

        public MessageListViewModel(string legado, int agregador, IEnumerable<Message> messages)
        {
            Legado = legado;
            Agregador = agregador;
            Mensagens = messages;
        }
    }
}