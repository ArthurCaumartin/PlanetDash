
using UnityEngine;

[UpgradePriority(UpgradePriority.StatFlat)]
[CreateAssetMenu(fileName = "Missile_CountFlat", menuName = "Mwa/Upgrade/Missile/CountFlat")]
public class Upgrade_MissileCountFlat : ScriptableUpgrade
{
    [SerializeField] private int _countToAdd = 0;

    public override void ApplyStatChange(IAbility ability)
    {
        base.ApplyStatChange(ability);
        if (ability is Ability_Missile abilityMissile)
        {
            abilityMissile.missileCount = Mathf.Clamp(abilityMissile.missileCount + _countToAdd, 0, int.MaxValue);
        }
    }
}