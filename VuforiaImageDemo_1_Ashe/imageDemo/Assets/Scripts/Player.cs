using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
public class Player : MonoBehaviour 
{
    private void Update()
    {
        if (this.transform.GetChild(0).position.y < 0)
            this.transform.GetChild(0).position += new Vector3(0, 0.01f, 0);
    }
}
 

