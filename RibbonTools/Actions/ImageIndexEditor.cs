// --------------------------------------------------------------------------
// Description : CDiese Toolkit library
// Author	   : Serge Weinstock
//
//	You are free to use, distribute or modify this code
//	as long as this header is not removed or modified.
// --------------------------------------------------------------------------
using System;
using System.Drawing;
using System.Drawing.Design;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;

namespace WinForms.Design
{
    /// <summary>
    /// The base class for the UITypeEditor for an index in an ImageList.
    /// </summary>
    public abstract class ImageIndexEditorBase : UITypeEditor
    {
        /// <summary>
        /// Indicates whether the specified context supports painting a representation of an object's value within the specified context.
        /// </summary>
        /// <param name="context">An ITypeDescriptorContext that can be used to gain additional context information.</param>
        /// <returns>true if PaintValue is implemented; otherwise, false.</returns>
        public override bool GetPaintValueSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        /// <summary>
        /// Paints a representation of the value of an object using the specified PaintValueEventArgs.
        /// </summary>
        /// <param name="pe">A PaintValueEventArgs that indicates what to paint and where to paint it.</param>
        public override void PaintValue(PaintValueEventArgs pe)
        {
            if (!(pe.Value is int))
            {
                return;
            }
            Image img = GetImage(pe.Context, (int)pe.Value);
            if (img != null)
            {
                pe.Graphics.DrawImage(img, pe.Bounds);
            }
        }
        /// <summary>
        /// Get the image corresponding to the corresponding index. The imageList is determined firstly by looking at property of type ImageList among the instance properties,
        /// secondly by looking at the properties of the "Parent" property of the instance
        /// </summary>
        /// <param name="context">The context of the call</param>
        /// <param name="index">The index of the image</param>
        /// <returns>The corresponding image or null</returns>
        private Image GetImage(ITypeDescriptorContext context, int index)
        {
            ImageList il = GetImageList(context);
            if (il == null || index < 0 || index >= il.Images.Count)
            {
                return null;
            }
            return il.Images[index];
        }
        /// <summary>
        /// Abstract method which allow to find the ImageList for the given context
        /// </summary>
        /// <param name="context">The context of the call</param>
        /// <returns>An ImageList</returns>
        protected abstract ImageList GetImageList(ITypeDescriptorContext context);
    }

    /// <summary>
    /// An UITypeEditor for an index in an ImageList. There is one in System.Windows.Form.Design but unfortunately it's a private class.
    /// </summary>
    public class ImageIndexEditor : ImageIndexEditorBase
    {
        /// <summary>
        /// Finds the ImageList for the given context by looking first at property of type ImageList among the instance properties,
        /// and if the search was unsuccessful by looking at the properties of the "Parent" property of the instance
        /// </summary>
        /// <param name="context">The context of the call</param>
        /// <returns>An ImageList</returns>
        protected override ImageList GetImageList(ITypeDescriptorContext context)
        {
            // try first to find a property of type ImageList among the instance properties
            ImageList il = null;
            PropertyInfo[] props = context.Instance.GetType().GetProperties();
            foreach (PropertyInfo prop in props)
            {
                if (prop.PropertyType == typeof(ImageList) && prop.CanRead)
                {
                    il = (ImageList)prop.GetValue(context.Instance, null);
                    break;
                }
            }
            // if not found, look for a "Parent" property and do the same search on the parent
            if (il == null)
            {
                PropertyInfo parentprop = context.Instance.GetType().GetProperty("Parent");
                if (parentprop != null)
                {
                    object parent = parentprop.GetValue(context.Instance, null);
                    if (parent != null)
                    {
                        props = parent.GetType().GetProperties();
                        foreach (PropertyInfo prop in props)
                        {
                            if (prop.PropertyType == typeof(ImageList) && prop.CanRead)
                            {
                                il = (ImageList)prop.GetValue(parent, null);
                                break;
                            }
                        }
                    }
                }
            }
            return il;
        }
    }
}
