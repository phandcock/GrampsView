//-----------------------------------------------------------------------
//
// <copyright file="DuplicateKeyException.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//
// Include three constructors in custom exception classes
// Use at least the three common constructors when creating your own exception classes: the default constructor, a constructor that takes a string message, and a constructor that takes a string message and an inner exception.
// Exception(), which uses default values.
// Exception(String), which accepts a string message.
// Exception(String, Exception), which accepts a string message and an inner exception.
// For an example, see How to: Create User-Defined Exceptions.
//-----------------------------------------------------------------------

namespace GrampsView.Exceptions
{
    using System;

    /// <summary>
    /// </summary>
    public class DuplicateKeyException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateKeyException"/> class.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        public DuplicateKeyException(string key)
        : base("Attempted to insert duplicate key " + key + " in collection")
        {
            Key = key;
        }

        public DuplicateKeyException()
        {
        }

        public DuplicateKeyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public string Key
        {
            get; private set;
        }
    }
}