namespace Basket.API.Entities
{
    public class ShoppingCart
    {
        public string UserName { get; set; }
        public List<ShoppigCartItem> Items { get; set; } = new List<ShoppigCartItem>();

        public ShoppingCart() { }

        public ShoppingCart(string userName)
        {
            UserName = userName;
        }

        public decimal TotalPrice
        {
            get
            {
                decimal totalprice = 0;
                foreach (var item in Items) 
                {
                totalprice += item.Price * item.Quantity;}
                return totalprice;
            }
        }
    }
}
