using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour
{
    //それぞれの背景のY座標
    [SerializeField] float point;
    //移動速度
    float bgSpeed;

    // Start is called before the first frame update
    void Start()
    {
        //初期位置
        transform.position = new Vector3(0, point, 0);
        bgSpeed = 0.01f;
    }

    // Update is called once per frame
    void Update()
    {
        //移動
        transform.position -= new Vector3(0, bgSpeed, 0);

        //画面の下に消えたら・・・
        if (transform.position.y < -40)
        {
            //最上部に行って準備
            transform.position = new Vector3(0, 120, 0);
        }
    }
}
