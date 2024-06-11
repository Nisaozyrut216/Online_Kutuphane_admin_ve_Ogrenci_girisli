using WebUygulamaProje1.Utility;

namespace WebUygulamaProje1.Models
{
    public class KitapTuruRepository : Repository<KitapTuru>, IKitapTuruRepository
    {
        private UygulamaDbContext _uygulamaDbContext;
        public KitapTuruRepository(UygulamaDbContext uygulamaDbContext) : base(uygulamaDbContext)
        {
            _uygulamaDbContext = uygulamaDbContext;
        }

        public void Guncelle(KitapTuru kitap)
        {
            _uygulamaDbContext.Update(kitap);
        }

        public void Kaydet()
        {
            _uygulamaDbContext.SaveChanges();
        }
    }
}
