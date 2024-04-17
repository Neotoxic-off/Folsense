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

using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System.Runtime.Serialization;


namespace Folsense.ViewModels
{
    public class StatisticsViewModel : BaseClass
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

        private DatabaseManager? _databaseManager;
        public DatabaseManager? databaseManager
        {
            get { return _databaseManager; }
            set { SetProperty(ref _databaseManager, value); }
        }

        private DatabaseModel? _databaseModel;
        public DatabaseModel? databaseModel
        {
            get { return _databaseModel; }
            set { SetProperty(ref _databaseModel, value); }
        }

        private SeriesCollection? _chartSeries;
        public SeriesCollection? ChartSeries
        {
            get { return _chartSeries; }
            set { SetProperty(ref _chartSeries, value); }
        }

        private ObservableCollection<string>? _dates;
        public ObservableCollection<string>? Dates
        {
            get { return _dates; }
            set { SetProperty(ref _dates, value); }
        }

        private MarkdownConverter _markdownConverter;
        public MarkdownConverter markdownConverter
        {
            get { return _markdownConverter; }
            set { SetProperty(ref _markdownConverter, value); }
        }

        public StatisticsViewModel()
        {
            databaseManager = new DatabaseManager();
            databaseModel = new DatabaseModel();
            Dates = new ObservableCollection<string>();

            LoadDatabase();
            LoadCharts();
        }

        private void LoadDatabase()
        {
            databaseModel.Content = databaseManager.Get();
        }

        private void LoadCharts()
        {
            Dictionary<DateTime, double> records = new Dictionary<DateTime, double>();
            ChartValues<double> values = new ChartValues<double>();

            foreach (Models.Database.IO.FolderModel folder in databaseModel.Content)
            {
                if (records.ContainsKey(folder.Date.Value.Date) == false)
                {
                    records.Add(folder.Date.Value.Date, folder.Files.Count());
                }
                else
                {
                    records[folder.Date.Value.Date] += folder.Files.Count();
                }
            }

            records = new Dictionary<DateTime, double>(records.OrderBy(d => d.Key));

            foreach (KeyValuePair<DateTime, double> pair in records)
            {
                Dates.Add(pair.Key.ToString());
                values.Add(pair.Value);
            }

            ChartSeries = new SeriesCollection
            {
                new LineSeries
                {
                    Values = values
                }
            };
        }
    }
}
