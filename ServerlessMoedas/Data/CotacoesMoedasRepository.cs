using System;
using MongoDB.Driver;
using ServerlessMoedas.Models;
using ServerlessMoedas.Documents;

namespace ServerlessMoedas.Data
{
    public static class CotacoesMoedasRepository
    {
        private static IMongoCollection<CotacaoMoedaDocument> GetCollectionCotacoes()
        {
            var client = new MongoClient(
                Environment.GetEnvironmentVariable("MongoConnection"));
            IMongoDatabase db = client.GetDatabase(
                Environment.GetEnvironmentVariable("MongoDatabase"));

            return db.GetCollection<CotacaoMoedaDocument>(
                Environment.GetEnvironmentVariable("MongoCollection"));
        }

        public static void Save(CotacaoMoeda cotacao)
        {
            var horario = DateTime.Now;
            var document = new CotacaoMoedaDocument();
            document.HistLancamento = cotacao.Sigla + horario.ToString("yyyyMMdd-HHmmss");
            document.Sigla = cotacao.Sigla;
            document.Valor = cotacao.Valor.Value;
            document.Data = horario.ToString("yyyy-MM-dd HH:mm:ss");

            GetCollectionCotacoes().InsertOne(document);
        }        

        public static object ListAll()
        {
            return GetCollectionCotacoes().Find(all => true).ToList();
        }
    }
}