using OxyPlot;
using OxyPlot.Series;
using QuestPDF.Infrastructure;
using OxyPlot.Axes;
using OxyPlot.Legends;

public class OxyplotController
{
    public PlotModel GetFirstPlot()
    {
        // Create an OxyPlot PlotModel
        var plotModel = new PlotModel
        {
            Padding = new OxyThickness(0, 10, 0, 0),
            PlotMargins = new OxyThickness(50, 10, 20, 20),
            Title = "Linearity accross the reportable range",
            TitleFontSize = 8,
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
            FontSize = 8,
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
            FontSize = 8,
        };
        plotModel.Axes.Add(yAxis);

        // Create a series for your chart
        var series0 = new LineSeries
        {
            Title = "Applied Limits",
            LineStyle = LineStyle.Dot,
            Color = OxyColors.Blue,
            StrokeThickness = 1,
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
            StrokeThickness = 1,
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
            Title = "Linear Regression",
            LineStyle = LineStyle.Dot,
            Color = OxyColors.Goldenrod,
            StrokeThickness = 1,
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
            Title = "Theoretical Values",
            LineStyle = LineStyle.Dot,
            Color = OxyColors.Transparent,
            StrokeThickness = 2,
            MarkerType = MarkerType.Square,
            MarkerFill = OxyColors.Cyan,
            MarkerSize = 2,
            ItemsSource = new DataPoint[]
            {
                new DataPoint(1, 0),
                new DataPoint(2, 500),
                new DataPoint(3, 1000),
                new DataPoint(4, 1500),
                new DataPoint(10, 4500),
            },
        };

        var series4 = new LineSeries
        {
            Title = "Observed Values",
            LineStyle = LineStyle.Dot,
            Color = OxyColors.Transparent,
            StrokeThickness = 1,
            MarkerType = MarkerType.Triangle,
            MarkerFill = OxyColors.DarkGreen,
            MarkerSize = 3,
            ItemsSource = new DataPoint[]
            {
                new DataPoint(1, 0),
                new DataPoint(2, 500),
                new DataPoint(3, 1000),
                new DataPoint(4, 1500),
                new DataPoint(10, 4500),
                new DataPoint(10, 2500),
            },
        };

        var legendOne = new Legend
        {
            LegendPosition = LegendPosition.BottomCenter,
            LegendOrientation = LegendOrientation.Horizontal,
            LegendPlacement = LegendPlacement.Outside,
            LegendFontSize = 8,
            AllowUseFullExtent = true,
            LegendItemSpacing = 30,
            LegendLineSpacing = 5,
            LegendPadding = 15,
            LegendMargin = 10
        };

        // Add the series and legends to the plot model
        plotModel.Series.Add(series0);
        plotModel.Series.Add(series1);
        plotModel.Series.Add(series2);
        plotModel.Series.Add(series3);
        plotModel.Series.Add(series4);
        plotModel.Legends.Add(legendOne);

        return plotModel;
    }

    public PlotModel GetSecondPlot()
    {
        // Create an OxyPlot PlotModel
        var plotModel = new PlotModel
        {
            Padding = new OxyThickness(0, 10, 0, 0),
            PlotMargins = new OxyThickness(50, 10, 20, 20),
            Title = "% Difference Versus Peer Mean: GC3 LD",
            TitleFontSize = 8,
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
            MajorStep = 1,
            Maximum = 6,
            Minimum = 1,
            MajorGridlineStyle = LineStyle.Solid,
            MajorGridlineColor = OxyColor.FromRgb(200, 200, 200),
            FontSize = 8,
        };
        plotModel.Axes.Add(xAxis);

        var yAxis = new LinearAxis
        {
            Title = "Values",
            Position = AxisPosition.Left,
            AxisTickToLabelDistance = 5,
            MajorStep = 10,
            MinorStep = 5,
            MajorTickSize = 5,
            MinorTickSize = 2.5,
            Maximum = 50,
            Minimum = -10,
            LabelFormatter = _percentageFormatter,
            MajorGridlineStyle = LineStyle.Solid,
            MajorGridlineColor = OxyColor.FromRgb(200, 200, 200),
            FontSize = 8,
        };
        plotModel.Axes.Add(yAxis);

        // Create a series for your chart
        var series0 = new LineSeries
        {
            LineStyle = LineStyle.Dot,
            Color = OxyColors.Blue,
            StrokeThickness = 3,
            MarkerFill = OxyColors.Blue,
            MarkerSize = 5,
            ItemsSource = new DataPoint[]
            {
                new DataPoint(1, -10),
                new DataPoint(2, -10),
                new DataPoint(3, -10),
                new DataPoint(4, -10),
                new DataPoint(5, -10),
                new DataPoint(6, -10),
            },
        };

        var series1 = new LineSeries
        {
            Title = "+ / - 2x CV%",
            LineStyle = LineStyle.Dot,
            Color = OxyColors.Yellow,
            StrokeThickness = 1,
            MarkerFill = OxyColors.Yellow,
            MarkerType = MarkerType.Circle,
            MarkerSize = 1,
            ItemsSource = new DataPoint[]
            {
                new DataPoint(1, 0),
                new DataPoint(2, 0),
                new DataPoint(3, 0),
                new DataPoint(4, 0),
                new DataPoint(5, 0),
                new DataPoint(6, 0),
            },
        };

        var series2 = new LineSeries
        {
            Title = "Applied Limit (+ / -)",
            LineStyle = LineStyle.Dot,
            Color = OxyColors.Blue,
            StrokeThickness = 1,
            MarkerType = MarkerType.Circle,
            MarkerFill = OxyColors.Blue,
            MarkerSize = 1,
            ItemsSource = new DataPoint[]
            {
                new DataPoint(1, 10),
                new DataPoint(2, 10),
                new DataPoint(3, 10),
                new DataPoint(4, 10),
                new DataPoint(5, 10),
                new DataPoint(6, 10),
            },
        };

        var series3 = new LineSeries
        {
            Title = "% Diff",
            LineStyle = LineStyle.None,
            StrokeThickness = 0,
            MarkerFill = OxyColors.Blue,
            MarkerType = MarkerType.Square,
            MarkerSize = 2,
            ItemsSource = new DataPoint[]
            {
                new DataPoint(1, 0),
                new DataPoint(2, 2),
                new DataPoint(3, 0),
                new DataPoint(4, 1),
                new DataPoint(5, 2),
                new DataPoint(5.9, 40),
            },
        };

        var legendOne = new Legend
        {
            LegendPosition = LegendPosition.BottomCenter,
            LegendOrientation = LegendOrientation.Horizontal,
            LegendPlacement = LegendPlacement.Outside,
            LegendFontSize = 8,
            AllowUseFullExtent = true,
            LegendItemSpacing = 30,
            LegendLineSpacing = 5,
            LegendPadding = 15,
            LegendMargin = 10
        };

        // Add the series and legends to the plot model
        plotModel.Series.Add(series0);
        plotModel.Series.Add(series1);
        plotModel.Series.Add(series2);
        plotModel.Series.Add(series3);
        plotModel.Legends.Add(legendOne);

        return plotModel;
    }

    public byte[] PlotModelToSvg(PlotModel plotModel, Size size)
    {
        using var stream = new MemoryStream();
        var exporter = new OxyPlot.SkiaSharp.PngExporter
        {
            Width = (int)size.Width,
            Height = (int)size.Height,
            Dpi = 220,
        };
        exporter.Export(plotModel, stream);
        stream.Position = 0;

        byte[] data = stream.ToArray();
        File.WriteAllBytes("chart.png", data);

        return data;
    }

    private static string _percentageFormatter(double d)
    {
        return String.Format("{0}%", d);
    }
}