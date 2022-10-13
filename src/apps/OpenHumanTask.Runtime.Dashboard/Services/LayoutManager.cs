using Microsoft.AspNetCore.Components;

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

        private LeftMenu? _leftMenu;
        /// <inheritdoc/>
        public LeftMenu? LeftMenu
        {
            get => this._leftMenu;
            set
            {
                if (this._leftMenu == value) return;
                this._leftMenu = value;
                this.UpdateHeader();
            }
        }

        private Footer? _footer;
        /// <inheritdoc/>
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
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Header)));
        }

        public void UpdateLeftMenu()
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.LeftMenu)));
        }

        public void UpdateFooter()
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Footer)));
        }

    }

}
