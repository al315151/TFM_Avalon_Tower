using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Mouse_Game_Detector : MonoBehaviour
{
    Construction_Grid_Generator grid_Script;
    Camera mainCamera;
    EventSystem currentEventSystem;
    

    public bool Allow_position_mouse;


    // Start is called before the first frame update
    void Start()
    {
        grid_Script = GetComponent<Construction_Grid_Generator>();
        currentEventSystem = EventSystem.current;
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
       


    }
}
