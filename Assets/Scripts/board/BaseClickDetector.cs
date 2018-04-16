using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseClickDetector : MonoBehaviour {

    public Texture2D tex;


    private IEnumerator OnMouseDown () {

        Component[] renders = GetComponentsInChildren<SpriteRenderer>();

        SpriteRenderer render = (SpriteRenderer) renders[0];
        Vector3 initialPosition = render.transform.position;
        render.transform.position = new Vector3(initialPosition.x+0.2f, initialPosition.y, initialPosition.z);
        //Wait for 2 seconds
        yield return new WaitForSeconds(0.01f);
        render.transform.position = new Vector3(initialPosition.x, initialPosition.y, initialPosition.z);
        yield return new WaitForSeconds(0.01f);
        render.transform.position = new Vector3(initialPosition.x - 0.1f, initialPosition.y, initialPosition.z);
        yield return new WaitForSeconds(0.01f);
        render.transform.position = new Vector3(initialPosition.x, initialPosition.y, initialPosition.z);

        SpriteRenderer render2 = (SpriteRenderer)renders[1];
        render2.sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
    }
}
