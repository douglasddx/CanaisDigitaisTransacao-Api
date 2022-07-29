namespace canalTransacao.ViewModel
{
    public class CallBackViewModel
    {
        public int StatusHttp { get; set; }

        public string StatusDescription { get; set; }

        public string Error { get; set; }

        public CallBackViewModel(int statusHttp, string statusDescription, string error)
        {
            StatusHttp = statusHttp;
            StatusDescription = statusDescription;
            Error = error;
        }
    }
}