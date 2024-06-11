using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebUygulamaProje1.Models
{
    public class KitapTuru
    {
        [Key]    //primary key olarak tanımlanması
        public int Id { get; set; }

        [Required(ErrorMessage ="Kitap Türü Adı Alanı Boş Bırakılamaz")]   //not null anlamına gelir
        [MaxLength(30)]
        [DisplayName("Kitap Türünün Adı")]
        public string Ad { get; set; }
    }
}
