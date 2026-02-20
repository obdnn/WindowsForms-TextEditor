using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LabTextRedactorWindowsFormsApp
{
    public partial class FontSettingsWindow : Window
    {
        // Результаты, которые Form1 считает после закрытия окна
        public string SelectedFont { get; private set; } = "Microsoft Sans Serif";
        public double SelectedSize { get; private set; } = 12;
        public bool IsBold { get; private set; } = false;
        public bool IsItalic { get; private set; } = false;

        public FontSettingsWindow(string currentFont, double currentSize, bool bold, bool italic)
        {
            InitializeComponent();

            // Заполняем список шрифтов системы
            foreach (var family in Fonts.SystemFontFamilies)
                cmbFont.Items.Add(family.Source);

            cmbFont.SelectedItem = currentFont;
            sliderSize.Value = currentSize;
            chkBold.IsChecked = bold;
            chkItalic.IsChecked = italic;

            // Обновляем превью при любом изменении
            cmbFont.SelectionChanged += (s, e) => UpdatePreview();
            sliderSize.ValueChanged  += (s, e) => UpdatePreview();
            chkBold.Checked          += (s, e) => UpdatePreview();
            chkBold.Unchecked        += (s, e) => UpdatePreview();
            chkItalic.Checked        += (s, e) => UpdatePreview();
            chkItalic.Unchecked      += (s, e) => UpdatePreview();

            UpdatePreview();
        }

        private void UpdatePreview()
        {
            if (previewText == null) return;

            if (cmbFont.SelectedItem != null)
                previewText.FontFamily = new FontFamily(cmbFont.SelectedItem.ToString());

            previewText.FontSize   = sliderSize.Value;
            previewText.FontWeight = chkBold.IsChecked == true ? FontWeights.Bold : FontWeights.Normal;
            previewText.FontStyle  = chkItalic.IsChecked == true ? FontStyles.Italic : FontStyles.Normal;
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            SelectedFont = cmbFont.SelectedItem?.ToString() ?? "Microsoft Sans Serif";
            SelectedSize = sliderSize.Value;
            IsBold       = chkBold.IsChecked == true;
            IsItalic     = chkItalic.IsChecked == true;
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
