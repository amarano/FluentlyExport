using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentlyExport
{
    public interface ISerializer<in T>
    {
        byte[] Serialize(T t);
    }
}
