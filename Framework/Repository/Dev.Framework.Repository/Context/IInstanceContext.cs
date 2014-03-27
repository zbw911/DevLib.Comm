// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 13:28
// 
// 修改于：2013年02月18日 18:24
// 文件名：IInstanceContext.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
 

using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading;

namespace Kt.Framework.Repository.Context
{
    /// <summary>
    /// Interface wrapper that wraps a <see cref="InstanceContext"/> instance.
    /// </summary>
    public interface IInstanceContext
    {
        /// <summary>
        /// Gets the underlying <see cref="IExtensionCollection{T}"/> from the underlying
        /// <see cref="InstanceContext"/>.
        /// </summary>
        IExtensionCollection<InstanceContext> Extensions { get; }
        /// <summary>
        /// Gets a <see cref="IServiceHost"/> instance that wraps the underlying <see cref="ServiceHost"/>
        /// from the InstanceContxt.
        /// </summary>
        IServiceHost Host { get; }
        /// <summary>
        /// Gets a <see cref="ICollection{T}"/> instance containing a list of incoming channels
        /// from the wrapped <see cref="InstanceContext"/>.
        /// </summary>
        ICollection<IChannel> IncomingChannels { get; }
        /// <summary>
        /// Gets or sets the manual flow control limit on the wrapped <see cref="InstanceContext"/>.
        /// </summary>
        int ManualFlowControlLimit { get; set; }
        /// <summary>
        /// Gets a <see cref="ICollection{T}"/> instance containing a list of outgoing channels
        /// from the wrapped <see cref="InstanceContext"/>.
        /// </summary>
        ICollection<IChannel> OutgoinChannels { get; }
        /// <summary>
        /// Gets the <see cref="SynchronizationContext"/> from the wrapped <see cref="InstanceContext"/>.
        /// </summary>
        SynchronizationContext SynchronizationContext { get; set; }
        /// <summary>
        /// Increments the manual control flow limit
        /// </summary>
        /// <param name="limit">int. The flow control limit to increment to.</param>
        void IncrementManualFlowControlLimit(int limit);
        /// <summary>
        /// Gets the service instance.
        /// </summary>
        /// <returns>object.</returns>
        object GetServiceInstance();
        /// <summary>
        /// Gets the service instance for the specified <see cref="Message"/>.
        /// </summary>
        /// <param name="message">A <see cref="Message"/> instance.</param>
        /// <returns>object.</returns>
        object GetServiceInstance(Message message);
    }
}