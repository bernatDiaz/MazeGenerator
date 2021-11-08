using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public static class SaveSystem
{
    private static string fileType = ".dat";
    private static string touchFile = "/scoreTouch";
    private static string gyroFile = "/scoreGyro";
    private static string Path(CONTROL control, DIFICULTIES dificulty)
    {
        if (control == CONTROL.TOUCH)
            return Application.persistentDataPath + touchFile + dificulty.ToString() + fileType;
        else
            return Application.persistentDataPath + gyroFile + dificulty.ToString() + fileType;
    }
    public static bool SaveScore(float score, DIFICULTIES dificulty, CONTROL control, out int position)
    {

        string path = Path(control, dificulty);
        FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate);
        if (File.Exists(path) && fileStream.Length > 0)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            ScoreData scoreData = binaryFormatter.Deserialize(fileStream) as ScoreData;
            fileStream.Close();
            if (scoreData.IsTopScore(score))
            {
                FileStream fileStreamReplace = new FileStream(path, FileMode.Create);
                scoreData.AddScore(score, out position);
                binaryFormatter.Serialize(fileStreamReplace, scoreData);
                fileStreamReplace.Close();
                return true;
            }
            else
            {
                position = CONST.NOT_HIGHSCORE;
                return false;
            }
        }
        else
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            ScoreData scoreData = new ScoreData();
            scoreData.AddScore(score, out position);
            binaryFormatter.Serialize(fileStream, scoreData);
            fileStream.Close();
            return true;
        }
    }
    public static ScoreData LoadScore(CONTROL control, DIFICULTIES dificulty)
    {
        string path = Path(control, dificulty);
        FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate);
        if (File.Exists(path) && fileStream.Length > 0)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            ScoreData scoreData = binaryFormatter.Deserialize(fileStream) as ScoreData;
            fileStream.Close();
            return scoreData;
        }
        else
        {
            return new ScoreData();
        }
    }

    public static void ResetScores()
    {
        if (Application.isEditor)
        {
            ScoreData scoreData = new ScoreData();
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            foreach (CONTROL control in Enum.GetValues(typeof(CONTROL)))
                foreach(DIFICULTIES dificulty in Enum.GetValues(typeof(DIFICULTIES)))
                {
                    string path = Path(control, dificulty);
                    FileStream fileStream = new FileStream(path, FileMode.Create);
                    binaryFormatter.Serialize(fileStream, scoreData);
                    fileStream.Close();
                }
        }
    }
    /*
    public static bool CheckTopScore(float score, int dificulty)
    {
        string path = Application.persistentDataPath + "/score.dat";
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate);

        ScoreData scoreData = binaryFormatter.Deserialize(fileStream) as ScoreData;
        fileStream.Close();
        return scoreData.IsTopScore(score, dificulty);
    }
    */
}
