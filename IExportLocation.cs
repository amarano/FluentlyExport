namespace FluentlyExport
{
    public interface IExportLocation
    {
        
    }

    public interface ISerializedExportLocation : IExportLocation
    {
        void DoExport(byte[] bytes);
    }

    public interface IObjectExportLocation<in T> : IExportLocation
    {
        void DoExport(T t);
    }
}