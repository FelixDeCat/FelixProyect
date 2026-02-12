using UnityEngine;

public class GPSManager : MonoBehaviour
{
    MyDataService _dataService;

    private void Awake()
    {
        _dataService = new MyDataService("MyChunkExample01.db");
        _dataService.CreateDB();
    }

    public void SaveStaticGPS(string ID)
    {
        var gps = new GPS
        {
            Id = System.Guid.NewGuid().ToString(),
            State = "Active"
        };
       // _dataService.CreateGPS(gps);
        Debug.Log($"GPS with ID {gps.Id} saved to database.");
    }
}
