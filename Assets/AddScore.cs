using UnityEngine;
using TMPro;

public class AddScore : MonoBehaviour
{
    [SerializeField]
    private ScoreManager _scoreManager;
    private Vector3 _initialPosition;
    public int Score;

    private void Start()
    {
        if (_scoreManager == null)
        {
            _scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        }

        if (_scoreManager == null)
        {
            Debug.LogError("ScoreManager component not found!");
        }

        _initialPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            print(message: "+1");
            _scoreManager.AddScore();

            gameObject.SetActive(false);

            Invoke("RespawnObject", 2f);
        }
    }

    void RespawnObject()
    {
        transform.position = _initialPosition;
        gameObject.SetActive(true);
    }
}