using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Map : MonoBehaviour
{
    public Crust[] crusts;

    //public Boundary[] boundaries;

    // the done button to show when all ships are placed
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



}
