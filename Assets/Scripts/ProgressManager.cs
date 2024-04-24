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
    private GameObject sceneManagerObg;
    SceneManager SceneManager;
    
    
    //progress bar
    public GameObject progressBarUI;
    public GameObject roundUnit;
    public GameObject breakUnit;

    public int roundsPerSection;
    public List<int> breakTimes;
    // public List<GameObject> roundUnits;

    public int currentRound;
    private int maxRound;

    //test
    public float inputBlockTimeTest = 10;
    public bool isInputBlock = false;
    
    // Start is called before the first frame update
    void Start()
    {
        player0Manager = player0Obj.GetComponent<PlayerManager>();
        player1Manager = player1Obj.GetComponent<PlayerManager>();
        
        pointGridPlayManager = pointGridPlayManagerObj.GetComponent<PointGridPlayManager>();
        actionCardsPlayManager = actionCardsPlayManagerObj.GetComponent<ActionCardsPlayManager>();
        territoryPlayManager = territoryPlayManagerObj.GetComponent<TerritoryPlayManager>();

        sceneManagerObg = GameObject.Find("SceneManager");
        SceneManager = sceneManagerObg.GetComponent<SceneManager>();
        
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
        // Debug.Log("Set Current Round: " + round);
        
        currentRound = round;
        if (currentRound == 0)
        {
            resetProgressBarUI();
        }
        else 
        {
            updateProgressBarUI(currentRound);
        }
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

    public void resetProgressBarUI()
    {
        for (int i = 0; i < maxRound; i++)
        {
            if (i % (roundsPerSection + 1) == 5)
            {
                progressBarUI.transform.GetChild(i).GetComponent<BreakProgressUnit>().updateImg(false);
            }
            else
            {
                progressBarUI.transform.GetChild(i).GetComponent<RoundProgressUnit>().updateImg(false);
            }
        }
    }
    
    public void startGame()
    {
        if (!isInputBlock)
        {
            if (currentRound < maxRound)
            {
                    currentRound++;
                    SetCurrentRound(currentRound);                          
                
                if (currentRound % (roundsPerSection + 1) == 0)
                {
                    //start break
                    // Debug.Log("Start Break");
                    // Debug.Log("Break Time: " + breakTimes[(currentRound / (roundsPerSection + 1)) - 1]);

                    //reset
                    pointGridPlayManager.SetGridPlayingState(pointGridPlayingState.None);
                    actionCardsPlayManager.setActionCardsPlayingState(actionCardsPlayingState.None, false);
                    territoryPlayManager.setTerritoryPlayingState(territoryPlayingState.None);
                    pointGridPlayManager.SetGridPlayResult(pointGridPlayResult.None);

                    //break function
                    int breakRound = (currentRound / (roundsPerSection + 1)) - 1;
                    pointGridPlayManager.BreakPointGridGame(breakTimes[breakRound]);
                    // inputBlock(inputBlockTimeTest);
                    inputBlock(breakTimes[breakRound]);
                }
                else
                {
                    //start games
                    // Debug.Log("Start Round");

                    //update games
                    pointGridPlayManager.SetGridPlayingState(pointGridPlayingState.Selecting);
                    actionCardsPlayManager.setActionCardsPlayingState(actionCardsPlayingState.None, false);
                    territoryPlayManager.setTerritoryPlayingState(territoryPlayingState.None);

                    //start function
                    pointGridPlayManager.StartPointGridGame();
                }
            }
            else
            {
                endGame();
            } 
        }
    }

    public void inputBlock(float time)
    {
        StartCoroutine(inputBlockCoroutine(time));
    }

    IEnumerator inputBlockCoroutine(float time)
    {
        isInputBlock = true;
        yield return new WaitForSeconds(time);
        isInputBlock = false;
    }

    public bool getIsInputBlock()
    {
        return isInputBlock;
    }

    public void setIsInputBlock(bool value)
    {
        isInputBlock = value;
        // Debug.Log("Set isInputBlock to " + value);
    }

    public void endGame()
    {
        SceneManager.endGameScene();
        // Debug.Log("Game Over");
    }

    public void restartGame()
    {
        player0Manager.SetPlayerCoin(0);
        player1Manager.SetPlayerCoin(0);

        currentRound = 0;
        SetCurrentRound(currentRound);
        setIsInputBlock(false);
        resetProgressBarUI();
        SceneManager.restartGameScene();

        // Debug.Log("Restart Game");
        pointGridPlayManager.ResetPointGridGame();
        actionCardsPlayManager.ResetActionCardsPlay();
        territoryPlayManager.ResetTerritory();

        startGame();
    }

    public void backHome()
    {
        player0Manager.SetPlayerCoin(0);
        player1Manager.SetPlayerCoin(0);
        
        currentRound = 0;
        SetCurrentRound(currentRound);
        setIsInputBlock(true);
        resetProgressBarUI();
        SceneManager.backHomeScene();

        // Debug.Log("Back Home");
        pointGridPlayManager.ResetPointGridGame();
        actionCardsPlayManager.ResetActionCardsPlay();
        territoryPlayManager.ResetTerritory();
    }
}
