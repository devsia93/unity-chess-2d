using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{

    enum States
    {
        None,
        Drag
    }

    States state;
    GameObject item;
    Vector2 offset;
    Vector2 fromPosition;
    Vector2 toPosition;

    public delegate void deDropObject(Vector2 from, Vector2 to);
    public delegate void dePickObject(Vector2 from);

    deDropObject DropObject;
    dePickObject PickObject;

    public DragAndDrop(dePickObject PickObject, deDropObject DropObject)
    {
        this.PickObject = PickObject;
        this.DropObject = DropObject;
        this.state = States.None;
        this.item = null;
    }

    public void ActionHandler()
    {
        if (state == States.None)
        {
            if (IsMouseButtonPressed() && GetClickedObject().name != ".")
            {
                PickUp();
                Debug.Log(item.name);
            }
        }
        else
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

        bool IsMouseButtonPressed()
        {
            return Input.GetMouseButton(0);
        }

        void Drop()
        {
            toPosition = item.transform.position;
            DropObject(fromPosition, toPosition);
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
            fromPosition = clickedItem.position;
            offset = fromPosition - clickPosition;
            PickObject(fromPosition);
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

        GameObject GetClickedObject()
        {
            Vector2 clickPosition = GetClickPosition();
            Transform clickedItem = GetItemAt(clickPosition);
            return clickedItem.gameObject;
        }
    }
}

