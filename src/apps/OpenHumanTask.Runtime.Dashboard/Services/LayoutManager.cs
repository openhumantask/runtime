using Microsoft.AspNetCore.Components;
using System.ComponentModel;

namespace OpenHumanTask.Runtime.Dashboard.Services
{
    /// <summary>
    /// Represents the default implementation of the <see cref="ILayoutManager"/>
    /// </summary>
    public class LayoutManager
        : ILayoutManager
    {

        /// <summary>
        /// Gets the event fired whenever a layout property has changed
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Gets the layout's header <see cref="RenderFragment"/>
        /// </summary>
        public RenderFragment? HeaderFragment => this.Header?.ChildContent;

        private Header? _header;
        /// <summary>
        /// Gets/sets the layout's header
        /// </summary>
        public Header? Header
        {
            get => this._header;
            set
            {
                if (this._header == value) return;
                this._header = value;
                this.UpdateHeader();
            }
        }

        private Footer? _footer;
        /// <summary>
        /// Gets/sets the layout's footer
        /// </summary>
        public Footer? Footer
        {
            get => this._footer;
            set
            {
                if (this._footer == value) return;
                this._footer = value;
                this.UpdateHeader();
            }
        }

        /// <inheritdoc/>
        public void UpdateHeader()
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Header)));
            }
        }

    }

}
