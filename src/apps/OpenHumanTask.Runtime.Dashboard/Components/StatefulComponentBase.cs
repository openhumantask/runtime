using Microsoft.AspNetCore.Components;
using System.Reactive.Linq;

namespace OpenHumanTask.Runtime.Dashboard.Components
{

    /// <summary>
    /// Represents the base class for all <see cref="ComponentBase"/>s using a Flux feature/state.
    /// </summary>
    /// <typeparam name="TState">The type of Flux state managed by the <see cref="StatefulComponentBase{TState}"/></typeparam>
    public abstract class StatefulComponentBase<TState>
        : ComponentBase, IDisposable
    {

        private bool _Disposed;
        private IDisposable? _Subscription;

        /// <summary>
        /// Gets the current <see cref="IDispatcher"/>
        /// </summary>
        [Inject]
        public IDispatcher Dispatcher { get; set; } = null!;

        /// <summary>
        /// Gets the current <see cref="IStore"/>
        /// </summary>
        [Inject]
        public IStore Store { get; set; } = null!;

        /// <summary>
        /// Gets the <see cref="IFeature"/> managed by the <see cref="StatefulComponentBase{TState}"/>
        /// </summary>
        public IFeature<TState> Feature { get; private set; } = null!;

        /// <summary>
        /// Gets the state of the <see cref="StatefulComponentBase{TState}"/>'s <see cref="IFeature"/>
        /// </summary>
        public TState State => this.Feature.State;

        /// <inheritdoc/>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            this.Feature = this.Store.GetFeature<TState>();
            this._Subscription = this.Feature.DistinctUntilChanged().Subscribe(_ => this.StateHasChanged());
        }

        /// <inheritdoc/>
        protected virtual void Dispose(bool disposing)
        {
            if (!this._Disposed)
            {
                if (disposing)
                {
                    this._Subscription?.Dispose();
                    this._Subscription = null;
                }
                this._Disposed = true;
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            this.Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

    }

}
