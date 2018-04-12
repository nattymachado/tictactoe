using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyBehaviourScript : MonoBehaviour {

    private Camera _camera = null;
    private Vector3 _limitOfWorld;
    private SpriteRenderer _sprite = null;
    private float _totalTime = 0;
    private float _initScaleX = 0;
    private float _baseLocation;

    public float WaveLength = 10;
    public float WaveAmplitude = 5f;
    public float Speed = 5f;

    void Start()
    {
        _camera = Camera.main;
        _sprite = GetComponent<SpriteRenderer>();
        _initScaleX = transform.localScale.x;
        _totalTime = 0;
        _limitOfWorld = _camera.ScreenToWorldPoint(new Vector3(0f, Screen.height, 0f));
        
        initializePositionAndColor();
    }

    void initializePositionAndColor()
    {
        _sprite.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        _baseLocation = Random.Range(0f, _limitOfWorld.x);
        transform.position = new Vector3(_baseLocation, -_limitOfWorld.y, 0f);
    }

    void updatePosition()
    {
        _totalTime += Time.deltaTime;
        Vector3 position = transform.position;
        position.y += Speed * Time.deltaTime;
        position.x = _baseLocation + Mathf.Sin(position.y * 2 * Mathf.PI / WaveLength) * WaveAmplitude / 2;

        Debug.Log(position);
        if (position.y > _limitOfWorld.y)
        {
            initializePositionAndColor();
        } else
        {
            transform.position = position;
        }

        if ( (_totalTime % 1) < 0.6)
        {
            transform.localScale = new Vector3(_initScaleX / 2f, transform.localScale.y, 1f);
        } else{
            transform.localScale = new Vector3(_initScaleX, transform.localScale.y, 1f);
        }
            
    }

    void Update () {
        updatePosition();
    }
}
