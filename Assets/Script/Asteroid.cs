using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    //爆発
    [SerializeField] GameObject explosion;

    //SpriteRenderer型の変数を宣言
    public Sprite[] sprites;

    //SpriteRenderer型の変数を宣言
    private SpriteRenderer ast_Renderer;

    //大きさの係数
    float scale;

    // Start is called before the first frame update
    void Start()
    {
        //SpriteRenderer を取得
        ast_Renderer = GetComponent<SpriteRenderer>();

        //乱数で画像を指定
        ast_Renderer.sprite = sprites[Random.Range(0, 6)];

        //大きさ係数を乱数で決定
        scale = Random.Range(3.0f, 8.0f);

        //大きさを指定
        transform.localScale = new Vector3(scale, scale, 1);

        //速度を与える
        GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-3, 3), Random.Range(-3f, -1f));
    }

    //衝突判定
    void OnTriggerEnter2D(Collider2D col)
    {
        //衝突判定
        if (col.gameObject.tag == TagName.Player ||
            col.gameObject.tag == TagName.PlayerProjectile)
        {
            //強度を1ずつ減算
            scale--;

            //爆発のプレハブを生成
            Instantiate(explosion, transform.position, Quaternion.identity);

            //もし強度が0以下になったら
            if (scale <= 0)
            {
                //消滅させる
                Destroy(gameObject);
            }
        }
    }

    //隕石の爆発関数
    public void DestroyAsteroid()
    {
        //消滅
        Destroy(gameObject);

        //爆発の生成
        Instantiate(explosion, transform.position, Quaternion.identity);
    }
}
