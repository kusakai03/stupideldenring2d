using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    private float lengthX, lengthY, startPosX, startPosY;
    public GameObject cam;
    public float parallaxEffectMultiplierX;
    public float parallaxEffectMultiplierY;

    void Start()
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;
        lengthX = GetComponent<SpriteRenderer>().bounds.size.x;
        lengthY = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void Update()
    {
        float tempX = (cam.transform.position.x * (1 - parallaxEffectMultiplierX));
        float distX = (cam.transform.position.x * parallaxEffectMultiplierX);

        float tempY = (cam.transform.position.y * (1 - parallaxEffectMultiplierY));
        float distY = (cam.transform.position.y * parallaxEffectMultiplierY);

        transform.position = new Vector3(startPosX + distX, startPosY + distY, transform.position.z);

        if (tempX > startPosX + lengthX) startPosX += lengthX;
        else if (tempX < startPosX - lengthX) startPosX -= lengthX;

        if (tempY > startPosY + lengthY) startPosY += lengthY;
        else if (tempY < startPosY - lengthY) startPosY -= lengthY;
    }
}
