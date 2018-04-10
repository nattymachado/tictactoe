using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyBehaviourScript : MonoBehaviour {

    private Camera _camera = null;
    private Vector3 _limitOfWorld;
    private SpriteRenderer _sprite = null;
    private float _index=0;



    // Use this for initialization
    void Start()
    {
        _camera = Camera.main;
        _sprite = GetComponent<SpriteRenderer>();
        Vector3 limitVector = new Vector3(0f, Screen.height, 0f);
        _limitOfWorld = _camera.ScreenToWorldPoint(limitVector);
        initializePosition();
    }

    void initializePosition()
    {
        transform.position = new Vector3(Random.Range(0f, _limitOfWorld.x), -_limitOfWorld.y, 0f);
        _sprite.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        
    }

    void updatePosition()
    {
     /*   transform.position += new Vector3(0.1f, Mathf.Abs(0.5 * Mathf.Sin(1f * (float) Time.deltaTime)), 0f);
        */


            _index += Time.deltaTime;
        float x = transform.position.x + 0.1f;
        Debug.Log(_index);
        transform.position += new Vector3( 0.05f * Mathf.Sin(_index * 5), 0.1f, 0f);
       if (transform.position.y > _limitOfWorld.y)
        {
            initializePosition();
        }

        Debug.Log(_index);
        Debug.Log(_index % 2);
        if ( (_index % 1) < 0.6)
        {
            transform.localScale = new Vector3(0.5f, transform.localScale.y, 1f);
        } else{
            transform.localScale = new Vector3(1f, transform.localScale.y, 1f);
        }
            
    }

    // Update is called once per frame
    void Update () {
        updatePosition();
        



    }
}
