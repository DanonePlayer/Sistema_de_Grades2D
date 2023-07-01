using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Testing : MonoBehaviour
{
    private Grade<bool> grid;

    private void Start()
    {
        grid = new Grade<bool>(4, 2, 20f, Vector3.zero);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 position = UtilsClass.GetMouseWorldPosition();
        }

        if(Input.GetMouseButtonDown(1))
        {
            Debug.Log(grid.Pegar_Valor(UtilsClass.GetMouseWorldPosition()));
        }
    }
}
