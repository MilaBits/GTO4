using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScrollingChainGenerator : MonoBehaviour {
    public float ScrollSpeed;
    [Space] public List<GameObject> AvailableParts;
    [Space] public float PartSize;
    public bool UseRandomSize;
    public float MinSize = 2;
    public float MaxSize = 20;
    [Space] public int MaxCurrentParts = 3;
    public int GeneratedParts;
    public List<GameObject> CurrentParts;
    public float DeleteDistance;
    public float distance = 0;

    // Use this for initialization
    void Start() {
        for (int i = 0; i < MaxCurrentParts; i++) {
            GeneratePart();
        }

        foreach (var currentPart in CurrentParts) {
            currentPart.transform.localPosition = new Vector3(currentPart.transform.localPosition.x,
                currentPart.transform.localPosition.y, currentPart.transform.localPosition.z - distance);
        }
    }

    private void GeneratePart() {
        GameObject part = Instantiate(AvailableParts[Random.Range(0, AvailableParts.Count)]);
        part.transform.SetParent(transform);

        if (UseRandomSize) {
            PartSize = Random.Range(MinSize, MaxSize);
        }

        if (CurrentParts.Count > 0) {
            //part.transform.localPosition = new Vector3(0, 0, CurrentParts.Last().transform.localPosition.z + PartSize);
            part.transform.localPosition = new Vector3(0, 0, CurrentParts.Last().transform.localPosition.z + PartSize);
        }
        else {
            part.transform.localPosition = new Vector3(0, 0, PartSize * GeneratedParts);
        }

        CurrentParts.Add(part);
        GeneratedParts++;
        distance += PartSize;
    }

    // Update is called once per frame
    void Update() {
        for (int i = 0; i < transform.childCount; i++) {
            GameObject part = transform.GetChild(i).gameObject;

            Vector3 newPos = part.transform.localPosition;
            newPos.z -= ScrollSpeed;

            part.transform.localPosition =
                Vector3.Lerp(part.transform.localPosition, newPos, Time.deltaTime * ScrollSpeed);
        }

        if (CurrentParts[0].transform.localPosition.z < -DeleteDistance) {
            Destroy(CurrentParts[0]);
            CurrentParts.RemoveAt(0);
            GeneratePart();
        }
    }
}