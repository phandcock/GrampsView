// <copyright file="MenuItemModel.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

/// <summary>
/// Menu item for NavigationView
/// </summary>
namespace GrampsView.Data.Model
{
    /// <summary>
    /// Menu item for NavigationView.
    /// </summary>
    public sealed class MenuItemModel
    {
        /// <summary>
        /// Gets or sets the symbol for the icon.
        /// </summary>
        /// <value>
        /// The icon.
        /// </value>
        public string Icon
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the menu item text.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the type of the page the menuitem navigates to.
        /// </summary>
        /// <value>
        /// The type of the page.
        /// </value>
        public string PageType
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the menu item tool tip.
        /// </summary>
        /// <value>
        /// The tool tip.
        /// </value>
        public string ToolTip
        {
            get; set;
        }
    }
}