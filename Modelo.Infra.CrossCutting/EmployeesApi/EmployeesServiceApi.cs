using Modelo.Domain.Entities;
using Modelo.Infra.CrossCutting.ConsumerApi;
using Modelo.Infra.CrossCutting.Interfaces;
using Newtonsoft.Json;
using System.Net;

namespace Modelo.Infra.CrossCutting.EmployeesApi
{
    public class EmployeesServiceApi : EmployeesServiceBase, IEmployeesServiceApi
    {
        public async Task<EmployeesModels> GetEmployees()
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync($"{this.GetUriServiceAsync()}");
                response.EnsureSuccessStatusCode();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseService = JsonConvert.DeserializeObject<EmployeesModels>(response.Content.ReadAsStringAsync().Result);

                    return responseService;
                    
                }
                return new EmployeesModels();

            }
            catch (Exception ex)
            {
                throw new Exception($"Aconteceu um erro ao tentar buscar os dados da api publica erro:{ex.Message}");
            }
        }
    }
}
