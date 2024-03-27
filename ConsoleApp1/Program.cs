using ConsoleApp1.Configurations;
using ConsoleApp1.Models;
using System;

class Program
{
    static async Task Main(string[] args)
    {
        var settings = new MongoDbSettings
        {
            ConnectionString = "mongodb://localhost:27017",
            DatabaseName = "meudb"
        };

        // Instancia o repositório para Produtos
        var produtoRepository = new MongoRepository<Product>(settings);

        // Cria um novo Produto
        var novoProduto = new Product
        {
            Nome = "Notebook Gamer",
            Preco = 5000.00M,
            QuantidadeEmEstoque = 10
        };

        // Adiciona o novo Produto ao banco de dados
        await produtoRepository.AddAsync(novoProduto);

        Console.WriteLine("Produto adicionado com sucesso!");

        var produtos = await produtoRepository.GetAllAsync();

        foreach(var produto in produtos)
        {
            Console.WriteLine(produto.Nome);
        }
    }
}