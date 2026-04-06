using UnityEngine;

public class AbilityControler : MonoBehaviour
{
    [SerializeField] private DashControler _dashControler;
    public IAbility abilityTest;

    void Start()
    {
        if (abilityTest != null)
            UnSubAbilityToControlerEvents(abilityTest);

        abilityTest = new Ability_Missile(new BaseAbility());
        SubAbilityToControlerEvents(abilityTest);
    }

    private void UnSubAbilityToControlerEvents(IAbility ability)
    {
        _dashControler.onDashStart.RemoveListener(ability.OnDashStart);
        _dashControler.onDashMove.RemoveListener(ability.OnDashMove);
        _dashControler.onDashHit.RemoveListener(ability.OnDashHit);
        _dashControler.onDashEnd.RemoveListener(ability.OnDashEnd);
    }

    private void SubAbilityToControlerEvents(IAbility ability)
    {
        _dashControler.onDashStart.AddListener(ability.OnDashStart);
        _dashControler.onDashMove.AddListener(ability.OnDashMove);
        _dashControler.onDashHit.AddListener(ability.OnDashHit);
        _dashControler.onDashEnd.AddListener(ability.OnDashEnd);
    }
}

