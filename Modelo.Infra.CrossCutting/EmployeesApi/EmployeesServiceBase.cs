namespace Modelo.Infra.CrossCutting.ConsumerApi
{
    public class EmployeesServiceBase
    {
        protected HttpClient _client;
        private string BaseApiUrl { get; set; }
        public EmployeesServiceBase()
        {
            _client = new HttpClient();
        }

        public string GetUriServiceAsync()
        {
            BaseApiUrl = $"https://randomuser.me/api/?results=50";
            return BaseApiUrl;
        }
    }
}
