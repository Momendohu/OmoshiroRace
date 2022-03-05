using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ForwardAccel();
        Steering();
    }

    public void ForwardAccel()
    {
        if (GetInputForwardAccel())
        {
            _forwardAccel += _accelSpeed * Time.deltaTime;
        }

        if (GetInputBackAccel())
        {
            _forwardAccel -= _accelSpeed * Time.deltaTime;
        }

        if(!GetInputForwardAccel() && !GetInputBackAccel())
        {
            _stopTimer += Time.deltaTime;
            _forwardAccel = Mathf.Lerp(_forwardAccel, 0, _stopTimer / _stopTime);
        }
        else
        {
            _stopTimer = 0.0f;
        }

        _forwardAccel = Mathf.Clamp(_forwardAccel, _maxAccelSpeed * -1, _maxAccelSpeed);
        _rigidbody.velocity = _rigidbody.transform.forward * _forwardAccel;
    }

    void Steering()
    {
        if (GetInputLeftAccel())
        {
            _steering -= _corneringSpec * Time.deltaTime;
        }
        else if (GetInputRightAccel())
        {
            _steering += _corneringSpec * Time.deltaTime;
        }
        else
        {
            _steering = Mathf.Lerp(_steering, 0, 0.1f);
        }
        _steering = Mathf.Clamp(_steering, -_maxCornering, _maxCornering);
        Vector3 rot = transform.rotation.eulerAngles;
        rot.y += _steering;
        transform.rotation = Quaternion.Euler(rot);
    }

    private bool GetInputForwardAccel()
    {
        return Input.GetKey(KeyCode.Z);
    }

    private bool GetInputBackAccel()
    {
        return Input.GetKey(KeyCode.X);
    }

    private bool GetInputRightAccel()
    {
        return Input.GetKey(KeyCode.RightArrow);
    }

    private bool GetInputLeftAccel()
    {
        return Input.GetKey(KeyCode.LeftArrow);
    }

    [SerializeField]
    private float _accelSpeed = 5.0f;

    [SerializeField]
    private float _maxAccelSpeed = 20.0f;

    [SerializeField]
    private float _stopTime = 3.0f;

    [SerializeField]
    private float _corneringSpec = 5f;
    [SerializeField]
    private float _maxCornering = 50f;

    private float _steering = 0;
    private Rigidbody _rigidbody;
    private float _forwardAccel;
    private float _stopTimer;
}
