using Marzersoft;
using Marzersoft.Themes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private static Image image;

        /// <summary>
        /// Text displayed to the left of the control.
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
                Invalidate(new Rectangle(30, 0, 150, Height));
            }
        }
        private string text = "";

        /// <summary>
        /// Is the control in warning mode?
        /// </summary>
        [Browsable(false),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool ShowWarningIcon
        {
            get { return showWarning; }
            set
            {
                if (showWarning != value)
                {
                    showWarning = value;
                    Invalidate(new Rectangle(0, 0, 30, Height));
                }
            }
        }
        private bool showWarning = false;

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
            panContents.Bounds = Rectangle.FromLTRB(185, 0, Width, Height);
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

            if (!((showWarning && !IsDesignMode) || text.Length > 0))
                return;

            e.Graphics.AsQuality(GraphicsQuality.High, (g) =>
            {
                if (showWarning && !IsDesignMode)
                {
                    if (image == null)
                        image = App.Images.Resource("warning_24", App.Assembly);
                    if (image != null)
                    {
                        using (ImageAttributes ia = new ImageAttributes())
                        {
                            ColorMatrix cm = new ColorMatrix();
                            if (!Enabled)
                                cm = cm.SetAlpha(0.5f);
                            ia.SetColorMatrix(cm);

                            g.DrawImage(image,
                                new Rectangle(15 - image.Width / 2, Height / 2 - image.Height / 2,
                                image.Width, image.Height),
                                0, 0, image.Width, image.Height,
                                GraphicsUnit.Pixel, ia);
                        }
                    }
                }

                if (text.Length > 0)
                {
                    using (var sf = new StringFormat())
                    {
                        sf.Alignment = StringAlignment.Far;
                        sf.LineAlignment = StringAlignment.Center;
                        sf.Trimming = StringTrimming.EllipsisWord;
                        g.DrawString(text, Font,
                            Color.FromArgb(Enabled ? 255 : 120, ForeColor),
                            new Rectangle(30, 0, 150, Height), sf);
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

        protected override void Initialize(Panel contents)
        {
            contents.Controls.Add(ComboBox = new ThemedComboBox());
            ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBox.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
            PositionControl(contents, null);
            contents.SizeChanged += PositionControl;
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
                TrackBar.Parent.Invalidate(new Rectangle(0, 0, 50, TrackBar.Parent.ClientRectangle.Height));
            }
        }
        private string valueFormat = "{0}";

        protected override void Initialize(Panel contents)
        {
            contents.Controls.Add(TrackBar = new TrackBar());
            TrackBar.Margin = new Padding(0);
            TrackBar.AutoSize = false;
            TrackBar.Bounds = Rectangle.FromLTRB(50, 0, contents.ClientRectangle.Width, contents.ClientRectangle.Height);
            TrackBar.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            TrackBar.ValueChanged += (s, e) =>
            {
                contents.Invalidate(new Rectangle(0, 0, 50, contents.ClientRectangle.Height));
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
                            Color.FromArgb(contents.Parent.Enabled ? 255 : 120, ForeColor),
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
        private Label lblValue;
        
        /// <summary>
        /// Text displayed in the body of the control.
        /// </summary>
        [Browsable(true),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
            DefaultValue(""),
            Category("Settings Row")]
        public string Value
        {
            get { return lblValue.Text; }
            set { lblValue.Text = value; }
        }

        protected override void Initialize(Panel contents)
        {
            contents.Controls.Add(lblValue = new Label());
            lblValue.AutoSize = false;
            lblValue.TextAlign = ContentAlignment.MiddleLeft;
            lblValue.Margin = new Padding(0);
            lblValue.Padding = new Padding(0);
            lblValue.Dock = DockStyle.Fill;
        }
    }
}
