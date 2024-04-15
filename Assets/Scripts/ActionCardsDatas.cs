using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCardsDatas : MonoBehaviour
{
    public List<string> names;
    public List<Sprite> imgs;
    public List<int> coinCosts;
    public List<int> gridCounts;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string getName(int cardNo)
    {
        return names[cardNo];
    }

    public Sprite getImg(int cardNo)
    {
        return imgs[cardNo];
    }

    public int getCoinCost(int cardNo)
    {
        return coinCosts[cardNo];
    }

    public int getGridCount(int cardNo)
    {
        return gridCounts[cardNo];
    }  
}
