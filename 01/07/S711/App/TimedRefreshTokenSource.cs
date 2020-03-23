using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace App
{
    public class TimedRefreshTokenSource<TOptions> : IOptionsChangeTokenSource<TOptions>
    {
        private OptionsChangeToken _changeToken;
        public string Name { get; }

        public TimedRefreshTokenSource(TimeSpan interval, string name)
        {
            Name = name ?? Options.DefaultName;
            _changeToken = new OptionsChangeToken();
            ChangeToken.OnChange(() => new CancellationChangeToken(new CancellationTokenSource(interval).Token),
                () =>
                {
                    var previous = Interlocked.Exchange(ref _changeToken, new OptionsChangeToken());
                    previous.OnChange();
                });
        }

        public IChangeToken GetChangeToken() => _changeToken;

        private class OptionsChangeToken : IChangeToken
        {
            private readonly CancellationTokenSource _tokenSource;

            public OptionsChangeToken() => _tokenSource = new CancellationTokenSource();
            public bool HasChanged => _tokenSource.Token.IsCancellationRequested;
            public bool ActiveChangeCallbacks => true;
            public IDisposable RegisterChangeCallback(Action<object> callback, object state) => _tokenSource.Token.Register(callback, state);
            public void OnChange() => _tokenSource.Cancel();
        }
    }
}
