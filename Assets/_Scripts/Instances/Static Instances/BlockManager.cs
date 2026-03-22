using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    [SerializeField]
    private protected Dictionary<int, GameObject> blockDatabase = new Dictionary< int, GameObject>();

    private protected GameObject currentHandledBlock;
    private protected BlockBehavioursAux currentHandledBlockBehaviours;

    private void Setup()
    {
        foreach (var block in blockDatabase.Values)
        {
            currentHandledBlock = block;
            currentHandledBlockBehaviours = currentHandledBlock.GetComponent<BlockBehavioursAux>();

            currentHandledBlockBehaviours._onBlockStateChange += UpdateBlockState;
        }
    }

    private void UpdateBlockState()
    {
        if(currentHandledBlockBehaviours.blockData.health==0) DestroyBlock(1);

    }

    private void DestroyBlock(int index)
    {
        currentHandledBlock = blockDatabase[index];
        currentHandledBlockBehaviours = currentHandledBlock.GetComponent<BlockBehavioursAux>();

        currentHandledBlockBehaviours.Despawn();
        blockDatabase.Remove(index);
    }
}
