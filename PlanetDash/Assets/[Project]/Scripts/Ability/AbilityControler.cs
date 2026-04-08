using System.Collections.Generic;
using UnityEngine;

public class AbilityControler : MonoBehaviour
{
    [SerializeField] private List<ScriptableUpgrade> _startUpgradeList = new List<ScriptableUpgrade>();
    [SerializeField] private DashController _dashControler;
    public IAbility abilityTest;

    private void Start()
    {
        SetNewAbilityUpgrade(_startUpgradeList);
    }

    public void SetNewAbilityUpgrade(List<ScriptableUpgrade> upgradeList)
    {
        _startUpgradeList = upgradeList;
        if (abilityTest != null)
            UnSubAbilityToEvents(abilityTest);
        abilityTest = ComposeAbility();
        SubAbilityToEvents(abilityTest);
    }

    public IAbility ComposeAbility()
    {
        IAbility ability = new BaseAbility();
        int prioCount = EnumUtils.MaxIntValue<UpgradePriority>();
        for (int i = 0; i < prioCount; i++)
            ability = ComposeForPriority(i, ability);
        return ability;
    }

    private IAbility ComposeForPriority(int priority, IAbility ability)
    {
        foreach (var item in _startUpgradeList)
        {
            int upgradePrio = UpgradePriorityReader.GetPriority(item);
            if (upgradePrio == -1)
                continue;

            if (upgradePrio == priority)
            {
                print(item.name + " upgradePrio : " + upgradePrio);
                ability = item.Decorate(ability);
                item.ApplyStatChange(ability);
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

