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
    }
}