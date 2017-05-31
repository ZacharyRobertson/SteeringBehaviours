using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;

[RequireComponent(typeof(Spawner))]
public class SpawnerXML : MonoBehaviour
{
    // Stores individual data associated with each spawned object
    public class SpawnerData
    {
        public Vector3 position;
        public Quaternion rotation;
    }
    [XmlRoot]
    public class XMLContainer
    {
       [XmlArray]
       public SpawnerData[] spawners;
    }

    public string fileName = "DefaultFileName";

    private Spawner spawner;
    private string fullPath;
   
    // data container for XML
    private XMLContainer data;

    // Saves XMLContainer instance to a file as XML format
    void SaveToPath(string path)
    {
        // Create a serializer of type XMLContainer
        XmlSerializer serializer = new XmlSerializer(typeof(XMLContainer));
        // Open a file stream at path using Create file mode
        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            // Serialize stream to data
            serializer.Serialize(stream, data);
        }
    }

    // Loads XML container from path (only run if the file exists)
    XMLContainer Load(string path)
    {
        //Create a serializer of type XML Container
        XmlSerializer serializer = new XmlSerializer(typeof(XMLContainer));
        // Open a file stream at path using Open file mode
        using (FileStream stream = new FileStream(path, FileMode.Open))
        {
            // Return the deserialized stream as XmlContainer
            return serializer.Deserialize(stream) as XMLContainer;
        }
    }
    // saves whatever data value is to Xml File
    public void Save()
    {
        // Set data to new data
        data = new XMLContainer();
        // Set objects to objects in spawner
        List<GameObject> objects = spawner.objects;
        // Set data.spawners to new SpawnerData [objects.count]
        data.spawners = new SpawnerData[objects.Count];
        // For i= 0 to objects.count
        for (int i = 0; i < objects.Count; i++)
        
        {
            // Set spawnerData to new SpawnerData
            SpawnerData spawnerData = new SpawnerData();
            // set item to objects [i]
            GameObject item = objects[i];
            // Set spawner's position to position
            spawnerData.position = transform.position;
            // Set spawner's rotation to rotation
            spawnerData.rotation = transform.rotation;
            // Set data.spawners[i] = spawnerData
            data.spawners[i] = spawnerData;
        }
        // Call SaveToPath(fullPath)
        SaveToPath(fullPath);
    }
    // Applies the save data to scene using Spawner
    void Apply()
    {
        // Set spawners to data.spawners
        SpawnerData[] spawners = data.spawners;
        // For i = 0 to spawners.Length
        for (int i = 0; i < spawners.Length; i++)
        {
            // Set data to spawners[i]
            SpawnerData data = spawners[i];
            // Call spawner.Spawn() and pass data.position, data.rotation
            spawner.Spawn(data.position, data.rotation);
        }

    }


    void Awake()
    {
        // Set spawner to spawner component
        spawner = GetComponent<Spawner>();
    }
    // Use this for initialization
    void Start()
    {
        // Set fullPath to application.dataPath + "/" + fileName + ".xml";
        fullPath = Application.dataPath + "/" + fileName + ".xml";
        // If File exists at fullPath
        if(fullPath != null)
        {
            // Set Data to Load(fullPath)
            data = Load(fullPath);
            // Call Apply
            Apply();
        }
    }

   
}
