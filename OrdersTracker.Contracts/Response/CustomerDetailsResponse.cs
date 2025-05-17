namespace OrdersTracker.Contracts.Response
{
    public class CustomerDetailsResponse
    {
        public required string CustomerID { get; init; }
        public string CompanyName { get; init; }
        public required string ContactName { get; init; }
        public string ContactTitle { get; init; }
        public string Address { get; init; }
        public string City { get; init; }
        public string Region { get; init; }
        public string PostalCode { get; init; }
        public string Country { get; init; }
        public string Phone { get; init; }
        public string Fax { get; init; }
    }
}
