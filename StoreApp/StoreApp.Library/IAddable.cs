using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace StoreApp.Library
{
    interface IAddable<T>
    {
        void Add(T entity);
    }
}
