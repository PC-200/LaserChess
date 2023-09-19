
using System.Collections.Generic;

public interface AiBehaviour
{
    public Tile GetMove(Piece piece);
    public List<Piece> GetPiecesToAttack(Piece piece); 

}

