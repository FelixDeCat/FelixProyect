using UnityEngine;

public static class Extensions
{
    public static string FiveChar(this string text)
    {
        text = (text.Length > 5 ? text.Substring(0, 5) : text.PadRight(5, '_'));
        return text;
    }
}
public static class Tools
{
    public static Vector3 Random_XZ_PosInBound(Vector3 center, float radius)
    {
        float xMin = center.x - radius;
        float xMax = center.x + radius;
        float zMin = center.z - radius;
        float zMax = center.z + radius;

        return new Vector3(Random.Range(xMin, xMax), center.y, Random.Range(zMin, zMax));
    }
}
