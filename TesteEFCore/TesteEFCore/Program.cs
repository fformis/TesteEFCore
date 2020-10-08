using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace TesteEFCore
{
    public class Produto
    {
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public decimal? Valor { get; set; }
    }

    public class Contexto : DbContext
    {
        public DbSet<Produto> Produtos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=NOTE-FLAVIO\SQL2017;Initial Catalog=TesteEF;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Contexto db = new Contexto();
            db.Produtos.Add(new Produto() { Nome = "teste"+DateTime.Now.Ticks.ToString() });
            db.SaveChanges();

            var ps = (from x in db.Produtos
                      select x).ToList();

            foreach (Produto p in ps)
                Console.WriteLine(p.Nome + "    " + p.ProdutoId.ToString());

            Console.ReadKey();
        }
    }
}
