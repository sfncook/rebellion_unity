using System;
public class StoryLineReport: Report
{
    public readonly Personnel reporter;

    public StoryLineReport(
        string title,
        Personnel reporter
    ): base(title, "Story Line Report Dialog")
    {
        this.reporter = reporter;
    }
}
