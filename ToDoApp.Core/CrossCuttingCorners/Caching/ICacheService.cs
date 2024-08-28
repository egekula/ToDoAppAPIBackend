using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Core.CrossCuttingCorners.Caching
{
    public interface ICacheService
    {
        T Get<T>(string key);
        object Get(string key);
        bool isAdd(string key);
        void Add(string key, object value,int duration);
        void Remove(string key);
        void RemoveByPattern(string pattern);
    }
}
