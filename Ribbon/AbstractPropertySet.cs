//*****************************************************************************
//
//  File:       GalleryItemPropertySet.cs
//
//  Contents:   Helper class that wraps a gallery item simple property set.
//
//*****************************************************************************

using System.Diagnostics;
using RibbonLib.Interop;
//using Windows.Win32;
//using Windows.Win32.Foundation;
//using Windows.Win32.UI.Ribbon;
//using Windows.Win32.UI.Shell.PropertiesSystem;
//using Windows.Win32.System.Com.StructuredStorage;

namespace RibbonLib
{
    /// <summary>
    /// Helper abstract class that wraps a gallery item or command IUISimplePropertySet.
    /// This abstract class is a constrain for UICollection of T
    /// </summary>
    public abstract class AbstractPropertySet : IUISimplePropertySet
    {
        /// <summary>
        /// Retrieves the stored value of a given property
        /// </summary>
        /// <param name="key">The Property Key of interest.</param>
        /// <param name="value">When this method returns, contains a pointer to the value for key.</param>
        /// <returns></returns>
        public abstract HRESULT GetValueImpl(ref PropertyKey key, out PropVariant value);
		
        #region IUISimplePropertySet Members

        /// <summary>
        /// Retrieves the stored value of a given property
        /// </summary>
        /// <param name="key">The Property Key of interest.</param>
        /// <param name="value">When this method returns, contains a pointer to the value for key.</param>
        /// <returns></returns>
        public HRESULT GetValue(ref PropertyKey key, out PropVariant value)
        {
            return GetValueImpl(ref key, out value);
        }

        #endregion
    }
}
