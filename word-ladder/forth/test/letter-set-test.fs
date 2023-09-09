\ letter-set-test.fs

REQUIRE ffl/tst.fs
REQUIRE ../src/letter-set.fs

CR .( letter set ) CR

.(   a letter set can be added letters, and copied on a string buffer ) CR
T{
    LS-EMPTY PAD LETTER-SET>S PAD COUNT s" " ?STR
    LS-EMPTY
    CHAR a SWAP LS-ADD-LETTER
    CHAR z SWAP LS-ADD-LETTER
    CHAR t SWAP LS-ADD-LETTER
    PAD LETTER-SET>S PAD COUNT s" atz" ?STR

}T

