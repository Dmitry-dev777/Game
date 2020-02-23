using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HeroController : MonoBehaviour
{
    Rigidbody2D rb;
    public float Jump_height;
    private float Move;
    public float Run_speed;
    private Animator Anim_Player;
    private bool PlayerFace = true;
    private bool PlayerGround = true;
    Transform Ground;
    public LayerMask layerMask;
    public GameObject SpawnPos;
    public static int Player_Level;
    private SavePlayer SaveP = new SavePlayer();
    public Text AllGoldStats;

    private void Awake()
    {
        if(PlayerPrefs.HasKey("SaveP"))
        {
            SaveP = JsonUtility.FromJson<SavePlayer>(PlayerPrefs.GetString("SaveP"));
            GoldMiner.goldStack = SaveP.goldStack;
        }
    }
    void Start()
    {
        Anim_Player = GetComponent<Animator>();
        Ground = GameObject.Find(this.name + "/GroundPlatform").transform;
        rb = GetComponent<Rigidbody2D>();
        AllGoldStats.text = SaveP.goldStack.ToString();
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpPlayer();
        }
    }
    void FixedUpdate()
    {
        PlayerGround = Physics2D.Linecast(transform.position, Ground.position, layerMask);
        Move = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(Move * Run_speed, rb.velocity.y);
        if(Move != 0)
        {
            Anim_Player.SetBool("RunState", true);
        } else
        {
            Anim_Player.SetBool("RunState", false);
        }
        if(PlayerFace == false && Move > 0)
        {
            FaceTransfom();
        } else if(PlayerFace == true && Move < 0)
        {
            FaceTransfom();
        }
    }
    void JumpPlayer()
    {
        if(PlayerGround)
        {
            rb.AddForce(transform.up * Jump_height, ForceMode2D.Impulse);
        }
    }
    void FaceTransfom()
    {
        PlayerFace = !PlayerFace;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("GoldMine"))
        {
            GoldMiner.goldStack += 1;
            LevelTimer.Level_Timer += 3;
            SaveP.goldStack += 1;
            Destroy(collision.gameObject);
            AllGoldStats.text = SaveP.goldStack.ToString();
        }
        if (collision.tag.Equals("Respawn"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().path);
            LevelTimer.Level_Timer -= 10;
        }
        if(collision.tag.Equals("Level_0_Finish"))
        {
            SceneManager.LoadSceneAsync("Level[1]");
            Player_Level = 2;
        }
        if (collision.tag.Equals("Level_1_Finish"))
        {
            SceneManager.LoadSceneAsync("Level[2]");
            Player_Level = 3;
        }
        if (collision.tag.Equals("Level_2_Finish"))
        {
            PlayerPrefs.SetString("SaveP", JsonUtility.ToJson(SaveP));
            SceneManager.LoadSceneAsync(1);
            Player_Level = 1;
        }
    }
    public void OnApplicationQuit()
    {
        PlayerPrefs.SetString("SaveP", JsonUtility.ToJson(SaveP));
    }
    public class SavePlayer
    {
        public int goldStack;
    }
}
