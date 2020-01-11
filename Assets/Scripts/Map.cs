using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Map : MonoBehaviour
{
    //public Crust[] crusts;

    //public Boundary[] boundaries;

    public Button uiDoneButton;

    public GameObject crustPrefab;

    public GameObject boundaryPrefab;

    public int level;

    public List<Sprite> continentalSprites;

    public Sprite oceanicSprite;

    

    public List<GameObject> GenerateGrid(int rows, int cols, float tileSize, float offSet)
    {
        List<GameObject> crusts = new List<GameObject>();

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                GameObject tile;
                float type = UnityEngine.Random.Range(0, 2);
                if (type == 0)
                {
                    tile = Instantiate(crustPrefab, transform);
                    tile.transform.GetComponent<SpriteRenderer>().sprite = (continentalSprites[UnityEngine.Random.Range(1, continentalSprites.Count - 2)]);
                    tile.GetComponent<Crust>().crustType = "continental";
                    float posX = col * tileSize;
                    float posY = row * -tileSize;
                    tile.transform.position = new Vector2(posX + offSet, posY + 3);
                    tile.transform.GetComponent<Collider2D>().enabled = false;
                    crusts.Add(tile);
                }

                if (type > 0)
                {
                    tile = Instantiate(crustPrefab, transform);
                    tile.transform.GetComponent<SpriteRenderer>().sprite = oceanicSprite;
                    tile.GetComponent<Crust>().crustType = "oceanic";
                    float posX = col * tileSize;
                    float posY = row * -tileSize;
                    tile.transform.position = new Vector2(posX + offSet, posY + 3);
                    tile.transform.GetComponent<Collider2D>().enabled = false;
                    crusts.Add(tile);
                }

            }
        }
        return crusts;
    }

    public List<GameObject> GenerateEmptyGrid(int rows, int cols, float tileSize, float offSet)
    {
        List<GameObject> crusts = new List<GameObject>();

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                GameObject tile;
                tile = Instantiate(crustPrefab, transform);
                float posX = col * tileSize;
                float posY = row * -tileSize;
                tile.transform.position = new Vector2(posX + offSet, posY + 3);
                tile.GetComponent<Crust>().isLocked = true;
                crusts.Add(tile);
            }
        }
        return crusts;
    }

    public List<GameObject> GenerateBoundaryGrid(int lvl, float tileSize, float offSet)
    {
        List<GameObject> boundaries = new List<GameObject>();
        int boundaryRows = (lvl * 2) + 1;
        Debug.Log(boundaryRows);
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
                    Debug.Log("row " + row);
                    Debug.Log("posY " + posY);
                    tile.transform.position = new Vector2(posX + offSet, posY + 3);
                    boundaries.Add(tile);
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
                    Debug.Log("row " + row);
                    Debug.Log("posY " + posY);
                    tile.transform.position = new Vector2(posX + offSet, posY + 3);
                    boundaries.Add(tile);
                    shortRow = true;
                }
                inc = inc + 1;
            }
        }
        return boundaries;
    }



}
