namespace TestMVCApp.libs.API
{

    // struc class gibi value taypeların bir kapsayıcısı sadece içerisinde valuetype değerler kullanılıar. class kullanamayız.

    public struct ValidThru {
        public int Year { get; set; } // 90 - 22
        public int Month { get; set; } // 0-12
    }


    // card expire olmamış kullanılabilen bir kart mı ?
    public interface ICreditValidator
    {
         bool IsValid(ValidThru validThru);

    }
}