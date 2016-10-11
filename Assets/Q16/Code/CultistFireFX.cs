using UnityEngine;
using System.Collections;

public class CultistFireFX : MonoBehaviour
{
    public GameObject impactPS;
    public SkinnedMeshRenderer mr;
    public ParticleSystem chestPS;
    CultistBehaviour cb;

    bool shouldFade = false;
    bool hasFired = false;
    bool hasSpawned = false;
    float fadeSpeed = 1.0f;
    float currentSlider = 0.0f;

    void Start ()
    {
        cb = GetComponent<CultistBehaviour>();
    }

    void Update ()
    {
        if (shouldFade)
        {
            if (currentSlider < 1.0f)
            {
                currentSlider += 1.0f * Time.deltaTime * fadeSpeed;
                mr.material.SetFloat("_FireSlider", currentSlider);
            }
            else if (currentSlider >= 1.0f && hasFired == false)
            {
                cb.SetOnFire();
                chestPS.gameObject.SetActive(true);
                hasFired = true;
            }
        }
    }

    public void StartFadingShader()
    {
        shouldFade = true;
    }

    public void SpawnImpactPS(Vector3 pos)
    {
        if (!hasSpawned)
        {
            pos = pos + new Vector3(0, 0.6f, 0);
            Instantiate(impactPS, pos, Quaternion.identity);
            hasSpawned = true;
        }
    }
}
