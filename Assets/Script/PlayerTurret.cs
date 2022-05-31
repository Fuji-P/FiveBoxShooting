using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurret : MonoBehaviour
{
    //⑦PlayerCOntroller を格納する変数
    PlayerController playercontroller;
    //ミサイル
    [SerializeField] GameObject weapon_prefab;
    
    //ミサイル速度
    float shot_speed;

    // Start is called before the first frame update
    void Start()
    {
        //ミサイルの速度
        shot_speed = 800;

        //⑧PlayerControllerの取得
        playercontroller = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

        //⑨Zキーを押したらミサイルが発射される
        if (Input.GetKey(KeyCode.Z))
        {
            GameObject bullet = Instantiate(weapon_prefab, transform.position, transform.rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up * shot_speed);
        }

        //⑩右回転
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(new Vector3(0, 0, -playercontroller.turret_rotation_speed));
        }

        //⑪左回転
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(new Vector3(0, 0, playercontroller.turret_rotation_speed));
        }
    }
}
