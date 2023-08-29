\ test-groups.fs
REQUIRE ffl/tst.fs
REQUIRE ../src/groups.fs

CR .( groups ) CR

T{ .(   the nth group of a word is that word with ~ in place of the nth char. ) CR
    S" horse" 0 PAD S>NTH-GROUP PAD COUNT S" ~orse" ?STR
    S" horse" 4 PAD S>NTH-GROUP PAD COUNT S" hors~" ?STR
}T

T{ .(   given a char and a string, replace the nth char. ) CR
    S" horse" OVER CHAR m 0 ROT NTH-C! S" morse" ?STR
    S" horse" OVER CHAR u 2 ROT NTH-C! S" house" ?STR
}T

CR .( letter-set ) CR

T{ .(   an empty letter-set has no letters. ) CR
    LS-EMPTY PAD LS>S PAD COUNT S" " ?STR
}T

T{ .(   a letter set can be added a letter.  ) CR
    CHAR a LS-EMPTY LS-ADD-LETTER PAD LS>S PAD COUNT S" a" ?STR
}T

T{ .(   a letter set can be converted in the string of all its letters. ) CR
    CHAR a LS-EMPTY LS-ADD-LETTER
    CHAR g SWAP LS-ADD-LETTER
    CHAR b SWAP LS-ADD-LETTER
    PAD LS>S PAD COUNT S" abg" ?STR 
    2 BASE ! 11101010 DECIMAL  PAD LS>S PAD COUNT S" acefg" ?STR
}T

T{ .(   a word can be splitted into its nth group and letter. ) CR
    s" group" 0 PAD S>GROUP-LETTER CHAR g ?S PAD COUNT S" ~roup" ?STR
    s" group" 4 PAD S>GROUP-LETTER CHAR p ?S PAD COUNT S" grou~" ?STR
}T

T{ .(   a group dictionary holds groups and their letter sets. ) CR
    GROUP-DICTIONARY gd
    s" horse" gd GD-ADD-WORD
    s" house" gd GD-ADD-WORD
    s" morse" gd GD-ADD-WORD 
    s" worse" gd GD-ADD-WORD
    s" harse" gd GD-ADD-WORD
    s" ~orse" pad gd GD-GROUP>LETTERS PAD COUNT s" hmw" ?STR
    s" h~rse" pad gd GD-GROUP>LETTERS PAD COUNT s" ao" ?STR
    s" ho~se" pad gd GD-GROUP>LETTERS PAD COUNT s" ru" ?STR
    s" hor~e" pad gd GD-GROUP>LETTERS PAD COUNT s" s" ?STR
    s" hors~" pad gd GD-GROUP>LETTERS PAD COUNT s" e" ?STR
}T

