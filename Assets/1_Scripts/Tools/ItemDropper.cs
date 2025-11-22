using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemDropper
{
    Rigidbody body;

    public ItemDropper(Rigidbody rig)
    {
        if (ItemDropperPositions.Instance == null)
            throw new System.Exception("No puedo trabajar sin una instancia de ItemDropperPositions, " +
                "por favor crearlo en un Manager para obtener las posiciones");

            body = rig;
        animate = false;

        
    }

    bool animate;
    float timer;
    public void Drop()
    {
        animate = true;
    }

    public void Update()
    {
        if (animate)
        {

        }
    }
}

public class ItemDropperPositions
{
    public static ItemDropperPositions Instance;

    public Vector3[] all_directions;
    int index = -1;

    public Vector3 PopDir
    {
        get 
        {
            Instance.index++;
            Instance.index %= Instance.all_directions.Length;
            return Instance.all_directions[index];
        }
    }

    public static Vector3[] GetDirections(int quant)
    {
        return Instance._getdirections(quant);
    }
    Vector3[] _getdirections(int quant)
    {
        int i = 0;
        Vector3[] dirs = new Vector3[quant];
        while (i < quant)
        {
            dirs[i] = PopDir;
            i++;
        }
        return dirs;
    }
    public ItemDropperPositions(int randomizations = 5, params Transform[] _directions_to_randomize)
    {
        if (Instance == null) Instance = this;
        else
        {
            throw new System.Exception($"No puede Haber dos ItemDropperPositions, ya existe la instancia: " + Instance.GetHashCode());
        }

        var seed = _directions_to_randomize.Select(x => x.position).ToArray();
        List<Vector3> toShuffle = new List<Vector3>(seed);
        List<Vector3> toAdd = new List<Vector3>();

        toShuffle.OrderBy(x => Random.Range(0, toShuffle.Count));

        for (int r = 0; r < randomizations; r++)
        {
            for (int i = 0; i < toShuffle.Count; i++)
            {
                toAdd.Add(toShuffle[i]);
            }
        }

        all_directions = toShuffle.ToArray();
    }
}
