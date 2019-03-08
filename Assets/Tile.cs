using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    private Vector2 startingPos;
    public List<Transform> touchingNodes;
    private Transform myParent;

    private void Awake()
    {
        startingPos = transform.position;
        touchingNodes = new List<Transform>();
        myParent = transform.parent;
    }

    public void PickUp()
    {
        float usablescale = 0.3f / transform.parent.localScale.x;
        transform.localScale = new Vector3(usablescale, usablescale, 1f);
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
    }

    public void Drop()
    {

        float usablescale = 0.2f / transform.parent.localScale.x;
        transform.localScale = new Vector3(usablescale, usablescale, 1);
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 0;

        Vector2 newPos;
        if (touchingNodes.Count == 0)
        {
            transform.position = startingPos;
            transform.SetParent(myParent, true);
            return;
        }

        var currentNode = touchingNodes[0];
        if (touchingNodes.Count == 1)
        {
            newPos = currentNode.position;
        }
        else
        {
            var distance = Vector2.Distance(transform.position, touchingNodes[0].position);

            foreach (Transform Node in touchingNodes)
            {
                if (Vector2.Distance(transform.position, Node.position) < distance)
                {
                    currentNode = Node;
                    distance = Vector2.Distance(transform.position, Node.position);
                }
            }
            newPos = currentNode.position;
        }
        if (currentNode.childCount != 0)
        {
            transform.position = startingPos;
            transform.SetParent(myParent,true);
            return;
        }
        else
        {
            transform.SetParent(currentNode, true);
            StartCoroutine(SlotIntoPlace(transform.position, newPos));
        }


    }


    void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.tag != "Node") return;
        if (!touchingNodes.Contains(other.transform) && ((other.tag == "Node") || (other.tag == "NodeStrong")))
        {
            touchingNodes.Add(other.transform);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
     //   if (other.tag != "Node") return;
        if (touchingNodes.Contains(other.transform) && ((other.tag == "Node")||(other.tag == "NodeStrong")))
        {
            touchingNodes.Remove(other.transform);
        }
    }

    IEnumerator SlotIntoPlace(Vector2 startingPos, Vector2 endingPos)
    {
        float duration = 0.1f;
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            transform.position = Vector2.Lerp(startingPos, endingPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = endingPos;
    }

}
