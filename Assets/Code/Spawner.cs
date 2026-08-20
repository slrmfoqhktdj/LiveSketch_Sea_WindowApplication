using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class Spawner : MonoBehaviour
{
    public static Transform[] SharkspawnPoint;
    public static Transform[] StringrayspawnPoint;
    public static Transform[] SquidspawnPoint;
    public static Transform[] TunaspawnPoint;

    public CreateModel[] creation;
    // public GameObject creation;

    public static Action<int,string> create;

    private bool c_t = false;
    private int number = 0;
    private string base64 = "";

    private int spawnNumber = 0;

    private GameObject creature;

    private Creation_Shark shark_creation_funtion;
    private Creation_Stringray stringray_creation_funtion;
    private Creation_Squid squid_creation_funtion;
    private Creation_Tuna tuna_creation_funtion;

    private void Awake()
    {
        create = (i,j) => {
            number = i;
            base64 = j;
            c_t = true;
        };

    }

    void Start(){
        SharkspawnPoint = SharkStartPoints.startpoints;
        StringrayspawnPoint = StringrayStartPoints.startpoints;
        SquidspawnPoint = SquidStartPoints.startpoints;
        TunaspawnPoint = TunaStartPoints.startpoints;
    }

    void Update(){
        if(c_t){
            // int randomInt = UnityEngine.Random.Range(0,3);
            int randomInt = returnNumber();
            switch(number){
                case 0:
                    
                    creature  = (GameObject)Instantiate(creation[0].creation, SharkspawnPoint[randomInt].position, SharkspawnPoint[randomInt].rotation);
                    shark_creation_funtion = creature.GetComponent<Creation_Shark>();
                    shark_creation_funtion.MyWayPoints(randomInt);
                    

                    try{
                        // base64 받아와서 byte 변환
                        byte[]  imageBytes = Convert.FromBase64String(base64);
                        // texture2d 생성
                        Texture2D tex = new Texture2D(1, 1);
                        tex.LoadImage( imageBytes );

                        // 이미지 회전
                        Texture2D r_tex = RotateTexture(tex,270);

                        // 반전 이미지
                        Texture2D tex_f = FlipTexture(r_tex);

                        // 이미지 합치기
                        Texture2D final_tex = CombineTextures(r_tex, tex_f);
                        









                        // tex = Rotate(tex);

                        //그 하위에 다른 오브젝트있을시 그 이전 오브젝트 삭제
                        // if(CreationSpawnPoints[spawnNumber].transform.childCount !=0)
                        // {
                        //     Destroy(CreationSpawnPoints[spawnNumber].transform.GetChild(0).gameObject);
                        // }


                        //포인트의 자식으로 들어가게 생성- true를 사용하면 자식 개체가 현재 월드 위치를 유지함. false를 사용하면 부모에 상대적인 위치가 지정됨
                        // creature.transform.SetParent(CreationSpawnPoints[spawnNumber].transform,false);

                        // Material material1 = new Material(Shader.Find("Shader Graphs/CreationSurface"));
                        // material1.SetTexture("_Texture2D",tex);

                        // Debug.Log("getChild = " + creature.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material.SetTexture("_Texture2D",tex));
                        // creature.transform.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().material = material1;
                        creature.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material.SetTexture("_Texture2D", final_tex);
                        Debug.Log("Image Set");
                        //이미지 파일 생성
                        //System.IO.File.WriteAllBytes("Assets/Image/"+"img"+1+".png",imageBytes);
                        System.IO.File.WriteAllBytes("Assets/Image/" + "img" + 1 + ".png", final_tex.EncodeToPNG());



                        // System.IO.File.WriteAllBytes("Assets/Image/"+"img"+1+".png",tex.EncodeToPNG());
                        // cnt ++;

                    }catch(Exception ex){
                        Debug.Log(ex);
                    }

                    c_t= false;
                break;
                case 1:
                    creature  = (GameObject)Instantiate(creation[1].creation, StringrayspawnPoint[randomInt].position, StringrayspawnPoint[randomInt].rotation);
                    stringray_creation_funtion = creature.GetComponent<Creation_Stringray>();
                    stringray_creation_funtion.MyWayPoints(randomInt);
                    
                    c_t= false;
                break;
                case 2:
                    creature  = (GameObject)Instantiate(creation[2].creation, SquidspawnPoint[randomInt].position, SquidspawnPoint[randomInt].rotation);
                    squid_creation_funtion = creature.GetComponent<Creation_Squid>();
                    squid_creation_funtion.MyWayPoints(randomInt);
                    
                    c_t= false;
                break;
                case 3:
                    creature  = (GameObject)Instantiate(creation[3].creation, TunaspawnPoint[randomInt].position, TunaspawnPoint[randomInt].rotation);
                    tuna_creation_funtion = creature.GetComponent<Creation_Tuna>();
                    tuna_creation_funtion.MyWayPoints(randomInt);
                    
                    c_t= false;
                break;
                default:
                    Debug.Log("생성되지 않는 크리쳐");
                break;
            }
        }
    }

    int returnNumber(){        
        if(spawnNumber >= 2){
            spawnNumber = 0;
        }
        else{
            spawnNumber++;
        }

        return spawnNumber;
    }

    private Texture2D Rotate(Texture2D image)
    {
        Debug.Log("Rotate 90");
        Texture2D Tex2D = new Texture2D(image.height, image.width);

        for (int y = 0; y < image.height; ++y)
        {
            for (int x = 0; x < image.width; ++x)
            {
                Tex2D.SetPixel(y, image.width - 1 - x, image.GetPixel(x, y));
            }
        }
        Tex2D.Apply();
        return Tex2D;
    }

    private Texture2D FlipTexture(Texture2D original)
    {
        Texture2D f_t = new Texture2D(original.width, original.height);

        for(int y = 0; y < original.height; y ++)
        {
            for( int x = 0; x< original.width; x++)
            {
                f_t.SetPixel(original.width - 1 - x, y, original.GetPixel(x, y));
            }
        }

        f_t.Apply();
        return f_t;
    }

    private Texture2D CombineTextures(Texture2D original, Texture2D flipped)
    {
        Texture2D combinedTexture = new Texture2D(original.width * 2, original.height);


        // 반전 이미지 복사
        Color[] flippedPixels = flipped.GetPixels();
        combinedTexture.SetPixels(0, 0, flipped.width, flipped.height, flippedPixels);

        // 원본 이미지 복사
        Color[] originalPixels = original.GetPixels();
        combinedTexture.SetPixels(flipped.width, 0, original.width, original.height, originalPixels);


        combinedTexture.Apply(); // 변경 사항을 적용
        return combinedTexture;
    }


     public Texture2D RotateTexture(Texture2D original, float angle)
    {
        // 회전 각도를 라디안으로 변환
        float radians = angle * Mathf.Deg2Rad;
        
        // 회전 후의 너비와 높이 계산
        int width = original.width;
        int height = original.height;
        
        // 회전 후의 크기 계산
        int newWidth = Mathf.RoundToInt(Mathf.Abs(width * Mathf.Cos(radians)) + Mathf.Abs(height * Mathf.Sin(radians)));
        int newHeight = Mathf.RoundToInt(Mathf.Abs(width * Mathf.Sin(radians)) + Mathf.Abs(height * Mathf.Cos(radians)));

        // 새로운 텍스처 생성
        Texture2D rotatedTexture = new Texture2D(newWidth, newHeight);
        Color[] pixels = new Color[newWidth * newHeight];

        // 원본 텍스처의 픽셀 색상 가져오기
        Color[] originalPixels = original.GetPixels();

        // 회전 중심
        Vector2 originalCenter = new Vector2(width / 2f, height / 2f);
        Vector2 newCenter = new Vector2(newWidth / 2f, newHeight / 2f);

        for (int y = 0; y < newHeight; y++)
        {
            for (int x = 0; x < newWidth; x++)
            {
                // 새로운 픽셀 위치를 원본 픽셀 위치로 변환
                Vector2 originalPos = RotatePoint(new Vector2(x, y), newCenter, originalCenter, radians);
                
                // 원본 텍스처의 픽셀 색상 설정
                if (originalPos.x >= 0 && originalPos.x < width && originalPos.y >= 0 && originalPos.y < height)
                {
                    pixels[y * newWidth + x] = originalPixels[(int)(originalPos.y) * width + (int)(originalPos.x)];
                }
                else
                {
                    pixels[y * newWidth + x] = Color.clear; // 범위를 벗어난 경우 투명하게 설정
                }
            }
        }

        // 회전된 텍스처의 픽셀 색상 설정
        rotatedTexture.SetPixels(pixels);
        rotatedTexture.Apply();

        return rotatedTexture;
    }

    private static Vector2 RotatePoint(Vector2 point, Vector2 center, Vector2 originalCenter, float radians)
    {
        // 회전 변환
        float cos = Mathf.Cos(radians);
        float sin = Mathf.Sin(radians);

        // 원점 이동
        point -= center;
        
        // 회전 적용
        float xNew = point.x * cos - point.y * sin;
        float yNew = point.x * sin + point.y * cos;

        // 원점 복원
        return new Vector2(xNew, yNew) + originalCenter;
    }

}




