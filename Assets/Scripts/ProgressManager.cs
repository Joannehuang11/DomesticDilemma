using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    public GameObject progressBarUI;
    public GameObject roundUnit;
    public GameObject breakUnit;

    public int roundsPerSection;
    public List<int> breakTimesInGame;

    public int currentRound;
    private int maxRound;
    
    // Start is called before the first frame update
    void Start()
    {
        maxRound = roundsPerSection * (breakTimesInGame.Count-1);
        currentRound = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCurrentRound(int round)
    {
        currentRound = round;
        // Debug.Log("Start Round from progressManager: " + currentRound);
    }
    
    public int GetCurrentRound()
    {
        return currentRound;
    }

    public int GetMaxRound()
    {
        return maxRound;
    }
}
