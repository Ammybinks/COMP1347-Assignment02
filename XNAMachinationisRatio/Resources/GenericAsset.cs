using System;
using System.Collections.Generic;

namespace XNAMachinationisRatio.Resource {
    /// <summary>
    /// Generic Asset for the game
    /// </summary>
    public abstract class GenericAsset {
        protected String mAssetName;
        protected String mAssetID;
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="pAssetIdentifier">Asset ID</param>
        /// <param name="pAssetName">Asset Name</param>
        public GenericAsset(String pAssetIdentifier, String pAssetName) {
            mAssetName = pAssetName;
            mAssetID = pAssetIdentifier;
        }
        /// <summary>
        /// Asset Name : shall be a name contained in Content Project
        /// </summary>
        public String Name { get { return mAssetName; } }
        /// <summary>
        /// Asset Id : Asset unique identifier
        /// </summary>
        public String ID { get { return mAssetID; } }
    }
}
