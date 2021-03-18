using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    public Sprite convergentSprite;
    public string boundaryType;
    public GameObject boundary;
    public Sprite[] landforms;
    public Sprite[] convergentLandforms;
    public Sprite[] divergentLandforms;
    public Sprite transformLandform;
    public new Collider2D collider;
    public bool rotated = false;
    //public int boundarySize;
    //public int anchorOffset;
    GamePlayManager GM;
    public Collider2D[] crusts;
    private Vector3 startPos;
    private bool startRotation;
    private bool isMoveable = true;
    public ContactFilter2D contactFilter;
    public string landform;
    public GameObject convergentPrefab;
    public bool gameMap = true;


    void Start()
    {
        startPos = transform.position;
        startRotation = rotated;
        GM = GameObject.Find("GamePlayManager").GetComponent<GamePlayManager>();
        crusts = new Collider2D[2];
        collider.OverlapCollider(contactFilter, crusts);
        if (gameMap == true)
        {
            ChooseBoundaryType();
            BoundaryToLandform();
        }

    }

    public Collider2D[] GetCrusts()
    {
        collider.OverlapCollider(contactFilter, crusts);
        return crusts;
    }

        void OnMouseDown()
        {
            GM.GetComponent<GamePlayManager>().primedCrust = null;

            if (GM.GetComponent<GamePlayManager>().primedBoundary == "convergent")
               {
                    this.GetComponent<SpriteRenderer>().sprite = convergentSprite;
                    this.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                    boundaryType = "convergent";
                    GM.GetComponent<GamePlayManager>().boundaries.Add(this);
                    GetCrusts();
                }

        }

    public void BoundaryToLandform()
    {
        if (boundaryType == "transform")
        {
            this.GetComponent<SpriteRenderer>().sprite = transformLandform;
        }
        else
        {
            SetLandformsList();
            if (crusts[0].GetComponent<Crust>().crustType == "continental"
                && crusts[1].GetComponent<Crust>().crustType == "continental")
            {
                this.GetComponent<SpriteRenderer>().sprite = landforms[0];
            }
            else if (crusts[0].GetComponent<Crust>().crustType == "oceanic"
                && crusts[1].GetComponent<Crust>().crustType == "oceanic")
            {
                this.GetComponent<SpriteRenderer>().sprite = landforms[1];
            }
            else if (boundaryType != "divergent" &&
                (crusts[0].GetComponent<Crust>().crustType == "oceanic"
               && crusts[1].GetComponent<Crust>().crustType == "continental") ||
               crusts[1].GetComponent<Crust>().crustType == "oceanic"
               && crusts[0].GetComponent<Crust>().crustType == "continental")
            {
                this.GetComponent<SpriteRenderer>().sprite = landforms[2];
            }
        }
        this.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    void SetLandformsList()
    {
        if (boundaryType == "convergent")
        {
            landforms = convergentLandforms;
        }
        else if (boundaryType == "divergent")
        {
            landforms = divergentLandforms;
        }
    }

    void ChooseBoundaryType()
    {
        float type = UnityEngine.Random.Range(0, 3);

        if ((type == 0 || type == 1) && (crusts[0].GetComponent<Crust>().crustType == crusts[1].GetComponent<Crust>().crustType))
        {
            boundaryType = "divergent";
        }
        else if (type == 2)
        {
            boundaryType = "transform";

        }
        else
        {
            boundaryType = "convergent";
        }
    }

}
