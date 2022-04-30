using System.Collections;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using UnityEngine;

public class SharedMemoryReader : MonoBehaviour
{
    // name of the shared memory
    [SerializeField] string sharedMemoryName = "MocapForAllFaceRaw";

    // size of the data from shared memory in bytes
    [SerializeField] int dataSizeInBytes = 468 * 3 * 4;

    private MemoryMappedFile mmf;
    private MemoryMappedViewAccessor accessor;
    private float[] buffer;
    private byte counterLast = 0;

    // Start is called before the first frame update
    void Start()
    {
        // initialize shared memory accessor
        mmf = MemoryMappedFile.CreateOrOpen(sharedMemoryName, 1 + 4 + dataSizeInBytes); // Total data size = 1(Data update counter) + 4(Data length) + n(Body). or mmf = MemoryMappedFile.OpenExisting(sharedMemoryName);
        accessor = mmf.CreateViewAccessor();

        buffer = new float[dataSizeInBytes / 4];
    }

    // Update is called once per frame
    void Update()
    {
        // check if updated
        byte counter = accessor.ReadByte(0);
        if (counter != counterLast)
        {
            counterLast = counter;

            // get data length
            int length = accessor.ReadInt32(1) / 4;
            if (length > buffer.Length)
            {
                buffer = new float[length];
            }

            // read data
            accessor.ReadArray<float>(5, buffer, 0, length);

            OutputData(buffer);
        }
    }

    public virtual void OutputData(float[] buffer)
    {
    }
}
