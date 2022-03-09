using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    /// <summary>
    /// ステータスごとのアニメーションを管理するためのクラス
    /// </summary>
    [System.Serializable]
    public class StateAnimations
    {
        public enum STATES
        {
            IDLE,
            MOVE,

            COUNT,
        }

        public AnimationClip[] animationClips = new AnimationClip[(int)STATES.COUNT];
    }

    /* [SerializeField] */
    [SerializeField] private StateAnimations[] selectionAnimations;
    [SerializeField] private int maxHP;
    [SerializeField] private float speed;
    /* [SerializeField] */


    /* Public */
    public int m_maxHP { get { return maxHP; } }                //最大HP（SerializeField参照）
    public int m_hp { get; private set; }                       //HP
    public int m_selection { get; private set; } = 0;           //現在の形態
    public int m_nextSelection { get; private set; } = 0;       //次の形態
    /* Public */


    /* Private */
    private Animator m_animator;                                //アニメーション
    private int m_state;                                        //現在のステータス
    private float m_inputX;                                     //横入力量（Update）
    /* Private */



    private void Start()
    {
        m_hp = maxHP;
        SetState(StateAnimations.STATES.IDLE);
        m_animator = GetComponent<Animator>();
    }

    private void Update()
    {
        m_inputX = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(m_inputX * speed, 0, 0);

        if (m_inputX == 0)
        {
            SetState(StateAnimations.STATES.IDLE);
        }
        else
        {
            SetState(StateAnimations.STATES.MOVE);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            Pick(collision.gameObject.GetComponent<Item>());
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Damage();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }

    public void AnimationEnded()//ループしないアニメーション用
    {
        SetState(StateAnimations.STATES.IDLE);
    }

    private void SetState(StateAnimations.STATES states)
    {
        if (m_state == (int)states) return;

        m_state = (int)states;
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        if (selectionAnimations.Length <= m_selection) return;
        if (selectionAnimations[m_selection].animationClips.Length < (int)StateAnimations.STATES.COUNT) return;

        m_animator.Play(selectionAnimations[m_selection].animationClips[m_state].name);
    }

    public void Damage()
    {
        if (0 < m_hp)
        {
            m_hp--;
        }
    }

    public void Heal()
    {
        if (m_hp < maxHP)
        {
            m_hp++;
        }
    }

    public void Pick(Item item)
    {
        m_nextSelection = item.m_flowerSelection;
    }

    public void Select()
    {
        m_selection = m_nextSelection;
        UpdateAnimation();
    }
}
