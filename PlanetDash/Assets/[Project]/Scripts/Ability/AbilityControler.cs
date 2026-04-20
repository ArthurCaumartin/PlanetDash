using System.Collections.Generic;
using UnityEngine;

public class AbilityControler : MonoBehaviour
{
    [SerializeField] private List<ScriptableUpgrade> _startUpgradeList = new List<ScriptableUpgrade>();
    [SerializeField] private DashController _dashControler;
    [SerializeField] public AbilityDecorator abilityTest;
    // [SerializeField] private IAbility[] abilities;

    private void Start()
    {
        SetNewAbilityUpgrade(_startUpgradeList);
    }

    public void SetNewAbilityUpgrade(List<ScriptableUpgrade> upgradeList)
    {
        _startUpgradeList = upgradeList;
        if (abilityTest != null)
            UnSubAbilityToEvents(abilityTest);
        abilityTest = ComposeAbility() as AbilityDecorator;
        SubAbilityToEvents(abilityTest);
    }

    public IAbility ComposeAbility()
    {
        IAbility ability = new BaseAbility();
        int prioCount = EnumUtils.MaxIntValue<UpgradePriority>();
        // print("PrioCount : " + prioCount);
        for (int i = 0; i <= prioCount; i++)
            ability = ComposeForPriority(i, ability);
        return ability;
    }

    private IAbility ComposeForPriority(int priority, IAbility ability)
    {
        foreach (ScriptableUpgrade upgrade in _startUpgradeList)
        {
            int upgradePrio = upgrade.GetPriority();
            if (upgradePrio == -1)
            {
                Debug.LogError(upgrade.name + " / NO PRIO DEFINE !!!");
                continue;
            }

            if (upgradePrio == priority)
            {
                // print(item.name + " Apply //" + upgradePrio);
                upgrade.ApplyStatChange(ability);
            }
        }
        return ability;
    }

    private void SubAbilityToEvents(IAbility ability)
    {
        _dashControler.onDashStart.AddListener(ability.OnDashStart);
        _dashControler.onDashMove.AddListener(ability.OnDashMove);
        _dashControler.onDashHit.AddListener(ability.OnDashHit);
        _dashControler.onDashEnd.AddListener(ability.OnDashEnd);
    }

    private void UnSubAbilityToEvents(IAbility ability)
    {
        _dashControler.onDashStart.RemoveListener(ability.OnDashStart);
        _dashControler.onDashMove.RemoveListener(ability.OnDashMove);
        _dashControler.onDashHit.RemoveListener(ability.OnDashHit);
        _dashControler.onDashEnd.RemoveListener(ability.OnDashEnd);
    }
}

