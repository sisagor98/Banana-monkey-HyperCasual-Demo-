using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScripts : MonoBehaviour
{
    private Rigidbody2D myBody;
    public float move_Speed = 2f;
    public float normal_Push = 10f;
    public float extra_Push = 14f;

    private bool initial_Push;
    private int push_Count;
    private bool player_Died;
    // Start is called before the first frame update
    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (player_Died)
            return;
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            myBody.velocity = new Vector2(move_Speed, myBody.velocity.y);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
            {
                myBody.velocity = new Vector2(-move_Speed, myBody.velocity.y);
            }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "ExtraPush")
        {
            if (!initial_Push)
            {
                initial_Push = true;
                myBody.velocity = new Vector2(myBody.velocity.x, 18f);
                other.gameObject.SetActive(false);
                return;
            }
        }
        if(other.tag == "NormalPush")
        {
            myBody.velocity = new Vector2(myBody.velocity.x, normal_Push);
            other.gameObject.SetActive(false);
            push_Count++;
        }
        if (other.tag == "ExtraPush")
        {
            myBody.velocity = new Vector2(myBody.velocity.x, extra_Push);
            other.gameObject.SetActive(false);
            push_Count++;
        }

        if(push_Count == 2)
        {
            push_Count = 0;
            PlatformSpawner.instance.SpawnPlatform();
        }

    }
}
