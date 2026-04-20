using UnityEngine;


[CreateAssetMenu(fileName = "Upgrade_", menuName = "Mwa/Upgrade")]
public class ScriptableUpgrade : ScriptableObject
{
    [SerializeField] private ScriptableAbility _scriptableAbility;
    [SerializeField] private int _strenght = 0;
    [SerializeField] private int _intelligence = 0;
    [SerializeField] private int _dexterity = 0;

    public int Strenght => _strenght;
    public int Intelligence => _intelligence;
    public int Dexterity => _dexterity;


    public ScriptableAbility AbilityData => _scriptableAbility;
}
