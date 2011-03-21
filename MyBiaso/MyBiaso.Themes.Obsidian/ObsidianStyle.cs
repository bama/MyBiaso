using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using AiFrame.InterfaceLib.Windows.Themes;

namespace MyBiaso.Themes.Obsidian {
    /// <summary>
    /// Diese Klasse implementiert das Obsidian-Theme.
    /// Für weitere Beschreibungen bitte bei ITheme nachschauen.
    /// </summary>
    public class ObsidianStyle : ITheme {
        private readonly Font _headerFont =
                new Font("Arial", 14, FontStyle.Bold);
        private readonly Font _navBarButtonFont =
                new Font("Microsoft Sans Serif", 8, FontStyle.Regular);
        private readonly Font _dataWindowHeaderFont =
                new Font("Arial", 14, FontStyle.Bold);
        private readonly Font _dataNavBarButtonFont =
                new Font("Microsoft Sans Serif", 7, FontStyle.Regular);


        public Font GetNavBarHeaderFont() {
            return _headerFont;
        }

        public Image GetNavBarHeaderImage() {
            Bitmap bitmap = new Bitmap(186, 24);

            using (Graphics graphics = Graphics.FromImage(bitmap)) {
                using (LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, 186, 24), Color.FromArgb(216, 232, 255), Color.FromArgb(175, 210, 255), LinearGradientMode.Horizontal)) { graphics.FillRectangle(brush, new Rectangle(0, 0, 186, 24)); }

            }
            return bitmap;
        }

        public Image GetNavBarFooterImage() {
            return GetNavBarHeaderImage();
        }

        public Font GetNavBarButtonFont() {
            return _navBarButtonFont;
        }

        public Color GetNavBarButtonFontColor() {
            return Color.Black;
        }

        public Image GetNavBarButtonImageNormal() {
            Bitmap bitmap = new Bitmap(186, 24);

            using (Graphics graphics = Graphics.FromImage(bitmap)) {
                using (LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, 186, 24), Color.FromArgb(207, 227, 255), Color.FromArgb(175, 210, 255), LinearGradientMode.Vertical)) { graphics.FillRectangle(brush, new Rectangle(0, 0, 186, 24)); }
            }
            return bitmap;
        }

        public Image GetNavBarButtonImageHovered() {
            Bitmap bitmap = new Bitmap(186, 24);

            using (Graphics graphics = Graphics.FromImage(bitmap)) {
                using (LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, 186, 24), Color.FromArgb(207, 227, 255), Color.FromArgb(96, 166, 255), LinearGradientMode.Vertical)) { graphics.FillRectangle(brush, new Rectangle(0, 0, 186, 24)); }
            }
            return bitmap;
        }

        public Image GetNavBarButtonImagePressed() {
            Bitmap bitmap = new Bitmap(186, 24);

            using (Graphics graphics = Graphics.FromImage(bitmap)) {
                using (LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, 186, 24), Color.FromArgb(255, 223, 117), Color.FromArgb(254, 178, 70), LinearGradientMode.Vertical)) { graphics.FillRectangle(brush, new Rectangle(0, 0, 186, 24)); }
            }
            return bitmap;
        }

        public Color GetNavBarContentArea() {
            return Color.White;
        }

        public Image GetHeaderImageSpace() {
            return null;
        }

        public Image GetDataWindowHeaderIcon() {
            return null;
        }

        public Image GetListHeaderIcon() {
            return null;
        }

        public Color GetPanelBeginColor() {
            return Color.White;
        }

        public Color GetPanelEndColor() {
            return Color.White;
        }

        public ToolStripRenderer GetProfessionalColorTable() {
            return new ObsidianRenderer();
        }

        public Image GetMainHeaderImage() {
            return null;
        }

        public Color GetMainHeaderFontColor() {
            return Color.Black;
        }

        public Font GetMainHeaderFont() {
            return _headerFont;
        }

        public Point GetMainHeaderHeadlinePosition() {
            return new Point(120, 30);
        }

        public Point GetMainHeaderLogoPosition() {
            return new Point(10, 5);
        }

        public Image GetProgramHeader() {
            return null;
        }

        public Image GetDataWindowHeader() {
            return null;
        }

        public Color GetDataWindowHeaderFontColor() {
            return Color.Black;
        }

        public Font GetDataWindowHeaderFont() {
            return _dataWindowHeaderFont;
        }

        public Image GetDataWindowBgLogo() {
            return null;
        }

        #region Implementation of IDataNavigationBarTheme

        public Image GetDataNavBarHeaderImage() {
            return null;
        }

        public Color GetDataNavBarGradientStartColor() {
            return Color.White;
            //            return Color.FromArgb(250, 249, 244);
        }

        public Color GetDataNavBarGradientEndColor() {
            return Color.White;
            //            return Color.FromArgb(250, 249, 244);
        }

        public Font GetDataNavBarButtonFont() {
            return _dataNavBarButtonFont;
        }

        public Color GetDataNavBarButtonFontColor() {
            return Color.Black;
        }

        #endregion

        public Color GetDataGridBackgroundColor() {
            return Color.FromArgb(250, 249, 244);
        }
    }
}
