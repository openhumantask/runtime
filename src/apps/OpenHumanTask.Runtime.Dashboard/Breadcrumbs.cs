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
        /// Gets an <see cref="IEnumerable{T}"/> containing the 'Human Task Templates' breadcrumb chain
        /// </summary>
        public static IEnumerable<IBreadcrumbItem> HumanTaskTemplates = new List<IBreadcrumbItem>(Home) { new BreadcrumbItem("Task Templates", "/tasks/templates", "bi-file-code") };
        /// <summary>
        /// Gets an <see cref="IEnumerable{T}"/> containing the 'Human Tass' breadcrumb chain
        /// </summary>
        public static IEnumerable<IBreadcrumbItem> HumanTasks = new List<IBreadcrumbItem>(Home) { new BreadcrumbItem("Tasks", "/tasks/instances", "bi-file-code") };

    }

}
