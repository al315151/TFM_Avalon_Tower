using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Construction_Grid_Generator : MonoBehaviour
{
    //GRID RELATED VARIABLES
    public GameObject grid_Node;

    GameObject[] nodeVector;
    float row_number  = 10f;
    float column_number = 10f;
    float gap_between_elements = 5f;
    Vector3 startPosition;

    //MOUSE DETECTION RELATED VARIABLES
    public bool Allow_position_mouse = true;
    Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        CreateNodeVector();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Allow_position_mouse && Input.GetMouseButtonDown(0))
        {
            Debug.Log("Click Izquierdo Apretado");
            DetectNodeFromMousePosition();
        }
    }

    void CreateNodeVector()
    {
       startPosition = new Vector3(transform.position.x - gap_between_elements * row_number / 2, 0,
                                       transform.position.z - gap_between_elements * row_number / 2);

        for (int i = 0; i < row_number; i++)
        {
            for (int j = 0; j < column_number; j++)
            {
                GameObject newObject = Instantiate(grid_Node, new Vector3(startPosition.x + gap_between_elements * i, 0,
                                                                      startPosition.z + gap_between_elements * j), Quaternion.identity, transform);

            }
        }
    }

    void DetectNodeFromMousePosition()
    {
        print(mainCamera.ScreenToWorldPoint(Input.mousePosition));
        Vector3 mouse_WC = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        if (mouse_WC.x > startPosition.x && mouse_WC.z > startPosition.z &&
            mouse_WC.x < startPosition.x * (gap_between_elements * row_number) &&
            mouse_WC.z < startPosition.z * (gap_between_elements * column_number))
        {
            Debug.Log("La posicion del raton esta dentro del grid!!!");

        }


    }




}
