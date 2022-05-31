using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    //リジッドボディ型の変数を宣言
    Rigidbody2D itemRd2d;

    // Start is called before the first frame update
    void Start()
    {
        //コンポーネントを取得
        itemRd2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //❶下向きに移動
        itemRd2d.velocity = transform.up * -2f;

        //x座標が-15を下回ったら
        if (transform.position.y < -15f)
        {
            //Item を削除
            Destroy(gameObject);
        }
    }
}
