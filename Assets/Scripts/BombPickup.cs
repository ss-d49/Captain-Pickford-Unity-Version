using UnityEngine;
using System.Collections;

public class BombPickup : MonoBehaviour
{
    private Animator anim;              // Reference to the animator component.
    private bool landed = false;        // Whether or not the crate has landed yet.


    void Start()
    {
        anim = transform.root.GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
			ExtAudio.sounding.PlayOneShot(ExtAudio.bombPickup);
            Spawner.bombCount++;
            Destroy(transform.root.gameObject);
        }
        else if (other.tag == "ground" && !landed)
        {
            anim.SetTrigger("Land");
            transform.parent = null;
            gameObject.AddComponent<Rigidbody2D>();
            landed = true;
        }
    }
}