using Texon.Shared.AnaliyticsVeiwModel;

namespace Texon.Service.Abstraction.IService
{
    public interface IAnaliytics
    {
        Task<AnaliyticsVeiwModel>  GetAnaliyticsData();

    }
}
