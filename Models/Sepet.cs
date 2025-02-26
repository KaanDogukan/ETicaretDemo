namespace ETicaretDemo.Models
{
    public class Sepet
    {
        public int Id { get; set; }
        public string UrunAdi { get; set; }
        public int Adet { get; set; }
        public decimal BirimFiyati { get; set; }
        public decimal ToplamTutar { get; set; }
    }
}
