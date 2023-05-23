using OxyPlot.Series;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

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
            page.Margin(40, Unit.Point);
            page.Header().Element(ComposeHeader);
            page.Content().Element(ComposeContentFirstPage);
            page.Size(PageSizes.A4);
        });
        container.Page(page =>
        {
            page.Margin(40, Unit.Point);
            page.Header().Element(ComposeHeader2);
            page.Content().Element(ComposeContentSecondPage);
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
                column.Item().Width(80, QuestPDF.Infrastructure.Unit.Point).Image(lgcLogo);
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
                column.Item().AlignRight().Width(80, QuestPDF.Infrastructure.Unit.Point).Image(msdrInfinity);
            });
        });
    }

    void ComposeHeader2(IContainer container)
    {
        var titleStyle = TextStyle.Default.FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);

        byte[] lgcLogo = File.ReadAllBytes("./lgclogo.jpg");
        byte[] msdrInfinity = File.ReadAllBytes("./msdr.png");

        container.Row(row =>
        {
            row.RelativeItem().Column(column =>
            {
                column.Item().Width(80, Unit.Point).Image(lgcLogo);
            });

            row.RelativeItem().Column(column =>
            {
                column.Item().AlignCenter().Text(text =>
                {
                    text.Line("REPORT: LINEARITY / CALIBRATION VERIFICATION").ExtraBold().FontSize(10);
                });
            });

            row.RelativeItem().Column(column =>
            {
                column.Item().AlignRight().Width(80, Unit.Point).Image(msdrInfinity);
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
            column.Item().Padding(2);
            column.Item().Element(ComposeLineThrough);
            column.Item().Padding(2);
            // Footer
            column.Item().Element(ComposeFooter);
        });
    }

    void ComposeContentSecondPage(IContainer container)
    {
        container.Column(column =>
        {
            // Add Section
            column.Item().Element(ComposeFirstSectionSecondPage);
            column.Item().Element(ComposeSecondSectionSecondPage);
            column.Item().Element(ComposeLineThrough);
            column.Item().Element(ComposeThirdSectionSecondPage);
            column.Item().Element(ComposeLineThrough);
            column.Item().Element(ComposeFourthSectionSecondPage);
            column.Item().Element(ComposeLineThrough);
            column.Item().Element(ComposeLastSectionSecondPage);
            // Footer
            column.Item().Element(ComposeFooter2);
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
                column.Item().AlignRight().Width(120, Unit.Point).Image(validate);
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
        container.Row(row =>
        {
            row.RelativeItem().Column(col =>
            {
                col.Item().LineHorizontal(1).LineColor(QuestPDF.Helpers.Colors.Black);
            });
        });
    }

    void ComposeGraph(IContainer container)
    {
        container.Column(column =>
        {
            var plotModel = _oxyplotController.GetFirstPlot();
            column.Item()
                .Height(225)
                .Canvas((canvas, size) =>
                {
                    var svg = _oxyplotController.PlotModelToSvg(plotModel, size);
                    canvas.DrawPicture(svg.Picture);
                });
        });
    }

    void ComposeSecondGraph(IContainer container)
    {
        container.Column(column =>
        {
            var plotModel = _oxyplotController.GetSecondPlot();
            column.Item()
                .Height(225)
                .Canvas((canvas, size) =>
                {
                    var svg = _oxyplotController.PlotModelToSvg(plotModel, size);
                    canvas.DrawPicture(svg.Picture);
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
                header.Cell().Element(CellStyle).Background("#DCDCDC").Padding(2).AlignLeft().Text("Code");
                header.Cell().Element(CellStyle).Background("#DCDCDC").Padding(2).AlignLeft().Text("Analyte");
                header.Cell().Element(CellStyle).Background("#DCDCDC").Padding(2).AlignLeft().Text("Method");
                header.Cell().Element(CellStyle).Background("#DCDCDC").Padding(2).AlignLeft().Text("Test Date");
                header.Cell().Element(CellStyle).Background("#DCDCDC").Padding(2).AlignLeft().Text("Verified Range");
                header.Cell().Element(CellStyle).Background("#DCDCDC").Padding(2).AlignLeft().Text("Status");
            });

            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignLeft().Text("LD");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignLeft().Text("Lactate Dehydrogenase");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignLeft().Text("Enzymatic UV");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignLeft().Text("05/02/2023");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignLeft().Text("27.5 to 2,527.50 U/L");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2);
        });
    }

    void ComposeTable1Page2(IContainer container)
    {
        container.DefaultTextStyle(x => x.FontSize(6)).Table(table =>
        {
            // step 1
            table.ColumnsDefinition(columns =>
            {
                columns.RelativeColumn();
                columns.RelativeColumn();
                columns.RelativeColumn();
                columns.RelativeColumn();
                columns.RelativeColumn();
                columns.RelativeColumn();
                columns.RelativeColumn();
            });

            static IContainer CellStyle(IContainer container)
            {
                return container.DefaultTextStyle(x => x.SemiBold()).Border(1).BorderColor(QuestPDF.Helpers.Colors.Black);
            }

            // step 2
            table.Header(header =>
            {
                header.Cell().Element(CellStyle).Background("#DCDCDC").Padding(2).AlignCenter().Text("");
                header.Cell().Element(CellStyle).Background("#DCDCDC").Padding(2).AlignCenter().Text("Level 1");
                header.Cell().Element(CellStyle).Background("#DCDCDC").Padding(2).AlignCenter().Text("Level 2");
                header.Cell().Element(CellStyle).Background("#DCDCDC").Padding(2).AlignCenter().Text("Level 3");
                header.Cell().Element(CellStyle).Background("#DCDCDC").Padding(2).AlignCenter().Text("Level 4");
                header.Cell().Element(CellStyle).Background("#DCDCDC").Padding(2).AlignCenter().Text("Level 5");
                header.Cell().Element(CellStyle).Background("#DCDCDC").Padding(2).AlignCenter().Text("Level 6");
            });

            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("Replicate 1");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("25");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("525");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("1025");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("1525");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("2025");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("2525");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("Replicate 2");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("30");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("530");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("1030");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("1530");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("2030");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("2530");
        });
    }

    void ComposeTable2Page2(IContainer container)
    {
        container.DefaultTextStyle(x => x.FontSize(6)).Table(table =>
        {
            // step 1
            table.ColumnsDefinition(columns =>
            {
                columns.RelativeColumn(2);
                columns.RelativeColumn();
                columns.RelativeColumn();
                columns.RelativeColumn();
                columns.RelativeColumn();
                columns.RelativeColumn();
                columns.RelativeColumn();
            });

            static IContainer CellStyle(IContainer container)
            {
                return container.DefaultTextStyle(x => x.SemiBold()).Border(1).BorderColor(QuestPDF.Helpers.Colors.Black);
            }

            // step 2
            table.Header(header =>
            {
                header.Cell().Element(CellStyle).Background("#DCDCDC").Padding(2).AlignCenter().Text("");
                header.Cell().Element(CellStyle).Background("#DCDCDC").Padding(2).AlignCenter().Text("Level 1");
                header.Cell().Element(CellStyle).Background("#DCDCDC").Padding(2).AlignCenter().Text("Level 2");
                header.Cell().Element(CellStyle).Background("#DCDCDC").Padding(2).AlignCenter().Text("Level 3");
                header.Cell().Element(CellStyle).Background("#DCDCDC").Padding(2).AlignCenter().Text("Level 4");
                header.Cell().Element(CellStyle).Background("#DCDCDC").Padding(2).AlignCenter().Text("Level 5");
                header.Cell().Element(CellStyle).Background("#DCDCDC").Padding(2).AlignCenter().Text("Level 6");
            });

            table.Cell().Element(CellStyle).Background("#DCDCDC").Padding(2).AlignLeft().Text("Effective X");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("1");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("2");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("3");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("4");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("5");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("10.06");
            table.Cell().Element(CellStyle).Background("#DCDCDC").Padding(2).AlignLeft().Text("Theoretical Value *");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("27.50");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("527.50 ");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("1,027.50");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("1530");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("2030");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("2530");
            table.Cell().Element(CellStyle).Background("#DCDCDC").Padding(2).AlignLeft().Text("Observed Mean ");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("30");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("530");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("1030");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("1530");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("2030");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("2530");
            table.Cell().Element(CellStyle).Background("#DCDCDC").Padding(2).AlignLeft().Text("+/- Difference");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("30");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("530");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("1030");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("1530");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("2030");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("2530");
            table.Cell().Element(CellStyle).Background("#DCDCDC").Padding(2).AlignLeft().Text("% Difference");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("30");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("530");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("1030");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("1530");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("2030");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("2530");
            table.Cell().Element(CellStyle).Background("#DCDCDC").Padding(2).AlignLeft().Text("+/- Limit");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("30");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("530");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("1030");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("1530");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("2030");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("2530");
            table.Cell().Element(CellStyle).Background("#DCDCDC").Padding(2).AlignLeft().Text("% Limit");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("30");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("530");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("1030");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("1530");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("2030");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("2530");
        });
    }

    void ComposeTable3Page2(IContainer container)
    {
        container.DefaultTextStyle(x => x.FontSize(6)).Table(table =>
        {
            // step 1
            table.ColumnsDefinition(columns =>
            {
                columns.RelativeColumn(2);
                columns.RelativeColumn();
                columns.RelativeColumn();
                columns.RelativeColumn();
                columns.RelativeColumn();
                columns.RelativeColumn();
                columns.RelativeColumn();
            });

            static IContainer CellStyle(IContainer container)
            {
                return container.DefaultTextStyle(x => x.SemiBold()).Border(1).BorderColor(QuestPDF.Helpers.Colors.Black);
            }

            // step 2
            table.Header(header =>
            {
                header.Cell().Element(CellStyle).Background("#DCDCDC").Padding(2).AlignCenter().Text("");
                header.Cell().Element(CellStyle).Background("#DCDCDC").Padding(2).AlignCenter().Text("Level 1");
                header.Cell().Element(CellStyle).Background("#DCDCDC").Padding(2).AlignCenter().Text("Level 2");
                header.Cell().Element(CellStyle).Background("#DCDCDC").Padding(2).AlignCenter().Text("Level 3");
                header.Cell().Element(CellStyle).Background("#DCDCDC").Padding(2).AlignCenter().Text("Level 4");
                header.Cell().Element(CellStyle).Background("#DCDCDC").Padding(2).AlignCenter().Text("Level 5");
                header.Cell().Element(CellStyle).Background("#DCDCDC").Padding(2).AlignCenter().Text("Level 6");
            });

            table.Cell().Element(CellStyle).Background("#DCDCDC").Padding(2).AlignLeft().Text("# Labs");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("1");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("2");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("3");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("4");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("5");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("10.06");
            table.Cell().Element(CellStyle).Background("#DCDCDC").Padding(2).AlignLeft().Text("# Data Sets ");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("27.50");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("527.50 ");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("1,027.50");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("1530");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("2030");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("2530");
            table.Cell().Element(CellStyle).Background("#DCDCDC").Padding(2).AlignLeft().Text("Peer Min ");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("30");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("530");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("1030");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("1530");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("2030");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("2530");
            table.Cell().Element(CellStyle).Background("#DCDCDC").Padding(2).AlignLeft().Text("Peer Median");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("30");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("530");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("1030");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("1530");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("2030");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("2530");
            table.Cell().Element(CellStyle).Background("#DCDCDC").Padding(2).AlignLeft().Text("Peer Max ");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("30");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("530");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("1030");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("1530");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("2030");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("2530");
        });
    }

    void ComposeTable4Page2(IContainer container)
    {
        container.DefaultTextStyle(x => x.FontSize(6)).Table(table =>
        {
            // step 1
            table.ColumnsDefinition(columns =>
            {
                columns.RelativeColumn(2);
                columns.RelativeColumn();
                columns.RelativeColumn();
                columns.RelativeColumn();
                columns.RelativeColumn();
                columns.RelativeColumn();
                columns.RelativeColumn();
            });

            static IContainer CellStyle(IContainer container)
            {
                return container.DefaultTextStyle(x => x.SemiBold()).Border(1).BorderColor(QuestPDF.Helpers.Colors.Black);
            }

            // step 2
            table.Header(header =>
            {
                header.Cell().Element(CellStyle).Background("#DCDCDC").Padding(2).AlignCenter().Text("");
                header.Cell().Element(CellStyle).Background("#DCDCDC").Padding(2).AlignCenter().Text("Level 1");
                header.Cell().Element(CellStyle).Background("#DCDCDC").Padding(2).AlignCenter().Text("Level 2");
                header.Cell().Element(CellStyle).Background("#DCDCDC").Padding(2).AlignCenter().Text("Level 3");
                header.Cell().Element(CellStyle).Background("#DCDCDC").Padding(2).AlignCenter().Text("Level 4");
                header.Cell().Element(CellStyle).Background("#DCDCDC").Padding(2).AlignCenter().Text("Level 5");
                header.Cell().Element(CellStyle).Background("#DCDCDC").Padding(2).AlignCenter().Text("Level 6");
            });

            table.Cell().Element(CellStyle).Background("#DCDCDC").Padding(2).AlignLeft().Text("Peer Mean");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("1");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("2");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("3");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("4");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("5");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("10.06");
            table.Cell().Element(CellStyle).Background("#DCDCDC").Padding(2).AlignLeft().Text("Observed Mean");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("27.50");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("527.50 ");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("1,027.50");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("1530");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("2030");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("2530");
            table.Cell().Element(CellStyle).Background("#DCDCDC").Padding(2).AlignLeft().Text("+/- Difference ");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("30");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("530");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("1030");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("1530");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("2030");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("2530");
            table.Cell().Element(CellStyle).Background("#DCDCDC").Padding(2).AlignLeft().Text("% Difference");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("30");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("530");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("1030");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("1530");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("2030");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("2530");
            table.Cell().Element(CellStyle).Background("#DCDCDC").Padding(2).AlignLeft().Text("+/- Limit");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("30");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("530");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("1030");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("1530");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("2030");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("2530");
            table.Cell().Element(CellStyle).Background("#DCDCDC").Padding(2).AlignLeft().Text("% Limit");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("30");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("530");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("1030");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("1530");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("2030");
            table.Cell().Element(CellStyle).Background("#FFFFFF").Padding(2).AlignCenter().Text("2530");
        });
    }

    void ComposeFirstSectionSecondPage(IContainer container)
    {
        container.Padding(3).Row(row =>
        {
            row.RelativeItem(3).Height(50).DefaultTextStyle(x => x.FontSize(8)).Background(QuestPDF.Helpers.Colors.Grey.Lighten2).Border(1).Padding(3).Column(column =>
            {
                column.Item().AlignLeft().Text("LD - Lactate Dehydrogenase").ExtraBold().FontSize(8);
                column.Item().Row(row =>
                {
                    row.RelativeItem(2).Text("Method:").ExtraBold();
                    row.RelativeItem(2).Text("Lactate Dehydrogenase");
                    row.RelativeItem();
                    row.RelativeItem();
                });
                column.Item().Row(row =>
                {
                    row.RelativeItem(2).Text("Method Type:").ExtraBold();
                    row.RelativeItem(2).Text("Enzymatic UV");
                    row.RelativeItem().Text("Units: ").ExtraBold();
                    row.RelativeItem().Text("U/L");
                });
            });
            row.Spacing(10);
            row.RelativeItem(2).DefaultTextStyle(x => x.FontSize(8)).Border(1).Padding(3).Column(column =>
            {
                column.Item().Row(row =>
                {
                    row.RelativeItem().Text("Part #:").ExtraBold();
                    row.RelativeItem().Text("1300ab");
                    row.RelativeItem();
                    row.RelativeItem();
                });
                column.Item().Row(row =>
                {
                    row.RelativeItem().Text("Description:").ExtraBold();
                    row.RelativeItem().Text("GC3");
                    row.RelativeItem();
                    row.RelativeItem();
                });
                column.Item().Row(row =>
                {
                    row.RelativeItem().Text("Lot #:").ExtraBold();
                    row.RelativeItem().Text("10634898");
                    row.RelativeItem();
                    row.RelativeItem();
                });
                column.Item().Row(row =>
                {
                    row.RelativeItem().Text("Expiration:").ExtraBold();
                    row.RelativeItem().Text("2/28/2023");
                    row.RelativeItem();
                    row.RelativeItem();
                });
                column.Item().Row(row =>
                {
                    row.RelativeItem().Text("Submission ID:").ExtraBold();
                    row.RelativeItem().Text("3-302763");
                    row.RelativeItem();
                    row.RelativeItem();
                });
            });
        });
    }

    void ComposeSecondSectionSecondPage(IContainer container)
    {
        container.Padding(3).Row(row =>
        {
            row.RelativeItem(3).DefaultTextStyle(x => x.FontSize(7)).Padding(3).PaddingTop(-30).Column(column =>
            {
                column.Item().Row(row =>
                {
                    row.RelativeItem(1).Text("Account #: ").ExtraBold();
                    row.RelativeItem(3).Text("25212");
                });
                column.Item().Row(row =>
                {
                    row.RelativeItem(1).Text("Company:").ExtraBold();
                    row.RelativeItem(3).Text("LGC Maine Standards");
                });
                column.Item().Row(row =>
                {
                    row.RelativeItem(1).Text("Facility:").ExtraBold();
                    row.RelativeItem(3).Text("St. Mary's Regional Testing Center");
                });
                column.Item().Row(row =>
                {
                    row.RelativeItem(1).Text("Analyzer: ").ExtraBold();
                    row.RelativeItem(3).Text("Test");
                });
                column.Item().Row(row =>
                {
                    row.RelativeItem(1).Text("Serial #:").ExtraBold();
                    row.RelativeItem(3).Text("85050");
                });
                column.Item().Row(row =>
                {
                    row.RelativeItem(1).Text("Model: ").ExtraBold();
                    row.RelativeItem(3).Text("Roche Diagnostics - cobas® - 8000");
                });
                column.Item().Row(row =>
                {
                    row.RelativeItem(1).Text("Test Date:").ExtraBold();
                    row.RelativeItem(3).Text("05/02/2023");
                });
                column.Item().Row(row =>
                {
                    row.RelativeItem(1).Text("Technician:").ExtraBold();
                });
            });
            row.Spacing(10);
            row.RelativeItem(2).DefaultTextStyle(x => x.FontSize(7)).Padding(3).Column(column =>
            {
                column.Item().Row(row =>
                {
                    row.RelativeItem(1).Text("TEa:").ExtraBold();
                    row.RelativeItem(3).Text("5.00 U/L or 20.00 %, whichever is greater");
                });
                column.Item().Row(row =>
                {
                    row.RelativeItem(1).Text("Source:").ExtraBold();
                    row.RelativeItem(3).Text("MSC");
                });
                column.Item().Row(row =>
                {
                    row.RelativeItem(1).Text("Applied limit:").ExtraBold();
                    row.RelativeItem(3).Text("50%");
                });
                column.Item().Row(row =>
                {
                    row.RelativeItem(1).Text("Comments:").ExtraBold();
                    row.RelativeItem(3).Text("");
                });
            });
        });
    }

    void ComposeThirdSectionSecondPage(IContainer container)
    {
        container.Row(row =>
        {
            row.RelativeItem(3).DefaultTextStyle(x => x.FontSize(7)).Column(column =>
            {
                column.Item().Text("Data Set").FontSize(8).ExtraBold();
                column.Item().Element(ComposeTable1Page2);
                column.Item().Padding(3);
                column.Item().Element(ComposeLineThrough);
                column.Item().Text("Linearity Data Analysis").FontSize(8).ExtraBold();
                column.Item().Element(ComposeTable2Page2);
                column.Item().Padding(3);
                column.Item().DefaultTextStyle(x => x.FontSize(6)).Padding(3).Text(txt =>
                {
                    txt.Span("Regression: Theoretical vs. Observed Mean");
                    txt.Span("y = 0.536 x + 410.881 r2 = 0.848").ExtraBlack().ExtraBold();
                });
                column.Item().DefaultTextStyle(x => x.FontSize(6).ExtraBold()).Padding(1).Text("(*) Theoretical values are calculated from the best-fit line between L1, L3.");
            });
            row.Spacing(5);
            row.RelativeItem(2).DefaultTextStyle(x => x.FontSize(7)).Column(column =>
            {
                column.Item().Element(ComposeGraph);
            });
        });
    }

    void ComposeFourthSectionSecondPage(IContainer container)
    {
        container.Row(row =>
        {
            row.RelativeItem(3).DefaultTextStyle(x => x.FontSize(7)).Column(column =>
            {
                column.Item().Text("Peer Group Statistics").FontSize(8).ExtraBold();
                column.Item().Element(ComposeTable3Page2);
                column.Item().Padding(3);
                column.Item().Element(ComposeLineThrough);
                column.Item().Text("Peer Group Comparison").FontSize(8).ExtraBold();
                column.Item().Element(ComposeTable4Page2);
            });
            row.Spacing(5);
            row.RelativeItem(2).DefaultTextStyle(x => x.FontSize(7)).Column(column =>
            {
                column.Item().Element(ComposeSecondGraph);
            });
        });
    }

    void ComposeLastSectionSecondPage(IContainer container)
    {
        container.DefaultTextStyle(x => x.FontSize(6)).PaddingTop(5).Row(row =>
        {
            row.RelativeItem().Column(column =>
            {
                column.Item().AlignLeft().Text(txt =>
                {
                    txt.Line("Data Review").FontSize(7).ExtraBold();
                    txt.Span("Reportable Range:").Underline().Bold();
                    txt.Line("  10.00 to 1,000.00 U/L");
                    txt.Span("Verified Range:").Underline().Bold();
                    txt.Line("  27.5 to 2,527.50 U/L");
                });
            });
            row.RelativeItem().Column(column =>
            {
                column.Item().AlignLeft().Text(txt =>
                {
                    txt.Span("Authorizing Signature:").Bold();
                    txt.Line("                                            ").Underline();
                    txt.Span("Name:").Bold();
                });
            });
            row.RelativeItem().Column(column =>
            {
                column.Item().AlignRight().Text(txt =>
                {
                    txt.Line("Date:05/02/2023").Bold();
                });
            });
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

    void ComposeFooter2(IContainer container)
    {
        byte[] validate = File.ReadAllBytes("./validate.png");
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

            row.RelativeItem().AlignRight().Column(column =>
            {
                column.Item().AlignRight().Text(txt =>
                {
                    txt.Span("Page ");
                    txt.CurrentPageNumber();
                    txt.Span(" of ");
                    txt.TotalPages();
                });

                column.Item().PaddingRight(-15).Width(80, Unit.Point).AlignRight().Image(validate);
            });
        });
    }
}