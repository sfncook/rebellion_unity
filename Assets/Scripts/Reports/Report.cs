using System;
public abstract class Report
{
    public readonly string title;
    public readonly string dialogScene;
    public readonly ReportSeverity severity;
    public readonly Personnel reporter;
    public readonly int dayComplete;

    public Report(
        string title,
        string dialogScene,
        ReportSeverity severity,
        Personnel reporter,
        int dayComplete
    )
    {
        this.title = title;
        this.dialogScene = dialogScene;
        this.severity = severity;
        this.reporter = reporter;
        this.dayComplete = dayComplete;
    }
}
