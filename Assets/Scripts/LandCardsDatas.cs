using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandCardsDatas : MonoBehaviour
{   
    public List<GameObject> landCards;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < landCards.Count; i++)
        {
            landCards[i].GetComponent<LandUnit>().setStartLandCard(i, -1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetLandCard(int landNo)
    {
        return landCards[landNo];
    }
}
