\ letter-set-test.fs

REQUIRE ffl/tst.fs
REQUIRE ../src/letter-set.fs

CR .( letter set ) CR

.(   a letter set that is empty has 0 letters. ) CR
T{
    LS-EMPTY LS-LENGTH@ 0 ?S
}T
.(   after adding a letter, the letter has 1 more letter.  ) CR
T{
    LS-EMPTY 
    CHAR c SWAP LS-ADD-CHAR
    DUP LS-LENGTH@ 1 ?S
    CHAR z SWAP LS-ADD-CHAR
    DUP LS-LENGTH@ 2 ?S
    CHAR g SWAP LS-ADD-CHAR
    DUP LS-LENGTH@ 3 ?S
    DROP
}T
.(   letters in a letter set can be copied in a string. ) CR
T{
    LS-EMPTY
    CHAR c SWAP LS-ADD-CHAR
    CHAR a SWAP LS-ADD-CHAR
    CHAR e SWAP LS-ADD-CHAR
    CHAR y SWAP LS-ADD-CHAR
    CHAR z SWAP LS-ADD-CHAR
    LS-CHARS S" aceyz" ?STR
}T
