using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyFadeManager : MonoBehaviour
{
    #region Singleton

    private static MyFadeManager instance;

    public static MyFadeManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (MyFadeManager)FindObjectOfType(typeof(MyFadeManager));

                if (instance == null)
                {
                    Debug.LogError(typeof(MyFadeManager) + "is nothing");
                }
            }

            return instance;
        }
    }

    #endregion Singleton

    [SerializeField] private Color fadeColor = Color.black;

    private float fadeAlpha = 0;
    private bool isFading = false;



    public void Awake()
    {
        if (this != Instance)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }


    private void OnGUI()
    {
        if (isFading)
        {
            fadeColor.a = fadeAlpha;
            GUI.color = fadeColor;
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);
        }
    }

    public void LoadScene(string scene, float delay, float fadeIn, float fadeOut, bool autoScaleReset = false)
    {
        if (isFading) return;
        StartCoroutine(Transition(scene, delay, fadeIn, fadeOut, autoScaleReset));
    }
    public void LoadScene(string scene, float delay, float interval, bool autoScaleReset = false)
    {
        if (isFading) return;
        StartCoroutine(Transition(scene, delay, interval, interval, autoScaleReset));
    }
    public void LoadScene(string scene, float interval, bool autoScaleReset = false)
    {
        if (isFading) return;
        StartCoroutine(Transition(scene, 0f, interval, interval, autoScaleReset));
    }

    public bool IsFading()
    {
        return isFading;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="scene">�ǂݍ��ރV�[��</param>
    /// <param name="delay">�t�F�[�h�ɓ���܂ł̎���</param>
    /// <param name="fadeIn">�t�F�[�h�C���̎���</param>
    /// <param name="fadeOut">�t�F�[�h�A�E�g�̎���</param>
    /// <param name="autoScaleReset">Time.timeScale�����Z�b�g���邩�̃t���O</param>
    private IEnumerator Transition(string scene, float delay, float fadeIn, float fadeOut, bool autoScaleReset = false)
    {
        isFading = true;
        float time = 0;
        while (time < delay)
        {
            time += Time.unscaledDeltaTime;
            yield return null;
        }

        time = 0;
        while (time < fadeIn)
        {
            fadeAlpha = Mathf.Lerp(0f, 1f, time / fadeIn);
            time += Time.unscaledDeltaTime;
            yield return null;
        }

        //�V�[���ؑ� .
        SceneManager.LoadScene(scene);
        if (autoScaleReset)
        {
            Time.timeScale = 1;
        }

        time = 0;
        while (time < fadeOut)
        {
            fadeAlpha = Mathf.Lerp(1f, 0f, time / fadeIn);
            time += Time.unscaledDeltaTime;
            yield return null;
        }
        isFading = false;
    }
}
