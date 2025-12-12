using UnityEngine;

[System.Serializable]
public struct DamageData
{
    public int damage;
    public KnockBackInfo[] knockBackInfo;
    public DamageEffect[] effects;
}
[System.Serializable]
public struct DamageEffect
{
    public string effectName;
    public int initialDamage;
    public float duration;
    public int progressiveDamage;
}
[System.Serializable]
public struct KnockBackInfo
{
    public Vector3 origin;
    public Vector3 direction;
    public float force;
}
