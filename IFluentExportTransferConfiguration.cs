namespace FluentlyExport
{
    public interface IFluentExportTransferConfiguration<T>
    {
        void Please();
    }

    public interface IFluentExportSerializedTransferConfiguration<T> : IFluentExportTransferConfiguration<T>
    {
        IFluentExportSerializedTransferConfiguration<T> SerializeItUsing(ISerializer<T> serializer);
        IFluentExportTransferConfiguration<T> AndAlso(ISerializedExportLocation location);
    }

    public interface IFluentExportObjectTransferConfiguration<T> : IFluentExportTransferConfiguration<T>
    {
        IFluentExportTransferConfiguration<T> AndAlso(IObjectExportLocation<T> location);
    }
}