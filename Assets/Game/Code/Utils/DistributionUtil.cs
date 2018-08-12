using UnityEngine;
using System.Collections.Generic;
using UnityTK;

public static class DistributionUtil<TObject>
{
    public struct DistributionResult
    {
        public TObject obj;
        public float amount;
    }

    public struct DistributionInput
    {
        public TObject obj;
        public float requestedAmount;
    }

    /// <summary>
    /// Runs distribution algorithm on input.
    /// </summary>
    /// <param name="input"></param>
    /// <param name="output"></param>
    /// <param name="available">The amount you want to distribute</param>
    /// <returns>The overflow</returns>
    public static float ReqDistribute(List<DistributionInput> input, List<DistributionResult> output, float available)
    {
        List<TObject> tmp = ListPool<TObject>.Get();
        List<TObject> objs = ListPool<TObject>.Get();
        Dictionary<TObject, float> distributed = DictionaryPool<TObject, float>.Get();
        Dictionary<TObject, float> requested = DictionaryPool<TObject, float>.Get();
        float availablePrev = -available;

        // Parse
        foreach (var inp in input)
        {
            if (Mathf.Approximately(inp.requestedAmount, 0))
                continue;

            distributed.Add(inp.obj, 0);
            requested.Add(inp.obj, inp.requestedAmount);
            objs.Add(inp.obj);
        }

        int panic = 0;
        while (available > 0 && !Mathf.Approximately(available, availablePrev) && requested.Count > 0)
        {
            availablePrev = available;
            float perInput = available / (float)requested.Count;

            foreach (var obj in objs)
            {
                float alreadyDistributed = distributed[obj];
                float stillRequested = requested[obj];

                float distributing = Mathf.Min(stillRequested, perInput);
                distributed[obj] += distributing;
                requested[obj] -= distributing;
                available -= distributing;

                if (Mathf.Approximately(requested[obj], 0))
                {
                    tmp.Add(obj);
                }
            }

            foreach (var obj in tmp)
            {
                objs.Remove(obj);
                requested.Remove(obj);
            }
            tmp.Clear();

            panic++;
            if (panic > 1000)
            {
                Debug.LogError("Distribution alrogithm triggered panic exit!");
                return available;
            }
        }

        // Write back
        foreach (var kvp in distributed)
        {
            output.Add(new DistributionResult()
            {
                amount = kvp.Value,
                obj = kvp.Key
            });
        }

        DictionaryPool<TObject, float>.Return(distributed);
        DictionaryPool<TObject, float>.Return(requested);
        ListPool<TObject>.Return(tmp);
        ListPool<TObject>.Return(objs);

        return available;
    }
}