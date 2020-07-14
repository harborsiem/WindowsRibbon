using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace RibbonLib
{
    /// <summary>
    /// Class for the Ribbon colors
    /// </summary>
    public sealed class RibbonColors
    {
        internal RibbonColors(Color background, Color highlight, Color text)
        {
            BackgroundColor = background;
            HighlightColor = highlight;
            TextColor = text;
        }

        /// <summary>
        /// The Background Color
        /// </summary>
        public Color BackgroundColor { get; private set; }
        /// <summary>
        /// The Highlight Color
        /// </summary>
        public Color HighlightColor { get; private set; }
        /// <summary>
        /// The Text Color
        /// </summary>
        public Color TextColor { get; private set; }
    }
}
