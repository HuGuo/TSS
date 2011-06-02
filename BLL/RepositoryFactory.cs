using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSS.BLL
{
    public class RepositoryFactory<T> where T : new()
    {
        private static readonly T instance = new T();

        public static T Get()
        {
            return instance;
        }
    }
}
