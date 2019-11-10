using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{

    TextMesh textMesh;
    private Color TextColor;

    public float showTime = 2.5f;
    private float offset;

    void Awake()
    {
        textMesh = this.GetComponent<TextMesh>();
        TextColor = textMesh.color;
        offset = Random.Range(-1f, 1f);
    }

    void Start()
    {
        StartCoroutine(Animate());
    }

    public void SetText(string text)
    {
        textMesh.text = text;
    }

    public void SetColor(Color color)
    {
        TextColor = color;
    }

    IEnumerator Animate()
    {
        TextMesh tm = this.GetComponent<TextMesh>();
        tm.color = TextColor;

        float timer = 0.0f;

        while (timer <= showTime)
        {
            transform.localPosition += Vector3.up * 2.5f * Time.deltaTime + (new Vector3(offset, 0, 0) * Time.deltaTime);

            //get the current color
            Color color = tm.color;
            //set the alpha and color
            color.a -= showTime * Time.deltaTime;
            tm.color = color;

            timer += Time.deltaTime;
            //wait till next frame
            yield return null;
        }

        Destroy(gameObject);
    }
}
