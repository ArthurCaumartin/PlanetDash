
using UnityEngine;

[UpgradePriority(UpgradePriority.Base)]
[UpgradeFamilyAtribute(UpgradeFamily.Missile)]
[CreateAssetMenu(fileName = "Missile_Unlock", menuName = "Mwa/Upgrade/Missile/Missile Base Upgrade")]
public class Upgrade_Missile : ScriptableUpgrade
{
    [SerializeField] private Missile _missilePrefab;

    public override IAbility Decorate(IAbility ability)
    {
        // Debug.Log("Decorate with missile.");
        return new Ability_Missile(ability, _missilePrefab);
    }
}
