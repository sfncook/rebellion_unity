using System.Collections.Generic;

public abstract class MissionReport: Report
{
    private static Dictionary<bool, string> successStr = new Dictionary<bool, string> {
        { true, "Success!"},
        { false, "Failure" }
    };

    public readonly MissionType missionType;
    public bool success;

    public MissionReport(
        Personnel reporter,
        MissionType missionType,
        bool success,
        int dayComplete,
        bool showImmediately = false
    ) : base(
        missionType.name + " Mission " + successStr[success],
        "Mission Report",
        success ? ReportSeverity.Success : ReportSeverity.Failure,
        reporter,
        dayComplete,
        showImmediately
    )
    {
        this.missionType = missionType;
        this.success = success;
    }
}
