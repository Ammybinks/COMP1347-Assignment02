using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace XNAMachinationisRatio.Resource {
    /// <summary>
    /// A library of asset
    /// </summary>
    public class AssetLibrary {
        /// <summary>
        /// Asset map : key String id
        /// </summary>
        IDictionary<String, GenericAsset> mAssetMap;
        
        protected AssetLibrary() {
            mAssetMap = new Dictionary<String, GenericAsset>();
        }
        /// <summary>
        /// Create an empty asset library
        /// </summary>
        /// <returns>Empty asset library</returns>
        public static AssetLibrary CreateAnEmptyLibrary() {
            return new AssetLibrary();
        }
        /// <summary>
        /// Import an asset in the library
        /// </summary>
        /// <param name="pAsset">Asset to import</param>
        public void ImportAsset(GenericAsset pAsset) {
            Debug.Assert(pAsset != null, "Asset to import shall not be null");
            mAssetMap.Add(pAsset.ID, pAsset);
        }
        /// <summary>
        /// Get an asset by Id
        /// </summary>
        /// <param name="pAssetID">Id of the asset</param>
        /// <returns></returns>
        public GenericAsset GetAsset(String pAssetID) {
            Debug.Assert(pAssetID != null, "Asset ID shall not be null");
            Debug.Assert(!pAssetID.Equals(""),"Asset ID shall be different from void string");
            GenericAsset asset;
            mAssetMap.TryGetValue(pAssetID,out asset);
            return asset;
        }
    }
}
