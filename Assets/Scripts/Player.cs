using UnityEngine;

namespace Assets.Scripts
{
    public class Player : MonoBehaviour
    {
       [SerializeField] private float _speed = 10.0f;
       [SerializeField] private float _gravity = -9.8f;
       [SerializeField] private float _hp = 100;
       [SerializeField] private CharacterController characterController;


        private void Awake()
        {
            characterController.GetComponent<CharacterController>();
        }
        private void Update()
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            Vector3 move = transform.right * x + transform.forward * z;
            characterController.Move(move * _speed * Time.deltaTime);
        }
    }
}
