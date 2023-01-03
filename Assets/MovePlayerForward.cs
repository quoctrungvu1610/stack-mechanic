using UnityEngine;

public class MovePlayerForward : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    private void Start() {
        Application.targetFrameRate = 60;
    }
    void Update()
    {
        transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime * speed);
    }
}
