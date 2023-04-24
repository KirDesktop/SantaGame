using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public bool isGround;

    private void OnTriggerStay2D(Collider2D collision)
    {
        isGround = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isGround = false;
    }
}
