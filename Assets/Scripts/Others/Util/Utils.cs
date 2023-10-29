using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    //随机生成长为r的向量，且z = 0
    public static Vector3 GenerateRandomVec(float r)
    {
        // 随机生成一个角度（0 到 360 度之间）
        float randomAngle = Random.Range(0f, 360f);

        // 将角度转化为弧度
        float radianAngle = randomAngle * Mathf.Deg2Rad;

        // 使用三角函数计算二维单位向量的 x 和 y 分量
        float x = Mathf.Cos(radianAngle);
        float y = Mathf.Sin(radianAngle);

        return new Vector3(x, y, 0) * r;
    }
}
