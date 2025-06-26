using UnityEngine;

public class GuidePanelController : MonoBehaviour
{
    [Header("Halaman Panduan")]
    public GameObject[] guidePanels; // Isi dengan GuidePanel_0, GuidePanel_1, dst

    [Header("Panel Background")]
    public GameObject backgroundPanel; // Isi dengan GameObject 'Background' (yang berisi PlayBtn, TipsBtn, dll)

    private int currentPage = 0;

    void Start()
    {
        // Sembunyikan semua panel panduan saat game mulai
        foreach (var panel in guidePanels)
        {
            panel.SetActive(false);
        }

        // Pastikan background (menu utama) terlihat
        if (backgroundPanel != null)
            backgroundPanel.SetActive(true);
    }

    // Menampilkan halaman pertama
    public void OpenGuide()
    {
        ShowPage(0);

        // Sembunyikan tombol-tombol menu utama
        if (backgroundPanel != null)
            backgroundPanel.SetActive(false);
    }

    // Menampilkan halaman ke-X
    public void ShowPage(int index)
    {
        for (int i = 0; i < guidePanels.Length; i++)
        {
            guidePanels[i].SetActive(i == index);
        }

        currentPage = index;
    }

    // Halaman berikutnya
    public void NextPage()
    {
        if (currentPage < guidePanels.Length - 1)
            ShowPage(currentPage + 1);
    }

    // Halaman sebelumnya
    public void PreviousPage()
    {
        if (currentPage > 0)
            ShowPage(currentPage - 1);
    }

    // Menutup semua halaman panduan dan kembali ke menu
    public void CloseAll()
    {
        foreach (var panel in guidePanels)
        {
            panel.SetActive(false);
        }

        if (backgroundPanel != null)
            backgroundPanel.SetActive(true);
    }
}
