using OxyPlot.Series;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

public class RSSMDocument : IDocument
{
    public OxyplotController _oxyplotController { get; private set; }

    public RSSMDocument(OxyplotController oxyplotController)
    {
        _oxyplotController = oxyplotController;
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
    public DocumentSettings GetSettings() => DocumentSettings.Default;

    [Obsolete]
    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Margin(15, Unit.Point);
            page.Header().DefaultTextStyle(x => x.FontFamily("Arial")).Element(ComposeHeader);
            page.Content().DefaultTextStyle(x => x.FontFamily("Arial")).Element(ComposeContentFirstPage);
            // Footer
            page.Footer().Element(ComposeFooter);
            page.Size(PageSizes.A4);
        });
    }

    void ComposeHeader(IContainer container)
    {
        var titleStyle = TextStyle.Default.FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);

        byte[] primeroLogo = File.ReadAllBytes("./primerologo.jpg");

        container.Row(row =>
        {
            row.RelativeItem(3).DefaultTextStyle(x => x.FontSize(7)).Column(column =>
            {
                column.Item().AlignLeft().Text(text =>
                {
                    text.Line("StripeInvoices - Connect").ExtraBold().FontSize(14);
                    text.Line("3225 North Harbor Drive").Bold();
                    text.Line("San Diego, 92101").Bold();
                    text.Line("(852) 963-7419").Bold();
                    text.Line("gvrssmps@gmail.com").Bold();
                    text.Line("www.primerosystems.com").Bold();
                });
            });

            row.RelativeItem(1).Column(column =>
            {
                column.Item().AlignRight().Width(80, Unit.Point).Image(primeroLogo);
            });
        });
    }

    void ComposeContentFirstPage(IContainer container)
    {
        container.PaddingVertical(10).Column(column =>
        {
            column.Spacing(5);
            // Table
            column.Item().Element(ComposeTableData);
            column.Item().Element(ComposeTable);
            column.Item().Element(ComposeTableDetails);
        });
    }

    void ComposeTableData(IContainer container)
    {
        container.Padding(3).DefaultTextStyle(x => x.FontSize(7).Bold()).Row(row =>
        {
            row.RelativeItem().Column(column =>
            {
                column.Item().AlignLeft().Text(text =>
                {
                    text.Line("B I L L  T O: ").ExtraBold().FontSize(14);
                    text.Line("Lucas Allegri").ExtraBold();
                });
            });
            row.RelativeItem().Column(column =>
            {
                column.Item().AlignLeft().Text(text =>
                {
                    text.Line("I N V O I C E").ExtraBold().FontSize(20);
                    text.Line("Invoice Number: 1005").Bold().FontSize(8);
                    text.Line("Date: 5/10/2023").Bold().FontSize(8);
                });
            });
        });
    }

    void ComposeTable(IContainer container)
    {
        container.DefaultTextStyle(x => x.FontSize(8)).Table(table =>
        {
            // step 1
            table.ColumnsDefinition(columns =>
            {
                columns.RelativeColumn(5);
                columns.RelativeColumn();
                columns.RelativeColumn();
                columns.RelativeColumn();
            });

            static IContainer CellStyle(IContainer container)
            {
                return container.DefaultTextStyle(x => x.SemiBold()).BorderBottom(1).BorderColor(Colors.Black);
            }

            // step 2
            table.Header(header =>
            {
                header.Cell().Element(CellStyle).Background("#000000").Padding(2).AlignLeft().Text("Sales Item").FontColor(Colors.White);
                header.Cell().Element(CellStyle).Background("#000000").Padding(2).AlignLeft().Text("Price").FontColor(Colors.White);
                header.Cell().Element(CellStyle).Background("#000000").Padding(2).AlignLeft().Text("Qty").FontColor(Colors.White);
                header.Cell().Element(CellStyle).Background("#000000").Padding(2).AlignLeft().Text("Total").FontColor(Colors.White);
            });

            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignLeft().Text(txt =>
            {
                txt.Line("Sample Annual Dues.").ExtraBold();
                txt.Line("This membership is for demo purposes only.You should create a NEW MEMBERSHIP for your Chamber using the CREATE MEMBERSHIP BUTTON on the Memberships page. If you would like this sample removed, please contact ChamberMate Support");
                txt.Line("1-2 Full Time Employees");
                txt.Line("10 May 2023 - 31 Dec 2023");
            });
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignLeft().Text("$100.00");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignLeft().Text("1");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignLeft().Text("$100.00");
        });
    }

    void ComposeTableDetails(IContainer container)
    {
        container.Padding(3).DefaultTextStyle(x => x.FontSize(7).ExtraBold()).Row(row =>
        {
            row.RelativeItem(8).Column(column =>
            {
            });
            row.RelativeItem().Column(column =>
            {
                column.Item().AlignRight().Text(text =>
                {
                    text.Line("Subtotal:");
                    text.Line("Discount:");
                    text.Line("Sales Tax:");
                    text.Line("Total:");
                });
            });
            row.RelativeItem().Column(column =>
            {
                column.Item().AlignRight().Text(text =>
                {
                    text.Line("$100.00");
                    text.Line("$0.00");
                    text.Line("$0.00");
                    text.Line("$100.00");
                });
            });
        });
    }

    void ComposeFooter(IContainer container)
    {
        container.BorderTop(1).PaddingTop(5).DefaultTextStyle(x => x.FontSize(6).Bold()).Row(row =>
        {
            row.RelativeItem().Column(column =>
            {
                column.Item().AlignRight().Text(txt =>
                {
                    txt.Span("Page ");
                    txt.CurrentPageNumber();
                    txt.Span(" of ");
                    txt.TotalPages();
                });
            });
        });
    }
}