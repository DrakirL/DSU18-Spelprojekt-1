using UnityEngine;

public class LightningDie : MonoBehaviour
{
    [SerializeField]
    private float despawnTime = 1f;
    private float despawnIn;

    // Start is called before the first frame update
    private void Start() => despawnIn = despawnTime;

    // Update is called once per frame
    private void Update()
    {
        if (Time.timeScale != 1)
        {
            return;
        }

        despawnIn -= Time.deltaTime;
        if (despawnIn > 0)
            return;


        Destroy(gameObject);

    }
}
