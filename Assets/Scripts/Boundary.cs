using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    public GameObject boundary;
    public GameObject contContLF;
    public string ccLandform;
    public GameObject contOceanicLF;
    public string coLandform;
    public GameObject oceanicOceanicLF;
    public string ooLandform;
    public new Collider2D collider;
    public bool rotated = false;
    //public int boundarySize;
    //public int anchorOffset;
    GamePlayManager GM;
    private Collider2D[] crusts;
    private Vector3 startPos;
    private bool startRotation;
    private bool isMoveable = true;
    public ContactFilter2D contactFilter;
    public string landform;
    public GameObject convergentPrefab;


    // Start is called before the first frame update
    void Awake()
    {
        startPos = transform.position;
        startRotation = rotated;
        crusts = new Collider2D[2];
        int crustNum = collider.OverlapCollider(contactFilter, crusts);
        GM = GameObject.Find("GamePlayManager").GetComponent<GamePlayManager>();
        

    }

    private Vector3 screenPoint; private Vector3 offset;

    void OnMouseDown()
    {
        GameObject newboundary;
        if (GM.GetComponent<GamePlayManager>().primedBoundary == "convergent")
        {
            newboundary = Instantiate(convergentPrefab);
            newboundary.transform.position = this.transform.position;
            //this.GetComponent<GameObject>().transform
            GM.GetComponent<GamePlayManager>().boundaries.Add(newboundary);
            int crustNum = collider.OverlapCollider(contactFilter, crusts);
            Debug.Log(crustNum);
        }
        else if (GM.GetComponent<GamePlayManager>().primedBoundary == "divergent")
        {

        }
    }

    public void BoundaryToLandform()
    {
        if (crusts[0].GetComponent<Crust>().crustType == "continental"
            && crusts[1].GetComponent<Crust>().crustType == "continental")
        {
            contContLF.SetActive(true);
            //this.gameObject.GetComponent<SpriteRenderer>().sprite = contContLF;
            landform = ccLandform;
        }
        if (crusts[0].GetComponent<Crust>().crustType == "oceanic"
            && crusts[1].GetComponent<Crust>().crustType == "oceanic")
        {
            oceanicOceanicLF.SetActive(true);
            //this.gameObject.GetComponent<SpriteRenderer>().sprite = oceanicOceanicLF;
            landform = ooLandform;
        }
        if ((crusts[0].GetComponent<Crust>().crustType == "oceanic"
           && crusts[1].GetComponent<Crust>().crustType == "continental") ||
           crusts[1].GetComponent<Crust>().crustType == "oceanic"
           && crusts[0].GetComponent<Crust>().crustType == "continental")
        {
            contOceanicLF.SetActive(true);
            //this.gameObject.GetComponent<SpriteRenderer>().sprite = contOceanicLF;
            landform = coLandform;
        }
        boundary.SetActive(false);
    }
}
