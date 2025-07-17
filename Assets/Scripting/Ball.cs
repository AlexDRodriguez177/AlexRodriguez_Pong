using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public enum CollisionTag
    {
        BounceWall,
        Player,
        ScoreWall,
    }
    
    [SerializeField] private float ballSpeed = 8f;
    [SerializeField] private List<string> collisionTags;
    
    private Vector2 ballDirection;

    [SerializeField] public AudioSource ballAudioScource;
    [SerializeField] public AudioClip wallBounceAudio;
    [SerializeField] public AudioClip playerBounceAudio;
    [SerializeField] public AudioClip scoreAudio;
    void Start()
    {
        transform.position = Vector2.zero;
        ballDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    void Update()
    {
        ballAudioScource= GetComponent<AudioSource>();
        transform.Translate(ballDirection * ballSpeed * Time.deltaTime);
    }

    private void ResetBall()
    {
        transform.position = Vector2.zero;
        ballDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(collisionTags[(int)CollisionTag.ScoreWall]))
        {
            ballAudioScource.PlayOneShot(scoreAudio);
            ResetBall();
            GameManager.IncrementScore(other.GetComponent<ScoreWall>().scoringPlayer);
        }
        
        else if (other.CompareTag(collisionTags[(int)CollisionTag.BounceWall]))
        {
            ballAudioScource.PlayOneShot(wallBounceAudio);
            ballDirection.y = -ballDirection.y;
        }
        else if (other.CompareTag(collisionTags[(int)CollisionTag.Player]))
        {
            ballAudioScource.PlayOneShot(playerBounceAudio);
            ballDirection.x = -ballDirection.x;
            ballDirection.y = transform.position.y - other.transform.position.y;
            ballDirection = ballDirection.normalized;
        }
    }
}
