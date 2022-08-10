using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private float speed = 8.0f;
    private float maxVelocty = 4.0f;
    private Vector3 scale;
    private int jumpforce = 250;
    private int ziplamasayisi = 2;
    private int skyforce = 250;
    private Rigidbody2D myRigidbody;
    private Animator myanim;

    bool kaymak = false;
    float kaymatimer = 0f;
    public float maxkaymatimer = 1.5f;
    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        scale = this.transform.localScale;
        myanim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {


        ziplama();
        kosma();
        airattack();
        attack();
        kayma();
    }

    private void kosma()
    {
        float force = 0.0f;
        float velocity = Mathf.Abs(myRigidbody.velocity.x);
        float h = Input.GetAxis("Horizontal");
        if (h > 0)
        {
            if (velocity < maxVelocty)
            {
                force = speed;
                scale.x = 1;
                this.transform.localScale = scale;
            }

        }
        else if (h < 0)
        {
            if (velocity < maxVelocty)
            {
                force = -speed;
                scale.x = -1;
                this.transform.localScale = scale;
            }


        }

        myRigidbody.AddForce(new Vector2(force, 0));
        myanim.SetFloat("speed", Mathf.Abs(h));
    }
    private void ziplama()
    {
        if (ziplamasayisi > 0)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                myRigidbody.AddForce(Vector2.up * jumpforce);
                myanim.Play("atlama");
                myanim.SetBool("zemindemi", false);


                ziplamasayisi--;
            }

        }

        Debug.Log(ziplamasayisi);
    }
    private void kayma()
    {
        if (Input.GetKeyDown(KeyCode.S) && !kaymak)
        {
            kaymatimer = 0f;
            myanim.SetBool("kayma", true);
            kaymak = true;
        }
        if (kaymak)
        {
            kaymatimer += Time.deltaTime;
            if (kaymatimer > maxkaymatimer)
            {
                kaymak = false;
                myanim.SetBool("kayma", false);
            }
        }
    }
    public void airattack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            myanim.SetBool("havasaldiri", true);
        }
        else
        {
            myanim.SetBool("havasaldiri", false);
        }
    }
    public void airattackanimation()
    {
        myanim.SetBool("havasaldiri", false);
    }
    public void attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            myanim.SetBool("saldiri", true);
        }
        else
        {
            myanim.SetBool("saldiri", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D hedef)
    {
        if (hedef.gameObject.tag == "Atilemap")
        {
            ziplamasayisi = 2;
            myanim.SetBool("zemindemi", true);
        }
    }
}
 