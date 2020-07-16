using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ServerlessMoedas.Data;

namespace ServerlessMoedas
{
    public static class Cotacoes
    {
        [FunctionName("Cotacoes")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Cotacoes (HttpTrigger) - Processando requisição");
            return new OkObjectResult(CotacoesMoedasRepository.ListAll());
        }
    }
}