using myProject.Database;
using myProject.Model;
using Microsoft.EntityFrameworkCore;

namespace myProject.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        //injetar dependencia do contexto
        private readonly BaseDbContext _context;

        public ProdutoRepository(BaseDbContext context) { 
            _context = context;
        }

        public void AddProduto(Produto produto)
        {
            _context.Add(produto);
        }

        public void AtualizarProduto(Produto produto)
        {
            _context.Update(produto);
        }

        public void DeletarProduto(Produto produto)
        {
            _context.Remove(produto);
        }

        public async Task<Produto> GetProdutoById(int id)
        {
            return await _context.Produtos
            .Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Produto>> GetProdutos()
        {
            return await _context.Produtos.ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}