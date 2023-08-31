\ wl-letterset-test.fs
REQUIRE ffl/tst.fs
REQUIRE ../src/wl-letterset.fs

CR .( letterset ) CR

.(   an empty letter set does not contain any letter. ) CR
T{
    CHAR a LS-EMPTY LS-HAS-LETTER? ?FALSE
}T
.(   after adding a letter the letter set has this letter. ) CR
T{
    CHAR a LS-EMPTY LS-ADD-LETTER
    CHAR a SWAP LS-HAS-LETTER? ?TRUE
}T
.(   a letter set can copy all its letters in a string. ) CR
T{
    LS-EMPTY 
    CHAR a SWAP LS-ADD-LETTER
    CHAR b SWAP LS-ADD-LETTER
    CHAR i SWAP LS-ADD-LETTER
    CHAR g SWAP LS-ADD-LETTER
    CHAR e SWAP LS-ADD-LETTER
    CHAR l SWAP LS-ADD-LETTER
    PAD LS>S PAD COUNT S" abegil" ?STR
}T


