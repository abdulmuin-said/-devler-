namespace KeyloggerTespitSistemi.Models.Enums;

public enum DetectionRuleType
{
    UnknownPublisher = 1,
    RunsAtStartup = 2,
    RunsFromTempOrAppData = 3,
    KeyboardAccessSuspicion = 4,
    SuspiciousNetworkConnection = 5,
    FakeSystemFileName = 6,
    RunsInBackground = 7
}
