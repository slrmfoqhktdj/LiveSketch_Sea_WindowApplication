void SkinningFunction(
   float3 position,
   float4 blendWeights,
   float4 blendIndices,
   float4x4 boneMatrixArray[100], // 본 행렬 배열
   out float3 skinnedPosition
)
{
    float4 skinnedPos = float4(0.0, 0.0, 0.0, 0.0);

    for (int i = 0; i < 4; i++)
    {
        int index = int(blendIndices[i]);
        skinnedPos += mul(boneMatrixArray[index], float4(position, 1.0)) * blendWeights[i];
    }

    skinnedPosition = skinnedPos.xyz;
}