using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
            START,
            IDLE,
            MOVE,
            GET,
            TRANSFORM,
            JUMP,
            DAMAGE,
            DOWN,
            ATTACK,
            GOAL,
            ABILITY_VERTICAL,
            ABILITY_HORIZONTAL,

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
                case (int)STATES.START:
                    return selection == 1 || selection == 2;
                case (int)STATES.TRANSFORM:
                    return selection == 0;
                case (int)STATES.ATTACK:
                    return selection == 0 || selection == 2;
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
                case (int)STATES.START:
                    return selection == 1 || selection == 2;
                case (int)STATES.TRANSFORM:
                    return selection == 0;
                case (int)STATES.ATTACK:
                    return selection == 0 || selection == 2;
                default:
                    return false;
            }
        }

        public AnimationClip GetAnimation(int state)
        {
            switch (state)
            {
                case (int)STATES.START:
                    return animationStart;
                case (int)STATES.IDLE:
                    return animationIdle;
                case (int)STATES.MOVE:
                    return animationMove;
                case (int)STATES.GET:
                    return animationGet;
                case (int)STATES.TRANSFORM:
                    return animationTransform;
                case (int)STATES.JUMP:
                    return animationJump;
                case (int)STATES.DAMAGE:
                    return animationDamage;
                case (int)STATES.DOWN:
                    return animationDown;
                case (int)STATES.ATTACK:
                    return animationAttack;
                case (int)STATES.GOAL:
                    return animationGoal;
                case (int)STATES.ABILITY_VERTICAL:
                    return animationAbilityVertical;
                case (int)STATES.ABILITY_HORIZONTAL:
                    return animationAbilityHorizontal;
                default:
                    return null;
            }
        }

        //public AnimationClip[] animationClips = new AnimationClip[(int)STATES.COUNT];
        public AnimationClip animationStart;
        public AnimationClip animationIdle;
        public AnimationClip animationMove;
        public AnimationClip animationGet;
        public AnimationClip animationTransform;
        public AnimationClip animationJump;
        public AnimationClip animationDamage;
        public AnimationClip animationDown;
        public AnimationClip animationAttack;
        public AnimationClip animationGoal;
        public AnimationClip animationAbilityVertical;
        public AnimationClip animationAbilityHorizontal;
    }

    /* [SerializeField] */
    [SerializeField] private GroundCheck groundCheck;
    [SerializeField] private WireCheck wireCheck;
    [SerializeField] private StateAnimations[] selectionAnimations;
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private int maxHP;
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float lifeTime;
    [SerializeField] private float lifeHealTime;
    /* [SerializeField] */


    /* Public */
    public float m_limitLifeTime { get { return lifeTime; } }        //最大生存時間（SerializeField参照）
    public float m_lifeHealTime { get { return lifeHealTime; } }     //全回復までの時間（SerializeField参照）
    public float m_timeLife { get; private set; }               //生存時間
    public int m_maxHP { get { return maxHP; } }                //最大HP（SerializeField参照）
    public int m_selection { get; private set; } = 0;           //現在の形態
    public int m_nextSelection { get; private set; } = 0;       //次の形態
    public bool m_isGoal { get; private set; }
    public bool m_isFinish { get; private set; }
    public bool m_isGoalEnd { get; private set; }
    /* Public */


    /* Private */
    private BoxCollider2D m_collider;
    private Rigidbody2D m_rigidbody;
    private Animator m_animator;                                //アニメーション
    private int m_state;                                        //現在のステータス
    private float m_inputX;                                     //横入力量（Update）
    private float m_inputY;                                     //縦入力量（）
    private Vector3 m_manualTargetPos;
    private Item m_triggerItem = null;                          //取得可能なアイテム
    private bool m_keyDownSpace;                                //Space入力
    private bool m_keyDownW;                                    //W入力
    private bool m_keyDownE;                                    //E入力
    private bool m_keyDownF;                                    //F入力
    private bool m_keyDownTen4;                                 //4入力
    private bool m_keyDownTen6;                                 //6入力
    private bool m_keyDownTen8;                                 //8入力
    private bool m_isInHealAria;                                //回復エリア内か
    private AudioSource m_audioSource;
    private GameObject m_currentItem;                           //現在のアイテム情報 
    private bool m_isDebug;
    private bool m_isJump;
    private float m_abilityTime;
    private float m_abilityHitTime;
    private float m_abilityMoveStart = 0.1f;
    private float m_abilityMoveEnd = 0.2f;
    private float m_abilityMoveTime = 0.40f;
    private bool m_isAbilityChecked;
    private bool m_isAbilityCancel;
    private UnityEvent m_HealEreaEnterEvents = new UnityEvent();
    private UnityEvent<Item> m_ItemEnterEvents = new UnityEvent<Item>();
    private bool m_isPause;
    /* Private */


    private void Awake()
    {
        m_collider = GetComponent<BoxCollider2D>();
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
        m_audioSource = GetComponent<AudioSource>();
       
        m_rigidbody.bodyType = RigidbodyType2D.Kinematic;
        SetState(StateAnimations.STATES.IDLE);
        Debug.Log("Awake End:" + m_rigidbody.isKinematic);
    }

    private void Start()
    {
        Debug.Log("Start:" + m_rigidbody.isKinematic);
       
    }

    private void Update()
    {
        //Debug.Log("Update(" + Time.time + "):" + m_rigidbody.isKinematic);
        if (m_rigidbody.isKinematic && !m_isGoal && m_abilityTime <= 0)
        {
            m_rigidbody.bodyType = RigidbodyType2D.Dynamic;
        }
        if (m_isPause)
        {
            if (Time.timeScale != 0)
            {
                m_isPause = false;
                m_audioSource.Play();
            }
        }
        else if (Time.timeScale == 0)
        {
            m_audioSource.Pause();
        }

        if (m_isGoal) return;

        m_inputX = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_keyDownSpace = true;
        }
        if (Input.GetKeyUp(KeyCode.Space) || !Input.GetKey(KeyCode.Space))
        {
            m_keyDownSpace = false;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            m_keyDownW = true;
        }
        if (Input.GetKeyUp(KeyCode.W) || !Input.GetKey(KeyCode.W))
        {
            m_keyDownW = false;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            m_keyDownE = true;
        }
        if (Input.GetKeyUp(KeyCode.E) || !Input.GetKey(KeyCode.E))
        {
            m_keyDownE = false;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            m_keyDownF = true;
        }
        if (Input.GetKeyUp(KeyCode.F) || !Input.GetKey(KeyCode.F))
        {
            m_keyDownF = false;
        }

        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            m_keyDownTen4 = true;
        }
        if (Input.GetKeyUp(KeyCode.Keypad4) || !Input.GetKey(KeyCode.Keypad4))
        {
            m_keyDownTen4 = false;
        }

        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            m_keyDownTen6 = true;
        }
        if (Input.GetKeyUp(KeyCode.Keypad6) || !Input.GetKey(KeyCode.Keypad6))
        {
            m_keyDownTen6 = false;
        }

        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            m_keyDownTen8 = true;
        }
        if (Input.GetKeyUp(KeyCode.Keypad8) || !Input.GetKey(KeyCode.Keypad8))
        {
            m_keyDownTen8 = false;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (m_selection !=0 )
            {
               
                m_nextSelection = 0;
                Select();
              
                 m_currentItem.SetActive(true);
            }
        }
    }

    private void FixedUpdate()
    {
        Debug.Log("FixedUpdate(" + Time.time + "):" + m_rigidbody.isKinematic);
        if (!m_isGoal && m_timeLife < m_limitLifeTime && !m_isDebug)
        {
            if (m_isInHealAria)
            {
                m_timeLife -= Time.deltaTime * (m_limitLifeTime / m_lifeHealTime);
                if (m_timeLife < 0)
                {
                    m_timeLife = 0;
                }
            }
            else if (m_timeLife < m_limitLifeTime)
            {
                m_timeLife += Time.deltaTime;
                if (m_timeLife >= m_limitLifeTime)
                {
                    m_timeLife = m_limitLifeTime;
                }
            }
        }
        if (m_timeLife >= m_limitLifeTime)
        {
            SetState(StateAnimations.STATES.DOWN);
        }

        if (StateAnimations.IsMoveAnim(m_selection, m_state))
        {
            if (m_isGoal)
            {
                if (!m_isGoalEnd)
                {
                    Vector3 dif = m_manualTargetPos - transform.position;
                    float distance = Vector3.Distance(m_manualTargetPos, transform.position);
                    dif.Normalize();
                    dif *= distance;
                    m_inputX = dif.x;
                    m_inputY = dif.y;

                    if (m_inputX * speed > dif.x)
                    {
                        m_inputX = dif.x / speed;
                    }
                    if (m_inputY * speed > dif.y)
                    {
                        m_inputY = dif.y / speed;
                    }

                    transform.position += new Vector3(m_inputX * speed, m_inputY * speed, 0);
                }
            }
            else
            {
                transform.position += new Vector3(m_inputX * speed, 0, 0);
            }

            Vector3 scale = transform.localScale;
            if (m_inputX < 0) scale.x = -Mathf.Abs(scale.x);
            if (m_inputX > 0) scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;

            if (m_keyDownW && groundCheck.IsGround())
            {
                m_keyDownW = false;
                SetState(StateAnimations.STATES.JUMP);
            }

            if (m_keyDownF)
            {
                m_keyDownF = false;

                if (m_selection == 1)
                {
                    SetState(StateAnimations.STATES.ATTACK);
                }
                else if (m_selection == 2)
                {
                    m_abilityTime = 0;
                    m_abilityHitTime = 0;
                    m_isAbilityChecked = false;
                    m_isAbilityCancel = false;
                    m_animator.SetFloat("WireAbility", 1);
                    if (m_keyDownTen8)
                    {
                        SetState(StateAnimations.STATES.ABILITY_VERTICAL);
                    }
                    else if (m_keyDownTen6 || !m_keyDownTen4)
                    {
                        scale.x = Mathf.Abs(scale.x);
                        transform.localScale = scale;
                        SetState(StateAnimations.STATES.ABILITY_HORIZONTAL);
                    }
                    else
                    {
                        scale.x = -Mathf.Abs(scale.x);
                        transform.localScale = scale;
                        SetState(StateAnimations.STATES.ABILITY_HORIZONTAL);
                    }
                    m_keyDownTen4 = false;
                    m_keyDownTen6 = false;
                    m_keyDownTen8 = false;
                    PlaySE();
                }
            }
        }

        if (m_state == (int)StateAnimations.STATES.ABILITY_VERTICAL ||
            m_state == (int)StateAnimations.STATES.ABILITY_HORIZONTAL)
        {
            //Debug.Log(m_abilityTime + ":" + m_isAbilityChecked);
            if (m_isAbilityCancel)
            {
                m_abilityTime -= Time.deltaTime;
                if (m_abilityTime <= 0)
                {
                    AnimationEnded();
                }
            }
            else
            {
                m_abilityTime += Time.deltaTime;
            }
            if (!m_isAbilityChecked)
            {
                if (wireCheck.IsWire())
                {
                    m_isAbilityChecked = true;
                    m_abilityHitTime = m_abilityTime;
                    m_abilityTime = 2.0f - (m_abilityTime - Time.deltaTime);
                    m_rigidbody.bodyType = RigidbodyType2D.Kinematic;
                    m_rigidbody.velocity = Vector3.zero;
                }
                else if (wireCheck.IsGround())
                {
                    m_isAbilityChecked = true;
                    m_isAbilityCancel = true;
                }
                else
                {
                }
            }
            if (m_abilityHitTime > 0)
            {
                if (m_abilityTime - 1 < (m_abilityMoveEnd / m_abilityMoveTime))
                {
                    if (m_abilityTime - 1 > (m_abilityMoveStart / m_abilityMoveTime))
                    {
                        if (m_state == (int)StateAnimations.STATES.ABILITY_VERTICAL)
                        {
                            transform.position += new Vector3(0, (5.2f * (m_abilityHitTime / 1.0f)) * (Time.deltaTime / ((m_abilityMoveEnd - m_abilityMoveStart) / m_abilityMoveTime)));
                        }
                        else
                        {
                            int lrflg = transform.localScale.x < 0 ? -1 : 1;
                            transform.position += new Vector3((6.0f * (m_abilityHitTime / 1.0f)) * (Time.deltaTime / ((m_abilityMoveEnd - m_abilityMoveStart) / m_abilityMoveTime)) * lrflg, 0);
                        }
                    }
                }
            }
            if (m_abilityTime >= 2)
            {
                AnimationEnded();
            }
            m_animator.SetFloat("WireAbility", 1 - Mathf.PingPong(Mathf.Clamp(m_abilityTime, 0, 2), 1));
        }

        if (transform.position.y < -5)
        {
            m_timeLife = m_limitLifeTime;
        }

        if (StateAnimations.IsLoopAnim(m_selection, m_state))
        {
            if (m_inputX == 0 || (!groundCheck.IsGround() && !m_isGoal))
            {
                SetState(StateAnimations.STATES.IDLE);
            }
            else
            {
                SetState(StateAnimations.STATES.MOVE);
            }
            PlaySE();

            if (groundCheck.IsGround())
            {
                m_isJump = false;
            }
        }
        else
        {
            if (m_state == (int)StateAnimations.STATES.JUMP && m_isJump)
            {
                if (groundCheck.IsGround())
                {
                    SetState(StateAnimations.STATES.IDLE);
                }
            }
            if (!IsAnimationMatchState())
            {
                UpdateAnimation();//バグ回避
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_isGoal || m_timeLife >= m_limitLifeTime) return;
        if (collision.gameObject.CompareTag("Finish"))
        {
            m_isGoal = true;
            m_manualTargetPos = transform.position;
            m_rigidbody.bodyType = RigidbodyType2D.Kinematic;
            m_rigidbody.velocity = Vector3.zero;
        }
        if (collision.gameObject.CompareTag("Item"))
        {
            m_triggerItem = collision.gameObject.GetComponent<Item>();
            m_ItemEnterEvents.Invoke(m_triggerItem);
        }
        if (collision.gameObject.CompareTag("HealArea"))
        {
            m_isInHealAria = true;
            m_HealEreaEnterEvents.Invoke();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (m_isGoal || m_timeLife >= m_limitLifeTime) return;
        if (collision.gameObject.CompareTag("HealArea"))
        {
            m_isInHealAria = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (m_isGoal || m_timeLife >= m_limitLifeTime) return;
        if (collision.gameObject.CompareTag("Item"))
        {
            if (m_triggerItem == collision.gameObject.GetComponent<Item>())
            {
                 m_triggerItem = null;
               // m_triggerItem.enabled=false;
            }
        }
        if (collision.gameObject.CompareTag("HealArea"))
        {
            m_isInHealAria = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("CollisionEnter:" + m_rigidbody.isKinematic);
        if (m_isGoal || m_timeLife >= m_limitLifeTime) return;
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!m_isDebug) Damage();
        }
    }

    public void PlaySE()
    {
        if (m_state != (int)StateAnimations.STATES.MOVE)
        {
            if (audioClips[(int)StateAnimations.STATES.MOVE] &&
                m_audioSource.clip == audioClips[(int)StateAnimations.STATES.MOVE])
            {
                m_audioSource.Stop();
            }
        }
        switch (m_state)
        {
            case (int)StateAnimations.STATES.MOVE:
                {
                    if (audioClips[m_state])
                    {
                        if (m_audioSource.clip != audioClips[m_state] || !m_audioSource.isPlaying)
                        {
                            m_audioSource.clip = audioClips[m_state];
                            m_audioSource.loop = true;
                            m_audioSource.Play();
                        }
                    }
                }
                break;
            case (int)StateAnimations.STATES.GET:
            case (int)StateAnimations.STATES.JUMP:
            case (int)StateAnimations.STATES.DAMAGE:
            case (int)StateAnimations.STATES.GOAL:
            case (int)StateAnimations.STATES.DOWN:
                {
                    if (audioClips[m_state])
                    {
                        m_audioSource.clip = audioClips[m_state];
                        m_audioSource.loop = false;
                        m_audioSource.Play();
                    }
                }
                break;
        }
    }

    public void JumpAddForce()
    {
        m_rigidbody.AddForce(Vector2.up * jumpPower);
    }

    public void JumpHighEnd()
    {
        m_isJump = true;
    }

    public void AbilityChecked()
    {
        Debug.Log("AbilityChecked:" + m_abilityTime + "," + m_isAbilityChecked);
        if (!m_isAbilityChecked)
        {
            m_isAbilityChecked = true;
        }
    }

    public void AnimationEnded()//ループしないアニメーション用
    {
        if (m_state == (int)StateAnimations.STATES.JUMP)
        {
            if (groundCheck.IsGround())
            {
                SetState(StateAnimations.STATES.IDLE);
            }
        }
        else
        {
            Debug.Log("anim end:" + m_state);
            bool isAbilityAnim = m_state == (int)StateAnimations.STATES.ABILITY_VERTICAL ||
                m_state == (int)StateAnimations.STATES.ABILITY_HORIZONTAL;
            SetState(StateAnimations.STATES.IDLE);
            if (isAbilityAnim)
            {
                m_abilityTime = 0;
                m_abilityHitTime = 0;
                m_animator.SetFloat("WireAbility", 1);
            }
        }
    }

    public void GoalEnd()
    {
        m_isGoalEnd = true;
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
        if (selectionAnimations[m_selection].GetAnimation(m_state) == null) return;

        m_animator.Play(selectionAnimations[m_selection].GetAnimation(m_state).name);
    }

    private bool IsAnimationMatchState()
    {
        if (selectionAnimations.Length <= m_selection) return false;
        if (selectionAnimations[m_selection].GetAnimation(m_state) == null) return false;

        return m_animator.GetCurrentAnimatorStateInfo(0).IsName(selectionAnimations[m_selection].GetAnimation(m_state).name);
    }

    public void Damage()
    {
        if (m_state == (int)StateAnimations.STATES.DAMAGE || m_state == (int)StateAnimations.STATES.DOWN) return;

        if (m_limitLifeTime <= m_timeLife) return;

        float oneHpTime = m_limitLifeTime / m_maxHP;
        m_timeLife += oneHpTime;
        if (m_limitLifeTime <= m_timeLife)
        {
            m_timeLife = m_limitLifeTime;
            SetState(StateAnimations.STATES.DOWN);
        }
        else
        {
            SetState(StateAnimations.STATES.DAMAGE);
        }
    }

    public void Heal()
    {
        float oneHpTime = m_limitLifeTime / m_maxHP;
        m_timeLife -= oneHpTime;
        if (m_timeLife < 0)
        {
            m_timeLife = 0;
        }
    }

    public bool IsTriggerItem()
    {
        return m_triggerItem != null;
       // return m_triggerItem != false;
    }

    public void Pick()
    {
        if (!IsTriggerItem()) return;
        m_nextSelection = m_triggerItem.m_flowerSelection;
        SetState(StateAnimations.STATES.GET);

        m_currentItem = m_triggerItem.gameObject;

        m_triggerItem.gameObject.SetActive(false);
        
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

    public void SetGoalPos(Vector2 gPos)
    {
        m_manualTargetPos = gPos;
    }

    public void Finish()
    {
        m_isFinish = true;
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x);
        transform.localScale = scale;
        SetState(StateAnimations.STATES.GOAL);
    }

    public void ToggleDebugMode()
    {
        m_isDebug = !m_isDebug;
    }

    public void AddHealEreaEnterEvent(UnityAction action)
    {
        m_HealEreaEnterEvents.AddListener(action);
    }

    public void RemoveHealEreaEnterEvent(UnityAction action)
    {
        m_HealEreaEnterEvents.RemoveListener(action);
    }

    public void AddItemEnterEvent(UnityAction<Item> action)
    {
        m_ItemEnterEvents.AddListener(action);
    }

    public void RemoveItemEnterEvent(UnityAction<Item> action)
    {
        m_ItemEnterEvents.RemoveListener(action);
    }

    public void SetFirstStartAnim()
    {
        SetState(StateAnimations.STATES.START);
    }
}
