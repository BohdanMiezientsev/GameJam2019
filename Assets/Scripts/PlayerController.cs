using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Resources.Score;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private bool canMove;
    
    [SerializeField] private float stress = 0f;
    [SerializeField] private float time = 1000f;
    [SerializeField] private int itn;
    [SerializeField] private int speedMultiplier = 1;
    [SerializeField] private float stressMax = 1000f;
    [SerializeField] private float timeMax = 1000f;
    
    
    [SerializeField] private Spawner spawner;
    
    [SerializeField] private Transform firePoint;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private int damage;
    [SerializeField] private Bar stressBar;
    [SerializeField] private Bar timeBar;
    
    [SerializeField] private Canvas endGameCanvas;
    [SerializeField] private LoadScores loadScores;
    [SerializeField] private HandController hand;

    [SerializeField] private List<Sprite> faces;
    [SerializeField] private TextMeshProUGUI scoreText;
    
    private SpriteRenderer sprite;
    private Rigidbody2D rigidBody;
    private Laser laser;
    private float score { get; set; } = 0;
    private float t = 0;
    private float period = 4f;


    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
        laser = GetComponent<Laser>();
        endGameCanvas.enabled = false;
    }
    
    private void Start()
    {
        lineRenderer.enabled = false;
        
        stressBar.setValue(stress/stressMax);
        timeBar.setValue(time/timeMax);
        
    }

    private void Update()
    {
        
        t += Time.deltaTime;
        if (t >= period)
        {
            t = 0;
            spawner.SpawnRandomProjectile();
        }

        if(canMove) Move();

        if ((int) score / 10 > speedMultiplier && speedMultiplier<=10)
        {
            speedMultiplier++;
            speed = speedMultiplier;
        }
        
        if (Input.GetKey(KeyCode.E)) Shoot();
        else lineRenderer.enabled = false;

        if (stress >= stressMax)
        {
            itn++;
            stress /= 2;
            changeFace();
            hand.SetHand(itn-1);
        }

        if (stress < 0)
        {
            stress = 0;
        }
        if (time < 0)
        {
            time = 0;
        }
        if (time > 1000f)
        {
            time = 1000f;
        }
        
        stressBar.setValue(stress/stressMax);
        timeBar.setValue(time/timeMax);

        if (itn == 3)
            Die();
    }



    private void Move()
    {
        Vector3 movement = Vector3.right * speed * Time.deltaTime;
        score += movement.x;
        scoreText.text = (int)score + "";
        transform.Translate(movement);
        
        //float axis = Input.GetAxisRaw("");
       // if (axis == 0)
           float axis = Input.GetAxisRaw("Vertical");
        //Debug.Log(Input.GetAxis("Mouse ScrollWheel"));
        rigidBody.AddForce(Vector2.up * axis, ForceMode2D.Impulse);
    }
    
    private void Shoot()
    {
        
        if (time <= 0)
            return;
        time--;
        
        lineRenderer.enabled = true;
      
        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right);
        if (hitInfo)
        {
            hitInfo.collider.gameObject.GetComponent<Projectile>().ReceiveDamage(damage);
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, hitInfo.point);
        }
        else
        {
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, firePoint.position + firePoint.right * 100);
        }
    }

    public void Die()
    {
       
        endGameCanvas.enabled = true;
        loadScores.score = (int)score;
        Destroy(gameObject);

    }

    public void changeStress(int stress)
    {
        this.stress += stress;
        changeFace();
    }

    public void changeTime(int time)
    {
        this.time += time;
    }

    public void BounceFrom(bool topdown)
    {
        rigidBody.AddForce( (topdown ? Vector2.up : Vector2.down) * 5, ForceMode2D.Impulse );
    }

    private void changeFace()
    {
        if (stress >= 0 && stress < 150)
            sprite.sprite = faces[0];
        if (stress >= 150 && stress < 500)
            sprite.sprite = faces[1];
        if (stress >= 500 && stress < 750)
            sprite.sprite = faces[2];
        if (stress >= 750 && stress <= 1000)
            sprite.sprite = faces[3];
    }
    
    
}
