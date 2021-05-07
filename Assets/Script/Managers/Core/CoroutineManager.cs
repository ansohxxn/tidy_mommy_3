using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineManager
{
    private Dictionary<float, WaitForSeconds> timeInterval = new Dictionary<float, WaitForSeconds>();
    
    public WaitForSeconds WaitSeconds(float seconds)
    {
        if (!timeInterval.ContainsKey(seconds))
            timeInterval.Add(seconds, new WaitForSeconds(seconds));
        return timeInterval[seconds];
    }
}
