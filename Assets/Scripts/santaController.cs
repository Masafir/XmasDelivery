using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class santaController : MonoBehaviour
{
    // Animations du perso
    Animator animator;
    private bool walk = false;
    private bool idle = true;
    private bool run = false;
 

    // Vitesse de déplacement
    public float walkSpeed;
    public float runSpeed;
    public float turnSpeed;

    // Game objects
    GameObject bag;
    GameObject backSit;
    Transform collectibleTransform;
    Transform[] collectibles;

    // Inputs
    public string inputFront;
    public string inputBack;
    public string inputLeft;
    public string inputRight;
    //public string space;




    //public Vector3 jumpSpeed;
    CapsuleCollider playerCollider;




    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        playerCollider = gameObject.GetComponent<CapsuleCollider>();
        bag = GameObject.FindGameObjectWithTag("Bag");
    }



    bool IsGrounded()
    {
        return Physics.CheckCapsule(playerCollider.bounds.center, new Vector3(playerCollider.bounds.center.x,
            playerCollider.bounds.min.y - 0.1f, playerCollider.bounds.center.z), 0.08f);
    }

    private void OnTriggerEnter(Collider other)
    {
        print("triggerA");
        if(other.tag == "Collectibles" && bag.GetComponentsInChildren<Transform>().Length < 6)
        {
            runSpeed -= runSpeed * 0.01f;
            walkSpeed -= walkSpeed * 0.01f; 
            collectibleTransform = other.transform;
            collectibleTransform.transform.parent = bag.transform;
            collectibleTransform.transform.position = bag.transform.position;
            print(bag.transform.childCount);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("collision with  : " + collision.gameObject.tag);
    }
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Car")
        {
            collectibles = bag.GetComponentsInChildren<Transform>();
            backSit = GameObject.FindGameObjectWithTag("BackSit");
            if(collectibles.Length > 0)
            {
                int index = 0;
                foreach(Transform collectible in collectibles)
                {
                    index++;
                    if (index > 1)
                    {
                        collectibleTransform = collectible;
                        collectibleTransform.transform.parent = backSit.transform;
                        collectibleTransform.transform.position = new Vector3(backSit.transform.position.x, backSit.transform.position.y + (collectible.localScale.y / 3) * index, backSit.transform.position.z);
                    }
                    
                }
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        
    }
    void Update()
    {



        // si on avance
        if (Input.GetKey(inputFront) && !Input.GetKey(KeyCode.LeftShift))
        {
            transform.Translate(0, 0, walkSpeed * Time.deltaTime);
            if (!animator.GetBool("walk") && !walk)
            {
                //print("move forward : " + walk);
                animator.SetBool("walk", true);
                animator.SetBool("idle", false);
                walk = true;
                idle = false;
            }
        }



        // Si on sprint
        if (Input.GetKey(inputFront) && Input.GetKey(KeyCode.LeftShift))
        {
            transform.Translate(0, 0, runSpeed * Time.deltaTime);
            //animations.Play("run");
            if (!animator.GetBool("run") && !run)
            {
                //print("move forward : " + walk);
                animator.SetBool("run", true);
                animator.SetBool("walk", false);
                animator.SetBool("idle", false);
                run = true;
                walk = false;
                idle = false;
            }
        }



        // si on recule
        if (Input.GetKey(inputBack))
        {
            transform.Translate(0, 0, -(walkSpeed / 2) * Time.deltaTime);
            if (!animator.GetBool("walk") && !walk)
            {
                //print("move forward : " + walk);
                animator.SetBool("walk", true);
                animator.SetBool("idle", false);
                //animator.SetBool("walk", false);
                idle = false;
                walk = true;
            }

        }



        // rotation à gauche 
        if (Input.GetKey(inputLeft))
        {
            transform.Rotate(0, -turnSpeed * Time.deltaTime, 0);
        }



        // rotation à droite 
        if (Input.GetKey(inputRight))
        {
            transform.Rotate(0, turnSpeed * Time.deltaTime, 0);
        }



        // Si on avance pas et que on recule pas non plus
        if (!Input.GetKey(inputFront) && !Input.GetKey(inputBack) && (walk||run) && !idle)
        {
            //print("move forward : " + idle + walk);
            animator.SetBool("run", false);
            animator.SetBool("walk", false);
            animator.SetBool("idle", true);
            idle = true;
            walk = false;
            run = false;
        }



        // Si on saute
        /*if (Input.GetKey(space) && IsGrounded())
        {
            // Préparation du saut (Nécessaire en C#)
            Vector3 v = gameObject.GetComponent<Rigidbody>().velocity;
            v.y = jumpSpeed.y;



            // Saut
            gameObject.GetComponent<Rigidbody>().velocity = jumpSpeed;
        }*/
    }
}
