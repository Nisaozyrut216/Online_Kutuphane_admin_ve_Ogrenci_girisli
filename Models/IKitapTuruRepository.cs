﻿namespace WebUygulamaProje1.Models
{
    public interface IKitapTuruRepository : IRepository<KitapTuru>
    {
        void Guncelle(KitapTuru kitap);
        void Kaydet();
    }
}
