﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket_script : MonoBehaviour
{


    public GameObject active_OBJS;
    public int dmg;
    public GameObject explosion;
    public int speed;



    public void Start()
    {
        active_OBJS = GameObject.Find("props_active");
    }


    public Transform aim;
    public bool rocket_ready;
    public GameObject particle;


    void Update()
    {
        if (rocket_ready)
        {
            transform.Translate(Vector3.forward*speed*Time.deltaTime);


            var q = Quaternion.LookRotation(aim.position - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 35 * Time.deltaTime);

            active_rocket();

        }
    }


    bool only_once;
    public void active_rocket()
    {
        if(!only_once)
        {
            only_once = true;

            GetComponent<AudioSource>().Play();
            particle.SetActive(true);

            transform.parent = null;


        }
    }




    private void OnTriggerStay(Collider collider)
    {

        if(!rocket_ready)
        {
            return;
        }

        if (collider.tag != "Untagged")
        {



            foreach (GameObject gg in active_OBJS.GetComponent<find_destory_able_props>().objs_2)
            {
                if (gg != null)
                {

                    if (Vector3.Distance(transform.position, gg.transform.position) < 6)
                    {


                        if (gg.GetComponent<destroy_object>())
                        {
                            gg.GetComponent<destroy_object>().Input_damage(dmg, false);
                        }

                        gg.GetComponent<Rigidbody>().AddExplosionForce(2500, transform.position, 4);

                    }
                }


            }
            foreach (GameObject gg in active_OBJS.GetComponent<find_destory_able_props>().objs_3)
            {
                if (gg != null)
                {

                    if (Vector3.Distance(transform.position, gg.transform.position) < 6)
                    {


                        if (gg.GetComponent<destroy_object>())
                        {
                            gg.GetComponent<destroy_object>().Input_damage(dmg, false);
                        }

                        gg.GetComponent<Rigidbody>().AddExplosionForce(2500, transform.position, 4);

                    }

                }
            }
            foreach (GameObject gg in active_OBJS.GetComponent<find_destory_able_props>().objs_4)
            {

                if (gg != null)
                {

                    if (Vector3.Distance(transform.position, gg.transform.position) < 6)
                    {


                        if (gg.GetComponent<destory_simple>())
                        {
                            gg.GetComponent<destory_simple>().Destroyy();
                        }

                        gg.AddComponent<Rigidbody>();
                        gg.GetComponent<Rigidbody>().AddExplosionForce(2500, transform.position, 4);

                    }


                }
            }
            foreach (GameObject gg in active_OBJS.GetComponent<find_destory_able_props>().objs_5)
            {

                if (gg != null)
                {

                    if (Vector3.Distance(transform.position, gg.transform.position) < 6)
                    {

                        if (gg.GetComponent<destory_simple>())
                        {
                            if (gg.GetComponent<destory_simple>())
                            {
                                gg.GetComponent<destory_simple>().Destroyy();
                            }

                            if (gg.GetComponent<Rigidbody>())
                            {
                                gg.GetComponent<Rigidbody>().AddExplosionForce(2500, transform.position, 3);
                            }
                        }
                    }
                }

            }
            foreach (GameObject gg in active_OBJS.GetComponent<find_destory_able_props>().objs_6)
            {
                if (gg != null)
                {

                    if (Vector3.Distance(transform.position, gg.transform.position) < 6)
                    {


                        if (gg.GetComponent<petrol_can>())
                        {
                            gg.GetComponent<petrol_can>().explosion_on();
                        }



                    }

                }
            }
            foreach (Transform gg in active_OBJS.GetComponent<find_destory_able_props>().objs_7)
            {
                if (gg != null)
                {

                    if (Vector3.Distance(transform.position, gg.transform.position) < 8)
                    {


                        if (gg.GetComponent<bunny_receive_dmg>())
                        {
                            gg.GetComponent<bunny_receive_dmg>().take_dmg(dmg);
                        }



                    }

                }
            }





            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }




    }







}