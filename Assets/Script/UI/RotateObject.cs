using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    // Start is called before the first frame update
 public GameObject targetObject; // Đối tượng muốn xoay
public float rotationSpeed = 5f; // Tốc độ xoay
private Quaternion targetRotation; // Góc xoay mục tiêu

void Start()
{
    targetRotation = targetObject.transform.rotation; // Lưu góc xoay ban đầu
}

void Update()
{
    // Kiểm tra các cử chỉ vuốt từ SwipeManager
    if (SwipeManager.swipeLeft)
    {
        // Thêm góc xoay cho trục Y khi vuốt sang trái
        targetRotation *= Quaternion.Euler(0, 90, 0);
    }
    if (SwipeManager.swipeRight)
    {
        // Thêm góc xoay cho trục Y khi vuốt sang phải
        targetRotation *= Quaternion.Euler(0, -90, 0);
    }

    // Nội suy giữa góc hiện tại và góc mục tiêu
    targetObject.transform.rotation = Quaternion.Lerp(
        targetObject.transform.rotation, 
        targetRotation, 
        Time.deltaTime * rotationSpeed
    );
}

}
