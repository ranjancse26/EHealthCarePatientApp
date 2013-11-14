using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHealthCareDesktopApp.Helpers
{
    public sealed class Singleton<T> where T : class, new()
    {
        private static volatile Singleton<T> instance;
        private static object syncRoot = new Object();

        private Singleton() { }

        public static Singleton<T> Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new Singleton<T>();
                    }
                }

                return instance;
            }
        }
    }
}
