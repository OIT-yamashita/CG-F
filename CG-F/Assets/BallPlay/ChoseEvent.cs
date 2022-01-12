using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChoseEvent : MonoBehaviour
{
    GameObject Choices;
    GameObject BallToy;
    GameObject Framy;
    AudioSource FramyAudio;
    public AudioClip FramyBadVoice;
    public AudioClip FramyGoodVoice;
    GameObject Oyatu;
    GameObject TitleWindow;

    // Start is called before the first frame update
    void Start()
    {
        Choices = GameObject.Find("Choices");
        BallToy = GameObject.Find("BallToy");
        Framy = GameObject.Find("Framy");
        FramyAudio = GetComponent<AudioSource>();
        Oyatu = GameObject.Find("Oyatu");
        TitleWindow = GameObject.Find("TitleWindow");
        TitleWindow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BallUbau()
    {
        Invoke(nameof(BadBall), 0.7f);
    }
    public void BadBall()
    {
        BallToy.SetActive(false);
        FramyAudio.PlayOneShot(FramyBadVoice);
        TitleWindow.SetActive(true);
    }


    public void EsaEvent()
    {
        Choices.SetActive(false);
        Oyatu.SetActive(true);
        Invoke(nameof(DropBall), 0.7f);
        //StartCoroutine(DropBall());


    }
    public void DropBall()
    {
        BallToy.SetActive(false);
        FramyAudio.PlayOneShot(FramyGoodVoice);
        TitleWindow.SetActive(true);

    }
    /*
    private IEnumerator DropBall()
    {
        
        yield return new WaitForSeconds(1);
        BallToy.SetActive(false);
        FramyAudio.PlayOneShot(FramyGoodVoice);
        TitleWindow.SetActive(true);

    }*/
}
