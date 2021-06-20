using System;
public abstract class Report
{
    public readonly string title;
    public readonly string dialogScene;
    public readonly ReportSeverity severity;
    public readonly Personnel reporter;

    public Report(
        string title,
        string dialogScene,
        ReportSeverity severity,
        Personnel reporter
    )
    {
        this.title = title;
        this.dialogScene = dialogScene;
        this.severity = severity;
        this.reporter = reporter;
    }
}
