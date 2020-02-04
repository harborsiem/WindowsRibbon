using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace RibbonLib
{
    public sealed class RibbonColors
    {
        internal RibbonColors(Color background, Color highlight, Color text)
        {
            BackgroundColor = background;
            HighlightColor = highlight;
            TextColor = text;
        }

        public Color BackgroundColor { get; private set; }
        public Color HighlightColor { get; private set; }
        public Color TextColor { get; private set; }
    }
}
