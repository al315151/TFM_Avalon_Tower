using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Construction_Grid_Generator : MonoBehaviour
{
   
    public GameObject grid_Node;

    public GameObject up_left_limit_Node;
    public GameObject down_right_limit_Node;

    
    //GRID RELATED VARIABLES
    List<List<GameObject>> grid_matrix;
    [Range(1, 20)]
    public float row_number  = 10f;
    [Range(1, 20)]
    public float column_number = 10f;
    float gap_between_elements = 5f;
    Vector3 startPosition;

    //MOUSE DETECTION RELATED VARIABLES
    public bool Allow_position_mouse = true;
    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        grid_matrix = new List<List<GameObject>>();
        //CreateNodeVector();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
       // if (Allow_position_mouse && Input.GetMouseButtonDown(0))
       // {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                GameObject objectHit = hit.transform.gameObject;
                //grid_Node.transform.position = hit.point;
                //UpdateGridSelectionByMouse(hit.point);
                MousePositionByGridMatrix(hit.point,
                    up_left_limit_Node.transform.position,
                    down_right_limit_Node.transform.position);
            }

           // Debug.Log("Click Izquierdo Apretado");
           // DetectNodeFromMousePosition();
       // }
    }

    void CreateNodeVector()
    {
       startPosition = new Vector3(transform.position.x - gap_between_elements * row_number / 2, 0,
                                       transform.position.z - gap_between_elements * row_number / 2);
        grid_matrix.Clear();
        for (int i = 0; i < row_number; i++)
        {
            List<GameObject> new_row = new List<GameObject>();
            for (int j = 0; j < column_number; j++)
            {
                GameObject newObject = Instantiate(grid_Node, new Vector3(startPosition.x + gap_between_elements * i, 0,
                                                                      startPosition.z + gap_between_elements * j), Quaternion.identity, transform);
                new_row.Add(newObject);
            }
            grid_matrix.Add(new_row);
        }
    }

    void MousePositionByGridMatrix(Vector3 mousePos, Vector3 up_left, Vector3 down_right)
    {
        
          if (mousePos.x < up_left.x || mousePos.x > down_right.x ||
              mousePos.z > up_left.z || mousePos.z < down_right.z)
            { return;        }

        // obtenemos espacio de trabajo, y lo centramos en 0,0.
        float difference_in_X = down_right.x - up_left.x - up_left.x;
        float difference_in_Z = up_left.z - down_right.z - down_right.z;
        float mouse_pos_x = mousePos.x - up_left.x;
        float mouse_pos_z = mousePos.z - down_right.z;


        // en cuanto dividimos el espacio de trabajo.
        float diff_to_row = difference_in_X / row_number;
        float diff_to_col = difference_in_Z / column_number;

        //Ahora, tenemos que ver como trasladar coordenadas. 
        
        float x_by_grid = mouse_pos_x / diff_to_row;
        float z_by_grid = mouse_pos_z / diff_to_col;



        grid_Node.transform.position = mousePos;
        //Debug.Log(x_by_grid + " for X coordinates. ");
        Debug.Log(z_by_grid + " for z coordinates. ");
        

    }



   void UpdateGridSelectionByMouse(Vector3 impactPoint)
    {
        float min_X = grid_matrix[0][0].transform.position.x;
        float min_Z = grid_matrix[0][0].transform.position.z;
        float max_X = grid_matrix[grid_matrix.Count - 1][grid_matrix.Count - 1].transform.position.x;
        float max_Z = grid_matrix[grid_matrix.Count - 1][grid_matrix.Count - 1].transform.position.z;

        if (impactPoint.x < min_X || impactPoint.x > max_X ||
            impactPoint.z < min_Z || impactPoint.z > max_Z)
        {   return;    }

        float matrix_row_of_imp_point = (impactPoint.x / max_X);
        float matrix_col_of_imp_point = (impactPoint.z / max_Z);
        Debug.Log(matrix_row_of_imp_point + " for rows. ");
        Debug.Log(matrix_col_of_imp_point + " for cols. ");

    }

    

}
