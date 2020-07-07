//-----------------------------------------------------------------------
//
// Various routines used by the App class that are put here to keep the App class cleaner
//
// <copyright file="CommonBindableBase.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
//
// adds serliasation to MVVM BindableBase

namespace GrampsView.Common
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    using Prism.Mvvm;

    /// <summary>
    /// Implementation of <see cref="INotifyPropertyChanged"/> to simplify models.
    /// </summary>
    [DataContract]
    public abstract class CommonBindableBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notifies listeners that a property value has changed.
        /// </summary>
        /// <param name="propertyName">
        /// Name of the property used to notify listeners. This value is optional and can be
        /// provided automatically when invoked from compilers that support <see cref="CallerMemberNameAttribute"/>.
        /// </param>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Raises this object's PropertyChanged event.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the property that has a new value.
        /// </typeparam>
        /// <param name="propertyExpression">
        /// A Lambda expression representing the property that has a new value.
        /// </param>
        protected void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            var propertyName = PropertySupport.ExtractPropertyName(propertyExpression);
            OnPropertyChanged(propertyName);
        }

        /// <summary>
        /// Checks if a property already matches a desired value. Sets the property and notifies
        /// listeners only when necessary.
        /// </summary>
        /// <typeparam name="T">
        /// Type of the property.
        /// </typeparam>
        /// <param name="storage">
        /// Reference to a property with both getter and setter.
        /// </param>
        /// <param name="value">
        /// Desired value for the property.
        /// </param>
        /// <param name="propertyName">
        /// Name of the property used to notify listeners. This value is optional and can be
        /// provided automatically when invoked from compilers that support CallerMemberName.
        /// </param>
        /// <returns>
        /// True if the value was changed, false if the existing value matched the desired value.
        /// </returns>
        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return false;
            }

            storage = value;
            OnPropertyChanged(propertyName);

            return true;
        }

        /// <summary>
        /// Verifies the name of the property.
        /// </summary>
        /// <param name="propertyName">
        /// Name of the property.
        /// </param>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        protected void VerifyPropertyName(string propertyName)
        {
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                Debug.Fail(
                    string.Format(
                        System.Globalization.CultureInfo.CurrentCulture,
                            "Invalid property name. Type: {0}, Name: {1}",
                            GetType(),
                            propertyName));
            }
        }
    }
}