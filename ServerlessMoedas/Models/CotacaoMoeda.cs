namespace ServerlessMoedas.Models
{
    public class CotacaoMoeda
    {
        private string _sigla;
        public string Sigla
        {
            get => _sigla;
            set => _sigla = value?.Trim().ToUpper();
        }

        public double? Valor { get; set; }
    }
}