using System;

public class UpgradePriorityAttribute : Attribute
{
    public UpgradePriority Priority { get; }
    public UpgradePriorityAttribute(UpgradePriority priority)
    {
        Priority = priority;
    }
}
