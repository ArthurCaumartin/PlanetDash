using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerData_", menuName = "Mwa/PlayerData_")]
public class ScriptablePlayerData : ScriptableObject
{
    [SerializeField] private int _strenght;
    [SerializeField] private int _intelligence;
    [SerializeField] private int _dexterity;
    [SerializeField] private List<ScriptableAbility> _abilitySelectedList = new List<ScriptableAbility>();

    public int Strenght => _strenght;
    public int Intelligence => _intelligence;
    public int Dexterity => _dexterity;
    public List<ScriptableAbility> Abilities => _abilitySelectedList;

    public void RecordNewUpgrade(ScriptableUpgrade upgrade)
    {
        ScriptableAbility abilityData = upgrade.AbilityData;
        if (abilityData)
            _abilitySelectedList.Add(abilityData);

        _strenght += upgrade.Strenght;
        _intelligence += upgrade.Intelligence;
        _dexterity += upgrade.Dexterity;
    }

    public void ClearData()
    {
        _strenght = 0;
        _intelligence = 0;
        _dexterity = 0;
        _abilitySelectedList.Clear();
    }
}