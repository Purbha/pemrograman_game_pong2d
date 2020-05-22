using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Digunakan untuk mengakses komponen-komponen untuk UI.

public class BallController : MonoBehaviour
{
    public int force; //Dengan lewat inspector, nilai ini untuk mengatur kecepatan gerak bola.
    Rigidbody2D rigid; //Menyimpan variabel rigidbody yang dapat dipanggil sewaktu-waktu.
    int scoreP1;
    int scoreP2;
    Text scoreUIP1; //Digunakan untuk menyimpan GameObject teks.
    Text scoreUIP2; //Digunakan untuk menyimpan GameObject teks.  
    GameObject panelSelesai; //Digunakan untuk meng-handle UI selesai.
    Text txPemenang;
    AudioSource audio; //Sebagai kontroler audio.
    public AudioClip hitSound; //Menyimpan berkas audio.

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>(); //Memanggil komponen Rigidbody2D ke dalam script.
        Vector2 arah = new Vector2(2, 0).normalized; //Menyatakan arah dari Force, yaitu 2 satuan ke kanan dan 0 satuan ke atas.
        rigid.AddForce(arah * force); //Melontarkan bola sesuai dengan arah dan kekuatan.
        scoreP1 = 0;
        scoreP2 = 0;
        //Digunakan untuk mengakses GameObject yang memiliki nama Score1 dan Score2.
        //Kemudian dari GameObject tersebut dicari komponen Text yang ada didalamnya yang kemudian disimpan ke scoreUIP1 dan ScoreUIP2.
        scoreUIP1 = GameObject.Find("Score1").GetComponent<Text>();
        scoreUIP2 = GameObject.Find("Score2").GetComponent<Text>();
        //Memanggil GameObject PanelSelesai yang terdapat di Hierarchy.Kemudian memastikan dalam keadaaan nonaktif.
        panelSelesai = GameObject.Find("PanelSelesai");
        panelSelesai.SetActive(false);
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D coll) //Ketika Bola mendeteksi suatu objek.
    {
        audio.PlayOneShot(hitSound);
        if (coll.gameObject.name == "TepiKanan") //Memastikan objek tersebut adalah TepiKanan
        {
            scoreP1 += 1;
            TampilkanScore();
            if (scoreP1 == 5) //Jika skor pemukul1 lebih dari 5.
            {
                panelSelesai.SetActive(true); //Halaman selesai ditampilkan.
                txPemenang = GameObject.Find("Pemenang").GetComponent<Text>(); //Cari komponen UI Text pada GameObject Pemenang.
                txPemenang.text = "Player Biru Pemenang"; //Tampilkan teks Player Biru Pemenang.
                Destroy(gameObject); //Hilangkan Bola.
                return; //Kode selesai dan tidak dilanjutkan untuk membaca kode selanjutnya.
            }
            ResetBall();
            Vector2 arah = new Vector2(2, 0).normalized; //Menentukan arah pergerakan bola. Nilai 2 berarti bola mengarah ke kanan.
            rigid.AddForce(arah * force); //Melontarkan bola berdasarkan arah dan kecepatan bola.
        }
        if (coll.gameObject.name == "TepiKiri")
        {
            scoreP2 += 1;
            TampilkanScore();
            if (scoreP2 == 5) //Jika skor pemukul2 lebih dari 5.
            {
                panelSelesai.SetActive(true); //Halaman selesai ditampilkan.
                txPemenang = GameObject.Find("Pemenang").GetComponent<Text>(); //Cari komponen UI Text pada GameObject Pemenang.
                txPemenang.text = "Player Merah Pemenang"; //Tampilkan teks Player Merah Pemenang.
                Destroy(gameObject); //Hilangkan Bola.
                return; //Kode selesai dan tidak dilanjutkan untuk membaca kode selanjutnya.
            }
            ResetBall();
            Vector2 arah = new Vector2(-2, 0).normalized; //Menentukan arah pergerakan bola. Nilai -2 berarti bola mengarah ke kiri.
            rigid.AddForce(arah * force); //Melontarkan bola berdasarkan arah dan kecepatan bola.
        }
        //Memastikan objek yang bersentuhan dengan bola adalah GameObject Pemukul1 dan Pemukul2.
        if (coll.gameObject.name == "Pemukul1" || coll.gameObject.name == "Pemukul2")
        {
            //Kemudian hitung seberapa kemiringan yang diberikan ke bola.
            float sudut = (transform.position.y - coll.transform.position.y) * 5f;
            //Menentukan arah bola yang akan dipantulkan.
            Vector2 arah = new Vector2(rigid.velocity.x, sudut).normalized;
            //Reset gerak bola (dengan kode ini, bola akan diam).
            rigid.velocity = new Vector2(0, 0);
            //Implementasikan arah, kekuatan lontar dan kecepatan setelah bola menyentuh Paddle.
            rigid.AddForce(arah * force * 2);
        }
    }

    void ResetBall()
    {
        transform.localPosition = new Vector2(0, 0); //Memposisikan bola pada tengah-tengah area.
        rigid.velocity = new Vector2(0, 0); //Tanpa memberi pergerakan pada bola.
    }

    void TampilkanScore()
    {
        //Digunakan untuk mengimplementasikan penampilan skor dengan memanggil fungsi TampilanScore().
        scoreUIP1.text = scoreP1 + "";
        scoreUIP2.text = scoreP2 + "";
    }

}
