\ small-string-test.fs

REQUIRE ffl/tst.fs
REQUIRE ../src/small-string.fs

CR .( small string ) CR
.(   a string of length 5 or less can be stored as a cell value, a small string. ) CR
T{
    S" horse" SMALL-STRING
    SMALL-STRING-S S" horse" ?STR
}T

.(   a small string can be modified by writing one of its chars. ) CR
T{
    S" shark" SMALL-STRING
    CHAR p 4 ROT SMALL-STRING-C!
    SMALL-STRING-S S" sharp" ?STR
}T
.(   the nth char of a small string can be extracted. ) CR
T{
    S" mouse" SMALL-STRING
    2 SMALL-STRING-C@ CHAR u ?S
    S" anvil" SMALL-STRING
    4 SMALL-STRING-C@ CHAR l ?S
}T
