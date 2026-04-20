using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgradeControler : MonoBehaviour
{
    [SerializeField] private AbilityControler _abilityControler;
    [SerializeField] private List<ScriptableUpgrade> _startUpgrade = new List<ScriptableUpgrade>();
    [SerializeField] private List<ScriptableUpgrade> _upgradeList = new List<ScriptableUpgrade>();

    private void Start()
    {
        foreach (var item in _startUpgrade)
            SetNewUpgrade(item);
    }

    public void SetNewUpgrade(ScriptableUpgrade newUpgrade)
    {
        _upgradeList.Add(newUpgrade);
        _abilityControler.AddAbility(newUpgrade.Ability);
    }
}