using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSwingScript : MonoBehaviour
{   
     // 追加
    private Vector3 screenPoint;
    private Vector3 offset;
    public GameObject framy;
    public GameObject Choices;
    GameObject BallToy;
    GameObject Oyatu;
    private bool IsDrag;
    bool GoToPlayer ;
    bool SelectTime;
    Vector3 PlayerPlace;
    AudioSource FramyAudio;
    public AudioClip FramyBadVoice;
    public AudioClip FramyGoodVoice;
    private bool pass;


    // Start is called before the first frame update
    void Start()
    {
        pass = false;
        //ボールの初期位置
        transform.position = new Vector3((float)6, (float)-4.3, -4);
        IsDrag = false;// ドラッグし始めたフラグ
        framy = GameObject.Find("Framy");
        GoToPlayer = false;//フレーミーがボールを拾った後プレーヤーに駆け寄る
        PlayerPlace = new Vector3(-0.8f, -4.6f,-4);
        SelectTime=false;
        Choices = GameObject.Find("Choices");
        Choices.SetActive(false);
        Oyatu = GameObject.Find("Oyatu");
        Oyatu.SetActive(false);
        FramyAudio = GetComponent<AudioSource>();
        BallToy = GameObject.Find("BallToy");


        //GameObject ball = GameObject.Find("BallToy");
    }

    // Update is called once per frame
    void Update()
    {
            Vector3 end = new Vector3((float)-5.6, (float)-2.9, -4);
            Vector3 ThrowPoint = new Vector3((float)5, (float)-3.3f, -4);
        if (IsDrag)
        {
            Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + this.offset;
            float tend = 0.008f;

            //if(Vector3.SqrMagnitude(transform.position - currentPosition)>0)
            if (currentPosition.x < transform.position.x || currentPosition.y > transform.position.y)
            {
                if (Vector3.SqrMagnitude(transform.position - ThrowPoint) < 1)
                {
                    IsDrag = false;
                    StartThrow(4f, ThrowPoint, end, (float)40);
                }
                else
                {
                    Vector3 tmp = new Vector3(transform.position.x + -tend * Vector3.SqrMagnitude(transform.position - currentPosition), transform.position.y + tend * Vector3.SqrMagnitude(transform.position - currentPosition), transform.position.z);
                    if (tmp.x < ThrowPoint.x || tmp.y > ThrowPoint.y)
                    {
                        //transform.position = ThrowPoint;
                    }
                    else
                    {
                        transform.position = tmp;
                    }
                }

            }
        }
        else
        {



            if (GoToPlayer)
            {
                if(Vector3.SqrMagnitude(transform.position - PlayerPlace) < 0.0001)
                {
                    SelectTime = true;

                }
                framy.transform.position = Vector3.MoveTowards(framy.transform.position, new Vector3(PlayerPlace.x + 1.5f, PlayerPlace.y + 0.5f,PlayerPlace.z), 0.2f);
                transform.position = Vector3.MoveTowards(transform.position, PlayerPlace, 0.2f);

            }
            else
            {
                if (Vector3.SqrMagnitude(transform.position - end) < 0.01)
                {
                    framy.transform.position = Vector3.MoveTowards(framy.transform.position, new Vector3(transform.position.x + 1.5f, transform.position.y + 0.5f,transform.position.z), 0.02f);
                    if (Vector3.SqrMagnitude(framy.transform.position - new Vector3(transform.position.x + 1.5f, transform.position.y + 0.5f,transform.position.z)) < 0.01)
                    {
                        GoToPlayer = true;
                    }
                }
            }
            if (SelectTime&&!pass)
            {
                Choices.SetActive(true);
                SelectTime = false;
                pass = true;
            }
            /*
            if ()
            {
        Choices.SetActive(false);

            }
            if(){
        Choices.SetActive(false);
            }
            */
        }

    }
    // 追加
    void OnMouseDown()
    {
        this.screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        this.offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }
    private void OnMouseUp()
    {
        IsDrag = false;
    }
    // 追加
    void OnMouseDrag()
    {
        IsDrag = true;

    }

    //ボールを投げた後の動き
    public void StartThrow( float height, Vector3 start, Vector3 end, float duration)
    {
        // 中点を求める
        Vector3 half = end - start * 0.50f + start;
        half.y += Vector3.up.y + height;

        StartCoroutine(LerpThrow( start, half, end, duration));
    }
    IEnumerator LerpThrow(Vector3 start, Vector3 half, Vector3 end, float duration)
    {
        float startTime = Time.timeSinceLevelLoad;
        float rate = 0.001f;
        while (true)
        {
            if (rate >= 1.0f)
            {
                transform.position = Vector3.MoveTowards(transform.position, end, 0.01f);
                yield break;
            }
            /*
            if (Vector3.SqrMagnitude(transform.position - end) < 1)
            {
                transform.position = Vector3.MoveTowards(transform.position, end, 0.01f);
                yield break;
            }*/
                float diff = Time.timeSinceLevelLoad - startTime;
            rate = diff / (duration / 60f);
            transform.position = CalcLerpPoint(start, half, end, rate);

            yield return null;
        }
    }
    Vector3 CalcLerpPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        var a = Vector3.Lerp(p0, p1, t);
        var b = Vector3.Lerp(p1, p2, t);
        return Vector3.Lerp(a, b, t);
    }

    // ここまで


  

}
