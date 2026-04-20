using UnityEngine;

[CreateAssetMenu(fileName = "Ability_", menuName = "Mwa/Ability")]
public class ScriptableAbility : ScriptableObject
{
    [SerializeField] private string _abilityName;
    [SerializeField] private string _abilityDescription;
    [SerializeField] private Sprite _abilityLogo;
    [Space]
    [SerializeField] private Ability _abilityPrefab;
    [SerializeField] private GameObject _abilityCardPrefab;

    public Ability Ability => _abilityPrefab;
}