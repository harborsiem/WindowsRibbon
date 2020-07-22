// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc.
//
// To add a suppression to this file, right-click the message in the 
// Code Analysis results, point to "Suppress Message", and click 
// "In Suppression File".
// You do not need to add suppressions to this file manually.

//The disposable fields are disposed. But the Code analysis don't know the ? operator
//For this we suppress the messages

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "_application", Scope = "member", Target = "UIRibbonTools.TRibbonDocument.#Dispose(System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "_keytip", Scope = "member", Target = "UIRibbonTools.TRibbonCommand.#Dispose(System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "_labelDescription", Scope = "member", Target = "UIRibbonTools.TRibbonCommand.#Dispose(System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "_labelTitle", Scope = "member", Target = "UIRibbonTools.TRibbonCommand.#Dispose(System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "_largeHighContrastImages", Scope = "member", Target = "UIRibbonTools.TRibbonCommand.#Dispose(System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "_largeImages", Scope = "member", Target = "UIRibbonTools.TRibbonCommand.#Dispose(System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "_smallHighContrastImages", Scope = "member", Target = "UIRibbonTools.TRibbonCommand.#Dispose(System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "_smallImages", Scope = "member", Target = "UIRibbonTools.TRibbonCommand.#Dispose(System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "_tooltipDescription", Scope = "member", Target = "UIRibbonTools.TRibbonCommand.#Dispose(System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "_tooltipTitle", Scope = "member", Target = "UIRibbonTools.TRibbonCommand.#Dispose(System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "_controls", Scope = "member", Target = "UIRibbonTools.TRibbonAppMenuGroup.#Dispose(System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "_buttonItem", Scope = "member", Target = "UIRibbonTools.TRibbonSplitButton.#Dispose(System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "_controls", Scope = "member", Target = "UIRibbonTools.TRibbonSplitButton.#Dispose(System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "_menuGroups", Scope = "member", Target = "UIRibbonTools.TRibbonSplitButton.#Dispose(System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "_controls", Scope = "member", Target = "UIRibbonTools.TRibbonDropDownButton.#Dispose(System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "_menuGroups", Scope = "member", Target = "UIRibbonTools.TRibbonDropDownButton.#Dispose(System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "_controls", Scope = "member", Target = "UIRibbonTools.TRibbonControlGroup.#Dispose(System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "_controls", Scope = "member", Target = "UIRibbonTools.TRibbonGallery.#Dispose(System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "_menuGroups", Scope = "member", Target = "UIRibbonTools.TRibbonGallery.#Dispose(System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "_menuLayout", Scope = "member", Target = "UIRibbonTools.TRibbonGallery.#Dispose(System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "_menuGroups", Scope = "member", Target = "UIRibbonTools.TRibbonApplicationMenu.#Dispose(System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "_recentItems", Scope = "member", Target = "UIRibbonTools.TRibbonApplicationMenu.#Dispose(System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "_controls", Scope = "member", Target = "UIRibbonTools.TRibbonQuickAccessToolbar.#Dispose(System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "_idealSizes", Scope = "member", Target = "UIRibbonTools.TRibbonScalingPolicy.#Dispose(System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "_scales", Scope = "member", Target = "UIRibbonTools.TRibbonScalingPolicy.#Dispose(System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "_elements", Scope = "member", Target = "UIRibbonTools.TRibbonRow.#Dispose(System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "_controlSizeDefinitions", Scope = "member", Target = "UIRibbonTools.TRibbonControlSizeGroup.#Dispose(System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "_elements", Scope = "member", Target = "UIRibbonTools.TRibbonGroupSizeDefinition.#Dispose(System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "_controlNameMap", Scope = "member", Target = "UIRibbonTools.TRibbonSizeDefinition.#Dispose(System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "_groupSizeDefinitions", Scope = "member", Target = "UIRibbonTools.TRibbonSizeDefinition.#Dispose(System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "_controls", Scope = "member", Target = "UIRibbonTools.TRibbonGroup.#Dispose(System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "_sizeDefinition", Scope = "member", Target = "UIRibbonTools.TRibbonGroup.#Dispose(System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "_groups", Scope = "member", Target = "UIRibbonTools.TRibbonTab.#Dispose(System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "_scalingPolicy", Scope = "member", Target = "UIRibbonTools.TRibbonTab.#Dispose(System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "_tabs", Scope = "member", Target = "UIRibbonTools.TRibbonTabGroup.#Dispose(System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "_applicationMenu", Scope = "member", Target = "UIRibbonTools.TRibbonViewRibbon.#Dispose(System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "_contextualTabs", Scope = "member", Target = "UIRibbonTools.TRibbonViewRibbon.#Dispose(System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "_helpButton", Scope = "member", Target = "UIRibbonTools.TRibbonViewRibbon.#Dispose(System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "_quickAccessToolbar", Scope = "member", Target = "UIRibbonTools.TRibbonViewRibbon.#Dispose(System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "_sizeDefinitions", Scope = "member", Target = "UIRibbonTools.TRibbonViewRibbon.#Dispose(System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "_tabs", Scope = "member", Target = "UIRibbonTools.TRibbonViewRibbon.#Dispose(System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "_menuGroups", Scope = "member", Target = "UIRibbonTools.TRibbonMiniToolbar.#Dispose(System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "_menuGroups", Scope = "member", Target = "UIRibbonTools.TRibbonContextMenu.#Dispose(System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "_contextMaps", Scope = "member", Target = "UIRibbonTools.TRibbonViewContextPopup.#Dispose(System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "_contextMenus", Scope = "member", Target = "UIRibbonTools.TRibbonViewContextPopup.#Dispose(System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "_miniToolbars", Scope = "member", Target = "UIRibbonTools.TRibbonViewContextPopup.#Dispose(System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "_commands", Scope = "member", Target = "UIRibbonTools.TRibbonApplication.#Dispose(System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "_commandsById", Scope = "member", Target = "UIRibbonTools.TRibbonApplication.#Dispose(System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "_commandsByName", Scope = "member", Target = "UIRibbonTools.TRibbonApplication.#Dispose(System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "_views", Scope = "member", Target = "UIRibbonTools.TRibbonApplication.#Dispose(System.Boolean)")]

