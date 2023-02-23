using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonShooter : MonoBehaviour
{

    private bool _timerEnd = false;
    [SerializeField]
    private float _timerLeft = 1f;
    private float _currTimerLeft;

    [SerializeField]
    private Rigidbody _bullet;

    [SerializeField]
    private float _bulletSpeed = 0.8f;

    // Start is called before the first frame update
    void Start()
    {

        _currTimerLeft = _timerLeft;
    }

    // Update is called once per frame
    void Update()
    {

        if (!_timerEnd)
        {
            _currTimerLeft -= Time.deltaTime;
            if (_currTimerLeft < 0)
            {
                _timerEnd = true;
                _currTimerLeft = _timerLeft;
            }
        }
        else
        {
            Rigidbody bullet = (Rigidbody) Instantiate(_bullet, transform.position, transform.rotation);
            bullet.velocity = -transform.right * _bulletSpeed;
            _timerEnd = false;
        }
    }
}
