using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    public GameObject progressBarUI;
    public GameObject roundUnit;
    public GameObject breakUnit;

    public int roundsPerSection;
    public List<int> breakTimes;

    public int currentRound;
    private int maxRound;
    
    // Start is called before the first frame update
    void Start()
    {
        maxRound = roundsPerSection * (breakTimes.Count+1) + breakTimes.Count;
        currentRound = 0;

        initiateProgressBarUI();
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

    public void initiateProgressBarUI()
    {
        int breakRound = 0;
        
        for (int i = 1; i < maxRound + 1; i++)
        {
            GameObject unit;

            if (i % (roundsPerSection+1) == 0)
            {
                unit = Instantiate(breakUnit, progressBarUI.transform);
                unit.GetComponent<BreakProgressUnit>().setUnit(i - 1, breakTimes[breakRound], false);
                breakRound ++;
                // Debug.Log("Initiate break unit");
            }
            else
            {
                unit = Instantiate(roundUnit, progressBarUI.transform);
                unit.GetComponent<RoundProgressUnit>().setUnit(i - 1, false);
                // Debug.Log("Initiate round unit");
            }
        }
    }
    
    public void updateProgressBarUI(int currentRound)
    {

    }
}
