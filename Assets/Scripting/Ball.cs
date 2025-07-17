using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    //Ball vaiables
    public enum CollisionTag
    {
        BounceWall,
        Player,
        ScoreWall,
    }
    [SerializeField] private float ballSpeed = 8f;
    [SerializeField] private List<string> collisionTags;
    private Vector2 ballDirection;
    private float ballDirectionRange = 1f;
    // Audio variables
    [SerializeField] public AudioSource ballAudioScource;
    [SerializeField] public AudioClip wallBounceAudio;
    [SerializeField] public AudioClip playerBounceAudio;
    [SerializeField] public AudioClip scoreAudio;


    /// <summary>
    /// Initializes the ball's position
    /// Pushes the ball in a random direction when spawned
    /// </summary>
    void Start()
    {
        transform.position = Vector2.zero;
        ballDirection = new Vector2(Random.Range(-ballDirectionRange, ballDirectionRange), Random.Range(-ballDirectionRange, ballDirectionRange)).normalized;
    }
    /// <summary>
    /// Grabs the AudioSource component from the ball
    /// Moves the ball in the direction it is facing
    /// Uses ball speed to determine how fast it moves
    /// Uses Time.deltaTime to make the speed consistent across different frame rates
    /// </summary>
    void Update()
    {
        ballAudioScource= GetComponent<AudioSource>();
        transform.Translate(ballDirection * ballSpeed * Time.deltaTime);
    }
    /// <summary>
    /// resets the ball's position to the center of the screen (0,0)
    /// Pushes the ball in a random direction
    /// </summary>
    private void ResetBall()
    {
        transform.position = Vector2.zero;
        ballDirection = new Vector2(Random.Range(-ballDirectionRange, ballDirectionRange), Random.Range(-ballDirectionRange, ballDirectionRange)).normalized;
    }
    /// <summary>
    /// Chechs if the ball collides with any of the walls, player, or goal
    /// Plays the appropriate audio clip based on the collision
    /// If goal is scored, resets the ball and increments the score for the player who scored
    /// If the ball collides with a wall it bounces off in the opposite direction
    /// If the ball collides with a player it will bounce off in the opposite direction it will also adjust the y direction based on the player's position
    /// The bal's direction is normalized to ensure consistent speed
    /// </summary>
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
