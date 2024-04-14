using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UIElements;


public enum playerCall
{
    None,
    Collab,
    NotCollab
}
public class PlayerManager : MonoBehaviour
{
    public int playerNo;

    public int playerCoin;
    public playerCall playerCall;
    
    // Start is called before the first frame update
    void Start()
    {
        playerCoin = 0;
        playerCall = playerCall.None;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayerCall(playerCall call)
    {
        playerCall = call;
    }

    public playerCall GetPlayerCall()
    {
        return playerCall;
    }

    public void SetPlayerCoin(int coin)
    {
        playerCoin = coin;
    }
    public int GetPlayerCoin()
    {
        return playerCoin;
    }


}
