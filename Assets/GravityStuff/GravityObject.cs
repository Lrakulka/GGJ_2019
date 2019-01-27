using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityObject : MonoBehaviour
{
    public static List<Rigidbody2D> list = new List<Rigidbody2D>();

    private void Awake()
    {
        list.Add(this.GetComponent<Rigidbody2D>());
    }

    private void OnDestroy()
    {
        list.Remove(this.GetComponent<Rigidbody2D>());
    }

    public static float GetBalanceShift()
    {
        float result = 0f;

        foreach (var e in list)
            result += e.mass * e.transform.position.x;

        return result;
    }
}
