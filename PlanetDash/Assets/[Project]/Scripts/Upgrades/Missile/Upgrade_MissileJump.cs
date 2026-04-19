
using UnityEngine;

[UpgradePriority(UpgradePriority.BaseWarper)]
[CreateAssetMenu(fileName = "Missile_Jump", menuName = "Mwa/Upgrade/Missile/Jump")]
public class Upgrade_MissileJump : ScriptableUpgrade
{
    [SerializeField] private Missile _missilePrefab;
    [SerializeField] private float _detectionRadius = 20;
    [SerializeField] private LayerMask _detectionLayerMask;

    public override IAbility Decorate(IAbility ability)
    {
        // Debug.Log("Decorate with missile jump.");
        return new Abiltiy_MissileJump(ability, _missilePrefab, _detectionRadius, _detectionLayerMask);
    }
}