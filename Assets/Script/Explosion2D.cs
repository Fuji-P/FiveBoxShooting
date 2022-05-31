using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion2D : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var child = transform.Find("BaseSmoke").gameObject;

        var particle = child.GetComponent<ParticleSystem>();

        //もし"particle" が起動していなければ
        if (!particle.IsAlive())
        {
            //このゲームオブジェクトを削除
            Destroy(gameObject);
        }
    }
}
