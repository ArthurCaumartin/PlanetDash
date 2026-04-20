using System;

public class UpgradeFamilyAtribute : Attribute
{
    public UpgradeFamily family { get; }

    public UpgradeFamilyAtribute(UpgradeFamily family)
    {
        this.family = family;
    }
}
