using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.ServiceLocation;
using Ninject;

namespace Dev.CommonServiceLocator.NinjectAdapter
{
    /// <summary>
    /// 初始化
    /// </summary>
    public class AdapterIniter
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="kernel"></param>
        public static void Do(IKernel kernel)
        {
            ServiceLocator.SetLocatorProvider(() => new NinjectServiceLocator(kernel));
        }
    }
}
