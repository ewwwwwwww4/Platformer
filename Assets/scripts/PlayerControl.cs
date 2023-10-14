using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerControl : MonoBehaviour
{
    float horizontalMove;
    public float speed = 2f;

    Rigidbody2D myBody;

    bool grounded = false;

    public float castDist = 1f;
    public float jumpPower = 2f;
    public float gravityScale = 5f;
    public float gravityFall = 40f;


    //public Transform bulletSpawnPoint;
    //public GameObject bullerPrefab;
  //  public float bullerSpeed = 10;

    //private bool enterAllowed;
    //private string sceneToLoad;
    public string targetSceneName = "the end";

    public string targetSceneName2 = "Lv.2";

    public string targetSceneName3 = "win";


    private int life = 3;

    private Vector3 respawnPoint;

    bool jump = false;
    bool doubleJump;

    Animator myAnim;
    SpriteRenderer myRend;

    public GameObject life01, life02, life03;

    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myRend = GetComponent<SpriteRenderer>();
        respawnPoint = transform.position;
    }


    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");

      //  if (Input.GetKeyDown(KeyCode.Return))  {
        //    var bullet = Instantiate(bullerPrefab,bulletSpawnPoint.position,bulletSpawnPoint.rotation);
          //  bullet.GetComponent<Rigidbody2D>().velocity = bulletSpawnPoint.up * bullerSpeed;  }

        if (grounded && !Input.GetButton("Jump"))
        {
            doubleJump = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (grounded || doubleJump)
            {
                myAnim.SetBool("jumping", true);
                jump = true;
                doubleJump = !doubleJump;
            }
        }


        if (horizontalMove > 0.2f)
        {
            myAnim.SetBool("running", true);
            myRend.flipX = false;
        }
        else if (horizontalMove < -0.2f)
        {
            myAnim.SetBool("running", true);
            myRend.flipX = true;
        }
        else
        {
            myAnim.SetBool("running", false);
        }
    }

    void FixedUpdate()
    {
        float moveSpeed = horizontalMove * speed;


        if (jump)
        {
            myBody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            jump = false;
        }


        if (myBody.velocity.y >= 0)
        {
            myBody.gravityScale = gravityScale;
        }
        else if (myBody.velocity.y < 0)
        {
            myBody.gravityScale = gravityFall;
        }


        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, castDist);

        Debug.DrawRay(transform.position, Vector2.down * castDist, Color.red);


        if (hit.collider != null && hit.transform.name == "Ground")
        {
            myAnim.SetBool("jumping", false);
            grounded = true;
        }
        else
        {
            grounded = false;
        }

        myBody.velocity = new Vector3(moveSpeed, myBody.velocity.y, 0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Trap")
        {
            if (transform.position.y > collision.gameObject.transform.position.y)
            {
                life--;
                Life();
            }
        }


        if (collision.gameObject.tag == "Door1")
        {
            SceneManager.LoadScene(targetSceneName2);
        }

        if (collision.gameObject.tag == "Door2")
        {
            SceneManager.LoadScene(targetSceneName3);
        }


        void Life()
        {
            if (life == 3)
            {
                life03.SetActive(true);
                life02.SetActive(true);
                life01.SetActive(true);
            }
            if (life == 2)
            {
                life03.SetActive(false);
                life02.SetActive(true);
                life01.SetActive(true);
            }
            if (life == 1)
            {
                life03.SetActive(false);
                life02.SetActive(false);
                life01.SetActive(true);
            }
            if (life < 1)
            {
                life03.SetActive(false);
                life02.SetActive(false);
                life01.SetActive(false);
                //sceneToLoad = "the end";
                // enterAllowed = true;
                SceneManager.LoadScene(targetSceneName);
            }
        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "DDL")
        {
            //SceneManager.LoadScene(targetSceneName);
            transform.position = respawnPoint;
            life--;
            Life();
        }
        else if (collision.tag == "checkpoint")
        {
            respawnPoint = transform.position;
        }
        void Life()
        {
            if (life == 3)
            {
                life03.SetActive(true);
                life02.SetActive(true);
                life01.SetActive(true);
            }
            if (life == 2)
            {
                life03.SetActive(false);
                life02.SetActive(true);
                life01.SetActive(true);
            }
            if (life == 1)
            {
                life03.SetActive(false);
                life02.SetActive(false);
                life01.SetActive(true);
            }
            if (life < 1)
            {
                life03.SetActive(false);
                life02.SetActive(false);
                life01.SetActive(false);
                //sceneToLoad = "the end";
                // enterAllowed = true;
                SceneManager.LoadScene(targetSceneName);
            }

        }
    }
}

