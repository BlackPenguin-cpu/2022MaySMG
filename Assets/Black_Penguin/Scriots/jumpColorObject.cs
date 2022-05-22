using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using System.Threading.Tasks;
using Utils;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class jumpColorObject : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    //public Vector3 moveSpeed;
    Rigidbody2D rigid;
    public Vector2 jumpHeight;
    public GameObject particle;
    public GameObject deadCell;
    public FloatReference greenValue;
    public AudioClip clip;

    public List<Sprite> sprites;

    public FloatReference difficulty;

    float colorValue;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.AddForce(new Vector2(Random.Range(0, 2) == 0 ? jumpHeight.x : -jumpHeight.x, jumpHeight.y), ForceMode2D.Impulse);
        rigid.AddTorque(Random.Range(-30, 30));

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Random.ColorHSV();
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Count)];

        float minusColor = (spriteRenderer.color.r + spriteRenderer.color.b) / 2;

        colorValue += spriteRenderer.color.g - minusColor;
        if (colorValue > 0)
        {
            spriteRenderer.color += new Color(-1, 0, -1);
            particle.SetActive(true);
        }
    }

    void Update()
    {
        if (transform.position.y < -6)
            Destroy(gameObject);
        if (GreenObjectManager.instance.isClicked)
            ClickCheck();
    }
    void ClickCheck()
    {
        RaycastHit2D[] obj = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward);
        foreach (RaycastHit2D hit in obj)
        {
            if (hit.transform.GetComponent<jumpColorObject>() == this)
            {
                OnClicked();
            }
        }
    }
    void OnClicked()
    {
        greenValue.Value += colorValue * 40f / difficulty.Value;
        if (colorValue > 0)
        {
            GreenObjectManager.instance._comboCount++;
            GameObject obj = Instantiate(deadCell, transform.position, Quaternion.identity);
            obj.GetComponent<SpriteRenderer>().color = new Color(0.6509804f, 0.8862745f, 0.3803922f);
        }
        else
        {
            CameraShake();
            GreenObjectManager.instance._comboCount = 0;
        }
        SoundManager.Instance.PlayDuplicatedSFXAsync(clip);
        Destroy(gameObject);
    }
    public async void CameraShake()
    {
        float duration = 0.5f;
        Vector3 pos = new Vector3(0, 0, -10);
        while (duration > 0)
        {
            Camera.main.transform.localPosition = pos;
            Camera.main.transform.localPosition = (Vector3)(Random.insideUnitCircle) / 3 + pos;

            await Task.Delay(1);
            duration -= Time.deltaTime;
        }
        Camera.main.transform.localPosition = pos;
    }
}
