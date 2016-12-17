using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class BallScript : MonoBehaviour {

    public GameObject gameOverObject;
    public GameObject gameClearObject;

    // Use this for initialization
    void Start () {

        var rigidbody = GetComponent<Rigidbody>();
        rigidbody.maxAngularVelocity = 100;
    }
	
	// Update is called once per frame
	void Update ()
    {
        var position = transform.position;
        var rigidbody = GetComponent<Rigidbody>();
        var velocity = rigidbody.velocity;

        //var rotation = transform.rotation;
        var x = transform.position.x;

        float gensoku = 0.2f, kasoku = 0.5f, kasokuZ = 0.1f;

        if (Mathf.Abs(velocity.x) < gensoku) velocity.x = 0;
        else if (gensoku < velocity.x) velocity.x -= gensoku;
        else if (velocity.x < -gensoku) velocity.x += gensoku;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            velocity.x +=kasoku;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            velocity.x -= kasoku;
        }
		else if (Input.GetKey(KeyCode.UpArrow))
		{//上ボタン
			velocity.z += kasokuZ;
		}
		else if (Input.GetKey(KeyCode.DownArrow))
		{//下ボタン
			velocity.z -= kasokuZ;
			if (velocity.z < 0.3)
				velocity.z = 0.3f;
		}

        //落下速度が-20msを超えたらゲームオーバー
        if (velocity.y < -20)
        {
            //Time.timeScale = 0;
            gameOverObject.SetActive(true);
        }
        //transform.rotation = rotation;
        transform.position = position;
        rigidbody.velocity = velocity;
        //Debug.Log("aaaa");
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("OnCollisionEnter");
        if(other.gameObject.CompareTag("GoalLine"))
        {
            gameClearObject.SetActive(true);
            Debug.Log("Goal111");
            Time.timeScale = 0;
        }
    }
}
