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
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        crusts[0] = null;
        crusts[1] = null;
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        var currentPos = curPosition;
        if (rotated == false)
        {
            transform.position = new Vector3(Mathf.Round(currentPos.x) + 0.5f,
                                         Mathf.Round(currentPos.y),
                                         Mathf.Round(currentPos.z));
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            this.gameObject.transform.Rotate(0, 0, 90, Space.Self);
            transform.position = new Vector3(Mathf.Round(currentPos.x),
                                         Mathf.Round(currentPos.y) + 0.5F,
                                         Mathf.Round(currentPos.z));

        }


    }

    private void Poof()
    {
        Destroy(this.gameObject);
    }

    //if the boundary is in a valid position(over two crusts), sets the boundary down
    private void OnMouseUp()
    {
        int crustNum = collider.OverlapCollider(contactFilter, crusts);
        if (crusts[0] == null || crusts[1] == null)
        {
            Poof();
        }
        else
            //Debug.Log(this.gameObject);
            GM.AddBoundaryToList(this.gameObject);
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
