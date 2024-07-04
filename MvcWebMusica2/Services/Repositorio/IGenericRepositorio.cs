using System.Linq.Expressions;

namespace MvcWebMusica2.Services.Repositorio
{
    public interface IGenericRepositorio<T> where T : class
    {
        Task<bool> Agregar(T element);
        Task<bool> Borrar(int id);
        Task<List<T>> DameTodos();
        Task<T?> DameUno(int? id);
        Task<List<T>> Filtra(Expression<Func<T, bool>> predicado);
        Task<int> Modificar(int id, T element);
    }
}
