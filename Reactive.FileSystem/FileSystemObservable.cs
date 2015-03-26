using System;
using System.IO;
using System.Reactive.Linq;

namespace Reactive.FileSystem
{
    public class FileSystemObservable : IDisposable
    {
        readonly FileSystemWatcher _watcher;

        public FileSystemObservable(string path)
        {
            _watcher = new FileSystemWatcher(path);
        }

        public IObservable<FileSystemEventArgs> Changed
        {
            get
            {
                var obs = Observable.FromEventPattern<FileSystemEventHandler, FileSystemEventArgs>(
                    h => _watcher.Changed += h,
                    h => _watcher.Changed -= h);

                return obs.Select(x => x.EventArgs);
            }
        }

        bool _disposed;
        public void Dispose()
        {
            if (_disposed) return;
            
            _watcher.Dispose();
            _disposed = true;
        }
    }
}
