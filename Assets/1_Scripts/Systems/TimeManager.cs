using System;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoSingleton<TimeManager>
{
    List<TimedData> datList = new List<TimedData>();
    public override void SingletonAwake()
    {
         string savedData = PlayerPrefs.GetString("TimedDataList", "");
    }

    public void AddTimedData(TimedData element)
    {
        if (!datList.Contains(element))
        {
            datList.Add(element);
            PlayerPrefs.SetString("TimedDataList", string.Join(";", datList.ConvertAll(TimedDataToString)));
        }
    }

    public void RemoveTimedData(TimedData element)
    {
        if (datList.Contains(element))
        {
            datList.Remove(element);
        }
    }

    private void Update()
    {
        long current = TimeService.NowUnixSeconds;
        for (int i = datList.Count - 1; i >= 0; i--)
        {
            if (current >= datList[i].endTime)
            {
                // Dispara evento de finalización de entidad
                Debug.Log($"elemento de granja {datList[i].id} ha finalizado.");
                
                // SI ya esta lo elimino
                datList.RemoveAt(i);
            }
        }
    }

    #region Serialization
    /// <summary>
    /// Converts the specified TimedData instance to a comma-separated string representation.
    /// </summary>
    /// <param name="data">The TimedData object to convert to a string. Cannot be null.</param>
    /// <returns>A string containing the id, begin time, and end time of the TimedData, separated by commas.</returns>
    public string TimedDataToString(TimedData data)
    {     
        return $"{data.id},{data.beginTime},{data.endTime}"; 
    }

    /// <summary>
    /// Parses a comma-separated string into a TimedData object containing an identifier and time values.
    /// </summary>
    /// <param name="data">A string containing three comma-separated values: the identifier, the begin time, and the end time. The begin
    /// and end times must be valid 64-bit integer representations.</param>
    /// <returns>A TimedData object populated with the identifier, begin time, and end time extracted from the input string.</returns>
    /// <exception cref="FormatException">Thrown if the input string does not contain exactly three comma-separated values or if the time values cannot be
    /// parsed as 64-bit integers.</exception>
    public TimedData StringToTimeData(string data)
    {
        string[] parts = data.Split(',');
        if (parts.Length != 3)
        {
            throw new FormatException("El formato de la cadena no es válido.");
        }
        return new TimedData
        {
            id = parts[0],
            beginTime = long.Parse(parts[1]),
            endTime = long.Parse(parts[2])
        };
    }
    #endregion

}

/// <summary>
/// Servicio estático que expone la hora actual en segundos Unix (UTC).
/// </summary>
/// <remarks>
/// Utiliza <see cref="DateTimeOffset.UtcNow"/> y <see cref="DateTimeOffset.ToUnixTimeSeconds"/>
/// para calcular los segundos transcurridos desde el 1 de enero de 1970 (epoch) en tiempo UTC.
/// Diseñado para ofrecer una marca de tiempo consistente y adecuada para persistencia o sincronización.
/// </remarks>
public static class TimeService
{
    /// <summary>
    /// Obtiene el instante actual en segundos Unix (UTC).
    /// </summary>
    /// <value>
    /// Un <see cref="long"/> que representa el número de segundos transcurridos desde
    /// la época Unix (1970-01-01T00:00:00Z) hasta el momento actual en UTC.
    /// </value>
    /// <remarks>
    /// Esta propiedad es de solo lectura y estática; cada acceso calcula el valor a partir
    /// de <see cref="DateTimeOffset.UtcNow"/>. No realiza caching ni depende del reloj local.
    /// </remarks>
    /// <example>
    /// // Obtener la marca de tiempo actual en segundos Unix
    /// long unixNow = TimeService.NowUnixSeconds;
    /// </example>
    public static long NowUnixSeconds
    {
        get
        {
            return DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }
    }
}
