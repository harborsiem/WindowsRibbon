using System;
using System.Drawing;
using RibbonLib;
using RibbonLib.Controls;

namespace RibbonLib.Controls
{
    partial class RibbonItems
    {
        public void Init()
        {
        }
		
		public void Load()
        {
            // set ribbon colors
            Ribbon.SetColors(Color.Wheat, Color.IndianRed, Color.BlueViolet);
        }

    }
}
