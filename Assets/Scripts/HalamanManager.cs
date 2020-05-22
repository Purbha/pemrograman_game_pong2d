using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Digunakan untuk mengakses method-method mengelola scene.

public class HalamanManager : MonoBehaviour
{
    //Digunakan untuk menentukan fungsi tombol Escape untuk kembali ke Menu atau ke Main(Gameplay).
    public bool isEscapeToExit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Ketika menekan tombol Escape,
        //jika nilai isEscapeToExit bernilai benar maka akan keluar dari Game
        //dan jika tidak maka akan kembali ke Menu.
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (isEscapeToExit)
            {
                Debug.Log("APP QUIT");
                Application.Quit();
            }
            else
            {
                KembaliKeMenu();
            }
        }
    }

    public void MulaiPermainan()
    {
        //Digunakan untuk berpindah ke halaman Main.
        SceneManager.LoadScene("SampleScene");
    }

    public void KembaliKeMenu() 
    {
        //Digunakan untuk berpindah ke halaman Menu.
        SceneManager.LoadScene("Menu");
    }

}
