using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StreetGenerator : MonoBehaviour {
    public float ScrollSpeed;
    [Space]
    public List<GameObject> AvailabeStreetParts;
    public float PartSize;
    [Space]
    public int MaxCurrentParts = 3;
    public int GeneratedParts;
    public List<GameObject> CurrentStreetParts;
    public float DeleteDistance;


    // Use this for initialization
    void Start() {
        for (int i = 0; i < MaxCurrentParts; i++) {
            GeneratePart();
        }
    }

    private void GeneratePart() {
        GameObject part = Instantiate(AvailabeStreetParts[Random.Range(0, AvailabeStreetParts.Count)]);
        part.transform.SetParent(transform);
        if (CurrentStreetParts.Count > 0) {
            part.transform.localPosition = new Vector3(0, 0, CurrentStreetParts.Last().transform.localPosition.z + PartSize);
        }
        else {
            part.transform.localPosition = new Vector3(0, 0, PartSize * GeneratedParts);
        }
        CurrentStreetParts.Add(part);
        GeneratedParts++;
    }

    // Update is called once per frame
    void Update() {
        for (int i = 0; i < transform.childCount; i++) {
            GameObject part = transform.GetChild(i).gameObject;

            Vector3 newPos = part.transform.localPosition;
            newPos.z -= ScrollSpeed;

            part.transform.localPosition = Vector3.Lerp(part.transform.localPosition, newPos, Time.deltaTime * ScrollSpeed);
        }

        if (CurrentStreetParts[0].transform.localPosition.z < -DeleteDistance) {
            Destroy(CurrentStreetParts[0]);
            CurrentStreetParts.RemoveAt(0);
            GeneratePart();
        }
    }
}
