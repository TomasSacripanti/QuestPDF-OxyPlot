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
            page.Margin(40, QuestPDF.Infrastructure.Unit.Point);
            page.Header().Element(ComposeHeader);
            page.Content().Element(ComposeContentFirstPage);
            page.Size(PageSizes.A4);
        });
    }

    void ComposeHeader(IContainer container)
    {
        var titleStyle = TextStyle.Default.FontSize(20).SemiBold().FontColor(QuestPDF.Helpers.Colors.Blue.Medium);

        byte[] lgcLogo = File.ReadAllBytes("./lgclogo.jpg");
        byte[] msdrInfinity = File.ReadAllBytes("./msdr.png");

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

    void ComposeContentFirstPage(IContainer container)
    {
        container.PaddingVertical(10).Column(column =>
        {
            column.Spacing(5);

            // Add Data Rectangle
            column.Item().Element(ComposeDataRectangle);

            // Several Data
            column.Item().Element(ComposeData);
            column.Item().Element(ComposeData2);
            column.Item().Element(ComposeData3);
            // Table
            column.Item().Element(ComposeLineThrough);
            column.Item().Element(ComposeTableData);
            column.Item().Element(ComposeTable);
            column.Item().Element(ComposeLineThrough);
            // Footer
            column.Item().Element(ComposeFooter);
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
                    text.Line("Analyzer:  Test");
                    text.Line("Serial#:  85050");
                });
            });
            row.RelativeItem().Column(column =>
            {
                column.Item().AlignLeft().Text(text =>
                {
                    text.DefaultTextStyle(x => x.FontSize(10).Bold());
                    text.Line("Account:  125212");
                    text.Line("Company: ");
                    text.Line("Facility:  St. Mary's Regional Testing Center");
                });
            });
        });
    }

    void ComposeData(IContainer container)
    {
        byte[] validate = File.ReadAllBytes("./validate.png");

        container.Padding(3).Row(row =>
        {
            row.RelativeItem().DefaultTextStyle(x => x.FontSize(10)).Column(column =>
            {
                column.Item().AlignLeft().Text(text =>
                {
                    text.Span("Submission ID: ");
                    text.Span(" 23-302763").ExtraBold();
                });
                column.Item().AlignLeft().Text(text =>
                {
                    text.Span("Entry Type:  Calibration Verification");
                });
            });
            row.RelativeItem().Column(column =>
            {
                column.Item().AlignRight().Width(120, QuestPDF.Infrastructure.Unit.Point).Image(validate);
            });
        });
    }

    void ComposeData2(IContainer container)
    {
        container.Padding(3).Row(row =>
        {
            row.RelativeItem().Column(col =>
            {
                col.Item().AlignLeft().Text("The following data was processed through LGC Maine Standards’ MSDRx® software program, using VALIDATE® Linearity and Calibration Verification materials.If you have any questions, please call LGC Maine Standards at 1 - 207 - 892 - 1300 or e - mail msc.datareduction@lgcgroup.com.Access historical reports at any time via msdrx.mainestandards.com.").FontSize(8);
            });
        });
    }

    void ComposeData3(IContainer container)
    {
        container.Padding(3).Row(row =>
        {
            row.RelativeItem().Column(col =>
            {
                col.Item().AlignLeft().Text("To view the Analyte Report Explanation Guide, please visit: www.mainestandards.com/report_guide").FontSize(8);
            });
        });
    }

    void ComposeTableData(IContainer container)
    {
        container.Padding(3).DefaultTextStyle(x => x.FontSize(9).Bold()).Row(row =>
        {
            row.RelativeItem().Column(column =>
            {
                column.Item().AlignLeft().Text(text =>
                {
                    text.Span("Product:  ").ExtraBold();
                    text.Span("GC3");
                });
                column.Item().AlignLeft().Text(text =>
                {
                    text.Span("SKU:  ").ExtraBold();
                    text.Span("1300AB");
                });
                column.Item().AlignLeft().Text(text =>
                {
                    text.Span("Comments:").ExtraBold();
                });
            });
            row.RelativeItem().Column(column =>
            {
                column.Item().AlignLeft().Text(text =>
                {
                    text.Span("Lot #:  ").ExtraBold();
                    text.Span("10534898");
                });
                column.Item().AlignLeft().Text(text =>
                {
                    text.Span("Expiration:  ").ExtraBold();
                    text.Span("12/28/23");
                });
            });
        });
    }

    void ComposeLineThrough(IContainer container)
    {
        container.Padding(3).Row(row =>
        {
            row.RelativeItem().Column(col =>
            {
                col.Item().PaddingVertical(5).LineHorizontal(1).LineColor(QuestPDF.Helpers.Colors.Black);
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

    void ComposeFooter(IContainer container)
    {
        container.DefaultTextStyle(x => x.FontSize(6).Bold()).Row(row =>
        {
            row.RelativeItem().Column(column =>
            {
                column.Item().AlignLeft().Text(txt =>
                {
                    txt.Line("Generated by MSDRx®");
                    txt.Line("Template: 0.92");
                });
            });

            row.RelativeItem().Column(column =>
            {
                column.Item().AlignRight().Text(txt =>
                {
                    txt.Span("Page ");
                    txt.CurrentPageNumber();
                    txt.Span(" of ");
                    txt.TotalPages();
                });

                column.Item().AlignRight().Text(txt =>
                {
                    txt.Line("printed: 05/02/2023");
                });
            });
        });
    }
}