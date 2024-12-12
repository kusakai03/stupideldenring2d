using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow Instance { get; private set; }
    public float interpVelocity;
    private GameObject target;
    public Vector3 offset;
    Vector3 targetPos;
    private Camera cam;
    private float camsize = 6;
    private float newCamsize;
    private bool isShaking = false; // Trạng thái rung

    // Use this for initialization
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        cam = GetComponent<Camera>();
        targetPos = transform.position;
        newCamsize = camsize;
    }

    public void SetCamSize(float camsize)
    {
        newCamsize = camsize;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target && !isShaking) // Không di chuyển camera nếu đang rung
        {
            if (target.CompareTag("Player"))
            {
                if (target.GetComponent<PlayerMoving>().state == PlayerMoving.playerState.Dead)
                    target = null;
            }
            Vector3 posNoZ = transform.position;
            posNoZ.z = target.transform.position.z;
            Vector3 targetDirection = (target.transform.position - posNoZ);
            interpVelocity = targetDirection.magnitude * 5f;
            targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, targetPos + offset, 0.4f);
            if (cam.orthographic)
            {
                float distanceToTarget = targetDirection.magnitude;
                cam.orthographicSize = camsize;
                float steps = (camsize / 6);
                offset.y = steps * 0.2f;
            }
        }
        if (camsize < newCamsize) camsize += 0.1f;
        if (camsize > newCamsize) camsize -= 0.1f;
    }

    public void SetTarget(GameObject newTarget)
    {
        target = newTarget;
    }

    private void OnDisable()
    {
        target = null;
    }

    // Hàm rung camera
    public void ShakeScreen(float intensity, float duration = 0.3f)
    {
        StartCoroutine(Shake(intensity, duration));
    }

    private IEnumerator Shake(float intensity, float duration)
    {
        isShaking = true;
        Vector3 originalPos = transform.position; // Lưu vị trí gốc
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float offsetX = Random.Range(-1f, 1f) * intensity;
            float offsetY = Random.Range(-1f, 1f) * intensity;

            transform.position = new Vector3(originalPos.x + offsetX, originalPos.y + offsetY, originalPos.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Kết thúc rung, trả vị trí gốc
        isShaking = false;
        transform.position = originalPos;
    }
}
