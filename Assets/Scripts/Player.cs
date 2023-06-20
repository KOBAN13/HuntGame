using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _gravity;
        [SerializeField] private CharacterController characterController;
        [SerializeField] private Rigidbody rigidbodyPlayer;
        [SerializeField] private float jumpForse;
        [SerializeField] private float groundDistanse;
        [SerializeField] LayerMask groundMask;
        private Vector3 move = Vector3.zero;
        private bool isGrounded = true;


        private void Awake()
        {
            characterController = rigidbodyPlayer.GetComponent<CharacterController>();
            rigidbodyPlayer.GetComponent<Rigidbody>();
        }
        private void Update()
        {
            PlayerMove();
        }

        private void Jump()
        {
            if (Input.GetButton("Jump"))
            {
                move.y = jumpForse;
            }
        }

        private bool CheckGround()
        {
            RaycastHit raycastHit;
            if (Physics.Raycast(transform.position, Vector3.down, out raycastHit, groundDistanse, groundMask))
                isGrounded = true;
            else
                isGrounded = false;
            return isGrounded;
        }

        private void PlayerMove()
        {
            if (CheckGround())
            {
                float x = Input.GetAxis("Horizontal");
                float z = Input.GetAxis("Vertical");
                move = new Vector3(x, 0, z);
                move = transform.TransformDirection(move);
                move *= _speed;
                Jump();
            }
            move.y -= _gravity * Time.deltaTime;
            characterController.Move(move * Time.deltaTime);
        }
    }
}
