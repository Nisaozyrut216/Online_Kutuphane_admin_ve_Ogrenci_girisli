using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebUygulamaProje1.Models;
using WebUygulamaProje1.Utility;

namespace WebUygulamaProje1.Controllers
{
    [Authorize(Roles = UserRoles.Role_Admin)]
    public class KiralamaController : Controller
    {
        private readonly IKiralamaRepository _kiralamaRepository;
        private readonly IKitapRepository _kitapRepository;
        public readonly IWebHostEnvironment _webHostEnvironment;
        public KiralamaController(IKiralamaRepository kiralamaRepository, IKitapRepository kitapRepository,
            IWebHostEnvironment webHostEnvironment)
        {
            _kiralamaRepository = kiralamaRepository;
            _kitapRepository = kitapRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
			List<Kiralama> objKiralamaList = _kiralamaRepository.GetAll(inculedeProps:"Kitap").ToList();
			return View(objKiralamaList);
        }

        public IActionResult EkleGuncelle(int? id)
        {
            IEnumerable<SelectListItem> KitapList = _kitapRepository.GetAll().Select(k => new SelectListItem
            {
                Text = k.KitapAdi,
                Value = k.Id.ToString()
            });

            ViewBag.KitapList = KitapList;
            if (id == null || id==0)
            {
                return View();
            }
            else
            {
                //güncelleme
                Kiralama? kiralamaVt = _kiralamaRepository.Get(u => u.Id == id);  //Expression<Func<T, bool>> filtre
                if (kiralamaVt == null)
                {
                    return NotFound();
                }
                return View(kiralamaVt);

            }           
        }

        [HttpPost]
        public IActionResult EkleGuncelle(Kiralama kiralama)
        {
            var errors = ModelState.Values.SelectMany(x => x.Errors);

            if (ModelState.IsValid)
            {
                if(kiralama.Id== 0) 
                {
                    _kiralamaRepository.Ekle(kiralama);
					TempData["basarili"] = "Yeni Kiralama Başarılı Bir Şekilde Oluşturuldu!";
				}
                else
                {
                    _kiralamaRepository.Guncelle(kiralama);
					TempData["basarili"] = "Kiralama kayıt Güncelleme Başarılı!";
				}

                _kiralamaRepository.Kaydet();   //save changes yapılmazsa bilgiler veri tabanına eklenmez               
                return RedirectToAction("Index", "Kiralama");
            }

            return View();
        }


		//GET ACTION
		public IActionResult Sil(int? id)
		{
			IEnumerable<SelectListItem> KitapList = _kitapRepository.GetAll().Select(k => new SelectListItem
			{
					Text = k.KitapAdi,
					Value = k.Id.ToString()
				});
			ViewBag.KitapList = KitapList;

			if (id == null || id == 0)
			{
				return NotFound();
			}
			Kiralama? kiralamaVt = _kiralamaRepository.Get(u => u.Id == id);
			if (kiralamaVt == null)
			{
				return NotFound();
			}
			return View(kiralamaVt);
		}


		[HttpPost, ActionName("Sil")]
        public IActionResult SilPOST(int? id)
        {
            Kiralama? kiralama = _kiralamaRepository.Get(u => u.Id == id);
            if (kiralama == null)
            {
                return NotFound();
            }
            _kiralamaRepository.Sil(kiralama);
            _kiralamaRepository.Kaydet();   //save changes yapılmazsa bilgiler veri tabanına eklenmez
            TempData["basarili"] = "Kiralama İçin Silme Başarılı";
            return RedirectToAction("Index", "Kiralama");
        }


    }
}
