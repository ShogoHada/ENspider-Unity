using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

static public class ProbabilityRatio<T>
{
    static public T GetResult(Dictionary<T, int> events)
    {
        int sum = 0;
        foreach (var e in events)
        {
            sum += e.Value;
        }
        int rnd = UnityEngine.Random.Range(0, sum);
        int tmp = 0;
        foreach (var e in events)
        {
            tmp += e.Value;
            if (rnd < tmp)
            {
                return e.Key;
            }
        }
        return events.FirstOrDefault(x => x.Value > 0).Key;
    }
}