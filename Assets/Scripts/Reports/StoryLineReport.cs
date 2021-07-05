using System;
using System.Collections.Generic;

public class StoryLineReport: Report
{
    public readonly List<string> contentPages;

    public StoryLineReport(
        string title,
        Personnel reporter,
        int dayComplete,
        List<string> contentPages
    ) : base(
        title,
        "Story Line Report Dialog",
        ReportSeverity.Info,
        reporter,
        dayComplete
    )
    {
        this.contentPages = new List<string>(contentPages);
    }
}
