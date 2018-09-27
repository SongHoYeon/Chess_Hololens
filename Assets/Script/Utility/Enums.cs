namespace Enums
{
    public enum PieceType
    {
        King = 0,//In Field
        Sha,//In Field
        Cha,//Every, Field
        Pho,//Every, Field, Need Jump(Not Pho Either)
        Mha,//Front2 & LR1
        Shang,//Front3 & LR2
        Jol//Front1, LR1
    }

    public enum PieceSetting
    {
        King = 0,
        Sha1,
        Sha2,
        Cha1,
        Cha2,
        Pho1,
        Pho2,
        Mha1,
        Mha2,
        Shang1,
        Shang2,
        Jol1,
        Jol2,
        Jol3,
        Jol4,
        Jol5
    }

    public enum Player
    {
        Player1 = 0,
        Player2,
    }
}