using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;   //❸namespace を指定

public class GameManager : MonoBehaviour
{
    //隕石のプレファブ
    [SerializeField] GameObject asteroidPrefab;

    //デブリのプレハブ
    [SerializeField] GameObject Debri;

    //③Enemy1のプレファブ
    [SerializeField] GameObject Enemy1;

    //①Score_Text オブジェクトを入れる変数
    [SerializeField] Text scoreText;

    //❶EnemyStationAプレハブ
    [SerializeField] GameObject EnemyStationA;

    //❶gameStartTextを宣言
    public GameObject gameStartText;

    //❷gameOverTextを宣言
    public GameObject gameOverText;

    //④敵の管理
    public int enemyCount1 = 0;
    public int enemyCount2 = 0;
    public int enemyCount3 = 0;

    //❷Enemyの出現パターン管理変数
    int enemyAppPattern;

    //出現判定係数
    int D;

    //②スコアを入れる変数
    static int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        //③スコアテキストを更新
        scoreText.text = "Score："+score.ToString();

        //❸初期化
        enemyAppPattern = 1;

        //❹gameOverTextは隠しておく
        gameOverText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //❺スタート条件
        if (
            !IsGame() &&
            Input.GetKeyDown(KeyCode.S)
            )
        {
            //❻ゲームスタート
            gameStartText.SetActive(false);
        }
        
        if (
            gameOverText.activeSelf &&
            Input.GetKeyDown(KeyCode.S)
            )
        {
            //❹Sceneセット
            Scene thisScene = SceneManager.GetActiveScene();
            //❺Scene発動
            SceneManager.LoadScene(thisScene.name);
        }
        
        //❼もしゲーム中ならば（内部記述済み）
        if (!IsGame())
        {
            return;
        }
        //❹Enemy1出現条件
        if (
            enemyCount1 == 0 &&
            enemyCount2 == 0 &&
            enemyCount3 == 0 &&
            enemyAppPattern <= 2
            )
        {
            //⑪CreateEnemy()を発動
            StartCoroutine(CreateEnemy());
        }

        //❺EnemyStation出現条件
        else if (
            enemyCount1 == 0 &&
            enemyCount2 == 0 &&
            enemyCount3 == 0 &&
            enemyAppPattern == 3
            )
        {
            //❻EnemyStationAを生成
            Instantiate(EnemyStationA);

            //❼出現パターン管理変数加算
            enemyAppPattern++;
        }

        //もしDが0なら
        D = Random.Range(0, 2000);

        //もしDが0なら
        if (D == 0)
        {
            //隕石を生成
            CriateAsteroid();
        }
        else if (D <= 8)
        {
            //デブリ生成関数
            CriateDebris();
        }
    }
     //隕石生成
    void CriateAsteroid()
    {
        //隕石を生成し変数に代入
        GameObject asteroid = Instantiate(asteroidPrefab);

        //隕石の出現場所を乱数で指定
        asteroid.transform.position = new Vector3(Random.Range(-20, 20), 20f, 0);
    }

    //デブリ生成
    void CriateDebris()
    {
        //デブリの生成
        GameObject debri = Instantiate(Debri);

        //デブリの生成場所変更
        debri.transform.position = new Vector3(Random.Range(-20, 20), 20f, 0);
    }

    //スコア加算関数
    public void AddScore(int s)
    {
        //④スコアを加算
        score += s;
        //③と同じ
        scoreText.text = "Score:" + score.ToString();
    }

    //❸ゲーム中かどうかを判定する関数
    public bool IsGame()
    {
        if (gameStartText.activeSelf)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    IEnumerator CreateEnemy()
    {
        //⑥6体生成するので
        enemyCount1 = 6;
        yield return new WaitForSeconds(2);
        //⑦[Enemy1Controller]型のインスタンス：e1を生成
        Enemy1Controller e1 = Instantiate(Enemy1).GetComponent<Enemy1Controller>();
        //⑧e1 の[appPos](方向を決める引数に入る変数) に 1 を代入
        e1.appPos = 1;
        //⑨2体目のインスタンス:e2 を生成
        Enemy1Controller e2 = Instantiate(Enemy1).GetComponent<Enemy1Controller>();
        //⑩e2の[appPos]には -1 を代入、これで左から出現
        e2.appPos = -1;
        yield return new WaitForSeconds(2.2f);
        Enemy1Controller e3 = Instantiate(Enemy1).GetComponent<Enemy1Controller>();
        e3.appPos = 1;
        Enemy1Controller e4 = Instantiate(Enemy1).GetComponent<Enemy1Controller>();
        e4.appPos = -1;
        yield return new WaitForSeconds(2.2f);
        Enemy1Controller e5 = Instantiate(Enemy1).GetComponent<Enemy1Controller>();
        e5.appPos = 1;
        Enemy1Controller e6 = Instantiate(Enemy1).GetComponent<Enemy1Controller>();
        e6.appPos = -1;
        //❽出現パターン管理変数を加算
        enemyAppPattern++;
    }
}
