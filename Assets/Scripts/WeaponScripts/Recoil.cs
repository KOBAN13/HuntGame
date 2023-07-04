using UnityEngine;

namespace Weapon
{
    public class Recoil : MonoBehaviour
    {
        private Vector3 currentRotations; // текущий поворот
        private Vector3 targetRotations; // возврат в начальную позицию

        [SerializeField] private float recoilX;//отдача по X
        [SerializeField] private float recoilY;//отдача по Y
        [SerializeField] private float recoilZ;//отдача по Z

        [SerializeField] private float snappiness; // быстрота перемещени€ в сфере
        [SerializeField] private float returnSpeed; // линейна€ быстрота перемещени€
        void Start()
        {
            //тут будет код
        }
        public void Update()
        {
            // все за€в€зано на интерпол€ции надо с ней разобратьс€
            targetRotations = Vector3.Lerp(targetRotations, Vector3.zero, returnSpeed * Time.deltaTime); // эта строчка нужна чтоб мы плавно после отдачи возвратащлись в 
                                                                                                         // вектор3 0.0.0, в исходное положение прицела с помощью линейной интерпол€ции
            currentRotations = Vector3.Slerp(currentRotations, targetRotations, snappiness * Time.deltaTime);// это та же сама€ интерпол€ци€ только сферическа€ и используетьс€ в основном дл€ поворота камеры

            //основна€ их разница в том что Lerp выполн€ет линейную интерпол€цию между двум€ векторами на пр€мой линии, а Slerp выполн€ет сферическую интерпол€цию на кротчайшем пути к сфере

            transform.localRotation = Quaternion.Euler(currentRotations); //конкретный поворот с помощью углов эйлера
        }

        public void ShootRecoil()
        {
            targetRotations += new Vector3(recoilX, Random.Range(-recoilY, recoilY), Random.Range(-recoilZ, recoilZ)); //конкретна€ отдача оружи€, прчем рандомна€
        }


        //конкретно отдача от бедра — —»—“≈ћќ… ѕ–»÷≈Ћ»¬јЌ»я Ќјƒќ Ѕ”ƒ≈“ —ƒ≈Ћј“№ „”“№ ѕќ ƒ–”√ќћ”
    }

}
