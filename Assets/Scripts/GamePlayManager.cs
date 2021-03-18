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

    public Button continentalButton;

    public Crust movingCrust;

    public Boundary convergentRef;

    public List<Boundary> boundaries;

    public String primedBoundary;

    public String primedCrust;

    public GameObject primedPrefab;

    public Texture2D cursorLook;

    public Texture2D[] cursors;


    // Start is called before the first frame update
    void Start()
    {
        CreateGrids(1);
    }

    private void Update()
    {
        Cursor.SetCursor(cursorLook, Vector2.zero, CursorMode.ForceSoftware);
    }

    public void DoneButtonClicked()
    {
        BoundariesToLandforms(boundaries);
        if(CheckCrustsMatch(gameCrustList, playerCrustList))
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        ClearScene(playerCrustList);
        ClearScene(gameCrustList);
        ClearScene(playerBoundaryList);
        ClearScene(gameBoundaryList);

        level = level + 1;

        CreateGrids(level);
    }
    
    public void ClearScene(List<GameObject> toClear)
    {
        foreach(GameObject go in toClear)
        {
            Destroy(go);
        }
    }

    public void CreateGrids(int level)
    {
        // Procedurally generate crust and boundary grids for game
        gameCrustList = gameMap.GenerateCrustGrid(level + 1, level + 1, 1, 3);
        gameBoundaryList = gameMap.GenerateBoundaryGrid(level, 1, 3);

        // generate empty crust and boundary lists 
        playerCrustList = playerMap.GenerateEmptyCrustGrid(level + 1, level + 1, 1, -3);
        playerBoundaryList = playerMap.GenerateEmptyBoundaryGrid(level, 1, -3);
    }

    public bool CheckCrustsMatch(List<GameObject> l1, List<GameObject> l2)
    {
        bool match = true;
            for (int i = 0; i < l1.Count; i++)
            {
                if (l1[i].GetComponent<SpriteRenderer>().sprite != l2[i].GetComponent<SpriteRenderer>().sprite)
                {
                match = false;
                }
            }
        return match;
        
    }

    public void AddBoundaryToList(Boundary boundary)
    {
        boundaries.Add(boundary);
    }

    public void BoundariesToLandforms(List<Boundary> boundaries)
    {
        foreach(Boundary b in boundaries)
        {
            b.BoundaryToLandform();
        }
    }

    public void ConvergentButtonPressed()
    {
        primedBoundary = "convergent";
        cursorLook = cursors[0];
    }

    public void ContinentalButtonPressed()
    {
        primedCrust = "continental";
        cursorLook = cursors[1];
    }

    public void OceanicButtonPressed()
    {
        primedCrust = "oceanic";
        cursorLook = cursors[2];

    }

}
