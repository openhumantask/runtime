/*
 * Copyright © 2022-Present The Synapse Authors
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */

using Microsoft.AspNetCore.Components;
using System.Runtime.CompilerServices;

namespace OpenHumanTask.Runtime.Dashboard.Services
{

    /// <summary>
    /// The service used to manage the breadcrumb
    /// </summary>
    public class BreadcrumbManager        
        : IBreadcrumbManager, INotifyPropertyChanged
    {

        /// <summary>
        /// Notifies when the list has changed
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// The list of displayed <see cref="IBreadcrumbItem"/>
        /// </summary>
        public List<IBreadcrumbItem> Items { get; init; }

        /// <summary>
        /// The <see cref="NavigationManager"/> service
        /// </summary>
        protected NavigationManager NavigationManager { get; init; }

        /// <summary>
        /// Initializes a new <see cref="BreadcrumbManager"/>
        /// </summary>
        /// <param name="navigationManager"></param>
        public BreadcrumbManager(NavigationManager navigationManager)
        {
            this.NavigationManager = navigationManager;
            this.Items = new List<IBreadcrumbItem>(Breadcrumbs.Home);
        }

        /// <summary>
        /// Adds the specified <see cref="IBreadcrumbItem"/> to the list
        /// </summary>
        /// <param name="breadcrumbItem"></param>
        /// <returns></returns>
        public void AddItem(IBreadcrumbItem breadcrumbItem)
        {
            if (!this.Items.Contains(breadcrumbItem)) this.Items.Add(breadcrumbItem);
            this.OnChange();
        }

        /// <summary>
        /// Creates a new <see cref="IBreadcrumbItem"/> with the specified label and icon for the active route and adds it to the list
        /// </summary>
        /// <param name="label"></param>
        /// <param name="icon"></param>
        /// <returns>The created  <see cref="IBreadcrumbItem"/></returns>
        public IBreadcrumbItem AddCurrentUri(string label, string? icon = null)
        {
            var item = new BreadcrumbItem(label, this.NavigationManager.Uri, icon);
            this.Items.Add(item);
            this.OnChange();
            return item;
        }

        /// <summary>
        /// Adds the specified <see cref="IBreadcrumbItem"/> to the list
        /// </summary>
        /// <param name="breadcrumbItem"></param>
        /// <returns></returns>
        public void RemoveItem(IBreadcrumbItem breadcrumbItem)
        {
            if (this.Items.Contains(breadcrumbItem)) this.Items.Remove(breadcrumbItem);
            this.OnChange();
        }

        /// <summary>
        /// Clears the <see cref="IBreadcrumbItem"/>'s list
        /// </summary>
        /// <returns></returns>
        public void Clear()
        {
            this.Items?.Clear();
            this.OnChange();
        }

        /// <summary>
        /// Replaces the current <see cref="IBreadcrumbItem"/>'s list with the provided one
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public void Use(IEnumerable<IBreadcrumbItem> list)
        {
            this.Clear();
            list.ToList().ForEach(i => this.AddItem(i));
        }

        ///<inheritdoc/>
        public void NavigateTo(IBreadcrumbItem breadcrumbItem)
        {
            var itemIndex = this.Items.IndexOf(breadcrumbItem);
            var newState = new List<IBreadcrumbItem>(this.Items.Take(itemIndex + 1));
            this.Use(newState);
            this.NavigationManager.NavigateTo(breadcrumbItem.Link);
        }

        /// <summary>
        /// Notifies a change
        /// </summary>
        protected void OnChange([CallerMemberName] string propertyName = "Items")
        {
            if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    
    }

}
