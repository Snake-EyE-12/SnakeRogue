using System;
using UnityEngine;

public class GameSetup : MonoBehaviour
{
    
    [SerializeField] private BoardConstructor constructor;
    [SerializeField] private Camera cam;

    private Board board;
    private void Awake()
    {
        CreateBoard();
        cam.transform.position = (Vector3)board.Center + new Vector3(0, 0, -10);
    }

    [SerializeField] private MapLayout layout;
    private void CreateBoard()
    {
        board = constructor.BuildBoard(layout);
    }
    
    
    /*

Has a board

build the board out
scale, offset

using (layout)    ---> where to try to place tiles -> grid with size -> list of positions to try
using (tile)      ---> the tile with color and shape
using (place effector) ---> things that manipulate the tile placement -> chance % | 



effectos:

condition effects:  chance of placing | 
layout effects:     adds to edge | removes some | 
tile effects:       ground items : spikes, walls, lava, holes, material

On Game Start
-> Build Board using given layout, tile, list of effectors

Board = boardGuy.BuildBoard(Ilayout, Itile, Ieffectors)


scriptable layout:
List<effectors>

scriptable conditions:
List<effectors>

scriptable tiles:
List<effectors>


Map: 
       collection of layout options:             Square | Rect | Donut
                        effects:                 Edge Adder | Edge Remover
       tile options:                             Checkered | Random | Single
                        

picker: random, weighted


     */
}
