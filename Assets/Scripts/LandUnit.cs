using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LandUnit : MonoBehaviour
{
    public int landNo;
    public Image landImg;
    public bool isPlaced;

    //interface
    public GameObject player0Obj;
    PlayerManager player0Manager;
    public GameObject player1Obj;
    PlayerManager player1Manager;
    
    // Start is called before the first frame update
    void Start()
    {
        player0Manager = player0Obj.GetComponent<PlayerManager>();
        player1Manager = player1Obj.GetComponent<PlayerManager>();        
        
        landImg.sprite = null;
        isPlaced = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLandImg(Sprite img)
    {
        landImg.sprite = img;
        isPlaced = true;
    }
}
