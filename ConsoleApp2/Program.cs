using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Filters;
using OxyPlot;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using System.Diagnostics;

QuestPDF.Settings.License = LicenseType.Community;

var filePath = "invoice2.pdf";

var oxyplotController = new OxyplotController();

//var msdrDocument = new MSDRDocument(oxyplotController);
//msdrDocument.GeneratePdf(filePath);
//msdrDocument.ShowInPreviewer();

var rssmDocument = new RSSMDocument(oxyplotController);
rssmDocument.GeneratePdf(filePath);
rssmDocument.ShowInPreviewer();

Process.Start("explorer.exe", filePath);

