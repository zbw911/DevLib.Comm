using System;
using System.Threading;

namespace Dev.Comm.ThreadTool
{
    /// <summary>
    /// 读写锁，使用的时候
    /// 
    ///    ReaderWriterLock locker = new ReaderWriterLock();
    /// 
    ///    using(locker.ReadLock())
    ///    {
    ///             read....
    ///     }
    /// 
    /// </summary>
    public class ReaderWriterLock
    {
        private readonly ReaderWriterLockSlim _locker = new ReaderWriterLockSlim();


        /// <summary>
        /// 读锁
        /// </summary>
        /// <returns></returns>
        public IDisposable ReadLock()
        {
            return new ReaderLock(_locker);
        }

        /// <summary>
        /// 写锁
        /// </summary>
        /// <returns></returns>
        public IDisposable WriteLock()
        {
            return new WriterLock(_locker);
        }


        private class ReaderLock : IDisposable
        {
            private readonly ReaderWriterLockSlim _locker;

            public ReaderLock(ReaderWriterLockSlim locker)
            {
                _locker = locker;
                _locker.EnterReadLock();
            }

            public void Dispose()
            {
                _locker.ExitReadLock();
            }
        }

        private class WriterLock : IDisposable
        {
            private readonly ReaderWriterLockSlim _locker;

            public WriterLock(ReaderWriterLockSlim locker)
            {
                _locker = locker;
                _locker.EnterWriteLock();
            }

            public void Dispose()
            {
                _locker.ExitWriteLock();
            }
        }
    }
}
