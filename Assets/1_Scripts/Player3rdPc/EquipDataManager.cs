using System;
using UnityEngine;

[System.Serializable]
public class EquipDataManager : IStarteable
{
    public static EquipDataManager Instance;
    void IStarteable.Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    internal Vector3 GetPosition(EquipableType equipable_type)
    {
        switch (equipable_type)
        {
            case EquipableType.oneHand: return rightHand.position;
            case EquipableType.twoHand: return rightHand.position;
            case EquipableType.head: return head.position;
            case EquipableType.chest: return chest.position;
            case EquipableType.pants: return pants.position;
            case EquipableType.back: return back.position;
            case EquipableType.foots: return foots.position;
            case EquipableType.space1: return space1.position;
            case EquipableType.space2: return space2.position;
            case EquipableType.ring1: return ring1.position;
            case EquipableType.ring2: return ring2.position;
            case EquipableType.neck: return neck.position;
        }
        return Vector3.zero;
    }

    [SerializeField] Transform leftHand;
    [SerializeField] Transform rightHand;
    [SerializeField] Transform head;
    [SerializeField] Transform chest;
    [SerializeField] Transform pants;
    [SerializeField] Transform back;
    [SerializeField] Transform foots;
    [SerializeField] Transform space1;
    [SerializeField] Transform space2;
    [SerializeField] Transform ring1;
    [SerializeField] Transform ring2;
    [SerializeField] Transform neck;
    public static Transform LeftHand    { get => Instance.leftHand; }
    public static Transform RightHand   { get => Instance.rightHand; }
    public static Transform Head        { get => Instance.head; }
    public static Transform Chest       { get => Instance.chest; }
    public static Transform Pants       { get => Instance.pants; }
    public static Transform Back        { get => Instance.back; }
    public static Transform Foots       { get => Instance.foots; }
    public static Transform Space1      { get => Instance.space1; }
    public static Transform Space2      { get => Instance.space2; }
    public static Transform Ring1       { get => Instance.ring1; }
    public static Transform Ring2       { get => Instance.ring2; }
    public static Transform Neck        { get => Instance.neck; }
}
