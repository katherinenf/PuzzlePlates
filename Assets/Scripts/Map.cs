using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Map : MonoBehaviour
{


    public GameObject crustPrefab;

    public GameObject boundaryPrefab;

    //public List<Sprite> continentalSprites;
    
    public Sprite continentalSprite;

    public Sprite oceanicSprite;

    public GamePlayManager GM;

    void Start()
    {
        GM = GameObject.Find("GamePlayManager").GetComponent<GamePlayManager>();
    }

    public List<GameObject> GenerateCrustGrid(int rows, int cols, float tileSize, float offSet, bool isFull)
    {
            List<GameObject> crusts = new List<GameObject>();

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    GameObject tile;
                    float posX = col * tileSize;
                    float posY = row * -tileSize;
                    tile = Instantiate(crustPrefab, new Vector2(posX + offSet, posY + 3), new Quaternion(), transform);
                    if (isFull)
                     {
                         float type = UnityEngine.Random.Range(0, 2);
                         if (type == 0)
                         {
                             // Note: this line can be used to choose from multiple continental prefabs
                             // tile.transform.GetComponent<SpriteRenderer>().sprite = (continentalSprites[UnityEngine.Random.Range(1, continentalSprites.Count - 2)]);
                             tile.transform.GetComponent<SpriteRenderer>().sprite = continentalSprite;
                             tile.GetComponent<Crust>().crustType = "continental";
                             tile.transform.GetComponent<Collider2D>().enabled = true;
                             tile.GetComponent<Renderer>().sortingOrder = -1;
                             crusts.Add(tile);
                         }

                         if (type > 0)
                         {
                             tile.transform.GetComponent<SpriteRenderer>().sprite = oceanicSprite;
                             tile.GetComponent<Crust>().crustType = "oceanic";
                             tile.transform.GetComponent<Collider2D>().enabled = true;
                             tile.GetComponent<Renderer>().sortingOrder = -1;
                             crusts.Add(tile);
                         }
                     }
                }
            }
        return crusts;
    }

    public List<GameObject> GenerateEmptyBoundaryGrid(int lvl, float tileSize, float offSet)
    {
        List<GameObject> boundaries = new List<GameObject>();
        int boundaryRows = (lvl * 2) + 1;
        bool shortRow = true;
        float inc = 0;
        for(int row = 0; row < boundaryRows; row++)
        {
            if (shortRow)
            {
                for (int col = 0; col < lvl; col++)
                {
                    GameObject tile;
                    tile = Instantiate(boundaryPrefab, transform);
                    float posX = col * tileSize + 0.5f;
                    float rowPos;
                    rowPos = row;
                    float posY = rowPos * - tileSize + inc;
                    tile.transform.position = new Vector2(posX + offSet, posY + 3);
                    tile.GetComponent<Boundary>().gameMap = false;
                    shortRow = false;
                }
            }
            else if(!shortRow)
            {
                for (int col = 0; col < (lvl + 1); col++)
                {
                    GameObject tile;
                    tile = Instantiate(boundaryPrefab, transform);
                    float posX = col * tileSize;
                    float rowPos;
                    rowPos = row;
                    float posY = rowPos * - tileSize + 0.5f + inc;
                    tile.transform.position = new Vector2(posX + offSet, posY + 3);
                    tile.GetComponent<Boundary>().gameMap = false;
                    boundaries.Add(tile);
                    shortRow = true;
                }
                inc++;
            }
        }

        return boundaries;
        
    }
    public List<GameObject> GenerateBoundaryGrid(int lvl, float tileSize, float offSet)
    {
        List<GameObject> boundaries = new List<GameObject>();
        int boundaryRows = (lvl * 2) + 1;
        bool shortRow = true;
        float inc = 0;
        for (int row = 0; row < boundaryRows; row++)
        {
            if (shortRow)
            {
                for (int col = 0; col < lvl; col++)
                {
                    GameObject tile;
                    float posX = col * tileSize + 0.5f;
                    float rowPos;
                    rowPos = row;
                    float posY = rowPos * -tileSize + inc;
                    tile = Instantiate(boundaryPrefab, new Vector2(posX + offSet, posY + 3), new Quaternion(), transform);
                    GM.boundaries.Add(tile.GetComponent<Boundary>());
                    shortRow = false;
                }
            }
            else if (!shortRow)
            {
                for (int col = 0; col < (lvl + 1); col++)
                {
                    GameObject tile;
                    float posX = col * tileSize;
                    float rowPos;
                    rowPos = row;
                    float posY = rowPos * -tileSize + 0.5f + inc;
                    tile = Instantiate(boundaryPrefab, new Vector2(posX + offSet, posY + 3), new Quaternion(), transform);
                    GM.boundaries.Add(tile.GetComponent<Boundary>());
                    shortRow = true;
                }
                inc++;
            }
        }

        return boundaries;

    }
}
