using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fruit.Map;

namespace Fruit.Behaviour
{
    enum ControlMode
    {
        RUN, WALK
    }

    public class PlayerInputController : MonoBehaviour
    {
        public static PlayerInputController instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = FindObjectOfType<PlayerInputController>();
                }

                return m_instance;
            }
        }
        private static PlayerInputController m_instance; //싱글턴이 할당될 변수

        public float runSpeed = 2.0f;
        public float turnSpeed = 200.0f;
        public float jumpForce = 4.0f;
        public Animator _animator;
        public Rigidbody _rigidbody;
        ControlMode _controlMode = ControlMode.RUN;

        PlayerInputActions _input;
        public VariableJoystick variableJoystick;

        float currentV = 0;
        float currentH = 0;

        readonly float interpolation = 10;
        readonly float walkScale = 0.33f;
        readonly float backwardsWalkScale = 0.16f;
        readonly float backwardRunScale = 0.66f;

        bool wasGrounded, isGrounded;
        Vector3 currentDirection = Vector3.zero;

        float jumpTimeStamp = 0;
        float minJumpInterval = 0.25f;
        bool jumpInput = false;

        private List<Collider> Collisions = new List<Collider>();

        public List<string> CoList;

        public string trigger;

        private void OnCollisionEnter(Collision collision)
        {
            ContactPoint[] contactPoints = collision.contacts;
            for (int i = 0; i < contactPoints.Length; i++)
            {
                if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
                {
                    if (!Collisions.Contains(collision.collider))
                    {
                        Collisions.Add(collision.collider);
                    }
                    isGrounded = true;
                }
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            ContactPoint[] contactPoints = collision.contacts;
            bool validSurfaceNormal = false;
            for (int i = 0; i < contactPoints.Length; i++)
            {
                if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
                {
                    validSurfaceNormal = true; break;
                }
            }

            if (validSurfaceNormal)
            {
                isGrounded = true;
                if (!Collisions.Contains(collision.collider))
                {
                    Collisions.Add(collision.collider);
                }
            }
            else
            {
                if (Collisions.Contains(collision.collider))
                {
                    Collisions.Remove(collision.collider);
                }
                if (Collisions.Count == 0) { isGrounded = false; }
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (Collisions.Contains(collision.collider))
            {
                Collisions.Remove(collision.collider);
            }
            if (Collisions.Count == 0) { isGrounded = false; }
        }

        void Awake()
        {
            CoList = new List<string>();
        }

        void Start()
        {
            _input = new PlayerInputActions();
            _input.Player.RUN.Enable();
            _input.Player.WALK.Enable();
            _input.Player.JUMP.Enable();
            _input.Player.INTERACTION.Enable();
        }
        
        public bool isJump; 
        public bool isInteraction;
        void Update()
        {                           
            // if (Application.platform != RuntimePlatform.Android)
            // {
            //     isJump = _input.Player.JUMP.ReadValue<float>() > 0.5f ? true : false;
            // }             

            if (!jumpInput && isJump)
            {
                jumpInput = true;
            }
            isJump = false; 

            if (CoList.Count == 0)
            {
                // if (Application.platform != RuntimePlatform.Android)
                // {
                //     isInteraction = _input.Player.INTERACTION.ReadValue<float>() > 0.5f ? true : false;
                // }

                if (isInteraction == true)
                {                
                    StartCoroutine("PublishInteractionEvent");
                    isInteraction = false;
                }
            }                       
        }

        void FixedUpdate()
        {
            _animator.SetBool("Grounded", isGrounded);

            switch (_controlMode)
            {
                case ControlMode.RUN:
                    RUNUpdate();
                    break;

                case ControlMode.WALK:
                    WALKUpdate();
                    break;

                default:
                    Debug.LogError("Unsupported state");
                    break;
            }

            wasGrounded = isGrounded;
            jumpInput = false;
        }

        void RUNUpdate()
        {
            float v, h;
            // if (Application.platform == RuntimePlatform.Android)
            {
                v = variableJoystick.Vertical;
                h = variableJoystick.Horizontal;
            }
            // else
            // {
            //     v = Input.GetAxis("Vertical");
            //     h = Input.GetAxis("Horizontal");
            // }        

            Transform camera = Camera.main.transform;

            if (_input.Player.WALK.ReadValue<float>() > 0.5f)
            {
                v *= walkScale;
                h *= walkScale;
            }

            currentV = Mathf.Lerp(currentV, v, Time.deltaTime * interpolation);
            currentH = Mathf.Lerp(currentH, h, Time.deltaTime * interpolation);

            Vector3 direction = camera.forward * currentV + camera.right * currentH;

            float directionLength = direction.magnitude;
            direction.y = 0;
            direction = direction.normalized * directionLength;

            if (direction != Vector3.zero)
            {
                currentDirection = Vector3.Slerp(currentDirection, direction, Time.deltaTime * interpolation);

                transform.rotation = Quaternion.LookRotation(currentDirection);
                transform.position += currentDirection * runSpeed * Time.deltaTime;

                _animator.SetFloat("MoveSpeed", direction.magnitude);
            }

            JumpingAndLanding();
        }

        void WALKUpdate()
        {
            float v, h;
            // if (Application.platform == RuntimePlatform.Android)
            {
                v = variableJoystick.Vertical;
                h = variableJoystick.Horizontal;
            }
            // else
            // {
            //     v = Input.GetAxis("Vertical");
            //     h = Input.GetAxis("Horizontal");
            // }
            
            bool walk = _input.Player.WALK.ReadValue<float>() > 0.5f? true : false;

            if (v < 0)
            {
                if (walk) { v *= backwardsWalkScale; }
                else { v *= backwardRunScale; }
            }
            else if (walk)
            {
                v *= walkScale;
            }

            currentV = Mathf.Lerp(currentV, v, Time.deltaTime * interpolation);
            currentH = Mathf.Lerp(currentH, h, Time.deltaTime * interpolation);

            transform.position += transform.forward * currentV * runSpeed * Time.deltaTime;
            transform.Rotate(0, currentH * turnSpeed * Time.deltaTime, 0);

            _animator.SetFloat("MoveSpeed", currentV);

            JumpingAndLanding();
        }

        void JumpingAndLanding()
        {
            bool jumpCooldownOver = (Time.time - jumpTimeStamp) >= minJumpInterval;

            if (jumpCooldownOver && isGrounded && jumpInput)
            {
                jumpTimeStamp = Time.time;
                _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }

            if (!wasGrounded && isGrounded)
            {
                _animator.SetTrigger("Land");
            }

            if (!isGrounded && wasGrounded)
            {
                _animator.SetTrigger("Jump");
            }
        }

        public void ClickJumpButton()
        {
            isJump = true;
        }

        public void ClickInteractionButton()
        {
            isInteraction = true;
        }

        IEnumerator PublishInteractionEvent()
        {
            CoList.Add(1.ToString());
            switch (trigger)
            {
                case "SEEINGPAINT": 
                    InteractionEventBus.Publish(InteractionEventType.SEEINGPAINT);
                    break;
                case "VIEWINGPOINT":
                    InteractionEventBus.Publish(InteractionEventType.VIEWINGPOINT);
                    break;
                default: break;
            }
            
            yield return new WaitForSecondsRealtime(0.3f);
            CoList.Clear();
        }
    }
}