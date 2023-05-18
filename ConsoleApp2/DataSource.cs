using OxyPlot;
using OxyPlot.Series;
using QuestPDF.Helpers;

public static class InvoiceDocumentDataSource
{
    private static Random Random = new Random();

    public static LineSeries GetInvoiceDetails()
    {
        var dataPoints = Enumerable
            .Range(1, 5)
            .Select(i => GenerateRandomDataPoints(i))
            .ToList();

        return new LineSeries
        {
            LineStyle = LineStyle.Dot,
            Color = OxyColors.Red,
            DataFieldX = Placeholders.Label(),
            DataFieldY = Placeholders.Label(),
            ToolTip = Placeholders.Label(),
            StrokeThickness = 2,
            MarkerType = MarkerType.Triangle,
            MarkerFill = OxyColors.DarkGreen,
            MarkerSize = 5,
            ItemsSource = dataPoints,
        };
    }

    private static DataPoint GenerateRandomDataPoints(int randomNumber)
    {
        return new DataPoint(randomNumber, randomNumber * 100);
    }

    private static Address GenerateRandomAddress()
    {
        return new Address
        {
            CompanyName = Placeholders.Name(),
            Street = Placeholders.Label(),
            City = Placeholders.Label(),
            State = Placeholders.Label(),
            Email = Placeholders.Email(),
            Phone = Placeholders.PhoneNumber()
        };
    }
}