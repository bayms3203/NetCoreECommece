
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestMVCApp.libs.Application;
using TestMVCApp.libs.Data.Contexts;
using TestMVCApp.libs.Data.Repositories;
using TestMVCApp.libs.Domain;
using TestMVCApp.Models;

namespace TestMVCApp.Controllers
{


    // uygulama içerisindeki bir durumun saklama şekillerine biz state management ismini durum yönetimiş ismini vberiyor

    // State Management 2 ye ayrılır

    // 1. ServerSide => Session, Application, Cache => Sunucu yani deploy çıktımızı Hosting makinadaki çözümler
    // 2. ClientSide => QueryString, Hidden Input, Cookie, WebStorage vs.. Tarayıcı tarafındaki çözümler

    // QueryString => sayfalar araası veri taşıma işlemlerinde, sayfada bir search sonucu bir kayıt araması yaparken tercih edilir.

    // Örnek => www.sanalticaret.com/ürünler?marka=adidas&beden=42
    // marka ve beden => key iken
    // adidas ve 42 değerleri ise value dur.

    // Hidden Input => bazen form bilgilerini suncuuya gönderirken bazı alanları input type="hidden" ile son kullanıcıya göstermeden göndermemiz gerekebilir. mesela edit yaparken id alanın bir önceki dersimizde hidden input ile httpost methoduna taşımıştık.

    // Cookie => Kullanıcı Login olduktan sonraki kullanıcı adı ve Profil fotoğrafı gibi bilgileri her istek de sunucundan tekrar tekrar sorgulamak bir maliyet ve performans kaybıdır. bu sebep ile bu tarz bilgiler key value formatında string olarak cookie de saklanır. tarayıcnın veri saklamak için kullandığı bir alandır. Maksimum 4kb veri saklanabilir. Cookie deki bilgiler, suncuya istek altığında gerekirse HttpRequest içerisinden otomatik olarak taşınır. Hassas bilgiler için güvenli bir veri taşıma yöntemi değildir. Javascript ile cookie bilgisi okunarak ekranda bir şeyler gösterilebilir. 

    // Web Storage yönteminde ise cookieden farklı olarak tarayıcıdaki storage alanında session yani oturum bazlı yada persist yani kalıcı olarak key value cinsinden veri saklayabiliriz. Her bir httpRequest de bu veriler tarayıcıdan sunucuya taşınmaz. Bu sebep ile cookieden farklı olup daha güvenli bir yöntemdir. (LocalStorage, SessionStorage) 2 adet storage tekniğini tarayıcılar destekler. Javascript ile bu verileri manüpüle edebiliyoruz.

    // Server Side statelerimiz ise Session => Client tarayıcıyı açıp ilgili domain adresine bir web request de bulunduğu an sunucu tarafında bu web request için tanımlanmış unique bir id'dir. biz buna sunucu tarafında session diyoruz. Bu unique id client tarayıcı kapatmayana kadar sunucu tarafında değişmez. Ne zaman tarayıcıyı kapatır yada (gizli sekmeden) bir tab açarız bu durumda sunucu tarafında yeni bir sessionıd oluşur. Session Id oturum bazlı en güvenilir veri tutma yönetmidir. çünkü session daki bilgiler web request içerinde saklanmaz sunucu tarafında tutulur. Session hacklenmesi için sessionId değerinin client a taşınması gerekir ki, hacker server'a eriştikten sonra ilgili sessionId üzerinden sunucudaki session ait bilgileri çalsın. Sepet gibi hassas bilgiler, kart bilgileri vs session üzerinde tutulabilir. Yada oturum açan kullanıcıya ait özlük bilgileri her seferinde db den çekilmek yerine sessionda saklanabilir.

    // Application ise => Uygulama bazlı sunucu tarafında veri tutmamızı sağlayan bir yöntemdir. Application ile sitemizi ziyaret eden aktif kullanıcı sayısı, uygulama ile alaklı ayarlar (mail atmak için gerekli olan konfigürasyon bilgileri) vb durumlar için tercih edilir. Oturum ve kullanıcı bazlı herhangi bir bilgi saklamak doğru değildir. Bu web sayfamızın gün içerisinde kaç web request aldığını filan bulabiliriz. 



    // Cache => Yani verilerin sürekli veri kaynağından çekmek yerine bir başka kaynak belli bir süre tutulup, buradan daha hızlı okunması işlemi. Genelde gün içerisinde çok fazla değişim göstemeyecek, günde 1 kez güncellenen veri operasyonları için tercih edilen bir yöntemdir. 
    // Veri tabanındaki ürün kategorileri, web uygulamamıza ait menüler, web sitemize ait renk temaları ve renk kodları, uygulamaya ait bazı konfigürasyon değerleri vs cache üzerinden okunabilir. Cacheden okunan değerler bir sunucunun raminde belli bir müddet tutulduğu için diskten yani hardiskten yada bir databaseden okumadan çok daha performanslıdır. 


    // EF Core Identity Service => CookieAuthentication (FormsAuthentication) => Web Project tercihi
    // Api TokenBasedAuthentication => Access Token erişim bileti ile kimlik dpoğrulama
    // Windows => LDAP Windows Autentication => PC hesabı ile kimlik doğrulama
    // School and Coperation Account ile Authentication => sadece şirket kimlik doğrulama

    // External Authentication => FB, Twitter, Google vs üzerinden kimlik doğrulama

    // EF Core Identity ile yukarıdaki tüm kimilk dopğrulama yöntemlerini kullanabiliyoruz. bunun için hazır yapılar var ve hazır tablolar var









    public class OrderController : Controller
    {

        private ICartService _cartService;
        private IOrderService _orderService;

        private ICustomerRepository _custormerRepo;

        private readonly UserManager<IdentityUser> _userManager;

        public OrderController(
        ICartService cartService,
        IOrderService orderService,
        UserManager<IdentityUser> userManager,
        ICustomerRepository customerRepo

        )
        {


            _orderService = orderService;
            _cartService = cartService;
            _userManager = userManager;
            _custormerRepo = customerRepo;
        }

        // Product/Save
        // Sepeti tamamlama işleminde yada satın alma işleminde OrderService ile çalışmamız lazım.

        // JsonResult ile sayfa yenilenmeden AJAx ile data işlemleri yapabiliriz. Sepete ekleme işleminde sayfa yenilenmeden bu işlemi yapmış olduk


        [HttpGet]
        public async Task<ActionResult> DoOrder()
        {

            // sipariş formu açılısında kullanıcı oturum açmış ise bu durumda şuan oturum açan müşteri bilgilerini getir.
            if (User.Identity.IsAuthenticated)
            {
                var authenticatedUser = await _userManager.FindByNameAsync(User.Identity.Name);

                var customer = _custormerRepo.Find(authenticatedUser.Id);

                var model = new OrderInputModel();
                model.CustomerDetail = new CustomerDetailModel
                {
                    FullName = customer.FullName,
                    ShipAddress = customer.DefaultShippingAddress,
                    CustomerId = customer.Id,
                    PhoneNumber = customer.PhoneNumber
                };


                return View(model);

            }


            return View();
        }


        [HttpPost]
        public ActionResult DoOrder(OrderInputModel model)
        {
            // 1 sipariş düzgün bir şekilde oluşur ise bu bilgileri database gönderip siparişe çevireceğiz ve 
            // daha sonrasında kullanıcıya bir mesaj verip sms email filan göndereceğiz.

            // Kredi kartı işlem enum
            // Başarılı 200
            // red 300
            // Limit Yertersiz 500 gibi
            // eğer Başarılı ise Sipariş oluşturma işlemini yapıp kullanıcıya sipariş kodunu dönelim.


            // 2 kredi kartı doğrulmasını yalancıktan yapalım eğer limit yeterli ise ve red verilmediyse sipariş oluşsun ve kullanıcıya ViewBag ile sipariş kodunu göster


            if (ModelState.IsValid)
            {

                var cart = _cartService.GetCart();
                _orderService.DoOrder(cart, model);

                ViewBag.OrderCode = _orderService.orderCode;
                ViewBag.OrderStatus = _orderService.Status;

                return View();

            }


            ViewBag.OrderCode = string.Empty;
            ViewBag.OrderStatus = string.Empty;




            return View();
        }


    }
}
