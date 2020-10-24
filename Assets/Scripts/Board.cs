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
       Debug.Log(state);
       if (state == States.None)
       {

       } else 
       {

       }
        
    }
}
