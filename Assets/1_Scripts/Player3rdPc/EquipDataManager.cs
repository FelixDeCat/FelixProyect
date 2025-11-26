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
