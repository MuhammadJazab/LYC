namespace ViewModels.Product
{
    public class ProductVM
    {
        public long? ProductId { get; set; }
        public long? BranchId { get; set; }
        public string ProductNumber { get; set; }
        public string ProductName { get; set; }
        public decimal ProductCost { get; set; }
        public decimal DisplayCost { get; set; }
        public string ProductStatus { get; set; }
        public string ProductImage { get; set; }
    }
}