using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;

namespace SqLiteSample.Platform.WPF.ViewModels
{
    public class ViewModelBase
    {
        protected CompositeDisposable Disposables { get; } = [];

        public void Dispose() => Disposables.Dispose();
    }
}
