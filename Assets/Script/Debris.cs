using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
    //当たった時のエフェクト
    [SerializeField] GameObject hit_effect;

    //爆発
    [SerializeField] GameObject explosion;

    //Sprite型の配列変数を宣言
    public Sprite[] sprites;

    //SpriteRenderer型の変数を宣言
    private SpriteRenderer debRenderer;

    // Start is called before the first frame update
    void Start()
    {
        //SpriteRenderer を取得
        debRenderer = GetComponent<SpriteRenderer>();

        //乱数で画像を指定
        debRenderer.sprite = sprites[Random.Range(0, 6)];

        //デブリに速度を与える
        GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-15, 16), Random.Range(-10, -2));

    }

    // Update is called once per frame
    void Update()
    {
        //Y座標が-15を下回ったら削除
        if (transform.position.y < -15f)
        {
            Destroy(gameObject);
        }
    }

    //何かのエリアに侵入したとき
    private void OnTriggerEnter2D(Collider2D col)
    {
        //衝突の対象が "Enemy" でなければ
        if (col.gameObject.tag == TagName.Player ||
            col.gameObject.tag == TagName.PlayerProjectile)
        {
            DebrisDestroy();
        }
    }

    private void DebrisDestroy()
    {
        //ヒットエフェクトを生成
        Instantiate(hit_effect, transform.position, Quaternion.identity);

        //デブリ自身は消滅
        Destroy(gameObject);

        //爆発のエフェクトを生成
        GameObject effect = Instantiate(explosion, transform.position, transform.rotation);

        //爆発エフェクトの削除
        Destroy(effect, 1.5f);
    }
}
