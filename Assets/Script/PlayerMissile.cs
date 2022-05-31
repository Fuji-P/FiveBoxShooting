using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissile : MonoBehaviour
{
    Rigidbody2D rb2d;

    //爆発のプレハブ
    [SerializeField] GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        //❸コルーチン型関数を発動
        StartCoroutine(shot());
    }

    // Update is called once per frame
    void Update()
    {
        //❹移動の力を与える
        rb2d.velocity -= new Vector2(0, 0.005f);
        //❺徐々に小さく
        transform.localScale += new Vector3(-0.0005f, -0.0005f, 0);
    }

    //❷コルーチン型関数（爆弾の動き）
    IEnumerator shot()
    {
        //4秒待つ
        yield return new WaitForSeconds(4f);
        //自分自身を削除
        Destroy(gameObject);
        //爆発を生成
        Instantiate(explosion, transform.position, Quaternion.identity);
    }
}
