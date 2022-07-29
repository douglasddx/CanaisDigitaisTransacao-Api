using System;

namespace canalTransacao.Entities
{
    public class Registry
    {
        public Registry() { }
        public Registry(DateTime data, String canal, String transacao, int quantidade, decimal valor, int? grupo)
        {
            Data = data;
            Canal = canal;
            Transacao = transacao;
            Quantidade = quantidade;
            Valor = valor;
            Grupo = grupo;
        }

        public int Id { get; set; }

        public DateTime Data { get; set; }

        public string Canal { get; set; }

        public string Transacao { get; set; }

        public int Quantidade { get; set; }

        public decimal Valor { get; set; }

        public int? Grupo { get; set; }
    }
}