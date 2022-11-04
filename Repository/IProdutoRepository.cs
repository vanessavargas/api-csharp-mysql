using myProject.Model;

namespace myProject.Repository
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto>> GetProdutos();
        Task<Produto> GetProdutoById(int id);
        void AddProduto(Produto produto);
        void AtualizarProduto(Produto produto);
        void DeletarProduto(Produto produto);
        Task<bool> SaveChangesAsync();
        
    }
}