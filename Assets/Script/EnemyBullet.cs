using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    //発射時のエフェクト
    public GameObject shoot_effect;

    //当たった時のエフェクト
    public GameObject hit_effect;

    // Start is called before the first frame update
    void Start()
    {
        //弾の大きさ
        //5倍に変更
        transform.localScale = new Vector3(5, 5, 1);
        Instantiate(shoot_effect, transform.position, Quaternion.identity);

        //3秒後に消滅
        Destroy(gameObject, 3f);
    }

    //衝突判定
    private void OnTriggerEnter2D(Collider2D col)
    {
        //タグが Enemy で無かったら
        if (col.tag == TagName.Player)
        {
            Instantiate(hit_effect, transform.position, Quaternion.identity);
            //当たったら消滅
            Destroy(gameObject);
        }
    }
}
