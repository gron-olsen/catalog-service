namespace catalogServiceAPI.Models;

public class AuctionProduct
{
    public int ProductID { get; set; }
    public int ProductStartPrice  { get; set; }
    public DateTime ProductEndDate { get; set; }

    public AuctionProduct(Product product)
    {
        ProductID = product.ProductID;
        ProductStartPrice = product.ProductStartPrice;
        ProductEndDate = product.ProductEndDate;
    }
    public AuctionProduct()
    {}
}