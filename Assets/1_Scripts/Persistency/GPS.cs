using SQLite4Unity3d;
using UnityEngine;

/// <summary>
/// G: Generic
/// P: Persistent
/// S: State
/// Example: 
/// </summary>
public class GPS
{
    [PrimaryKey, AutoIncrement]
    public string Id { get; set; }
    public string State { get; set; }
}
