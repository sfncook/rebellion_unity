using System;
public abstract class Report
{
    public readonly string title;
    public readonly string dialogScene;

    public Report(string title, string dialogScene)
    {
        this.title = title;
        this.dialogScene = dialogScene;
    }
}
