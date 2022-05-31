using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Controller : MonoBehaviour
{
    //時間を管理する変数
    private float totalTime;

    //登場と通常の動きを判定するフラグ
    private bool IsMove;

    //敵のRigidbody
    private Rigidbody2D enemyRd2d;

    //左に出現(-1)、右に出現(1)
    public int appPos;

    //弾の速さ
    private float shot_speed;

    //ヒットポイント
    private float shotDelay;

    //ヒットポイント
    private int hp;

    //EnemyBulletを格納
    [SerializeField] GameObject EnemyBullet;

    //爆発エフェクト
    [SerializeField] GameObject explosion;

    //Enemy2プレハブ
    [SerializeField] GameObject enemy2;

    //アイテムプレハブ
    [SerializeField] GameObject item;

    //GameManager
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        hp = 3;
        shot_speed = 800;

        //Rigidbody2D を取得
        enemyRd2d = GetComponent<Rigidbody2D>();

        //コルーチン発動
        StartCoroutine(Enemy1MoveCol(appPos));
        StartCoroutine(Enemy1Shot());

        //変数：gameManager の中身を取得する
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //通常の動き
        Enemy1Move(appPos);
    }

    //通常の動き 引数（-1と1）で画面左と画面右の動きを左右対象とする
    private void Enemy1Move(int n)
    {
        if (IsMove)
        {
            //totalTime を加算
            totalTime += Time.deltaTime;
            //横方向の加速
            float x = Mathf.Sin(totalTime) * 7 * n;
            //縦方向の加速
            float y = Mathf.Cos(totalTime * 2) * 10;
            //加速値をリジッドボディに代入
            enemyRd2d.velocity = new Vector2(x, y);
        }
    }

    //侵入判定（プレイヤーの攻撃に当たったらダメージ）
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //衝突条件
        if (
            collision.gameObject.tag == TagName.PlayerProjectile &&
            IsMove
            )
        {
            Enemy1Damage(collision);
        }
    }

    //ダメージ処理
    private void Enemy1Damage(Collider2D collision)
    {
        //弾の削除
        Destroy(collision.gameObject);
        //HPの減算
        hp--;
        //2発当たる場合があるので、ここは==にしておきます
        if (hp == 0)
        {
            Enemy1Destroy();
        }
    }

    //消滅させる関数を作成
    private void Enemy1Destroy()
    {
        //爆発エフェクトの生成
        GameObject effect = Instantiate(explosion, transform.position, transform.rotation);
        //爆発エフェクトの削除
        Destroy(effect, 1.5f);
        //Enemyの削除
        Destroy(gameObject);
        //EnemyCountを減算
        gameManager.enemyCount1--;
        //スコア加算
        gameManager.AddScore(100);
        //Enemy2の生成
        Instantiate(enemy2);
        //EnemyCountを加算
        gameManager.enemyCount2++;
        //1/2の確率で
        if (Random.Range(0, 2) < 1)
        {
            //アイテム生成
            Instantiate(item, transform.position, Quaternion.identity);
        }
    }

    //敵登場時の動き 引数（-1と1）で登場場所と動きを左右対称とする
    IEnumerator Enemy1MoveCol(int n)
    {
        //初期位置
        transform.position = new Vector3(18f * n, 10f, 0);
        //左方向に移動
        enemyRd2d.velocity = new Vector2(-3f * n, -1f);
        //5秒継続
        yield return new WaitForSeconds(5);
        //移動を止める
        enemyRd2d.velocity = transform.up * 0;
        //0.5秒継続
        yield return new WaitForSeconds(0.5f);
        //シールドを削除
        Destroy(transform.GetChild(0).gameObject);
        //フラグをtrueに
        IsMove = true;
    }

    //敵の攻撃
    IEnumerator Enemy1Shot()
    {
        //動き出すまでの時間待つ
        yield return new WaitForSeconds(6);

        while (true)
        {
            //発射間隔を乱数で指定
            shotDelay = Random.Range(0.5f, 4f);

            //発射間隔を乱数で指定
            yield return new WaitForSeconds(shotDelay);

            //弾の生成
            GameObject Laser = Instantiate(EnemyBullet, transform.position, transform.rotation);

            //弾の速度
            Laser.GetComponent<Rigidbody2D>().AddForce(EnemyBullet.transform.up * shot_speed);
        }
    }
}
