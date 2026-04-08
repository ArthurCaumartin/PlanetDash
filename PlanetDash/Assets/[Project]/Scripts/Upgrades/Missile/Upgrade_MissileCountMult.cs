
using UnityEngine;

[UpgradePriority(UpgradePriority.StatMult)]
[CreateAssetMenu(fileName = "Missile_CountMult", menuName = "Mwa/Upgrade/Missile/CountMult")]
public class Upgrade_MissileCountMult : ScriptableUpgrade
{
    [SerializeField] private float _mult = 1;

    public override void ApplyStatChange(IAbility ability)
    {
        base.ApplyStatChange(ability);
        if (ability is Ability_Missile missileAbility)
        {
            if (_mult < 0)
            {
                Debug.LogError(name + ": _mult is negative !!!");
                return;
            }
            missileAbility.missileCount = (int)Mathf.Round(missileAbility.missileCount * _mult);
        }
    }
}