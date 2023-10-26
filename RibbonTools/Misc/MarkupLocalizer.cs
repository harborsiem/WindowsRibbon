using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;
using System.Resources;

namespace UIRibbonTools
{
    /// <summary>
    /// Helper class for localization for the markup file.
    /// </summary>
    public class MarkupLocalizer
    {
        /// <summary>
        /// Convert the Ribbon markup with default language to a localization markup file with a resx file.
        /// Resx file name is same like outputFile with resx extension.
        /// One can use the output resx file to build more languages.
        /// </summary>
        /// <param name="inputFile">Input markup file name</param>
        /// <param name="outputFile">Output markup file name, is also used for resx file name with resx extension</param>
        public void Localize(string inputFile, string outputFile)
        {
            if (!File.Exists(inputFile))
                throw new ArgumentException("File not exist", nameof(inputFile));
            string directory = Path.GetDirectoryName(Path.GetFullPath(outputFile));
            string resxFilename = Path.Combine(directory, Path.GetFileNameWithoutExtension(outputFile) + ".resx");
            ResXResourceWriter writer = new ResXResourceWriter(resxFilename);
            TRibbonDocument ribbonDocument = new TRibbonDocument();
            ribbonDocument.LoadFromFile(inputFile);
            TRibbonList<TRibbonCommand> commandList = ribbonDocument.Application.Commands;
            string resourceName;
            for (int i = 0; i < commandList.Count; i++)
            {
                TRibbonCommand command = commandList[i];
                string name = command.Name;
                string labelTitle = command.LabelTitle.Content;
                if (labelTitle != null)
                {
                    resourceName = name + "_LabelTitle";
                    writer.AddResource(resourceName, labelTitle);
                    command.LabelTitle.Content = "{Resource:" + resourceName + "}";
                }
                string labelDescription = command.LabelDescription.Content;
                if (labelDescription != null)
                {
                    resourceName = name + "_LabelDescription";
                    writer.AddResource(resourceName, labelDescription);
                    command.LabelDescription.Content = "{Resource:" + resourceName + "}";
                }
                string tooltipTitle = command.TooltipTitle.Content;
                if (tooltipTitle != null)
                {
                    resourceName = name + "_TooltipTitle";
                    writer.AddResource(resourceName, tooltipTitle);
                    command.TooltipTitle.Content = "{Resource:" + resourceName + "}";
                }
                string tooltipDescription = command.TooltipDescription.Content;
                if (tooltipDescription != null)
                {
                    resourceName = name + "_TooltipDescription";
                    writer.AddResource(resourceName, tooltipDescription);
                    command.TooltipDescription.Content = "{Resource:" + resourceName + "}";
                }
                string keytip = command.Keytip.Content;
                if (keytip != null)
                {
                    resourceName = name + "_Keytip";
                    writer.AddResource(resourceName, keytip);
                    command.Keytip.Content = "{Resource:" + resourceName + "}";
                }
                string comment = command.Comment;
                if (comment != null)
                {
                    resourceName = name + "_Comment";
                    writer.AddResource(resourceName, comment);
                    command.Comment = "{Resource:" + resourceName + "}";
                }
            }
            writer.Close();
            ribbonDocument.SaveToFile(outputFile);
            ribbonDocument.Dispose();
        }

        /// <summary>
        /// Convert the localization Ribbon markup with default language resx file to a markup file with language values inside.
        /// Resx file name must be same like inputFile with resx extension.
        /// The key for a value in the resx file must be like Localize method (eg. Command name + _LabelTitle)
        /// </summary>
        /// <param name="inputFile">Input markup file name and resx file name</param>
        /// <param name="outputFile">Output markup file name</param>
        public void DeLocalize(string inputFile, string outputFile)
        {
            if (!File.Exists(inputFile))
                throw new ArgumentException("File not exist", nameof(inputFile));
            string directory = Path.GetDirectoryName(Path.GetFullPath(inputFile));
            string resxFilename = Path.Combine(directory, Path.GetFileNameWithoutExtension(inputFile) + ".resx");
            if (!File.Exists(resxFilename))
                throw new ArgumentException("Resx file not exist");
            ResXResourceReader reader = new ResXResourceReader(resxFilename);
            Dictionary<string, string> dict = new Dictionary<string, string>();
            foreach (DictionaryEntry entry in reader)
            {
                if (entry.Key is string sKey && entry.Value is string sValue)
                    dict.Add(sKey, sValue);
            }
            TRibbonDocument ribbonDocument = new TRibbonDocument();
            ribbonDocument.LoadFromFile(inputFile);
            TRibbonList<TRibbonCommand> commandList = ribbonDocument.Application.Commands;
            string resourceName;
            for (int i = 0; i < commandList.Count; i++)
            {
                TRibbonCommand command = commandList[i];
                string name = command.Name;
                if (command.LabelTitle.Content != null)
                {
                    resourceName = name + "_LabelTitle";
                    if (dict.ContainsKey(resourceName))
                        command.LabelTitle.Content = dict[resourceName];
                }
                if (command.LabelDescription.Content != null)
                {
                    resourceName = name + "_LabelDescription";
                    if (dict.ContainsKey(resourceName))
                        command.LabelDescription.Content = dict[resourceName];
                }
                if (command.TooltipTitle.Content != null)
                {
                    resourceName = name + "_TooltipTitle";
                    if (dict.ContainsKey(resourceName))
                        command.TooltipTitle.Content = dict[resourceName];
                }
                if (command.TooltipDescription.Content != null)
                {
                    resourceName = name + "_TooltipDescription";
                    if (dict.ContainsKey(resourceName))
                        command.TooltipDescription.Content = dict[resourceName];
                }
                if (command.Keytip.Content != null)
                {
                    resourceName = name + "_Keytip";
                    if (dict.ContainsKey(resourceName))
                        command.Keytip.Content = dict[resourceName];
                }
                if (command.Comment != null)
                {
                    resourceName = name + "_Comment";
                    if (dict.ContainsKey(resourceName))
                        command.Comment = dict[resourceName];
                }
            }
            reader.Close();
            ribbonDocument.SaveToFile(outputFile);
            ribbonDocument.Dispose();
        }
    }
}

