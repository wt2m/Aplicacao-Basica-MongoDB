using MongoDB.Bson.Serialization.Attributes;

namespace ConsoleApp1.Models
{
    public class Product : MongoEntity
    {
        [BsonElement("nome")]
        public string Nome { get; set; }

        [BsonElement("preco")]
        public decimal Preco { get; set; }

        [BsonElement("quantidadeEmEstoque")]
        public int QuantidadeEmEstoque { get; set; }

    }
}
