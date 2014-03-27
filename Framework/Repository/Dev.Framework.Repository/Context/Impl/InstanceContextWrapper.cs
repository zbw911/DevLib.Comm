// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 13:28
// 
// 修改于：2013年02月18日 18:24
// 文件名：InstanceContextWrapper.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
 

using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading;

namespace Kt.Framework.Repository.Context.Impl
{
    /// <summary>
    /// Default implementation of <see cref="IInstanceContext"/>
    /// </summary>
    public class InstanceContextWrapper : IInstanceContext
    {
        readonly InstanceContext _context;

        /// <summary>
        /// Default Constructor.
        /// Creates a new instance of the <see cref="InstanceContextWrapper"/> class.
        /// </summary>
        /// <param name="context">The <see cref="InstanceContext"/> instance to wrap.</param>
        public InstanceContextWrapper(InstanceContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets the underlying <see cref="IExtensionCollection{T}"/> from the underlying
        /// <see cref="InstanceContext"/>.
        /// </summary>
        public IExtensionCollection<InstanceContext> Extensions
        {
            get { return _context.Extensions; }
        }

        /// <summary>
        /// Gets a <see cref="IServiceHost"/> instance that wraps the underlying <see cref="ServiceHost"/>
        /// from the InstanceContxt.
        /// </summary>
        public IServiceHost Host
        {
            get { return new ServiceHostWrapper(_context.Host); }
        }

        /// <summary>
        /// Gets a <see cref="ICollection{T}"/> instance containing a list of incoming channels
        /// from the wrapped <see cref="InstanceContext"/>.
        /// </summary>
        public ICollection<IChannel> IncomingChannels
        {
            get { return _context.IncomingChannels; }
        }

        /// <summary>
        /// Gets or sets the manual flow control limit on the wrapped <see cref="InstanceContext"/>.
        /// </summary>
        public int ManualFlowControlLimit
        {
            get { return _context.ManualFlowControlLimit; }
            set { _context.ManualFlowControlLimit = value; }
        }

        /// <summary>
        /// Gets a <see cref="ICollection{T}"/> instance containing a list of outgoing channels
        /// from the wrapped <see cref="InstanceContext"/>.
        /// </summary>
        public ICollection<IChannel> OutgoinChannels
        {
            get { return _context.OutgoingChannels; }
        }

        /// <summary>
        /// Gets the <see cref="SynchronizationContext"/> from the wrapped <see cref="InstanceContext"/>.
        /// </summary>
        public SynchronizationContext SynchronizationContext
        {
            get { return _context.SynchronizationContext; }
            set { _context.SynchronizationContext = value; }
        }

        /// <summary>
        /// Increments the manual control flow limit
        /// </summary>
        /// <param name="limit">int. The flow control limit to increment to.</param>
        public void IncrementManualFlowControlLimit(int limit)
        {
            _context.IncrementManualFlowControlLimit(limit);
        }

        /// <summary>
        /// Gets the service instance.
        /// </summary>
        /// <returns>object.</returns>
        public object GetServiceInstance()
        {
            return _context.GetServiceInstance();
        }

        /// <summary>
        /// Gets the service instance for the specified <see cref="Message"/>.
        /// </summary>
        /// <param name="message">A <see cref="Message"/> instance.</param>
        /// <returns>object.</returns>
        public object GetServiceInstance(Message message)
        {
            return _context.GetServiceInstance(message);
        }
    }
}