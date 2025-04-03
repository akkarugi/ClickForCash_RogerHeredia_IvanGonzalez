using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public float points { get; private set; } = 0;
    public float maxpoints { get; private set; } = 10000;
    //[SerializeField] private float add_maxpoints = 10000;
    private List<Multiplier> multipliers = new List<Multiplier>();
    

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    public float GetMultiplicator()
    {
        float multiplicator = 0;

        foreach (Multiplier m in multipliers)
        {
            multiplicator += m.multi;
        }

        if (multiplicator > 0)
            return multiplicator;
        else 
            return 1;
    }
    public void AddPoints(float add_points)
    {
        points += add_points;
    }
}
