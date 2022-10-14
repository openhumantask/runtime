namespace OpenHumanTask.Runtime.Dashboard.Services
{

    /// <summary>
    /// Defines the fundamentals of a service used to monitor the status of the Dashboard Progressive Web Application
    /// </summary>
    public interface IApplicationStatusMonitor
    {

        /// <summary>
        /// Represents the event fired whenever the application's online status has changed
        /// </summary>
        event EventHandler<bool>? OnOnlineStatusChanged;

        /// <summary>
        /// Initializes the <see cref="IApplicationStatusMonitor"/>
        /// </summary>
        /// <returns>A new awaitable <see cref="Task"/></returns>
        Task InitializeAsync();

    }

}
