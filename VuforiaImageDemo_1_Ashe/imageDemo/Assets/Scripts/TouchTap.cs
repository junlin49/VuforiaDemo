using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///双击AIXI后，销毁物体
/// </summary>
public class TouchTap : MonoBehaviour 
{
    private float touchTime;
    private bool touchNew = false;
    private void Update()
    {
        if (Input.GetMouseButton(0))//有触屏
        {
            //从点击位置发射射线
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
           //接受返回信息
            RaycastHit hitInfor;

            if (Physics.Raycast(ray,out hitInfor))//如果接受到信息
            {
              //  if(Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)//1根手指点击，且刚刚点击
               // {
                    //双击
                    //if(Input.GetTouch(0).tapCount == 2)//双击
                    //{
                    //    Destroy(hitInfor.collider.gameObject);//射线射到的collider所属的游戏物体，销毁
                    //}

                    //长按aixi，销毁
                    if (Input.touchCount == 1)//1根手指按
                    {
                        Touch touch = Input.GetTouch(0);//第一根触摸的手指
                        if (touch.phase == TouchPhase.Began)//刚按，A finger touched the screen.
                        {
                            touchNew = true;
                            touchTime = Time.time;//记录开始按的时候的时间
                        }
                        else if (touch.phase == TouchPhase.Stationary)//按住静止不动；A finger is touching the screen but hasn't moved.
                        {
                          if (touchNew == true && Time.time-touchTime>1)//长按1s后
                            {
                                touchNew = false;
                                Destroy(hitInfor.collider.gameObject);
                            }
                        }
                        else
                        {
                            touchNew = false;
                        }
                    }
               // }
            }
        }
    }
}
 

