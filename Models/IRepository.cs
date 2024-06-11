using System.Linq.Expressions;

namespace WebUygulamaProje1.Models
{
    public interface IRepository<T> where T : class      //T->Kitap Türüdür aslında
    {
        IEnumerable<T> GetAll(string? inculedeProps = null);
        T Get(Expression<Func<T, bool>> filtre, string? inculedeProps = null);
        void Ekle(T entity);
        void Sil(T entity);
        void SilAralik(IEnumerable<T> entities);
    }
}
