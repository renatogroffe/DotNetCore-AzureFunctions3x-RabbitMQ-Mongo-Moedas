using System.Text.Json;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using ServerlessMoedas.Models;
using ServerlessMoedas.Validators;
using ServerlessMoedas.Data;

namespace ServerlessMoedas
{
    public static class MoedasRabbitMQTrigger
    {
        [FunctionName("MoedasRabbitMQTrigger")]
        public static void Run(
            [RabbitMQTrigger("queue-cotacoes", ConnectionStringSetting = "BrokerRabbitMQ")] string inputMessage,
            ILogger log)
        {
            CotacaoMoeda cotacao = null;

            try
            {
                cotacao = JsonSerializer.Deserialize<CotacaoMoeda>(inputMessage,
                    new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
            catch
            {
                log.LogError("MoedasRabbitMQTrigger - Erro durante a deserialização!");
            }

            if (cotacao != null)
            {
                var validationResult = new CotacaoMoedaValidator().Validate(cotacao);
                if (validationResult.IsValid)
                {
                    log.LogInformation($"MoedasRabbitMQTrigger - Dados pós formatação: {JsonSerializer.Serialize(cotacao)}");
                    CotacoesMoedasRepository.Save(cotacao);
                    log.LogInformation("MoedasRabbitMQTrigger - Cotação registrada com sucesso!");
                }
                else
                {
                    log.LogError("MoedasRabbitMQTrigger - Dados inválidos para a Cotação");
                    foreach (var error in validationResult.Errors)
                        log.LogError($"MoedasRabbitMQTrigger - {error.ErrorMessage}");
                }
            }
        }
    }
}