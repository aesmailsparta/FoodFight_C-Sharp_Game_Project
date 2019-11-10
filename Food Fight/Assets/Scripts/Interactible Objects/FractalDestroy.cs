using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FractalDestroy : MonoBehaviour
{
    public GameObject ExplosionCenter;

    void Awake()
    {
        Rigidbody[] rigidBodies = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rb in rigidBodies)
        {
            Debug.Log("exploding");
            rb.AddExplosionForce(200f, ExplosionCenter.transform.position, 1f);
        }

        StartCoroutine(CleanUp());
    }

    IEnumerator CleanUp()
    {
        yield return new WaitForSeconds(2f);

        MeshRenderer[] mRenderers = this.gameObject.GetComponentsInChildren<MeshRenderer>();
        float timer = 0.0f;
        float showTime = 3f;

        while (timer <= showTime)
        {
            foreach (MeshRenderer m in mRenderers)
            {
                timer += Time.deltaTime;

                Color color = m.material.color;

                color.a -= Mathf.Lerp(1f, 0f, timer / showTime);
                m.material.color = color;
            }

            yield return null;
        }

        Destroy(gameObject);
    }
}
