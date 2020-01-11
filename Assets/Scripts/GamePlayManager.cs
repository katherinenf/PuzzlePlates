using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GamePlayManager : MonoBehaviour
{
    public Map playerMap;

    public List<GameObject> playerList;

    public Map gameMap;

    public List<GameObject> gameList;

    public int level;

    public Crust crustRef;

    public List<Sprite> continentalSprites;

    public Sprite oceanicSprite;

    public Button continentalButton;

    public Crust movingCrust;

    public Boundary convergentRef;

    public List<GameObject> boundaries;


    // Start is called before the first frame update
    void Start()
    {
        List<GameObject> glist = gameMap.GenerateGrid(level + 1, level + 1, 1, 3);
        gameList = glist;
        List<GameObject> plist = playerMap.GenerateEmptyGrid(level + 1, level + 1, 1, -3);
        playerList = plist;

    }

    private void Update()
    {
        locationMatch(playerList, movingCrust);
    }

    public void DoneButtonClicked()
    {
        BoundariesToLandforms(boundaries);
        if(CheckCrustsMatch(gameList, playerList))
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        ClearScene(playerList);
        ClearScene(gameList);

        level = level + 1;
        List<GameObject> glist = gameMap.GenerateGrid(level + 1, level + 1, 1, 3);
        gameList = glist;
        List<GameObject> plist = playerMap.GenerateEmptyGrid(level + 1, level + 1, 1, -3);
        playerList = plist;
    }
    
    public void ClearScene(List<GameObject> map)
    {
        foreach(GameObject go in map)
        {
            Destroy(go);
        }
        foreach(GameObject go in boundaries)
        {
            Destroy(go);
        }
    }


    public void NewContinentalCrust()
    {
        Crust newCrust = Instantiate(crustRef);
        newCrust.transform.GetComponent<SpriteRenderer>().sprite = (continentalSprites[0]);
        //newCrust.transform.GetComponent<SpriteRenderer>().sprite = (continentalSprites[UnityEngine.Random.Range(1, continentalSprites.Count - 2)]);
        newCrust.transform.position = new Vector3(-6, transform.position.y + 1, 1);
        newCrust.SetMoveable(true);
        newCrust.crustType = "continental";
        movingCrust = newCrust;
    }

    public void NewOceanicCrust()
    {
        Crust newCrust = Instantiate(crustRef);
        newCrust.transform.GetComponent<SpriteRenderer>().sprite = oceanicSprite;
        newCrust.transform.position = new Vector3(-6, transform.position.y + 1, 1);
        newCrust.SetMoveable(true);
        newCrust.crustType = "oceanic";
        movingCrust = newCrust;
    }

    public bool CheckCrustsMatch(List<GameObject> l1, List<GameObject> l2)
    {
            for (int i = 0; i < l1.Count; i++)
            {
                if (l1[i].GetComponent<SpriteRenderer>().sprite != l2[i].GetComponent<SpriteRenderer>().sprite)
                {
                    return false;
                }
            }
        return true;
    }


    public void locationMatch(List<GameObject> map, Crust crust)
    {
        if (crust != null)
        {
            foreach (GameObject i in map)
            {
                if (i.transform.GetComponent<SpriteRenderer>().sprite != null)
                {
                    if (i.transform.position.x == crust.transform.position.x &&
                        i.transform.position.y == crust.transform.position.y)
                    {
                        i.transform.GetComponent<SpriteRenderer>().sprite = crust.transform.GetComponent<SpriteRenderer>().sprite;
                        i.transform.GetComponent<Crust>().crustType = crust.transform.GetComponent<Crust>().crustType;
                    }
                }
            }
        }       
    }

    public void NewConvergent()
    {
        Boundary newBoundary = Instantiate(convergentRef);
        newBoundary.transform.position = new Vector3(-6, transform.position.y + 1, -1);
    }

    public void AddBoundaryToList(GameObject boundary)
    {
        boundaries.Add(boundary);
    }

    public void BoundariesToLandforms(List<GameObject> boundaries)
    {
        foreach(GameObject go in boundaries)
        {
            go.GetComponent<Boundary>().BoundaryToLandform();
        }
    }


}
