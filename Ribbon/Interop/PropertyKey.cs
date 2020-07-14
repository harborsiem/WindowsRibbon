//****************************************************************************
//
//  File:       PropertyKey.cs
//
//  Contents:   Interop wrapper for native PropertyKey structure. Originally 
//              sourced from http://code.msdn.microsoft.com/PreviewRibbon 
//              project. My modifications:
//                1. Separated PropertyKey definition from Ribbon related code
//                2. Exposed as public fields: FormatId and PropertyId
//
//****************************************************************************

using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace RibbonLib.Interop
{
    /// <summary>
    /// Interop wrapper for native PropertyKey structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PropertyKey
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the PropertyKey
        /// </summary>
        /// <param name="fmtid"></param>
        /// <param name="pid"></param>
        public PropertyKey(Guid fmtid, uint pid)
        {
            this.FormatId = fmtid;
            this.PropertyId = pid;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(PropertyKey left, PropertyKey right)
        {
            return ((left.FormatId == right.FormatId) && (left.PropertyId == right.PropertyId));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(PropertyKey left, PropertyKey right)
        {
            return !(left == right);
        }

        /// <summary>
        /// override ToString()
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "PKey: " + FormatId.ToString() + ":" + PropertyId.ToString(CultureInfo.InvariantCulture.NumberFormat);
        }

        /// <summary>
        /// Return pinned memory to unmanaged code so that it doesn't get freed while unmanaged code still needs it.
        /// </summary>
        /// <returns></returns>
        public IntPtr ToPointer()
        {
            if (!s_pinnedCache.ContainsKey(this))
            {
                s_pinnedCache.Add(this, GCHandle.Alloc(this, GCHandleType.Pinned));
            }

            return s_pinnedCache[this].AddrOfPinnedObject();
        }

        /// <summary>
        /// override Equals
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (!(obj is PropertyKey))
            {
                return false;
            }
            
            return (this == (PropertyKey)obj);
        }

        /// <summary>
        /// override GetHashCode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return (FormatId.GetHashCode() ^ PropertyId.GetHashCode());
        }

        #endregion

        #region Private fields

        // This cache allocates pinned memory for the property key so that it doesn't get garbage-collected
        // while the unmanaged code works with it on the other side of interop.
        // Use the ToPointer() function when calling interop methods which take property keys as parameters.
        static System.Collections.Generic.Dictionary<PropertyKey, GCHandle> s_pinnedCache =
            new System.Collections.Generic.Dictionary<PropertyKey, GCHandle>(16);
         
        /// <summary>
        /// 
        /// </summary>
        public Guid FormatId;

        /// <summary>
        /// 
        /// </summary>
        public uint PropertyId;

        #endregion
    }

    // It is sometimes useful to represent the struct as a reference-type 
    // (eg, for methods that allow passing a null PropertyKey pointer).
    /// <summary>
    /// represent the struct PropertyKey as a reference-type
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class PropertyKeyRef
    {
        /// <summary>
        /// The struct PropertyKey
        /// </summary>
        public PropertyKey PropertyKey;

        /// <summary>
        /// Convert the struct PropertyKey to a reference-type
        /// </summary>
        /// <param name="value"></param>
        /// <returns>The reference-type</returns>
        public static PropertyKeyRef From(PropertyKey value)
        {
            PropertyKeyRef obj = new PropertyKeyRef();
            obj.PropertyKey = value;
            return obj;
        }
    }
}
