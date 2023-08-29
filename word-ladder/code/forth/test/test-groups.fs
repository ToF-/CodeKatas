\ test-groups.fs
REQUIRE ffl/tst.fs
REQUIRE ../src/groups.fs

CR .( groups ) CR

T{ .(   the nth group of a word is that word with ~ in place of the nth char. ) CR
    S" horse" 0 PAD S>NTH-GROUP S" ~orse" ?STR
    S" horse" 4 PAD S>NTH-GROUP S" hors~" ?STR
}T

T{ .(   given a char and a string, replace the nth char. ) CR
    S" horse" OVER CHAR m 0 ROT NTH-C! S" morse" ?STR
    S" horse" OVER CHAR u 2 ROT NTH-C! S" house" ?STR
}T

CR .( letter-set ) CR

T{ .(   an empty letter-set has no letters. ) CR
    LS-EMPTY PAD hex LS>S S" " ?STR
}T

T{ .(   a letter set can be added a letter.  ) CR
    CHAR a LS-EMPTY LS-ADD-LETTER PAD LS>S S" a" ?STR
}T

T{ .(   a letter set can be converted in the string of all its letters. ) CR
    CHAR a LS-EMPTY LS-ADD-LETTER
    CHAR g SWAP LS-ADD-LETTER
    CHAR b SWAP LS-ADD-LETTER
    PAD LS>S S" abg" ?STR 
    4807 PAD LS>S S" abkn" ?STR
}T

T{ .(   a word can be splitted into its nth group and letter. ) CR
    s" group" 0 PAD S>GROUP-LETTER CHAR g ?S S" ~roup" ?STR
}T

