using System;

namespace canalTransacao.Entities
{
    public class Message
    {
        public Message(string telefone, string mensagem)
        {
            Numero = telefone;
            Mensagem = mensagem;
        }

        public string Numero { get; set; }

        public string Mensagem { get; set; }
    }
}