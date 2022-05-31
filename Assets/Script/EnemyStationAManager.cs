using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStationAManager : MonoBehaviour
{
    //基本の速度を管理する変数
    private float speed;

    //Rigidbody2Dを管理するための変
    Rigidbody2D sARb2d;

    //登場と通常の動きを判定するフラグ
    bool IsMove = false;

    //GameManagerクラスを呼出すための変数宣言
    GameManager gameManager;

    int hp;

    //爆発のプレハブ
    [SerializeField] GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        //speed の初期値を2fにセット
        speed = 2f;
        //Rigidbody2Dを取得
        sARb2d = GetComponent<Rigidbody2D>();
        //コルーチン型関数を発動
        StartCoroutine(StationA_Move());
        //hp に20を代入
        hp = 20;
        //検索
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        //初期化
        gameManager.enemyCount3 = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //登場時は移動制限がかからないように
        if (IsMove)
        {
            //移動制限
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -18f, 18f),
            Mathf.Clamp(transform.position.y, -12f, 12f), 1);
        }
    }

    //侵入判定
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //侵入したタグが"PlayerMissile"なら
        if (collision.tag == TagName.PlayerMissile)
        {
            //hpを1ずつ減少
            hp--;
            //デバック
            Debug.Log("中ボスHP:"+hp);

            //もし hp が 0 になったら
            if (hp == 0)
            {
                //コルーチン StationA_Bomb() 発動
                StartCoroutine(StationA_Bomb());
            }
        }
    }

    //移動のコルーチン型関数
    IEnumerator StationA_Move()
    {

        //初期位置から移動
        transform.position = new Vector3(Random.Range(-15f, 15f), 12f, 0);

        //下方向に移動
        sARb2d.velocity = new Vector2(0, -1f);

        //3秒継続
        yield return new WaitForSeconds(3);

        //移動を止める
        sARb2d.velocity = transform.up * 0;

        //0.5秒停止
        yield return new WaitForSeconds(0.5f);

        //IsMove をtrueに
        IsMove = true;

        //繰り返し処理
        while (IsMove)
        {
            //横の動きを乱数で取得
            float x = Random.Range(-3.0f, 3.0f);

            //縦の動きを乱数で取得
            float y = Random.Range(-3.0f, 3.0f);

            //移動方向を変数で取得
            Vector2 direction = new Vector2(x, y);

            //移動
            sARb2d.velocity = direction * speed;

            //移動
            yield return new WaitForSeconds(3);

            //移動を止める
            sARb2d.velocity = transform.up * 0;

            //1秒停止
            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator StationA_Bomb()
    {
        //通常の動きを止める
        IsMove = false;

        //速度を0にする
        sARb2d.velocity = transform.up * 0;

        //小さな爆発を20回繰り返す
        for (int i = 0; i < 20; i++)
        {
            //爆発位置(x)
            float x = transform.position.x + Random.Range(-4.0f, 4.0f);

            //爆発位置(y)
            float y = transform.position.y + Random.Range(-8.0f, 8.0f);

            //爆発間隔
            yield return new WaitForSeconds(Random.Range(0.1f, 0.3f));

            //爆発生成
            Instantiate(explosion, new Vector3(x, y, 1), Quaternion.identity);
        }

       //最後の大爆発の処理

       //大爆発を生成し変数にセット
        GameObject bomb = Instantiate(explosion);

        //ポジション指定
        bomb.transform.position = transform.position;

        //fireBall の子要素"FireBall"から「ParticleSystem」を取得し変数にセット
        var fireBall = bomb.transform.Find("FireBall").GetComponent<ParticleSystem>();

        //大きさを指定
        fireBall.startSize = 50;

        //スピードを指定
        fireBall.playbackSpeed = 0.2f;

        //2秒待つ
        yield return new WaitForSeconds(2f);

        //削除
        Destroy(gameObject);

        //0に戻して敵がいなくなったことを宣言
        gameManager.enemyCount3 = 0;
    }
}
