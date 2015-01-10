using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FluentlyExport
{
    public static class Take<T>
    {
        private static IFluentExportConfiguration<T> ConfigurationFactory(T item)
        {
            return new DefaultFluentExportConfiguration<T>(item);
        }

        public static IFluentExportConfiguration<T> This(T item)
        {
            var config = ConfigurationFactory(item);
            if (_transferConfigurationFactory != null)
                config.TransferConfigurationFactory = _transferConfigurationFactory(item);
            return config;
        }

        public static IFluentExportObjectTransferConfiguration<T> ThisObject(T item)
        {
            throw new NotImplementedException();
        } 

        private static Func<T, Func<IExportLocation, IFluentExportTransferConfiguration<T>>> _transferConfigurationFactory;

        public static Func<T, Func<IExportLocation, IFluentExportTransferConfiguration<T>>> TransferConfigurationFactory
        {
            get { return _transferConfigurationFactory; }
            set { _transferConfigurationFactory = value; }
        }
    }
}
