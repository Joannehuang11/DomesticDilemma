using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    //interfaces
    public GameObject player0Obj;
    PlayerManager player0Manager;
    public GameObject player1Obj;
    PlayerManager player1Manager;
    public GameObject pointGridPlayManagerObj;
    PointGridPlayManager pointGridPlayManager;    
    public GameObject actionCardsPlayManagerObj;
    ActionCardsPlayManager actionCardsPlayManager;
    public GameObject territoryPlayManagerObj;
    TerritoryPlayManager territoryPlayManager;
    
    
    //progress bar
    public GameObject progressBarUI;
    public GameObject roundUnit;
    public GameObject breakUnit;

    public int roundsPerSection;
    public List<int> breakTimes;
    public List<GameObject> roundUnits;

    public int currentRound;
    private int maxRound;
    
    // Start is called before the first frame update
    void Start()
    {
        player0Manager = player0Obj.GetComponent<PlayerManager>();
        player1Manager = player1Obj.GetComponent<PlayerManager>();
        
        pointGridPlayManager = pointGridPlayManagerObj.GetComponent<PointGridPlayManager>();
        actionCardsPlayManager = actionCardsPlayManagerObj.GetComponent<ActionCardsPlayManager>();
        territoryPlayManager = territoryPlayManagerObj.GetComponent<TerritoryPlayManager>();
        
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
        Debug.Log("Start Round from progressManager: " + currentRound);

        updateProgressBarUI(currentRound);
    }
    
    public int GetCurrentRound()
    {
        return currentRound;
    }

    public int GetMaxRound()
    {
        return maxRound;
    }

    public int GetRoundsPerSection()
    {
        return roundsPerSection;
    }

    public void initiateProgressBarUI()
    {
        int breakRound = 0;
        
        for (int i = 1; i < maxRound + 1; i++)
        {
            GameObject unit;

            if (i % (roundsPerSection + 1) == 0)
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
        if (currentRound % (roundsPerSection + 1) == 0)
        {
            progressBarUI.transform.GetChild(currentRound - 1).GetComponent<BreakProgressUnit>().updateImg(true);
        }
        else
        {
            progressBarUI.transform.GetChild(currentRound - 1).GetComponent<RoundProgressUnit>().updateImg(true);
        }
    }

    public void startGame()
    {
        if (currentRound < maxRound)
        {
                currentRound++;
                SetCurrentRound(currentRound);            
            
            if (currentRound % (roundsPerSection + 1) == 0)
            {
                //start break
                Debug.Log("Start Break");

                pointGridPlayManager.BreakPointGridGame();

                //reset
                pointGridPlayManager.SetGridPlayingState(pointGridPlayingState.None);
                actionCardsPlayManager.setActionCardsPlayingState(actionCardsPlayingState.None);
                territoryPlayManager.SetTerritoryPlayingState(territoryPlayingState.None);
            }
            else
            {
                //start games
                Debug.Log("Start Round");

                pointGridPlayManager.StartPointGridGame();

                //update games
                pointGridPlayManager.SetGridPlayingState(pointGridPlayingState.Selecting);
                actionCardsPlayManager.setActionCardsPlayingState(actionCardsPlayingState.None);
                territoryPlayManager.SetTerritoryPlayingState(territoryPlayingState.None);

            }
        }
        else
        {
            Debug.Log("Game Over");
        } 
    }

    public void startPlay()
    {

    }

    public void startBreak()
    {

    }
}
