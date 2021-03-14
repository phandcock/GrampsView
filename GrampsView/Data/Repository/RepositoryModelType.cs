// <copyright file="RepositoryModelType.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GrampsView.Data.Repositories
{
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;
    using GrampsView.Exceptions;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;

    /// <summary>
    /// </summary>
    /// <typeparam name="T1">
    /// Model Base.
    /// </typeparam>
    /// <typeparam name="T2">
    /// HLink Base.
    /// </typeparam>
    /// <seealso cref="GrampsView.Common.CommonBindableBase"/>
    /// /// /// /// /// /// /// ///
    /// <seealso cref="GrampsView.Data.Repositories.IRepositoryModelDictionary{T, U}"/>
    /// /// /// /// /// /// /// ///
    /// <seealso cref="System.ComponentViewModel.INotifyPropertyChanged"/>
    [DataContract]
    public class RepositoryModelDictionary<T1, T2> : Dictionary<string, T1>, IRepositoryModelDictionary<T1, T2>
        where T1 : ModelBase, new()
        where T2 : HLinkBase, new()
    {
        /// <summary>
        /// Initialize a simple random number generator.
        /// </summary>
        private readonly Random localRandomNumberGenerator = new Random();

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryModelDictionary{T, U}"/> class.
        /// </summary>
        public RepositoryModelDictionary()
        {
        }

        public List<T1> GetList
        {
            get
            {
                return Values.ToList();
            }
        }

        /// <summary>
        /// Gets the <see cref="T1"/> with the specified key.
        /// </summary>
        /// <value>
        /// The <see cref="T1"/>.
        /// </value>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// </returns>
        public new T1 this[string key]
        {
            get
            {
                if (string.IsNullOrEmpty(key))
                {
                    DataStore.Instance.CN.NotifyError(new ErrorInfo("Null or empty HLinkKey"));
                }

                //if (key == "_e5bfa72904e68ce059252b501df" || key == "_e5bfa72904e68ce059252b501df")
                //{
                //}

                return Find(key);
            }

            set
            {
                //if (string.IsNullOrEmpty(key))
                //{
                //}

                //if (key == "_e5bfa72904e68ce059252b501df" || key == "_e5bfa72904e68ce059252b501df")
                //{
                //}

                base[key] = value;
            }
        }

        /// <summary>
        /// Gets the <see cref="T1"/> with the specified h link.
        /// </summary>
        /// <value>
        /// The <see cref="T1"/>.
        /// </value>
        /// <param name="hLink">
        /// The h link.
        /// </param>
        /// <returns>
        /// </returns>
        public T1 this[T2 hLink]
        {
            get
            {
                if (string.IsNullOrEmpty(hLink.HLinkKey))
                {
                    DataStore.Instance.CN.NotifyError(new ErrorInfo("Null or empty HLinkKey"));
                }

                if (hLink.HLinkKey == "_e5bfa72904e68ce059252b501df" || hLink.HLinkKey == "_e5bfa72904e68ce059252b501df")
                {
                }

                return Find(hLink.HLinkKey);
            }
        }

        /// <summary>
        /// Adds the specified argument tot he end of the list and update sthe Indexes.
        /// </summary>
        /// <param name="arg">
        /// The argument.
        /// </param>
        public void Add(T1 arg)
        {
            if (string.IsNullOrEmpty(arg.HLinkKey))
            {
                DataStore.Instance.CN.NotifyError(new ErrorInfo("Null or empty HLinkKey"));
            }

            //Update(localItems.Count, arg);

            //if (arg.HLinkKey == "_e5bfa72904e68ce059252b501df" || arg.HLinkKey == "_e5bfa72904e68ce059252b501df")
            //{
            //}

            var key = KeySelector(arg);
            if (base.ContainsKey(key))
            {
                throw new DuplicateKeyException(key);
            }

            base.Add(arg.HLinkKey, arg);
        }

        /// <summary>
        /// Gets or sets the element with the specified key. If setting a new value, new value must
        /// have same key.
        /// </summary>
        /// <param name="key">
        /// Key of element to replace.
        /// </param>
        /// <returns>
        /// </returns>
        public T1 Find(string key)
        {
            if (ContainsKey(key) == true)
            {
                return base[key];
            }
            else
            {
                return new T1();
            }
        }

        /// <summary>
        /// Gets the model from h link.
        /// </summary>
        /// <param name="argHLink">
        /// The argument h link.
        /// </param>
        /// <returns>
        /// </returns>
        public T1 GetModelFromHLink(T2 argHLink)
        {
            if (argHLink is null)
            {
                throw new ArgumentNullException(nameof(argHLink));
            }

            return GetModelFromHLink(argHLink.HLinkKey);
        }

        /// <summary>
        /// Gets the model from h link.
        /// </summary>
        /// <param name="argHLink">
        /// The argument h link.
        /// </param>
        /// <returns>
        /// </returns>
        public T1 GetModelFromHLink(string argHLink)
        {
            T1 tempMO = this.Values.FirstOrDefault(x => x.HLinkKey == argHLink);

            if (tempMO == null)
            {
                return new T1();
            }

            return tempMO;
        }

        /// <summary>
        /// returns a random object from the objects recorded.
        /// </summary>
        public T1 GetRandomItem()
        {
            if (base.Count > 0)
            {
                return base.Values.ElementAt(localRandomNumberGenerator.Next(0, base.Count));
            }
            else
            {
                // return nothing
                return new T1();
            }
        }

        /// <summary>
        /// Keys the selector.
        /// </summary>
        /// <param name="argModel">
        /// The argument.
        /// </param>
        /// <returns>
        /// </returns>
        private string KeySelector(T1 argModel)
        {
            return argModel.HLinkKey;
        }
    }
}