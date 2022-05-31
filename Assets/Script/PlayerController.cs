using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    //マシンの加速度
    public float acceleration = 5f;

    //Rigidbodyを管理する変数
    public float turret_rotation_speed = 3f;

    //Rigidbodyを管理する変数
    Rigidbody2D playerRb2d;

    //②ミサイル速度
    float shot_speed;

    //Player のHP
    public int hp;

    //①ミサイル
    [SerializeField] GameObject weapon_prefab;

    //爆発エフェクト
    [SerializeField] GameObject Explosion;

    //①HPテキスト
    [SerializeField] Text HPText;

    //Missileプレハブ
    [SerializeField] GameObject missile;

    //❶変数として宣言
    [SerializeField]  GameObject gameOverText;

    // Start is called before the first frame update
    void Start()
    {
        //Rigidbodyコンポーネントの取得
        playerRb2d = GetComponent<Rigidbody2D>();

        //初期位置指定
        transform.position = new Vector3(0, -5, 0);

        //③ミサイルの速度に800を代入
        shot_speed = 800;

        //HPの初期値を10に指定
//        hp = 10;

        //②HPテキスト更新
        HPText.text = "HP:" + hp.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Restrictions();
        Shot();
    }

    /*Playerの動き*/
    private void Move()
    {

        //減速
        playerRb2d.velocity *= 0.995f;

        //左キーを押したとき
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //左向きの速度を与える
            playerRb2d.AddForce((-transform.right) * acceleration);
        }

        //右キーを押したとき
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //右向きの速度を与える
            playerRb2d.AddForce((transform.right) * acceleration);
        }

        //上キーを押したとき
        if (Input.GetKey(KeyCode.UpArrow))
        {
            //上向きの速度を与える
            playerRb2d.AddForce((transform.up) * acceleration);
        }

        //下キーを押したとき
        if (Input.GetKey(KeyCode.DownArrow))
        {
            //下向きの速度を与える
            playerRb2d.AddForce((-transform.up) * acceleration);
        }
    }

    /*移動制限*/
    private void Restrictions()
    {
        //右の移動制限
        if (transform.position.x > 19)
        {
            transform.position = new Vector3(19, transform.position.y, 0);
            playerRb2d.velocity = new Vector2(0, playerRb2d.velocity.y);
        }

        //左の移動制限
        if (transform.position.x < -19)
        {
            transform.position = new Vector3(-19, transform.position.y, 0);
            playerRb2d.velocity = new Vector2(0, playerRb2d.velocity.y);
        }

        //上の移動制限
        if (transform.position.y > 8)
        {
            transform.position = new Vector3(transform.position.x, 8, 0);
            playerRb2d.velocity = new Vector2(playerRb2d.velocity.x, 0);
        }

        //下の移動制限
        if (transform.position.y < -8)
        {
            transform.position = new Vector3(transform.position.x, -8, 0);
            playerRb2d.velocity = new Vector2(playerRb2d.velocity.x, 0);
        }
    }

    /*ミサイルを撃つ*/
    private void Shot()
    {
        /*スペースキーが押されたら*/
        if (Input.GetKey(KeyCode.Z))
        {
            /*ミサイルを生成*/
            GameObject bullet = Instantiate(weapon_prefab, transform.position, transform.rotation);

            /*ミサイルに速度を与える*/
            bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up * shot_speed);
            Destroy(bullet, 3f);
        }
        //❶X.キーが押されたら
        if (Input.GetKeyDown(KeyCode.X))
        {
            //ミサイルプレハブを自分の場所に生成
            Instantiate(missile, transform.position, transform.rotation);
        }
    }

    //③ダメージ時の処理
    private void SubHP(int h)
    {
        //HP減算
//        hp -= h;

        //HPテキスト更新
        HPText.text = "HP:" + hp.ToString();

        //HPが0になったら Destroy()関数発動
        if (hp <= 0)
        {
            StartCoroutine(PlayerDestroy());
        }
    }

    //侵入判定
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //対象のタグが"Enemy"なら
        if (
            collision.tag== "Enemy" ||
            collision.tag== "EnemyProjectile"
            )
        {
            //HPを1減らす
            SubHP(1);
        }

        //❺"Item"タグを持っていたら
        if (collision.tag == "Item")
        {
            //❻hp 1加算
            hp++;
            //❼HPText更新
            HPText.text = "HP:" + hp.ToString();
            //❽Item削除
            Destroy(collision.gameObject);
        }
    }

    //衝突判定
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //対象のタグが"Asteroid"なら
        if (collision.gameObject.tag == "Asteroid")
        {
            //hpを2ずつ減らす
            SubHP(2);
        }
    }

    //消滅させる関数を作成
    IEnumerator PlayerDestroy()
    {
        //爆発生成
        Instantiate(Explosion, transform.position, Quaternion.identity);
        //❷テキストを表示
        gameOverText.SetActive(true);
        yield return new WaitForSeconds(2f);
        //消滅
        Destroy(gameObject);
    }
}


