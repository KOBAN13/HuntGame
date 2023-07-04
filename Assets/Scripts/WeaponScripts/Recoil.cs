using UnityEngine;

namespace Weapon
{
    public class Recoil : MonoBehaviour
    {
        private Vector3 currentRotations; // ������� �������
        private Vector3 targetRotations; // ������� � ��������� �������

        [SerializeField] private float recoilX;//������ �� X
        [SerializeField] private float recoilY;//������ �� Y
        [SerializeField] private float recoilZ;//������ �� Z

        [SerializeField] private float snappiness; // �������� ����������� � �����
        [SerializeField] private float returnSpeed; // �������� �������� �����������
        void Start()
        {
            //��� ����� ���
        }
        public void Update()
        {
            // ��� ��������� �� ������������ ���� � ��� �����������
            targetRotations = Vector3.Lerp(targetRotations, Vector3.zero, returnSpeed * Time.deltaTime); // ��� ������� ����� ���� �� ������ ����� ������ ������������� � 
                                                                                                         // ������3 0.0.0, � �������� ��������� ������� � ������� �������� ������������
            currentRotations = Vector3.Slerp(currentRotations, targetRotations, snappiness * Time.deltaTime);// ��� �� �� ����� ������������ ������ ����������� � ������������� � �������� ��� �������� ������

            //�������� �� ������� � ��� ��� Lerp ��������� �������� ������������ ����� ����� ��������� �� ������ �����, � Slerp ��������� ����������� ������������ �� ���������� ���� � �����

            transform.localRotation = Quaternion.Euler(currentRotations); //���������� ������� � ������� ����� ������
        }

        public void ShootRecoil()
        {
            targetRotations += new Vector3(recoilX, Random.Range(-recoilY, recoilY), Random.Range(-recoilZ, recoilZ)); //���������� ������ ������, ����� ���������
        }


        //��������� ������ �� ����� � �������� ������������ ���� ����� ������� ���� �� �������
    }

}
