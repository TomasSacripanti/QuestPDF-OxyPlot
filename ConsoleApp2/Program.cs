using OxyPlot;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using System.Diagnostics;

QuestPDF.Settings.License = LicenseType.Community;

var filePathMSDR = "msdr-report.pdf";
//var filePathRSSM = "invoice-rssm.pdf";

var oxyplotController = new OxyplotController();

var msdrDocument = new MSDRDocument(oxyplotController);
msdrDocument.GeneratePdf(filePathMSDR);
msdrDocument.ShowInPreviewer();

//var rssmDocument = new RSSMDocument(oxyplotController);
//rssmDocument.GeneratePdf(filePathRSSM);
//rssmDocument.ShowInPreviewer();

Process.Start("explorer.exe", filePathMSDR);

