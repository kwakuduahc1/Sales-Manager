namespace SalesManager.Models
{
    public interface IAppFeatures
    {
        string AppName { get; }

        string Key { get; }

        string Audience { get; }

        string Issuer { get; }

        System.DateTime Expiry { get; }
    }
}