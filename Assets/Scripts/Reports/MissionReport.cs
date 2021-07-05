using System.Collections.Generic;

public abstract class MissionReport: Report
{
    private static Dictionary<bool, string> successStr = new Dictionary<bool, string> {
        { true, "Success!"},
        { false, "Failure" }
    };

    public bool success;

    public MissionReport(
        Personnel reporter,
        MissionType missionType,
        bool success,
        int dayComplete
    ) : base(
        missionType.name + " Mission " + successStr[success],
        "Mission Report Dialog",
        success ? ReportSeverity.Success : ReportSeverity.Failure,
        reporter,
        dayComplete
    )
    {
        this.success = success;
    }
}
