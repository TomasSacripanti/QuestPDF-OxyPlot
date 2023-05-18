using OxyPlot;
using OxyPlot.Series;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SkiaSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using System.Text;

public class InvoiceDocument : IDocument
{
    public LineSeries Model { get; }
    public OxyplotController _oxyplotController { get; private set; }

    public InvoiceDocument(LineSeries model, OxyplotController oxyplotController)
    {
        Model = model;
        _oxyplotController = oxyplotController;
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
    public DocumentSettings GetSettings() => DocumentSettings.Default;

    [Obsolete]
    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Margin(30, QuestPDF.Infrastructure.Unit.Point);
            page.Header().Element(ComposeHeader);
            page.Content().Element(ComposeContent);
        });
    }

    void ComposeHeader(IContainer container)
    {
        var titleStyle = TextStyle.Default.FontSize(20).SemiBold().FontColor(QuestPDF.Helpers.Colors.Blue.Medium);

        byte[] lgcLogo = File.ReadAllBytes("./lgclogo.jpg");
        byte[] msdrInfinity = File.ReadAllBytes("./msdr.png");
        byte[] validate = File.ReadAllBytes("./validate.png");

        container.Row(row =>
        {
            row.RelativeItem().Column(column =>
            {
                column.Item().Width(120, QuestPDF.Infrastructure.Unit.Point).Image(lgcLogo);
            });

            row.RelativeItem().Column(column =>
            {
                column.Item().AlignCenter().Text(text =>
                {
                    text.DefaultTextStyle(x => x.FontSize(10));
                    text.Line("Submission Summary").Bold().FontSize(12);
                    text.Line("Linearity & Calibration Verification").Bold();
                });
            });

            row.RelativeItem().Column(column =>
            {
                column.Item().AlignRight().Width(120, QuestPDF.Infrastructure.Unit.Point).Image(msdrInfinity);
            });
        });
    }

    void ComposeContent(IContainer container)
    {
        container.PaddingVertical(10).Column(column =>
        {
            column.Spacing(5);

            // Add Data Rectangle
            column.Item().Element(ComposeDataRectangle);

            // Add table
            column.Item().Element(ComposeTable);

            // Add Graph
            column.Item().Element(ComposeGraph);
        });
    }

    void ComposeDataRectangle(IContainer container)
    {
        container.Background(QuestPDF.Helpers.Colors.Grey.Lighten2).Border(1).BorderColor(QuestPDF.Helpers.Colors.Black).Padding(3).Row(row =>
        {
            row.RelativeItem().Column(column =>
            {
                column.Item().AlignLeft().Text(text =>
                {
                    text.DefaultTextStyle(x => x.FontSize(10).Bold());
                    text.Line("Analyzer: Test");
                    text.Line("Serial#: 85050");
                });
            });
            row.RelativeItem().Column(column =>
            {
                column.Item().AlignLeft().Text(text =>
                {
                    text.DefaultTextStyle(x => x.FontSize(10).Bold());
                    text.Line("Account:125212");
                    text.Line("Company: ");
                    text.Line("Facility: St. Mary's Regional Testing Center");
                });
            });
        });
    }

    void ComposeGraph(IContainer container)
    {
        container.Column(column =>
        {
            var plotModel = _oxyplotController.GetPlot();
            column.Item()
                .Height(300)
                .Canvas((canvas, size) =>
                {
                    var svg = _oxyplotController.PlotModelToSvg(plotModel, size);
                    canvas.DrawPicture(svg.Picture);
                });
        });
    }

    void ComposeTable(IContainer container)
    {
        container.Table(table =>
        {
            // step 1
            table.ColumnsDefinition(columns =>
            {
                columns.ConstantColumn(30);
                columns.RelativeColumn(3);
                columns.RelativeColumn();
                columns.RelativeColumn();
                columns.RelativeColumn(2);
                columns.ConstantColumn(30);
            });

            static IContainer CellStyle(IContainer container)
            {
                return container.DefaultTextStyle(x => x.SemiBold()).Border(1).BorderColor(QuestPDF.Helpers.Colors.Black);
            }

            // step 2
            table.Header(header =>
            {
                header.Cell().Element(CellStyle).Background("#DCDCDC").Padding(2).AlignLeft().Text("Code").FontSize(8);
                header.Cell().Element(CellStyle).Background("#DCDCDC").Padding(2).AlignLeft().Text("Analyte").FontSize(8);
                header.Cell().Element(CellStyle).Background("#DCDCDC").Padding(2).AlignLeft().Text("Method").FontSize(8);
                header.Cell().Element(CellStyle).Background("#DCDCDC").Padding(2).AlignLeft().Text("Test Date").FontSize(8);
                header.Cell().Element(CellStyle).Background("#DCDCDC").Padding(2).AlignLeft().Text("Verified Range").FontSize(8);
                header.Cell().Element(CellStyle).Background("#DCDCDC").Padding(2).AlignLeft().Text("Status").FontSize(8);
            });

            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignLeft().Text("LD").FontSize(8);
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignLeft().Text("Lactate Dehydrogenase").FontSize(8);
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignLeft().Text("Enzymatic UV").FontSize(8);
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignLeft().Text("05/02/2023").FontSize(8);
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignLeft().Text("27.5 to 2,527.50 U/L").FontSize(8);
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2);
        });
    }
}