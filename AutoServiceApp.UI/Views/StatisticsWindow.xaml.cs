using System;
using System.Windows;
using AutoServiceApp.Services;
using Data.Interfaces;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace AutoServiceApp
{
    public partial class StatisticsWindow : Window
    {
        private readonly StatisticsService _statisticsService;

        public PlotModel StatusPlotModel { get; set; }
        public PlotModel BrandPlotModel  { get; set; }
        public PlotModel MonthPlotModel  { get; set; }

        public StatisticsWindow(StatisticsService statisticsService)
        {
            InitializeComponent();
            _statisticsService = statisticsService;
            DataContext = this;
            LoadStatistics();
        }

        private RepairRequestsFilter BuildFilter()
        {
            DateOnly? value = StartDatePicker.SelectedDate.HasValue ? DateOnly.FromDateTime(StartDatePicker.SelectedDate.Value) : null;
            DateOnly? sd = value;
            DateOnly? ed = EndDatePicker.SelectedDate.HasValue ? DateOnly.FromDateTime(EndDatePicker.SelectedDate.Value) : null;
            return new RepairRequestsFilter { StartDate = sd, EndDate = ed };
        }

        private void LoadStatistics()
        {
            var filter = BuildFilter();
            LoadStatusChart(filter);
            LoadBrandChart(filter);
            LoadMonthChart(filter);

            DataContext = null;
            DataContext = this;
        }

        private void ApplyFilterButton_Click(object sender, RoutedEventArgs e) => LoadStatistics();
        private void ResetFilterButton_Click(object sender, RoutedEventArgs e)
        {
            StartDatePicker.SelectedDate = null;
            EndDatePicker.SelectedDate = null;
            LoadStatistics();
        }

        private void LoadStatusChart(RepairRequestsFilter filter)
        {
            var data = _statisticsService.GetRequestsByStatus(filter);
            var model = new PlotModel { Title = "Распределение заявок по статусам" };
            var pie = new PieSeries
            {
                StrokeThickness = 2,
                InsideLabelPosition = 0.6,
                AngleSpan = 360,
                StartAngle = 0
            };
            foreach (var item in data)
                pie.Slices.Add(new PieSlice(item.Status.ToString(), item.Count));
            model.Series.Add(pie);
            StatusPlotModel = model;
        }

        private void LoadBrandChart(RepairRequestsFilter filter)
        {
            var data = _statisticsService.GetRequestsByBrand(filter);
            var model = new PlotModel { Title = "Заявки по брендам" };

            var cat = new CategoryAxis { Position = AxisPosition.Left, Title = "Бренды" };
            foreach (var item in data) cat.Labels.Add(item.BrandName);
            model.Axes.Add(cat);

            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Количество", MinimumPadding = 0.1, MaximumPadding = 0.1 });

            var series = new BarSeries { Title = "Количество заявок" };
            foreach (var item in data) series.Items.Add(new BarItem { Value = item.Count });

            model.Series.Add(series);
            BrandPlotModel = model;
        }

        private void LoadMonthChart(RepairRequestsFilter filter)
        {
            var data = _statisticsService.GetRequestsByMonth(filter);
            var model = new PlotModel { Title = "Динамика заявок по месяцам" };

            var cat = new CategoryAxis { Position = AxisPosition.Bottom, Title = "Месяцы", Angle = -15 };
            foreach (var m in data) cat.Labels.Add(m.GetMonthName());
            model.Axes.Add(cat);

            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Количество", MinimumPadding = 0.1, MaximumPadding = 0.1 });

            var line = new LineSeries { Title = "Заявок" };
            for (int i = 0; i < data.Count; i++)
                line.Points.Add(new DataPoint(i, data[i].Count));

            model.Series.Add(line);
            MonthPlotModel = model;
        }
    }
}
