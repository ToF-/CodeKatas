require ffl/tst.fs
require wordladder.fs
PAGE

\ we settle for 3 letter words

3 LETTERS/WORD !

\ a word-key gives a unique numeric value for a string of 3, 4 or 5 letters
\ the word-key for the string ABC should have the binary value 00001 00010 00011
\ the word-key for the string CAT should have the binary value 00011 00001 10100
T{
    S" ABC"  S>WORD-KEY 2 BASE ! 000010001000011 ?S DECIMAL
    S" CAT"  S>WORD-KEY 2 BASE ! 000110000110100 ?S DECIMAL
}T

\ a word-key can be converted back into a string
\ (here with 4 letter words)

T{
    4 LETTERS/WORD !
    2 BASE !  00010000011010001000 DECIMAL
    PAD WORD-KEY>S  s" BATH" ?STR
}T

BYE

T{

    3 WORD-SIZE !
    0 MASK-POS 10 ?S
    1 MASK-POS  5 ?S
    2 MASK-POS  0 ?S

    5 WORD-SIZE !
    0 MASK-POS 20 ?S
    1 MASK-POS 15 ?S
    2 MASK-POS 10 ?S
    3 MASK-POS  5 ?S
    4 MASK-POS  0 ?S

    S" drone" 2DUP UPPERCASE! S" DRONE" ?STR

    CHAR A LETTER>INDEX 1 ?S
    CHAR Z LETTER>INDEX 26 ?S

    4 INDEX>LETTER CHAR D ?S
    20 INDEX>LETTER CHAR T ?S

    S" aaaaa" S>KEY S" aaaab" S>KEY <> ?TRUE
    S" cargo" S>KEY S" cargo" S>KEY = ?TRUE
    S" again" S>KEY PAD KEY>S PAD 5 S" AGAIN" ?STR

    S" CLERK" ADD-WORD
    S" AGAIN" ADD-WORD
    S" LOVER" ADD-WORD

    S" CLERK" IS-WORD? -1 ?S
    S" LOVER" IS-WORD? -1 ?S
    S" CROWN" IS-WORD? 0 ?S

    S" CROWN" S>KEY S" CROWD" S>KEY NEIGHBOR? ?TRUE
    S" CROWN" S>KEY S" BROWN" S>KEY NEIGHBOR? ?TRUE
    S" FROWN" S>KEY S" DROWN" S>KEY NEIGHBOR? ?TRUE
    S" BRAIN" S>KEY S" FRAIL" S>KEY NEIGHBOR? ?FALSE

    S" CROWN" S>KEY 4 MASK PAD KEY>S PAD 5 S" CROW@" ?STR
    S" CROWN" S>KEY 1 MASK PAD KEY>S PAD 5 S" C@OWN" ?STR
    S" CROWN" S>KEY 0 MASK PAD KEY>S PAD 5 S" @ROWN" ?STR

    S" CROWN" S>KEY 0 VAL:ROOT-KEY PAD KEY>S PAD 5 S" @ROWN" ?STR 3 ?S
    S" CROWN" S>KEY 1 VAL:ROOT-KEY PAD KEY>S PAD 5 S" C@OWN" ?STR 18 ?S
    S" CROWN" S>KEY 2 VAL:ROOT-KEY PAD KEY>S PAD 5 S" CR@WN" ?STR 15 ?S
    S" CROWN" S>KEY 3 VAL:ROOT-KEY PAD KEY>S PAD 5 S" CRO@N" ?STR 23 ?S
    S" CROWN" S>KEY 4 VAL:ROOT-KEY PAD KEY>S PAD 5 S" CROW@" ?STR 14 ?S

    S" @ROWN" S>KEY CONSTANT ROOTK
    ROOTK 2 0 ROOT-KEY|VAL PAD KEY>S PAD 5 S" BROWN" ?STR
    ROOTK 3 0 ROOT-KEY|VAL PAD KEY>S PAD 5 S" CROWN" ?STR
    ROOTK 4 0 ROOT-KEY|VAL PAD KEY>S PAD 5 S" DROWN" ?STR
    ROOTK 6 0 ROOT-KEY|VAL PAD KEY>S PAD 5 S" FROWN" ?STR
    ROOTK 7 0 ROOT-KEY|VAL PAD KEY>S PAD 5 S" GROWN" ?STR

}T
BYE
