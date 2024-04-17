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

    public List<Sprite> getImgs(int cardNo)
    {
        List<Sprite> thumbnailImgs = new List<Sprite>();

        if (cardNo == 15)
        {
            thumbnailImgs.Add(imgs[15]);
            thumbnailImgs.Add(imgs[16]);
            return thumbnailImgs;
        }
        else if (cardNo == 16)
        {
            thumbnailImgs.Add(imgs[17]);
            thumbnailImgs.Add(imgs[18]);
            return thumbnailImgs;
        }
        else if (cardNo == 17)
        {
            thumbnailImgs.Add(imgs[19]);
            thumbnailImgs.Add(imgs[20]);
            return thumbnailImgs;
        }
        else
        {
            thumbnailImgs.Add(imgs[cardNo]);
            return thumbnailImgs;
        }
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
