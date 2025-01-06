using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using Reactive.Bindings.Notifiers;
using SqLiteSample.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;

namespace SqLiteSample.Platform.WPF.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly SampleDBAccess _dbAccess = new();

        public ReactiveCommandSlim GetSQLiteVersion { get; } = new();

        public ReactivePropertySlim<string> LogMessage { get; } = new();

        public MainWindowViewModel()
        {
            GetSQLiteVersion
                .Subscribe(ExeGetSQLiteVersion)
                .AddTo(Disposables);
        }

        ~MainWindowViewModel()
        {
            Dispose();
        }

        private void ExeGetSQLiteVersion()
        {
            var version = _dbAccess.GetSQLiteVersion();
            LogMessage.Value = version;
        }
    }
}
