using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public enum CollisionTag
    {
        ScoreWall,
        BounceWall,
        Player
    }
    
    [SerializeField] private float ballSpeed = 8f;
    [SerializeField] private List<string> collisionTags;

    private Vector2 ballDirection;

    [SerializeField] private AudioSource aS;
    [SerializeField] private AudioClip clip1;
    [SerializeField] private AudioClip clip2;
    [SerializeField] private AudioClip clip3;
    void Start()
    {
        transform.position = Vector2.zero;
        ballDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    void Update()
    {
        transform.Translate(ballDirection * ballSpeed * Time.deltaTime);
    }

    private void ResetBall()
    {
        transform.position = Vector2.zero;
        ballDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(tags[(int)CollisionTag.ScoreWall]))
        {
            ResetBall();
        }
        else if (other.CompareTag(tags[(int)CollisionTag.BounceWall]))
        {
            ballDirection.y = -ballDirection.y;
        }
        else if (other.CompareTag(tags[(int)CollisionTag.Player]))
        {
            ballDirection.x = -ballDirection.x;
            ballDirection.y = transform.position.y - other.transform.position.y;
            ballDirection = ballDirection.normalized;
        }
    }
}
