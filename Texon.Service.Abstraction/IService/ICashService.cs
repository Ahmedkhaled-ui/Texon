namespace Texon.Service.Abstraction.IService
{
    public interface ICashService
    {
        Task<string?> GetCashAsync(string key);
        Task SetCashAsync(string key, object value, TimeSpan timeToLive);
    }
}
