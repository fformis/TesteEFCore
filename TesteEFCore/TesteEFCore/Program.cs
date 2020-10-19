using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace TesteEFCore
{
    public class Produto
    {
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public decimal? Valor { get; set; }
    }

    public class Autor
    {
        public int AutorId { get; set; }
        public string Nome { get; set; }
        public ICollection<Livro> Livros { get; set; }
    }

    public class Livro
    {
        public int LivroId { get; set; }
        public string Titulo { get; set; }
        public int AutorId { get; set; }
        public Autor Autor { get; set; }
    }

    public class Aluno
    {
        public int AlunoId { get; set; }
        public string Nome { get; set; }
        public ICollection<AlunoCurso> AlunoCursos { get; set; }

    }

    public class Curso
    {
        public int CursoId { get; set; }
        public string Nome { get; set; }
        public ICollection<AlunoCurso> AlunoCursos { get; set; }
    }

    public class AlunoCurso
    {
        public int AlunoId { get; set; }
        public Curso Curso { get; set; }
        public int CursoId { get; set; }
        public Aluno Aluno { get; set; }
    }

    public class Contexto : DbContext
    {
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Livro> Livros { get; set; }

        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<AlunoCurso> AlunosCursos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=NOTE-FLAVIO\SQL2017;Initial Catalog=TesteEF;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AlunoCurso>().HasKey(ac => new { ac.AlunoId, ac.CursoId });
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //TesteProduto();
            TesteAutorLivro();


            Console.ReadKey();
        }

        private static void TesteAutorLivro()
        {
            using (Contexto db = new Contexto())
            {
                //ModeloInsert1(db);
                //ModeloInsert2(db);
                //ModeloInsert3(db);

                db.SaveChanges();



                foreach (var autor in db.Autores.AsNoTracking().Include(x => x.Livros))
                {
                    Console.WriteLine($"{autor.AutorId} - {autor.Nome}");
                    foreach (var livro in autor.Livros)
                    {
                        Console.WriteLine($"\t {livro.LivroId} - {livro.Titulo}");
                    }
                }


            }
        }

        private static void ModeloInsert3(Contexto db)
        {
            //Modelo 3
            db.Add(new Livro()
            {
                Titulo = "Livro 4",
                AutorId = 1
            });
        }

        private static void ModeloInsert2(Contexto db)
        {
            //Modelo 2
            var a = new Autor()
            {
                Nome = "Autor 2"
            };
            List<Livro> livros = new List<Livro>()
                {
                    new Livro()
                    {
                        Titulo = "Livro 2", Autor = a
                    },
                    new Livro()
                    {
                        Titulo = "Livro 3", Autor = a
                    }
                };

            db.AddRange(livros);
        }

        private static void ModeloInsert1(Contexto db)
        {
            //Modelo 1
            db.Autores.Add(new Autor()
            {
                Nome = "Autor 1",
                Livros = new List<Livro>()
                    {
                        new Livro()
                        {
                            Titulo = "Livro 1"
                        }
                    }
            });
        }

        private static void TesteProduto()
        {
            using (Contexto db = new Contexto())
            {
                db.Produtos.Add(new Produto() { Nome = "teste" + DateTime.Now.Ticks.ToString() });
                db.SaveChanges();

                var ps = (from x in db.Produtos
                          select x).ToList();

                foreach (Produto p in ps)
                    Console.WriteLine(p.Nome + "    " + p.ProdutoId.ToString());
            }
        }
    }
}
