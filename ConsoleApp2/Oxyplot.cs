using OxyPlot;
using OxyPlot.Series;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Drawing;
using SkiaSharp;
using OxyPlot.SkiaSharp;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Legends;

public class OxyplotController
{
    public PlotModel GetPlot()
    {
        // Create an OxyPlot PlotModel
        var plotModel = new PlotModel
        {
            Padding = new OxyThickness(10, 20, 10, 10),
            Title = "Linearity accross the reportable range",
            TitleFontSize = 12,
            TitleHorizontalAlignment = TitleHorizontalAlignment.CenteredWithinView,
            PlotAreaBackground = OxyColors.White,
            TitleColor = OxyColors.Black,
            IsLegendVisible = true,
        };

        // Create the axis for the PlotModel
        var xAxis = new LinearAxis
        {
            Title = "Linear Dilution Level",
            Position = AxisPosition.Bottom,
            AxisTickToLabelDistance = 10,
            MajorStep = 5,
            Maximum = 15,
            MajorGridlineStyle = LineStyle.Solid,
            MajorGridlineColor = OxyColor.FromRgb(200, 200, 200),
        };
        plotModel.Axes.Add(xAxis);

        var yAxis = new LinearAxis
        {
            Title = "Values",
            Position = AxisPosition.Left,
            AxisTickToLabelDistance = 5,
            MajorStep = 1000,
            Maximum = 6000,
            MajorGridlineStyle = LineStyle.Solid,
            MajorGridlineColor = OxyColor.FromRgb(200, 200, 200),
        };
        plotModel.Axes.Add(yAxis);

        // Create a series for your chart
        var series0 = new LineSeries
        {
            LineStyle = LineStyle.Dot,
            Color = OxyColors.Blue,
            StrokeThickness = 2,
            MarkerFill = OxyColors.DarkGreen,
            MarkerSize = 5,
            ItemsSource = new DataPoint[]
            {
                new DataPoint(1, 0),
                new DataPoint(10, 5000),
            },
        };

        var series1 = new LineSeries
        {
            LineStyle = LineStyle.Dot,
            Color = OxyColors.Blue,
            StrokeThickness = 2,
            MarkerFill = OxyColors.DarkGreen,
            MarkerSize = 5,
            ItemsSource = new DataPoint[]
            {
                new DataPoint(1, 0),
                new DataPoint(10, 4000),
            },
        };

        var series2 = new LineSeries
        {
            LineStyle = LineStyle.Dot,
            Color = OxyColors.Red,
            StrokeThickness = 2,
            MarkerType = MarkerType.Triangle,
            MarkerFill = OxyColors.DarkGreen,
            MarkerSize = 5,
            ItemsSource = new DataPoint[]
            {
                new DataPoint(1, 0),
                new DataPoint(2, 500),
                new DataPoint(3, 1000),
                new DataPoint(4, 1500),
                new DataPoint(10, 4500),
            },
        };

        var series3 = new LineSeries
        {
            LineStyle = LineStyle.Dot,
            Color = OxyColors.Red,
            StrokeThickness = 2,
            MarkerType = MarkerType.Square,
            MarkerFill = OxyColors.Cyan,
            MarkerSize = 5,
            ItemsSource = new DataPoint[]
            {
                new DataPoint(10, 4500),
            },
        };

        // Add Legend
        var legendOne = new Legend
        {
            LegendPosition = LegendPosition.BottomCenter,
            LegendPlacement = LegendPlacement.Outside,
            LegendTitle = "Observed Values, Peer Mean, Applied Limits (+ / -), Linear Regression, Theoretical Values",
            AllowUseFullExtent = true,
        };

        // Add the series and legends to the plot model
        plotModel.Series.Add(series0);
        plotModel.Series.Add(series1);
        plotModel.Series.Add(series2);
        plotModel.Series.Add(series3);
        plotModel.Legends.Add(legendOne);

        return plotModel;
    }

    public SkiaSharp.Extended.Svg.SKSvg PlotModelToSvg(PlotModel plotModel, Size size)
    {
        using var stream = new MemoryStream();
        var exporter = new OxyPlot.SvgExporter
        {
            Width = size.Width,
            Height = size.Height,
        };

        exporter.Export(plotModel, stream);
        stream.Position = 0;
        var svg = new SkiaSharp.Extended.Svg.SKSvg();
        svg.Load(stream);
        return svg;
    }
}