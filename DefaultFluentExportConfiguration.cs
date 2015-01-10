using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FluentlyExport
{
    public class DefaultFluentExportConfiguration<T> : IFluentExportConfiguration<T>
    {
        internal readonly T Item;

        private Func<IExportLocation, IFluentExportTransferConfiguration<T>> _transferConfigurationFactory;

        public Func<IExportLocation, IFluentExportTransferConfiguration<T>> TransferConfigurationFactory
        {
            get {
                return _transferConfigurationFactory ??
                       (_transferConfigurationFactory =
                           (location) =>
                               new DefaultFluentSerializedTransferExportConfiguration<T>(location as ISerializedExportLocation, Item));
            }
             set { _transferConfigurationFactory = value; }
        }

        internal DefaultFluentExportConfiguration(T item)
        {
            Item = item;
        }

        public IFluentExportConfiguration<TNewEntity> MakeItLookLike<TNewEntity>(Func<T, TNewEntity> conversionFunc)
        {
            var newCollection = conversionFunc(Item);
            return new DefaultFluentExportConfiguration<TNewEntity>(newCollection);
        }

        public IFluentExportSerializedTransferConfiguration<T> AndMoveItTo(ISerializedExportLocation location)
        {

            var transferConfig =
                TransferConfigurationFactory(location) as IFluentExportSerializedTransferConfiguration<T>;
            if (transferConfig == null)
                throw new InvalidOperationException("Factory does not return a serialized transfer configuration");
            return transferConfig;
        }

        public IFluentExportObjectTransferConfiguration<T> AndMoveItTo(IObjectExportLocation<T> location)
        {
            var transferConfig = TransferConfigurationFactory(location) as IFluentExportObjectTransferConfiguration<T>;
            if (transferConfig == null)
                throw new InvalidOperationException("Factory does not return an object transfer configuration");
            return transferConfig;
        }
    }
}