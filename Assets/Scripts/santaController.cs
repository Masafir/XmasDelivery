﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class santaController : MonoBehaviour
{
    // Animations du perso
    Animator animator;
    private bool walk = false;
    private bool idle = true;
    private bool run = false;
    private bool fly = false;
 

    // Vitesse de déplacement
    public float walkSpeed;
    public float runSpeed;
    public float turnSpeed;
    private float lightSpeed;

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
        lightSpeed = runSpeed * 5;
    }



    bool IsGrounded()
    {
        return Physics.CheckCapsule(playerCollider.bounds.center, new Vector3(playerCollider.bounds.center.x,
            playerCollider.bounds.min.y - 0.1f, playerCollider.bounds.center.z), 0.08f);
    }

    private void OnTriggerEnter(Collider other)
    {
        int deliver = other.transform.Find("Delivery") != null ? other.transform.Find("Delivery").childCount : 1995;
        //print("triggerA : " + other.tag + " | " + deliver + " | " + bag.transform.childCount);

        if(other.tag == "Collectibles" && bag.GetComponentsInChildren<Transform>().Length < 6 && other.transform.parent.tag != "Delivery")
        {
            runSpeed -= runSpeed * 0.01f;
            walkSpeed -= walkSpeed * 0.01f; 
            collectibleTransform = other.transform;
            collectibleTransform.transform.parent = bag.transform;
            collectibleTransform.transform.position = bag.transform.position;
            //print(bag.transform.childCount);
        }
        
        if(other.tag == "DeliveryPlate" && other.transform.Find("Delivery").childCount < 1 && bag.transform.childCount > 0)
        {
            //print("Delivery empty");
            collectibles = bag.GetComponentsInChildren<Transform>();
            Transform present = collectibles[1];
            Transform delivery = other.transform.Find("Delivery");
            present.transform.parent = delivery;
            present.transform.position = delivery.transform.position;
            
        }
        if(other.tag == "Car")
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        print("collision with  : " + collision.gameObject.tag);
    }*/
    /* private void OnCollisionStay(Collision collision)
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
    }*/
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
                animator.SetBool("run", false);
                animator.SetBool("fly", false);
                animator.SetBool("idle", false);
                walk = true;
                idle = false;
                fly = false;
            }
        }



        // Si on sprint
        if (Input.GetKey(inputFront) && Input.GetKey(KeyCode.LeftShift))
        {
            transform.Translate(0, 0, runSpeed * Time.deltaTime);
            //animations.Play("run");
            if (!animator.GetBool("run") && !fly)
            {
                //print("move forward : " + walk);
                animator.SetBool("run", true);
                animator.SetBool("fly", false);
                animator.SetBool("walk", false);
                animator.SetBool("idle", false);
                run = true;
                fly = false;
                walk = false;
                idle = false;
            }
        }

        if(Input.GetKey(inputFront) && Input.GetKey(KeyCode.Space))
        {
            transform.Translate(0, 0, lightSpeed * Time.deltaTime);
            if (!animator.GetBool("fly") && !fly)
            {
                //print("move forward : " + walk);
                animator.SetBool("fly", true);
                animator.SetBool("run", false);
                animator.SetBool("walk", false);
                animator.SetBool("idle", false);
                fly = true;
                run = false;
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
                animator.SetBool("fly", false);
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
