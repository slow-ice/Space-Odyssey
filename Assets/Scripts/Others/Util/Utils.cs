using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    //������ɳ�Ϊr����������z = 0
    public static Vector3 GenerateRandomVec(float r)
    {
        // �������һ���Ƕȣ�0 �� 360 ��֮�䣩
        float randomAngle = Random.Range(0f, 360f);

        // ���Ƕ�ת��Ϊ����
        float radianAngle = randomAngle * Mathf.Deg2Rad;

        // ʹ�����Ǻ��������ά��λ������ x �� y ����
        float x = Mathf.Cos(radianAngle);
        float y = Mathf.Sin(radianAngle);

        return new Vector3(x, y, 0) * r;
    }
}
