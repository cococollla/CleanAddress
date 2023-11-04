namespace CleanAddress.Dadata.Client
{
    public interface IDadataClient
    {
        Task<CleanAddressDto?> GetStandardizedAddress(string address);
    }
}
