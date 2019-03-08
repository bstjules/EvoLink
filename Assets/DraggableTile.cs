using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableTile : MonoBehaviour
{

    private bool draggingTile = false;
    private GameObject draggedTile;
    private Vector2 touchOffset;

    Vector2 CurrentTouchPos
    {
        get
        {
            Vector2 inputPos;
            inputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return inputPos;
        }
    }
    private bool HasInput
    {
        get
        {
            // returns true if either the mouse button is down or at least one touch is felt on the screen
            return Input.GetMouseButton(0);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (HasInput)
        {
            PickorDrag();
        }
        else
        {
            if (draggingTile)
                DropTile();
        }
    }

    private void PickorDrag()
    {
        var inputPos = CurrentTouchPos;
        if (draggingTile)
        {
            draggedTile.transform.position = inputPos + touchOffset;
        }
        else
        {
            RaycastHit2D[] touches = Physics2D.RaycastAll(inputPos, inputPos, 0.5f);
            if (touches.Length > 0)
            {
                var hit = touches[0];
                if (hit.transform != null && hit.transform.tag == "Tile")
                {
                    draggingTile = true;
                    draggedTile = hit.transform.gameObject;
                    touchOffset = (Vector2)hit.transform.position - inputPos;
                    hit.transform.GetComponent<Tile>().PickUp();
                }
            }
        }
    }

    private void DropTile()
    {

        draggingTile = false;

        //transform.localScale = new Vector3(1, 1, 1);
        draggedTile.GetComponent<Tile>().Drop();
    }
}
