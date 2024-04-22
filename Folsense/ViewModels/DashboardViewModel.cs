using Folsense.Bases;
using Folsense.Models;
using Folsense.Models.Database;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using Folsense.Models.IO;
using System.IO;
using Folsense.Tools;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using Folsense.Models.Database.IO;
using LiveCharts.Wpf;
using LiveCharts;
using System.Reflection.Emit;


namespace Folsense.ViewModels
{
    public class DashboardViewModel : BaseClass
    {
        private string? _changelogContent;
        public string? ChangelogContent
        {
            get { return _changelogContent; }
            set { SetProperty(ref _changelogContent, value); }
        }

        private object? _changelogContentMarkdown;
        public object? ChangelogContentMarkdown
        {
            get { return _changelogContentMarkdown; }
            set { SetProperty(ref _changelogContentMarkdown, value); }
        }

        private MarkdownConverter _markdownConverter;
        public MarkdownConverter markdownConverter
        {
            get { return _markdownConverter; }
            set { SetProperty(ref _markdownConverter, value); }
        }

        private string? _username;
        public string? Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }

        private DateTime? _date;
        public DateTime? Date
        {
            get { return _date; }
            set { SetProperty(ref _date, value); }
        }

        private long _databaseSize;
        public long databaseSize
        {
            get { return _databaseSize; }
            set { SetProperty(ref _databaseSize, value); }
        }

        private long _diskSize;
        public long diskSize
        {
            get { return _diskSize; }
            set { SetProperty(ref _diskSize, value); }
        }

        private SeriesCollection? _chartSeries;
        public SeriesCollection? ChartSeries
        {
            get { return _chartSeries; }
            set { SetProperty(ref _chartSeries, value); }
        }

        private string[] _labels;
        public string[] Labels
        {
            get { return _labels; }
            set { SetProperty(ref _labels, value); }
        }

        public DashboardViewModel()
        {
            markdownConverter = new MarkdownConverter();
            databaseSize = DiskManager.GetDatabaseStorage();
            diskSize = DiskManager.GetUserStorage();

            LoadChangelog();
            LoadCharts();

            Username = Environment.UserName;
            Date = DateTime.Now;
        }

        private void LoadChangelog()
        {
            if (ISettings.Changelog.Exists == true)
            {
                ChangelogContent = File.ReadAllText(ISettings.Changelog.Path);
                ChangelogContentMarkdown = markdownConverter.Convert(ChangelogContent, null, null, null);
            }
        }

        private void LoadCharts()
        {
            ChartSeries = new SeriesCollection
            {
                new StackedRowSeries
                {
                    Values = new ChartValues<long> {diskSize, databaseSize},
                    DataLabels = true,
                    LabelPoint = p => p.X.ToString()
                }
            };
            Labels = new[] { "Disk", "Databases" };
        }
    }
}
