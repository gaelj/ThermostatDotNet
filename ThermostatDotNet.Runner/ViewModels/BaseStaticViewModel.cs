using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace ThermostatDotNet.Runner.ViewModels
{
    public class BaseStaticViewModel : INotifyPropertyChanged, IDisposable
    {
        private bool disposedValue;

        public BaseStaticViewModel()
        {
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Try to run an async function, show a message in case of exception
        /// </summary>
        /// <param name="result"></param>
        /// <param name="asyncFunction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected async Task<(bool result, T outputValue)> TryExecuteAsync<T>(Func<Task<T>> asyncFunction, bool updateLoaders=true)
        {
            var result = false;
            var outputValue = default(T);
            try {
                if (updateLoaders)
                    IncrementAsyncLoadersCount();
                outputValue = await asyncFunction.Invoke().ConfigureAwait(true);
                result = true;
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message + "\n" + ex.InnerException);
            }
            finally {
                if (updateLoaders)
                    DecrementAsyncLoadersCount();
            }
            return (result, outputValue);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notify that a property has changed
        /// </summary>
        /// <param name="key">Description of the updated field (default is caller name)</param>
        protected void NotifyPropertyChanged([CallerMemberName] string key = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(key));
        }


        /// <summary>
        /// To show a spinner while data is loading, and ensure components are displayed only when all of their data are available
        /// </summary>
        /// <value></value>
        public bool IsBusy => AsyncLoadersCount > 0;

        private static int asyncLoadersCount { get; set; } = 0;
        public int AsyncLoadersCount {
            get {
                lock (AsyncLoadersCountLock)
                    return asyncLoadersCount;
            }
        }

        public void IncrementAsyncLoadersCount()
        {
            lock (AsyncLoadersCountLock) {
                asyncLoadersCount++;
                if (asyncLoadersCount == 1) {
                    Task.Run(() => {
                        NotifyPropertyChanged(nameof(IsBusy));
                        NotifyPropertyChanged(nameof(AsyncLoadersCount));
                    });
                }
            }
        }

        public void DecrementAsyncLoadersCount()
        {
            lock (AsyncLoadersCountLock) {
                asyncLoadersCount--;
                if (asyncLoadersCount == 0) {
                    Task.Run(() => {
                        NotifyPropertyChanged(nameof(IsBusy));
                        NotifyPropertyChanged(nameof(AsyncLoadersCount));
                    });
                }
            }
        }

        private static object AsyncLoadersCountLock = new object();
    }
}