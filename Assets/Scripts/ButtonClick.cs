using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{
    [SerializeField] private bool IsOnceClick = false;

    private Button button;
    private AudioSource audioSource;
    private bool isClicked;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOnceClick && isClicked)
        {
            isClicked = false;
            button.enabled = true;
        }
    }

    private void LateUpdate()
    {

    }

    public void ClickedFunc(AudioClip clickSound)
    {
        if (IsOnceClick)
        {
            button.enabled = false;
            if (isClicked) return;

            isClicked = true;
        }
        audioSource.PlayOneShot(clickSound);
    }
}
