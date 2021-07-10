using System.Collections.Generic;
using UnityEngine;

public enum ReportSeverity
{
    Success, Failure, Info, Warning, Danger
}

public static class ReportSeverityHelper
{
    public static Dictionary<ReportSeverity, Color> severityToColor = new Dictionary<ReportSeverity, Color> {
        { ReportSeverity.Danger, Color.red},
        { ReportSeverity.Failure, new Color(1.0f, 0.2f, 0.2f)},
        { ReportSeverity.Info, Color.blue},
        { ReportSeverity.Success, Color.green},
        { ReportSeverity.Warning, Color.yellow}
    };
}