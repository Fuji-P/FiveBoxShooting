using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Controller : MonoBehaviour
{
    //登場と通常の動きを判定するフラグ
    private bool IsMove = false;

    //敵のRigidbody
    Rigidbody2D enemyRb2d;

    //弾の速さ
    private float shot_speed;

    //弾を撃つ間隔
    private float shotDelay;

    //ヒットポイント
    private int hp;

    //EnemyBulletを格納
    [SerializeField] GameObject EnemyBullet;

    //爆発エフェクト
    [SerializeField] GameObject explosion;

    //GameManagerを入れる変数
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        //hpに初期値5を代入
        hp = 5;

        //弾の速さ
        shot_speed = 300;

        //Rigidbody2D を取得
        enemyRb2d = GetComponent<Rigidbody2D>();

        //コルーチンを発動
        StartCoroutine(Enemy2MoveCol());
        StartCoroutine(Enemy2ShotCol());

        //gameManagaer を探してきて変数に代入
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //移動制限
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -18f, 18f), transform.position.y, 1);
    }

    //侵入判定
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (
            collision.gameObject.tag == TagName.PlayerProjectile &&
            IsMove
            )
        {
            Enemy2Damage(collision);
        }
    }

    private void Enemy2Damage(Collider2D collision)
    {
        //弾の削除
        Destroy(collision.gameObject);

        //hp の減算
        hp--;

        //hpが 0 になったら
        if (hp == 0)
        {
            Enemy2Destroy();
        }
    }

    //消滅させる関数を作成
    private void Enemy2Destroy()
    {
        //爆発のエフェクトを生成
        GameObject effect = Instantiate(explosion, transform.position, transform.rotation);
        //爆発エフェクトの削除
        Destroy(effect, 1.5f);
        //⑧EnemyCountを減算
        gameManager.enemyCount2--;
        //Enemyの削除
        Destroy(gameObject);
        //スコアを200追加
        gameManager.AddScore(200);
    }

    //敵の登場とその後の動き
    IEnumerator Enemy2MoveCol()
    {
        //初期位置
        transform.position = new Vector3(Random.Range(-15f,15f), 12f, 0);
        //下方向に移動
        enemyRb2d.velocity = new Vector2(0, -1f);
        //3秒継続
        yield return new WaitForSeconds(3);
        //移動を止める
        enemyRb2d.velocity = transform.up * 0;
        //0.5秒停止
        yield return new WaitForSeconds(0.5f);
        //❶シールドを削除する
        Destroy(transform.GetChild(0).gameObject);
        //フラグをtrueに
        IsMove = true;

        //ｙ座標が-10まで継続
        while (transform.position.y > -10f)
        {
            //x方向の速さを乱数で指定
            float moveX = Random.Range(-7.0f, 7.0f);
            //横方向の移動
            enemyRb2d.velocity = transform.right * moveX;
            //2秒継続
            yield return new WaitForSeconds(2);
            //移動を止める
            enemyRb2d.velocity = transform.right * 0;
            //0.5秒停止
            yield return new WaitForSeconds(0.5f);
            //下方向に移動
            enemyRb2d.velocity = new Vector2(0, -2f);
            //1秒継続
            yield return new WaitForSeconds(1);
        }
        //オブジェクトを削除
        Destroy(gameObject);
    }

    //敵２攻撃のコルーチン関数
    IEnumerator Enemy2ShotCol()
    {
        //仮）5秒待つ
        yield return new WaitForSeconds(5);
        //内部の同じ動きを繰り返す
        while (true)
        {
            //間隔を0.5秒から1.5秒の間の乱数で指定
            shotDelay = Random.Range(0.5f, 1.5f);
            //発射の間隔を1～3秒の間の乱数
            shotDelay = Random.Range(1.0f, 3f);

            //指定した間隔待つ
            yield return new WaitForSeconds(shotDelay);

            //弾の生成
            GameObject Laser = Instantiate(EnemyBullet, transform.position, transform.rotation);

            //弾に速度を与える
            Laser.GetComponent<Rigidbody2D>().AddForce(EnemyBullet.transform.up * shot_speed);
        }
    }
}
