using EMS.Core.Context;

namespace EMS.Core.Factory
{
    public interface IContextFactory
    {
        IDatabaseContext DbContext { get; }
    }
}
