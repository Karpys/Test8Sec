
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public static class SaveSystem 
{
    public static void SavePlayer(RessourceManager ressource)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/ShipRessource.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        RessourceData data = new RessourceData(ressource);

        formatter.Serialize(stream,data);
        stream.Close();
    }

    public static RessourceData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/ShipRessource.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            RessourceData data = formatter.Deserialize(stream) as RessourceData;
            stream.Close();

            return data;

        }
        else
        {
            RessourceData data = new RessourceData();
            return data;
        }
    }
}
