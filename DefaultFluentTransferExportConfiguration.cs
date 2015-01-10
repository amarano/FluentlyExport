using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentlyExport
{

    public class DefaultFluentObjectTransferExportConfiguration<T> : IFluentExportObjectTransferConfiguration<T>
    {
        internal readonly IList<IObjectExportLocation<T>> Locations;
        internal readonly T Item;

        public void Please()
        {
            foreach (var location in Locations)
            {
                location.DoExport(Item);
            }
        }

        public DefaultFluentObjectTransferExportConfiguration(IObjectExportLocation<T> location, T item )
        {
            Item = item;
            Locations = new List<IObjectExportLocation<T>> { location };
        }

        public DefaultFluentObjectTransferExportConfiguration(IEnumerable<IObjectExportLocation<T>> locations, T item )
        {
            Locations = locations.ToList();
            Item = item;
        }

        public IFluentExportTransferConfiguration<T> AndAlso(IObjectExportLocation<T> location)
        {
            var newLocations = Locations.Concat(new List<IObjectExportLocation<T>> { location });
            return new DefaultFluentObjectTransferExportConfiguration<T>(newLocations, Item);
        }
    }

    public class DefaultFluentSerializedTransferExportConfiguration<T> : IFluentExportSerializedTransferConfiguration<T>
    {
        internal readonly IList<ISerializedExportLocation> Locations;
        internal readonly T Item;
        private ISerializer<T> _serializer;

        internal ISerializer<T> Serializer
        {
            get
            {
                if (_serializer == null)
                    _serializer = new DefaultXmlSerializer<T>();
                return _serializer;
            }
            set { _serializer = value; }
        }

        internal DefaultFluentSerializedTransferExportConfiguration(ISerializedExportLocation location, T item )
        {
            Locations = new List<ISerializedExportLocation> {location};
            Item = item;
        }

        internal DefaultFluentSerializedTransferExportConfiguration(IEnumerable<ISerializedExportLocation> locations, T item)
        {
            Locations = locations.ToList();
            Item = item;
        }

        public void Please()
        {
            foreach (var location in Locations)
            {
                location.DoExport(Serializer.Serialize(Item));
            }
        }

        public IFluentExportTransferConfiguration<T> AndAlso(ISerializedExportLocation location)
        {
            var newLocations = Locations.Concat(new List<ISerializedExportLocation> {location});
            return new DefaultFluentSerializedTransferExportConfiguration<T>(newLocations, Item)
            {
                Serializer = Serializer
            };
        }

        public IFluentExportSerializedTransferConfiguration<T> SerializeItUsing(ISerializer<T> serializer)
        {
            Serializer = serializer;
            return this;
        }
    }
}