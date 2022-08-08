﻿using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

/* Note: animations are called via the controller for both the character and capsule using animator null checks
 */

namespace StarterAssets
{
    [RequireComponent(typeof(CharacterController))]
    public class EnemyController : MonoBehaviour
    {
        [Header("Enemy")]
        [Tooltip("Move speed of the character in m/s")]
        public float MoveSpeed = 10.0f;

        [Tooltip("Sprint speed of the character in m/s")]
        public float SprintSpeed = 5.335f;

        [Tooltip("How fast the character turns to face movement direction")]
        [Range(0.0f, 0.3f)]
        public float RotationSmoothTime = 0.12f;

        [Tooltip("Acceleration and deceleration")]
        public float SpeedChangeRate = 10.0f;

        public AudioClip LandingAudioClip;
        public AudioClip[] FootstepAudioClips;
        [Range(0, 1)] public float FootstepAudioVolume = 0.5f;

        [Space(10)]
        [Tooltip("The height the enemy can jump")]
        public float JumpHeight = 1.2f;

        [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
        public float Gravity = -15.0f;

        [Space(10)]
        [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
        public float JumpTimeout = 0.50f;

        [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
        public float FallTimeout = 0.15f;

        [Header("Enemy Grounded")]
        [Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
        public bool Grounded = true;

        [Tooltip("Useful for rough ground")]
        public float GroundedOffset = -0.14f;

        [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
        public float GroundedRadius = 0.28f;

        [Tooltip("What layers the character uses as ground")]
        public LayerMask GroundLayers;

        [Tooltip("How far in degrees can you move the camera up")]
        public float TopClamp = 70.0f;

        [Tooltip("How far in degrees can you move the camera down")]
        public float BottomClamp = -30.0f;

        [Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
        public float CameraAngleOverride = 0.0f;

        [Tooltip("For locking the camera position on all axis")]
        public bool LockCameraPosition = false;

        // enemy
        private float _speed;
        private float _animationBlend;
        private float _targetRotation = 0.0f;
        private float _verticalVelocity;
        private float _terminalVelocity = 53.0f;

        // timeout deltatime
        private float _jumpTimeoutDelta;
        private float _fallTimeoutDelta;

        // animation IDs
        private int _animIDSpeed;
        private int _animIDGrounded;
        private int _animIDJump;
        private int _animIDFreeFall;
        private int _animIDMotionSpeed;
        private int _animIDLeftPunch;
        private int _animIDBoxing;

        private Animator _animator;
        private CharacterController _controller;

        private const float _threshold = 0.01f;

        private bool _hasAnimator;


        public enum EnemyState
        {
            Walk,
            Wait,
            Chase
        };

        private Animator animator;
        //　目的地
        private Vector3 _destination;
        //　歩くスピード
        [SerializeField]
        private float walkSpeed = 1.0f;
        //　速度
        private Vector3 velocity;
        //　移動方向
        private Vector3 direction;
        //　到着フラグ
        private bool arrived;
        //　SetPositionスクリプト
        private SetPosition setPosition;
        //　待ち時間
        [SerializeField]
        private float waitTime = 5f;
        //　経過時間
        private float elapsedTime;
        // 敵の状態
        private EnemyState state;
        //　プレイヤーTransform
        private Transform playerTransform;


        private Vector3 startPosition;

        private void Start()
        {
            _hasAnimator = TryGetComponent(out _animator);
            _controller = GetComponent<CharacterController>();

            AssignAnimationIDs();

            // reset our timeouts on start
            _jumpTimeoutDelta = JumpTimeout;
            _fallTimeoutDelta = FallTimeout;



            animator = GetComponent<Animator>();
            CreateRandomPosition();
            velocity = Vector3.zero;
            arrived = false;
            elapsedTime = 0f;
            SetState(EnemyState.Walk);

            startPosition = transform.position;
            _destination = transform.position;
        }

        public void CreateRandomPosition()
        {
            //　ランダムなVector2の値を得る
            var randDestination = Random.insideUnitCircle * 8;
            //　現在地にランダムな位置を足して目的地とする
            _destination = startPosition + new Vector3(randDestination.x, 0, randDestination.y);
        }

        private void Update()
        {
            _hasAnimator = TryGetComponent(out _animator);

            JumpAndGravity();
            GroundedCheck();
            Move();
            //Attack();
            Damage();
        }

        private void LateUpdate()
        {
            //CameraRotation();
        }

        private void AssignAnimationIDs()
        {
            _animIDSpeed = Animator.StringToHash("Speed");
            _animIDGrounded = Animator.StringToHash("Grounded");
            _animIDJump = Animator.StringToHash("Jump");
            _animIDFreeFall = Animator.StringToHash("FreeFall");
            _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
            _animIDLeftPunch = Animator.StringToHash("LeftPunch");
            _animIDBoxing = Animator.StringToHash("RightPunch");
        }

        private void GroundedCheck()
        {
            // set sphere position, with offset
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset,
                transform.position.z);
            Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers,
                QueryTriggerInteraction.Ignore);

            // update animator if using character
            if (_hasAnimator)
            {
                _animator.SetBool(_animIDGrounded, Grounded);
            }
        }

        private void Move()
        {
            switch (state)
            {
                case EnemyState.Wait:
                    
                    elapsedTime += Time.deltaTime;
                    if (elapsedTime > waitTime)
                    {
                        SetState(EnemyState.Walk);
                    }
                    break;

                case EnemyState.Walk:
                    if (_controller.isGrounded)
                    {
                        velocity = Vector3.zero;
                        animator.SetFloat("Speed", 2.0f);
                        direction = (_destination - transform.position).normalized;
                        transform.LookAt(new Vector3(_destination.x, transform.position.y, _destination.z));
                        velocity = direction * walkSpeed;
                    }

                    if (Vector3.Distance(transform.position, _destination) < 2.0f)
                    {
                        SetState(EnemyState.Wait);
                        animator.SetFloat("Speed", 0.0f);
                    }
                    break;
                case EnemyState.Chase:
                    _destination = playerTransform.position;
                    if (_controller.isGrounded)
                    {
                        velocity = Vector3.zero;
                        animator.SetFloat("Speed", 2.0f);
                        direction = (_destination - transform.position).normalized;
                        transform.LookAt(new Vector3(_destination.x, transform.position.y, _destination.z));
                        velocity = direction * walkSpeed;
                    }

                    if (Vector3.Distance(transform.position, _destination) < 0.5f)
                    {
                        SetState(EnemyState.Wait);
                        animator.SetFloat("Speed", 0.0f);
                    }
                    break;
            }
            
            velocity.y += Physics.gravity.y * Time.deltaTime;
            _controller.Move(velocity * Time.deltaTime);
        }

        private void JumpAndGravity()
        {
            if (Grounded)
            {
                // reset the fall timeout timer
                _fallTimeoutDelta = FallTimeout;

                // update animator if using character
                if (_hasAnimator)
                {
                    _animator.SetBool(_animIDJump, false);
                    _animator.SetBool(_animIDFreeFall, false);
                }

                // stop our velocity dropping infinitely when grounded
                if (_verticalVelocity < 0.0f)
                {
                    _verticalVelocity = -2f;
                }

                // Jump
                //if (_input.jump && _jumpTimeoutDelta <= 0.0f)
                //{
                //    // the square root of H * -2 * G = how much velocity needed to reach desired height
                //    _verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);

                //    // update animator if using character
                //    if (_hasAnimator)
                //    {
                //        _animator.SetBool(_animIDJump, true);
                //    }
                //}

                // jump timeout
                if (_jumpTimeoutDelta >= 0.0f)
                {
                    _jumpTimeoutDelta -= Time.deltaTime;
                }
            }
            else
            {
                // reset the jump timeout timer
                _jumpTimeoutDelta = JumpTimeout;

                // fall timeout
                if (_fallTimeoutDelta >= 0.0f)
                {
                    _fallTimeoutDelta -= Time.deltaTime;
                }
                else
                {
                    // update animator if using character
                    if (_hasAnimator)
                    {
                        _animator.SetBool(_animIDFreeFall, true);
                    }
                }

                // if we are not grounded, do not jump
                //_input.jump = false;
            }

            // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
            if (_verticalVelocity < _terminalVelocity)
            {
                _verticalVelocity += Gravity * Time.deltaTime;
            }
        }

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }

        private void OnDrawGizmosSelected()
        {
            Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
            Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

            if (Grounded) Gizmos.color = transparentGreen;
            else Gizmos.color = transparentRed;

            // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
            Gizmos.DrawSphere(
                new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z),
                GroundedRadius);
        }

        private void OnFootstep(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
                if (FootstepAudioClips.Length > 0)
                {
                    var index = Random.Range(0, FootstepAudioClips.Length);
                    AudioSource.PlayClipAtPoint(FootstepAudioClips[index], transform.TransformPoint(_controller.center), FootstepAudioVolume);
                }
            }
        }

        private void OnLand(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
                AudioSource.PlayClipAtPoint(LandingAudioClip, transform.TransformPoint(_controller.center), FootstepAudioVolume);
            }
        }

        private void Attack()
        {
            _animator.SetBool(_animIDBoxing, false);
            if (Input.GetMouseButtonDown(1))
            {
                //_animator.SetTrigger("Boxing");
                _animator.SetBool(_animIDBoxing, true);
            }

            _animator.SetBool(_animIDLeftPunch, false);
            if (Input.GetMouseButtonDown(0))
            {
                _animator.SetBool(_animIDLeftPunch, true);
            }
        }

        public float _damage = 0;
        public Vector3 _damageVec;
        private void Damage()
        {
            if (_damage > 0)
            {
                _controller.Move(_damageVec * _damage * Time.deltaTime);
                _damage = _damage - 1;
            }
        }

        public void SetState(EnemyState tempState, Transform targetObj = null)
        {
            if (tempState == EnemyState.Walk)
            {
                arrived = false;
                elapsedTime = 0f;
                state = tempState;
                CreateRandomPosition();
            }
            else if (tempState == EnemyState.Chase)
            {
                state = tempState;
                //　待機状態から追いかける場合もあるのでOff
                arrived = false;
                //　追いかける対象をセット
                playerTransform = targetObj;
            }
            else if (tempState == EnemyState.Wait)
            {
                elapsedTime = 0f;
                state = tempState;
                arrived = true;
                velocity = Vector3.zero;
                animator.SetFloat("Speed", 0f);
            }
        }

        public EnemyState GetState()
        {
            return state;
        }
    }
}