using UnityEngine;

[CreateAssetMenu(fileName = "FarmElementData", menuName = "Items/FarmElement")]
public class FarmElementData : ScriptableObject
{
    public string elementName = "New Farm Element";
    public int secondsToGrow = 0;
    public int minutesToGrow = 1;
    public int hoursToGrow = 0;

    /// <summary>
    /// An array containing the possible growth states.
    /// Each state includes the time required to reach it and the corresponding model.
    /// </summary>
    public GrowState[] states;
}

[System.Serializable]
public class GrowState
{
    public int secondsToGrow = 0;
    public int minutesToGrow = 1;
    public int hoursToGrow = 0;
    public GameObject model;
}

public enum PlantState
{
    seed,
    sprout,
    growing,
    harvestable
}
