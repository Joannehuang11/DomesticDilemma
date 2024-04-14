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
    public pointGridPlayingState currentGridPlayingState = pointGridPlayingState.None;
    public pointGridPlayResult currentGridPlayResult = pointGridPlayResult.None;


    // imgs and obj for GamePlayBg
    public GameObject GamePlayUI;


    // Start is called before the first frame update
    void Start()
    {
        UpdateGridPlayingState(pointGridPlayingState.None);
        UpdateGridPlayResult(pointGridPlayResult.None);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartPointGridGame()
    {
        UpdateGridPlayingState(pointGridPlayingState.Selecting);
    }

    // Update the current state of the game    
    public void UpdateGridPlayingState (pointGridPlayingState newState)
    {
        currentGridPlayingState = newState;
        
        // Handle the new state
        switch (currentGridPlayingState)
        {
            case pointGridPlayingState.None:
                Debug.Log("pointGridPlayingState is None.");
                GamePlayUI.GetComponent<UpdateGridGameplayBg>().UpdateBg(false);
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