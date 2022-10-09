namespace OpenHumanTask.Runtime.Dashboard
{

    /// <summary>
    /// Exposes the application well known breadcrumbs
    /// </summary>
    public static class Breadcrumbs
    {

        /// <summary>
        /// Gets an <see cref="IEnumerable{T}"/> containing the 'Home' breadcrumb chain
        /// </summary>
        public static IEnumerable<IBreadcrumbItem> Home = new List<IBreadcrumbItem>() { new BreadcrumbItem("Home", "/", "bi-house") };
        /// <summary>
        /// Gets an <see cref="IEnumerable{T}"/> containing the 'Task Definitions' breadcrumb chain
        /// </summary>
        public static IEnumerable<IBreadcrumbItem> TaskDefinitions = new List<IBreadcrumbItem>(Home) { new BreadcrumbItem("Task Definitions", "/tasks/definitions", "bi-file-code") };

    }
}
