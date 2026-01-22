namespace microservicesSales.Domain
{
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message) { }
    }

    public class InsufficientStockException : DomainException
    {
        public Guid ProductId { get; }
        public int Available { get; }
        public int Requested { get; }

        public InsufficientStockException(Guid productId, int available, int requested)
            : base($"Stock insuficiente. ProductId={productId}, Disponible={available}, Solicitado={requested}")
        {
            ProductId = productId;
            Available = available;
            Requested = requested;
        }

      
    }
}
