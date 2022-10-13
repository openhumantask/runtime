namespace OpenHumanTask.Runtime.Dashboard.Services
{

    /// <summary>
    /// Defines the fundamentals of a service used to manage the application's layout
    /// </summary>
    public interface ILayoutManager
       : INotifyPropertyChanged
    {

        /// <summary>
        /// Gets/sets the layout's header
        /// </summary>
        Header? Header { get; set; }

        /// <summary>
        /// Gets/sets the layout's left menu
        /// </summary>
        LeftMenu? LeftMenu { get; set; }

        /// <summary>
        /// Gets/sets the layout's footer
        /// </summary>
        Footer? Footer { get; set; }

        /// <summary>
        /// Updates the header
        /// </summary>
        void UpdateHeader();

        /// <summary>
        /// Updates the left menu
        /// </summary>
        void UpdateLeftMenu();

        /// <summary>
        /// Updates the footer
        /// </summary>
        void UpdateFooter();

    }

}
