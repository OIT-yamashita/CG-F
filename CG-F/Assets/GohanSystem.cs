using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GohanSystem : MonoBehaviour
{

    public bool is_Renderer = true;

    //宣言
    public SpriteRenderer spriteRenderer;
    public Sprite sprite;
    public Sprite sprite2;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // if (is_Renderer)
        // {
        //    spriteRenderer.sprite = sprite;
        //}
        // else
        // {
        //    spriteRenderer.sprite = sprite2;
        // }

        // is_Renderer = !is_Renderer;

        OnMouseDrag();
    }

    public void ReturnTitle()
    {
        SceneManager.LoadScene("title");
    }

    public void EndGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
            Application.OpenURL("http://www.yahoo.co.jp");
#else
            Application.Quit();
#endif

    }

    public void OnMouseDrag()
    {
        //表示されてる画像によって処理を変える
        if (spriteRenderer.sprite == sprite2)
        {
            //画像切り替え①
            spriteRenderer.sprite = sprite;
        }
        else
        {
            //画像切り替え②
            spriteRenderer.sprite = sprite2;
        }

    }
}

