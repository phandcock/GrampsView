// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.Data.StoreSerial
{
    /// <summary>
    /// Interface definitions for IGrampsRepository.
    /// </summary>
    public interface IGrampsStoreSerial
    {
        /// <summary>
        /// Des the serialize repository.
        /// </summary>
        Task DeSerializeRepository();

        Task SerializeRepository();

        //Task<object> DeSerializeRMD(object argObject);

        /// <summary>
        /// Serializes the object.
        /// </summary>
        /// <param name="theObject">
        /// The object.
        /// </param>
        /// <returns>
        /// </returns>
        //Task SerializeRMD(object argObject);
    }
}