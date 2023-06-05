namespace AspnetRunBasics.Models
{
    public class BasketModel
    {
        public string UserName { get; set; }
        public List<BasketItemModel> Items { get; set; } = new List<BasketItemModel>(); 
        public int TotalPrice { get; set; }
    }
}
