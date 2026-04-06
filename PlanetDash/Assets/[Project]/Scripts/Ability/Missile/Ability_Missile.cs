
using UnityEditor;
using UnityEngine;

public class Ability_Missile : AbilityDecorator
{
    public Ability_Missile(IAbility ability) : base(ability) { }

    public override void OnDashHit(Health healthHit, Health[] healthsHitArray)
    {
        base.OnDashHit(healthHit, healthsHitArray);
        //TODO horrible de get la ref avec un LoadAsset o_O A CHANGER !!!! (je sais pas comment...)
        GameObject mRef = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/[Project]/Prefabs/Projectiles/Missile.prefab");
        for (int i = 0; i < 10; i++)
        {
            GameObject m = GameObject.Instantiate(mRef, healthHit.transform.position + healthHit.transform.up, healthHit.transform.rotation);
            m.GetComponent<Missile>().Init(healthsHitArray.GetRandome(), 50, 10, 150, 3);
        }
    }
}

