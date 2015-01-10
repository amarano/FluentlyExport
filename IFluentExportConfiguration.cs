using System;

namespace FluentlyExport
{
    public interface IFluentExportConfiguration<T>
    {
        Func<IExportLocation, IFluentExportTransferConfiguration<T>> TransferConfigurationFactory { get; set; }
        
        IFluentExportConfiguration<TNewEntity> MakeItLookLike<TNewEntity>(Func<T, TNewEntity> conversionFunc);
        IFluentExportSerializedTransferConfiguration<T> AndMoveItTo(ISerializedExportLocation location);
        IFluentExportObjectTransferConfiguration<T> AndMoveItTo(IObjectExportLocation<T> location);
    }
}