using System;
using System.IO;
using System.Reactive.Linq;

namespace Reactive.FileSystem
{
    public class FileSystemObservable : IDisposable
    {
        readonly FileSystemWatcher watcher;

        public FileSystemObservable(string path)
        {
            watcher = new FileSystemWatcher(path);
        }

        public IObservable<string> Changed
        {
            get
            {
                var obs = Observable.FromEventPattern<FileSystemEventHandler, FileSystemEventArgs>(
                h => watcher.Changed += h,
                h => watcher.Changed -= h);

                return obs.Select(x => x.EventArgs.FullPath);
            }
        }

        bool disposed = false;
        public void Dispose()
        {
            if (disposed) return;
            
            watcher.Dispose();
            disposed = true;
        }
    }
}
