using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Crust : MonoBehaviour
{
    // Periodic table properties 
    public string crustType;
    public List<Boundary> boundaries;

    public Map GM;
    private Vector3 startPos;
    private bool isMoveable;
    private bool isTrigger;
    public new Collider2D collider;
    public bool isLocked = false;
    private Vector3 screenPoint;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {

    }


    public void SetMoveable(bool val)
    {
        isMoveable = val;
        collider.enabled = isMoveable;
    }

    void OnMouseDown()
    {
        if (isLocked == false)
        {
            screenPoint = Camera.main.WorldToScreenPoint(transform.position);
            offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                                                                                    Input.mousePosition.y,
                                                                                    screenPoint.z));
        }
    }

    void OnMouseDrag()
    {
        if (isLocked == false)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            var currentPos = curPosition;
            transform.position = new Vector3(Mathf.Round(currentPos.x),
                                            Mathf.Round(currentPos.y),
                                            Mathf.Round(currentPos.z));
        }
    }

    private void OnMouseUp()
    {
        if (isLocked == false)
        {
            Destroy(this.gameObject);
        }
    }





}
