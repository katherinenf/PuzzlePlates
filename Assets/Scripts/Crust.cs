using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Crust : MonoBehaviour
{
    public string crustType;
    public List<Boundary> boundaries;
    public Sprite continentalSprite;
    public Sprite oceanicSprite;
    public GamePlayManager GM;
    private bool isMoveable;
    public new Collider2D collider;
    public bool isLocked = false;


    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("GamePlayManager").GetComponent<GamePlayManager>();
    }


    public void SetMoveable(bool val)
    {
        isMoveable = val;
        collider.enabled = isMoveable;
    }

    void OnMouseDown()
    {
        if (GM.GetComponent<GamePlayManager>().primedCrust == "continental")
        {
            this.GetComponent<SpriteRenderer>().sprite = continentalSprite;
            crustType = "continental";
        }
        else if (GM.GetComponent<GamePlayManager>().primedCrust == "oceanic")
        {
            this.GetComponent<SpriteRenderer>().sprite = oceanicSprite;
            crustType = "oceanic";
        }
    }
}