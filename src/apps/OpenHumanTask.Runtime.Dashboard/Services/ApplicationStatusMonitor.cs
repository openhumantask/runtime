using Microsoft.JSInterop;

namespace OpenHumanTask.Runtime.Dashboard.Services
{
    /// <summary>
    /// Represents the default implementation of the <see cref="IApplicationStatusMonitor"/> interface
    /// </summary>
    /// <remarks>Code inspired from https://johnsedlak.com/2022/03/building-an-offline-indicator-in-blazor/</remarks>
    public class ApplicationStatusMonitor
        : IApplicationStatusMonitor
    {

        /// <inheritdoc/>
        public event EventHandler<bool>? OnOnlineStatusChanged;

        private bool _isInitialized;

        /// <summary>
        /// Initializes a new <see cref="ApplicationStatusMonitor"/>
        /// </summary>
        /// <param name="jsRuntime">The service used to interact with JavaScript</param>
        public ApplicationStatusMonitor(IJSRuntime jsRuntime)
        {
            this.JSRuntime = jsRuntime;
            this.Reference = DotNetObjectReference.Create(this);
        }

        /// <summary>
        /// Gets the service used to interact with JavaScript
        /// </summary>
        protected IJSRuntime JSRuntime { get; }

        /// <summary>
        /// Gets the <see cref="ApplicationStatusMonitor"/>'s <see cref="DotNetObjectReference"/>
        /// </summary>
        protected DotNetObjectReference<ApplicationStatusMonitor> Reference { get; }

        /// <summary>
        /// Initializes the <see cref="ApplicationStatusMonitor"/>, if needed
        /// </summary>
        /// <returns>A new awaitable <see cref="Task"/></returns>
        public virtual async Task InitializeAsync()
        {
            if (this._isInitialized) return;
            await this.JSRuntime.InvokeVoidAsync("addOnlineStatusListener", this.Reference);
            this._isInitialized = true;
        }

        /// <summary>
        /// Updates the application's online status
        /// </summary>
        /// <param name="isOnline">A boolean indicating whether or not the application is online</param>
        [JSInvokable]
        public void OnOnlineStatusChange(bool isOnline)
        {
            this.OnOnlineStatusChanged?.Invoke(this, isOnline);
        }

    }

}
