using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///控制艾希旋转与放缩
/// </summary>
public class PlayerRotation : MonoBehaviour 
{
    //private Camera arCamera;
    public float xRotationSpeed = 1.0f;
    public float scaleOut = 1.025f;
    public float scaleIn = 0.975f;
    private Vector2 oldPositon1;
    private Vector2 oldPositon2;

    private void Start()
    {
        //arCamera = GameObject.Find("ARCamera").GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))//触屏了
        {
            if (Input.touchCount == 1)//一个手指头
            {
                if (Input.GetTouch(0).phase == TouchPhase.Moved)//状态移动
                {
                    this.transform.Rotate(0, -xRotationSpeed * Time.deltaTime * Input.GetAxis ("Mouse X"),0,Space.World);
                }
            }

            if (Input.touchCount == 2)//两个手指头
            {
                if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved)//两根手指头有一个在移动
                {
                    Vector2 temPositon1 = Input.GetTouch(0).position;
                    Vector2 temPositon2 = Input.GetTouch(1).position;
                    if (IsLarge(oldPositon1,oldPositon2,temPositon1,temPositon2))
                    {
                        this.transform.localScale = new Vector3(
                            this.transform.localScale.x * scaleOut, this.transform.localScale.y * scaleOut, this.transform.localScale.z * scaleOut);
                    }
                    else
                    {
                        this.transform.localScale = new Vector3(
                           this.transform.localScale.x * scaleIn, this.transform.localScale.y * scaleIn, this.transform.localScale.z * scaleIn);
                    }
                    oldPositon1 = temPositon1;
                    oldPositon2 = temPositon2;
                }
            }
        }
    }

    private bool IsLarge(Vector2 oldPoint1, Vector2 oldPoint2 , Vector2 newPoint1 , Vector2 newPoint2)
    {
        if (Vector2.Distance(oldPoint1, oldPoint2) < Vector2.Distance(newPoint1,newPoint2))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
 

