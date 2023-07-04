using UnityEngine;

namespace Humanoid
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
        [SerializeField] private Interaction _interactionAmmoBox;
        private Vector3 _move = Vector3.zero;
        private bool _isGrounded = true;
        
        private void Update()
        {
            PlayerMove();
            _interactionAmmoBox.OpenAndCloseAmmoBox();
        }

        private void Jump()
        {
            if (Input.GetButton("Jump"))
            {
                _move.y = jumpForse;
            }
        }

        private bool CheckGround()
        {
            RaycastHit raycastHit;
            if (Physics.Raycast(transform.position, Vector3.down, out raycastHit, groundDistanse, groundMask))
                _isGrounded = true;
            else
                _isGrounded = false;
            return _isGrounded;
        }

        private void PlayerMove()
        {
            if (CheckGround())
            {
                float x = Input.GetAxis("Horizontal");
                float z = Input.GetAxis("Vertical");
                _move = new Vector3(x, 0, z);
                _move = transform.TransformDirection(_move);
                _move *= _speed;
                Jump();
            }
            _move.y -= _gravity * Time.deltaTime;
            characterController.Move(_move * Time.deltaTime);
        }
    }
}
