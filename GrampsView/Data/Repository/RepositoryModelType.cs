namespace GrampsView.Data.Repositories
{
    using GrampsView.Data.Model;
    using GrampsView.Exceptions;

    using Microsoft.Extensions.DependencyInjection;

    using SharedSharp.Errors;

    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// </summary>
    /// <typeparam name="T1">
    /// Model Base.
    /// </typeparam>
    /// <typeparam name="T2">
    /// HLink Base.
    /// </typeparam>
    /// <seealso cref="GrampsView.Common.ObservableObject"/>
    /// /// /// /// /// /// /// ///
    /// <seealso cref="GrampsView.Data.Repositories.IRepositoryModelDictionary{T, U}"/>
    /// /// /// /// /// /// /// ///
    /// <seealso cref="System.ComponentViewModel.INotifyPropertyChanged"/>

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
                    App.Current.Services.GetService<IErrorNotifications>().NotifyError(new ErrorInfo("Null or empty HLinkKey"));
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
                if (hLink.HLinkKey.Valid)
                {
                    App.Current.Services.GetService<IErrorNotifications>().NotifyError(new ErrorInfo("Null or empty HLinkKey"));
                }

                //if (hLink.HLinkKey.Value == "_e5bfa72904e68ce059252b501df" || hLink.HLinkKey.Value == "_e5bfa72904e68ce059252b501df")
                //{
                //}

                return Find(hLink.HLinkKey.Value);
            }
        }

        /// <summary>
        /// Adds the specified argument to the end of the list and updates the Indexes.
        /// </summary>
        /// <param name="arg">
        /// The model to add.
        /// </param>
        public void Add(T1 arg)
        {
            if (!arg.HLinkKey.Valid)
            {
                App.Current.Services.GetService<IErrorNotifications>().NotifyError(new ErrorInfo("Null or empty HLinkKey"));
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

            base.Add(arg.HLinkKey.Value, arg);
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

            return GetModelFromHLink(argHLink);
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
            T1 tempMO = this.Values.FirstOrDefault(x => x.HLinkKey.Value == argHLink);

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
            return argModel.HLinkKey.Value;
        }
    }
}