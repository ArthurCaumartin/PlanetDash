using UnityEngine;

public abstract class ScriptableUpgrade : ScriptableObject
{
    public virtual IAbility Decorate(IAbility ability) { return ability; }
    public virtual void ApplyStatChange(IAbility ability)
    {
        // Debug.Log(name + " Apply stat change !");
    }
}