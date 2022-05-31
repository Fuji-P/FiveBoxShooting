using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    //回転速度（オブジェクト側から指定）
    public float rotateSpeed;

    // Update is called once per frame
    void Update()
    {
        //Z軸中心として回転
        transform.Rotate(new Vector3(0, 0, rotateSpeed)); 
    }
}
