using UnityEngine;

namespace Humanoid
{
    public class CameraMove : MonoBehaviour
    {
        [SerializeField] private float _mouseX;
        [SerializeField] private float _mouseY;
        [SerializeField] private float _mouseSpeed;
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private float _maxUp;
        [SerializeField] private float _maxDown;
        [SerializeField] private float _xRotation;
        
        private void Update()
        {
            GetAxis();
            SetMouseRotation();  //упаковать апдейт в игрока
        }

        private void GetAxis()
        {
            _mouseX = Input.GetAxis("Mouse X") * _mouseSpeed;
            _mouseY = Input.GetAxis("Mouse Y") * _mouseSpeed;
        }

        private void SetMouseRotation()
        {
            _xRotation -= _mouseY;
            transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
            _xRotation = Mathf.Clamp(_xRotation, (_maxDown * -1), (_maxUp * -1));
            _playerTransform.Rotate(Vector3.up * _mouseX);
        }

        private void Awake()
        {
            _playerTransform = FindObjectOfType<CharacterController>().GetComponent<Transform>();
        }
    }

}
