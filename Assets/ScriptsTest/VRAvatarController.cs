using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRAvatarController : MonoBehaviour
{
    [SerializeField] private Transform leftControllerTransform;
    [SerializeField] private Transform rightControllerTransform;
    [SerializeField] private Transform cameraTransform;

    [SerializeField] private Transform leftTargetTransform;
    [SerializeField] private Transform rightTargetTransform;
    [SerializeField] private Transform cameraTargetTransform;

    private void Update()
    {
        leftTargetTransform.position = leftControllerTransform.position;
        rightTargetTransform.position = rightControllerTransform.position;
        cameraTargetTransform.position = cameraTransform.position;

        leftTargetTransform.rotation = ChangeArmAngle(leftControllerTransform.rotation);
        rightTargetTransform.rotation = ChangeArmAngle(rightControllerTransform.rotation);
        cameraTargetTransform.rotation = Quaternion.Euler(0, cameraTransform.rotation.eulerAngles.y, 0);
    }

    public Quaternion ChangeArmAngle(Quaternion rotation) =>
        rotation * Quaternion.Euler(-90, 180, 0);
}
