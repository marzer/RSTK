using Marzersoft;
using Marzersoft.Themes;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace RSTK
{
    /// <summary>
    /// Base class for controls taking up one row in the settings section of the RSTK window.
    /// </summary>
    public abstract class SettingsRow : UserControl
    {
        /////////////////////////////////////////////////////////////////////
        // PROPERTIES/VARIABLES
        /////////////////////////////////////////////////////////////////////

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public readonly bool IsDesignMode;

        private Panel panContents;

        /// <summary>
        /// Text displayed to the left of the control.
        /// </summary>
        [Browsable(true),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
            DefaultValue(""),
            Category("Settings Row")]
        public string Caption
        {
            get { return caption; }
            set
            {
                if ((value = (value ?? "").Trim()).Equals(caption))
                    return;
                caption = value;
                if (IsDesignMode)
                    Refresh();
                else
                    Invalidate(new Rectangle(30, 0, 150, vertical ? 30 : Height));
            }
        }
        private string caption = "";

        /// <summary>
        /// Icon shown on the control's left edge.
        /// </summary>
        [Browsable(false),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Image Image
        {
            get { return image; }
            set
            {
                if (image != value)
                {
                    image = value;
                    Invalidate(new Rectangle(0, 0, 30, vertical ? 30 : Height));
                }
            }
        }
        private Image image = null;

        protected const int ImageWidth = 30;
        protected const int TextWidth = 230;
        protected const float DisabledAlpha = 0.3f;

        /// <summary>
        /// Is this vertically-oriented
        /// (content appears below the caption)?
        /// </summary>
        [Browsable(true),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
            Category("Settings Row"),
            DefaultValue(false)]
        public bool Vertical
        {
            get { return vertical; }
            set
            {
                if (vertical == value)
                    return;

                vertical = value;
                panContents.SuspendLayout();
                panContents.Bounds = vertical ?
                    Rectangle.FromLTRB(0, 30, Width, Height)
                    : Rectangle.FromLTRB(ImageWidth + TextWidth + 5, 0, Width, Height);
                panContents.ResumeLayout(true);
                Refresh();
            }
        }
        private bool vertical = false;

        /////////////////////////////////////////////////////////////////////
        // CONSTRUCTOR
        /////////////////////////////////////////////////////////////////////

        public SettingsRow()
        {
            IsDesignMode = DesignMode || this.DetectDesignMode();

            SuspendLayout();

            Margin = new Padding(0);
            Padding = new Padding(0);
            Height = 30;

            Controls.Add(panContents = new Panel());
            panContents.Margin = new Padding(0);
            panContents.Padding = new Padding(0);
            panContents.Bounds = Rectangle.FromLTRB(ImageWidth + TextWidth + 5, 0, Width, Height);
            panContents.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            panContents.TabStop = true;

            panContents.SuspendLayout();
            Initialize(panContents);
            panContents.ResumeLayout(true);

            ResumeLayout(false);

            if (!IsDesignMode)
                DoubleBuffered = true;
        }

        protected abstract void Initialize(Panel contents);

        /////////////////////////////////////////////////////////////////////
        // PAINTING
        /////////////////////////////////////////////////////////////////////

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (!((image != null && !IsDesignMode) || caption.Length > 0))
                return;

            e.Graphics.AsQuality(GraphicsQuality.High, (g) =>
            {
                if (image != null && !IsDesignMode)
                {
                    using (ImageAttributes ia = new ImageAttributes())
                    {
                        ColorMatrix cm = new ColorMatrix();
                        if (!Enabled)
                            cm = cm.SetAlpha(DisabledAlpha);
                        ia.SetColorMatrix(cm);

                        g.DrawImage(image,
                            new Rectangle((ImageWidth / 2) - image.Width / 2,
                                vertical ? 0 : Height / 2 - image.Height / 2,
                                image.Width, image.Height),
                            0, 0, image.Width, image.Height,
                            GraphicsUnit.Pixel, ia);
                    }
                }

                if (caption.Length > 0)
                {
                    using (var sf = new StringFormat())
                    {
                        sf.Alignment = StringAlignment.Far;
                        sf.LineAlignment = StringAlignment.Center;
                        sf.Trimming = StringTrimming.EllipsisWord;
                        g.DrawString(caption, Font,
                            Color.FromArgb(Enabled ? 255 : DisabledAlpha.Project(0,255), ForeColor),
                            new Rectangle(ImageWidth, 0, TextWidth, vertical ? 30 : Height), sf);
                    }
                }
            });
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            this.Refresh();
        }
    }

    /// <summary>
    /// Combo box row for the settings section of the RSTK window.
    /// </summary>
    public class ComboBoxRow : SettingsRow
    {
        [Browsable(true),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
            Category("Settings Row")]
        public ComboBox ComboBox { get; private set; }

        /// <summary>
        /// Alias for ComboBox.SelectedIndex.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectedIndex
        {
            get { return ComboBox.SelectedIndex; }
            set { ComboBox.SelectedIndex = value; }
        }

        protected override void Initialize(Panel contents)
        {
            contents.Controls.Add(ComboBox = new ThemedComboBox());
            ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBox.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
            PositionControl(contents, null);
            contents.SizeChanged += PositionControl;
            ComboBox.MouseWheel += (s, e) =>
            {
                if (!ComboBox.DroppedDown)
                    ((HandledMouseEventArgs)e).Handled = true;
            };
        }

        private void PositionControl(object sender, EventArgs args)
        {
            ComboBox.Width = ComboBox.Parent.ClientRectangle.Width;
            ComboBox.CenterInParent();
        }
    }

    /// <summary>
    /// Track bar row for the settings section of the RSTK window.
    /// </summary>
    public class TrackBarRow : SettingsRow
    {
        [Browsable(true),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
            Category("Settings Row")]
        public TrackBar TrackBar { get; private set; }

        /// <summary>
        /// Format string used to determine how the display label renders values.
        /// </summary>
        [Browsable(true),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
            DefaultValue("{0}"),
            Category("Settings Row")]
        public string ValueFormat
        {
            get { return valueFormat; }
            set
            {
                if ((value = (value ?? "").Trim()).Equals(valueFormat))
                    return;
                valueFormat = value;
                if (IsDesignMode)
                    Refresh();
                else
                    TrackBar.Parent.Invalidate(new Rectangle(0, 0, 50, TrackBar.Parent.ClientRectangle.Height));
            }
        }
        private string valueFormat = "{0}";

        /// <summary>
        /// Alias for TrackBar.Value.
        /// </summary>
        [Browsable(true),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
            DefaultValue(0)]
        public int Value
        {
            get { return TrackBar.Value; }
            set { TrackBar.Value = value; }
        }

        protected override void Initialize(Panel contents)
        {
            contents.Controls.Add(TrackBar = new TrackBar());
            TrackBar.Margin = new Padding(0);
            TrackBar.AutoSize = false;
            TrackBar.Bounds = new Rectangle(50, 0, contents.ClientRectangle.Width-50, contents.ClientRectangle.Height);
            TrackBar.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            TrackBar.ValueChanged += (s, e) =>
            {
                if (IsDesignMode)
                    Refresh();
                else
                    contents.Invalidate(new Rectangle(0, 0, 50, contents.ClientRectangle.Height));
            };
            TrackBar.MouseWheel += (s, e) =>
            {
                ((HandledMouseEventArgs)e).Handled = true;
            };

            contents.Paint += (s, e) =>
            {
                e.Graphics.AsQuality(GraphicsQuality.High, (g) =>
                {
                    string text;
                    try
                    {
                        text = string.Format(valueFormat, TrackBar.Value);
                    }
                    catch (FormatException)
                    {
                        text = "ERROR";
                    }

                    using (var sf = new StringFormat())
                    {
                        sf.Alignment = StringAlignment.Center;
                        sf.LineAlignment = StringAlignment.Center;
                        sf.Trimming = StringTrimming.None;
                        g.DrawString(text, Font,
                            Color.FromArgb(contents.Parent.Enabled ? 255 : DisabledAlpha.Project(0, 255), ForeColor),
                            new Rectangle(0, 0, 50, contents.ClientRectangle.Height), sf);
                    }
                });
            };

            contents.Invalidate(new Rectangle(0, 0, 50, contents.ClientRectangle.Height));
        }
    }

    /// <summary>
    /// Check box row for the settings section of the RSTK window.
    /// </summary>
    public class CheckBoxRow : SettingsRow
    {
        [Browsable(true),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
            Category("Settings Row")]
        public CheckBox CheckBox { get; private set; }

        /// <summary>
        /// Alias for CheckBox.Checked.
        /// </summary>
        [Browsable(true),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
            DefaultValue(false)]
        public bool Checked
        {
            get { return CheckBox.Checked; }
            set { CheckBox.Checked = value; }
        }

        /// <summary>
        /// Alias for CheckBox.Text.
        /// </summary>
        [Browsable(true),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
            DefaultValue(""),
            Category("Settings Row")]
        public override string Text
        {
            get { return CheckBox.Text; }
            set { CheckBox.Text = value; }
        }

        protected override void Initialize(Panel contents)
        {
            contents.Controls.Add(CheckBox = new CheckBox());
            CheckBox.Margin = new Padding(0);
            CheckBox.Padding = new Padding(0);
            CheckBox.AutoSize = false;
            CheckBox.Dock = DockStyle.Fill;
            CheckBox.CheckAlign = ContentAlignment.MiddleLeft;
        }
    }

    /// <summary>
    /// Text row for the settings section of the RSTK window.
    /// </summary>
    public class TextRow : SettingsRow
    {
        private Panel contents;

        /// <summary>
        /// Text displayed in the body of the control.
        /// </summary>
        [Browsable(true),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
            DefaultValue(""),
            Category("Settings Row")]
        public override string Text
        {
            get { return text; }
            set
            {
                if ((value = (value ?? "").Trim()).Equals(text))
                    return;
                text = value;
                if (IsDesignMode)
                    Refresh();
                else
                    contents.Invalidate();
            }
        }
        private string text = "";

        protected override void Initialize(Panel contents)
        {
            this.contents = contents;

            contents.Paint += (s, e) =>
            {
                if (text.Length == 0)
                    return;

                e.Graphics.AsQuality(GraphicsQuality.High, (g) =>
                {
                    using (var sf = new StringFormat())
                    {
                        sf.Alignment = StringAlignment.Near;
                        sf.LineAlignment = StringAlignment.Center;
                        sf.Trimming = StringTrimming.None;
                        g.DrawString(text, Font,
                            Color.FromArgb(contents.Parent.Enabled ? 255 : DisabledAlpha.Project(0, 255), ForeColor),
                            contents.ClientRectangle, sf);
                    }
                });
            };
        }
    }

    /// <summary>
    /// Text box row for the settings section of the RSTK window.
    /// </summary>
    public class TextBoxRow : SettingsRow
    {
        [Browsable(true),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
            Category("Settings Row")]
        public TextBox TextBox { get; private set; }

        /// <summary>
        /// Alias for TextBox.Text.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string Text
        {
            get { return TextBox.Text; }
            set { TextBox.Text = value; }
        }

        /// <summary>
        /// Alias for TextBox.Multiline.
        /// </summary>
        [Browsable(true),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
            Category("Settings Row")]
        public bool Multiline
        {
            get { return TextBox.Multiline; }
            set
            {
                if (value == TextBox.Multiline)
                    return;
                TextBox.Multiline = value;
                TextBox.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right
                    | (value ? AnchorStyles.Bottom : AnchorStyles.None);
                PositionControl(null, null);
            }
        }

        protected override void Initialize(Panel contents)
        {
            contents.Controls.Add(TextBox = new ThemedTextBox());
            Multiline = false;
            contents.SizeChanged += PositionControl;
        }

        private void PositionControl(object sender, EventArgs args)
        {
            var rect = TextBox.Parent.ClientRectangle;
            TextBox.Width = rect.Width;
            if (Multiline)
            {
                TextBox.Height = rect.Height;
                TextBox.Location = Point.Empty;
            }
            else
                TextBox.CenterInParent();
        }
    }
}
