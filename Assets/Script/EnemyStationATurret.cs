using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStationATurret : MonoBehaviour
{
    //ヒットポイント
    int hp;

    //弾の速さ
    float shot_speed;

    //弾を撃つ間隔
    float shotDelay;

    //EnemyBulletを格納
    [SerializeField] GameObject EnemyBullet;

    //爆発エフェクト
    [SerializeField] GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        //hp の初期値を50に指定
        hp = 50;

        //ショットスピードを800に指定
        shot_speed = 800;
        //ショットコルーチンを発動
        StartCoroutine(Shot());
    }

    // Update is called once per frame
    void Update()
    {
        //❸ずっと回転
        transform.Rotate(new Vector3(0, 0, 0.1f));
    }

    //❷ダメージ判定のイベント関数
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Player の攻撃に当たったら
        if (collision.gameObject.tag == "PlayerProjectile" )
        {
            EnemyStationADamage(collision);
        }
    }

    private void EnemyStationADamage(Collider2D collision)
    {
        //弾の削除
        Destroy(collision.gameObject);
        //hp を1ずつ減らす
        hp--;
        //HPが0になったら
        if (hp == 0)
        {
            EnemyStationADestroy();
        }
    }

    private void EnemyStationADestroy()
    {
        //爆発のエフェクトを生成
        GameObject effect = Instantiate(explosion, transform.position, transform.rotation);

        //爆発エフェクトの削除
        Destroy(effect, 1.5f);

        //Enemyの削除
        Destroy(gameObject);
    }


    //❶弾を生成するコルーチン型関数
    IEnumerator Shot()
    {
        //仮の待ち時間
        yield return new WaitForSeconds(1);

        //ずっとこの内部の同じ動きを繰り返す
        while (true)
        {
            //発射の間隔を0.5～4秒の乱数
            shotDelay = Random.Range(0.5f, 3f);

            //指定した間隔待って次の処理へ
            yield return new WaitForSeconds(shotDelay);

           //弾を生成するプログラム
            GameObject bullet = Instantiate(EnemyBullet, transform.position, transform.rotation);

            //弾に速さを与えるプログラム
            bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up * shot_speed);
        }
    }
}
