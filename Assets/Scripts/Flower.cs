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
            GET,
            TRANSFORM,
            JUMP,
            DAMAGE,
            DOWN,
            ATTACK,

            COUNT,
        }

        public static bool IsLoopAnim(int selection, int state)
        {
            switch (state)
            {
                case (int)STATES.IDLE:
                case (int)STATES.MOVE:
                    return true;
                case (int)STATES.GET:
                case (int)STATES.DOWN:
                    return selection == 1;
                case (int)STATES.TRANSFORM:
                case (int)STATES.ATTACK:
                    return selection == 0;
                default:
                    return false;
            }
        }

        public static bool IsMoveAnim(int selection, int state)
        {
            switch (state)
            {
                case (int)STATES.IDLE:
                case (int)STATES.MOVE:
                case (int)STATES.JUMP:
                    return true;
                case (int)STATES.GET:
                case (int)STATES.DOWN:
                    return selection == 1;
                case (int)STATES.TRANSFORM:
                case (int)STATES.ATTACK:
                    return selection == 0;
                default:
                    return false;
            }
        }

        public AnimationClip[] animationClips = new AnimationClip[(int)STATES.COUNT];
    }

    /* [SerializeField] */
    [SerializeField] private GroundCheck groundCheck;
    [SerializeField] private StateAnimations[] selectionAnimations;
    [SerializeField] private int maxHP;
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float lifeTime;
    /* [SerializeField] */


    /* Public */
    public float m_lifeTime { get { return lifeTime; } }        //最大生存時間（SerializeField参照）
    public float m_timeLife { get; private set; }               //生存時間
    public int m_maxHP { get { return maxHP; } }                //最大HP（SerializeField参照）
    public int m_selection { get; private set; } = 0;           //現在の形態
    public int m_nextSelection { get; private set; } = 0;       //次の形態
    /* Public */


    /* Private */
    private Rigidbody2D m_rigidbody;
    private Animator m_animator;                                //アニメーション
    private int m_state;                                        //現在のステータス
    private float m_inputX;                                     //横入力量（Update）
    private Item m_triggerItem = null;
    private bool m_keyDownSpace;
    private bool m_keyDownE;
    private bool m_isInHealAria;
    /* Private */



    private void Start()
    {
        SetState(StateAnimations.STATES.IDLE);
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
    }

    private void Update()
    {
        m_inputX = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_keyDownSpace = true;
        }
        if (Input.GetKeyUp(KeyCode.Space) || !Input.GetKey(KeyCode.Space))
        {
            m_keyDownSpace = false;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            m_keyDownE = true;
        }
        if (Input.GetKeyUp(KeyCode.E) || !Input.GetKey(KeyCode.E))
        {
            m_keyDownE = false;
        }
    }

    private void FixedUpdate()
    {
        if (m_isInHealAria)
        {
            m_timeLife -= Time.deltaTime;
            if (m_timeLife < 0)
            {
                m_timeLife = 0;
            }
        }
        else if (m_timeLife < m_lifeTime)
        {
            m_timeLife += Time.deltaTime;
            if (m_timeLife >= m_lifeTime)
            {
                m_timeLife = m_lifeTime;
                SetState(StateAnimations.STATES.DOWN);
            }
        }

        if (StateAnimations.IsMoveAnim(m_selection, m_state))
        {
            transform.position += new Vector3(m_inputX * speed, 0, 0);

            if (m_keyDownSpace && groundCheck.IsGround())
            {
                m_keyDownSpace = false;
                SetState(StateAnimations.STATES.JUMP);
            }

            if (m_keyDownE)
            {
                m_keyDownE = false;
                SetState(StateAnimations.STATES.ATTACK);
            }

            if (m_inputX < 0) transform.localScale = new Vector3(-1, 1, 1);
            if (m_inputX > 0) transform.localScale = Vector3.one;
        }

        if (StateAnimations.IsLoopAnim(m_selection, m_state))
        {
            if (m_inputX == 0)
            {
                SetState(StateAnimations.STATES.IDLE);
            }
            else
            {
                SetState(StateAnimations.STATES.MOVE);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            m_triggerItem = collision.gameObject.GetComponent<Item>();
        }
        if (collision.gameObject.CompareTag("HealArea"))
        {
            m_isInHealAria = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            if (m_triggerItem == collision.gameObject.GetComponent<Item>())
            {
                m_triggerItem = null;
            }
        }
        if (collision.gameObject.CompareTag("HealArea"))
        {
            m_isInHealAria = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Damage();
        }
    }

    public void JumpAddForce()
    {
        m_rigidbody.AddForce(Vector2.up * jumpPower);
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
        if (m_lifeTime <= m_timeLife) return;

        float oneHpTime = m_lifeTime / m_maxHP;
        m_timeLife += oneHpTime;
        if (m_lifeTime <= m_timeLife)
        {
            m_timeLife = m_lifeTime;
            SetState(StateAnimations.STATES.DOWN);
        }
        else
        {
            SetState(StateAnimations.STATES.DAMAGE);
        }
    }

    public void Heal()
    {
        float oneHpTime = m_lifeTime / m_maxHP;
        m_timeLife -= oneHpTime;
        if (m_timeLife < 0)
        {
            m_timeLife = 0;
        }
    }

    public bool IsTriggerItem()
    {
        return m_triggerItem != null;
    }

    public void Pick()
    {
        if (!IsTriggerItem()) return;
        m_nextSelection = m_triggerItem.m_flowerSelection;
        SetState(StateAnimations.STATES.GET);

        Destroy(m_triggerItem.gameObject);
        m_triggerItem = null;
    }

    public void Pick(Item item)
    {
        m_nextSelection = item.m_flowerSelection;
        SetState(StateAnimations.STATES.GET);
    }

    public bool IsGet()
    {
        return m_state == (int)StateAnimations.STATES.GET;
    }

    public void Select()
    {
        m_selection = m_nextSelection;
        SetState(StateAnimations.STATES.TRANSFORM);
    }
}
