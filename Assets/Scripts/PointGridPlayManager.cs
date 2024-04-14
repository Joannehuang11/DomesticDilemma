using UnityEngine;

public enum pointGridPlayingState
{
    None,
    Selecting,
    Result
}

public enum pointGridPlayResult
{
    None,
    P1Win,
    P2Win,
    P1P2Draw,
    P1P2GiveUp
}

public class PointGridPlayManager : MonoBehaviour
{
    //basic game data
    public pointGridPlayingState currentGridPlayingState = pointGridPlayingState.None;
    public pointGridPlayResult currentGridPlayResult = pointGridPlayResult.None;
    public int currentRound;
    public int maxRound = 20;

    // trigger updates to the UI
    public GameObject GamePlayUI;
    public GameObject PointGridUI;


    // Start is called before the first frame update
    void Start()
    {
        currentRound = 0;
        UpdateGridPlayingState(pointGridPlayingState.None);
        UpdateGridPlayResult(pointGridPlayResult.None);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartPointGridGame()
    {
        if (currentRound < maxRound)
        {
            currentRound++;
            Debug.Log("Start Round" + currentRound);
            UpdateGridPlayingState(pointGridPlayingState.Selecting);
            PointGridUI.GetComponent<UpdatePointGridImg>().startCountDown();
            //get game result

            // get player 1 selected
            // get player 2 selected
            // get game result
            // update grid play result
                // update point grid img
                // update text
            // update game playing state
            // update 
        }
        else
        {
            Debug.Log("Game Over");
        }
        
    }

    // Update the current state of the game    
    public void UpdateGridPlayingState (pointGridPlayingState newState)
    {
        currentGridPlayingState = newState;
        
        // Handle the new state
        switch (currentGridPlayingState)
        {
            case pointGridPlayingState.None:
                GamePlayUI.GetComponent<UpdateGridGameplayBg>().UpdateBg(false);
                Debug.Log("pointGridPlayingState is None.");
                break;
            case pointGridPlayingState.Selecting:
                GamePlayUI.GetComponent<UpdateGridGameplayBg>().UpdateBg(true);
                Debug.Log("pointGridPlayingState is Selecting.");
                break;
            case pointGridPlayingState.Result:
                GamePlayUI.GetComponent<UpdateGridGameplayBg>().UpdateBg(false);
                Debug.Log("pointGridPlayingState is Result.");
                break;
        }
    }

    public void UpdateGridPlayResult (pointGridPlayResult newResult)
    {
        currentGridPlayResult = newResult;

        // Handle the new result
        switch (currentGridPlayResult)
        {
            case pointGridPlayResult.None:
                Debug.Log("pointGridPlayResult is None.");
                break;
            case pointGridPlayResult.P1Win:
                Debug.Log("pointGridPlayResult is P1Win.");
                break;
            case pointGridPlayResult.P2Win:
                Debug.Log("pointGridPlayResult is P2Win.");
                break;
            case pointGridPlayResult.P1P2Draw:
                Debug.Log("pointGridPlayResult is P1P2Draw.");
                break;
            case pointGridPlayResult.P1P2GiveUp:
                Debug.Log("pointGridPlayResult is P1P2GiveUp.");
                break;
        }
    }
}