using System.Collections.Generic;

[System.Serializable]
public class GameResult
{
    public string oyuncuAdi;
    public string oyuncuSoyadi;
    public int skor;
    public float maxKolAci;
    public string oynamaTarihi;
    public string kullanilanKol;
    public string oyunAdi;  // Oyun ad� eklendi
}

[System.Serializable]
public class GameResultsList
{
    public List<GameResult> results = new List<GameResult>();
}
