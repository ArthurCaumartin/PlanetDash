using System.Collections.Generic;
using UnityEngine;

public class AbilityControler : MonoBehaviour
{
    [SerializeField] private Transform _abilityContainer;
    [SerializeField] private List<Ability> _abilityList = new List<Ability>();

    public void AddAbility(Ability newAbility)
    {
        if (_abilityList.ContaineType(newAbility.GetType())) return;

        Ability ability = Instantiate(newAbility, _abilityContainer);
        _abilityList.Add(ability);
    }
}