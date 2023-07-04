using UnityEngine;

public class HPObject : MonoBehaviour
{
    [SerializeField] private float _hpObject;
    [SerializeField] private float _arrmor;
    private bool _conditionExecute;

    public float CurrentHp => _hpObject;

    public void CheckDestroy(float damage)
    {
        if (damage <= 0)
            return;
        
        switch (_arrmor - damage < 0 && !_conditionExecute)
        {
            case true : var remainder = _arrmor - damage;
                _arrmor = 0;
                _hpObject += remainder;
                _conditionExecute = true;
                break;
            default: _hpObject = _arrmor <= 0 ? _hpObject -= damage : _hpObject;
                break;
        }

        if (_hpObject <= 0) 
        {
            Destroy(gameObject);
        }

        _arrmor = _arrmor > 0 ? _arrmor -= damage : _arrmor;
    }
}
