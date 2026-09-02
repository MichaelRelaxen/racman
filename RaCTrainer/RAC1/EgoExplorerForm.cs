using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace racman
{
    internal sealed class EgoExplorerForm : Form
    {
        private const uint WeaponsRecordBase = 0x0072A6FC;
        private const uint MainSelectedIndexAddress = 0x0072A47C;
        private const uint CombinedSelectedIndexAddress = 0x0072A8B0;
        private const uint MenuRootPointerAddress = 0x00A52BB4;
        private const uint MainWeaponsDescriptorAddress = 0x0072A440;
        private const uint CombinedWeaponsDescriptorAddress = 0x0072A874;
        private const uint HandDescriptorAddress = 0x0072A078;
        private const uint BackpackDescriptorAddress = 0x0072A0E0;
        private const uint HeadDescriptorAddress = 0x0072A148;
        private const uint BootsDescriptorAddress = 0x0072A1B0;
        private const uint CombinedHandDescriptorAddress = 0x0072A8DC;
        private const uint HandRecordBase = 0x0072A218;
        private const uint ItemsDescriptorAddress = 0x0072D660;
        private const uint ItemsSelectedIndexAddress = 0x0072D69C;
        private const uint ItemsRecordBase = 0x0072D604;
        private const uint ItemsFlagArrayAddress = 0x0096BFF0;
        private const uint ItemsPreviewMonitorFlagsAddress = 0x0072D588;
        private const uint UnlockArrayAddress = 0x0096C140;
        private const uint ItemDefinitionBase = 0x00721008;
        private const int ItemDefinitionSize = 0x4C;
        private const uint EquippedItemBase = 0x00A52BE0;
        private const uint WeaponClassArray = 0x00A4D540;
        private const uint WeaponClassCount = 0x00A4DAE4;
        private const uint OClassToMClassTable = 0x00A354C0;
        private const uint OClassToMClassLength = 0x800;
        private const int WeaponsColumns = 5;
        private const int WeaponsRows = 3;
        private const int GadgetColumns = 3;
        private const int GadgetLegitSlots = 14;
        private const int BootsPhysicalStart = 12;
        private const int GadgetThreeColumnRows = 4;
        private const int ItemsColumns = 3;
        private const int ItemsRows = 3;
        private const int RecordSize = 10;
        private const int GridRows = 9;
        private static readonly Color LegitFill = Color.FromArgb(24, 18, 62);
        private static readonly Color LegitBorder = Color.FromArgb(145, 99, 255);
        private static readonly Color NoItemFill = Color.FromArgb(14, 29, 36);
        private static readonly Color NoItemBorder = Color.FromArgb(82, 155, 178);
        private static readonly Color EquippableFill = Color.FromArgb(49, 42, 8);
        private static readonly Color EquippableBorder = Color.FromArgb(255, 214, 92);
        private static readonly Color InvalidLockedFill = Color.FromArgb(48, 7, 14);
        private static readonly Color InvalidLockedBorder = Color.FromArgb(205, 43, 61);
        private static readonly Color InvalidUnlockedFill = Color.FromArgb(104, 13, 20);
        private static readonly Color InvalidUnlockedBorder = Color.FromArgb(255, 105, 72);
        private static readonly string[] ItemNames =
        {
            "No item",
            "Base Clank",
            "Heli-Pack",
            "Thruster-Pack",
            "Hydro-Pack",
            "Sonic Summoner",
            "O2 Mask",
            "Pilot's Helmet",
            "Wrench",
            "Suck Cannon",
            "Bomb Glove",
            "Devastator",
            "Swingshot",
            "Visibomb Gun",
            "Taunter",
            "Blaster",
            "Pyrocitor",
            "Mine Glove",
            "Walloper",
            "Tesla Claw",
            "Glove of Doom",
            "Morph-o-Ray",
            "Hydrodisplacer",
            "RYNO",
            "Drone Device",
            "Decoy Glove",
            "Trespasser",
            "Metal Detector",
            "Magneboots",
            "Grindboots",
            "Hoverboard",
            "Hologuise",
            "PDA",
            "Map-O-Matic",
            "Bolt Grabber",
            "Persuader"
        };
        private static readonly string[] ItemsFlagNames =
        {
            "Platinum Zoomerator",
            "Raritanium",
            "CodeBot",
            "Unmapped item flag 3",
            "Premium Nanotech",
            "Ultra Nanotech"
        };

        private static readonly InputDefinition[] InputDefinitions =
        {
            new InputDefinition("U", DirectionMask.Up),
            new InputDefinition("D", DirectionMask.Down),
            new InputDefinition("L", DirectionMask.Left),
            new InputDefinition("R", DirectionMask.Right),

            new InputDefinition("UD", DirectionMask.Up | DirectionMask.Down),
            new InputDefinition("UL", DirectionMask.Up | DirectionMask.Left),
            new InputDefinition("UR", DirectionMask.Up | DirectionMask.Right),
            new InputDefinition("DL", DirectionMask.Down | DirectionMask.Left),
            new InputDefinition("DR", DirectionMask.Down | DirectionMask.Right),
            new InputDefinition("LR", DirectionMask.Left | DirectionMask.Right),

            new InputDefinition("UDL", DirectionMask.Up | DirectionMask.Down | DirectionMask.Left),
            new InputDefinition("UDR", DirectionMask.Up | DirectionMask.Down | DirectionMask.Right),
            new InputDefinition("ULR", DirectionMask.Up | DirectionMask.Left | DirectionMask.Right),
            new InputDefinition("DLR", DirectionMask.Down | DirectionMask.Left | DirectionMask.Right),

            new InputDefinition("UDLR", DirectionMask.Up | DirectionMask.Down | DirectionMask.Left | DirectionMask.Right)
        };

        private readonly rac1 game;
        private readonly Timer refreshTimer;
        private readonly OverflowGridCanvas gridCanvas;
        private readonly Label currentIndexLabel;
        private readonly Label currentDetailLabel;
        private readonly Label liveLabel;
        private readonly ComboBox descriptorSelector;
        private readonly ToolStripStatusLabel statusLabel;
        private readonly ToolStripStatusLabel hoverLabel;
        private readonly TabControl mapTabs;
        private readonly TabPage weaponsTab;
        private readonly TabPage gadgetsTab;
        private readonly TabPage itemsTab;
        private readonly TabPage slotLogTab;
        private readonly SplitContainer mapSurface;
        private readonly ListView slotLog;
        private readonly List<MoveCard> moveCards = new List<MoveCard>();
        private readonly Dictionary<ulong, byte[]> watchedSlotBytes =
            new Dictionary<ulong, byte[]>();

        private bool autoUsesCombined;
        private bool hasWeaponCursorSnapshot;
        private WeaponCursorKind lastMovedWeaponCursor;
        private uint lastGadgetDescriptor = HandDescriptorAddress;
        private int liveMainIndex;
        private int liveCombinedIndex;
        private int currentIndex;
        private int highlightedDestination = int.MinValue;
        private string highlightedInput = string.Empty;
        private int weaponScrollRows;
        private int gadgetScrollRows;
        private int itemsScrollRows;
        private int hoveredCellIndex = int.MinValue;
        private Func<int, DirectionMask, int> resolveDestinationFromIndex;
        private ExplorerMap selectedMap = ExplorerMap.Weapons;

        private enum ExplorerMap
        {
            Weapons,
            Gadgets,
            Items
        }

        private enum WeaponCursorKind
        {
            None,
            Main,
            Combined
        }

        [Flags]
        private enum DirectionMask
        {
            None = 0,
            Up = 1,
            Down = 2,
            Left = 4,
            Right = 8
        }

        private enum LandingSafety
        {
            Safe,
            Caution,
            Crash,
            Unknown
        }

        private sealed class InputDefinition
        {
            public readonly string Name;
            public readonly DirectionMask Mask;

            public InputDefinition(string name, DirectionMask mask)
            {
                Name = name;
                Mask = mask;
            }
        }

        private sealed class CellInfo
        {
            public int Index;
            public uint Address;
            public short ItemId;
            public short IconId;
            public short IconVariant;
            public short SourceSelector;
            public bool Owned;
            public bool UsesItemsFlag;
            public byte StateByte;
            public LandingSafety Safety;
            public uint DefinitionAddress;
            public uint FakeGroup;
            public uint FakeModel;
            public uint EffectiveModel;
            public uint GateAddress;
            public byte GateByte;
            public uint ConfirmTarget;
            public bool PreviewUsesWeaponLoader;
            public bool WeaponCacheMatch;
            public string LiveVerdict;
            public string LiveDetail;
            public string XEffect;
            public bool DynamicSelectedItemMirror;

            public string ItemName
            {
                get
                {
                    string name;
                    if (UsesItemsFlag &&
                        ItemId >= 0 &&
                        ItemId < ItemsFlagNames.Length)
                        name = ItemsFlagNames[ItemId];
                    else if (UsesItemsFlag)
                        name = "Item flag " + ItemId;
                    else if (ItemId >= 0 && ItemId < ItemNames.Length)
                        name = ItemNames[ItemId];
                    else
                        name = "Invalid ID " + ItemId;

                    return DynamicSelectedItemMirror
                        ? "Live mirror: " + name
                        : name;
                }
            }
        }

        private sealed class RouteInfo
        {
            public int Destination;
            public string Inputs;
            public LandingSafety Safety;
        }

        private sealed class LiveItemInfo
        {
            public uint DefinitionAddress;
            public uint FakeGroup;
            public uint FakeModel;
            public uint EffectiveModel;
            public uint GateAddress;
            public byte GateByte;
            public uint ConfirmTarget;
            public bool PreviewUsesWeaponLoader;
            public bool WeaponCacheMatch;
            public LandingSafety Safety;
            public string Verdict;
            public string Detail;
            public string XEffect;
        }

        private sealed class DescriptorState
        {
            public uint Address;
            public uint Flags;
            public int SelectedIndex;
            public int Rows;
            public int Columns;
            public uint RecordBase;
            public uint Up;
            public uint Down;
            public uint Left;
            public uint Right;
        }

        private sealed class OverflowGridCanvas : Control
        {
            private readonly Font indexFont;
            private readonly Font itemFont;
            private readonly Font metaFont;
            private readonly Font routeFont;
            private readonly Font cursorFont;
            private List<CellInfo> cells = new List<CellInfo>();
            private List<RouteInfo> routes = new List<RouteInfo>();
            private int startIndex;
            private int currentIndex;
            private int routeSourceIndex;
            private int highlightedDestination = int.MinValue;
            private string highlightedInput = string.Empty;
            private Rectangle[] cellBounds = new Rectangle[0];
            private int displayColumns = WeaponsColumns;
            private int displayRows = GridRows;
            private int legitCount = WeaponsColumns * WeaponsRows;
            private bool gadgetRaggedLayout;
            private int firstVisualRow;
            private bool showWeaponCursors;
            private int mainWeaponCursor;
            private int combinedWeaponCursor;
            private WeaponCursorKind brightWeaponCursor;

            public event Action<CellInfo> CellHovered;
            public event Action CellHoverEnded;
            public event Action<int> ScrollRequested;
            public event Action CenterRequested;

            public OverflowGridCanvas()
            {
                SetStyle(
                    ControlStyles.AllPaintingInWmPaint |
                    ControlStyles.OptimizedDoubleBuffer |
                    ControlStyles.ResizeRedraw |
                    ControlStyles.Selectable |
                    ControlStyles.UserPaint,
                    true);

                BackColor = Color.FromArgb(6, 7, 7);
                Cursor = Cursors.Cross;
                TabStop = true;
                indexFont = new Font("Consolas", 13F, FontStyle.Bold);
                itemFont = new Font("Consolas", 11F, FontStyle.Bold);
                metaFont = new Font("Consolas", 8.5F, FontStyle.Bold);
                routeFont = new Font("Consolas", 8.5F, FontStyle.Bold);
                cursorFont = new Font("Consolas", 8.5F, FontStyle.Bold);
            }

            public void SetRoutes(List<RouteInfo> nextRoutes, int nextRouteSourceIndex)
            {
                routes = nextRoutes ?? new List<RouteInfo>();
                routeSourceIndex = nextRouteSourceIndex;
                Invalidate();
            }

            public void SetLayout(
                int nextColumns,
                int nextRows,
                int nextLegitCount,
                bool nextGadgetRaggedLayout,
                int nextFirstVisualRow)
            {
                displayColumns = Math.Max(1, nextColumns);
                displayRows = Math.Max(1, nextRows);
                legitCount = Math.Max(0, nextLegitCount);
                gadgetRaggedLayout = nextGadgetRaggedLayout;
                firstVisualRow = nextFirstVisualRow;
                Invalidate();
            }

            public void SetSnapshot(
                List<CellInfo> nextCells,
                int nextStartIndex,
                int nextCurrentIndex,
                int nextHighlightedDestination,
                string nextHighlightedInput)
            {
                cells = nextCells ?? new List<CellInfo>();
                startIndex = nextStartIndex;
                currentIndex = nextCurrentIndex;
                highlightedDestination = nextHighlightedDestination;
                highlightedInput = nextHighlightedInput ?? string.Empty;
                Invalidate();
            }

            public void SetWeaponCursors(
                bool visible,
                int mainIndex,
                int combinedIndex,
                WeaponCursorKind brightCursor)
            {
                showWeaponCursors = visible;
                mainWeaponCursor = mainIndex;
                combinedWeaponCursor = combinedIndex;
                brightWeaponCursor = brightCursor;
                Invalidate();
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing)
                {
                    indexFont.Dispose();
                    itemFont.Dispose();
                    metaFont.Dispose();
                    routeFont.Dispose();
                    cursorFont.Dispose();
                }
                base.Dispose(disposing);
            }

            protected override void OnMouseMove(MouseEventArgs e)
            {
                base.OnMouseMove(e);
                for (int i = 0; i < cellBounds.Length && i < cells.Count; i++)
                {
                    if (!cellBounds[i].Contains(e.Location))
                        continue;

                    if (CellHovered != null)
                        CellHovered(cells[i]);
                    return;
                }

                if (CellHoverEnded != null)
                    CellHoverEnded();
            }

            protected override void OnMouseEnter(EventArgs e)
            {
                base.OnMouseEnter(e);
                Focus();
            }

            protected override void OnMouseWheel(MouseEventArgs e)
            {
                base.OnMouseWheel(e);
                if (ScrollRequested != null && e.Delta != 0)
                    ScrollRequested(e.Delta > 0 ? -1 : 1);
            }

            protected override void OnDoubleClick(EventArgs e)
            {
                base.OnDoubleClick(e);
                if (CenterRequested != null)
                    CenterRequested();
            }

            protected override void OnMouseLeave(EventArgs e)
            {
                base.OnMouseLeave(e);
                if (CellHoverEnded != null)
                    CellHoverEnded();
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                const int outerPadding = 7;
                const int headerHeight = 4;
                int availableWidth = Math.Max(100, ClientSize.Width - (outerPadding * 2));
                int availableHeight = Math.Max(100, ClientSize.Height - headerHeight - outerPadding);
                int cellHeight = availableHeight / displayRows;
                cellBounds = new Rectangle[cells.Count];

                for (int i = 0; i < cells.Count; i++)
                {
                    int row;
                    int column;
                    int rowColumns;
                    if (gadgetRaggedLayout)
                    {
                        int absoluteRow = GadgetVisualRow(cells[i].Index);
                        row = absoluteRow - firstVisualRow;
                        column = GadgetVisualColumn(cells[i].Index);
                        rowColumns =
                            absoluteRow < GadgetThreeColumnRows
                                ? GadgetColumns
                                : 2;
                    }
                    else
                    {
                        row = i / displayColumns;
                        column = i % displayColumns;
                        rowColumns = displayColumns;
                    }

                    int cellWidth = availableWidth / rowColumns;
                    Rectangle bounds = new Rectangle(
                        outerPadding + (column * cellWidth),
                        headerHeight + (row * cellHeight),
                        cellWidth - 3,
                        cellHeight - 3);
                    cellBounds[i] = bounds;
                    DrawCell(e.Graphics, cells[i], bounds);
                }

                DrawAllRouteArrows(e.Graphics);
                DrawHighlightArrow(e.Graphics);
            }

            private void DrawCell(Graphics graphics, CellInfo cell, Rectangle bounds)
            {
                Color fill = SafetyFill(cell.Safety);
                Color border = SafetyBorder(cell.Safety);
                bool isLegitWeaponSlot = cell.Index >= 0 && cell.Index < legitCount;
                bool isCurrent = !showWeaponCursors && cell.Index == currentIndex;
                bool isDestination = cell.Index == highlightedDestination;
                bool isRouteSource = cell.Index == routeSourceIndex;
                bool isNoItem = IsNoItemCell(cell);
                bool isEquippableItem = IsEquippableItemCell(cell);
                bool isInvalidCrash =
                    IsInvalidItemId(cell) &&
                    cell.Safety == LandingSafety.Crash;
                if (isNoItem)
                {
                    fill = NoItemFill;
                    border = NoItemBorder;
                }
                else if (isEquippableItem)
                {
                    fill = EquippableFill;
                    border = EquippableBorder;
                }
                if (isLegitWeaponSlot)
                {
                    fill = LegitFill;
                    border = LegitBorder;
                }
                if (isInvalidCrash)
                {
                    fill = cell.GateByte != 0
                        ? InvalidUnlockedFill
                        : InvalidLockedFill;
                    border = cell.GateByte != 0
                        ? InvalidUnlockedBorder
                        : InvalidLockedBorder;
                }
                if (isCurrent)
                {
                    border = Color.FromArgb(53, 232, 255);
                }

                using (GraphicsPath path = RoundedRectangle(bounds, 2))
                using (SolidBrush brush = new SolidBrush(fill))
                using (Pen borderPen = new Pen(border, 1.4F))
                {
                    graphics.FillPath(brush, path);
                    graphics.DrawPath(borderPen, path);
                }

                if (isCurrent)
                {
                    using (Pen glowPen = new Pen(Color.FromArgb(135, 54, 220, 255), 8F))
                    using (GraphicsPath glowPath = RoundedRectangle(Rectangle.Inflate(bounds, -5, -5), 5))
                    {
                        graphics.DrawPath(glowPen, glowPath);
                    }
                    using (Pen currentPen = new Pen(Color.White, 3F))
                    using (GraphicsPath currentPath = RoundedRectangle(Rectangle.Inflate(bounds, -7, -7), 4))
                    {
                        graphics.DrawPath(currentPen, currentPath);
                    }
                    using (SolidBrush currentBand = new SolidBrush(Color.FromArgb(205, 0, 155, 190)))
                    {
                        graphics.FillRectangle(
                            currentBand,
                            bounds.X + 7,
                            bounds.Y + 5,
                            bounds.Width - 14,
                            25);
                    }
                }
                else if (isRouteSource && !showWeaponCursors)
                {
                    using (Pen sourcePen = new Pen(Color.White, 3F))
                    using (GraphicsPath sourcePath = RoundedRectangle(
                        Rectangle.Inflate(bounds, -3, -3),
                        2))
                    {
                        graphics.DrawPath(sourcePen, sourcePath);
                    }
                }
                else if (isDestination)
                {
                    using (Pen destinationPen = new Pen(Color.White, 3F))
                    using (GraphicsPath destinationPath = RoundedRectangle(Rectangle.Inflate(bounds, -3, -3), 5))
                    {
                        graphics.DrawPath(destinationPen, destinationPath);
                    }
                }

                Rectangle line1 = new Rectangle(
                    bounds.X + 10,
                    bounds.Y + 4,
                    bounds.Width - 20,
                    25);
                Rectangle line2 = new Rectangle(
                    bounds.X + 10,
                    bounds.Y + 27,
                    bounds.Width - 20,
                    24);
                Rectangle line3 = new Rectangle(
                    bounds.X + 10,
                    bounds.Bottom - 19,
                    bounds.Width - 20,
                    16);

                using (SolidBrush primary = new SolidBrush(Color.White))
                using (SolidBrush secondary = new SolidBrush(Color.FromArgb(221, 226, 235)))
                using (SolidBrush meta = new SolidBrush(Color.FromArgb(160, 178, 188)))
                {
                    graphics.DrawString("#" + cell.Index, indexFont, primary, line1);
                    graphics.DrawString(
                        Ellipsize(CellDisplayName(cell), 28),
                        itemFont,
                        secondary,
                        line2);
                    graphics.DrawString(
                        CellMetaLine(cell),
                        metaFont,
                        meta,
                        line3);
                }

                if (showWeaponCursors)
                    DrawWeaponCursorMarkers(graphics, cell.Index, bounds);
            }

            private void DrawWeaponCursorMarkers(
                Graphics graphics,
                int cellIndex,
                Rectangle bounds)
            {
                bool hasMain = cellIndex == mainWeaponCursor;
                bool hasCombined = cellIndex == combinedWeaponCursor;
                if (!hasMain && !hasCombined)
                    return;

                bool mainIsBright =
                    hasMain && brightWeaponCursor == WeaponCursorKind.Main;
                bool combinedIsBright =
                    hasCombined && brightWeaponCursor == WeaponCursorKind.Combined;
                int mainOffset = hasMain && hasCombined ? 50 : 0;

                if (hasMain && !mainIsBright)
                {
                    DrawWeaponCursorMarker(
                        graphics,
                        bounds,
                        "MAIN",
                        Color.FromArgb(255, 72, 190),
                        false,
                        mainOffset);
                }
                if (hasCombined && !combinedIsBright)
                {
                    DrawWeaponCursorMarker(
                        graphics,
                        bounds,
                        "QS",
                        Color.FromArgb(50, 235, 255),
                        false,
                        0);
                }
                if (mainIsBright)
                {
                    DrawWeaponCursorMarker(
                        graphics,
                        bounds,
                        "MAIN",
                        Color.FromArgb(255, 72, 190),
                        true,
                        mainOffset);
                }
                if (combinedIsBright)
                {
                    DrawWeaponCursorMarker(
                        graphics,
                        bounds,
                        "QS",
                        Color.FromArgb(50, 235, 255),
                        true,
                        0);
                }
            }

            private void DrawWeaponCursorMarker(
                Graphics graphics,
                Rectangle bounds,
                string label,
                Color color,
                bool bright,
                int rightOffset)
            {
                Rectangle markerBounds = new Rectangle(
                    bounds.Right - 55 - rightOffset,
                    bounds.Y + 6,
                    48,
                    20);

                if (bright)
                {
                    using (Pen glowPen = new Pen(Color.FromArgb(125, color), 8F))
                    using (GraphicsPath glowPath = RoundedRectangle(
                        Rectangle.Inflate(bounds, -5, -5),
                        4))
                    {
                        graphics.DrawPath(glowPen, glowPath);
                    }
                    using (Pen brightPen = new Pen(Color.White, 3.5F))
                    using (GraphicsPath brightPath = RoundedRectangle(
                        Rectangle.Inflate(bounds, -7, -7),
                        4))
                    {
                        graphics.DrawPath(brightPen, brightPath);
                    }
                }
                else
                {
                    using (Pen dimPen = new Pen(Color.FromArgb(115, color), 1.8F))
                    using (GraphicsPath dimPath = RoundedRectangle(
                        Rectangle.Inflate(bounds, -4, -4),
                        3))
                    {
                        graphics.DrawPath(dimPen, dimPath);
                    }
                }

                using (SolidBrush background = new SolidBrush(
                    bright
                        ? Color.FromArgb(245, color)
                        : Color.FromArgb(85, color)))
                using (Pen markerPen = new Pen(
                    bright
                        ? Color.White
                        : Color.FromArgb(145, color),
                    bright ? 2F : 1F))
                using (SolidBrush foreground = new SolidBrush(
                    bright
                        ? Color.Black
                        : Color.FromArgb(185, 220, 225, 230)))
                using (GraphicsPath markerPath = RoundedRectangle(markerBounds, 3))
                using (StringFormat centered = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                })
                {
                    graphics.FillPath(background, markerPath);
                    graphics.DrawPath(markerPen, markerPath);
                    graphics.DrawString(
                        label,
                        cursorFont,
                        foreground,
                        markerBounds,
                        centered);
                }
            }

            private void DrawAllRouteArrows(Graphics graphics)
            {
                if (routes.Count == 0 || cells.Count == 0)
                    return;

                for (int i = 0; i < routes.Count; i++)
                {
                    PointF from;
                    PointF to;
                    if (!TryGetRoutePoints(routes[i].Destination, out from, out to))
                        continue;

                    Color routeColor = SafetyBorder(routes[i].Safety);
                    using (Pen routePen = new Pen(Color.FromArgb(155, routeColor), 2.2F))
                    {
                        if (routes[i].Destination == routeSourceIndex)
                        {
                            graphics.DrawEllipse(routePen, from.X - 13, from.Y - 13, 26, 26);
                        }
                        else
                        {
                            routePen.CustomEndCap = new AdjustableArrowCap(3F, 4.5F, true);
                            graphics.DrawLine(routePen, from, to);
                        }
                    }
                }

                for (int i = 0; i < routes.Count; i++)
                {
                    PointF from;
                    PointF to;
                    if (!TryGetRoutePoints(routes[i].Destination, out from, out to))
                        continue;

                    float x = from.X + ((to.X - from.X) * 0.72F);
                    float y = from.Y + ((to.Y - from.Y) * 0.72F);
                    if (routes[i].Destination == routeSourceIndex)
                    {
                        x += 18;
                        y -= 18;
                    }

                    SizeF labelSize = graphics.MeasureString(routes[i].Inputs, routeFont);
                    RectangleF labelBounds = new RectangleF(
                        x - (labelSize.Width / 2F) - 4,
                        y - (labelSize.Height / 2F) - 1,
                        labelSize.Width + 8,
                        labelSize.Height + 2);
                    Color routeColor = SafetyBorder(routes[i].Safety);
                    using (SolidBrush background = new SolidBrush(Color.FromArgb(220, 10, 14, 20)))
                    using (SolidBrush foreground = new SolidBrush(Color.White))
                    using (Pen borderPen = new Pen(Color.FromArgb(210, routeColor), 1F))
                    {
                        graphics.FillRectangle(background, labelBounds);
                        graphics.DrawRectangle(
                            borderPen,
                            labelBounds.X,
                            labelBounds.Y,
                            labelBounds.Width,
                            labelBounds.Height);
                        graphics.DrawString(
                            routes[i].Inputs,
                            routeFont,
                            foreground,
                            labelBounds.X + 4,
                            labelBounds.Y + 1);
                    }
                }
            }

            private bool TryGetRoutePoints(int destination, out PointF from, out PointF to)
            {
                from = PointF.Empty;
                to = PointF.Empty;
                int currentOffset = routeSourceIndex - startIndex;
                int destinationOffset = destination - startIndex;
                if (currentOffset < 0 || currentOffset >= cellBounds.Length ||
                    destinationOffset < 0 || destinationOffset >= cellBounds.Length)
                    return false;

                Rectangle fromRect = cellBounds[currentOffset];
                Rectangle toRect = cellBounds[destinationOffset];
                from = new PointF(
                    fromRect.X + (fromRect.Width / 2F),
                    fromRect.Y + (fromRect.Height / 2F));
                to = new PointF(
                    toRect.X + (toRect.Width / 2F),
                    toRect.Y + (toRect.Height / 2F));
                return true;
            }

            private void DrawHighlightArrow(Graphics graphics)
            {
                if (highlightedDestination == int.MinValue || cells.Count == 0)
                    return;

                int currentOffset = routeSourceIndex - startIndex;
                int destinationOffset = highlightedDestination - startIndex;
                if (currentOffset < 0 || currentOffset >= cellBounds.Length ||
                    destinationOffset < 0 || destinationOffset >= cellBounds.Length)
                    return;

                Rectangle fromRect = cellBounds[currentOffset];
                Rectangle toRect = cellBounds[destinationOffset];
                PointF from = new PointF(fromRect.X + fromRect.Width / 2F, fromRect.Y + fromRect.Height / 2F);
                PointF to = new PointF(toRect.X + toRect.Width / 2F, toRect.Y + toRect.Height / 2F);

                if (routeSourceIndex == highlightedDestination)
                {
                    using (Pen ringPen = new Pen(Color.FromArgb(235, 255, 255, 255), 4F))
                    {
                        graphics.DrawEllipse(ringPen, Rectangle.Inflate(fromRect, -9, -9));
                    }
                    return;
                }

                using (Pen arrowPen = new Pen(Color.FromArgb(225, 72, 220, 255), 4F))
                {
                    arrowPen.CustomEndCap = new AdjustableArrowCap(5F, 7F, true);
                    graphics.DrawLine(arrowPen, from, to);
                }

                if (string.IsNullOrEmpty(highlightedInput))
                    return;

                SizeF labelSize = graphics.MeasureString(highlightedInput, indexFont);
                float labelX = (from.X + to.X) / 2F - labelSize.Width / 2F;
                float labelY = (from.Y + to.Y) / 2F - labelSize.Height / 2F;
                RectangleF labelBounds = new RectangleF(
                    labelX - 5,
                    labelY - 2,
                    labelSize.Width + 10,
                    labelSize.Height + 4);
                using (SolidBrush background = new SolidBrush(Color.FromArgb(225, 14, 18, 24)))
                using (SolidBrush foreground = new SolidBrush(Color.White))
                {
                    graphics.FillRectangle(background, labelBounds);
                    graphics.DrawString(highlightedInput, indexFont, foreground, labelX, labelY);
                }
            }

            internal static GraphicsPath RoundedRectangle(Rectangle bounds, int radius)
            {
                radius = 2;
                int diameter = radius * 2;
                GraphicsPath path = new GraphicsPath();
                path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
                path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);
                path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
                path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90);
                path.CloseFigure();
                return path;
            }

            private static string Ellipsize(string value, int maxLength)
            {
                if (string.IsNullOrEmpty(value) || value.Length <= maxLength)
                    return value;
                return value.Substring(0, maxLength - 3) + "...";
            }
        }

        private sealed class MoveCard : Control
        {
            private readonly Font inputFont;
            private readonly Font targetFont;
            private bool hovered;

            public InputDefinition Definition;
            public int Destination;
            public LandingSafety Safety;
            public bool IsHovered { get { return hovered; } }

            public event Action<MoveCard> CardHovered;
            public event Action<MoveCard> CardHoverEnded;

            public MoveCard(InputDefinition definition)
            {
                Definition = definition;
                Size = new Size(126, 39);
                Margin = new Padding(3);
                Cursor = Cursors.Hand;
                inputFont = new Font("Consolas", 11F, FontStyle.Bold);
                targetFont = new Font("Consolas", 11F, FontStyle.Bold);
                SetStyle(
                    ControlStyles.AllPaintingInWmPaint |
                    ControlStyles.OptimizedDoubleBuffer |
                    ControlStyles.ResizeRedraw |
                    ControlStyles.UserPaint,
                    true);
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing)
                {
                    inputFont.Dispose();
                    targetFont.Dispose();
                }
                base.Dispose(disposing);
            }

            protected override void OnMouseEnter(EventArgs e)
            {
                base.OnMouseEnter(e);
                hovered = true;
                Invalidate();
                if (CardHovered != null)
                    CardHovered(this);
            }

            protected override void OnMouseLeave(EventArgs e)
            {
                base.OnMouseLeave(e);
                hovered = false;
                Invalidate();
                if (CardHoverEnded != null)
                    CardHoverEnded(this);
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                Rectangle bounds = new Rectangle(1, 1, Width - 3, Height - 3);
                Color fill = SafetyFill(Safety);
                Color border = hovered ? Color.White : SafetyBorder(Safety);

                using (GraphicsPath path = OverflowGridCanvas.RoundedRectangle(bounds, 2))
                using (SolidBrush fillBrush = new SolidBrush(fill))
                using (Pen borderPen = new Pen(border, hovered ? 2.5F : 1.3F))
                {
                    e.Graphics.FillPath(fillBrush, path);
                    e.Graphics.DrawPath(borderPen, path);
                }

                string input = Definition.Name;
                string destination = "#" + Destination;
                using (SolidBrush white = new SolidBrush(Color.White))
                {
                    e.Graphics.DrawString(input, inputFont, white, 7, 8);
                    SizeF targetSize = e.Graphics.MeasureString(destination, targetFont);
                    e.Graphics.DrawString(
                        destination,
                        targetFont,
                        white,
                        Width - targetSize.Width - 7,
                        8);
                }
            }
        }

        public EgoExplorerForm(rac1 game)
        {
            this.game = game;

            Text = "EGO Explorer";
            ClientSize = new Size(1280, 790);
            MinimumSize = new Size(1050, 680);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.FromArgb(7, 8, 8);
            Font = new Font("Consolas", 9F);

            Panel header = new Panel
            {
                Dock = DockStyle.Top,
                Height = 62,
                BackColor = Color.FromArgb(13, 15, 15),
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(10, 7, 10, 6)
            };

            currentIndexLabel = new Label
            {
                AutoSize = true,
                ForeColor = Color.FromArgb(53, 232, 255),
                Font = new Font("Consolas", 15F, FontStyle.Bold),
                Location = new Point(10, 5),
                Text = "EGO // CELL --"
            };
            currentDetailLabel = new Label
            {
                AutoSize = true,
                ForeColor = Color.FromArgb(165, 190, 170),
                Font = new Font("Consolas", 11F, FontStyle.Bold),
                Location = new Point(12, 35),
                Text = "waiting for live menu..."
            };
            liveLabel = new Label
            {
                AutoSize = true,
                ForeColor = Color.FromArgb(255, 82, 94),
                Font = new Font("Consolas", 9F, FontStyle.Bold),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Text = "OFFLINE",
                Location = new Point(ClientSize.Width - 78, 8)
            };
            descriptorSelector = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Width = 112,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(ClientSize.Width - 124, 29),
                Font = new Font("Consolas", 8F)
            };
            descriptorSelector.Items.Add("AUTO");
            descriptorSelector.Items.Add("MAIN");
            descriptorSelector.Items.Add("COMBINED");
            descriptorSelector.SelectedIndex = 0;

            header.Controls.Add(currentIndexLabel);
            header.Controls.Add(currentDetailLabel);
            header.Controls.Add(liveLabel);
            header.Controls.Add(descriptorSelector);

            gridCanvas = new OverflowGridCanvas { Dock = DockStyle.Fill };
            gridCanvas.CellHovered += GridCanvas_CellHovered;
            gridCanvas.CellHoverEnded += GridCanvas_CellHoverEnded;
            gridCanvas.ScrollRequested += GridCanvas_ScrollRequested;
            gridCanvas.CenterRequested += GridCanvas_CenterRequested;

            Panel movePanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(9, 10, 10),
                AutoScroll = true,
                Padding = new Padding(8)
            };

            FlowLayoutPanel moveFlow = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                BackColor = Color.Transparent,
                Padding = new Padding(0)
            };
            moveFlow.Controls.Add(CreateSpatialMoveGroup(
                "D-PAD",
                new int[,]
                {
                    { 5, 0, 6 },
                    { 2, -1, 3 },
                    { 7, 1, 8 }
                }));
            moveFlow.Controls.Add(CreateSpatialMoveGroup(
                "CHORDS",
                new int[,]
                {
                    { 10, 12, 11 },
                    { 4, 14, 9 },
                    { -1, 13, -1 }
                }));
            movePanel.Controls.Add(moveFlow);

            mapSurface = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Vertical,
                FixedPanel = FixedPanel.Panel2,
                IsSplitterFixed = false,
                BackColor = Color.FromArgb(42, 46, 46),
                Size = new Size(1280, 650)
            };
            mapSurface.Panel1MinSize = 590;
            mapSurface.Panel2MinSize = 430;
            mapSurface.SplitterDistance = 800;
            mapSurface.Panel1.Controls.Add(gridCanvas);
            mapSurface.Panel2.Controls.Add(movePanel);

            weaponsTab = new TabPage("WEAPONS")
            {
                BackColor = Color.FromArgb(6, 7, 7),
                ForeColor = Color.FromArgb(190, 210, 195)
            };
            gadgetsTab = new TabPage("GADGETS")
            {
                BackColor = Color.FromArgb(6, 7, 7),
                ForeColor = Color.FromArgb(190, 210, 195)
            };
            itemsTab = new TabPage("ITEMS")
            {
                BackColor = Color.FromArgb(6, 7, 7),
                ForeColor = Color.FromArgb(190, 210, 195)
            };
            slotLogTab = new TabPage("SLOT LOG")
            {
                BackColor = Color.FromArgb(6, 7, 7),
                ForeColor = Color.FromArgb(190, 210, 195)
            };
            weaponsTab.Controls.Add(mapSurface);

            slotLog = new ListView
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(7, 9, 9),
                ForeColor = Color.FromArgb(190, 220, 198),
                BorderStyle = BorderStyle.None,
                Font = new Font("Consolas", 8.5F),
                FullRowSelect = true,
                GridLines = false,
                HeaderStyle = ColumnHeaderStyle.Nonclickable,
                View = View.Details
            };
            slotLog.Columns.Add("TIME", 92);
            slotLog.Columns.Add("MAP", 75);
            slotLog.Columns.Add("SLOT", 70);
            slotLog.Columns.Add("ADDRESS", 100);
            slotLog.Columns.Add("ID", 100);
            slotLog.Columns.Add("CHANGED FIELDS", 210);
            slotLog.Columns.Add("OLD -> NEW", 610);

            Button clearLogButton = new Button
            {
                Dock = DockStyle.Right,
                Width = 84,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(18, 22, 20),
                ForeColor = Color.FromArgb(145, 205, 155),
                Text = "CLEAR"
            };
            clearLogButton.FlatAppearance.BorderColor = Color.FromArgb(65, 90, 70);
            clearLogButton.Click += delegate { slotLog.Items.Clear(); };
            Label logHint = new Label
            {
                Dock = DockStyle.Fill,
                ForeColor = Color.FromArgb(120, 155, 128),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(8, 0, 0, 0),
                Text = "LIVE CHANGES // ANIMATION-ONLY VARIANT TICKS ARE IGNORED"
            };
            Panel logToolbar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 30,
                BackColor = Color.FromArgb(12, 14, 14)
            };
            logToolbar.Controls.Add(logHint);
            logToolbar.Controls.Add(clearLogButton);
            slotLogTab.Controls.Add(slotLog);
            slotLogTab.Controls.Add(logToolbar);

            mapTabs = new TabControl
            {
                Dock = DockStyle.Fill,
                Appearance = TabAppearance.FlatButtons,
                Font = new Font("Consolas", 9F, FontStyle.Bold),
                ItemSize = new Size(120, 25),
                SizeMode = TabSizeMode.Fixed
            };
            mapTabs.TabPages.Add(weaponsTab);
            mapTabs.TabPages.Add(gadgetsTab);
            mapTabs.TabPages.Add(itemsTab);
            mapTabs.TabPages.Add(slotLogTab);
            mapTabs.SelectedIndexChanged += MapTabs_SelectedIndexChanged;

            FlowLayoutPanel legend = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                Height = 30,
                BackColor = Color.FromArgb(11, 12, 12),
                Padding = new Padding(6, 5, 2, 2),
                WrapContents = false
            };
            AddLegendItem(legend, LegitBorder, "LEGIT");
            AddLegendItem(legend, NoItemBorder, "NO ITEM");
            AddLegendItem(legend, SafetyBorder(LandingSafety.Safe), "SAFE / X OFF");
            AddLegendItem(legend, EquippableBorder, "ITEM / X ON");
            AddLegendItem(legend, SafetyBorder(LandingSafety.Caution), "X ACTIVE");
            AddLegendItem(legend, SafetyBorder(LandingSafety.Crash), "CRASH");
            AddLegendItem(legend, InvalidLockedBorder, "INVALID / UNLOCK NO");
            AddLegendItem(legend, InvalidUnlockedBorder, "INVALID / UNLOCK YES");
            AddLegendItem(legend, SafetyBorder(LandingSafety.Unknown), "?");

            StatusStrip status = new StatusStrip
            {
                Dock = DockStyle.Bottom,
                BackColor = Color.FromArgb(18, 20, 20),
                ForeColor = Color.FromArgb(170, 200, 175),
                SizingGrip = false
            };
            statusLabel = new ToolStripStatusLabel
            {
                Text = string.Empty,
                ForeColor = Color.FromArgb(125, 170, 132)
            };
            hoverLabel = new ToolStripStatusLabel
            {
                Spring = true,
                TextAlign = ContentAlignment.MiddleRight,
                ForeColor = Color.FromArgb(200, 220, 205)
            };
            status.Items.Add(statusLabel);
            status.Items.Add(hoverLabel);

            Controls.Add(mapTabs);
            Controls.Add(legend);
            Controls.Add(status);
            Controls.Add(header);

            refreshTimer = new Timer { Interval = 75 };
            refreshTimer.Tick += RefreshTimer_Tick;
            Shown += delegate
            {
                RefreshSnapshot();
                refreshTimer.Start();
            };
            FormClosed += delegate
            {
                refreshTimer.Stop();
                refreshTimer.Dispose();
            };
        }

        private GroupBox CreateSpatialMoveGroup(string title, int[,] positions)
        {
            int rowCount = positions.GetLength(0);
            int columnCount = positions.GetLength(1);
            GroupBox group = new GroupBox
            {
                Text = title,
                ForeColor = Color.FromArgb(125, 170, 132),
                Font = new Font("Consolas", 8F, FontStyle.Bold),
                BackColor = Color.Transparent,
                Width = 404,
                Height = 35 + (rowCount * 49),
                Margin = new Padding(0, 0, 0, 7),
                Padding = new Padding(6)
            };

            TableLayoutPanel cards = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = columnCount,
                RowCount = rowCount,
                BackColor = Color.Transparent,
                Padding = new Padding(1, 4, 1, 1),
                Margin = new Padding(0)
            };
            for (int column = 0; column < columnCount; column++)
                cards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F / columnCount));
            for (int row = 0; row < rowCount; row++)
                cards.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / rowCount));

            for (int row = 0; row < rowCount; row++)
            {
                for (int column = 0; column < columnCount; column++)
                {
                    int definitionIndex = positions[row, column];
                    if (definitionIndex < 0)
                        continue;

                    MoveCard card = new MoveCard(InputDefinitions[definitionIndex])
                    {
                        Dock = DockStyle.Fill
                    };
                    card.CardHovered += MoveCard_Hovered;
                    card.CardHoverEnded += MoveCard_HoverEnded;
                    moveCards.Add(card);
                    cards.Controls.Add(card, column, row);
                }
            }

            group.Controls.Add(cards);
            return group;
        }

        private static void AddLegendItem(FlowLayoutPanel legend, Color color, string text)
        {
            Panel dot = new Panel
            {
                BackColor = color,
                Size = new Size(10, 10),
                Margin = new Padding(5, 3, 4, 0)
            };
            Label label = new Label
            {
                AutoSize = true,
                ForeColor = Color.FromArgb(180, 195, 182),
                Font = new Font("Consolas", 9F, FontStyle.Bold),
                Margin = new Padding(0, 0, 10, 0),
                Text = text
            };
            legend.Controls.Add(dot);
            legend.Controls.Add(label);
        }

        private void MapTabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mapTabs.SelectedTab == weaponsTab)
            {
                selectedMap = ExplorerMap.Weapons;
                weaponsTab.Controls.Add(mapSurface);
                descriptorSelector.Visible = true;
            }
            else if (mapTabs.SelectedTab == gadgetsTab)
            {
                selectedMap = ExplorerMap.Gadgets;
                gadgetsTab.Controls.Add(mapSurface);
                descriptorSelector.Visible = false;
            }
            else if (mapTabs.SelectedTab == itemsTab)
            {
                selectedMap = ExplorerMap.Items;
                itemsTab.Controls.Add(mapSurface);
                descriptorSelector.Visible = false;
            }
            else
            {
                descriptorSelector.Visible = false;
            }

            highlightedDestination = int.MinValue;
            highlightedInput = string.Empty;
            hoveredCellIndex = int.MinValue;
            RefreshSnapshot();
        }

        private void GridCanvas_ScrollRequested(int rowDelta)
        {
            if (selectedMap == ExplorerMap.Weapons)
                weaponScrollRows += rowDelta;
            else if (selectedMap == ExplorerMap.Gadgets)
                gadgetScrollRows += rowDelta;
            else
                itemsScrollRows += rowDelta;
            RefreshSnapshot();
        }

        private void GridCanvas_CenterRequested()
        {
            if (selectedMap == ExplorerMap.Weapons)
                weaponScrollRows = 0;
            else if (selectedMap == ExplorerMap.Gadgets)
                gadgetScrollRows = 0;
            else
                itemsScrollRows = 0;
            RefreshSnapshot();
        }

        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            RefreshSnapshot();
        }

        private void RefreshSnapshot()
        {
            if (game == null || game.api == null)
                return;

            try
            {
                int mainIndex = ReadInt32(MainSelectedIndexAddress);
                int combinedIndex = ReadInt32(CombinedSelectedIndexAddress);
                uint activeDescriptor = ReadActiveDescriptor();
                int weaponIndex = SelectActiveIndex(
                    mainIndex,
                    combinedIndex,
                    activeDescriptor);
                Dictionary<uint, DescriptorState> gadgetDescriptors =
                    ReadGadgetDescriptors();
                DescriptorState gadgetDescriptor = SelectGadgetDescriptor(
                    activeDescriptor,
                    gadgetDescriptors);
                int gadgetIndex = NormalizeGadgetIndex(gadgetDescriptor);
                int itemsIndex = ReadInt32(ItemsSelectedIndexAddress);

                int selectedIndex;
                uint recordBase;
                int columns;
                int legitCount;
                int scrollRows;
                bool gadgetRaggedLayout = false;
                Func<int, DirectionMask, int> resolveDestination;

                if (selectedMap == ExplorerMap.Items)
                {
                    selectedIndex = itemsIndex;
                    recordBase = ItemsRecordBase;
                    columns = ItemsColumns;
                    legitCount = ItemsColumns * ItemsRows;
                    scrollRows = itemsScrollRows;
                    resolveDestination = delegate(int sourceIndex, DirectionMask mask)
                    {
                        return ApplyGridInput(
                            sourceIndex,
                            mask,
                            ItemsColumns,
                            ItemsRows);
                    };
                }
                else if (selectedMap == ExplorerMap.Gadgets)
                {
                    selectedIndex = gadgetIndex;
                    recordBase = HandRecordBase;
                    columns = GadgetColumns;
                    legitCount = GadgetLegitSlots;
                    scrollRows = gadgetScrollRows;
                    gadgetRaggedLayout = true;
                    resolveDestination = delegate(int sourceIndex, DirectionMask mask)
                    {
                        return SimulateGadgetInputFromPhysicalIndex(
                            gadgetDescriptor,
                            gadgetDescriptors,
                            sourceIndex,
                            mask);
                    };
                }
                else
                {
                    selectedIndex = weaponIndex;
                    recordBase = WeaponsRecordBase;
                    columns = WeaponsColumns;
                    legitCount = WeaponsColumns * WeaponsRows;
                    scrollRows = weaponScrollRows;
                    resolveDestination = delegate(int sourceIndex, DirectionMask mask)
                    {
                        return ApplyGridInput(
                            sourceIndex,
                            mask,
                            WeaponsColumns,
                            WeaponsRows);
                    };
                }

                if (selectedIndex < -100000 || selectedIndex > 100000)
                    throw new InvalidOperationException("selected index is outside the guarded range");

                currentIndex = selectedIndex;
                int firstVisualRow;
                int firstIndex;
                int lastIndex;
                if (gadgetRaggedLayout)
                {
                    firstVisualRow =
                        GadgetVisualRow(selectedIndex) -
                        (GridRows / 2) +
                        scrollRows;
                    firstIndex = GadgetFirstIndexForRow(firstVisualRow);
                    lastIndex =
                        GadgetFirstIndexForRow(firstVisualRow + GridRows) - 1;
                }
                else
                {
                    int memoryRowStart = FloorToMultiple(selectedIndex, columns);
                    firstVisualRow = memoryRowStart / columns -
                                     (GridRows / 2) +
                                     scrollRows;
                    firstIndex = memoryRowStart -
                                 ((GridRows / 2) * columns) +
                                 (scrollRows * columns);
                    lastIndex = firstIndex + (GridRows * columns) - 1;
                }
                List<CellInfo> cells = ReadCells(
                    recordBase,
                    firstIndex,
                    lastIndex,
                    selectedMap);
                CellInfo current = FindCell(cells, selectedIndex);
                currentCells = cells;
                gridCanvasStartIndex = firstIndex;
                resolveDestinationFromIndex = resolveDestination;

                gridCanvas.SetLayout(
                    columns,
                    GridRows,
                    legitCount,
                    gadgetRaggedLayout,
                    firstVisualRow);
                UpdateHeader(current, selectedMap);
                int routeSourceIndex =
                    hoveredCellIndex != int.MinValue &&
                    FindCell(cells, hoveredCellIndex) != null
                        ? hoveredCellIndex
                        : selectedIndex;
                Func<DirectionMask, int> activeResolver =
                    delegate(DirectionMask mask)
                    {
                        return resolveDestination(routeSourceIndex, mask);
                    };
                UpdateMoveCards(routeSourceIndex, cells, activeResolver);
                gridCanvas.SetRoutes(
                    BuildRoutes(cells, activeResolver),
                    routeSourceIndex);
                gridCanvas.SetWeaponCursors(
                    selectedMap == ExplorerMap.Weapons,
                    liveMainIndex,
                    liveCombinedIndex,
                    lastMovedWeaponCursor);
                gridCanvas.SetSnapshot(
                    cells,
                    firstIndex,
                    selectedIndex,
                    highlightedDestination,
                    highlightedInput);

                MonitorSlotTables(
                    weaponIndex,
                    gadgetIndex,
                    itemsIndex,
                    selectedMap,
                    firstIndex,
                    lastIndex);

                liveLabel.ForeColor = Color.FromArgb(72, 255, 132);
                liveLabel.Text = "LIVE";
                statusLabel.Text =
                    selectedMap == ExplorerMap.Weapons
                        ? "MAIN = PINK // QS = CYAN // BRIGHT = LAST MOVED // " +
                          "WHEEL: SCROLL // DOUBLE-CLICK: CENTER"
                        : "WHEEL: SCROLL // DOUBLE-CLICK: CENTER";
            }
            catch (Exception ex)
            {
                liveLabel.ForeColor = Color.FromArgb(255, 70, 82);
                liveLabel.Text = "ERROR";
                statusLabel.Text = ex.Message;
            }
        }

        private int SelectActiveIndex(
            int mainIndex,
            int combinedIndex,
            uint activeDescriptor)
        {
            TrackWeaponCursors(mainIndex, combinedIndex, activeDescriptor);

            int mode = descriptorSelector.SelectedIndex;
            if (mode == 1)
                return mainIndex;
            if (mode == 2)
                return combinedIndex;

            WeaponCursorKind activeCursor = CursorForDescriptor(activeDescriptor);
            if (activeCursor == WeaponCursorKind.Main)
                autoUsesCombined = false;
            else if (activeCursor == WeaponCursorKind.Combined)
                autoUsesCombined = true;

            return autoUsesCombined ? combinedIndex : mainIndex;
        }

        private void TrackWeaponCursors(
            int mainIndex,
            int combinedIndex,
            uint activeDescriptor)
        {
            WeaponCursorKind activeCursor = CursorForDescriptor(activeDescriptor);
            if (!hasWeaponCursorSnapshot)
            {
                lastMovedWeaponCursor =
                    activeCursor == WeaponCursorKind.None
                        ? WeaponCursorKind.Main
                        : activeCursor;
                hasWeaponCursorSnapshot = true;
            }
            else
            {
                bool mainMoved = mainIndex != liveMainIndex;
                bool combinedMoved = combinedIndex != liveCombinedIndex;
                if (mainMoved && !combinedMoved)
                    lastMovedWeaponCursor = WeaponCursorKind.Main;
                else if (combinedMoved && !mainMoved)
                    lastMovedWeaponCursor = WeaponCursorKind.Combined;
                else if (mainMoved && combinedMoved &&
                         activeCursor != WeaponCursorKind.None)
                    lastMovedWeaponCursor = activeCursor;
            }

            liveMainIndex = mainIndex;
            liveCombinedIndex = combinedIndex;
        }

        private static WeaponCursorKind CursorForDescriptor(uint descriptor)
        {
            switch (descriptor)
            {
                case MainWeaponsDescriptorAddress:
                case HandDescriptorAddress:
                case BackpackDescriptorAddress:
                case HeadDescriptorAddress:
                case BootsDescriptorAddress:
                    return WeaponCursorKind.Main;
                case CombinedWeaponsDescriptorAddress:
                case CombinedHandDescriptorAddress:
                    return WeaponCursorKind.Combined;
                default:
                    return WeaponCursorKind.None;
            }
        }

        private uint ReadActiveDescriptor()
        {
            uint menuRoot = ReadUInt32(MenuRootPointerAddress);
            if (menuRoot == 0)
                return 0;
            return ReadUInt32(unchecked(menuRoot + 0x40));
        }

        private Dictionary<uint, DescriptorState> ReadGadgetDescriptors()
        {
            Dictionary<uint, DescriptorState> result =
                new Dictionary<uint, DescriptorState>();
            uint[] addresses =
            {
                HandDescriptorAddress,
                BackpackDescriptorAddress,
                HeadDescriptorAddress,
                BootsDescriptorAddress,
                CombinedHandDescriptorAddress
            };

            for (int i = 0; i < addresses.Length; i++)
            {
                DescriptorState descriptor = ReadDescriptor(addresses[i]);
                result.Add(descriptor.Address, descriptor);
            }

            return result;
        }

        private DescriptorState ReadDescriptor(uint address)
        {
            byte[] bytes = game.api.ReadMemory(game.pid, address + 0x30, 0x2C);
            if (!HasBytes(bytes, 0x2C))
                throw new InvalidOperationException(
                    "could not read descriptor 0x" + address.ToString("X8"));

            DescriptorState result = new DescriptorState
            {
                Address = address,
                Flags = ReadUInt32(bytes, 0x00),
                SelectedIndex = unchecked((int)ReadUInt32(bytes, 0x0C)),
                Rows = unchecked((int)ReadUInt32(bytes, 0x10)),
                Columns = unchecked((int)ReadUInt32(bytes, 0x14)),
                RecordBase = ReadUInt32(bytes, 0x18),
                Up = ReadUInt32(bytes, 0x1C),
                Down = ReadUInt32(bytes, 0x20),
                Left = ReadUInt32(bytes, 0x24),
                Right = ReadUInt32(bytes, 0x28)
            };
            if (result.Rows <= 0 || result.Rows > 16 ||
                result.Columns <= 0 || result.Columns > 16 ||
                result.RecordBase == 0)
            {
                throw new InvalidOperationException(
                    "descriptor 0x" + address.ToString("X8") +
                    " is not initialized");
            }
            return result;
        }

        private DescriptorState SelectGadgetDescriptor(
            uint activeDescriptor,
            Dictionary<uint, DescriptorState> descriptors)
        {
            DescriptorState result;
            if (descriptors.TryGetValue(activeDescriptor, out result))
            {
                lastGadgetDescriptor = activeDescriptor;
                return result;
            }

            if (descriptors.TryGetValue(lastGadgetDescriptor, out result))
                return result;
            return descriptors[HandDescriptorAddress];
        }

        private static int NormalizeGadgetIndex(DescriptorState descriptor)
        {
            long byteDelta = (long)descriptor.RecordBase - HandRecordBase;
            return descriptor.SelectedIndex + (int)(byteDelta / RecordSize);
        }

        private static int SimulateGadgetInputFromPhysicalIndex(
            DescriptorState source,
            Dictionary<uint, DescriptorState> descriptors,
            int physicalIndex,
            DirectionMask mask)
        {
            long byteDelta = (long)source.RecordBase - HandRecordBase;
            DescriptorState simulatedSource = new DescriptorState
            {
                Address = source.Address,
                Flags = source.Flags,
                SelectedIndex = physicalIndex - (int)(byteDelta / RecordSize),
                Rows = source.Rows,
                Columns = source.Columns,
                RecordBase = source.RecordBase,
                Up = source.Up,
                Down = source.Down,
                Left = source.Left,
                Right = source.Right
            };
            return SimulateGadgetInput(simulatedSource, descriptors, mask);
        }

        private static int SimulateGadgetInput(
            DescriptorState source,
            Dictionary<uint, DescriptorState> descriptors,
            DirectionMask mask)
        {
            Dictionary<uint, int> selected = new Dictionary<uint, int>();
            foreach (KeyValuePair<uint, DescriptorState> pair in descriptors)
                selected[pair.Key] = pair.Value.SelectedIndex;

            int original = source.SelectedIndex;
            int current = original;
            int quotient = original / source.Columns;
            int column = original - (quotient * source.Columns);
            uint pendingDescriptor = 0;

            if ((mask & DirectionMask.Up) != 0)
            {
                if (quotient == 0)
                {
                    if (source.Up == 0)
                    {
                        if ((source.Flags & 0x8000) == 0)
                            current = original + source.Columns * (source.Rows - 1);
                    }
                    else
                    {
                        pendingDescriptor = source.Up;
                        SetNeighborSelection(
                            selected,
                            descriptors,
                            source.Up,
                            column,
                            source.Columns,
                            true);
                    }
                }
                else
                {
                    current = original - source.Columns;
                }
            }

            if ((mask & DirectionMask.Down) != 0)
            {
                if (quotient + 1 < source.Rows)
                {
                    current += source.Columns;
                }
                else if (source.Down == 0)
                {
                    if ((source.Flags & 0x8000) == 0)
                        current -= source.Columns * (source.Rows - 1);
                }
                else
                {
                    pendingDescriptor = source.Down;
                    SetNeighborSelection(
                        selected,
                        descriptors,
                        source.Down,
                        column,
                        source.Columns,
                        false);
                }
            }

            if ((mask & DirectionMask.Left) != 0)
            {
                if (column == 0)
                {
                    if (source.Left == 0)
                    {
                        if ((source.Flags & 0x8000) == 0)
                            current += source.Columns - 1;
                    }
                    else
                    {
                        pendingDescriptor = source.Left;
                    }
                }
                else
                {
                    current -= 1;
                }
            }

            if ((mask & DirectionMask.Right) != 0)
            {
                if (column + 1 < source.Columns)
                {
                    current += 1;
                }
                else if (source.Right == 0)
                {
                    if ((source.Flags & 0x8000) == 0)
                        current = current - source.Columns + 1;
                }
                else
                {
                    pendingDescriptor = source.Right;
                }
            }

            selected[source.Address] = current;
            uint destinationAddress =
                pendingDescriptor != 0 && descriptors.ContainsKey(pendingDescriptor)
                    ? pendingDescriptor
                    : source.Address;
            DescriptorState destination = descriptors[destinationAddress];
            int destinationLocal = selected[destinationAddress];
            long byteDelta = (long)destination.RecordBase - HandRecordBase;
            return destinationLocal + (int)(byteDelta / RecordSize);
        }

        private static void SetNeighborSelection(
            Dictionary<uint, int> selected,
            Dictionary<uint, DescriptorState> descriptors,
            uint targetAddress,
            int sourceColumn,
            int sourceColumns,
            bool bottomRow)
        {
            DescriptorState target;
            if (!descriptors.TryGetValue(targetAddress, out target))
                return;

            int targetColumn = sourceColumn;
            if (target.Columns == 5 && sourceColumns == 3 && sourceColumn >= 0)
                targetColumn++;
            else if (target.Columns == 3 && sourceColumns == 5 && sourceColumn >= 0)
                targetColumn = Math.Max(0, Math.Min(2, sourceColumn - 1));

            if (targetColumn >= target.Columns)
                targetColumn = target.Columns - 1;
            if (targetColumn < 0)
                targetColumn = 0;

            selected[targetAddress] = bottomRow
                ? (target.Rows - 1) * target.Columns + targetColumn
                : targetColumn;
        }

        private List<CellInfo> ReadCells(
            uint recordBase,
            int firstIndex,
            int lastIndex,
            ExplorerMap map)
        {
            long firstAddressLong = recordBase + ((long)firstIndex * RecordSize);
            long lastAddressLong = recordBase + ((long)lastIndex * RecordSize);
            if (firstAddressLong < 0 || lastAddressLong > uint.MaxValue)
                throw new InvalidOperationException("record window is outside guest memory");

            int count = lastIndex - firstIndex + 1;
            int byteCount = count * RecordSize;
            byte[] records = game.api.ReadMemory(game.pid, (uint)firstAddressLong, (uint)byteCount);
            if (!HasBytes(records, byteCount))
                throw new InvalidOperationException("record window returned too few bytes");

            byte[] unlocks = game.api.ReadMemory(game.pid, UnlockArrayAddress, 36);
            if (!HasBytes(unlocks, 36))
                unlocks = new byte[36];
            byte[] itemFlags = game.api.ReadMemory(
                game.pid,
                ItemsFlagArrayAddress,
                (uint)ItemsFlagNames.Length);
            if (!HasBytes(itemFlags, ItemsFlagNames.Length))
                itemFlags = new byte[ItemsFlagNames.Length];

            List<CellInfo> result = new List<CellInfo>(count);
            for (int i = 0; i < count; i++)
            {
                int index = firstIndex + i;
                int offset = i * RecordSize;
                short itemId = ReadInt16(records, offset + 6);
                short sourceSelector = ReadInt16(records, offset + 4);
                bool usesItemsFlag =
                    map == ExplorerMap.Items && sourceSelector != 0;
                byte stateByte = 0;
                if (usesItemsFlag &&
                    itemId >= 0 &&
                    itemId < itemFlags.Length)
                    stateByte = itemFlags[itemId];
                else if (!usesItemsFlag &&
                         itemId >= 0 &&
                         itemId < unlocks.Length)
                    stateByte = unlocks[itemId];
                result.Add(new CellInfo
                {
                    Index = index,
                    Address = (uint)(firstAddressLong + offset),
                    IconId = ReadInt16(records, offset),
                    IconVariant = ReadInt16(records, offset + 2),
                    SourceSelector = sourceSelector,
                    ItemId = itemId,
                    UsesItemsFlag = usesItemsFlag,
                    StateByte = stateByte,
                    Owned = stateByte != 0,
                    Safety = LandingSafety.Unknown
                });
            }
            AnalyzeLiveCells(result, map);
            return result;
        }

        private void AnalyzeLiveCells(List<CellInfo> cells, ExplorerMap map)
        {
            HashSet<uint> weaponClasses = ReadLiveWeaponClasses();
            Dictionary<short, LiveItemInfo> analyses = new Dictionary<short, LiveItemInfo>();
            bool itemsPreviewUsesRawIndex =
                map == ExplorerMap.Items &&
                (ReadUInt32(ItemsPreviewMonitorFlagsAddress) & 4) != 0;

            for (int i = 0; i < cells.Count; i++)
            {
                CellInfo cell = cells[i];
                if (itemsPreviewUsesRawIndex &&
                    (cell.Index < 0 ||
                     cell.Index >= ItemsColumns * ItemsRows))
                {
                    cell.Safety = LandingSafety.Crash;
                    cell.LiveVerdict = "PREVIEW FILE MISS";
                    cell.LiveDetail =
                        "the live Items monitor uses the raw cursor index for " +
                        "items_ss[index].ps3/.vram; retail only has indices 0..8, " +
                        "and its failed-load path still consumes image metadata";
                    cell.XEffect =
                        "cursor arrival starts the invalid preview load before X";
                    continue;
                }

                if (cell.UsesItemsFlag)
                {
                    cell.GateAddress = unchecked(
                        (uint)((long)ItemsFlagArrayAddress + cell.ItemId));
                    cell.GateByte = cell.StateByte;
                    cell.Safety =
                        cell.Index >= 0 &&
                        cell.Index < ItemsColumns * ItemsRows
                            ? LandingSafety.Safe
                            : LandingSafety.Unknown;
                    cell.LiveVerdict =
                        cell.Safety == LandingSafety.Safe
                            ? "X DISABLED"
                            : "FLAG / CONDITIONAL";
                    cell.LiveDetail = string.Format(
                        "renderer reads special-item flag {0} at 0x{1:X8}; " +
                        "it does not interpret {0} as an equipment ID",
                        cell.ItemId,
                        cell.GateAddress);
                    cell.XEffect =
                        "descriptor flag bit 0 disables confirm; navigation only";
                    continue;
                }

                LiveItemInfo analysis;
                if (!analyses.TryGetValue(cell.ItemId, out analysis))
                {
                    analysis = AnalyzeLiveItem(cell.ItemId, weaponClasses);
                    analyses.Add(cell.ItemId, analysis);
                }

                cell.DefinitionAddress = analysis.DefinitionAddress;
                cell.FakeGroup = analysis.FakeGroup;
                cell.FakeModel = analysis.FakeModel;
                cell.EffectiveModel = analysis.EffectiveModel;
                cell.GateAddress = analysis.GateAddress;
                cell.GateByte = analysis.GateByte;
                cell.Owned = analysis.GateByte != 0;
                cell.ConfirmTarget = analysis.ConfirmTarget;
                cell.PreviewUsesWeaponLoader = analysis.PreviewUsesWeaponLoader;
                cell.WeaponCacheMatch = analysis.WeaponCacheMatch;
                cell.Safety = analysis.Safety;
                cell.LiveVerdict = analysis.Verdict;
                cell.LiveDetail = analysis.Detail;
                cell.XEffect = analysis.XEffect;

                if (map == ExplorerMap.Weapons && cell.Index == -204)
                {
                    cell.DynamicSelectedItemMirror = true;
                    cell.LiveVerdict = analysis.Safety == LandingSafety.Caution
                        ? "MIRROR / X ACTIVE"
                        : analysis.Safety == LandingSafety.Safe
                            ? "LIVE MIRROR"
                            : "MIRROR / " + analysis.Verdict;
                    cell.LiveDetail =
                        "dynamic selected-item scratch record; its ID mirrors the " +
                        "item highlighted by the active equipment grid";
                    cell.XEffect = analysis.XEffect +
                        "; the mirrored ID is used, not address 0x00729F04";
                }

                if (map == ExplorerMap.Items &&
                    cell.Safety != LandingSafety.Crash)
                {
                    if (cell.Safety == LandingSafety.Caution)
                        cell.Safety = LandingSafety.Safe;
                    cell.LiveVerdict = cell.Safety == LandingSafety.Unknown
                        ? "X DISABLED / CONDITIONAL"
                        : "X DISABLED";
                    cell.XEffect =
                        "descriptor flag bit 0 disables confirm; navigation only";
                    cell.LiveDetail =
                        analysis.Detail + "; X is disabled by the Items descriptor";
                }
            }
        }

        private HashSet<uint> ReadLiveWeaponClasses()
        {
            HashSet<uint> classes = new HashSet<uint>();
            byte[] countBytes = game.api.ReadMemory(game.pid, WeaponClassCount, 4);
            if (!HasBytes(countBytes, 4))
                return classes;

            uint count = ReadUInt32(countBytes, 0);
            if (count == 0 || count > 0x100)
                return classes;

            byte[] classBytes = game.api.ReadMemory(game.pid, WeaponClassArray, count * 4);
            if (!HasBytes(classBytes, (int)count * 4))
                return classes;

            for (int i = 0; i < count; i++)
                classes.Add(ReadUInt32(classBytes, i * 4));
            return classes;
        }

        private LiveItemInfo AnalyzeLiveItem(short itemId, HashSet<uint> weaponClasses)
        {
            LiveItemInfo result = new LiveItemInfo();
            result.DefinitionAddress = unchecked((uint)((long)ItemDefinitionBase +
                                                        ((long)itemId * ItemDefinitionSize)));
            result.GateAddress = unchecked((uint)((long)UnlockArrayAddress + itemId));

            byte[] definition = game.api.ReadMemory(game.pid, result.DefinitionAddress, 12);
            byte[] gate = game.api.ReadMemory(game.pid, result.GateAddress, 1);
            if (!HasBytes(definition, 12) || !HasBytes(gate, 1))
            {
                result.Safety = LandingSafety.Unknown;
                result.Verdict = "CONDITIONAL";
                result.Detail = "live definition or ownership gate could not be read";
                result.XEffect = "unknown";
                return result;
            }

            result.FakeGroup = ReadUInt32(definition, 0);
            result.FakeModel = ReadUInt32(definition, 8);
            result.EffectiveModel = itemId == 24 ? 0x1DFU : result.FakeModel;
            result.GateByte = gate[0];
            result.ConfirmTarget = unchecked(EquippedItemBase + (result.FakeGroup * 4));
            result.PreviewUsesWeaponLoader =
                itemId != 24 &&
                result.FakeGroup != 1 &&
                result.FakeGroup != 2 &&
                result.FakeGroup != 3;
            result.WeaponCacheMatch = weaponClasses.Contains(result.EffectiveModel);

            bool realItem = itemId >= 1 && itemId < ItemNames.Length;
            bool confirmActive = itemId != 0 && result.GateByte != 0;
            result.XEffect = confirmActive
                ? string.Format(
                    "ACTIVE: group 0x{0:X8} stages ID {1} at 0x{2:X8}",
                    result.FakeGroup,
                    itemId,
                    result.ConfirmTarget)
                : (itemId == 0
                    ? "inactive: item ID 0"
                    : string.Format("gated off by byte 0x{0:X8}", result.GateAddress));

            if (result.EffectiveModel == uint.MaxValue)
            {
                SetSurvivableRisk(
                    result,
                    confirmActive,
                    "model -1 suppresses preview");
                return result;
            }

            if (result.EffectiveModel >= OClassToMClassLength)
            {
                result.Safety = LandingSafety.Crash;
                result.Verdict = "CRASH";
                result.Detail = string.Format(
                    "model 0x{0:X8} indexes outside the 0x800-byte oClass map; X is unreachable",
                    result.EffectiveModel);
                return result;
            }

            if (result.PreviewUsesWeaponLoader && !result.WeaponCacheMatch)
            {
                result.Safety = LandingSafety.Crash;
                result.Verdict = "CRASH";
                result.Detail = string.Format(
                    "model 0x{0:X8} misses the live weapon cache; X is unreachable",
                    result.EffectiveModel);
                return result;
            }

            byte[] mappingBytes = game.api.ReadMemory(
                game.pid,
                OClassToMClassTable + result.EffectiveModel,
                1);
            if (!HasBytes(mappingBytes, 1))
            {
                result.Safety = LandingSafety.Unknown;
                result.Verdict = "CONDITIONAL";
                result.Detail = "live oClass mapping could not be read";
                return result;
            }

            byte mapping = mappingBytes[0];
            if (!result.PreviewUsesWeaponLoader && mapping == 0xFF)
            {
                SetSurvivableRisk(
                    result,
                    confirmActive,
                    "0xFF oClass mapping suppresses model spawn");
                return result;
            }

            if (realItem)
            {
                SetSurvivableRisk(
                    result,
                    confirmActive,
                    "retail item preview prerequisites pass");
                return result;
            }

            result.Safety = confirmActive
                ? LandingSafety.Caution
                : LandingSafety.Unknown;
            result.Verdict = confirmActive ? "X ACTIVE?" : "CONDITIONAL";
            result.Detail = string.Format(
                "fake model 0x{0:X8} passes primary bounds, but later preview metadata is unverified",
                result.EffectiveModel);
            return result;
        }

        private static void SetSurvivableRisk(
            LiveItemInfo result,
            bool confirmActive,
            string landingDetail)
        {
            result.Safety = confirmActive
                ? LandingSafety.Caution
                : LandingSafety.Safe;
            result.Verdict = confirmActive ? "X ACTIVE" : "NAV SAFE";
            result.Detail = landingDetail + "; " + result.XEffect;
        }

        private void MonitorSlotTables(
            int weaponIndex,
            int gadgetIndex,
            int itemsIndex,
            ExplorerMap visibleMap,
            int visibleFirstIndex,
            int visibleLastIndex)
        {
            ObserveSlotRange("WEAPON", WeaponsRecordBase, 0, 15);
            ObserveSlotRange("GADGET", HandRecordBase, 0, GadgetLegitSlots);
            ObserveSlotRange(
                "ITEMS",
                ItemsRecordBase,
                0,
                ItemsColumns * ItemsRows);

            int weaponFirst =
                FloorToMultiple(weaponIndex, WeaponsColumns) -
                ((GridRows / 2) * WeaponsColumns);
            ObserveSlotRange(
                "WEAPON",
                WeaponsRecordBase,
                weaponFirst,
                GridRows * WeaponsColumns);

            int gadgetFirst =
                FloorToMultiple(gadgetIndex, GadgetColumns) -
                ((GridRows / 2) * GadgetColumns);
            ObserveSlotRange(
                "GADGET",
                HandRecordBase,
                gadgetFirst,
                GridRows * GadgetColumns);

            int itemsFirst =
                FloorToMultiple(itemsIndex, ItemsColumns) -
                ((GridRows / 2) * ItemsColumns);
            ObserveSlotRange(
                "ITEMS",
                ItemsRecordBase,
                itemsFirst,
                GridRows * ItemsColumns);

            ObserveSlotRange(
                WatchTableName(visibleMap),
                RecordBaseForMap(visibleMap),
                visibleFirstIndex,
                visibleLastIndex - visibleFirstIndex + 1);
        }

        private static string WatchTableName(ExplorerMap map)
        {
            switch (map)
            {
                case ExplorerMap.Gadgets:
                    return "GADGET";
                case ExplorerMap.Items:
                    return "ITEMS";
                default:
                    return "WEAPON";
            }
        }

        private static uint RecordBaseForMap(ExplorerMap map)
        {
            switch (map)
            {
                case ExplorerMap.Gadgets:
                    return HandRecordBase;
                case ExplorerMap.Items:
                    return ItemsRecordBase;
                default:
                    return WeaponsRecordBase;
            }
        }

        private void ObserveSlotRange(
            string table,
            uint recordBase,
            int firstIndex,
            int count)
        {
            if (count <= 0 || count > 4096)
                return;

            long firstAddressLong = recordBase + ((long)firstIndex * RecordSize);
            long byteCountLong = (long)count * RecordSize;
            if (firstAddressLong < 0 ||
                firstAddressLong + byteCountLong > uint.MaxValue)
                return;

            byte[] bytes;
            try
            {
                bytes = game.api.ReadMemory(
                    game.pid,
                    (uint)firstAddressLong,
                    (uint)byteCountLong);
            }
            catch
            {
                return;
            }

            if (!HasBytes(bytes, (int)byteCountLong))
                return;

            for (int i = 0; i < count; i++)
            {
                int index = firstIndex + i;
                int offset = i * RecordSize;
                uint address = (uint)(firstAddressLong + offset);
                uint tableKey = table == "WEAPON"
                    ? 1U
                    : table == "GADGET"
                        ? 2U
                        : 3U;
                ulong key = ((ulong)tableKey << 32) | address;
                byte[] previous;
                if (!watchedSlotBytes.TryGetValue(key, out previous))
                {
                    watchedSlotBytes[key] = CopyRecord(bytes, offset);
                    continue;
                }

                bool changed = false;
                bool animationOnly = true;
                for (int byteIndex = 0; byteIndex < RecordSize; byteIndex++)
                {
                    if (previous[byteIndex] == bytes[offset + byteIndex])
                        continue;
                    changed = true;
                    if (byteIndex < 2 || byteIndex > 3)
                        animationOnly = false;
                }

                if (!changed)
                    continue;

                byte[] current = CopyRecord(bytes, offset);
                watchedSlotBytes[key] = current;
                if (animationOnly)
                    continue;

                AddSlotLogEntry(
                    table,
                    index,
                    address,
                    previous,
                    current);
            }
        }

        private void AddSlotLogEntry(
            string table,
            int index,
            uint address,
            byte[] previous,
            byte[] current)
        {
            short oldId = ReadInt16(previous, 6);
            short newId = ReadInt16(current, 6);
            List<string> fields = new List<string>();
            if (previous[0] != current[0] || previous[1] != current[1])
                fields.Add("ICON");
            if (previous[2] != current[2] || previous[3] != current[3])
                fields.Add("VARIANT");
            if (previous[4] != current[4] || previous[5] != current[5])
                fields.Add("SOURCE");
            if (oldId != newId)
                fields.Add("ID");
            if (previous[8] != current[8] || previous[9] != current[9])
                fields.Add("TAIL");

            ListViewItem item = new ListViewItem(
                DateTime.Now.ToString("HH:mm:ss.fff"));
            item.SubItems.Add(table);
            item.SubItems.Add("#" + index);
            item.SubItems.Add("0x" + address.ToString("X8"));
            item.SubItems.Add(oldId + " -> " + newId);
            item.SubItems.Add(string.Join("+", fields.ToArray()));
            item.SubItems.Add(
                RecordHex(previous) + " -> " + RecordHex(current));
            item.ForeColor = oldId != newId
                ? Color.FromArgb(255, 190, 70)
                : Color.FromArgb(170, 205, 178);
            slotLog.Items.Insert(0, item);
            while (slotLog.Items.Count > 2000)
                slotLog.Items.RemoveAt(slotLog.Items.Count - 1);
        }

        private static byte[] CopyRecord(byte[] source, int offset)
        {
            byte[] result = new byte[RecordSize];
            Buffer.BlockCopy(source, offset, result, 0, RecordSize);
            return result;
        }

        private static string RecordHex(byte[] record)
        {
            return BitConverter.ToString(record).Replace("-", string.Empty);
        }

        private void UpdateHeader(CellInfo current, ExplorerMap map)
        {
            if (current == null)
            {
                currentIndexLabel.Text = string.Format(
                    "EGO // {0} // LIVE CELL #{1}",
                    MapName(map),
                    currentIndex);
                currentDetailLabel.Text =
                    "Live cursor is outside the scrolled window // double-click to center";
                return;
            }

            currentIndexLabel.Text = string.Format(
                "EGO // {0} // CELL #{1} // {2}",
                MapName(map),
                current.Index,
                CellDisplayName(current));
            string state =
                current.Safety == LandingSafety.Crash
                    ? "CRASH"
                    : current.Safety == LandingSafety.Unknown
                        ? "UNKNOWN"
                        : IsNoItemCell(current)
                            ? "EMPTY"
                            : IsEquippableItemCell(current)
                                ? "X ON"
                                : current.Safety == LandingSafety.Safe
                                    ? "SAFE"
                                    : "X ACTIVE";
            currentDetailLabel.Text =
                CellMetaLine(current) + " // " + state;
        }

        private static string MapName(ExplorerMap map)
        {
            switch (map)
            {
                case ExplorerMap.Gadgets:
                    return "GADGETS";
                case ExplorerMap.Items:
                    return "ITEMS";
                default:
                    return "WEAPONS";
            }
        }

        private void UpdateMoveCards(
            int sourceIndex,
            List<CellInfo> cells,
            Func<DirectionMask, int> resolveDestination)
        {
            for (int i = 0; i < moveCards.Count; i++)
            {
                MoveCard card = moveCards[i];
                int destination = resolveDestination(card.Definition.Mask);
                CellInfo destinationCell = FindCell(cells, destination);
                card.Destination = destination;
                card.Safety = destinationCell == null
                    ? LandingSafety.Unknown
                    : destinationCell.Safety;
                if (card.IsHovered)
                {
                    highlightedDestination = destination;
                    highlightedInput = card.Definition.Name;
                }
                card.Invalidate();
            }
        }

        private static List<RouteInfo> BuildRoutes(
            List<CellInfo> cells,
            Func<DirectionMask, int> resolveDestination)
        {
            List<RouteInfo> routes = new List<RouteInfo>();
            Dictionary<int, RouteInfo> byDestination = new Dictionary<int, RouteInfo>();
            for (int i = 0; i < InputDefinitions.Length; i++)
            {
                InputDefinition definition = InputDefinitions[i];
                int destination = resolveDestination(definition.Mask);
                RouteInfo route;
                if (!byDestination.TryGetValue(destination, out route))
                {
                    route = new RouteInfo
                    {
                        Destination = destination,
                        Inputs = definition.Name,
                        Safety = FindCell(cells, destination) == null
                            ? LandingSafety.Unknown
                            : FindCell(cells, destination).Safety
                    };
                    byDestination.Add(destination, route);
                    routes.Add(route);
                }
                else
                {
                    route.Inputs += " / " + definition.Name;
                }
            }
            return routes;
        }

        private void MoveCard_Hovered(MoveCard card)
        {
            highlightedDestination = card.Destination;
            highlightedInput = card.Definition.Name;
            gridCanvas.SetSnapshot(
                GetCurrentCells(),
                gridCanvasStartIndex,
                currentIndex,
                highlightedDestination,
                highlightedInput);

            hoverLabel.Text = string.Format(
                "{0} -> #{1} // {2}",
                card.Definition.Name,
                card.Destination,
                SafetyLabel(card.Safety));
        }

        private void MoveCard_HoverEnded(MoveCard card)
        {
            highlightedDestination = int.MinValue;
            highlightedInput = string.Empty;
            gridCanvas.SetSnapshot(
                GetCurrentCells(),
                gridCanvasStartIndex,
                currentIndex,
                highlightedDestination,
                highlightedInput);
            hoverLabel.Text = string.Empty;
        }

        private List<CellInfo> currentCells = new List<CellInfo>();
        private int gridCanvasStartIndex;

        private List<CellInfo> GetCurrentCells()
        {
            return currentCells;
        }

        private void GridCanvas_CellHovered(CellInfo cell)
        {
            hoveredCellIndex = cell.Index;
            highlightedDestination = int.MinValue;
            highlightedInput = string.Empty;

            if (resolveDestinationFromIndex == null)
                return;

            Func<DirectionMask, int> hoveredResolver =
                delegate(DirectionMask mask)
                {
                    return resolveDestinationFromIndex(cell.Index, mask);
                };
            UpdateMoveCards(cell.Index, GetCurrentCells(), hoveredResolver);
            gridCanvas.SetRoutes(
                BuildRoutes(GetCurrentCells(), hoveredResolver),
                cell.Index);
            gridCanvas.SetSnapshot(
                GetCurrentCells(),
                gridCanvasStartIndex,
                currentIndex,
                highlightedDestination,
                highlightedInput);
            hoverLabel.Text =
                "FROM #" + cell.Index + " // " + CellDisplayName(cell);
        }

        private void GridCanvas_CellHoverEnded()
        {
            hoveredCellIndex = int.MinValue;
            highlightedDestination = int.MinValue;
            highlightedInput = string.Empty;
            if (resolveDestinationFromIndex != null)
            {
                Func<DirectionMask, int> currentResolver =
                    delegate(DirectionMask mask)
                    {
                        return resolveDestinationFromIndex(currentIndex, mask);
                    };
                UpdateMoveCards(currentIndex, GetCurrentCells(), currentResolver);
                gridCanvas.SetRoutes(
                    BuildRoutes(GetCurrentCells(), currentResolver),
                    currentIndex);
            }
            gridCanvas.SetSnapshot(
                GetCurrentCells(),
                gridCanvasStartIndex,
                currentIndex,
                highlightedDestination,
                highlightedInput);
            hoverLabel.Text = string.Empty;
        }

        private int ReadInt32(uint address)
        {
            return unchecked((int)ReadUInt32(address));
        }

        private uint ReadUInt32(uint address)
        {
            byte[] bytes = game.api.ReadMemory(game.pid, address, 4);
            if (!HasBytes(bytes, 4))
                throw new InvalidOperationException("could not read 0x" + address.ToString("X8"));
            return ReadUInt32(bytes, 0);
        }

        private static short ReadInt16(byte[] data, int offset)
        {
            ushort value = (ushort)((data[offset] << 8) | data[offset + 1]);
            return unchecked((short)value);
        }

        private static uint ReadUInt32(byte[] data, int offset)
        {
            return ((uint)data[offset] << 24) |
                   ((uint)data[offset + 1] << 16) |
                   ((uint)data[offset + 2] << 8) |
                   data[offset + 3];
        }

        private static bool HasBytes(byte[] data, int count)
        {
            return data != null && data.Length >= count;
        }

        private static CellInfo FindCell(List<CellInfo> cells, int index)
        {
            for (int i = 0; i < cells.Count; i++)
            {
                if (cells[i].Index == index)
                    return cells[i];
            }
            return null;
        }

        private static int FloorToMultiple(int value, int multiple)
        {
            int remainder = value % multiple;
            if (remainder < 0)
                remainder += multiple;
            return value - remainder;
        }

        private static int GadgetVisualRow(int index)
        {
            if (index < BootsPhysicalStart)
                return FloorToMultiple(index, GadgetColumns) / GadgetColumns;
            return GadgetThreeColumnRows +
                   ((index - BootsPhysicalStart) / 2);
        }

        private static int GadgetVisualColumn(int index)
        {
            int row = GadgetVisualRow(index);
            return index - GadgetFirstIndexForRow(row);
        }

        private static int GadgetFirstIndexForRow(int row)
        {
            if (row < GadgetThreeColumnRows)
                return row * GadgetColumns;
            return BootsPhysicalStart +
                   ((row - GadgetThreeColumnRows) * 2);
        }

        private static int ApplyGridInput(
            int index,
            DirectionMask mask,
            int columns,
            int rows)
        {
            int row = index / columns;
            int column = index % columns;
            int delta = 0;

            if ((mask & DirectionMask.Up) != 0)
                delta += row == 0 ? (rows - 1) * columns : -columns;

            if ((mask & DirectionMask.Down) != 0)
                delta += row + 1 < rows ? columns : -(rows - 1) * columns;

            if ((mask & DirectionMask.Left) != 0)
                delta += column == 0 ? columns - 1 : -1;

            if ((mask & DirectionMask.Right) != 0)
                delta += column + 1 < columns ? 1 : -(columns - 1);

            return index + delta;
        }

        private static bool IsNoItemCell(CellInfo cell)
        {
            return !cell.UsesItemsFlag &&
                   cell.ItemId == 0 &&
                   cell.Safety != LandingSafety.Crash;
        }

        private static string CellDisplayName(CellInfo cell)
        {
            if (!cell.UsesItemsFlag && cell.ItemId == 0)
                return "EMPTY";
            if (!cell.UsesItemsFlag &&
                (cell.ItemId < 0 || cell.ItemId >= ItemNames.Length))
                return "INVALID ID " + cell.ItemId;
            return cell.ItemName.ToUpperInvariant();
        }

        private static bool IsInvalidItemId(CellInfo cell)
        {
            return !cell.UsesItemsFlag &&
                   (cell.ItemId < 0 || cell.ItemId >= ItemNames.Length);
        }

        private static string CellMetaLine(CellInfo cell)
        {
            if (cell.UsesItemsFlag)
                return "FLAG ID " + cell.ItemId;

            string group = cell.FakeGroup <= 999
                ? cell.FakeGroup.ToString()
                : "0x" + cell.FakeGroup.ToString("X8");
            if (IsInvalidItemId(cell))
            {
                return string.Format(
                    "GROUP {0}   UNLOCK {1}",
                    group,
                    cell.GateByte != 0 ? "YES" : "NO");
            }

            return "ITEM ID " + cell.ItemId + "   GROUP " + group;
        }

        private static bool IsEquippableItemCell(CellInfo cell)
        {
            return !cell.UsesItemsFlag &&
                   cell.ItemId > 0 &&
                   cell.ItemId < ItemNames.Length &&
                   cell.Owned &&
                   cell.Safety == LandingSafety.Caution;
        }

        private static string SafetyLabel(LandingSafety safety)
        {
            switch (safety)
            {
                case LandingSafety.Safe:
                    return "LIVE SAFE / X GATED";
                case LandingSafety.Caution:
                    return "LIVE X ACTIVE";
                case LandingSafety.Crash:
                    return "LIVE PREVIEW CRASH";
                default:
                    return "LIVE CONDITIONAL";
            }
        }

        private static Color SafetyFill(LandingSafety safety)
        {
            switch (safety)
            {
                case LandingSafety.Safe:
                    return Color.FromArgb(8, 42, 24);
                case LandingSafety.Caution:
                    return Color.FromArgb(58, 39, 0);
                case LandingSafety.Crash:
                    return Color.FromArgb(58, 10, 16);
                default:
                    return Color.FromArgb(24, 27, 24);
            }
        }

        private static Color SafetyBorder(LandingSafety safety)
        {
            switch (safety)
            {
                case LandingSafety.Safe:
                    return Color.FromArgb(72, 255, 132);
                case LandingSafety.Caution:
                    return Color.FromArgb(255, 178, 0);
                case LandingSafety.Crash:
                    return Color.FromArgb(255, 63, 78);
                default:
                    return Color.FromArgb(102, 115, 104);
            }
        }
    }
}
