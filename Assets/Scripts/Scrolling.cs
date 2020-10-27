using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour
{
    private Rigidbody2D rb;
    private float speed = -1.5f;

    [SerializeField]
    private bool cancelScrolling;




    private BoxCollider2D pieceOfBgCollider;
    private float pieceOfBgSize;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0f, speed);

        pieceOfBgCollider = GetComponent<BoxCollider2D>();
        pieceOfBgSize = pieceOfBgCollider.bounds.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (cancelScrolling)
            rb.velocity = Vector2.zero;
        else
            rb.velocity = new Vector2(0f, speed);


        if (transform.position.y < -pieceOfBgSize)
        {
            if (Controller.instance.countDown <= 0)
            {
                if (Controller.instance.bgOrder == 0)
                {
                    Controller.instance.SwitchColor(Controller.instance.green, 0);
                    Controller.instance.bgOrder += 1;
                    Debug.Log("Color Switched");
                }
                else if (Controller.instance.bgOrder == 1)
                {
                    Controller.instance.SwitchColor(Controller.instance.green, 1);
                    Controller.instance.bgOrder = 0;
                    Controller.instance.countDown = 10;
                    Controller.instance.ResetBtnEnabled();
                    Debug.Log("Color Switched");
                }
               
            }
           
            SwitchBGPosition();
        }
    }


    void SwitchBGPosition()
    {
        Vector2 pieceOfBgOffset = new Vector2(0f, pieceOfBgSize * 2f);
        transform.position = (Vector2)transform.position + pieceOfBgOffset;
    }

}
