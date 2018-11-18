using System;
using System.Collections.Generic;
using System.Text;

namespace AutoHome.Training.Core
{
    public abstract class DisposableObject : IDisposable
    {
        #region Finalization Constructs 终结期释放资源
        /// <summary>
        /// Finalizes the object.
        /// </summary>
        ~DisposableObject()
        {
            this.Dispose(false);
        }
        #endregion

        #region Protected Methods 用于子类实现具体Dispose
        /// <summary>
        /// Disposes the object.
        /// </summary>
        /// <param name="disposing">A <see cref="System.Boolean"/> value which indicates whether
        /// the object should be disposed explicitly.</param>
        protected abstract void Dispose(bool disposing);
        #endregion

        #region 接口实现方法
        /// <summary>
        /// Provides the facility that disposes the object in an explicit manner,
        /// preventing the Finalizer from being called after the object has been
        /// disposed explicitly.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        /// <summary>
        /// 符合规范，例如C++
        /// </summary>
        public void Close()
        {
            Dispose();
        }
    }
}
