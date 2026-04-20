using UnityEngine;


[CreateAssetMenu(fileName = "Upgrade_", menuName = "Mwa/Upgrade")]
public class ScriptableUpgrade : ScriptableObject
{
    [SerializeField] private Ability _abilityPrefab;

    [SerializeField] private int _strenght = 0;
    [SerializeField] private int _intelligence = 0;
    [SerializeField] private int _dexterity = 0;
    [SerializeField] private int _luck = 0;


    public Ability Ability => _abilityPrefab;
}