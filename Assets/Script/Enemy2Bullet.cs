using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Bullet : MonoBehaviour
{

    //発射時のエフェクト
    public GameObject shoot_effect;

    //当たった時のエフェクト
    public GameObject hit_effect;

    //敵の弾のレンダラー
    SpriteRenderer ebrenderer;

    // Start is called before the first frame update
    void Start()
    {
        //ebrenderer を取得
        ebrenderer =GetComponent<SpriteRenderer>();
        Instantiate(shoot_effect, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        //枠外に出たら
        if(!ebrenderer.isVisible)
        {
            //消滅
            Destroy(gameObject);
        }
    }

    //侵入判定
    private void OnTriggerEnter2D(Collider2D col)
    {
        //タグが Enemy で無かったら
        if (col.tag == TagName.Player)
        {
            Instantiate(hit_effect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
