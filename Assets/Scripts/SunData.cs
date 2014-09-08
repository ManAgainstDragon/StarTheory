using UnityEngine;
using System.Collections;


public class SunData : MonoBehaviour {

    public int id = -1;

    public float mass = -1;         //1 unit is equal to earth's mass, 1 EM = 5974.2 * 10^21 kg
    public float surfaceTemp = -1;  //In Kelvins
    public float coreTemp = -1;
    public float luminosity = -1;   //1 equals luminosity of sun
    public float lifetime = -1;     //In mln of years
    public float year = -1;         //How long, in seconds, it takes to make full spin

    public Color frameColor = new Color(255, 255, 255);
    public Color frameColorOver = new Color(255, 0, 255);

    private GUITexture frame;
    public Texture frameTexture;

    private bool isMouseOver;
    
	void Start () {
        isMouseOver = false;

        if (id == -1)           id              = 0;
	    if (mass == -1)         mass            = 332950.0f;
        if (year == -1)         year            = 365.25f;
        if (surfaceTemp == -1)  surfaceTemp     = 5778.0f;
        if (coreTemp == -1)     coreTemp        = 15710000.0f;
        if (luminosity == -1)   luminosity      = 1.0f;
        if (lifetime == -1)     lifetime        = 10000.0f;

        frame = new GameObject("sunFrame").AddComponent<GUITexture>();
        frame.transform.parent = this.transform;
        frame.transform.rotation = Quaternion.identity;
        frame.transform.localScale = new Vector3(0.0f, 0.0f, 1.0f);
        frame.texture = frameTexture;
        frame.enabled = false;


	}
	
	void Update () {
        DrawFrame();
        RotateSun();
	}

    void OnMouseEnter()
    {
        isMouseOver = true;
    }

    void OnMouseExit()
    {
        isMouseOver = false;
    }

    void DrawFrame()
    {
        if (IsSunVisible())
        {
            Vector2 framePos = Camera.main.WorldToViewportPoint(this.transform.position);
            Vector2 min = Camera.main.WorldToScreenPoint(collider.bounds.min);
            Vector2 max = Camera.main.WorldToScreenPoint(collider.bounds.max);
            float w = max.x - min.x;
            float h = max.y - min.y;

            frame.transform.position = framePos;
            frame.transform.rotation = Quaternion.identity;
            frame.pixelInset = new Rect(w / -2.0f, h / -2.0f, w, h);
            if (isMouseOver)
            {
                frame.color = frameColorOver;
            }
            else
            {
                frame.color = frameColor;
            }

            frame.enabled = true;
        }
        else frame.enabled = false;
    }

    bool IsSunVisible()
    {
        Vector2 framePos = Camera.main.WorldToViewportPoint(this.transform.position);
        Vector2 min = Camera.main.WorldToViewportPoint(collider.bounds.min);
        Vector2 max = Camera.main.WorldToViewportPoint(collider.bounds.max);

        Vector2 boundsMin = new Vector2(0.0f, 0.0f);
        Vector2 boundsMax = new Vector2(1.0f, 1.0f);

        if (framePos.x < boundsMax.x && framePos.x > boundsMin.x && framePos.y < boundsMax.y && framePos.y > boundsMin.y) return true;
        if (min.x < boundsMax.x && min.x > boundsMin.x && min.y < boundsMax.y && min.y > boundsMin.y) return true;
        if (max.x < boundsMax.x && max.x > boundsMin.x && max.y < boundsMax.y && max.y > boundsMin.y) return true;

        return false;
    }

    void RotateSun()
    {
        transform.Rotate(Vector3.up, (1000/year) * Time.deltaTime);
    }
}
