﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sturmgewehr46 : MonoBehaviour
{




    public GameObject recyle_particles_performance;
    public GameObject shoot_handle;

    public GameObject sight_a;
    public GameObject sight_b;





   public void Start()
    {
        // if starting with this gun, getting the equipment updated
        shoot_handle = player.GetComponent<polygon_fps_controller>().shoot_handle;
        _3rd_view = player.GetComponent<polygon_fps_controller>()._3rd;
        change_equipment();




        // 2 different shooting points / none suppressor and with
        if (suppressor_a_bool || suppressor_c_bool || suppressor_d_bool)
        {

            Shoot_start_point.transform.localPosition = shoot_origin_suppressor;
        }
        if (!suppressor_a_bool && !suppressor_c_bool && !suppressor_d_bool)
        {

            Shoot_start_point.transform.localPosition = shoot_origin_none_suppressor;
        }


        recyle_particles_performance = GameObject.FindGameObjectWithTag("rycle");



        sight_a.SetActive(true);
        sight_b.SetActive(true);


    }

    private void OnEnable()
    {
        in_reload = false;
        in_shoot = false;
    }



    float Power_bolt;
    public GameObject player;

    bool running;
    bool walking;
    bool walking_side;
    bool duck_walk;
    bool reload;

    public bool _3rd_view;
    public Vector3 _3rd_view_cam;

    void FixedUpdate()
    {
        // running key input checking
        Input_Status();

        // getting stats from the main player, if walking as example, that we don't need to check here, if the player is walking, just need to reading out the main player
        running = player.GetComponent<polygon_fps_controller>().running;
        walking = player.GetComponent<polygon_fps_controller>().walking;
        walking_side = player.GetComponent<polygon_fps_controller>().walking_side;
        duck_walk = player.GetComponent<polygon_fps_controller>().duck_walk;
        reload = player.GetComponent<polygon_fps_controller>().reload;
        cam_toggled = player.GetComponent<polygon_fps_controller>().cam_toggled;

        // Push force against shoot animation, force get added, if shoot to play the animation forward


        // each shoot add 1 to power_bolt, which is the animatin position for the shooting, shooting means
        // that the gun animation play forward to the maximal determined point and this -0.1f presses it back
        // to the idle output point
        if (Power_bolt > -1)
        {
            Power_bolt -= 0.1f;

        }
        // getting sure, that the backpress playing of the animation/ shooting not getting faster than -1
        if (Power_bolt < -1)
        {
            Power_bolt = -1;
        }

        // changing the gun animation playing time  ->  force getting added at shooting and plays until the maximal 
        //recoil and pressed down by -0.1 by each process of the update function

        ani.SetFloat("Power_bolt", Power_bolt);







        // In shooting with a magazine upper than 0 bullets  / not able while reloading and running
        if (button_shoot && !in_shoot && magazine_current > 0 && !in_reload && !running)
        {
            in_shoot = true;
            finished_shoot = false;
            shooting = StartCoroutine(shoot());


        }
        // not in shooting,   recovering current_spread
        if (!button_shoot && finished_shoot)
        {
            in_shoot = false;
            current_spread = 0;
        }


        if (button_aim && !button_shoot && !in_reload)
        {
            // aim animation / animator
            ani.SetInteger("sturmgewehr46", 4);
        }
        if (!button_aim && !button_shoot && !in_reload)
        {
            // idle animation / animator
            ani.SetInteger("sturmgewehr46", 0);
        }

        // staying in aim position  :: for adjustments
        //   button_aim = true;
        // ani.SetInteger("assault57", 4);

        // walking states /  not accessable while reloading
        if (walking && !in_reload)
        {
            if (!button_aim)
            {
                // idle with walking
                ani.SetInteger("sturmgewehr46", 2);

                // if walking in ducking, will the speed for the animation decrease to 50 %
                if (duck_walk)
                {
                    ani.SetFloat("weapon_speed", 0.5f);
                }
                else
                {
                    ani.SetFloat("weapon_speed", 1);
                }
                if (walking_side)
                {
                    ani.SetFloat("weapon_speed", 0.8f);
                }
            }

            if (button_aim)
            {
                // aim with walking
                ani.SetInteger("sturmgewehr46", 6);

                // if walking in ducking, will the speed for the animation decrease to 50 %
                if (duck_walk)
                {
                    ani.SetFloat("weapon_speed", 0.5f);
                }
                else
                {
                    ani.SetFloat("weapon_speed", 1);
                }
            }
        }


        // running animation at animator
        if (running && !in_reload)
        {

            ani.SetInteger("sturmgewehr46", 8);



        }


        // aim shoot animation at animator   // blocked while reloading and with a empty magazine
        if (button_aim && button_shoot && !running && !in_reload && magazine_current > 0)
        {
            ani.SetInteger("sturmgewehr46", 7);
        }
        // idle shoot animation at animator   // blocked while reloading and with a empty magazine
        if (!button_aim && button_shoot && !running && !in_reload && magazine_current > 0)
        {
            ani.SetInteger("sturmgewehr46", 1);
        }

        // execute reloading
        if (reload && !in_reload && stored_bullets > 0 && magazine_current != magazine_max)
        {
            reloading = StartCoroutine(reload_start());

        }



        cam_equipment();
    }


    bool button_shoot;
    bool button_aim;
    bool key_reload;
    bool cam_toggled;


    public void Input_Status()
    {

        // Extra void to get the controll clear and to make it more clean and comfortable to work with


        if (Input.GetButton("Fire1"))
        {
            button_shoot = true;
        }
        else
        {
            button_shoot = false;
        }

        if (cam_toggled)
        {
            _3rd_view = true;
            player.GetComponent<polygon_fps_controller>()._3rd = false;
            player.GetComponent<polygon_fps_controller>().head_3rd_status();
        }
        else
        {
            _3rd_view = false;
            player.GetComponent<polygon_fps_controller>()._3rd = true;
            player.GetComponent<polygon_fps_controller>().head_3rd_status();
        }


        if (Input.GetButton("Fire2"))
        {
            button_aim = true;
        }
        else
        {
            button_aim = false;
        }


        if (Input.GetKey(KeyCode.R))
        {
            key_reload = true;
        }
        else
        {
            key_reload = false;
        }

        // moving cam to 3rd position

        if (_3rd_view)
        {
            Cam.transform.localPosition = Vector3.Lerp(Cam.transform.localPosition, _3rd_view_cam, 15 * Time.deltaTime);

        }
    }





    public GameObject Shoot_start_point;
    public GameObject muzzle;


    float Push_bolt;



    bool in_shoot;
    Coroutine shooting;
    public bool full_auto;
    public float firerate;
    bool finished_shoot;


    public bool silence;
    public AudioClip silence_shoot_sound;
    public AudioClip shoot_sound;



    public int Joule;




    public float vertical_force;
    public float horizontal_force;

    public float spread_height;
    public float current_spread;









    public IEnumerator shoot()
    {

        yield return new WaitForSeconds(0);


        // The bullet can pass unlimted glass and a serveral amount of flesh and will stoped by hard ground as wood and stone  /  Here it gets refreshed





        // Increasing spread at automatic fire / reset if not fired                 Suppressor -> 33 % less spread
        current_spread += spread_height;

        Vector3 Add_spread = Shoot_start_point.transform.forward;

        float hor = Random.Range(-current_spread, current_spread);
        float ver = Random.Range(-current_spread, current_spread);


        // merge of horizontal and vertical spread to add it to the shoot rotation
        Add_spread = new Vector3(hor, ver, 0);




        // The Shoot animation isn't looped, if you shoot will a positive time get setted on the animtor/animation/shooting -> Power_bolt
        Power_bolt = 1;


        // dropping bullet shell
        bullet_drop();


        // addind recoil on aimming -> movement of Polygon_fps_controller 

        float add_ver_force = UnityEngine.Random.Range(0, vertical_force);
        float add_hor_force = UnityEngine.Random.Range(-horizontal_force, horizontal_force);

        // only if there isn't a suppressor on / spread won't get reduce here
        if (!suppressor_a_bool && !suppressor_c_bool && !suppressor_d_bool)
        {
            player.GetComponent<polygon_fps_controller>().vertical_float_spread = -add_ver_force;
            player.GetComponent<polygon_fps_controller>().horizontal_float_spread = add_hor_force;
        }


        //  with suppressor 33% less movement adding for shooting for the player aimming camera on the polygon_fps_controller script
        if (suppressor_a_bool || suppressor_c_bool || suppressor_d_bool)
        {
            player.GetComponent<polygon_fps_controller>().vertical_float_spread = -add_ver_force / 3;
            player.GetComponent<polygon_fps_controller>().horizontal_float_spread = add_hor_force / 3;
        }


        // starting muzzle for shooting
        GameObject spawned_muzzle = Instantiate(muzzle, Shoot_start_point.transform.position, Shoot_start_point.transform.rotation);
        spawned_muzzle.GetComponent<muzzle_flash>().origin = Shoot_start_point;


        // adding suppressed shoot sound or a normal one
        if (!suppressor_a_bool && !suppressor_c_bool && !suppressor_d_bool)
        {
            GameObject g = Instantiate(Clip_on_point, Shoot_start_point.transform.position, transform.rotation);
            g.GetComponent<AudioSource>().clip = shoot_sound;
            g.GetComponent<AudioSource>().maxDistance = 100;
            g.GetComponent<AudioSource>().Play();
            g.transform.parent = Shoot_start_point.transform;
        }
        if (suppressor_a_bool || suppressor_c_bool || suppressor_d_bool)
        {
            GameObject g = Instantiate(Clip_on_point, Shoot_start_point.transform.position, transform.rotation);
            g.GetComponent<AudioSource>().clip = silence_shoot_sound;
            g.GetComponent<AudioSource>().maxDistance = 50;
            g.GetComponent<AudioSource>().Play();
            g.transform.parent = Shoot_start_point.transform;

            // Suppressor decreases the spread to 33 %
            Add_spread -= (Add_spread / 3);
        }





        RaycastHit hit;

        // shooting forward at postion and rotation Shoot_start_point + adding the Spread

        // execute the shooting from an seperate script, to not slowing down the firerate
        shoot_handle.GetComponent<shoot_handle>().register_shoot(Cam.transform.position, Cam.transform.TransformDirection(Vector3.forward - Add_spread), Joule);

        // turning delay for shoot in semi shoot mode off for bnetter experience
        if (full_auto)
        {
            yield return new WaitForSeconds(firerate);
        }


        // minus for the magazine of the weapon here
        magazine_current -= 1;


        // check if magazine has a bullet left to repeat the shooting and if the trigger is pressed down and if fullauto is enabled
        if (magazine_current > 0 && full_auto && button_shoot)
        {
            StopCoroutine(shoot());
            shooting = StartCoroutine(shoot());
        }

        // break the shoot, because it is setted on semiauto
        if (!full_auto)
        {
            finished_shoot = true;
            StopCoroutine(shoot());
        }
        // no shoot button pressed
        if (!button_shoot)
        {
            finished_shoot = true;
            StopCoroutine(shoot());
        }

    }


    public int magazine_max;
    public int magazine_current;
    public int stored_bullets;
    public AudioClip reload_unempty;
    public AudioClip reload_empty;


    Coroutine reloading;
    bool in_reload;
    float reload_time;
    bool finished_reload_in_reload;

    public GameObject Clip_on_point;
    public IEnumerator reload_start()
    {

        // blocking other key input to interprut the reload and blocking shoot with turning on in_reload and finished_reload_in_reload
        in_reload = true;
        finished_reload_in_reload = false;


        // 2 different kind of reloading, if the magazine is empty and if not for different animations
        if (magazine_current > 0)
        {
            reload_time = 1.499f;
            ani.SetInteger("sturmgewehr46", 5); // unempty reload    - putting aduioclip on new spawned empty object and transform parent it to the point Shoot_start_point for a static sound

            GameObject g = Instantiate(Clip_on_point, Shoot_start_point.transform.position, transform.rotation);
            g.GetComponent<AudioSource>().clip = reload_unempty;
            g.GetComponent<AudioSource>().maxDistance = 15;
            g.GetComponent<AudioSource>().Play();
            g.transform.parent = Shoot_start_point.transform;
        }
        else
        {
            reload_time = 2;
            ani.SetInteger("sturmgewehr46", 3); // empty reload

            GameObject g = Instantiate(Clip_on_point, Shoot_start_point.transform.position, transform.rotation);
            g.GetComponent<AudioSource>().clip = reload_empty;
            g.GetComponent<AudioSource>().maxDistance = 15;
            g.GetComponent<AudioSource>().Play();
            g.transform.parent = Shoot_start_point.transform;
        }

        // reload time over
        yield return new WaitForSeconds(reload_time);


        // if there aren't enough bullet to fill the complet magazine, different reloading handle for bullets
        if (stored_bullets < magazine_max && !finished_reload_in_reload)
        {
            finished_reload_in_reload = true;
            stored_bullets += magazine_current;
            magazine_current = 0;
            magazine_current = stored_bullets;
            stored_bullets = 0;
        }

        // enough bullets in stored bullets just to fill the full magazine and if the bullets released, will the shooting get unlocked with finished_reload_in_reload
        if (stored_bullets > magazine_max || stored_bullets == magazine_max && !finished_reload_in_reload)
        {
            finished_reload_in_reload = true;
            stored_bullets += magazine_current;
            magazine_current = 0;
            magazine_current = magazine_max;
            stored_bullets -= magazine_max;
        }
        // releasing weapon for the update function, to get shooted again
        finished_shoot = true;
        in_reload = false;
        // stopping all runs for reloading
        StopCoroutine(reload_start());

    }



    public GameObject Cam;
    public Animator ani;


    // camera positions for different scopes and states as 3rd
    public Vector3 idle_cam;
    public Vector3 aim_cam;
    public Vector3 run_cam;


    public Vector3 red_dot_a_cam;
    public Vector3 red_dot_b_cam;
    public Vector3 red_dot_c_cam;
    public GameObject red_dot_c_cam_obj;
    public Vector3 red_dot_d_cam;
    public Vector3 red_dot_e_cam;
    public GameObject red_dot_e_cam_obj;

    public Vector3 scope_a_cam;
    public GameObject scope_a_cam_obj;
    public Vector3 scope_b_cam;
    public GameObject scope_b_cam_obj;
    public Vector3 scope_c_cam;
    public GameObject scope_c_cam_obj;




    public bool lamp_a_bool;
    public bool lamp_laser_a_bool;
    public bool lamp_laser_b_bool;
    public bool laser_a_bool;

    public bool red_dot_a_bool;
    public bool red_dot_b_bool;
    public bool red_dot_c_bool;
    public bool red_dot_d_bool;
    public bool red_dot_e_bool;

    public bool scope_a_bool;
    public bool scope_b_bool;
    public bool scope_c_bool;

    public bool suppressor_a_bool;
    public bool suppressor_c_bool;
    public bool suppressor_d_bool;



    public GameObject lamp_a;
    public GameObject lamp_laser_a;
    public GameObject lamp_laser_b;
    public GameObject laser_a;

    public GameObject red_dot_a;
    public GameObject red_dot_b;
    public GameObject red_dot_c;
    public GameObject red_dot_d;
    public GameObject red_dot_e;

    public GameObject scope_a;
    public GameObject scope_b;
    public GameObject scope_c;

    public GameObject suppressor_a;
    public GameObject suppressor_c;
    public GameObject suppressor_d;














    public Vector3 shoot_origin_none_suppressor;
    public Vector3 shoot_origin_suppressor;

    public void cam_equipment()
    {

        // changing cam postion for different situation as 3rd and running and different little postion at different scopes


        if (_3rd_view)
        {
            Cam.transform.localPosition = Vector3.Lerp(Cam.transform.localPosition, _3rd_view_cam, 15 * Time.deltaTime);

            return;
        }




        if (in_reload)
        {
            Cam.transform.localPosition = Vector3.Lerp(Cam.transform.localPosition, idle_cam, 15 * Time.deltaTime);
        }
        if (!button_aim && !running && !in_reload) //idle
        {

            Cam.transform.localPosition = Vector3.Lerp(Cam.transform.localPosition, idle_cam, 15 * Time.deltaTime);


        }
        if (button_aim && !running && !in_reload && !scope) //aim
        {
            Cam.transform.localPosition = Vector3.Lerp(Cam.transform.localPosition, aim_cam, 15 * Time.deltaTime);

        }
        if (running && !in_reload)
        {
            Cam.transform.localPosition = Vector3.Lerp(Cam.transform.localPosition, run_cam, 15 * Time.deltaTime);
        }




        // Cam position for different scopes/dotes

        if (red_dot_a_bool && !in_reload && button_aim)
        {
            Cam.transform.localPosition = Vector3.Lerp(Cam.transform.localPosition, red_dot_a_cam, 15 * Time.deltaTime);
        }
        if (red_dot_b_bool && !in_reload && button_aim)
        {
            Cam.transform.localPosition = Vector3.Lerp(Cam.transform.localPosition, red_dot_b_cam, 15 * Time.deltaTime);
        }
        if (red_dot_c_bool && !in_reload && button_aim)
        {
            Cam.transform.localPosition = Vector3.Lerp(Cam.transform.localPosition, red_dot_c_cam, 15 * Time.deltaTime);
        }
        if (red_dot_d_bool && !in_reload && button_aim)
        {
            Cam.transform.localPosition = Vector3.Lerp(Cam.transform.localPosition, red_dot_d_cam, 15 * Time.deltaTime);
        }
        if (red_dot_e_bool && !in_reload && button_aim)
        {
            Cam.transform.localPosition = Vector3.Lerp(Cam.transform.localPosition, red_dot_e_cam, 15 * Time.deltaTime);
        }



        if (scope_a_bool && !in_reload && button_aim)
        {
            Cam.transform.localPosition = Vector3.Lerp(Cam.transform.localPosition, scope_a_cam, 15 * Time.deltaTime);
        }
        if (scope_b_bool && !in_reload && button_aim)
        {
            Cam.transform.localPosition = Vector3.Lerp(Cam.transform.localPosition, scope_b_cam, 15 * Time.deltaTime);
        }
        if (scope_c_bool && !in_reload && button_aim)
        {
            Cam.transform.localPosition = Vector3.Lerp(Cam.transform.localPosition, scope_c_cam, 15 * Time.deltaTime);
        }







    }


    public bool scope;

    public void change_equipment()
    {

        // script to prepare the weapon, or if a scope has been changed to update the mesh status.

        // checking if you get a scope, if yes, will you use different cam positions for the scopes
        scope = false;







        // turning all of, to update the new status

        lamp_a.SetActive(false);
        lamp_laser_a.SetActive(false);
        lamp_laser_b.SetActive(false);
        laser_a.SetActive(false);

        red_dot_a.SetActive(false);
        red_dot_b.SetActive(false);
        red_dot_c.SetActive(false);
        red_dot_d.SetActive(false);
        red_dot_e.SetActive(false);

        scope_a.SetActive(false);
        scope_b.SetActive(false);
        scope_c.SetActive(false);

        suppressor_a.SetActive(false);
        suppressor_c.SetActive(false);
        suppressor_d.SetActive(false);


        // turning on the chose equipment





        if (lamp_a_bool)
        {
            lamp_a.SetActive(true);
            scope = false;
        }
        if (lamp_laser_a_bool)
        {
            lamp_laser_a.SetActive(true);
            scope = false;
        }
        if (lamp_laser_b_bool)
        {
            lamp_laser_b.SetActive(true);
            scope = false;
        }
        if (laser_a_bool)
        {
            laser_a.SetActive(true);
            scope = false;
        }




        if (suppressor_a_bool)
        {
            suppressor_a.SetActive(true);
            scope = false;
        }
        if (suppressor_c_bool)
        {
            suppressor_c.SetActive(true);
            scope = false;
        }
        if (suppressor_d_bool)
        {
            suppressor_d.SetActive(true);
            scope = false;
        }










        if (red_dot_a_bool)
        {
            red_dot_a.SetActive(true);
            scope = true;
        }
        if (red_dot_b_bool)
        {
            red_dot_b.SetActive(true);
            scope = true;
        }
        if (red_dot_c_bool)
        {
            red_dot_c_cam_obj.SetActive(true);
            red_dot_c.SetActive(true);
            scope = true;
        }
        if (red_dot_d_bool)
        {
            red_dot_d.SetActive(true);
            scope = true;
        }
        if (red_dot_e_bool)
        {
            red_dot_e_cam_obj.SetActive(true);
            red_dot_e.SetActive(true);
            scope = true;
        }




        if (scope_a_bool)
        {
            scope_a_cam_obj.SetActive(true);
            scope_a.SetActive(true);
            scope = true;
        }
        if (scope_b_bool)
        {
            scope_b_cam_obj.SetActive(true);
            scope_b.SetActive(true);
            scope = true;
        }
        if (scope_c_bool)
        {
            scope_c_cam_obj.SetActive(true);
            scope_c.SetActive(true);
            scope = true;
        }


        if(scope)
        {
            sight_a.transform.localEulerAngles = new Vector3(-188.458f, -90f, -1.525879e-05f);
            sight_b.transform.localEulerAngles = new Vector3(-182.742f, -90f, -1.525879e-05f);
        }


        // 2 different shooting points / none suppressor and with
        if (suppressor_a_bool || suppressor_c_bool || suppressor_d_bool)
        {

            Shoot_start_point.transform.localPosition = shoot_origin_suppressor;
        }
        if (!suppressor_a_bool && !suppressor_c_bool && !suppressor_d_bool)
        {

            Shoot_start_point.transform.localPosition = shoot_origin_none_suppressor;
        }





    }



    public void turn_off_weapon()
    {

        // reset all, also the animation to -1, that none animations from an different gun, runs into the animation/with the current one, or the result would be awful and not wished

        ani.SetInteger("sturmgewehr46", -1);

        // turning off all scopes && equipment, sights
        lamp_a.SetActive(false);
        lamp_laser_a.SetActive(false);
        lamp_laser_b.SetActive(false);
        laser_a.SetActive(false);

        red_dot_a.SetActive(false);
        red_dot_b.SetActive(false);
        red_dot_c.SetActive(false);
        red_dot_d.SetActive(false);
        red_dot_e.SetActive(false);

        scope_a.SetActive(false);
        scope_b.SetActive(false);
        scope_c.SetActive(false);

        suppressor_a.SetActive(false);
        suppressor_c.SetActive(false);
        suppressor_d.SetActive(false);


        sight_a.SetActive(false);
        sight_b.SetActive(false);

        red_dot_c_cam_obj.SetActive(false);
        red_dot_e_cam_obj.SetActive(false);
        scope_a_cam_obj.SetActive(false);
        scope_b_cam_obj.SetActive(false);
        scope_c_cam_obj.SetActive(false);

    }


    public GameObject bullet_shell;
    public GameObject Bullet_shell_output;
    public Vector3 Bullet_shell_output_position;

    public void bullet_drop()
    {

        bullet_shell.transform.localPosition = Bullet_shell_output_position;

        GameObject b = Instantiate(bullet_shell, Bullet_shell_output.transform.position, Bullet_shell_output.transform.rotation);

        b.GetComponent<Rigidbody>().AddForce(Bullet_shell_output.transform.right + Bullet_shell_output.transform.up, ForceMode.Impulse);


    }











}