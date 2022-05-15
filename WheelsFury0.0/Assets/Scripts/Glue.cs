using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CCPractice
{
    public class Glue : MonoBehaviour
    {
        Transform carRoot;
        Transform box;
        Rigidbody sphereRB;

        float restSqrDistance;
        float maxSqrDistance;
        [SerializeField] float maxStretch;
        [SerializeField] float maxForce;
        private void Awake()
        {
            carRoot = transform.root;
            sphereRB = carRoot.GetComponentInChildren<SphereCollider>().gameObject.GetComponent<Rigidbody>();
            box = carRoot.GetComponentInChildren<BoxCollider>().transform;
        }
        // Start is called before the first frame update
        void Start()
        {
            restSqrDistance = Vector3.Magnitude(box.position - sphereRB.position);
            maxSqrDistance = restSqrDistance + maxStretch;
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 spherePosition = sphereRB.transform.position;
            Quaternion sphereRotation = sphereRB.transform.rotation;
            carRoot.position = sphereRB.transform.position;
            carRoot.rotation = Quaternion.Euler(carRoot.rotation.eulerAngles.x, sphereRotation.eulerAngles.y, carRoot.rotation.eulerAngles.z);
            sphereRB.transform.position = spherePosition;
            sphereRB.transform.rotation = sphereRotation;
        }

        private void FixedUpdate()
        {
            float stretch = Vector3.Magnitude(box.position - sphereRB.position) - restSqrDistance;
            float force = stretch * (maxForce / maxStretch);
            Vector3 forceDir = box.position - sphereRB.position;
            sphereRB.AddForce(force * forceDir);
        }
    }
}
