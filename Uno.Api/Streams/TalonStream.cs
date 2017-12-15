using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Uno.Interfaces;
using Uno.ValueObjects;

namespace Uno.Api.Streams
{
    public class TalonStream
    {
        private readonly ITalon talon;
        public TalonStream(ITalon talon)
        {
            this.talon = talon;
        }

        public IObservable<Carte> StreamTalon()
        {
            return Observable.Create((IObserver<Carte> observer) => {            
                talon.CartePosee += carte => 
                {
                    observer.OnNext(carte);
                };
                return Disposable.Empty;
            });
        }
    }
}