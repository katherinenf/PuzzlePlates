using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GamePlayManager : MonoBehaviour
{
    public Map playerMap;

    public List<GameObject> playerCrustList;

    public List<GameObject> playerBoundaryList;

    public Map gameMap;

    public List<GameObject> gameCrustList;

    public List<GameObject> gameBoundaryList;

    public int level;

    public Crust crustRef;

    public List<Sprite> continentalSprites;

    public Sprite oceanicSprite;

    public Button continentalButton;

    public Crust movingCrust;

    public Boundary convergentRef;

    public List<GameObject> boundaries;

    public String primedBoundary;

    public String primedCrust;

    //public GameObject primedPrefab;


    // Start is called before the first frame update
    void Start()
    {
        List<GameObject> gCList = gameMap.GenerateGrid(level + 1, level + 1, 1, 3);
        gameCrustList = gCList;
        List<GameObject> pCList = playerMap.GenerateEmptyGrid(level + 1, level + 1, 1, -3);
        playerCrustList = pCList;
        List<GameObject>  gBList = gameMap.GenerateBoundaryGrid(level, 1, 3);
        playerBoundaryList = gBList;
        List<GameObject> pBList = playerMap.GenerateBoundaryGrid(level, 1, -3);
        playerBoundaryList = pBList;

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Debug.Log(hit.transform.gameObject.name);
            }
        }
        //locationMatch(playerCrustList, movingCrust);
    }

    public void DoneButtonClicked()
    {
        LevelUp();
        BoundariesToLandforms(boundaries);
        /*if(CheckCrustsMatch(gameCrustList, playerCrustList))
        {
            LevelUp();
        }*/
    }

    public void LevelUp()
    {
        ClearScene(playerCrustList);
        ClearScene(gameCrustList);
        //ClearScene(playerBoundaryList);
        ClearScene(gameBoundaryList);

        level = level + 1;
        List<GameObject> glist = gameMap.GenerateGrid(level + 1, level + 1, 1, 3);
        gameCrustList = glist;
        List<GameObject> plist = playerMap.GenerateEmptyGrid(level + 1, level + 1, 1, -3);
        playerCrustList = plist;
        List<GameObject> gBList = gameMap.GenerateBoundaryGrid(level, 1, 3, true);
        playerBoundaryList = gBList;
        List<GameObject> pBList = playerMap.GenerateBoundaryGrid(level, 1, -3, false);
        playerBoundaryList = pBList;
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


    /*public void NewContinentalCrust()
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
    }*/

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


   /* public void locationMatch(List<GameObject> map, Crust crust)
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
*/
    /*public void NewConvergent()
    {
        Boundary newBoundary = Instantiate(convergentRef);
        newBoundary.transform.position = new Vector3(-6, transform.position.y + 1, -1);
    }*/

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

    public void ConvergentButtonPressed()
    {
        primedBoundary = "convergent";
    }

    public void ContinentalButtonPressed()
    {
        primedCrust = "continental";
    }

    public void OceanicButtonPressed()
    {
        primedCrust = "oceanic";
    }





}
