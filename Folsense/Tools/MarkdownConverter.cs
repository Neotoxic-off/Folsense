using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Markup;

namespace Folsense.Tools
{
    public class MarkdownConverter : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string markdown)
            {
                var converter = new Markdown.Xaml.Markdown();
                var flowDocument = converter.Transform(markdown);

                flowDocument.FontFamily = new System.Windows.Media.FontFamily("Segoe UI");
                flowDocument.FontSize = 12;

                return flowDocument;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
