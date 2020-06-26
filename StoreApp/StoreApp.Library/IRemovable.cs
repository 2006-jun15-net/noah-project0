using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library
{
    interface IRemovable<T>
    {
        void Remove(T entity);
    }
}
