
public class InfoReport : Report
{
    public readonly string content;
    public readonly AbstractTarget targetUnit;

    public InfoReport(
        string title,
        Personnel reporter,
        int dayComplete,
        ReportSeverity severity,
        string content,
        AbstractTarget targetUnit
    ) : base(
        title,
        "Info Report Dialog",
        severity,
        reporter,
        dayComplete
    )
    {
        this.content = content;
        this.targetUnit = targetUnit;
    }
}
