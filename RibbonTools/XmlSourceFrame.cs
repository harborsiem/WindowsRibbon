#define xele
//#define xtext
//#define XmlChars

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Diagnostics;

namespace UIRibbonTools
{
    partial class XmlSourceFrame : UserControl
    {
        //Stopwatch watch = new Stopwatch();

        private const int TEXT_FLAGS = NativeMethods.DT_NOPREFIX | NativeMethods.DT_NOCLIP | NativeMethods.DT_SINGLELINE;
        private const int COLOR_FOCUS_RGB = unchecked((int)0xFFFFFFC0);
        private const int COLOR_MARGIN_RGB = unchecked((int)0xffF4F4F4);
        private const int COLOR_MARGIN_TEXT_RGB = unchecked((int)0xff9999CC);
        private const int COLOR_SYMBOL_RGB = unchecked((int)0xff0000FF);
        private const int COLOR_ELEMENT_RGB = unchecked((int)0xffA31515);
        private const int COLOR_ATTRIBUTE_RGB = unchecked((int)0xffFF0000);
        private const int COLOR_CONTENT_RGB = unchecked((int)0xff000000);

        private const int COLOR_FOCUS = 0xC0FFFF;
        private const int COLOR_MARGIN = 0xF4F4F4;
        private const int COLOR_MARGIN_TEXT = 0xCC9999;
        private const int COLOR_SYMBOL = 0xFF0000;
        private const int COLOR_ELEMENT = 0x1515A3;
        private const int COLOR_ATTRIBUTE = 0x0000FF;
        private const int COLOR_CONTENT = 0x000000;

        private TRibbonDocument _document;
        private XDocument _xmlDoc;
        private int _marginWidth;
        private int _spaceWidth;
        private int _equalWidth;
        private int _quoteWidth;
        private int _lessThanSlashWidth;
        private int _greaterThanWidth;
        private int _lineCount;
        private bool _allowExpandCollapse;
        private List<TreeNode> _treeNodes;

#if xtext
        private List<string> _xmlFile;
#endif
        private Pen _marginTextPen = new Pen(Color.FromArgb(COLOR_MARGIN_TEXT_RGB));
        private Brush _marginBrush = new SolidBrush(Color.FromArgb(COLOR_MARGIN_RGB));
        private Brush _focusBrush = new SolidBrush(Color.FromArgb(COLOR_FOCUS_RGB));

        //public static void SetDoubleBuffered(Control control)
        //{
        //    // set instance non-public property with name "DoubleBuffered" to true
        //    typeof(Control).InvokeMember("DoubleBuffered",
        //        BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
        //        null, control, new object[] { true });
        //}

        public XmlSourceFrame()
        {
#if Core
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
#endif
            InitializeComponent();
#if xele
            treeViewXmlSource.NodeMouseClick += TreeViewXmlSourceClick;
            treeViewXmlSource.NodeMouseDoubleClick += TreeViewXmlSourceDblClick;
            treeViewXmlSource.BeforeCollapse += TreeViewXmlSourceCollapsing;
            treeViewXmlSource.BeforeExpand += TreeViewXmlSourceExpanding;

            treeViewXmlSource.DrawMode = TreeViewDrawMode.OwnerDrawAll;
            treeViewXmlSource.DrawNode += TreeViewXmlSourceCustomDrawItemX2;
#endif
        }

        public void SetFonts(Font font)
        {
            this.Font = font;
        }

#if xele
        private TreeNode AddNode(TreeNode parent, XElement element,
            ref int lineNum)
        {
            TreeNode result;
            TreeNode endNode;
            string text = string.Empty;
            if (parent != null)
            {
                result = parent.Nodes.Add(text);
            }
            else
            {
                result = new TreeNode(text);
                _treeNodes.Add(result);
            }
            result.Tag = element;
            result.ImageIndex = lineNum;
            lineNum++;
            if (element.Nodes().Count() > 0)
            {
                bool isXmlElement = false;
                foreach (XNode child in element.Nodes())
                {
                    XElement childElement = child as XElement;
                    if (childElement != null)
                    {
                        isXmlElement = true;
                        AddNode(result, childElement, ref lineNum);
                    }
                }

                if (isXmlElement)
                {
                    text = element.Name.LocalName;
                    if (parent != null)
                    {
                        endNode = parent.Nodes.Add(text);
                    }
                    else
                    {
                        endNode = new TreeNode(text);
                        _treeNodes.Add(endNode);
                    }
                    endNode.ImageIndex = lineNum;
                    lineNum++;
                }
            }
            return result;
        }
#endif

#if xtext
        private TreeNode AddNodeT(TreeNode parent, XElement element,
            ref int lineNum)
        {
            TreeNode result;
            TreeNode endNode;
            string text;
            text = _xmlFile[lineNum - 1];
            if (parent != null)
            {
                result = parent.Nodes.Add(text);
            }
            else
            {
                result = new TreeNode(text);
                _treeNodes.Add(result);
                //treeViewXmlSource.Nodes.Add(result);
            }
            result.Tag = element;
            result.ImageIndex = lineNum;
            lineNum++;
            if (element.Nodes().Count() > 0)
            {
                bool isXmlElement = false;
                foreach (XNode child in element.Nodes())
                {
                    XElement childElement = child as XElement;
                    if (childElement != null)
                    {
                        isXmlElement = true;
                        AddNodeT(result, childElement, ref lineNum);
                    }
                }

                if (isXmlElement)
                {
                    text = _xmlFile[lineNum - 1];
                    if (parent != null)
                    {
                        endNode = parent.Nodes.Add(text);
                    }
                    else
                    {
                        endNode = new TreeNode(text);
                        _treeNodes.Add(endNode);
                    }
                    endNode.Tag = element.Name;
                    endNode.ImageIndex = lineNum;
                    lineNum++;
                }
            }
            return result;
        }
#endif

        public void ClearDocument()
        {
            treeViewXmlSource.Nodes.Clear();
        }

        public void ShowDocument(TRibbonDocument document)
        {
            _document = document;
        }

        public void ActivateFrame()
        {
            _treeNodes = new List<TreeNode>();
            TreeNode root = null;
            int lineNum;
            _marginWidth = -1;
            treeViewXmlSource.Visible = false;
            //watch.Restart();
            treeViewXmlSource.Nodes.Clear();
            treeViewXmlSource.BeginUpdate();
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if ((_document != null) && File.Exists(_document.Filename))
                {
                    _xmlDoc = XDocument.Load(_document.Filename);
#if xtext
                    MemoryStream stream = new MemoryStream();
                    _xmlDoc.Save(stream);
                    stream.Position = 0;

                    StreamReader sr = new StreamReader(stream);
                    _xmlFile = new List<string>();
                    while (!sr.EndOfStream)
                        _xmlFile.Add(sr.ReadLine().Trim());
                    sr.Close();
#endif

                    lineNum = 2;
#if xele
                    root = AddNode(null, _xmlDoc.Root, ref lineNum);
#endif
#if xtext
                    root = AddNodeT(null, _xmlDoc.Root, ref lineNum);
#endif
                    treeViewXmlSource.Nodes.AddRange(_treeNodes.ToArray());
                    _lineCount = lineNum - 1;
                    _allowExpandCollapse = true;
                    treeViewXmlSource.SelectedNode = root;
                    treeViewXmlSource.ExpandAll();
                    root.EnsureVisible();
                }
            }
            finally
            {
                treeViewXmlSource.EndUpdate();
                treeViewXmlSource.Visible = true;
                //watch.Stop();
                treeViewXmlSource.Select();
                _allowExpandCollapse = false;
                this.Cursor = Cursors.Default;
            }
        }

        public void DeactivateFrame()
        {
            //Nothing yet
        }

        private void TreeViewXmlSourceCollapsing(object sender,
            TreeViewCancelEventArgs e)
        {
            e.Cancel = !_allowExpandCollapse;
        }

        private void TreeViewXmlSourceClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode node = e.Node;
            if (node != null)
                treeViewXmlSource.SelectedNode = node;

            if ((node != null) && (node.Nodes.Count > 0) && (e.X < _marginWidth) && (e.X > (_marginWidth - 10)))
            {
                _allowExpandCollapse = true;
                try
                {
                    if (!node.IsExpanded)
                    {
                        node.ExpandAll();
                    }
                    else
                        node.Collapse(true);
                }
                finally
                {
                    _allowExpandCollapse = false;
                }
            }
        }

        private void TreeViewXmlSourceDblClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode node = e.Node;
            if ((node != null) && (node.Nodes.Count > 0))
            {
                _allowExpandCollapse = true;
                try
                {
                    if (!node.IsExpanded)
                    {
                        node.ExpandAll();
                    }
                    else
                        node.Collapse(true);
                }
                finally
                {
                    _allowExpandCollapse = false;
                }
            }
        }

        private void TreeViewXmlSourceExpanding(object sender,
            TreeViewCancelEventArgs e)
        {
            e.Cancel = !_allowExpandCollapse;
        }

        /// <summary>
        /// CustomDraw with the most native functions (DrawText for calculation of text length)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeViewXmlSourceCustomDrawItemX2(object sender, DrawTreeNodeEventArgs e)
        {
            if (e.Bounds.Height < 1 || e.Bounds.Width < 1)
                return;
            TreeView view = (TreeView)sender;
            if (e.Bounds.Y > view.Height)
                return;
            //former code speed up the OwnerDraw TreeView

            NativeMethods.RECT RN, RT, RM;
            IntPtr DC; //HDC
            XElement element;
            string s;
            int i;
            //Font nodeFont;
            Size proposedSize = new Size(int.MaxValue, int.MaxValue);
            //TextFormatFlags flags = TextFormatFlags.NoPrefix | TextFormatFlags.NoPadding;
            //nodeFont = e.Node.NodeFont;
            //if (nodeFont == null) nodeFont = ((TreeView)sender).Font;

            element = e.Node.Tag as XElement;
            DC = e.Graphics.GetHdc();
            NativeMethods.SetBkMode(DC, NativeMethods.TRANSPARENT);

            RM = new NativeMethods.RECT();
            RN = new NativeMethods.RECT(e.Bounds); //Node.DisplayRect(false);
            RT = new NativeMethods.RECT(Rectangle.Inflate(e.Bounds, -e.Node.Bounds.X + 19, 0)); // onlyText
            RT.Right = RN.Right;

            if (_marginWidth < 0)
            {
                s = _lineCount.ToString();
                char[] ch = new char[s.Length];
                for (i = 0; i < s.Length; i++)
                    ch[i] = '0';
                s = new string(ch);

                NativeMethods.DrawText(DC, s, s.Length, ref RM, TEXT_FLAGS | NativeMethods.DT_CALCRECT);
                _marginWidth = RM.Size.Width + 19;
                NativeMethods.DrawText(DC, " ", 1, ref RM, TEXT_FLAGS | NativeMethods.DT_CALCRECT);
                _spaceWidth = RM.Size.Width;
                NativeMethods.DrawText(DC, "=", 1, ref RM, TEXT_FLAGS | NativeMethods.DT_CALCRECT);
                _equalWidth = RM.Size.Width;
                NativeMethods.DrawText(DC, "\"", 1, ref RM, TEXT_FLAGS | NativeMethods.DT_CALCRECT);
                _quoteWidth = RM.Size.Width;
                NativeMethods.DrawText(DC, "</", 2, ref RM, TEXT_FLAGS | NativeMethods.DT_CALCRECT);
                _lessThanSlashWidth = RM.Size.Width;
                NativeMethods.DrawText(DC, ">", 1, ref RM, TEXT_FLAGS | NativeMethods.DT_CALCRECT);
                _greaterThanWidth = RM.Size.Width;
            }
            e.Graphics.ReleaseHdc(DC);

            // Focus rect
            if ((e.State & TreeNodeStates.Focused) != 0)
            {
                e.Graphics.FillRectangle(_focusBrush, RN.ToRectangle());
            }

            // Margin with line numbers
            RN.Right = RN.Left + _marginWidth;
            e.Graphics.FillRectangle(_marginBrush, RN.ToRectangle());
            i = e.Node.ImageIndex;
            if (((i % 10) == 0) || ((e.State & TreeNodeStates.Focused) != 0))
                s = e.Node.ImageIndex.ToString();
            else
                s = "-";

            DC = e.Graphics.GetHdc();
            NativeMethods.SetTextColor(DC, COLOR_MARGIN_TEXT);
            RN.Right -= 18;
            NativeMethods.DrawText(DC, s, s.Length, ref RN, TEXT_FLAGS | NativeMethods.DT_RIGHT);
            e.Graphics.ReleaseHdc(DC);

            // Expand / Collapse markers
            if (e.Node.Nodes.Count > 0)
            {
                e.Node.Checked = true;
                RN.Left = RN.Right + 6;
                RN.Top = RN.Top + (RN.Bottom - RN.Top - 9) / 2;
                RN.Right = RN.Left + 9;
                RN.Bottom = RN.Top + 9;
                e.Graphics.DrawRectangle(_marginTextPen, RN.ToRectangle());
                e.Graphics.DrawLine(_marginTextPen, RN.Left + 2, RN.Top + 4, RN.Left + 7, RN.Top + 4);
                if (!e.Node.IsExpanded)
                {
                    e.Graphics.DrawLine(_marginTextPen, RN.Left + 4, RN.Top + 2, RN.Left + 4, RN.Top + 7);
                }
            }
            else
            {
                RN.Left = RN.Right + 10;
                e.Graphics.DrawLine(_marginTextPen, RN.Left, RN.Top, RN.Left, RN.Bottom);
            }

            // Draw element
            DC = e.Graphics.GetHdc();
            NativeMethods.SetTextColor(DC, COLOR_SYMBOL);
            RT.Left += _marginWidth;
            int width;
            if ((element != null))
            {
                s = "<";
                width = _greaterThanWidth;
            }
            else
            {
                s = "</";
                width = _lessThanSlashWidth;
            }
            NativeMethods.DrawText(DC, s, s.Length, ref RT, TEXT_FLAGS);
            RT.Left += width;

            NativeMethods.SetTextColor(DC, COLOR_ELEMENT);
            if (element != null)
                s = element.Name.LocalName;
            else
                s = e.Node.Text;
            NativeMethods.DrawText(DC, s, s.Length, ref RT, TEXT_FLAGS);

            NativeMethods.DrawText(DC, s, s.Length, ref RM, TEXT_FLAGS | NativeMethods.DT_CALCRECT);
            RT.Left += RM.Right - RM.Left;

            // Draw attributes
            if (element != null)
            {
                foreach (XAttribute attr in element.Attributes())
                {
                    RT.Left += _spaceWidth;
                    NativeMethods.SetTextColor(DC, COLOR_ATTRIBUTE);
                    s = attr.Name.LocalName;
                    NativeMethods.DrawText(DC, s, s.Length, ref RT, TEXT_FLAGS);

                    NativeMethods.DrawText(DC, s, s.Length, ref RM, TEXT_FLAGS | NativeMethods.DT_CALCRECT);
                    RT.Left += RM.Right - RM.Left;

                    NativeMethods.SetTextColor(DC, COLOR_SYMBOL);
                    NativeMethods.DrawText(DC, "=", 1, ref RT, TEXT_FLAGS);
                    RT.Left += _equalWidth;

                    NativeMethods.SetTextColor(DC, COLOR_CONTENT);
                    NativeMethods.DrawText(DC, "\"", 1, ref RT, TEXT_FLAGS);

                    RT.Left += _quoteWidth;

                    NativeMethods.SetTextColor(DC, COLOR_SYMBOL);
                    s = attr.Value;
#if XmlChars
                    s = System.Net.WebUtility.HtmlEncode(s);
                    s = s.Replace(((char)0xA).ToString(), @"&#xA;");
#else
                    s = s.Replace(((char)0xA).ToString(), @"\n");
#endif
                    NativeMethods.DrawText(DC, s, s.Length, ref RT, TEXT_FLAGS);

                    NativeMethods.DrawText(DC, s, s.Length, ref RM, TEXT_FLAGS | NativeMethods.DT_CALCRECT);
                    RT.Left += RM.Right - RM.Left;

                    NativeMethods.SetTextColor(DC, COLOR_CONTENT);
                    NativeMethods.DrawText(DC, "\"", 1, ref RT, TEXT_FLAGS);

                    RT.Left += _quoteWidth;
                }
            }

            if (element != null && element.Nodes().Count() == 1 && element.FirstNode is XText)
                s = element.Value;
            else
                s = string.Empty;

            if (!string.IsNullOrEmpty(s))
            {
#if XmlChars
                s = System.Net.WebUtility.HtmlEncode(s);
                s = s.Replace(((char)0xA).ToString(), @"&#xA;");
#else
                s = s.Replace(((char)0xA).ToString(), @"\n");
#endif
                NativeMethods.SetTextColor(DC, COLOR_SYMBOL);
                NativeMethods.DrawText(DC, ">", 1, ref RT, TEXT_FLAGS);
                RT.Left += _greaterThanWidth;

                NativeMethods.SetTextColor(DC, COLOR_CONTENT);
                NativeMethods.DrawText(DC, s, s.Length, ref RT, TEXT_FLAGS);

                NativeMethods.DrawText(DC, s, s.Length, ref RM, TEXT_FLAGS | NativeMethods.DT_CALCRECT);
                RT.Left += RM.Right - RM.Left;

                NativeMethods.SetTextColor(DC, COLOR_SYMBOL);
                NativeMethods.DrawText(DC, "</", 2, ref RT, TEXT_FLAGS);
                RT.Left += _lessThanSlashWidth;

                s = element.Name.LocalName;
                NativeMethods.SetTextColor(DC, COLOR_ELEMENT);
                NativeMethods.DrawText(DC, s, s.Length, ref RT, TEXT_FLAGS);

                NativeMethods.DrawText(DC, s, s.Length, ref RM, TEXT_FLAGS | NativeMethods.DT_CALCRECT);
                RT.Left += RM.Right - RM.Left;

                NativeMethods.SetTextColor(DC, COLOR_SYMBOL);
                NativeMethods.DrawText(DC, ">", 1, ref RT, TEXT_FLAGS);
            }
            else
            {
                NativeMethods.SetTextColor(DC, COLOR_SYMBOL);
                if ((element != null) && (e.Node.Nodes.Count == 0))
                    s = "/>";
                else
                    s = ">";
                NativeMethods.DrawText(DC, s, s.Length, ref RT, TEXT_FLAGS);
            }
            e.Graphics.ReleaseHdc(DC);
            e.DrawDefault = false;
        }
    }
}
