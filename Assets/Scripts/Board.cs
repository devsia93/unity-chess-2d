using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    DragAndDrop dd;
    // Start is called before the first frame update
    void Start()
    {
        dd = new DragAndDrop();
    }

    // Update is called once per frame
    void Update()
    {
        dd.ActionHandler();
    }
}

class DragAndDrop 
{

    enum States
    {
        None,
        Drag
    }

    States state;
    GameObject item;

    public DragAndDrop()
    {
        this.state = States.None;
        this.item = null;
    }

    public void ActionHandler()
    {
       if (state == States.None)
       {
            if (IsMouseButtonPressed())
            {
                PickUp();
            }
       } else 
       {
            if (IsMouseButtonPressed())
            {
                Drag();
            }
            else
            {
                Drop();
            }
        }
        
    }

    bool IsMouseButtonPressed()
    {
        return Input.GetMouseButton(0);
    }

    void Drop()
    {
        item = null;
        state = States.None;
    }

    void Drag()
    {
        item.transform.position = GetClickPosition();
    }

    void PickUp()
    {
        Vector2 clickPosition = GetClickPosition();
        Transform clickedItem = GetItemAt(clickPosition);
        if (clickedItem == null)
            return;
        state = States.Drag;
        item = clickedItem.gameObject;
        Debug.Log(item.name);
    }

    Vector2 GetClickPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    Transform GetItemAt(Vector2 position)
    {
       RaycastHit2D[] figures = Physics2D.RaycastAll(position, position, 0.5f);
        if (figures.Length == 0)
            return null;
        return figures[0].transform;
    }
}
