using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class move : MonoBehaviour
{
    public float speed;
    public float points;
    public Text countText;
    public Text bola1Text;
    public Text bola2Text;
    public Text bola3Text;
    public Text bola4Text;
    public Text bonusText;

    //timer 

    public float myCoolTimer = 99;
    public Text timerText;


    //rb
    private Rigidbody rb;
    private float count, bola1, bola2, bola3, bola4;


    // teste de cameras
    public Camera main;
    public Camera zoom;
    //teste de particulas

    public ParticleSystem partfast;
    public bool includeChildren = true;

    //color
    public Color personagem = new Color(221f, 78f, 147f, 255f);
    //material para o personagem

    Material pwrfast;

    //teste combo

    public float bonuspoints;
    public float total;

    // Use this for initialization
    void Start()
    {


        rb = GetComponent<Rigidbody>();
        count = 0;
        bola1 = 0;
        bola2 = 0;
        bola3 = 0;
        bola4 = 0;
        SetCountText();
        try
        {
            countText.text = "Count: " + count.ToString();
        }
        catch (NullReferenceException ex)
        {
            Debug.Log("error");
        }
        //tentativa de mudar o material

        pwrfast = GetComponent<Renderer>().material;
        print("Materials " + Resources.FindObjectsOfTypeAll(typeof(Material)).Length);
        //testando o funcionamento da particula 
        partfast.Pause(includeChildren);
        main.enabled = true;
        zoom.enabled = false;


        //timer
        timerText = GetComponent<Text>();


    }

    // Update is called once per frame
    void Update()
    {



        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
        //xunxo

        if (Input.GetKeyUp(KeyCode.A))
        {

            rb.velocity = Vector3.zero;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {

            rb.velocity = Vector3.zero;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {

            rb.velocity = Vector3.zero;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {

            rb.velocity = Vector3.zero;
        }

        //para burro


        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {

            rb.velocity = Vector3.zero;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {

            rb.velocity = Vector3.zero;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {

            rb.velocity = Vector3.zero;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {

            rb.velocity = Vector3.zero;
        }
        //teste tempo

        try
        {
            myCoolTimer -= Time.deltaTime;
            timerText.text = myCoolTimer.ToString("f0");
            print(myCoolTimer);
        }
        catch (NullReferenceException)
        {

        }


        if (myCoolTimer <= 0)
        {
            total = (count * bonuspoints);

            if (total > 332)
            {
                endscene3();
            }
            else if ((total > 82) && (total <= 332))
            {
                endscene2();
            }
            else if (total <= 82)
            {
                endscene1();
            }


        }

        //sistema de combo 
        //resetacombo(resetcombo);

    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "FRUTA")
        {
            //identificar colisao
            Debug.Log("Do something here");
            //somar variavel
            count += 0.5f;
            bola1 += 0.5f;

            SetCountText();


            //destruir objeto
            Destroy(collision.gameObject);


        }
        if (collision.gameObject.tag == "FRUTA 2")
        {
            //identificar colisao
            Debug.Log("Do something here");
            //somar variavel
            count += 0.5f;
            bola2 += 0.5f;
            SetCountText();


            //destruir objeto
            Destroy(collision.gameObject);

        }
        if (collision.gameObject.tag == "FRUTA 3")
        {
            //identificar colisao
            Debug.Log("Do something here");
            //somar variavel
            count += 0.5f;
            bola3 += 0.5f;
            SetCountText();


            //destruir objeto
            Destroy(collision.gameObject);

        }
        if (collision.gameObject.tag == "FRUTA 4")
        {
            //identificar colisao
            Debug.Log("Do something here");
            //somar variavel
            count += 0.5f;
            bola4 += 0.5f;
            SetCountText();


            //destruir objeto
            Destroy(collision.gameObject);

        }
        if (collision.gameObject.tag == "FAST")
        {
            //identificar colisao
            Debug.Log("Do something here");

            StartCoroutine(PwrFast());

            //destruir objeto
            Destroy(collision.gameObject);


        }
        if (collision.gameObject.tag == "ZOOM")
        {
            //identificar colisao
            Debug.Log("Do something here");

            StartCoroutine(CamZoom());

            //destruir objeto
            Destroy(collision.gameObject);


        }
        if (collision.gameObject.tag == "BONUS")
        {
            //identificar colisao
            Debug.Log("Do something here");

            StartCoroutine(bonus());

            SetCountText();
            //destruir objeto
            Destroy(collision.gameObject);


        }


    }

    void SetCountText()
    {
        float cbola1, cbola2, cbola3, cbola4;
        //////CORRIGIR A QUANTIDADE DE BOLAS TOTAIS DE CADA COR/////
        countText.text = "Count: " + count.ToString();

        cbola1 = 26 - bola1;
        bola1Text.text = "Falta Vermelho: " + cbola1.ToString();

        cbola2 = 21 - bola2;
        bola2Text.text = "Falta Amarelo: " + cbola2.ToString();

        cbola3 = 37 - bola3;
        bola3Text.text = "Falta Roxa: " + cbola3.ToString();

        cbola4 = 40 - bola4;
        bola4Text.text = "Falta Azul: " + cbola4.ToString();

        bonusText.text = "Bonus: x " + bonuspoints.ToString();

    }

    IEnumerator PwrFast()
    {
        float fast;
        fast = 30;
        speed = fast;
        pwrfast.color = Color.yellow;

        partfast.Play(includeChildren);

        yield return new WaitForSeconds(10f);
        pwrfast.color = personagem;
        partfast.Stop(includeChildren, ParticleSystemStopBehavior.StopEmittingAndClear);
        pwrfast.color = personagem;
        speed = 10;



    }
    IEnumerator CamZoom()
    {
        main.enabled = false;
        zoom.enabled = true;
        yield return new WaitForSeconds(10f);
        main.enabled = true;
        zoom.enabled = false;
    }

    void endscene1()
    {
        SceneManager.LoadScene("1Estrela");


    }

    void endscene2()
    {
        SceneManager.LoadScene("2Estrelas");


    }
    void endscene3()
    {
        SceneManager.LoadScene("3Estrelas");


    }


    IEnumerator bonus()
    {
        bonuspoints += 0.5f;

        yield return new WaitForSeconds(1f);



    }



    // IEnumerator resetacombo()
    //{

    //    if (resetcombo)
    //    {
    //        teste = true;
    //        yield return new WaitForSeconds(1f);
    //        Debug.Log("1");
    //        yield return new WaitForSeconds(1f);
    //        Debug.Log("2");
    //        yield return new WaitForSeconds(1f);
    //        Debug.Log("3");
    //        pontos = 0;
    //        Debug.Log("QUEBROU!!!");
    //        resetcombo = false;
    //            }
    //        } // fim timer divisivel por 2
}