using OxyPlot;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using System.Diagnostics;

QuestPDF.Settings.License = LicenseType.Community;

var filePath = "invoice2.pdf";

var model = InvoiceDocumentDataSource.GetInvoiceDetails();
var oxyplotController = new OxyplotController();
var document = new InvoiceDocument(model, oxyplotController);
document.GeneratePdf(filePath);

document.ShowInPreviewer();

Process.Start("explorer.exe", filePath);
